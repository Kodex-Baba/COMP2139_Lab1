using COMP2139_Lab1.Areas.ProjectManagement.Models;
using COMP2139_Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Lab1.Areas.ProjectManagement.Controllers
{

    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]

    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(int? projectID)
        {
            var taskQuery = _context.ProjectTasks.AsQueryable();

            if(projectID.HasValue)
            {
                taskQuery = taskQuery.Where(t => t.projectID == projectID.Value);
            }

            var tasks = await taskQuery.ToListAsync();
            ViewBag.ProjectID = projectID;

            return View(tasks);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var task = await _context.ProjectTasks.
                Include(t => t.Project).
                FirstOrDefaultAsync(task => task.projectID == id); // probably change task.projectID ==> task.ProjectTaskID

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create(int projectID)
        {
            var project = await _context.Projects.FindAsync(projectID);
            if (project == null)
            {
                return NotFound();
            }

            var task = new ProjectTask()
            {
                projectID = projectID,

            };

            return View(task);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title", "Description", "projectID")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                await _context.ProjectTasks.AddAsync(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { task.projectID });
            }

            var projects = await _context.Projects.ToListAsync();

            ViewBag.Projects = new SelectList(projects, "projectID", "Name", task.projectID);
            return View(task);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.ProjectTasks.
                Include(t => t.Project).
                FirstOrDefaultAsync(t => t.ProjectTaskId == id);

            if (task == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.ToListAsync();
            ViewBag.Projects = new SelectList(project, "projectID", "Name", task.projectID);
            return View(task);

        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId", "Title", "Description", "projectID")] ProjectTask task)
        {
            if (id == task.ProjectTaskId)
            {
                if (ModelState.IsValid)
                {
                    _context.ProjectTasks.Update(task);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { task.projectID });
                }

                var project = await _context.Projects.ToListAsync();

                ViewBag.Projects = new SelectList(project, "projectID", "Name", task.projectID);
                return View(task);
            }

            return NotFound();
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.ProjectTasks.
                Include(t => t.Project).
                FirstOrDefaultAsync(t => t.ProjectTaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.ToListAsync();

            ViewBag.Projects = new SelectList(project, "projectID", "Name", task.projectID);
            return View(task);

        }

        [HttpPost("DeleteConfirmed/{projectTaskID}"), ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int projectTaskID)
        {
            var task = await _context.ProjectTasks.FindAsync(projectTaskID);

            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { task.projectID });
            }

            return NotFound();


        }

        [HttpGet("Search/{searchString?}")]
        public async Task<IActionResult> Search(int? projectID, string searchString)
        {
            var taskQuery = _context.ProjectTasks.AsQueryable();

            // if projectID was passed 
            if (projectID.HasValue)
            {
                taskQuery = taskQuery.Where(t => t.projectID == projectID.Value);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                taskQuery = taskQuery.Where(t => t.Title.Contains(searchString)
                || t.Description.Contains(searchString));
            }

            var tasks = await taskQuery.ToListAsync();
            ViewBag.projectID = projectID;
            return View("Index", tasks);
        }
    }
}
