using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Core.DTOs.StatisticksDTOs;

namespace TooliRent.Core.Interfaces.IService
{
    public interface IStatisticsService
    {
        Task<StatisticsDTO> GetStatisticsAsync(CancellationToken ct = default);
    }
}
