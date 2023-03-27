namespace Tasks.DAL.Repositories.Interface
{
    public interface IRepository<T>
    {
        public T Get(int id);
        public ICollection<T> Get(Func<T, bool> where);
        public int GetCount(Func<T, bool> where);

        public ICollection<T> Get(Func<T, bool> where, int skip, int take);

        public T Save(T item);
        public void Delete(int id);
    }
}
