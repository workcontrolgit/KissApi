using Microsoft.AspNetCore.Mvc;
using Kiss.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Kiss.Core.Entities;
using Microsoft.AspNetCore.Http;
using Kiss.Application.Parameters;
using Kiss.Application.Parameters.Mock;
using System.Text.Json;

namespace Kiss.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PositionsController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public PositionsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// SELECT records from mock library GenFu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Position>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Position.GetAllAsync();
            return Ok(data);
        }
        /// <summary>
        /// Page records from mock library GenFu
        /// </summary>
        /// <param name="urlQueryParameters"></param>
        /// <returns></returns>
        [Route("paged")]
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] GetAllPositionsParameter urlQueryParameters)
        {
            var result = await unitOfWork.Position.GetPagedAsync(urlQueryParameters);
            var data = result.Data;
            var pagination = result.Pagination;

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

            return Ok(data);
        }

    }
}