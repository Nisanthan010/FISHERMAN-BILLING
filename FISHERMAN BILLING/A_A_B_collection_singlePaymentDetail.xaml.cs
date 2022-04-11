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
    public partial class A_A_B_collection_singlePaymentDetail : ContentPage
    {
        public A_A_B_collection_singlePaymentDetail()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Single_payment_detail_bill.ItemsSource = await Add_cumulated_over_all_total();
            await Total_without_reduction();
        }
        public B_customerdetail_save Last_selected;
        public async void Single_payment_detail_bill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = await MainPage.File.Delete_all_Reference_Customer();
            Last_selected = e.CurrentSelection.FirstOrDefault() as B_customerdetail_save;
            if (Last_selected != null)
            {
                Reference_Customer temp_selected = new Reference_Customer();
                temp_selected.Compare_ref_customer_id = Last_selected.CustomerId;
                temp_selected.Reference_date_compare = Last_selected.SetVerification_date_kg;
                temp_selected.Overall_total_Kg_date_db = Last_selected.Overall_total_dp;
                _ = await MainPage.File.Add_Reference_Customer(temp_selected);
                await Navigation.PushAsync(new A_A_B_A_add_single_kg_amoount());
            }
        }
        #region Add_cummulate_total
        public async Task<List<B_customerdetail_save>> Add_cumulated_over_all_total()
        {
            List<B_customerdetail_save> Cummulated_customer_data = await MainPage.File.Get_customerdetail_save();
            List<B_customerdetail_save> listofCustomer = new List<B_customerdetail_save>();
            string current_date = View_Date(await Date_Count(), await ListOfDate());
            float temp_total = 0;
            foreach (B_customerdetail_save data in Cummulated_customer_data)
            {
                if (data.Customer_detail_date_dp == current_date)
                {
                    listofCustomer.Add(data);
                    bool Is_detail = float.TryParse(data.Overall_total_Amount, out float O1);
                    if (Is_detail)
                    {
                        temp_total = O1 + temp_total;
                    }

                }
            }

            Over_all_amount.Text = Convert.ToString(temp_total);
            return listofCustomer;
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
        public async Task Total_without_reduction()
        {
            C_TotalWithoutReduction Without_reduction = new C_TotalWithoutReduction();
            Without_reduction.TotalWithoutReduction_date_dp = View_Date(await Date_Count(), await ListOfDate());
            Without_reduction.TotalWithoutReduction_dp = Over_all_amount.Text;
            _ = await MainPage.File.Add_C_TotalWithoutReduction(Without_reduction);
        }
    }
}