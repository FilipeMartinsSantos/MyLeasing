using System;
using System.Linq;
using System.Threading.Tasks;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random(); 
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Owners.Any())
            {
                AddOwner("Laura", "Fernandes", "230654321", "930456789", "Rua dos Fernandes");
                AddOwner("Guilherme", "Carreira", "216543210", "934567890", "Rua dos Carreiras");
                AddOwner("Camila", "Oliveira", "225432109", "915678901", "Rua das Oliveiras");
                AddOwner("Diogo", "Silva", "253321098", "926789012", "Rua das Silvas");
                AddOwner("Isabela", "Rodrigues", "223210987", "967890123", "Rua dos Rodrigues");
                AddOwner("Rafael", "Lima", "232109876", "918901234", "Rua das Limas");
                AddOwner("Larissa", "Pereira", "221098765", "939012345", "Rua das Pereiras");
                AddOwner("Gabriel", "Sousa", "210987654", "930123456", "Rua dos Sousas");
                AddOwner("Beatriz", "Mendes", "219876543", "911234567", "Rua dos Mendes");
                AddOwner("Pedro", "Costa", "244765432", "912345678", "Rua dos Costas");
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string firstname, string lastname, string fixedPhone, string cellPhone, string address)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(10000),
                FirstName = firstname,
                LastName = lastname,
                FixedPhone = fixedPhone,
                CellPhone = cellPhone,
                Address = address,
            });
        }
    }
}
