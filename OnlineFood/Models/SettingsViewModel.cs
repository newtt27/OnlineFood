using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using OnlineFood.Models;

namespace OnlineFood.Models
{
    public class SettingsViewModel
    {
        public List<Role> Roles { get; set; }
        public List<Function> Functions { get; set; }
    }
}