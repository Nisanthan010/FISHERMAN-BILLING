using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISHERMAN_BILLING.DATABASE
{
    public class Reference_Customer
    {
        [PrimaryKey]
        public int Compare_ref_customer_id { get; set; }
        public string Overall_total_Kg_date_db { get; set; }
        public int Reference_date_compare { get; set; }
    }
}
