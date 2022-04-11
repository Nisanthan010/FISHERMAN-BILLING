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
    public partial class A_A_A_bill_payment_page : ContentPage
    {
        private int find_current_data_customer_id = 0;
        private bool Is_customer = false;
        public A_A_A_bill_payment_page()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Bill_payment_date.Text = View_Date(await Date_Count(), await ListOfDate());
            Kg_collection_view.ItemsSource = await View_data_kg();
        }
        private async void Save_kg_Clicked(object sender, EventArgs e)
        {
            bool Is_check_kg = float.TryParse(Kg_entry_customer_bill.Text, out float single_kg);
            if(Is_check_kg)
            {
                List<B_customerdetail_save> ListOfcustome = await MainPage.File.Get_customerdetail_save(); ;
                find_current_data_customer_id = ListOfcustome.Count() + 1;
                await Add_database_kg();
                await Display_trail();
                Is_customer = true;
            }
            else
            {
                await Display_trail();
                await DisplayAlert("ALERT",
                  "PLEASE ENTER KG IN NUMERIC VALUES", "OK");
            }

            Kg_entry_customer_bill.Text = string.Empty;
        }
        private async void Save_customer_bill_Clicked(object sender, EventArgs e)
        {
            if(Is_customer)
            {
                Name_product_detail();
                await Add_customer_detail();
                _ = await Navigation.PopAsync();
            }

        }
        private async void Delete_kg_customer_bill_Clicked(object sender, EventArgs e)
        {
            if (Last_selected_kg != null)
            {
                await MainPage.File.Delete_kg_pass(Last_selected_kg);
            }
              await Display_trail();
        }
        private B_kg_entry_date Last_selected_kg;
        private void Kg_collection_view_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Last_selected_kg = e.CurrentSelection.FirstOrDefault() as B_kg_entry_date;
            if(Last_selected_kg!=null)
            {
                Kg_entry_customer_bill.Text = Last_selected_kg.Entry_kg_pass_one;
            }
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
        #region REDUCTION save as value
        private void Reduction_detail()
        {
            if (Reduction_count_entry_customer_bill.Text == null && Extra_reduction_customer_entry_bill.Text == null)
            {
                Reduction_count_entry_customer_bill.Text = "0";
                Extra_reduction_customer_entry_bill.Text = "0";

            }
           else
            {
                if (Reduction_count_entry_customer_bill.Text == null)
                {
                    Reduction_count_entry_customer_bill.Text = "0";
                }
                else
                {
                    if (Extra_reduction_customer_entry_bill.Text == null)
                    {
                        Extra_reduction_customer_entry_bill.Text = "0";
                    }
                }
            }

            _ = float.TryParse(Reduction_count_entry_customer_bill.Text, out float r1);
            _ = float.TryParse(Extra_reduction_customer_entry_bill.Text, out float r2);
            _ = float.TryParse(Customer_bill_count.Text, out float c1);
            float Total_reduction_temp = (c1 * r1) + r2;
            Total_reduction.Text = Convert.ToString(Total_reduction_temp);
            Overall_kg_bill();
        }
        #endregion
        #region NAME AND PRODUCT save as value
        private void Name_product_detail()
        {
            if (Name_entry_customer_bill.Text == null && product_entry_customer_bill.Text == null)
            {
                Name_entry_customer_bill.Text = "NULL";
                product_entry_customer_bill.Text = "NULL";

            }
            else
            {
                if (Name_entry_customer_bill.Text == null)
                {
                    Name_entry_customer_bill.Text = "NULL";
                }
                else
                {
                    if (product_entry_customer_bill.Text == null)
                    {
                        product_entry_customer_bill.Text = "NULL";
                    }
                }
            }
        }
        #endregion
        #region view data AND TOTAL       
        public async Task Display_trail()
        {
            Kg_collection_view.ItemsSource = await View_data_kg();
            List<B_kg_entry_date> temp_count = await View_data_kg();
            int Temp = temp_count.Count();
            Customer_bill_count.Text = Convert.ToString(Temp);
            Reduction_detail();
        }
        public async Task<List<B_kg_entry_date>> View_data_kg()
        {
       
            List<B_kg_entry_date> dataB_kg_entry_date_kg = await MainPage.File.Get_entry_kg_pass_only();
            List<B_kg_entry_date> entrydata_kg = new List<B_kg_entry_date>();

            foreach (B_kg_entry_date data in dataB_kg_entry_date_kg)
            {
                if (data.Entry_date_kg_pass_one == Bill_payment_date.Text && data.Data_entry_kg_id == find_current_data_customer_id)
                {
                    entrydata_kg.Add(data);
                }

            }

            Total_count(entrydata_kg);
            return entrydata_kg;
        }
        public void Total_count(List<B_kg_entry_date> KGs)
        {
            float Total_Kgs = 0;

            foreach (B_kg_entry_date x in KGs)
            {

               Total_Kgs = float.Parse(x.Entry_kg_pass_one) + Total_Kgs;

            }
            Total_kg_Customer_bill_data_number.Text = Convert.ToString(Total_Kgs);
           
        }
        #endregion
        #region overall total and reduction 
        private void Overall_kg_bill()
        {  
            float Overall_total_kg = float.Parse(Total_kg_Customer_bill_data_number.Text) - float.Parse(Total_reduction.Text);
            Overall_entry_Total_kg_Customer_bill.Text=Convert.ToString(Overall_total_kg);
        }
        #endregion
        private async Task Add_database_kg()
        {
            B_kg_entry_date add_kg = new B_kg_entry_date();
            add_kg.Data_entry_kg_id = find_current_data_customer_id;
            add_kg.Entry_date_kg_pass_one = Bill_payment_date.Text;
            add_kg.Entry_kg_pass_one = Kg_entry_customer_bill.Text;
            _ = await MainPage.File.Add_Entry_kg_pass_one(add_kg);

        }
       private async Task Add_customer_detail()
        {
            B_customerdetail_save customer_detail_database = new B_customerdetail_save();
            customer_detail_database.Customer_detail_date_dp = Bill_payment_date.Text;
            customer_detail_database.Customer_name_dp = Name_entry_customer_bill.Text;
            customer_detail_database.Product_name_dp = product_entry_customer_bill.Text;
            customer_detail_database.Count_dp = Customer_bill_count.Text;
            customer_detail_database.Reduction_count_dp = Reduction_count_entry_customer_bill.Text;
            customer_detail_database.Extra_reduction_kg_dp = Extra_reduction_customer_entry_bill.Text;
            customer_detail_database.TotaL_kg_dp = Total_kg_Customer_bill_data_number.Text;
            customer_detail_database.Total_reduction_dp = Total_reduction.Text;
            customer_detail_database.Overall_total_dp = Overall_entry_Total_kg_Customer_bill.Text;
            customer_detail_database.Overall_total_Amount = "ENTER THE AMOUNT";
            customer_detail_database.Each_total_kg_amount = "Null";
            customer_detail_database.SetVerification_date_kg = find_current_data_customer_id;
            _ = await MainPage.File.Add_B_customerdetail_save(customer_detail_database);
        }

    }
}