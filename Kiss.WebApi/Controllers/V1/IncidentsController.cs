using Kiss.Application.Interfaces;
using Kiss.Application.Parameters.Mock;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kiss.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class IncidentsController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public IncidentsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// SELECT records from mock library Bogus
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.IncidentReport.GetAllAsync();

            return Ok(data);
        }
        [Route("paged")]
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] GetAllIncidentsParameters urlQueryParameters)
        {
            var result = await unitOfWork.IncidentReport.GetPagedAsync(urlQueryParameters);
            var data = result.Data;
            var pagination = result.Pagination;

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

            return Ok(data);
        }

    }
}