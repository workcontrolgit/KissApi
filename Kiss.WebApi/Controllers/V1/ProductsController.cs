using Microsoft.AspNetCore.Mvc;
using Kiss.Application.Interfaces;
using Kiss.Application.Parameters;
using Kiss.Core.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Text.Json;

namespace Kiss.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Authorize]
    public class ProductsController : BaseApiController
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
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Product.GetAllAsync();
            return Ok(data);
        }
        /// <summary>
        /// Page records
        /// </summary>
        /// <param name="urlQueryParameters"></param>
        /// <returns></returns>
        [Route("paged")]
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] GetAllProductsParameter urlQueryParameters)
        {
            var result = await unitOfWork.Product.GetPagedAsync(urlQueryParameters);
            var data = result.Data;
            var pagination = result.Pagination;

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

            return Ok(data);
        }
        /// <summary>
        /// SELECT a record by id
        /// </summary>
        /// <param name="id">product unique id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await unitOfWork.Product.GetByIdAsync(id);
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
            var data = await unitOfWork.Product.AddAsync(product);
            return Ok(data);
        }
        /// <summary>
        /// UPDATE a record
        /// </summary>
        /// <param name="product">Data fields for update</param>
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var data = await unitOfWork.Product.UpdateAsync(product);
            return Ok(data);
        }
        /// <summary>
        /// DELETE a record by id
        /// </summary>
        /// <param name="id">product unique id</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await unitOfWork.Product.DeleteAsync(id);
            return Ok(data);
        }
    }
}