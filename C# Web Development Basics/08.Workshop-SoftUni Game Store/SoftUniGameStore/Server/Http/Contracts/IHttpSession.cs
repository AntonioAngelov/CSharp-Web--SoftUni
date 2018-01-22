namespace SoftUniGameStore.Server.Http.Contracts
{
    public interface IHttpSession
    {
        string Id { get; }

        object Get(string key);

        void Add(string key, object value);

        void Clear();

        T Get<T>(string key);

        bool Contains(string key);
    }
}
