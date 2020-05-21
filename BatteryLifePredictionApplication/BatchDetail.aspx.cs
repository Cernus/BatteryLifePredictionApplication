using AppFacade.Models;
using BatteryLifePredictionApplication.App_Code;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BatteryLifePredictionApplication
{
    public partial class BatchDetail : Page
    {
        // Redirect to the home page or initialise page before it is presented to the User
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Redirect User if they do not have permission to view this page
                Security.RedirectIfNoQueryString();
                Security.RedirectIfIsGuest();
                int userId = GetUserId();
                Security.RedirectIfUserIdMismatch(userId);

                // Populate the page with entity data
                PopulatePage();

                // Display Success/Error message on page if appropriate
                DisplayMessage();
            }
        }

        // Get User Id of the current User
        private static int GetUserId()
        {
            int userId = 0;
            try
            {
                userId = Int32.Parse(BatchService.GetUserId().ToString());
            }
            catch
            {
                Security.RedirectToHomePage();
            }

            return userId;
        }

        // Populate the page with entity data
        private void PopulatePage()
        {
            int batchId = 0;
            try
            {
                // Get object from BatchApi
                batchId = Int32.Parse(Security.GetQueryString());
                BatchDto batch = BatchService.GetBatch(batchId);

                BatchReferenceTb.Text = batch.Batch_Ref;
            }
            catch
            {
                // ERROR - User contained invalid data
                Response.Redirect("BatchDetail?Id{" + batchId + "}&message=DatabaseError");
            }

            List<BatteryDto> batteries = BatteryService.GetBatteries(batchId);
            if (batteries.Count > 0)
            {
                // If there are batteries in this batch; display the gridview
                BatteryGridView.DataSource = batteries;
                BatteryGridView.DataBind();
            }
            else
            {
                // Otherwise hide the gridview and display an alternative message
                hiddenDiv.Visible = true;
                SubtitleDiv.Visible = false;
                DownloadDiv.Visible = false;
            }
        }

        // Display Success/Error message on page if appropriate
        private void DisplayMessage()
        {
            string queryString = Security.GetQueryString("message");

            switch (queryString)
            {
                case "DatabaseError":
                    MessageLabel.Text = "ERROR: Unable to get batch data from the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "UpdateError":
                    MessageLabel.Text = "ERROR: Unable to update batch in the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "DeleteError":
                    MessageLabel.Text = "ERROR: Unable to delete batch in the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "Error":
                    MessageLabel.Text = "ERROR: Unable to upload batteries from file";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "DownloadError":
                    MessageLabel.Text = "ERROR: Unable to download batteries as spreadsheet";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "IdError":
                    MessageLabel.Text = "ERROR: Unexpected value read as BatteryId";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "PredictionError":
                    MessageLabel.Text = "ERROR: Unable to predict lifetime of battery";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "BatchPredictionError":
                    MessageLabel.Text = "ERROR: Unable to predict lifetime of batch";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "PredictionSuccess":
                    MessageLabel.Text = "SUCCESS: Prediction made for battery's lifetime";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

                case "BatchUpdateSuccess":
                    MessageLabel.Text = "SUCCESS: Successfully updated batch";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

                case "BatteriesUploadSuccess":
                    MessageLabel.Text = "SUCCESS: Successfully uploaded batteries to batch";
                    MessageLabel.CssClass = "successText";
                    MessageLabel.Visible = true;
                    return;

            }
        }

        // Navigate to the Create Battery page
        protected void CreateBatteryBtn_Click(object sender, EventArgs e)
        {
            string batchId = Security.GetQueryString();
            Response.Redirect("CreateBattery?id=" + batchId);
        }

        // Update this User in the database
        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            // Create Batch Object
            int batchId = Int32.Parse(Security.GetQueryString());
            BatchDto batch = BatchService.GetBatch(batchId);

            batch.Batch_Ref = BatchReferenceTb.Text;

            // Add to database
            if (BatchService.UpdateBatch(batch))
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=BatchUpdateSuccess");
            }
            else
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=UpdateError");
            }
        }

        // Soft-delete this User in the database
        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            int batchId = Int32.Parse(Security.GetQueryString());

            // Soft delete batch in Database
            if (BatchService.DeleteBatch(batchId))
            {
                Response.Redirect("Default.aspx?message=BatchDeleteSuccess");
            }
            else
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=DeleteError");
            }
        }

        // Upload CSV data as list of Batteries and add to the database
        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            int batchId = Int32.Parse(Security.GetQueryString());

            if (BatchService.UploadFromFile(BatteryUpload))
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=BatteriesUploadSuccess");
            }
            else
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=Error");
            }
        }

        // Download all Battery data as a CSV
        protected void DownloadBtn_Click(object sender, EventArgs e)
        {
            int batchId = 0;
            try
            {
                batchId = Int32.Parse(Security.GetQueryString());
            }
            catch
            {
                Security.RedirectToHomePage();
            }

            if (BatchService.DownloadSpreadsheet(batchId))
            {
                // Do nothing
            }
            else
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=DownloadError");
            }
        }

        // Predict the remaining lifetime for a Battery in the gridview
        protected void PredictLB_Command(object sender, CommandEventArgs e)
        {
            int batchId = Int32.Parse(Security.GetQueryString());

            // Get batteryId
            int batteryId = 0;
            try
            {
                batteryId = Int32.Parse(e.CommandArgument.ToString());
            }
            catch
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=IdError");
            }

            // Get prediction
            double prediction = 0;
            try
            {
                prediction = Double.Parse(PredictService.Single(batteryId).ToString());
            }
            catch
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=PredictionError");
            }

            // Update battery in database
            BatteryDto battery = BatteryService.GetBattery(batteryId);
            battery.Lifetime = prediction;

            if (BatteryService.UpdateBattery(battery))
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=PredictionSuccess");
            }
            else
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=UpdateError");
            }
        }

        // Request a Batch Prediction from Azure
        protected void BatchPredictBtn_Click(object sender, EventArgs e)
        {
            int batchId = Int32.Parse(Security.GetQueryString());
            if (PredictService.Batch(batchId))
            {
                Response.Redirect("Default.aspx?message=BatchPredictionSuccess");
            }
            else
            {
                Response.Redirect("BatchDetail.aspx?id=" + batchId + "&message=BatchPredictionError");
            }
        }
    }
}