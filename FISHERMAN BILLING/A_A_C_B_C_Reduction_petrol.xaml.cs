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
    public partial class A_A_C_B_C_Reduction_petrol : ContentPage
    {
        public A_A_C_B_C_Reduction_petrol()
        {
            InitializeComponent();
        }
        private async void Petrol_pay_Clicked(object sender, EventArgs e)
        {
            bool Is_check = float.TryParse(Petrol_rate.Text, out float r1);
            if (Is_check)
            {
                await Save_petrol();
                _ = await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("WARNINIG", "PLEASE ENTER THE AMOUNT", "OK");
            }
            Petrol_rate.Text = string.Empty;
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
        public async Task Save_petrol()
        {
            E_PetrolReduction Diesel = new E_PetrolReduction();
            Diesel.Petrol_amount_date = View_Date(await Date_Count(), await ListOfDate());
            Diesel.Petrol_amount = Petrol_rate.Text;
            _ = await MainPage.File.Add_petrol(Diesel);
        }
    }
}