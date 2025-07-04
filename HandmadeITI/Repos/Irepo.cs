namespace HandmadeITI.Respo
{
    public interface Irepo<T> where T : class
    {
        Task<T> GetById(int? id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task SaveChanges();
    }

}
