using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Exceptions
{
    public class SkuAlreadyExistsException : ApplicationException
    {
        public SkuAlreadyExistsException(string sku)
       : base($"Ya existe un producto con el SKU '{sku}'.") { }
    }
}
