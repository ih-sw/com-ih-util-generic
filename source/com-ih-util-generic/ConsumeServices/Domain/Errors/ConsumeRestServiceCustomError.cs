using System.Collections.Generic;
using com.ih.util.custom.errors.Domain;
using Microsoft.AspNetCore.Http;

namespace com.ih.util.generic.ConsumeServices.Domain.Errors
{
    public class ConsumeRestServiceCustomError : CustomErrorException
    {
        public override string message { get { return "Consume rest service error"; } }
        public override string code { get { return "consume-rest-service-error"; } }
        public override int status { get { return StatusCodes.Status424FailedDependency; } }

        public ConsumeRestServiceCustomError(string contentBodyRequest = null, string contentBodyResponse = null, Dictionary<string, string> details = null)
        {
            this.contentBodyRequest = contentBodyRequest;
            this.contentBodyResponse = contentBodyResponse;
            this.details = details;
        }
    }
}