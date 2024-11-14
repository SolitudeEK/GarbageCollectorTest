using Microsoft.AspNetCore.Mvc;

namespace MemoryManagingTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GCController : ControllerBase
    {
        /// <summary>
        /// Gets the current memory usage
        /// </summary>
        /// <returns> Memory usage info </returns>
        [HttpGet("info")]
        public ActionResult GetMemoryInfo()
        {
            var memInfo = new
            {
                TotalUsed = GC.GetTotalMemory(false) / 1024 / 1024,
                Gen0Count = GC.CollectionCount(0),
                Gen1Count = GC.CollectionCount(1),
                Gen2Count = GC.CollectionCount(2),
            };

            return Ok(memInfo);
        }

        /// <summary>
        /// Force GC for the specified generation.
        /// </summary>
        /// <param name="generation"> The generation to colelct (0, 1 or 2)</param>
        /// <return> Status of garbage collection</return>
        [HttpPost("collect/{generation}")]
        public ActionResult Collect(int generation)
        {
            if (generation < 0 || generation > 2)
                return BadRequest("Generation must be 0, 1 or 2");

            GC.Collect(generation, GCCollectionMode.Forced, true, true);
            return Ok($"Garbage collection triggered for generation {generation}.");

        }

        /// <summary>
        /// Triggers a full garbage collection across all generations.
        /// </summary>
        /// <returns>Status of the full garbage collection</returns>
        [HttpPost("collect/full")]
        public ActionResult FullCollect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return Ok("Full garbage collection triggered.");
        }
    }
}
