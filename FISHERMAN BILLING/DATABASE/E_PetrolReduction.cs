using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISHERMAN_BILLING.DATABASE
{
    public class E_PetrolReduction
    {
        [PrimaryKey, AutoIncrement]
        public int PetrolId { get; set; }
        public string Petrol_amount_date { get; set; }
        public string Petrol_amount { get; set; }
    }
}
