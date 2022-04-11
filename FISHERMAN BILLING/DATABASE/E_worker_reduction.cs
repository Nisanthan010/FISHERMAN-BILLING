using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISHERMAN_BILLING.DATABASE
{
    public class E_worker_reduction
    {
        [PrimaryKey, AutoIncrement]
        public int Worker_id { get; set; }
        public string Worker_reduction_date_dp { get; set; }
        public string Driver_reduction_no_dp { get; set; }
        public string Driver_reduction_amount_dp { get; set; }
        public string Worker_reduction_no_dp { get; set; }
        public string Worker_reduction_amount_dp { get; set; }
        public string Worker_total_reduction_dp { get; set; }
    }
}
