using Moq;
using EmployersSalary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployersSalary.Business;
using System.Data.Entity;
using EmployersSalary.Services;
using System.Collections.Generic;
using EmployersSalary.Tests.Extensions;
using FluentAssertions;

namespace EmployersSalary.Tests.Business
{
    [TestClass]
    public class EmployersBusinessTests
    {
        private EmployersBusiness _business;
        private Mock<DbSet<Employer>> _mockEmployers;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockEmployers = new Mock<DbSet<Employer>>();

            var mockContext = new Mock<IApplicationDbContext>();

            mockContext.SetupGet(x => x.Employers).Returns(_mockEmployers.Object);

            _business = new EmployersBusiness(mockContext.Object);
        }

        [TestMethod]
        public void GetEmployers_ValidRequest_ShouldReturnEmployersList()
        {
            var employer = new List<Employer> { new Employer() };

            _mockEmployers.SetSource(employer);

            var employers = _business.GetEmployers();

            employers.Should().Contain(employer);
        }

        [TestMethod]
        public void GetEmployer_ValidRequest_ShouldReturnEmployer()
        {
            var employer = new Employer { FirstName = "1", LastName = "2" };

            _mockEmployers.SetSource(new[] { employer });

            var employers = _business.GetEmployer("1", "2");

            employers.Should().Be(employer);
        }
    }
}
