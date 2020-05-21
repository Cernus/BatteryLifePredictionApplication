using BatteryLifePredictionApplication.App_Code;
using System;
using System.Web.UI;

namespace BatteryLifePredictionApplication
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideMenuButtons();
        }

        protected void LogOutLink_Click(object sender, EventArgs e)
        {
            Security.SignOut();
        }

        protected void ProfileLink_Click(object sender, EventArgs e)
        {
            int id = Security.GetCurrentUserId();
            Response.Redirect("UserDetail?id=" + id);
        }

        private void HideMenuButtons()
        {
            bool isLoggedIn = Security.IsLoggedIn();
            bool isAdmin = Security.IsAdmin();

            if (!isAdmin)
            {
                if (isLoggedIn)
                {
                    // Hide buttons for User
                    LogInLi.Visible = false;
                    RegisterLi.Visible = false;
                }
                else
                {
                    // Hide buttons for Guest
                    LogOutLi.Visible = false;
                    ProfileLi.Visible = false;
                    BatchDetailsLi.Visible = false;
                }

                // Hide buttons for Staff and Guest
                UserDetailsLi.Visible = false;
            }
        }
    }
}