using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Errors
{
    public class ApiServerErrorResponse : ApiErrorResponse
    {
        public string? Details { get; set; }
        public ApiServerErrorResponse(int StatusCode, string? Details = null, string? ErrorMessage = null) : base(StatusCode, ErrorMessage)
        {
            if(Details is not null)
            {
                this.Details = Details;
            }
        }
    }
}
