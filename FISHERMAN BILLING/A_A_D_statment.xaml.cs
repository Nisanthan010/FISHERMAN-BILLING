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
    public partial class A_A_D_statment : ContentPage
    {
        public A_A_D_statment()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Appearing_detail();
            _ = await MainPage.File.Delete_all_reference_statment();
        }
        #region current date
        public async Task<string> View_Date(int Date_Count, List<B_DateOnly> data_kg)
        {
            string Current_date = null;
            Final_Detail Current_final_detail = await Get_selected_item_date();
            if (Current_final_detail.Final_detai_date_dp==null)
            {
                foreach (B_DateOnly data in data_kg)
                {
                    if (data.Id == Date_Count)
                    {
                        Current_date = data.Entry_date_db;
                    }
                }
            }
            else
            {
                Current_date = Current_final_detail.Final_detai_date_dp;              
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
        #region FIND SELECTED DATE
        public async Task<Final_Detail> Get_selected_item_date()
        {
            List<Final_Detail> ListOf_Final_detail = await MainPage.File.Get_finalDetail();
            List<Reference_statment> selected_date_list = await MainPage.File.Get_reference_statment_Detail();
            Final_Detail Selected_customer = new Final_Detail();
            foreach (Reference_statment data in selected_date_list) 
            {
                foreach (Final_Detail final_detail in ListOf_Final_detail)
                {
                    if (data.Reference_statment_id == final_detail.Final_statment_id && data.Reference_date_statment == final_detail.Final_detai_date_dp)
                    {
                        Selected_customer = final_detail;
                    }
                }
            }
            return Selected_customer;
        }
        #endregion
        #region TotalWithout_reduction
        public async Task<string> DisplayStatment_total_withoutreduction()
        {
            string display = "null";
            List<C_TotalWithoutReduction> find_today_date = await MainPage.File.Get_totalWithOutReduction();
            foreach(C_TotalWithoutReduction data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.TotalWithoutReduction_date_dp)
                {
                    display = data.TotalWithoutReduction_dp;
                }
            }
            return display;
        }
        #endregion
        #region worker_reduction
        public async Task<string> DisplayStatment_worker_reduction()
        {
            string display = "null";
            List<E_worker_reduction> find_today_date = await MainPage.File.Get_worker_reduction();
            foreach (E_worker_reduction data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.Worker_reduction_date_dp)
                {
                    display = data.Worker_reduction_amount_dp;
                }
            }
            return display;
        }
        public async Task<string> DisplayStatment_driver_reduction()
        {
            string display = "null";
            List<E_worker_reduction> find_today_date = await MainPage.File.Get_worker_reduction();
            foreach (E_worker_reduction data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.Worker_reduction_date_dp)
                {
                    display = data.Driver_reduction_amount_dp;
                }
            }
            return display;
        }
        public async Task<string> DisplayStatment_worker_no_reduction()
        {
            string display = "null";
            List<E_worker_reduction> find_today_date = await MainPage.File.Get_worker_reduction();
            foreach (E_worker_reduction data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.Worker_reduction_date_dp)
                {
                    display = data.Worker_reduction_no_dp;
                }
            }
            return display;
        }
        public async Task<string> DisplayStatment_driver_no_reduction()
        {
            string display = "null";
            List<E_worker_reduction> find_today_date = await MainPage.File.Get_worker_reduction();
            foreach (E_worker_reduction data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.Worker_reduction_date_dp)
                {
                    display = data.Driver_reduction_no_dp;
                }
            }
            return display;
        }
        public async Task<string> DisplayStatment_total_reduction_worker()
        {
            string display = "null";
            List<E_worker_reduction> find_today_date = await MainPage.File.Get_worker_reduction();
            foreach (E_worker_reduction data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.Worker_reduction_date_dp)
                {
                    display = data.Worker_total_reduction_dp;
                }
            }
            return display;
        }
        #endregion
        #region Final_detail_amount
        public async Task<string> DisplayStatment_final_detail_total()
        {
            string display = "null";
            List<Final_Detail> find_today_date = await MainPage.File.Get_finalDetail();
            foreach (Final_Detail data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.Final_detai_date_dp)
                {
                    display = data.Final_detai_amount_dp;
                }
            }
            return display;
        }
        public async Task<string> DisplayStatment_final_detail_reduction()
        {
            string display = "null";
            List<Final_Detail> find_today_date = await MainPage.File.Get_finalDetail();
            foreach (Final_Detail data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.Final_detai_date_dp)
                {
                    display = data.Final_detai_reduction_dp;
                }
            }
            return display;
        }
        #endregion
        #region customer detail collect
        public async Task Customer_detail_collection()
        {
            List<B_customerdetail_save> Display_data = new List<B_customerdetail_save>();
            List<B_customerdetail_save> find_today_date = await MainPage.File.Get_customerdetail_save();
            foreach (B_customerdetail_save data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.Customer_detail_date_dp)
                {
                    Display_data.Add(data);
                }
            }
            Single_payment_detail_bills.ItemsSource = Display_data;
        }
        #endregion
        #region Extra_reduction_collectionview
        public async Task<string> DisplayStatment_diesel_reduction()
        {
            string display = "null";
            List<E_PetrolReduction> find_today_date = await MainPage.File.Get_petrol();
            foreach (E_PetrolReduction data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.Petrol_amount_date)
                {
                    display = data.Petrol_amount;
                }
            }
            return display;
        }
        public async Task Extra_reduction_collect()
        {
            List<E_extra_reduction> Display_data = new List<E_extra_reduction>();
            List<E_extra_reduction> find_today_date = await MainPage.File.Get_extra_reduction();
            foreach (E_extra_reduction data in find_today_date)
            {
                if (await View_Date(await Date_Count(), await ListOfDate()) == data.Extra_reduction_date_dp)
                {
                    Display_data.Add(data);
                }
            }
            Extra_reduction_collecting.ItemsSource = Display_data;
        }
        #endregion
        public async Task Appearing_detail()
        {
            Date_statement.Text = await View_Date(await Date_Count(), await ListOfDate());
            final_amount.Text = await DisplayStatment_final_detail_total();
            Total_income_amount.Text = await DisplayStatment_total_withoutreduction();
            total_reduction_amount.Text = await DisplayStatment_final_detail_reduction();
            worker_total_pay_amount.Text = await DisplayStatment_total_reduction_worker();
            no_of_person.Text = await DisplayStatment_worker_no_reduction();
            worker_per_amount.Text = await DisplayStatment_worker_reduction();
            per_driver_amount.Text = await DisplayStatment_driver_reduction();
            diesel_amount.Text = await DisplayStatment_diesel_reduction();
            await Extra_reduction_collect();
            await Customer_detail_collection();
        }
    }
}