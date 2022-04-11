using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace FISHERMAN_BILLING.DATABASE
{
    public class B_customerdetail_save
    {
        [PrimaryKey, AutoIncrement]
        public int CustomerId { get; set; }
        public string Customer_detail_date_dp { get; set; }
        public string Customer_name_dp { get; set; }
        public string Product_name_dp { get; set; }
        public string Count_dp { get; set; }
        public string Reduction_count_dp { get; set; }
        public string Extra_reduction_kg_dp { get; set; }
        public string TotaL_kg_dp { get; set; }
        public string Total_reduction_dp { get; set; }
        public string Overall_total_dp { get; set; }
        public string Each_total_kg_amount { get; set; }
        public string Overall_total_Amount { get; set; }
        public int SetVerification_date_kg { get; set; }
    }
}
