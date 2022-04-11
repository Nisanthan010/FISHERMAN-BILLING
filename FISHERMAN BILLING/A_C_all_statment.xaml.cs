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
    public partial class A_C_all_statment : ContentPage
    {
        public A_C_all_statment()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            All_payment_detail_statment.ItemsSource = await MainPage.File.Get_customerdetail_save();
        }

        private void All_payment_detail_statment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Find_Clicked(object sender, EventArgs e)
        {
            Date_find.Text = Date_fin_.Text.Substring(0,9);
            bool result;
            List<B_customerdetail_save> get_customer = await MainPage.File.Get_customerdetail_save(); ;
            List<B_customerdetail_save> find_customer = new List<B_customerdetail_save>();
            foreach(B_customerdetail_save data in get_customer)
            {
                result = data.Customer_detail_date_dp.Contains(Date_find.Text);
                if (result)
                {
                    find_customer.Add(data);
                }
            }
            All_payment_detail_statment.ItemsSource = find_customer;
        }
    }
}