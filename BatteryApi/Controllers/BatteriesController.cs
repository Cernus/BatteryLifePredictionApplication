using BatteryApi.Models;
using BatteryApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApi.Controllers
{
    [Route("api/batteries/{action}")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        public IBatteryRepository _batteryRepository { get; set; }

        public BatteriesController(IBatteryRepository batteryRepository)
        {
            _batteryRepository = batteryRepository;
        }

        // POST: api/Batteries/Creates
        [HttpPost]
        public async Task<ActionResult<Battery>> Creates([FromBody] List<BatteryDto> batteries)
        {
            List<Battery> entities = await _batteryRepository.CreateBatteries(batteries);

            if (entities == null)
            {
                return NotFound();
            }

            return Ok(entities);
        }

        // POST: api/Batteries/Create
        [HttpPost]
        public async Task<ActionResult<Battery>> Create([FromBody] BatteryDto battery)
        {
            Battery entity = await _batteryRepository.CreateBattery(battery);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        // GET: api/Batteries/Details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatteryDto>>> Details()
        {
            List<BatteryDto> batteries = await _batteryRepository.GetBatteries();

            if (batteries == null)
            {
                return NotFound();
            }

            return Ok(batteries);
        }

        // GET: api/Batteries/Details/
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BatteryDto>>> Details(int id)
        {
            List<BatteryDto> batteries = await _batteryRepository.GetBatteries(id);

            if (batteries == null)
            {
                return NotFound();
            }

            return Ok(batteries);
        }

        // GET: api/Batteries/Detail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BatteryDto>> Detail(int id)
        {
            BatteryDto battery = await _batteryRepository.GetBattery(id);

            if (battery == null)
            {
                return NotFound();
            }

            return Ok(battery);
        }

        // PUT: api/Batteries/Update
        public async Task<ActionResult<Battery>> Update([FromBody] BatteryDto battery)
        {
            Battery entity = await _batteryRepository.UpdateBattery(battery);

            if (entity == null)
            {
                return NotFound();
            }

            if (battery.BatteryId != entity.BatteryId)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        // PUT: api/Batteries/Updates
        [HttpPut]
        public async Task<ActionResult<Battery>> Updates([FromBody] List<BatteryDto> batteries)
        {
            List<Battery> entities = await _batteryRepository.UpdateBatteries(batteries);

            if (entities == null)
            {
                return NotFound();
            }

            if (batteries[0].BatteryId != entities[0].BatteryId)
            {
                return BadRequest();
            }

            return Ok(entities);
        }

        // PUT: api/Batteries/Delete/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Battery>> Delete(int id)
        {
            Battery entity = await _batteryRepository.DeleteBattery(id);

            if (entity == null)
            {
                return NotFound();
            }

            if (id != entity.BatteryId)
            {
                return BadRequest();
            }

            return Ok(entity);
        }
    }
}
