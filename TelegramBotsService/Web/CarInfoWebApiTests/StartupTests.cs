using System;
using System.Collections.Generic;
using CarInfoWebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Xunit;

namespace CarInfoWebApiTests
{
    public class StartupTests
    {
        private readonly Startup _target;

        public StartupTests()
        {
            var configuration = Substitute.For<IConfiguration>();
            _target = new Startup(configuration);
        }

        [Fact]
        public void Configuration_Test()
        {
            // Arrange
            // Action
            var actual = _target.Configuration;

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void ConfigureServicesTest()
        {
            var serviceCollection = Substitute.For<IServiceCollection>();
            _target.ConfigureServices(serviceCollection);
            serviceCollection.Received().AddControllers();
        }

        [Fact]
        public void Test()
        {
            // Arrange
            var applicationBuilder = Substitute.For<IApplicationBuilder>();
            applicationBuilder
                .ApplicationServices
                .GetService(Arg.Any<Type>()).Returns(applicationBuilder);

            var endpointRouteBuilder = Substitute.For<IEndpointRouteBuilder>();

            var properties = Substitute.For<IDictionary<string, object>>();
            properties.TryGetValue("__EndpointRouteBuilder", out var prop)
                .Returns(x =>
                {
                    x[1] = endpointRouteBuilder;
                    return true;
                });
            applicationBuilder
                .Properties.Returns(properties);

            void Configure(IEndpointRouteBuilder builder)
            {
                endpointRouteBuilder.MapControllers();
            }

            var webHostEnvironment = Substitute.For<IWebHostEnvironment>();
            webHostEnvironment.EnvironmentName.Returns(Environments.Development);

            // Act
            Action action = () =>
            {
                _target.Configure(applicationBuilder, webHostEnvironment);
            };

            // Assert
            Assert.Throws<InvalidCastException>(action);
            webHostEnvironment.Received().IsDevelopment();
            applicationBuilder.Received().UseDeveloperExceptionPage();
            applicationBuilder.Received().UseHttpsRedirection();
            applicationBuilder.Received().UseRouting();
            applicationBuilder.Received().UseAuthentication();
        }
    }
}