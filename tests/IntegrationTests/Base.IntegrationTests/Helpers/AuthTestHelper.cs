using Base.Api.Common;
using Base.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Base.IntegrationTests.Helpers
{
    public static class AuthTestHelper
    {
        public static async Task RegisterUserAsync(HttpClient client, string name, string username, string email, string password)
        {
            var response = await client.PostAsJsonAsync("/api/auth/register", new
            {
                name,
                username,
                email,
                password
            });

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("🟠 REGISTER STATUS: " + response.StatusCode);
            Console.WriteLine("🟠 REGISTER RESPONSE:");
            Console.WriteLine(content);

            response.EnsureSuccessStatusCode();
        }

        public static async Task<string> LoginAndGetTokenAsync(HttpClient client, string emailOrUsername, string password)
        {
            var response = await client.PostAsJsonAsync("/api/auth/login", new
            {
                username = emailOrUsername,
                password
            });

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("🔴 LOGIN STATUS: " + response.StatusCode);
            Console.WriteLine("🔴 LOGIN RESPONSE:");
            Console.WriteLine(content);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Falha ao logar: " + content);

            var result = await response.Content.ReadFromJsonAsync<LoginResponseViewModel>();
            return result?.Token ?? throw new Exception("Token não retornado.");
        }
    }
}
