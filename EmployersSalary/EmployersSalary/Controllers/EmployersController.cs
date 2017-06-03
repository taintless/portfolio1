using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EmployersSalary.Models;
using Microsoft.AspNet.Identity;
using EmployersSalary.Business;

namespace EmployersSalary.Controllers
{
    [System.Web.Mvc.Authorize]
    public class EmployersController : Controller
    {
        private readonly EmployersBusiness _employersBusiness;
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUserBusiness _applicationUser;

        public EmployersController()
        {
            _context = new ApplicationDbContext();
            _employersBusiness = new EmployersBusiness(_context);
            _applicationUser = new ApplicationUserBusiness(_context);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index()
        {
            
            if (User.IsInRole(RoleName.Admin))
                return View("List");
            if (User.IsInRole(RoleName.ProjectManager))
                return View("ListReadOnly");

            var loggedUserId = User.Identity.GetUserId();
            var user = _applicationUser.GetUser(loggedUserId);
            var employer = _employersBusiness.GetEmployer(user.Employer.FirstName, user.Employer.LastName);

            if (employer == null)
                throw new System.Exception("Employer doesn't exist.");

            return View("ListEmployer", employer);
        }


        [System.Web.Mvc.Authorize(Roles = RoleName.Admin)]
        [System.Web.Mvc.HttpPost]
        public ActionResult Save(EmployerViewModel employer)
        {
            if (!ModelState.IsValid)
                return View("EmployerForm", employer);

            var employerInDb = _employersBusiness.GetEmployer(employer.FirstName, employer.LastName);

            if (employerInDb == null)
                throw new System.Exception("Employer doesn't exist.");

            if (employer.NetSalary.HasValue)
                employerInDb.UpdateSalary(employer.NetSalary ?? default(float));

            _context.SaveChanges();

            return RedirectToAction("Index", "Employers");
        }

        [System.Web.Mvc.Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit([FromUri] string firstName, string lastName)
        {

            var employer = _employersBusiness.GetEmployer(firstName, lastName);

            if (employer == null)
                throw new System.Exception("Employer doesn't exist.");

            var employerViewModel = new EmployerViewModel
            {
                FirstName = employer.FirstName,
                LastName = employer.LastName,
                NetSalary = employer.NetSalary
            };

            return View("EmployerForm", employerViewModel);
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null || file.ContentType.StartsWith("image/"))
            {
                var loggedUserId = User.Identity.GetUserId();
                var loggedUser = _context.Users.Include(u => u.Employer).Single(u => u.Id == loggedUserId);
                string pic = System.IO.Path.GetFileName(loggedUser.Employer.FirstName + loggedUser.Employer.LastName+ ".png");
                string path = System.IO.Path.Combine(
                                        Server.MapPath(MyConstants.profileImagesPath), pic);
                file.SaveAs(path);
                
            }
            return RedirectToAction("Index", "Employers");
        }
    }
}