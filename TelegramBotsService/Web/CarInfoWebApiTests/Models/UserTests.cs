using System.Collections.Generic;
using CarInfoWebApi.Models;
using Xunit;

namespace CarInfoWebApiTests.Models
{
    public class UserTests
    {
        private readonly User _target;

        public UserTests()
        {
            _target = new User
            {
                Id = 567,
                Name = "PropName",
                Age = 123,
            };
        }

        [Fact]
        public void GetId()
        {
            Assert.Equal(567, _target.Id);
        }

        [Theory]
        [InlineData(890)]
        [InlineData(687)]
        public void SetId(int id)
        {
            _target.Id = id;
            Assert.Equal(id, _target.Id);
        }

        [Fact]
        public void GetName()
        {
            Assert.Equal("PropName", _target.Name);
        }

        [Fact]
        public void SetName()
        {
            _target.Name = "NewName";
            Assert.Equal("NewName", _target.Name);
        }

        [Fact]
        public void GetAge()
        {
            Assert.Equal(123, _target.Age);
        }

        [Theory]
        [MemberData(nameof(Ages))]
        public void SetAge(int age)
        {
            _target.Age = age;
            Assert.Equal(age, _target.Age);
        }

        public static IEnumerable<object[]> Ages()
        {
            yield return new object[] {20};
            yield return new object[] {40};
        }
    }
}