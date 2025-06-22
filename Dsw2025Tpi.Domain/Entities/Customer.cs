using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Customer : EntityBase
    {   
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Address { get; private set; } = null!;

        private Customer() { }

        public Customer(string name, string email, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre completo es obligatorio.", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo electrónico es obligatorio.", nameof(email));
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("La dirección es obligatoria.", nameof(address));

            Name = name;
            Email = email;
            Address = address;
        }
    }
}
