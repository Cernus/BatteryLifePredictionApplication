using AppFacade.Models;
using BatteryLifePredictionApplication.App_Code;
using System;
using System.Web.UI;

namespace BatteryLifePredictionApplication
{
    public partial class CreateBatch : Page
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
                Security.RedirectIfUserIsNotActive();

                // Display Success/Error message on page if appropriate
                DisplayMessage();
            }
        }

        // Display Success/Error message on page if appropriate
        private void DisplayMessage()
        {
            string queryString = Security.GetQueryString("message");

            switch (queryString)
            {
                case "Error":
                    MessageLabel.Text = "ERROR: Unable to create batch in the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;
            }
        }

        // Create Batch in the database
        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            BatchDto batch = null;
            try
            {
                // Create Batch Object
                batch = new BatchDto
                {
                    Batch_Ref = BatchReferenceTb.Text,
                    DecisionForestRegressionJobId = null,
                    LinearRegressionJobId = null,
                    UserId = Int32.Parse(Security.GetQueryString())
            };
            }
            catch
            {
                Response.Redirect("CreateBatch?message=Error");
            }

            // Add to database
            if (BatchService.CreateBatch(batch))
            {
                Response.Redirect("Default?message=BatchCreateSuccess");
            }
            else
            {
                Response.Redirect("CreateBatch?message=Error");
            }
        }
    }
}