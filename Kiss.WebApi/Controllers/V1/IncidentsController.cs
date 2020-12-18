using Microsoft.AspNetCore.Mvc;
using Kiss.Application.Interfaces;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

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
    }
}