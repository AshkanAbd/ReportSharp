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
    public class RequestController : ControllerBase
    {
        public RequestController(ReportSharpDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ReportSharpDbContext DbContext { get; }

        [HttpGet]
        public virtual async Task<ActionResult> Index()
        {
            if (!int.TryParse(HttpContext.Request.Query["page"], out var page)) page = 1;
            if (!int.TryParse(HttpContext.Request.Query["pageSize"], out var pageSize)) pageSize = 10;

            var logsQuery = DbContext.ReportSharpRequests
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.ReportSharpResponse.StatusCode != "500")
                .Select(x => new {
                    RequestId = x.Id,
                    Path = x.Url,
                    ResponseStatusCode = x.ReportSharpResponse.StatusCode,
                    ResponseBody = x.ReportSharpResponse.Content
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

            if (!await DbContext.ReportSharpRequests.AnyAsync(x =>
                x.Id == id && x.ReportSharpResponse.StatusCode != "500"
            ))
                return NotFound();

            var request = await DbContext.ReportSharpRequests
                .AsNoTracking()
                .Include(x => x.ReportSharpResponse)
                .Where(x => x.ReportSharpResponse.StatusCode != "500")
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(request);
        }
    }
}