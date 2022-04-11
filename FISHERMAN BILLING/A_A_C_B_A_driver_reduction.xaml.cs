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
    public partial class A_A_C_B_A_driver_reduction : ContentPage
    {
        public A_A_C_B_A_driver_reduction()
        {
            InitializeComponent();
        }
        private async void Employee_pay_Clicked(object sender, EventArgs e)
        {
            bool is_check_driver = float.TryParse(Driver_no.Text, out _);
            bool is_check_driver_no = float.TryParse(Paid_for_drive.Text, out _);
            bool is_check_worker = float.TryParse(Employee_no.Text, out _);
            bool is_check_worker_no = float.TryParse(Paid_for_emp.Text, out _);
            if (is_check_driver && is_check_driver_no && is_check_worker && is_check_worker_no)
            {
                await Total_employee_pay();
                await Save_worker();
                _ = await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("WARNING", "PLEASE FILL THE ENTER FIELD AND NUMERIC ONLY", "OK");
            }
            Driver_no.Text = string.Empty;
            Paid_for_drive.Text = string.Empty;
            Employee_no.Text = string.Empty;
            Paid_for_emp.Text = string.Empty;
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
        #region Total_employee
        public async Task Total_employee_pay()
        {
            float r1 = 0;
            float r2 = 0;
            float paid_driver = 0;
            float Paid_emplo = 0;
            float paid_driver_no = 0;
            float Paid_emplo_no = 0;
            float Total_reduction_worker = 0;
            bool is_paid_drive = float.TryParse(Paid_for_drive.Text, out r1);
            bool is_paid_emp =  float.TryParse(Paid_for_emp.Text, out r2);
            bool is_paid_drive_no = float.TryParse(Driver_no.Text, out paid_driver_no);
            bool is_paid_emp_no = float.TryParse(Employee_no.Text, out Paid_emplo_no);
            if (is_paid_drive || is_paid_emp|| is_paid_drive_no|| is_paid_emp_no)
            {
                paid_driver = await Find_tota_without_reduction() * (r1 / 1000);
                Paid_emplo = await Find_tota_without_reduction() * (r2 / 1000);
                Total_reduction_worker = (paid_driver * paid_driver_no) + (Paid_emplo * Paid_emplo_no);
            }
            Driver_no.Text = Convert.ToString(paid_driver_no);
            Employee_no.Text = Convert.ToString(Paid_emplo_no);
            Driver_per_amount.Text = Convert.ToString(paid_driver);
            Employee_Per_amount.Text = Convert.ToString(Paid_emplo);
            Employee_Total_reduction.Text = Convert.ToString(Total_reduction_worker);
        }
        public async Task<float> Find_tota_without_reduction()
        {
            string Current_date = View_Date(await Date_Count(), await ListOfDate());
            string totalwithout_reduction = null;
            List<C_TotalWithoutReduction> totalLidtOfwithOutRedution = await MainPage.File.Get_totalWithOutReduction();
            {
                foreach(C_TotalWithoutReduction data in totalLidtOfwithOutRedution)
                {
                    if (data.TotalWithoutReduction_date_dp == Current_date)
                    {
                        totalwithout_reduction = data.TotalWithoutReduction_dp;
                    }
                }
            }
            
            return float.Parse(totalwithout_reduction);
        }
        #endregion
        public async Task Save_worker()
        {
            E_worker_reduction worker_pay = new E_worker_reduction();
            worker_pay.Worker_reduction_date_dp = View_Date(await Date_Count(), await ListOfDate());
            worker_pay.Driver_reduction_no_dp = Driver_no.Text;
            worker_pay.Worker_reduction_no_dp = Employee_no.Text;
            worker_pay.Driver_reduction_amount_dp = Driver_per_amount.Text;
            worker_pay.Worker_reduction_amount_dp = Employee_Per_amount.Text;
            worker_pay.Worker_total_reduction_dp = Employee_Total_reduction.Text;
            await MainPage.File.Add_E_worker_reduction(worker_pay);
        }
    }
}