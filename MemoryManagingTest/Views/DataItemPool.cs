using MemoryManagingTest.Models;
using MemoryManagingTest.Views.Interfaces;

namespace MemoryManagingTest.Views
{
    public class DataItemPool : IDataItemPool
    {
        private readonly Queue<DataItem> _pool = new Queue<DataItem>();

        public DataItem Get()
        {
            if (_pool.Count > 0)
                return _pool.Dequeue();

            return new DataItem();
        }

        public void Return(DataItem item)
        {
            item.Id = 0;
            item.Value = 0;
            item.Timestamp = DateTime.MinValue;
            _pool.Enqueue(item);
        }
    }
}
