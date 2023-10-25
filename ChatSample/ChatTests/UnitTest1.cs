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
}


