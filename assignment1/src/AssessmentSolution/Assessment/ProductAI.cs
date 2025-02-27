using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assessment
{
    public class ProductAI
    {
        //TODO: Activity 1
        /*
         * Properties are auto implemented.
         * Validation is done in constructor
         */
        #region Properties
        public string Name { get; set; }
        public string ModelNumber { get; set; }
        public int ProductID { get; set; }
        #endregion

        #region Constructors
        public ProductAI(int productID, string name, string modelNumber)
        {
            ProductID = productID;

            // Product name is required, and cannot be blank and must be trimmed
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Product name is required.");
            }
            Name = name.Trim();

            // Model number is required, and cannot be blank and must be trimmed
            if (string.IsNullOrWhiteSpace(modelNumber))
            {
                throw new ArgumentNullException("Model number is required.");
            }
            ModelNumber = modelNumber.Trim();
        }
        #endregion

        #region Methods
        public override string ToString() => $"{ProductID},{Name},{ModelNumber}";
        #endregion
    }
}
