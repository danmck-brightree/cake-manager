﻿using System;
using System.Threading.Tasks;
using CakeManager.Client.Services;
using CakeManager.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;

namespace CakeManager.Client.Components.TokenGuard
{
    public class TokenGuardComponent : ComponentBase
    {
        #region dependency injection

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected ITokenService TokenService { get; set; }

        #endregion

        protected override async Task OnInitAsync()
        {
            var isLoggedIn = await TokenService.CheckToken();

            if (!isLoggedIn)
                UriHelper.NavigateTo(Constants.LoginRoute);

            await base.OnInitAsync();
        }

        public async Task CheckToken()
        {
            var checkToken = await TokenService.CheckToken();
            if (!checkToken)
                UriHelper.NavigateTo(Constants.LoginRoute);
        }
    }
}
