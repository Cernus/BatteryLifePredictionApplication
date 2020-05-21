using AppFacade.Models;
using BatteryLifePredictionApplication.App_Code;
using System;
using System.Web.UI;

namespace BatteryLifePredictionApplication
{
    public partial class BatteryDetail : Page
    {
        // Redirect to the home page or initialise page before it is presented to the User
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Redirect User if they do not have permission to view this page
                Security.RedirectIfNoQueryString();

                int? batchId = BatteryService.GetBatchId();
                int? userId = BatchService.GetUserId(batchId);

                Security.RedirectIfUserIdMismatch(userId);
                Security.RedirectIfIsGuest();

                // Populate the page with entity data
                PopulatePage();

                // Display Success/Error message on page if appropriate
                DisplayMessage();
            }
        }

        // Populate the page with entity data
        private void PopulatePage()
        {
            int batteryId = 0;
            try
            {
                // Get object from BatteryApi
                batteryId = Int32.Parse(Security.GetQueryString());
                BatteryDto battery = BatteryService.GetBattery(batteryId);

                BatteryReferenceTb.Text = battery.Battery_Ref;
                CycleIndexTb.Text = battery.Cycle_Index.ToString();
                ChargeCapacityTb.Text = battery.Charge_Capacity.ToString();
                DischargeCapacityTb.Text = battery.Discharge_Capacity.ToString();
                ChargeEnergyTb.Text = battery.Charge_Energy.ToString();
                DischargeEnergyTb.Text = battery.Discharge_Energy.ToString();
                dvdtTb.Text = battery.dvdt.ToString();
                InternalResistanceTb.Text = battery.Internal_Resistance.ToString();

            }
            catch
            {
                // ERROR - User contained invalid data
                Response.Redirect("BatteryDetail?Id{" + batteryId + "}&message=DatabaseError");
            }
        }

        // Display Success/Error message on page if appropriate
        private void DisplayMessage()
        {
            string queryString = Security.GetQueryString("message");

            switch (queryString)
            {
                case "DatabaseError":
                    MessageLabel.Text =
                        "Unable to get battery data from the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "UpdateError":
                    MessageLabel.Text =
                        "Unable to update battery in the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                case "DeleteError":
                    MessageLabel.Text =
                        "Unable to delete battery in the database";
                    MessageLabel.CssClass = "validationText";
                    MessageLabel.Visible = true;
                    return;

                default:
                    return;
            }
        }

        // Update Battery in the database
        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            // Create Battery Object
            int batteryId = Int32.Parse(Security.GetQueryString());
            BatteryDto battery = BatteryService.GetBattery(batteryId);

            battery.Battery_Ref = BatteryReferenceTb.Text;
            battery.Cycle_Index = Int32.Parse(CycleIndexTb.Text);
            battery.Charge_Capacity = Double.Parse(ChargeCapacityTb.Text);
            battery.Discharge_Capacity = Double.Parse(DischargeCapacityTb.Text);
            battery.Charge_Energy = Double.Parse(ChargeEnergyTb.Text);
            battery.Discharge_Energy = Double.Parse(DischargeEnergyTb.Text);
            battery.dvdt = Double.Parse(dvdtTb.Text);
            battery.Internal_Resistance = Double.Parse(InternalResistanceTb.Text);
            battery.BatchId = Int32.Parse(BatteryService.GetBatchId().ToString());
            battery.Lifetime = null;

            // update in database
            if (BatteryService.UpdateBattery(battery))
            {
                Response.Redirect("Default.aspx?message=BatteryUpdateSuccess");
            }
            else
            {
                Response.Redirect("BatteryDetail.aspx?id=" + batteryId + "&message=UpdateError");
            }
        }

        // Soft-delete Battery in the database
        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            int batteryId = Int32.Parse(Security.GetQueryString());

            // Soft delete battery in Database
            if (BatteryService.DeleteBattery(batteryId))
            {
                Response.Redirect("Default.aspx?message=BatteryDeleteSuccess");
            }
            else
            {
                Response.Redirect("BatteryDetail.aspx?id=" + batteryId + "&message=DeleteError");
            }
        }
    }
}