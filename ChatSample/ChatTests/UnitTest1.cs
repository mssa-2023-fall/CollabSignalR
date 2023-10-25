using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ChatSample.Hubs;
using Microsoft.AspNetCore.Builder;

namespace ChatSample.Tests
{
    [TestClass]
    public class ProgramTests
    {       
        [TestMethod]
        public void CreateHostBuilder_ReturnsIHostBuilder()
        {
            // Arrange
            var args = Array.Empty<string>();

            // Act
            var result = Program.CreateHostBuilder(args);

            // Assert
            NUnit.Framework.Assert.IsInstanceOf<IHostBuilder>(result);
        }
    }

    [TestClass]
    public class StartupTests
    {
        [TestMethod]
        public void ConfigureServices_AddsSignalRService()
        {
            // Arrange
            var services = new ServiceCollection();
            var startup = new Startup();

            // Act
            startup.ConfigureServices(services);

            // Assert
            NUnit.Framework.Assert.IsNotNull(services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(Microsoft.AspNetCore.SignalR.ISignalRServerBuilder)));
        }

        [TestMethod]
        public void Configure_AddsHubEndpoint()
        {
            // Arrange
            var appBuilderMock = new Mock<IApplicationBuilder>();
            var envMock = new Mock<IWebHostEnvironment>();

            var startup = new Startup();
            startup.Configure(appBuilderMock.Object, envMock.Object);

            // Assert
            // Verify that MapHub is called with the correct arguments
            appBuilderMock.Verify(app => app.UseEndpoints(ep => ep.MapHub<ChatHub>("/chat")), Times.Once);
        }
    }
}


