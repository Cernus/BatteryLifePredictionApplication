using AppFacade.Models;
using BatteryLifePredictionApplication.App_Code;
using System;
using System.Web.UI;

namespace BatteryLifePredictionApplication
{
    public partial class UserDetail : Page
    {
        // Redirect to the home page or initialise page before it is presented to the User
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Redirect User if they do not have permission to view this page
                Security.RedirectIfNoQueryString();
                Security.RedirectIfIsGuest();
                Security.RedirectIfUserIdMismatch();

                // Populate the page with entity data
                PopulatePage();

                // Display Success/Error message on page if appropriate
                DisplayMessage();
            }
        }

        // Populate the page with entity data
        private void PopulatePage()
        {
            int userId = 0;
            try
            {
                // Get object from UserApi
                userId = Int32.Parse(Security.GetQueryString());
                UserDto user = UserService.GetUser(userId);

                UsernameTb.Text = user.Username;
                PasswordTb.Text = user.Password;
                JobTitleTb.Text = user.JobTitle;
                EmailAddressTb.Text = user.EmailAddress;
            }
            catch
            {
                // ERROR - User contained invalid data
                Response.Redirect("UserDetail?Id{" + userId + "}&message=DatabaseError");
            }
        }

        // Display Success/Error message on page if appropriate
        private void DisplayMessage()
        {
            string queryString = Security.GetQueryString("message");

            switch (queryString)
            {
                case "DatabaseError":
                    MessageLabel.Text = "ERROR: Unable to get user data from the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "UpdateError":
                    MessageLabel.Text = "ERROR: Unable to update user in the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "DeleteError":
                    MessageLabel.Text = "ERROR: Unable to delete user in the database";
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

                case "UserUpdateSuccess":
                    MessageLabel.Text = "SUCCESS: Successfully updated user";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;
            }
        }

        // Update User in the database
        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            // Create User Object
            int userId = Int32.Parse(Security.GetQueryString());
            UserDto user = UserService.GetUser(userId);

            string currentUsername = UsernameTb.Text;

            user.Username = UsernameTb.Text;
            user.Password = PasswordTb.Text;
            user.JobTitle = JobTitleTb.Text;
            user.EmailAddress = EmailAddressTb.Text;

            // Check that username does not already exist in the database and is not different to the current user's name
            bool? usernameExists = UserService.UsernameExists(user.Username);
            if (usernameExists == true && currentUsername != user.Username)
            {
                Response.Redirect("UserDetail.aspx?id=" + userId + "&message=UsernameExists");
            }
            if (usernameExists == null)
            {
                Response.Redirect("UserDetail.aspx?id=" + userId + "&message=BadRequest");
            }

            // update in database
            if (UserService.UpdateUser(user))
            {
                Response.Redirect("UserDetail.aspx?id=" + userId + "&message=UserUpdateSuccess");
            }
            else
            {
                Response.Redirect("UserDetail.aspx?id=" + userId + "&message=UpdateError");
            }
        }

        // Soft-delete User in the database
        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            int userId = Int32.Parse(Security.GetQueryString());

            // Soft delete user in Database
            if (UserService.DeleteUser(userId))
            {
                Response.Redirect("Default.aspx?message=UserDeleteSuccess");
            }
            else
            {
                Response.Redirect("UserDetail.aspx?id=" + userId + "&message=DeleteError");
            }
        }
    }
}