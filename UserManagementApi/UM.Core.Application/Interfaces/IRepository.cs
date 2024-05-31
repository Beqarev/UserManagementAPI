namespace UM.Core.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    void Create(TEntity entity);
    TEntity Read(int id);
    Task<TEntity> ReadAsync(int id);
    IEnumerable<TEntity> Read();
    Task<IEnumerable<TEntity>> ReadAsync();
    void Delete(TEntity entity);
    void Delete(int id);
    void Update(TEntity entity);
    void Update(int id, TEntity entity);
}