using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Services
{
    public class CustomerSeederFromJson : ICustomerSeeder
    {   
        private readonly IRepository _repository;

        public CustomerSeederFromJson(IRepository repository)
        {
            _repository = repository;
        }

        public async Task SeedFromJsonAsync(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException("El archivo JSON no fue encontrado.", jsonFilePath);

            var json = await File.ReadAllTextAsync(jsonFilePath);
            var customers = JsonSerializer.Deserialize<List<Customer>>(json);

            if (customers is null || !customers.Any())
                throw new InvalidOperationException("No se encontraron clientes válidos en el archivo.");

            foreach (var customer in customers)
            {
                var exists = await _repository.First<Customer>(c => c.Id == customer.Id) != null;
                if (!exists)
                    await _repository.Add(customer);
            }
        }
    }
}
