using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xrm.Sdk;
using System;

namespace SignalRPlugin
{
    public class SignalRPlugin : IPlugin
    {
        private string _serverUrl;

        public SignalRPlugin(string serverUrl)
        {
            if (String.IsNullOrEmpty(serverUrl))
                throw new InvalidPluginExecutionException("serverUrl");

            _serverUrl = serverUrl;
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.PostEntityImages.Contains("PostImage") &&
                context.PostEntityImages["PostImage"] is Entity)
            {
                var entity = (Entity)context.PostEntityImages["PostImage"];

                try
                {
                    var hubConnection = new HubConnection(_serverUrl);
                    IHubProxy logHubProxy = hubConnection.CreateHubProxy("logHub");
                    hubConnection.Start().Wait();
                    logHubProxy.Invoke("Send", entity["fullname"], DateTime.Now.ToLocalTime().ToString());
                }
                catch (Exception e)
                {
                    tracingService.Trace("Exception: {0}", e.ToString());
                    throw;
                }
            }
        }
    }
}
