using MediatR;
using Microsoft.AspNetCore.Mvc;
using lab13.Features.Products.Queries; 
using lab13.Features.Clients.Queries; 

namespace lab13.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("products")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public async Task<IActionResult> GetProductsReport()
        {
            var query = new GetProductsReportQuery();
            var stream = await _mediator.Send(query);

            return File(stream, 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                "Reporte_Productos.xlsx");
        }

        [HttpGet("clients-orders")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public async Task<IActionResult> GetClientsOrdersReport()
        {
            var query = new GetClientsOrdersReportQuery();
            var stream = await _mediator.Send(query);

            return File(stream, 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                "Reporte_Clientes_y_Ordenes.xlsx");
        }
    }
}