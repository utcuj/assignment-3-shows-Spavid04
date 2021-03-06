﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shows.Core.Models
{
    public class UserShowHistory
    {
        public int Id { get; set; }

        public Guid PublicId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual User User { get; set; }
        public virtual Show Show { get; set; }

        public override string ToString()
        {
            return $"{Show} - {DateTime.ToShortDateString()}";
        }
    }
}
