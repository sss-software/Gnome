﻿using Gnome.Api.Services.Reports.Requests;
using Gnome.Core.DataAccess;
using Gnome.Core.Reports;
using Gnome.Core.Reports.Cummulative;
using Gnome.Core.Service.Query;
using Gnome.Core.Service.Search.Filters;
using MediatR;

namespace Gnome.Api.Services.Reports
{
    public class CumulativeReportHandler : IRequestHandler<GetCumulativeReport, AggregateEnvelope>
    {
        private readonly ICumulativeReportService service;
        private readonly IQueryService queryService;
        private readonly IReportRepository reportRepository;

        public CumulativeReportHandler(
            ICumulativeReportService service,
            IQueryService queryService,
            IReportRepository reportRepository)
        {
            this.service = service;
            this.queryService = queryService;
            this.reportRepository = reportRepository;
        }

        public AggregateEnvelope Handle(GetCumulativeReport message)
        {
            var report = reportRepository.Find(message.ReportId);
            var query = queryService.Get(report.Id);

            var filter = new TransactionSearchFilter()
            {
                Accounts = query.Accounts,
                DateFilter = message.DateFilter,
                ExcludeExpressions = query.ExcludeExpressions,
                IncludeExpressions = query.IncludeExpressions
            };

            return service.Report(filter, message.UserId);
        }
    }
}
