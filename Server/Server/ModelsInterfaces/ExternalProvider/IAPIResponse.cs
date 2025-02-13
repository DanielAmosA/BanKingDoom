namespace Server.ModelsInterfaces.ExternalProvider
{
    /// <summary>
    /// The interface responsible for Structure declaration for Generic ApiResponse
    /// </summary>
    public interface IAPIResponse<TResponse>
    {
        TResponse Data { get; set; }
    }
}
