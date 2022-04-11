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
    public partial class A_A_B_A_add_single_kg_amoount : ContentPage
    {
        public int Customer_Id { get; set; }
        public string Temp_total_kg { get; set; }
        public A_A_B_A_add_single_kg_amoount()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _ = await Get_selected_item();
        }
        private async void ENTER_PER_KG_RUPEES_Clicked(object sender, EventArgs e)
        {
            bool Is_check_enter = float.TryParse(Enter_in_rupees.Text, out float r1);
            if (Is_check_enter)
            {
                bool Is_check_enter_kg = float.TryParse(Total_kg_amount.Text, out float r2);
                if (Is_check_enter && Is_check_enter_kg)
                {
                    Convert_calculation(r1, r2);
                    await View_customer();
                }

            }
            else
            {
                await DisplayAlert("WARNING", "PLEASE ENTER THE RUPEES", "OK");

            }
        }
        #region calculation
        private void Convert_calculation(float r1, float r2)
        {
            float Convert_into_kg_amount = r1 * r2;
            convert_into_amount.Text = Convert.ToString(Convert_into_kg_amount);
        }
        #endregion
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
        #region Edit customer
        public async Task View_customer()
        {
            B_customerdetail_save Current_data = await Get_selected_item();
            Current_data.Overall_total_Amount = convert_into_amount.Text;
            _ = await MainPage.File.Update_B_customerdetail_save(Current_data);
        }

        #endregion
        #region FIND SELECTED ITEAM
        public async Task<B_customerdetail_save> Get_selected_item()
        {
            List<B_customerdetail_save> ListOfcustomer = await MainPage.File.Get_customerdetail_save();
            List<Reference_Customer> selected_customer_list = await MainPage.File.Get_Reference_Customer();
            B_customerdetail_save Selected_customer = new B_customerdetail_save();
            foreach(B_customerdetail_save customer in ListOfcustomer)
            {
                foreach(Reference_Customer data in selected_customer_list)
                {
                    if (data.Compare_ref_customer_id == customer.CustomerId && data.Reference_date_compare == customer.SetVerification_date_kg)
                    {
                        Selected_customer = customer;
                        Total_kg_amount.Text = Selected_customer.Overall_total_dp;
                    }
                }
            }
            return Selected_customer;
        }
        #endregion
    }
}