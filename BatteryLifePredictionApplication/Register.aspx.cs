using AppFacade.Models;
using BatteryLifePredictionApplication.App_Code;
using System;
using System.Web;
using System.Web.UI;

namespace BatteryLifePredictionApplication
{
    public partial class Register : Page
    {
        // Redirect to the home page or initialise page before it is presented to the User
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect User if they do not have permission to view this page
            Security.RedirectIfNotGuest();

            // Display Success/Error message on page if appropriate
            DisplayMessage();
        }

        // Display Success/Error message on page if appropriate
        private void DisplayMessage()
        {
            string queryString = Security.GetQueryString("message");

            switch (queryString)
            {
                case "CreateError":
                    MessageLabel.Text = "ERROR: Unable to create user in the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "UsernameExists":
                    MessageLabel.Text = "ERROR: Unable to create account as this username has already been taken";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "BadRequest":
                    MessageLabel.Text = "ERROR: A bad request was received from User Api";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;
            }
        }

        // Create User in the database and Log In as this User
        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            // Create User Object
            UserDto user = new UserDto
            {
                Username = UsernameTb.Text,
                Password = PasswordTb.Text,
                JobTitle = JobTitleTb.Text,
                EmailAddress = EmailAddressTb.Text
            };

            // Check that username does not already exist in the database
            bool? usernameExists = UserService.UsernameExists(user.Username);
            if (usernameExists == true)
            {
                Response.Redirect("Register?message=UsernameExists");
            }
            if (usernameExists == null)
            {
                Response.Redirect("Register?message=BadRequest");
            }

            // Add to database
            if (UserService.CreateUser(user))
            {
                Security.Authenticate(user.Username, user.Password);
                Response.Redirect("Default?message=UserCreateSuccess");
            }
            else
            {
                Response.Redirect("Register?message=CreateError");
            }
        }
    }
}