using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EmployersSalary.Controllers.Api;
using EmployersSalary.Services;
using EmployersSalary.Models;
using System.Collections.Generic;
using FluentAssertions;
using System.Web.Http.Results;

namespace EmployersSalary.Tests.Controllers.Api
{
    [TestClass]
    public class EmployersControllerTests
    {
        private EmployersController _controller;
        private Mock<IEmployersBusiness> _mockBusiness;
        public EmployersControllerTests()
        {
            _mockBusiness = new Mock<IEmployersBusiness>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Employers).Returns(_mockBusiness.Object);
            _controller = new EmployersController(mockUoW.Object);
        }
        [TestMethod]
        public void GetEmployers_ValidRequest_ShowReturnEmployersList()
        {
            var employers = new List<Employer> { new Employer() };

            _mockBusiness.Setup(x => x.GetEmployers()).Returns(employers);

            var result = _controller.GetEmployers();

            result.Should().BeOfType<OkNegotiatedContentResult<List<Employer>>>();
        }

        [TestMethod]
        public void GetEmployers_NoEmployers_ShowReturnNotFound()
        {
            var result = _controller.GetEmployers();

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void GetEmployer_ValidRequest_ShouldReturnEmployer()
        {
            var employer = new Employer { FirstName = "1", LastName = "2" };

            _mockBusiness.Setup(x => x.GetEmployer("1", "2")).Returns(employer);

            var result = _controller.GetEmployer("1", "2");

            result.Should().BeOfType<OkNegotiatedContentResult<Employer>>();
        }

        [TestMethod]
        public void GetEmployer_EmployerIsDisabled_ShouldReturnNotFound()
        {
            var employer = new Employer { FirstName = "1", LastName = "2" };
            employer.Disable();

            _mockBusiness.Setup(x => x.GetEmployer("1", "2")).Returns(employer);

            var result = _controller.GetEmployer("1", "2");

            result.Should().BeOfType<NotFoundResult>();
        }
        [TestMethod]
        public void GetEmployer_NoEmployerWithGivenNames_ShouldReturnNotFound()
        {

            var result = _controller.GetEmployer("1", "2");

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void UpdateEmployerSalary_ValidRequest_ShouldReturnOk()
        {
            var employer = new Employer();

            _mockBusiness.Setup(x => x.GetEmployer("1", "2")).Returns(employer);

            var employerToUpdate = new Employer { NetSalary = 1 };

            var result = _controller.UpdateEmployerSalary("1", "2", employerToUpdate);

            employer.NetSalary.Should().Be(employerToUpdate.NetSalary);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void UpdateEmployerSalary_NoSalaryToUpdate_ShouldReturnBadRequest()
        {
            var employer = new Employer();

            _mockBusiness.Setup(x => x.GetEmployer("1", "2")).Returns(employer);

            var employerToUpdate = new Employer();

            var result = _controller.UpdateEmployerSalary("1", "2", employerToUpdate);

            result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public void UpdateEmployerSalary_EmployersNotExistWithGivenNames_ShouldReturnNotFound()
        {
            var employerToUpdate = new Employer();

            var result = _controller.UpdateEmployerSalary("1", "2", employerToUpdate);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteEmployer_ValidRequest_ShouldReturnOK()
        {
            var employer = new Employer();

            _mockBusiness.Setup(x => x.GetEmployer("1", "2")).Returns(employer);

            var result = _controller.DeleteEmployer("1", "2");

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteEmployer_AlreadyCanceledEmployer_ShouldReturnBadRequest()
        {
            var employer = new Employer();

            employer.Disable();

            _mockBusiness.Setup(x => x.GetEmployer("1", "2")).Returns(employer);

            var result = _controller.DeleteEmployer("1", "2");

            result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public void DeleteEmployer_EmployerNotExistWithGivenNames_ShouldReturnNotFound()
        {
            var result = _controller.DeleteEmployer("1", "2");

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
