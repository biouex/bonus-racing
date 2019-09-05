using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BonusRacing.DataAccess.Repositories;
using BonusRacing.DataAccess.Services;
using BonusRacing.DataDomain.Common;

namespace BonusRacing.Api.Helpers
{
    public class LogRequestHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var settings = request.GetDependencyScope().GetService(typeof(SettingsService)) as SettingsService;
                if (await settings.GetLogEnable())
                {
                    await Log(request);
                }
            }
            catch
            {

            }
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task Log(HttpRequestMessage request)
        {
            var repository = request.GetDependencyScope().GetService(typeof(LogRepository)) as LogRepository;
            var logRequest = new LogRequest
            {
                Date = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Uri = request.RequestUri.OriginalString,
                Method = request.Method.ToString(),
                Headers = request.Headers.ToString()
            };

            var requestMessageBytes = await request.Content.ReadAsByteArrayAsync();
            string requestMessage = Encoding.UTF8.GetString(requestMessageBytes);
            logRequest.Content = requestMessage;
            await repository.Add(logRequest);
        }
    }
}