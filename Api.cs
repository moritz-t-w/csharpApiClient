﻿using System.Text.Json;

namespace Api
{
    public class Api<TAuthentication> where TAuthentication : IAuthenticator
    {
        /** Base URL */
        private readonly Uri _baseUrl;
        /** Endpoints */
        public readonly List<Endpoint> Endpoints;
        /** Authentication */
        public readonly Type Authentication;

        public Api(Uri baseUrl, List<Endpoint> endpoints)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            Endpoints = endpoints ?? throw new ArgumentNullException(nameof(endpoints));
            this.Authentication = typeof(TAuthentication);
        }
    }
}
