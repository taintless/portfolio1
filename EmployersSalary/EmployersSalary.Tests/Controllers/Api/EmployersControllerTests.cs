using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EmployersSalary.Controllers.Api;
using EmployersSalary.Services;
using EmployersSalary.Models;
using System.Collections.Generic;
using System.Web.Http;
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
        public void GetEmployers_validRequest_showReturnEmployersList()
        {
            var employers = new List<Employer> { new Employer() };

            _mockBusiness.Setup(x => x.GetEmployers()).Returns(employers);

            var result = _controller.GetEmployers();

            result.Should().BeOfType<OkNegotiatedContentResult<List<Employer>>>();
        }

        [TestMethod]
        public void GetEmployers_noEmployers_showReturnNotFound()
        {
            var result = _controller.GetEmployers();

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
