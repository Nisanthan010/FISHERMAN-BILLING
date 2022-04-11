using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISHERMAN_BILLING.DATABASE
{
    public class Final_Detail
    {
        [PrimaryKey, AutoIncrement]
        public int Final_statment_id { get; set; }
        public string Final_detai_date_dp { get; set; }
        public string Final_detai_amount_dp { get; set; }
        public string Final_detai_reduction_dp { get; set; }
    }
}
