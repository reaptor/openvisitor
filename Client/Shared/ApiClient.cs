// Copyright (c) Reaptor AB. All rights reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;

namespace OpenVisitor.Client.Shared
{
    public class ApiClient : HttpClient
    {
        public ApiClient(IAccessTokenProvider authenticationService, NavigationManager navigation, ILogger<ApiClient> logger)
            : base(new AuthorizingHttpClientHandler(authenticationService))
        {
            BaseAddress = new Uri(navigation.BaseUri);
        }
    }

    class AuthorizingHttpClientHandler : HttpClientHandler
    {
        private readonly IAccessTokenProvider _authenticationService;

        public AuthorizingHttpClientHandler(IAccessTokenProvider authenticationService)
        {
            _authenticationService = authenticationService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tokenResult = await _authenticationService.RequestAccessToken();
            if (tokenResult.TryGetToken(out var token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue($"Bearer", token.Value);
            }
            else
            {
                throw new Exception($"Bearer token request failed.");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}