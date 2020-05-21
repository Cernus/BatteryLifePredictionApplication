using BatteryLifePredictionApplication.App_Code;
using System;
using System.Web.UI;

namespace BatteryLifePredictionApplication
{
    public partial class _Default : Page
    {
        // Initialise page before it is presented to the User
        protected void Page_Load(object sender, EventArgs e)
        {
            // Display Success/Error message on page if appropriate
            DisplayMessage();
        }

        // Display Success/Error message on page if appropriate
        private void DisplayMessage()
        {
            string queryString = Security.GetQueryString("message");

            switch (queryString)
            {
                case "UserCreateSuccess":
                    MessageLabel.Text = "SUCCESS: You have successfully registered";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

                case "UserDeleteSuccess":
                    MessageLabel.Text = "SUCCESS: Successfully deleted user";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

                case "BatchCreateSuccess":
                    MessageLabel.Text = "SUCCESS: Batch successfully created";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

                case "BatchDeleteSuccess":
                    MessageLabel.Text = "SUCCESS: Successfully deleted batch";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

                case "BatteryCreateSuccess":
                    MessageLabel.Text = "SUCCESS: Battery successfully created";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

                case "BatteryUpdateSuccess":
                    MessageLabel.Text = "SUCCESS: Successfully updated Battery";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

                case "BatteryDeleteSuccess":
                    MessageLabel.Text = "SUCCESS: Successfully deleted Battery";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

                case "BatchPredictionSuccess":
                    MessageLabel.Text = "SUCCESS: Prediction job for batch sent to Azure";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

                case "ResultsNotDownloaded":
                    MessageLabel.Text = "NOTICE: Unable to view page as results from batch request have not finished downloading";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;
            }
        }
    }
}