using reSmart.Models;
using reSmart.Data;

namespace reSmart.Controllers
{
    [Authorize]
    public class TestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TestsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int courseId)
        {
            var tests = await _context.Tests
                .Include(t => t.Course)
                .Where(t => t.CourseId == courseId)
                .ToListAsync();
            return View(tests);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Create(int courseId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create(int courseId, Test test)
        {
            if (ModelState.IsValid)
            {
                test.CourseId = courseId;
                _context.Tests.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { courseId });
            }
            return View(test);
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateQuestion(int testId)
        {
            ViewBag.TestId = testId;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateQuestion(int testId, Question question)
        {
            if (ModelState.IsValid)
            {
                question.TestId = testId;
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { courseId = (await _context.Tests.FindAsync(testId)).CourseId });
            }
            return View(question);
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> TakeTest(int testId)
        {
            var test = await _context.Tests
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == testId);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> SubmitTest(int testId, int[] selectedAnswers)
        {
            var test = await _context.Tests
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == testId);

            if (test == null)
            {
                return NotFound();
            }

            int score = 0;

            foreach (var question in test.Questions)
            {
                var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);
                if (correctAnswer != null && selectedAnswers.Contains(correctAnswer.Id))
                {
                    score++;
                }
            }

            // Calculate the result and save it if needed

            return View("TestResult", score);
        }
    }
}
