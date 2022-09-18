﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Models
{
    public class ShippingAddress
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
