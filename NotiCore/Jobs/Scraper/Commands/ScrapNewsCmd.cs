using Hangfire;
using MediatR;
using NotiCore.API.Jobs.Notifications;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Services.ControllerServices;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotiCore.API.Jobs.Scraper.Commands
{
    public class ScrapNewsCmd : IRequest<Unit>
    {
    }
    public class ScrapNewsCmdHandler : IRequestHandler<ScrapNewsCmd, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IArticleService _articleService;
        private readonly DataContext _context;
        public ScrapNewsCmdHandler(IArticleService articleService, DataContext context, IMediator mediator)
        {
            _articleService = articleService;
            _context = context;
            _mediator = mediator;

        }
        public async Task<Unit> Handle(ScrapNewsCmd request, CancellationToken cancellationToken)
        {
            // ToDo: Resolve db context concurrency issue
            //var task = _context.Sources.Where(s=> s.IsActive).ToList().Select(async source =>
            //{
            //    await _articleService.SaveArticlesFromSourceAsync(source);
            //});
            //await Task.WhenAll(task);
            var sources = _context.Sources
                .Where(s=> s.IsActive && s.LastScrapedDate < DateTime.Today)
                .ToList();

            foreach (var source in sources)
            {
               // BackgroundJob.Enqueue(()=> _articleService.SaveArticlesFromSourceAsync(source));
               await _articleService.SaveArticlesFromSourceAsync(source);
            }
            //PythonEngine.EndAllowThreads();

            await _mediator.Publish(new ScrappedArticlesNotification());
            return Unit.Value;
        }
    }
}
