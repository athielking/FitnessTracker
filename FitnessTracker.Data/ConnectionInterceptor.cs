using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Data
{
    public class ConnectionInterceptor : DbConnectionInterceptor
    {
        private readonly string _partialDns;
        
        public ConnectionInterceptor(string partialDns)
        {
            if (string.IsNullOrEmpty(partialDns))
            {
                throw new ArgumentNullException(nameof(partialDns), "Failed to retrieve PartialDns from appsettings.  Appsettings value cannot be null");
            }
            _partialDns = partialDns;
        }

        public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            if (connection is SqlConnection conn && conn.DataSource.Contains($".{_partialDns}", StringComparison.InvariantCultureIgnoreCase))
            {
                conn.AccessToken = new AzureServiceTokenProvider()
                    .GetAccessTokenAsync($"https://{_partialDns}/")
                    .GetAwaiter()
                    .GetResult();
            }

            return result;
        }

        public override async Task<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            if (connection is SqlConnection conn && conn.DataSource.Contains($".{_partialDns}", StringComparison.InvariantCultureIgnoreCase))
            {
                conn.AccessToken = await new AzureServiceTokenProvider()
                    .GetAccessTokenAsync($"https://{_partialDns}/");
            }

            return result;
        }
    }
}
