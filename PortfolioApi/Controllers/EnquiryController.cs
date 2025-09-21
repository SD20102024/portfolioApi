using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly EnquiryService _enquiryService;
        public EnquiryController(EnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                var count = await _enquiryService.CountEnquiriesAsync();
                return Ok(new { success = true, message = "MongoDB connection successful!", documentCount = count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "MongoDB connection failed!", error = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateEnquiry([FromBody] EnquiryFormDto enquiryForm)
        {
            if (enquiryForm.Interest=="yes" && (string.IsNullOrWhiteSpace(enquiryForm.ContactName)||(string.IsNullOrWhiteSpace(enquiryForm.ContactNumber))))
            {
                return BadRequest("Name and Phone details are reuired if interested");
            }
            await _enquiryService.CreateEnquiryAsync(enquiryForm);
            return Ok(new { message="Enquiry info received",data=enquiryForm});
        }
    }
}
