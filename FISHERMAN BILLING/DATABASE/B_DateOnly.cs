using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISHERMAN_BILLING.DATABASE
{
    public class B_DateOnly
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Entry_date_db { get; set; }
    }
}
