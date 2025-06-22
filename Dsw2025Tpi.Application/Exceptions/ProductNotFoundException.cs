using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Exceptions
{
    public class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException(Guid id)
        : base($"No se encontró un producto con ID '{id}'.") { }
    }
}
