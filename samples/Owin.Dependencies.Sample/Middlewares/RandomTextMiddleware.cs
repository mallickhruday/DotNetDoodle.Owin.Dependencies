﻿using Microsoft.Owin;
using Owin.Dependencies.Sample.Repositories;
using System.Threading.Tasks;

namespace Owin.Dependencies.Sample.Middlewares
{
    public class RandomTextMiddleware : OwinMiddleware
    {
        public RandomTextMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            IOwinDependencyScope dependencyScope = context.GetRequestDependencyScope();
            IRepository repository = dependencyScope.GetService(typeof(IRepository)) as IRepository;

            if (context.Request.Path == "/random")
            {
                await context.Response.WriteAsync(repository.GetRandomText());
            }
            else
            {
                context.Response.Headers.Add("X-Random-Sentence", new[] { repository.GetRandomText() });
                await Next.Invoke(context);
            }
        }
    }
}