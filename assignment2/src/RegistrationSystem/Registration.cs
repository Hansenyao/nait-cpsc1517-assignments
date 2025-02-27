using System.Reflection;
using System.Text.RegularExpressions;

namespace RegistrationSystem
{
    public class Registration
    {
        public string ProductName{ get; set; }
        public string SerialNumber { get; set; }
        public DateTime DateOfPurchase{ get; set; }
        public string PurchasedFrom{ get; set; }
        public string PurchaseProvince{ get; set; }

        public Registration(string productname, string serialnumber, DateTime dateofpurchase, 
                                string purchasedfrom, string purchaseprovince)
        {
            ProductName = productname;
            SerialNumber = serialnumber;
            DateOfPurchase = dateofpurchase;
            PurchasedFrom = purchasedfrom;
            PurchaseProvince = purchaseprovince;
        }

        public override string ToString()
        {
            return $"{ProductName},{SerialNumber},{DateOfPurchase.ToString("MMM dd yyyy")}," +
                        $"{PurchasedFrom},{PurchaseProvince}";
        }

        public static Registration Parse(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                throw new ArgumentNullException("Record is empty");
            }
            string[] pieces = item.Split(',');
            if (pieces.Length != 5)
            {
                throw new FormatException($"Record format invalid {item}");
            }
            return new Registration(pieces[0],
                                    pieces[1],
                                   DateTime.Parse(pieces[2]),
                                   pieces[3],
                                   pieces[4]);
        }
    }
}
