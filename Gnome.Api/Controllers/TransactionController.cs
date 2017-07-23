﻿using Gnome.Api.Services.Transactions.Requests;
using Gnome.Core.Model;
using Gnome.Core.Service.Search.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gnome.Api.Controllers
{
    [Route("api")]
    public class TransactionController : BaseController
    {
        private readonly IMediator mediator;

        public TransactionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("fio-transactions")]
        public async Task<IActionResult> CreateFio([FromBody]FioTransaction transaction)
        {
            return new OkObjectResult(await mediator.Send(new CreateFioTransaction(transaction)));
        }

        [HttpPost("accounts/{accountId:int}/transactions")]
        public async Task<IActionResult> Search(int accountId, [FromBody] SingleAccountTransactionSearchFilter filter)
        {
            filter.AccountId = accountId;

            return new OkObjectResult(await mediator.Send(new SingleAccountSearchTransaction(filter, UserId)));
        }
    }
}