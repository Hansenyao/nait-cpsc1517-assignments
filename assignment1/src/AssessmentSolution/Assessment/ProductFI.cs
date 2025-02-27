using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assessment
{
    public class ProductFI
    {
        //TODO: Activity 2
        /*
         * Properties are fully implemented with validation.
         * Validation is in properties
         */
        #region Data members
        private string _Name = string.Empty;
        private string _ModelNumber = string.Empty;
        private int _ProductID;
        #endregion

        #region Properties
        public string Name
        {
            get { return _Name; }
            set
            {
                // Product name is required, and cannot be blank and must be trimmed
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Product name is required.");
                }
                _Name = value.Trim();
            }
        }
        public string ModelNumber
        {
            get { return _ModelNumber; }
            set
            {
                // Rule 1: Model number is required, and cannot be blank and must be trimmed
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Model number is required.");
                }
                // Rule 2: Must have a pattern of xx-x-999-x and must be trimmed.
                // Note: x represents a character (A-Z) and 9 represents a numerical digit (0-9).
                const string REGEX_PATTERN = "^[A-Z]{2}-[A-Z]-\\d{3}-[A-Z]$";
                Regex regex = new Regex(REGEX_PATTERN);
                if (!regex.IsMatch(value.Trim()))
                {
                    throw new ArgumentException($"Model number: {value} is invalid.");
                }
                _ModelNumber = value.Trim();
            }
        }
        public int ProductID 
        { 
            get { return _ProductID; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Product ID: {value} is invalid. It should be greater than 0.");
                }
                _ProductID = value;
            }
        }
        #endregion

        #region Constructors
        public ProductFI(int productID, string name, string modelNumber)
        {
            ProductID = productID;
            Name = name;
            ModelNumber = modelNumber;
        }
        #endregion

        #region Methods
        public override string ToString() => $"{ProductID},{Name},{ModelNumber}";
        #endregion
    }
}
