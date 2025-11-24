using MediatR;
using System.IO;

namespace lab13.Features.Clients.Queries
{
    public class GetClientsOrdersReportQuery : IRequest<MemoryStream> { }
}