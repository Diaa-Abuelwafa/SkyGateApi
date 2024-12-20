using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Errors
{
    public class ApiValidationErrorResponse : ApiErrorResponse
    {
        public List<string>? Errors { get; set; } = new List<string>();
        public ApiValidationErrorResponse(int StatusCode, List<string>? Errors = null, string? ErrorMessage = null) : base(StatusCode, ErrorMessage)
        {
            if(Errors is not null)
            {
                this.Errors = Errors;
            }
        }
    }
}
