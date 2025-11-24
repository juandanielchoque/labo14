using ClosedXML.Excel;
using lab13.Repositories; 
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace lab13.Features.Products.Queries
{
    public class GetProductsReportQueryHandler : IRequestHandler<GetProductsReportQuery, MemoryStream>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsReportQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<MemoryStream> Handle(GetProductsReportQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Productos");
                var currentRow = 1;
                
                worksheet.Cell(currentRow, 1).Value = "ProductId";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Description";
                worksheet.Cell(currentRow, 4).Value = "Price";
                worksheet.Row(currentRow).Style.Font.Bold = true;
                foreach (var product in products)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = product.ProductId;
                    worksheet.Cell(currentRow, 2).Value = product.Name;
                    worksheet.Cell(currentRow, 3).Value = product.Description;
                    worksheet.Cell(currentRow, 4).Value = product.Price;
                }

                worksheet.Columns().AdjustToContents();

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
        }
    }
}