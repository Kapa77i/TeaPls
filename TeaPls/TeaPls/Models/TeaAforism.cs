using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeaPls.Models
{
    public class TeaAforism
    {
        [PrimaryKey, AutoIncrement]
        public string _id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}
