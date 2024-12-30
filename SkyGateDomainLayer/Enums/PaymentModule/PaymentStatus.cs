using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Enums.PaymentModule
{
    public enum PaymentStatus
    {
        [EnumMember(Value = "Payment Received")]
        Received,

        [EnumMember(Value = "Payment Pending")]
        Pending,

        [EnumMember(Value = "Payment Failed")]
        Failed
    }
}
