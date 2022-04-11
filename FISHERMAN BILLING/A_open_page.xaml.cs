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
    public partial class A_open_page : ContentPage
    {
        public A_open_page()
        {
            InitializeComponent();
        }

        private async void New_bill_payment(object sender, EventArgs e)
        {
            List<B_DateOnly> ListOfDate = await MainPage.File.Get_DateOnly();
            int Date_Count = ListOfDate.Count();
            string Current_date = DateTime.Now.ToString();
            B_DateOnly date_current = new B_DateOnly
            {
                Entry_date_db = Current_date,
                Id = Date_Count + 1
            };
            _ = await MainPage.File.Add_date(date_current);

            await Navigation.PushAsync(new A_A_enter_page());
        }

        private async void DeleteTableAll_button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new A_D_delete_all_page());
        }

        private async void ExistPaymentstatment_button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new A_B_all_payment_detail_date());
        }

        private async void ExistPayment_button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new A_C_all_statment());
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}