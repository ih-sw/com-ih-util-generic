using System.Collections.Generic;
using com.ih.util.custom.errors.Domain;
using Microsoft.AspNetCore.Http;

namespace com.ih.util.generic.Errors
{
    public class MapperErrorCustomError : CustomErrorException
    {
        public override string message { get { return "Object mapper error"; } }
        public override string code { get { return "object-mapper-error"; } }
        public override int status { get { return StatusCodes.Status500InternalServerError; } }

        public MapperErrorCustomError(string contentBodyRequest = null, string contentBodyResponse = null, Dictionary<string, string> details = null)
        {
            this.contentBodyRequest = contentBodyRequest;
            this.contentBodyResponse = contentBodyResponse;
            this.details = details;
        }
    }
}
