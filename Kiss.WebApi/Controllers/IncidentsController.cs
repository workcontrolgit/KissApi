﻿using Microsoft.AspNetCore.Mvc;
using Kiss.Application.Interfaces;
using System.Threading.Tasks;

namespace Kiss.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
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
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var data = await unitOfWork.IncidentReport.GetAllAsync(pageNumber, pageSize);
            return Ok(data);
        }
    }
}