﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeaPls.Models
{
    public class TeaAforism
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string Aforism { get; set; }
    }
}
