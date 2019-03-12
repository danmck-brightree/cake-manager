using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace CakeManager.Client.Extensions
{
    public static class JSRuntimeExtensions
    {
        public const string TokenKey = "Token";

        #region local storage

        public static async Task<string> GetItem(this IJSRuntime jsRuntime, string key)
        {
            string item = null;

            try
            {
                item = await jsRuntime.InvokeAsync<string>("getItem", new { key });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return item;
        }

        public static async Task SetItem(this IJSRuntime jsRuntime, string key, string value)
        {
            try
            {
                await jsRuntime.InvokeAsync<bool>("setItem", new { key, value });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task RemoveItem(this IJSRuntime jsRuntime, string key)
        {
            try
            {
                await jsRuntime.InvokeAsync<bool>("removeItem", new { key });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

        #region bootstrap modal

        public static async Task ShowModal(this IJSRuntime jsRuntime, string id)
        {
            try
            {
                await jsRuntime.InvokeAsync<bool>("showModal", new { id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task HideModal(this IJSRuntime jsRuntime, string id)
        {
            try
            {
                await jsRuntime.InvokeAsync<bool>("hideModal", new { id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}
