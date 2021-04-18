using Hangfire;
using MediatR;
using Newtonsoft.Json;
using NotiCore.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Infraestructure.Extensions
{
    public static class HangfireExtensions
    {
        public static void Enqueue(this IMediator mediator, string displayName, IRequest<Unit> request)
        {
            var job = new BackgroundJobClient();
            job.Enqueue<MediatorWrapper>(c=> c.Send(displayName, request));
        }

        public static void UseMediator(this IGlobalConfiguration configuration)
        {
            var json = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };
            configuration.UseSerializerSettings(json);
        }
    }
}
