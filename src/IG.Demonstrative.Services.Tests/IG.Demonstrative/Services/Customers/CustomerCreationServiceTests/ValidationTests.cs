using FakeItEasy;
using FluentAssertions;
using IG.Demonstrative.Context;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace IG.Demonstrative.Services.Customers.CustomerCreationServiceTests
{
    public class ValidationTests
    {

        private readonly CustomerCreationService sut;

        public ValidationTests()
        {
            var context = A.Dummy<MainContext>();
            sut = new CustomerCreationService(context);
        }


        [Fact]
        public async Task ShouldNotAcceptNullArgument()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(
                () => sut.CreateAsync(null)
            );
            ex.ParamName.Should().Be("data");
        }


        [Theory]
        [ClassData(typeof(CustomerEditModelInvalidData))]

        public async Task ShouldValidateModel(string name, string errorMessage)
        {
            var ex = await Assert.ThrowsAsync<ValidationException>(
                () => sut.CreateAsync(new() { Name = name})
            );

            ex.Message.Should().Be(errorMessage);
        }

    }
}
