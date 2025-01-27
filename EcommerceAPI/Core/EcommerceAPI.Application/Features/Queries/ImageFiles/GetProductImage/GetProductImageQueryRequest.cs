using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.ImageFiles.GetProductImage
{
	public class GetProductImageQueryRequest : IRequest<List<GetProductImageQueryResponse>>
	{
        public string Id { get; set; }
    }
}
