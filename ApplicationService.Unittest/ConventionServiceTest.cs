using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace ApplicationService.Unittest
{
    [TestClass]
    public class ConventionServiceTest
    {
        private Mock<IConventionsRepository> conventionRepositoryMock;
        private Mock<IUserManagementRepository> userManagementRepositoryMock;
        public ConventionServiceTest()
        {
            conventionRepositoryMock = new Mock<IConventionsRepository>();
        }
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_PassNullConventionRepository_ThrowsException()
        {
            var conventionService = new ConventionService(null, userManagementRepositoryMock.Object);
        }
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_PassNullUserManagementRepository_ThrowsException()
        {
            var conventionService = new ConventionService(conventionRepositoryMock.Object, null);
        }

        public void GetConventionRelatedData_VerifyDependency_Pass()
        {
            userManagementRepositoryMock.Setup(x => x.GetSpeakers()).Returns(Task.FromResult(new List<Speaker>().AsEnumerable()));
            conventionRepositoryMock.Setup(x => x.GetMarvelCharacters()).Returns(Task.FromResult(new List<Character>
                {new Character {Description = "This is an description", Name = "Thor"}}.AsEnumerable()));
            var target = CreateTarget();
            var result = target.GetConventionRelatedData();

            userManagementRepositoryMock.Verify(x=> x.GetSpeakers(), Times.Once);

            conventionRepositoryMock.Verify(x=> x.GetMarvelCharacters(), Times.Once);
            
        }
        private ConventionService CreateTarget()
        {
            return new ConventionService(conventionRepositoryMock.Object, userManagementRepositoryMock.Object);
        }
    }
}
