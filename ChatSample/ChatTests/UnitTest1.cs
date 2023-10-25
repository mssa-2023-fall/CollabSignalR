using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ChatSample.Hubs;

namespace ChatTests
{
    [TestClass]
    public class ChatHubTests
    {

        [TestMethod]
        public async Task SendBroadcastsMessageToClients()
        {
            // Arrange
            var hubClients = new Mock<IHubCallerClients<IClientProxy>>();
            var chatHub = new ChatHub();

            
            chatHub.Clients = hubClients.Object;

            string name = "TestUser";
            string message = "Hello, World!";

            // Act
            await chatHub.Send(name, message);

            // Assert
            hubClients.Verify(c => c.All.SendAsync("broadcastMessage", name, message, default), Times.Once);
        }

    }
}