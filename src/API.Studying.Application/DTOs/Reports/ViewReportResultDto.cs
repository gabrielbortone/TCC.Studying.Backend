using System;
using System.Collections.Generic;

namespace API.Studying.Application.DTOs.Reports
{
    public class ViewReportResultDto
    {
        public List<DateTime> Dates { get; set; }
        public List<StudentDto> Students { get; set; }
        public ViewReportResultDto()
        {
            Dates = new List<DateTime>();
            Students = new List<StudentDto>();
        }
    }
}
