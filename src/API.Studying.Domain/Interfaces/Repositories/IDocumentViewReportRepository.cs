using API.Studying.Domain.Entities.Reports;
using System;
using System.Collections.Generic;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface IDocumentViewReportRepository : IRepositoryBase<DocumentViewReport>
    {
        List<DocumentViewReport> GetAll(DateTime begin, DateTime end, bool top);
    }
}
