using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class AppForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FormJson { get; set; }
        public string AppName { get; set; }
    }
}
