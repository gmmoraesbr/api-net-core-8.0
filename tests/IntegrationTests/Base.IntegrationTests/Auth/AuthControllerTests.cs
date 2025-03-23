using System.Net.Http.Json;
using Xunit;
using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Base.Application.ViewModels;
using Base.Api.Common;
using Base.IntegrationTests.Helpers;

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
            var username = "admin@admin2.com";
            var email = "admin@admin2.com";
            var password = "SenhaForte123!";

            _client.DefaultRequestHeaders.Remove("X-Correlation-ID");
            _client.DefaultRequestHeaders.Add("X-Correlation-ID", "d9a5a5f2-6a93-4a71-9db9-f74c78e32ec5");

            await AuthTestHelper.RegisterUserAsync(_client, "Admin", username, email, password);

            var token = await AuthTestHelper.LoginAndGetTokenAsync(_client, username, password);

            token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Should_Return_Unauthorized_With_Wrong_Credentials()
        {
            var payload = new
            {
                UserName = "admin@admin.com",
                Password = "senha_errada"
            };

            _client.DefaultRequestHeaders.Remove("X-Correlation-ID");
            _client.DefaultRequestHeaders.Add("X-Correlation-ID", "d9a5a5f2-6a93-4a71-9db9-f74c78e32ec5");

            var response = await _client.PostAsJsonAsync("/api/auth/login", payload);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
