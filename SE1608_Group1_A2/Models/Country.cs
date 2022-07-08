﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SE1608_Group1_A2.Models
{
    public partial class Country
    {
        public Country()
        {
            Films = new HashSet<Film>();
        }

        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}
