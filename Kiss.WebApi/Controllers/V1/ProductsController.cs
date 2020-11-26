using Microsoft.AspNetCore.Mvc;
using Kiss.Application.Interfaces;
using Kiss.Application.Parameters;
using Kiss.Core.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Kiss.Api.Controllers.v1
{
    //[Route("api/v1/[controller]")]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// SELECT records
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductsParameter filter)
        {
            var data = await unitOfWork.Products.GetAllAsync(filter.PageNumber, filter.PageSize);
            return Ok(data);
        }
        /// <summary>
        /// SELECT a record by id
        /// </summary>
        /// <param name="id">product unique id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await unitOfWork.Products.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        /// <summary>
        /// INSERT a record
        /// </summary>
        /// <param name="product">Data fields for insert</param>
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            var data = await unitOfWork.Products.AddAsync(product);
            return Ok(data);
        }
        /// <summary>
        /// UPDATE a record
        /// </summary>
        /// <param name="product">Data fields for update</param>
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var data = await unitOfWork.Products.UpdateAsync(product);
            return Ok(data);
        }
        /// <summary>
        /// DELETE a record by id
        /// </summary>
        /// <param name="id">product unique id</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await unitOfWork.Products.DeleteAsync(id);
            return Ok(data);
        }
    }
}