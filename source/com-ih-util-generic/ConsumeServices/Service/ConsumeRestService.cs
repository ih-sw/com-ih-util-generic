using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using com.ih.util.generic.ConsumeServices.Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace com.ih.util.generic.ConsumeServices.Service;

public enum HttpClientRequestMethod
{
    GET,
    POST,
    DELETE,
    PUT
}

public class ConsumeRestService : IDisposable
{
    private HttpClient? _client;

    public ConsumeRestService(HttpClient? client = null)
    {
        _client = client;
    }

    public async Task<ConsumeRestServiceResponse<TResponse?>> Request<TResponse>(
        HttpClientRequestMethod type,
        string url,
        List<KeyValuePair<string, string>>? headers = null,
        AuthenticationHeaderValue? authHeader = null)
    {
        return await Request<string, TResponse>(
            type: type,
            url: url,
            request: null,
            headers: headers,
            authHeader: authHeader);
    }

    public async Task<ConsumeRestServiceResponse<TResponse?>> Request<TRequest, TResponse>(
        HttpClientRequestMethod type,
        string url,
        TRequest? request,
        List<KeyValuePair<string, string>>? headers = null,
        AuthenticationHeaderValue? authHeader = null)
    {
        var processResponse = new ConsumeRestServiceResponse<TResponse?>()
        {
            IsSuccess = false,
            ContentBodyErrorResponse = string.Empty
        };

        try
        {
            StringContent? contentRequest = request != null
                ? new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
                : null;

            HttpResponseMessage? httpResponse = null;

            if (_client == null)
            {
                _client = new HttpClient();
            }

            // -- Adding authentication header
            if (authHeader != null)
            {
                _client.DefaultRequestHeaders.Authorization = authHeader;
            }

            //  --  Others Headers
            if (headers?.Count > 0)
            {
                headers.ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value) &&
                        item.Key.ToLower() != "authorization")
                    {
                        _client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                });
            }

            switch (type)
            {
                case HttpClientRequestMethod.GET:
                    httpResponse = await _client.GetAsync(url);
                    break;
                case HttpClientRequestMethod.DELETE:
                    httpResponse = await _client.DeleteAsync(url);
                    break;
                case HttpClientRequestMethod.POST:
                    httpResponse = await _client.PostAsync(url, contentRequest);
                    break;
                case HttpClientRequestMethod.PUT:
                    httpResponse = await _client.PutAsync(url, contentRequest);
                    break;
            }

            processResponse.StatusCode = httpResponse != null
                ? (int)httpResponse?.StatusCode
                : StatusCodes.Status417ExpectationFailed;

            var contentBodyResponse = string.Empty;

            if (httpResponse != null && httpResponse.IsSuccessStatusCode)
            {
                //  --  Success
                try
                {
                    //  --  Object mapped with Success
                    contentBodyResponse = await httpResponse.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    //  --  Object mapped with Error
                    contentBodyResponse = "ERROR_READ_CONTENT_BODY";
                    processResponse.ContentBodyErrorResponse = "ERROR_READ_CONTENT_BODY_ERROR => " + e;
                }
            }
            else
            {
                //  --  Response Error
                try
                {
                    //  --  Get content body for use in other moment
                    processResponse.ContentBodyErrorResponse = await httpResponse?.Content?.ReadAsStringAsync();
                    processResponse.IsSuccess = false;
                }
                catch (Exception e)
                {
                    //  -- Error in get content body
                    processResponse.ContentBodyErrorResponse = "ERROR_READ_CONTENT_BODY_ERROR => " + e;
                    processResponse.IsSuccess = false;
                }
            }

            httpResponse?.EnsureSuccessStatusCode();

            try
            {
                processResponse.Response = (!string.IsNullOrEmpty(contentBodyResponse) &&
                                            !contentBodyResponse.Equals("ERROR_READ_CONTENT_BODY"))
                    ? JsonConvert.DeserializeObject<TResponse>(contentBodyResponse)
                    : default;

                processResponse.IsSuccess = true;
            }
            catch (Exception e)
            {
                processResponse.ContentBodyErrorResponse = "ERROR_MAPPER_CONTENT_BODY_ERROR => " + e;
                processResponse.IsSuccess = false;
            }
        }
        catch (Exception ex)
        {
            processResponse.StatusCode = StatusCodes.Status424FailedDependency;
            processResponse.ContentBodyErrorResponse = "GENERAL_ERROR => " + ex;
            processResponse.IsSuccess = false;
        }

        return processResponse;
    }

    public void Dispose()
    {
        if (_client != null)
        {
            _client = null;
        }

        GC.SuppressFinalize(this);
    }
}