using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public ApiErrorResponse(int StatusCode, string? ErrorMessage = null)
        {
            this.StatusCode = StatusCode;
            
            if(ErrorMessage is null)
            {
                this.ErrorMessage = GetErrorMessage(StatusCode);
            }
            else
            {
                this.ErrorMessage = ErrorMessage;
            }
        }

        private string GetErrorMessage(int StatusCode)
        {
            string ErrorMessage = "";

            switch(StatusCode)
            {
                case 404:
                    ErrorMessage = "NotFound Resource";
                    break;

                case 401:
                    ErrorMessage = "UnAuthorize";
                    break;

                case 400:
                    ErrorMessage = "BadRequest";
                    break;

                case 500:
                    ErrorMessage = "Server Error";
                    break;

                default:
                    ErrorMessage = "UnExpected Error!";
                    break;
            }

            return ErrorMessage;
        }
    }
}
