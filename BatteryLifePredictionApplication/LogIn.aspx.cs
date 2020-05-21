using System;
using System.Web.UI;
using BatteryLifePredictionApplication.App_Code;

namespace BatteryLifePredictionApplication
{
    public partial class LogIn : Page
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
                case "InvalidCredentials":
                    MessageLabel.Text = "ERROR: Incorrect Log In credentials";
                    MessageLabel.Visible = true;
                    return;

                case "InactiveUser":
                    MessageLabel.Text = "ERROR: This account has been deactivated";
                    MessageLabel.Visible = true;
                    return;
            }
        }

        // Log In using the provided credentials
        protected void LogInBtn_Click(object sender, EventArgs e)
        {
            string username = usernameLabel.Text;
            string password = passwordLabel.Text;

            Security.Authenticate(username, password);
        }
    }
}