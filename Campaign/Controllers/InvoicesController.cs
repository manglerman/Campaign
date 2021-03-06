using Campaign.Core.Exceptions;
using Campaign.Core.Models.RequestModel;
using Campaign.Core.Services;
using Campaign.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoicesService _service;
        public InvoicesController(IInvoicesService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _service.Get(id);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _service.GetAll();
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InvoicesModel model)
        {
            try
            {
                var response = await _service.Add(model);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost("AddProductToInvoice")]
        public async Task<IActionResult> AddProductToInvoice([FromBody] AddProductToInvoiceRequestModel model)
        {
            try
            {
                var response = await _service.AddProduct(model);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
        [HttpPost("RemoveProductFromInvoice")]
        public async Task<IActionResult> RemoveProductFromInvoice([FromBody] RemoveProductFromInvoiceRequestModel model)
        {
            try
            {
                var response = await _service.RemoveProduct(model);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] InvoicesModel model)
        {
            try
            {
                var response = await _service.Update(model);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _service.Delete(id);
                return Ok(response);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
