using Microsoft.AspNetCore.Mvc;
using PredictionApi.Models;
using PredictionApi.Facades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PredictionApi.Controllers
{
    [Route("api/predictions/{action}")]
    [ApiController]
    public class PredictionsController : ControllerBase
    {
        public IPredictionFacade _predictionFacade { get; set; }

        public PredictionsController()
        {
            _predictionFacade = new PredictionFacade();
        }

        // Needs to be PUT rather than GET so battery information can be sent through the body
        // PUT: api/Predictions/Lifetime
        [HttpPut]
        public async Task<ActionResult<double>> Lifetime([FromBody] BatteryDto battery)
        {
            double lifetime;
            try
            {
                lifetime = await _predictionFacade.GetLifetime(battery);
                return Ok(lifetime);
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: api/Predictions/Lifetimes/5
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Lifetimes(int id, [FromBody] List<BatteryDto> batteries)
        {
            try
            {
                string jobId = await _predictionFacade.GetLifetimes(batteries, id);

                if (jobId == null)
                {
                    return BadRequest();
                }

                return Ok(jobId);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // PUT: api/Predictions/DownloadLinearResults/5
        [HttpPut("{id}")]
        public async Task<ActionResult<List<double>>> DownloadLinearResults(int id, [FromBody] string jobId)
        {
            try
            {
                List<double> predictions = await _predictionFacade.DownloadResults(id, jobId, false);

                if (predictions == null)
                {
                    return NotFound();
                }

                return Ok(predictions);
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: api/Predictions/DownloadDecisionForestResults/5
        [HttpPut("{id}")]
        public async Task<ActionResult<List<double>>> DownloadDecisionForestResults(int id, [FromBody] string jobId)
        {
            try
            {
                List<double> predictions = await _predictionFacade.DownloadResults(id, jobId, false);

                if (predictions == null)
                {
                    return NotFound();
                }

                return Ok(predictions);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
