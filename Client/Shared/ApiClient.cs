// Copyright (c) Reaptor AB. All rights reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace OpenVisitor.Client.Shared
{
    public class ApiClient
    {
        private readonly IAccessTokenProvider _authenticationService;
        private readonly NavigationManager _navigation;

        public ApiClient(IAccessTokenProvider authenticationService, NavigationManager navigation)
        {
            _authenticationService = authenticationService;
            _navigation = navigation;
        }

        public async Task Authorized(Action<HttpClient> action)
        {
            var httpClient = new HttpClient {BaseAddress = new Uri(_navigation.BaseUri)};
            var tokenResult = await _authenticationService.RequestAccessToken();
            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
                action(httpClient);
            }
            else
            {
                _navigation.NavigateTo(tokenResult.RedirectUrl);
            }
        }
    }
}