using CakeManager.Shared;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace CakeManager.Client.Extensions
{
    public static class AdalExtensions
    {
        public static async Task LogIn(this IJSRuntime jsRuntime)
        {
            try
            {
                await jsRuntime.InvokeAsync<string>("logIn");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task<string> GetToken(this IJSRuntime jsRuntime)
        {
            try
            {
                return await jsRuntime.InvokeAsync<string>("getToken");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static async Task LogOut(this IJSRuntime jsRuntime)
        {
            try
            {
                await jsRuntime.InvokeAsync<bool>("logOut");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task<User> GetUser(this IJSRuntime jsRuntime)
        {
            try
            {
                return await jsRuntime.InvokeAsync<User>("getUser");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
