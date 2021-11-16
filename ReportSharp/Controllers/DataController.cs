using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportSharp.Attributes;
using ReportSharp.DbContext;

namespace ReportSharp.Controllers
{
    [ApiAuthorize]
    public class DataController : ControllerBase
    {
        public DataController(ReportSharpDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ReportSharpDbContext DbContext { get; }

        [HttpGet]
        public virtual async Task<ActionResult> Index()
        {
            if (!int.TryParse(HttpContext.Request.Query["page"], out var page)) page = 1;
            if (!int.TryParse(HttpContext.Request.Query["pageSize"], out var pageSize)) pageSize = 10;

            var logsQuery = DbContext.ReportSharpDatas
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new {
                    x.Id,
                    x.Tag,
                    x.Data,
                    x.CreatedAt
                });

            var logs = await logsQuery.Take(pageSize)
                .Skip((page - 1) * pageSize)
                .ToListAsync();
            var logsCount = await logsQuery.LongCountAsync();

            return Ok(new {
                Data = logs,
                CurrentPage = page,
                CurrentPageSize = pageSize,
                PageCount = (long) Math.Ceiling(logsCount / (double) pageSize),
                DataCount = logsCount
            });
        }

        [HttpGet]
        public virtual async Task<ActionResult> Show()
        {
            if (!int.TryParse(HttpContext.Request.RouteValues["id"]?.ToString(), out var id)) id = 0;

            if (!await DbContext.ReportSharpDatas.AnyAsync(x => x.Id == id)) return NotFound();

            var request = await DbContext.ReportSharpDatas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(request);
        }
    }
}