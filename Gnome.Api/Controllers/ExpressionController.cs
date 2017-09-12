﻿using Gnome.Api.Services.Expressions.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gnome.Api.Controllers
{
    [Route("api/filter")]
    public class ExpressionController : IUserAuthenticatedController
    {
        private readonly IMediator mediator;

        public Guid UserId { get; set; }

        public ExpressionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{expressionId:Guid}")]
        public async Task<IActionResult> Get(Guid expressionId)
        {
            return new OkObjectResult(await mediator.Send(new GetExpression(expressionId)));
        }

        [HttpDelete("{expressionId:Guid}")]
        public async Task<IActionResult> Remove(Guid expressionId)
        {
            await mediator.Send(new RemoveExpression() { ExpressionId = expressionId });
            return new OkResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpression toCreate)
        {
            var id = await mediator.Send(toCreate);
            return new OkObjectResult(id);
        }

        [HttpPut("expressionId:Guid")]
        public async Task<IActionResult> Update(Guid expressionId, UpdateExpression toUpdate)
        {
            await mediator.Send(toUpdate);
            return new OkResult();
        }
    }
}