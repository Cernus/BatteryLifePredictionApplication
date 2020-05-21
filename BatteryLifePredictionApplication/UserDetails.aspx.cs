using AppFacade.Models;
using BatteryLifePredictionApplication.App_Code;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace BatteryLifePredictionApplication
{
    public partial class UserDetails : Page
    {
        // Redirect to the home page or initialise page before it is presented to the User
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect User if they do not have permission to view this page
            Security.RedirectIfNotAdmin();

            // Populate the page with entity data
            PopulatePage();
        }

        // Populate the page with entity data
        private void PopulatePage()
        {
            List<UserDto> users = UserService.GetUsers();

            if(users.Count > 0)
            {
                UserGridView.DataSource = UserService.GetUsers();
                UserGridView.DataBind();
            }
            else
            {
                hiddenDiv.Visible = true;
            }
        }
    }
}