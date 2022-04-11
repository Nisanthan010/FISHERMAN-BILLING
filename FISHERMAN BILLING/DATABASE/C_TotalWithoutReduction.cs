using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISHERMAN_BILLING.DATABASE
{
    public class C_TotalWithoutReduction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TotalWithoutReduction_date_dp { get; set; }
        public string TotalWithoutReduction_dp { get; set; }
    }
}
