using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FISHERMAN_BILLING
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class A_D_delete_all_page : ContentPage
    {
        public A_D_delete_all_page()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            date_collection.ItemsSource = await MainPage.File.Get_DateOnly();
            Kg_collection.ItemsSource = await MainPage.File.Get_entry_kg_pass_only();
            customer_collection.ItemsSource = await MainPage.File.Get_customerdetail_save();
            reference_detail_collection.ItemsSource = await MainPage.File.Get_reference_statment_Detail();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await MainPage.File.Delete_all_DateOnly();
            await MainPage.File.Delete_all_Reference_Customer();
            await MainPage.File.Delete_all_customerdetail_save();
            await MainPage.File.Delete_all_totalWithOutReduction();
            await MainPage.File.Delete_all_worker_reduction();
            await MainPage.File.Delete_all_petrol();
            await MainPage.File.Delete_all_extra_reduction();
            await MainPage.File.Delete_all_finalDetail();
            await MainPage.File.Delete_all_entry_kg_pass_one();
            await MainPage.File.Delete_all_reference_statment();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await MainPage.File.Delete_all_DateOnly();
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await MainPage.File.Delete_all_Reference_Customer();
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            await MainPage.File.Delete_all_customerdetail_save();
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            await MainPage.File.Delete_all_totalWithOutReduction();
        }

        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            await MainPage.File.Delete_all_worker_reduction();
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            await MainPage.File.Delete_all_extra_reduction();
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {
            await MainPage.File.Delete_all_petrol();
        }

        private async void Button_Clicked_8(object sender, EventArgs e)
        {
            await MainPage.File.Delete_all_finalDetail();
        }
    }
}