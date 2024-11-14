using MemoryManagingTest.Models;
using MemoryManagingTest.Views.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MemoryManagingTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataProcessingController : ControllerBase
    {
        private readonly IDataItemPool _dataItemPool;
        private readonly List<DataItem> _activeDataItems = new List<DataItem>();

        public DataProcessingController(IDataItemPool dataItemPool)
            => _dataItemPool = dataItemPool;

        /// <summary>
        /// Processes incoming data.
        /// </summary>
        [HttpPost("process")]
        public IActionResult ProcessData([FromBody] DataItem newData)
        {
            var dataItem = _dataItemPool.Get();
            dataItem.Id = newData.Id;
            dataItem.Value = newData.Value;
            dataItem.Timestamp = newData.Timestamp;

            _activeDataItems.Add(dataItem);

            return Ok("Data processed successfully.");
        }

        /// <summary>
        /// Clears processed data and returns to pool.
        /// </summary>
        [HttpPost("clear")]
        public IActionResult ClearData()
        {
            foreach (var dataItem in _activeDataItems)
            {
                _dataItemPool.Return(dataItem);
            }

            _activeDataItems.Clear();

            Console.WriteLine("Cleared active data items and returned them to the pool.");

            GC.Collect(1, GCCollectionMode.Forced, true, true);

            return Ok("Data cleared successfully.");
        }
    }
}
