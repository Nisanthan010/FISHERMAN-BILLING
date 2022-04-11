using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISHERMAN_BILLING.DATABASE
{
    public class B_kg_entry_date
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Data_entry_kg_id { get; set; }
        public string Entry_date_kg_pass_one { get; set; }
        public string Entry_kg_pass_one { get; set; }
            
    }
}
