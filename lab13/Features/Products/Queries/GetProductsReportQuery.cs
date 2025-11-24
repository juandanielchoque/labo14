using MediatR;
using System.IO;

namespace lab13.Features.Products.Queries
{
    public class GetProductsReportQuery : IRequest<MemoryStream> { }
}