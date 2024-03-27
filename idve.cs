public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(object id);
    void Insert(T obj);
    void Update(T obj);
    void Delete(object id);
    void Save();
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T GetById(object id)
    {
        return _dbSet.Find(id);
    }

    public void Insert(T obj)
    {
        _dbSet.Add(obj);
    }

    public void Update(T obj)
    {
        _context.Entry(obj).State = EntityState.Modified;
    }

    public void Delete(object id)
    {
        T existing = _dbSet.Find(id);
        _dbSet.Remove(existing);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
