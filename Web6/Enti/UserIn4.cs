using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web6.Enti
{
    public partial class UserIn4
    {
        [Key]
        public string? Id { get; set; }
        public string? Ten { get; set; }
        public string? Tuoi { get; set; }
        public string? DiaChi { get; set; }
        public string? Sdt { get; set; }
    }
}
