namespace Server.ModelsInterfaces.Settings
{
    /// <summary>
    /// The interface responsible for Structure declaration for Generic Factory
    /// </summary>
    public interface IFactory<TModelFactory> where TModelFactory : class
    {
        TModelFactory Create();
    }
}
