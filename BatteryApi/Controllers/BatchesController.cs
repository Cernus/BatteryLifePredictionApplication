using BatteryApi.Models;
using BatteryApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApi.Controllers
{
    [Route("api/batches/{action}")]
    [ApiController]
    public class BatchesController : ControllerBase
    {
        public IBatchRepository _batchRepository { get; set; }

        public BatchesController(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        // POST: api/Batches/Create
        [HttpPost]
        public async Task<ActionResult<Batch>> Create([FromBody] BatchDto batch)
        {
            Batch entity = await _batchRepository.CreateBatch(batch);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        // GET: api/Batches/Details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatchDto>>> Details()
        {
            List<BatchDto> batch = await _batchRepository.GetBatches();

            if (batch == null)
            {
                return NotFound();
            }

            return Ok(batch);
        }

        // GET: api/Batches/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BatchDto>>> Details(int id)
        {
            List<BatchDto> batch = await _batchRepository.GetBatches(id);

            if (batch == null)
            {
                return NotFound();
            }

            return Ok(batch);
        }

        // GET: api/Batches/Detail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BatchDto>> Detail(int id)
        {
            BatchDto batch = await _batchRepository.GetBatch(id);

            if (batch == null)
            {
                return NotFound();
            }

            return Ok(batch);
        }

        // PUT: api/Batches/Update
        public async Task<ActionResult<Batch>> Update([FromBody] BatchDto batch)
        {
            Batch entity = await _batchRepository.UpdateBatch(batch);

            if (entity == null)
            {
                return NotFound();
            }

            if (batch.BatchId != entity.BatchId)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        // PUT: api/Batches/Delete/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Batch>> Delete(int id)
        {
            Batch entity = await _batchRepository.DeleteBatch(id);

            if (entity == null)
            {
                return NotFound();
            }

            if (id != entity.BatchId)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        // GET: api/Batches/GetPredictionStatus/
        [HttpGet("{id}")]
        public ActionResult<PredictionStatus> GetPredictionStatus(int id)
        {
            PredictionStatus predictionStatus = _batchRepository.GetPredictionStatus(id).Result;

            return Ok(predictionStatus);
        }
    }
}
