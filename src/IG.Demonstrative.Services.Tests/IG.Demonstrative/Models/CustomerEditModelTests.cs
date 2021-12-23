using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IG.Demonstrative.Models
{
    public class CustomerEditModelTests
    {
        [Theory]
        [ClassData(typeof(CustomerEditModelInvalidData))]
        public void CheckForInvalidPropertyValues(string name, string errorMessage)
        {
            var sut = new CustomerEditModel { Name = name};

            var ex = Assert.Throws<ValidationException>(
                () => ValidationHelper.ValidateAnnotations(sut)
            );

            ex.Message.Should().Be(errorMessage);
        }

        [Theory]
        [InlineData("teste")]
        public void CheckForValidPropertyValues(string name)
        {
            var sut = new CustomerEditModel { Name = name };
            ValidationHelper.ValidateAnnotations(sut);
        }
    }
}
