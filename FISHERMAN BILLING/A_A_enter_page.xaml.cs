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
    public partial class A_A_enter_page : ContentPage
    {
        public A_A_enter_page()
        {
            InitializeComponent();
        }

        private async void NewBill_button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new A_A_A_bill_payment_page());
        }

        private async void DetailPayment_button_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new A_A_B_collection_singlePaymentDetail());
        }

        private async void Expensive_button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new A_A_C_B_reduction_detail());
        }

        private async void SingleStatment_button_Clicked(object sender, EventArgs e)
        {
            await Final_overall_total();       
            await Navigation.PushAsync(new A_A_D_statment());
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
        #region EXTRA REDUCTION TOTAL
        public async Task<float> Extra_reduction_total()
        {
            float total_extra_redution = 0;
            List<E_extra_reduction> Extra_reduction_findTotal = await MainPage.File.Get_extra_reduction();
           foreach(E_extra_reduction data in Extra_reduction_findTotal)
            {
                if (View_Date(await Date_Count(), await ListOfDate()) == data.Extra_reduction_date_dp)
                {
                    _ = float.TryParse(data.Extra_reduction_amount_dp, out float r1);
                    total_extra_redution = r1 + total_extra_redution;
                }
            }
            return total_extra_redution;
        }
        #endregion
        #region WORKER AND PETROL
        public async Task<float> Find_petrol_reduction()
        {
            float r1 = 0;
            List<E_PetrolReduction> Petrol_reduction = await MainPage.File.Get_petrol();
            foreach(E_PetrolReduction data in Petrol_reduction)
            {
                if (View_Date(await Date_Count(), await ListOfDate()) == data.Petrol_amount_date)
                {
                    _ = float.TryParse(data.Petrol_amount, out r1);
                }
            }
            return r1;
        }
        public async Task<float> Find_worker_reduction()
        {
            float r1 = 0;
            List<E_worker_reduction> Petrol_reduction = await MainPage.File.Get_worker_reduction();
            foreach (E_worker_reduction data in Petrol_reduction)
            {
                if (View_Date(await Date_Count(), await ListOfDate()) == data.Worker_reduction_date_dp)
                {
                    _ = float.TryParse(data.Worker_total_reduction_dp, out r1);
                }
            }
            return r1;
        }
        #endregion
        #region TOTAL REDUCTION
        public async Task<string> Finally_reduction_total()
        {
            float total_reduction = await Extra_reduction_total() + await Find_petrol_reduction() + await Find_worker_reduction();
            return Convert.ToString(total_reduction);
        }
        #endregion
        #region FINAL OVERALL TOTAL
        public async Task<string> OverAll_totalwith_reduction()
        {
            float r1 = 0;
            float overall_total_with_reduction = 0;
            List<C_TotalWithoutReduction> listOfwithoutReduction = await MainPage.File.Get_totalWithOutReduction();
            foreach(C_TotalWithoutReduction data in listOfwithoutReduction)
            {
                if (View_Date(await Date_Count(), await ListOfDate()) == data.TotalWithoutReduction_date_dp)
                {
                    _ = float.TryParse(data.TotalWithoutReduction_dp, out r1);
                }
            }
            _ = float.TryParse(await Finally_reduction_total(), out float r2);
            overall_total_with_reduction = r1 - r2;
            return Convert.ToString(overall_total_with_reduction);
        }
        #endregion
        public async Task Final_overall_total()
        {
            Final_Detail final_decide = new Final_Detail();
            final_decide.Final_detai_date_dp = View_Date(await Date_Count(), await ListOfDate());
            final_decide.Final_detai_reduction_dp = await Finally_reduction_total();
            final_decide.Final_detai_amount_dp = await OverAll_totalwith_reduction();
            _ = await MainPage.File.Add_Final_Detail(final_decide);
        }
        private async void Back_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new A_open_page());
        }
    }
}