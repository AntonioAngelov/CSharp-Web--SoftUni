﻿namespace WebServer.Server.Http
{
    using System.Collections;
    using System.Collections.Generic;
    using Common;
    using Contracts;
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            this.cookies[cookie.Key] = cookie;
        }

        public void Add(string key, string value)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            CoreValidator.ThrowIfNull(value, nameof(value));

            this.Add(new HttpCookie(key, value));
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            return this.cookies.ContainsKey(key);
        }

        public HttpCookie Get(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.cookies.ContainsKey(key))
            {
                return null;
            }

            return this.cookies[key];
        }

        public IEnumerator<HttpCookie> GetEnumerator()
            => this.cookies.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.cookies.Values.GetEnumerator();
    }
}
