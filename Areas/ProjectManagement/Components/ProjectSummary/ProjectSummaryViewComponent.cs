using COMP2139_Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Lab1.Areas.ProjectManagement.Components.ProjectSummary
{
    public class ProjectSummaryViewComponent: ViewComponent 
    {
        private readonly ApplicationDbContext _context;

        public ProjectSummaryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int projectID)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(project => project.projectID == projectID);

            if (project == null)
            {
                // Handle the case when the project is not found, return html content
                return Content("Project not found");
            }
            
            return View(project);

        }


    }
}
