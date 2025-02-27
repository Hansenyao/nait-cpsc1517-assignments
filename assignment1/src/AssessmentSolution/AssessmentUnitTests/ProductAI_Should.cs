using Assessment;
using System.Configuration;
using FluentAssertions;

namespace AssessmentUnitTests
{
    public class ProductAI_Should
    {
        //TODO: Activity 1 Unit Tests
        /*
         * Instance was properly created: success
         * ModelNumber has missing data: fail
         */

        #region Constructor
        [Fact]
        public void Create_A_Success_ProductAI()
        {
            // Arrange
            int expectedID = 1021;
            string expectedName = "Car";
            string expectedModelNumber = "AB-C-555-D";

            // Act
            ProductAI actual = new ProductAI(expectedID, expectedName, expectedModelNumber);

            // Assert
            actual.ProductID.Should().Be(expectedID);
            actual.Name.Should().Be(expectedName);
            actual.ModelNumber.Should().Be(expectedModelNumber);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_A_Failed_ProductAI_Without_ModelNumber(string modelNumber)
        {
            // Arrange
            int expectedID = 1021;
            string expectedName = "Car";

            // Act
            Action action = () => new ProductAI(expectedID, expectedName, modelNumber);

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>().WithMessage("*is required*");
        }
        #endregion
    }
}