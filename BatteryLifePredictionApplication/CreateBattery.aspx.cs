using AppFacade.Models;
using BatteryLifePredictionApplication.App_Code;
using System;
using System.Web.UI;

namespace BatteryLifePredictionApplication
{
    public partial class CreateBattery : Page
    {
        // Redirect to the home page or initialise page before it is presented to the User
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Redirect User if they do not have permission to view this page
                Security.RedirectIfNoQueryString();

                int? userId = BatchService.GetUserId();
                Security.RedirectIfUserIdMismatch(userId);
                Security.RedirectIfIsGuest();
                Security.RedirectIfBatchIsNotActive();

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
                    MessageLabel.Text = "ERROR: Unable to create battery in the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;
            }
        }

        // Create Battery in the database
        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            BatteryDto battery = null;
            int batchId = 0;
            try
            {
                batchId = Int32.Parse(Security.GetQueryString());

                // Create Battery Object
                battery = new BatteryDto
                {
                    Battery_Ref = BatteryReferenceTb.Text,
                    Cycle_Index = Int32.Parse(CycleIndexTb.Text),
                    Charge_Capacity = Double.Parse(ChargeCapacityTb.Text),
                    Discharge_Capacity = Double.Parse(DischargeCapacityTb.Text),
                    Charge_Energy = Double.Parse(ChargeEnergyTb.Text),
                    Discharge_Energy = Double.Parse(DischargeEnergyTb.Text),
                    dvdt = Double.Parse(dvdtTb.Text),
                    Internal_Resistance = Double.Parse(InternalResistanceTb.Text),
                    BatchId = batchId,
                    Lifetime = null
                };
            }
            catch
            {
                Response.Redirect("CreateBattery?id=" + batchId + "&message=Error");
            }

            // Add to database

            if (BatteryService.CreateBattery(battery))
            {
                Response.Redirect("Default?message=BatteryCreateSuccess");
            }
            else
            {
                Response.Redirect("CreateBattery?id" + batchId + "&message=Error");
            }
        }
    }
}