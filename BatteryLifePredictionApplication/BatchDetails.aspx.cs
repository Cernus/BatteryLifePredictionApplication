using AppFacade.Models;
using BatteryLifePredictionApplication.App_Code;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BatteryLifePredictionApplication
{
    public partial class BatchDetails : Page
    {
        // Redirect to the home page or initialise page before it is presented to the User
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                // Redirect User if they do not have permission to view this page
                Security.RedirectIfIsGuest();

                // Populate the page with entity data
                PopulatePage();
                PopulateDDL();
            }
        }

        // Populate the page with entity data
        private void PopulatePage()
        {
            int userId = 0;
            try
            {
                userId = Security.GetCurrentUserId();
            }
            catch
            {
                Security.RedirectToHomePage();
            }
            List<BatchDto> batches = BatchService.GetBatches(userId);

            BatchGridView.DataSource = null;
            BatchGridView.DataBind();
            hiddenDiv.Visible = false;

            if (batches.Count > 0)
            {
                BatchGridView.DataSource = batches;
                BatchGridView.DataBind();
            }
            else
            {
                hiddenDiv.Visible = true;
            }
        }

        // Populate the page for a specific User with entity data
        private void PopulatePage(int userId)
        {
            List<BatchDto> batches = BatchService.GetBatches(userId);

            BatchGridView.DataSource = null;
            BatchGridView.DataBind();
            hiddenDiv.Visible = false;

            if (batches.Count > 0)
            {
                BatchGridView.DataSource = batches;
                BatchGridView.DataBind();
            }
            else
            {
                hiddenDiv.Visible = true;
            }
        }

        // Populate the DDL with a list of all the active Users in the system
        private void PopulateDDL()
        {
            if (Security.IsAdmin())
            {
                DDLdiv.Visible = true;
                List<UserDto> users = UserService.GetUsers();

                if (users.Count > 0)
                {
                    foreach (UserDto user in users)
                    {
                        UserDDL.Items.Add(new ListItem(user.Username, user.UserId.ToString()));
                    }
                }
            }
        }

        // Redirect to the Create Batch page
        protected void CreateBatchBtn_Click(object sender, EventArgs e)
        {
            string userId;
            if (Security.IsAdmin())
            {
                userId = Int32.Parse(UserDDL.SelectedItem.Value).ToString();
            }
            else
            {
                userId = Security.GetCurrentUserId().ToString();
            }

            Response.Redirect("CreateBatch?id=" + userId);
        }

        // Repopulate the page when a User is selected from the DDL
        protected void UserDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Int32.Parse(UserDDL.SelectedItem.Value);
                PopulatePage(id);
            }
            catch
            {
                // Ignore
            }
        }
    }
}