namespace com.ih.util.generic.ConsumeServices.Domain;

public class ConsumeRestServiceResponse<TResponse>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public TResponse Response { get; set; }
    public string ContentBodyErrorResponse { get; set; }
}