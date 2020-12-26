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
    public class PersonsController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public PersonsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// SELECT records from mock library GenFu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Person>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetAll([FromQuery] QueryStringParameters filter)
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Person.GetAllAsync();
            return Ok(data);
        }
        /// <summary>
        /// Page records from mock library GenFu
        /// </summary>
        /// <param name="urlQueryParameters"></param>
        /// <returns></returns>
        [Route("paged")]
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] GetAllPersonsParameters urlQueryParameters)
        {
            var result = await unitOfWork.Person.GetPagedAsync(urlQueryParameters);
            var data = result.Data;
            var pagination = result.Pagination;

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

            return Ok(data);
        }

    }
}