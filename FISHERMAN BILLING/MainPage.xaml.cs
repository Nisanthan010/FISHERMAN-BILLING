using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FISHERMAN_BILLING.DATABASE;
using System.IO;

namespace FISHERMAN_BILLING
{
    public partial class MainPage : ContentPage
    {
        
        private static A_Task Data_table;
        public static A_Task File
        { get
            {
                if(Data_table == null)
                {
                    string Path_database = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    string file_name = "database1.db3";
                    string Data_table_path = Path.Combine(Path_database, file_name);
                    Data_table = new A_Task(Data_table_path) ;
                }
                return Data_table;
            }
        }
       
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new A_open_page());
        }

    }
}
