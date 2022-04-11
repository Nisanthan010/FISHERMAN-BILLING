using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FISHERMAN_BILLING.DATABASE;

namespace FISHERMAN_BILLING
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class A_A_C_B_B_Extra_reduction : ContentPage
    {
        public A_A_C_B_B_Extra_reduction()
        {
            InitializeComponent();
        }

        private async void Extra_pay_Clicked(object sender, EventArgs e)
        {
            if (Extra_reduction_name.Text == null && Extra_reduction_amount.Text == null)
            {
                Extra_reduction_name.Text = "0";
                Extra_reduction_amount.Text = "0";
            }
            else
            {
                if(Extra_reduction_name.Text == null || Extra_reduction_amount.Text == null)
                {
                    await DisplayAlert("WARNING", "PLEASE ENTER THE BOTH FIELDS", "OK");
                }
                else
                {
                    await Save_extra_reduction();
                }
                Extra_reduction_name.Text = string.Empty;
                Extra_reduction_amount.Text = string.Empty;
            }
        }

        #region current date
        public string View_Date(int Date_Count, List<B_DateOnly> data_kg)
        {
            string Current_date = null;

            foreach (B_DateOnly data in data_kg)
            {
                if (data.Id == Date_Count)
                {
                    Current_date = data.Entry_date_db;
                }
            }
            return Current_date;
        }
        private async Task<List<B_DateOnly>> ListOfDate()
        {
            return await MainPage.File.Get_DateOnly();
        }
        private async Task<int> Date_Count()
        {
            List<B_DateOnly> ListOfDate = await MainPage.File.Get_DateOnly();
            int Date_Count = ListOfDate.Count();
            return Date_Count;
        }
        #endregion
        public async Task Save_extra_reduction()
        {
            E_extra_reduction Extra_Reduction = new E_extra_reduction();
            Extra_Reduction.Extra_reduction_date_dp = View_Date(await Date_Count(), await ListOfDate());
            Extra_Reduction.Extra_reduction_name__dp = Extra_reduction_name.Text;
            Extra_Reduction.Extra_reduction_amount_dp = Extra_reduction_amount.Text;
            _ = await MainPage.File.Add_E_extra_reduction(Extra_Reduction);
        }
    }
}