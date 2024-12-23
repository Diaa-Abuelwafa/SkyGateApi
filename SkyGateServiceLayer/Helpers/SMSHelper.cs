using Microsoft.Extensions.Configuration;
using SkyGateDomainLayer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SkyGateServiceLayer.Helpers
{
    public static class SMSHelper
    {
        public static void SendSMS(SMSMessage Message, IConfiguration Config)
        {
            TwilioClient.Init(Config["TwilioSettings:AccountSID"], Config["TwilioSettings:AuthToken"]);

            MessageResource.Create(
                                to: new PhoneNumber(Message.PhoneNumber),
                                from: new PhoneNumber(Config["TwilioSettings:TwilioPhoneNumber"]),
                                body: Message.Body);
        }
    }
}
