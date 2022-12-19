using Microsoft.AspNetCore.Mvc;
using TechTest_v2.Shared;
using Bogus;

namespace TechTest_v2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        public IEnumerable<Employee>? employees;

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            int idInc = 0;

            Faker<Employee> faker = new Faker<Employee>()
                    .StrictMode(true)
                    .RuleFor(o => o.Id, f => idInc++)
                    .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                    .RuleFor(o => o.LastName, f => f.Name.LastName())
                    .RuleFor(o => o.Phone, f => f.Phone.PhoneNumber())
                    .RuleFor(o => o.Address, f => f.Address.StreetAddress())
                    .RuleFor(o => o.City, f => f.Address.City())
                    .RuleFor(o => o.State, f => f.Address.StateAbbr())
                    .RuleFor(o => o.Zip, f => f.Address.ZipCode())
                    .RuleFor(o => o.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName, "somecompany.com"));
            employees = faker.Generate(100).ToArray();

            return employees;
        }
    }
}
