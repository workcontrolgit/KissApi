using Microsoft.AspNetCore.Mvc;
using Kiss.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Kiss.Core.Entities;
using Microsoft.AspNetCore.Http;
using Kiss.Application.Parameters;

namespace Kiss.Api.Controllers.v1
{
    //    [Route("api/v1/[controller]")]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public PersonsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// SELECT records from mock library GenFu
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Person>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] RequestParameter filter)
        {
            var data = await unitOfWork.Persons.GetAllAsync(filter.PageNumber, filter.PageSize);
            return Ok(data);
        }
    }
}