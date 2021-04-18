using Hangfire;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Helpers
{
    public class MediatorWrapper
    {
        private readonly IMediator _mediator;
        public MediatorWrapper(IMediator mediator)
        {
            _mediator = mediator;
        }

        [DisplayName("{0}")]
        [AutomaticRetry(Attempts = 0)]
        public async Task Send(string displayName, IRequest<Unit> request)
        {
            await _mediator.Send(request);
        }
    }
}
