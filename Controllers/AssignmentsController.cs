using reSmart.Data;
using reSmart.Models;

namespace reSmart.Controllers
{
    [Authorize]
    public class AssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AssignmentsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int courseId)
        {
            var assignments = await _context.Assignments
                .Include(a => a.Course)
                .Include(a => a.Student)
                .Where(a => a.CourseId == courseId)
                .ToListAsync();
            return View(assignments);
        }

        [Authorize(Roles = "Student")]
        public IActionResult Create(int courseId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create(int courseId, Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                assignment.StudentId = user.Id;
                assignment.CourseId = courseId;
                _context.Assignments.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { courseId });
            }
            return View(assignment);
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Grade(int id)
        {
            var assignment = await _context.Assignments
                .Include(a => a.Course)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }
            return View(assignment);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Grade(int id, Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { courseId = assignment.CourseId });
            }
            return View(assignment);
        }
    }
}
