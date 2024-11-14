using MemoryManagingTest.Models;

namespace MemoryManagingTest.Views.Interfaces
{
    public interface IDataItemPool
    {
        public DataItem Get();

        public void Return(DataItem item);
    }
}
