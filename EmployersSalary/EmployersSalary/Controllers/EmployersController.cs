using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EmployersSalary.Models;
using Microsoft.AspNet.Identity;
using EmployersSalary.Business;
using EmployersSalary.Services;

namespace EmployersSalary.Controllers
{
    [System.Web.Mvc.Authorize]
    public class EmployersController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public EmployersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ViewResult Index()
        {
            
            if (User.IsInRole(RoleName.Admin))
                return View("List");
            if (User.IsInRole(RoleName.ProjectManager))
                return View("ListReadOnly");

            var loggedUserId = User.Identity.GetUserId();
            var user = _unitOfWork.Users.GetUser(loggedUserId);
            var employer = _unitOfWork.Employers.GetEmployer(user.Employer.Id);

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

            var employerInDb = _unitOfWork.Employers.GetEmployer(employer.Id);

            if (employerInDb == null)
                throw new System.Exception("Employer doesn't exist.");

            if (employer.NetSalary.HasValue)
                employerInDb.UpdateSalary(employer.NetSalary ?? default(float));

            _unitOfWork.Comlete();

            return RedirectToAction("Index", "Employers");
        }

        [System.Web.Mvc.Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit([FromUri] int id)
        {

            var employer = _unitOfWork.Employers.GetEmployer(id);

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
                var loggedUser = _unitOfWork.Users.GetUser(loggedUserId);
                string pic = System.IO.Path.GetFileName(loggedUser.Employer.Id + ".png");
                string path = System.IO.Path.Combine(
                                        Server.MapPath(MyConstants.profileImagesPath), pic);
                file.SaveAs(path);
                
            }
            return RedirectToAction("Index", "Employers");
        }
    }
}