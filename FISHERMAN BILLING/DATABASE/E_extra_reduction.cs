using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISHERMAN_BILLING.DATABASE
{
    public class E_extra_reduction
    {
        [PrimaryKey, AutoIncrement]
        public int Customer_extra_reduction { get; set; }
        public string Extra_reduction_date_dp { get; set; }
        public string Extra_reduction_name__dp { get; set; }
        public string Extra_reduction_amount_dp { get; set; }
    }
}
