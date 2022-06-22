using API.Studying.Domain.Entities.Reports;
using System;
using System.Collections.Generic;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface IVideoViewReportRepository : IRepositoryBase<VideoViewReport>
    {
        List<VideoViewReport> GetAll(DateTime begin, DateTime end, bool top);
    }
}
