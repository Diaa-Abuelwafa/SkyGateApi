﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.DTOs.PatmentModule
{
    public class PaymentResponse
    {
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
    }
}
