using NUnit.Framework;
using Moq;
using CreditReferencingConsoleApplication.Interfaces;

namespace CreditReferencingConsoleApplication.Tests
{
    [TestFixture]
    public class AffordabilityServiceTests
    {
        private Mock<IAffordabilityService> _mockAffordabilityService;

        [SetUp]
        public void SetUp()
        {
            _mockAffordabilityService = new Mock<IAffordabilityService>();
        }

        [Test]
        public void CreditReferenceCheck_AffordableProperties_ReturnsCorrectResult()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}