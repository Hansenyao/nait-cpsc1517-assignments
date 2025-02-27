using Assessment;
using System.Configuration;
using FluentAssertions;

namespace AssessmentUnitTests
{
    public class ProductFI_Should
    {
        //TODO: Activity 2 Unit Tests
        /*
         * Change ProductID value is greater than 0: success and fail
         * ModelNumber has a format of XX-X-999-X: fail
         */
        #region Change Properties
        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(9999)]
        public void Change_Success_ProductID_With_Positive_Int(int productID)
        {
            // Arrange
            ProductFI actual = new ProductFI(100, "Car", "AB-C-555-D");

            // Act
            actual.ProductID = productID;

            // Assert
            actual.ProductID.Should().Be(productID);
        }

        [Theory]
        [InlineData(-999)]
        [InlineData(-1)]
        [InlineData(0)]
        public void Change_Failed_ProductID_With_NegativeOrZero_Int(int productID)
        {
            // Arrange
            ProductFI actual = new ProductFI(100, "Car", "AB-C-555-D");

            // Act
            Action action = () => actual.ProductID = productID;

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage($"*{productID}*");
        }
        #endregion

        #region Constructors
        [Theory]
        [InlineData("AAB999C")]
        [InlineData("AAA-B-999-C")]
        [InlineData("11-A-888-C")]
        [InlineData("11-2-AAA-3")]
        [InlineData("  -A-666- ")]
        public void Create_Failed_ProductFI_With_Invalid_Format_ModelNumber(string modelNumber)
        {
            // Arrange

            // Act
            Action action = () => new ProductFI(100, "Car", modelNumber);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage($"*{modelNumber}*");
        }
        #endregion
    }
}