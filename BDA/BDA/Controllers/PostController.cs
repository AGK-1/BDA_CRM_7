using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BDA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace BDA.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly ExcelService _excelService;
		public PostController(AppDbContext context, ExcelService excelService)
		{
			_context = context;
			_excelService = excelService;
		}

		[HttpGet("GET DATA")]
		public async Task<IActionResult> GetCustomers()
		{
			//var customers = await _context.Customer.ToListAsync();
			var customers = await _context.Customer
								  .Include(c => c.CreatedByUser) // Eager load the related User data
								  .ToListAsync();
			return Ok(customers);
		}

		// POST: api/products
		[HttpPost]
		public async Task<ActionResult<Customers>> PostCustomer(Customers customer)
		{
			_context.Customer.Add(customer);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
		}

		[HttpPost("Merhaba")]
		public IActionResult Post([FromBody] Customers customer)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Load the User from the database
			var user = _context.Users.Find(customer.CreatedByUserId);
			if (user != null)
			{
				customer.CreatedByUser = user;
			}

			_context.Customer.Add(customer);
			_context.SaveChanges();

			return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
		}


			// POST: api/Post/ImportExcel
		[HttpPost("ImportExcel")]
		public async Task<IActionResult> ImportExcel(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return BadRequest("No file uploaded.");

			// Save the file to a temporary location
			var filePath = Path.GetTempFileName();
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			// Import data from Excel
			var customers = _excelService.ImportExcelData(filePath);
			
			// Save to the database
			_context.Customer.AddRange(customers);
			await _context.SaveChangesAsync();

			//return Ok("Data imported successfully.");
			string htmlContent = @"
            <span class='badge bg-primary' style='color: red;font-size: 50px;text-align: center;'>data imported successfully!</span>
            <script>
                setTimeout(function() {
                    window.location.href = 'https://www.google.com'; // Redirect to Google
                }, 2000); // Redirect after 2 seconds
            </script>";

			return Content(htmlContent, "text/html");
			//return Redirect("file:///C:/Users/wikto/OneDrive/Desktop/lessone/c-charpok/BDA/Cart.html");
		}

		[HttpPost("Add Users")]
		public IActionResult Post(User customer)  // Add [FromBody]
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			//customer.Id = user.Id;
			_context.Users.Add(customer);

			return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
		}
	}
}
