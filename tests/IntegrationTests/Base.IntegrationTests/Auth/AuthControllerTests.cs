using System.Net.Http.Json;
using Xunit;
using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Base.Application.ViewModels;
using Base.Api.Common;

namespace Base.IntegrationTests.Auth
{
    public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Should_Login_Successfully_And_Return_Token()
        {
            // Arrange
            var payload = new
            {
                Username = "admin@admin.com",
                Password = "SenhaForte123!"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", payload);

            // DEBUG: mostrar erro se falhar
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("🔴 STATUS: " + response.StatusCode);
            Console.WriteLine("🔴 RESPONSE:");
            Console.WriteLine(content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseViewModel>>();

            result.Should().NotBeNull();
            result!.Data.Should().NotBeNull();
            result.Data!.Token.Should().NotBeNullOrEmpty();

        }


        [Fact]
        public async Task Should_Return_Unauthorized_With_Wrong_Credentials()
        {
            var payload = new
            {
                Email = "admin@admin.com",
                Password = "senha_errada"
            };

            var response = await _client.PostAsJsonAsync("/api/auth/login", payload);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
