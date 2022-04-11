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
    public partial class A_B_all_payment_detail_date : ContentPage
    {
        public A_B_all_payment_detail_date()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            All_payment_detail_bill.ItemsSource = await MainPage.File.Get_finalDetail();
        }
        private Final_Detail Last_selected;
        private async void All_payment_detail_bill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Last_selected = (Final_Detail)e.CurrentSelection.FirstOrDefault();
            Reference_statment statement_overall = new Reference_statment();
            if (Last_selected != null)
            {
                statement_overall.Reference_statment_id = Last_selected.Final_statment_id;
                statement_overall.Reference_date_statment = Last_selected.Final_detai_date_dp;
                statement_overall.Reference_overall_amount = Last_selected.Final_detai_amount_dp;
                _ = await MainPage.File.Add_reference_statment_Detail(statement_overall);
                await Navigation.PushAsync(new A_A_D_statment());
            }
        }
        private async void Find_Clicked(object sender, EventArgs e)
        {

            Date_find.Text = Date_fin_.Text.Substring(0, 9);
            bool result;
            List<Final_Detail> get_customer = await MainPage.File.Get_finalDetail(); ;
            List<Final_Detail> find_customer = new List<Final_Detail>();
            foreach (Final_Detail data in get_customer)
            {
                result = data.Final_detai_date_dp.Contains(Date_find.Text);
                if (result)
                {
                    find_customer.Add(data);
                }
            }
            All_payment_detail_bill.ItemsSource = find_customer;
        }
    }
}