using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISHERMAN_BILLING.DATABASE
{
   public class Reference_statment
    {
        [PrimaryKey]
        public int Reference_statment_id { get; set; }
        public string Reference_date_statment { get; set; }
        public string Reference_overall_amount { get; set; }
    }
}
