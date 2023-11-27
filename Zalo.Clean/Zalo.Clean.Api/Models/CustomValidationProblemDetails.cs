using Microsoft.AspNetCore.Mvc;

namespace Zalo.Clean.Api.Models
{
    public class CustomValidationProblemDetails : ProblemDetails
    {

        public IDictionary<string, string[]> Errors { get; set; }



    }
}
