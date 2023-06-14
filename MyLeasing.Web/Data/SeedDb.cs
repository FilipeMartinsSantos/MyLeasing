using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random(); 
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("santos.filipe.m@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    Document = "1",
                    FirstName = "Filipe",
                    LastName = "Santos",
                    Email = "santos.filipe.m@hotmail.com",
                    UserName = "santos.filipe.m@hotmail.com",
                    PhoneNumber = "2444567890",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }
            }

            if (!_context.Owners.Any())
            {
                AddOwner("Laura", "Fernandes", "230654321", "930456789", "Rua dos Fernandes", user);
                AddOwner("Guilherme", "Carreira", "216543210", "934567890", "Rua dos Carreiras", user);
                AddOwner("Camila", "Oliveira", "225432109", "915678901", "Rua das Oliveiras", user);
                AddOwner("Diogo", "Silva", "253321098", "926789012", "Rua das Silvas", user);
                AddOwner("Isabela", "Rodrigues", "223210987", "967890123", "Rua dos Rodrigues", user);
                AddOwner("Rafael", "Lima", "232109876", "918901234", "Rua das Limas", user);
                AddOwner("Larissa", "Pereira", "221098765", "939012345", "Rua das Pereiras", user);
                AddOwner("Gabriel", "Sousa", "210987654", "930123456", "Rua dos Sousas", user);
                AddOwner("Beatriz", "Mendes", "219876543", "911234567", "Rua dos Mendes", user);
                AddOwner("Pedro", "Costa", "244765432", "912345678", "Rua dos Costas", user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string firstname, string lastname, string fixedPhone, string cellPhone, string address, User user)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(10000),
                FirstName = firstname,
                LastName = lastname,
                FixedPhone = fixedPhone,
                CellPhone = cellPhone,
                Address = address,
                User = user,
            });
        }
    }
}
