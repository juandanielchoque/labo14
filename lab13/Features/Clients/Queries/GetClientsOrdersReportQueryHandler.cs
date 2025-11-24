using ClosedXML.Excel;
using lab13.Repositories; 
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace lab13.Features.Clients.Queries
{
    public class GetClientsOrdersReportQueryHandler : IRequestHandler<GetClientsOrdersReportQuery, MemoryStream>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientsOrdersReportQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<MemoryStream> Handle(GetClientsOrdersReportQuery request, CancellationToken cancellationToken)
        {
            var clientsWithOrders = await _clientRepository.GetClientsWithOrdersAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Clientes y Ordenes");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "ClientId";
                worksheet.Cell(currentRow, 2).Value = "Client Name";
                worksheet.Cell(currentRow, 3).Value = "Client Email";
                worksheet.Cell(currentRow, 4).Value = "OrderId";
                worksheet.Cell(currentRow, 5).Value = "OrderDate";
                worksheet.Row(currentRow).Style.Font.Bold = true;
                
                foreach (var client in clientsWithOrders)
                {
                    if (!client.Orders.Any())
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = client.ClientId;
                        worksheet.Cell(currentRow, 2).Value = client.Name;
                        worksheet.Cell(currentRow, 3).Value = client.Email;
                    }
                    else
                    {
                        foreach (var order in client.Orders)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = client.ClientId;
                            worksheet.Cell(currentRow, 2).Value = client.Name;
                            worksheet.Cell(currentRow, 3).Value = client.Email;
                            worksheet.Cell(currentRow, 4).Value = order.OrderId;
                            worksheet.Cell(currentRow, 5).Value = order.OrderDate;
                        }
                    }
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