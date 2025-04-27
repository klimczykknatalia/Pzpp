using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pzpp.Controllers.Api;
using Pzpp.Data;
using Pzpp.Data.Entities;

namespace Pzpp.Tests
{
    public class ContactsApiControllerTests
    {
        private ApplicationDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new ApplicationDbContext(options);
        }

        private ClaimsPrincipal GetUser(string userId)
        {
            var claims = new List<Claim> { new Claim("sub", userId) };
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));
        }

        [Fact]
        public async Task Get_Returns_Only_User_Contacts()
        {
            var ctx = GetDbContext("GetOnlyUserContacts");
            ctx.Contacts.AddRange(
                new Contact { Id = 1, FirstName = "A", LastName = "B", Email = "a@b.com", PhoneNumber="123", UserId="u1" },
                new Contact { Id = 2, FirstName = "C", LastName = "D", Email = "c@d.com", PhoneNumber="456", UserId="u2" }
            );
            await ctx.SaveChangesAsync();

            var controller = new ContactsApiController(ctx)
            {
                ControllerContext = { HttpContext = new DefaultHttpContext { User = GetUser("u1") } }
            };

            var result = await controller.Get();
            Assert.Single(result);
            Assert.Equal("u1", result.First().UserId);
        }

        [Fact]
        public async Task GetById_Returns_NotFound_For_Wrong_User()
        {
            var ctx = GetDbContext("GetById_NotFound");
            ctx.Contacts.Add(new Contact { Id = 1, FirstName = "X", LastName = "Y", Email="x@y.com", PhoneNumber="000", UserId="u1" });
            await ctx.SaveChangesAsync();

            var controller = new ContactsApiController(ctx)
            {
                ControllerContext = { HttpContext = new DefaultHttpContext { User = GetUser("u2") } }
            };

            var actionResult = await controller.Get(1);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task Post_Creates_New_Contact_Assigned_To_User()
        {
            var ctx = GetDbContext("Post_Creates_Contact");
            var controller = new ContactsApiController(ctx)
            {
                ControllerContext = { HttpContext = new DefaultHttpContext { User = GetUser("u1") } }
            };

            var newContact = new Contact
            {
                FirstName = "New", LastName = "Contact",
                Email = "new@c.com", PhoneNumber = "999", UserId = null
            };

            var result = await controller.Post(newContact);
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var contact = Assert.IsType<Contact>(created.Value);

            Assert.Equal("u1", contact.UserId);
            Assert.Equal(1, ctx.Contacts.Count());
        }
    }
}
