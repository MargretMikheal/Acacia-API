using Acacia.Core.Resources;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Acacia.Core.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ResponseHandler(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
        }

        public Response<T> Success<T>(T data, string message = null)
        {
            return new Response<T>(data, message ?? _localizer[SharedResourcesKeys.Success])
            {
                Response_Code = HttpStatusCode.OK,
            };
        }

        public Response<T> Created<T>(T data)
        {
            return new Response<T>(data, _localizer[SharedResourcesKeys.Created])
            {
                Response_Code = HttpStatusCode.Created,
            };
        }

        public Response<T> Deleted<T>()
        {
            return new Response<T>(_localizer[SharedResourcesKeys.Deleted], succeeded: true)
            {
                Response_Code = HttpStatusCode.OK
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>(message ?? _localizer[SharedResourcesKeys.NotFound])
            {
                Response_Code = HttpStatusCode.NotFound,
            };
        }

        public Response<T> Unauthorized<T>(string message = null)
        {
            return new Response<T>(message ?? _localizer[SharedResourcesKeys.Unauthorized], succeeded: false)
            {
                Response_Code = HttpStatusCode.Unauthorized,
            };
        }

        public Response<T> BadRequest<T>(string message = null)
        {
            return new Response<T>(message ?? _localizer[SharedResourcesKeys.BadRequest])
            {
                Response_Code = HttpStatusCode.BadRequest,
            };
        }

        public Response<T> UnprocessableEntity<T>(Dictionary<string, List<string>> errors, string message = null)
        {
            return new Response<T>(message ?? _localizer[SharedResourcesKeys.UnprocessableEntity], succeeded: false)
            {
                Errors = errors,
                Response_Code = HttpStatusCode.UnprocessableEntity,
            };
        }
    }
}
