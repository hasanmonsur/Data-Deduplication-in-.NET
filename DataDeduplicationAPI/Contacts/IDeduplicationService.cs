namespace DataDeduplicationAPI.Contacts
{
    public interface IDeduplicationService
    {
        Task<bool> IsDuplicateAsync(string key);
        Task AddDataAsync(string key, string data);
    }
}
