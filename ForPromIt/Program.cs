using System;
using System.IO;

namespace forPromIt
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] receiptsPath = Directory.GetFiles(@"C:\Users\user\source\repos\ForPromIt\Receipts\");
            foreach (var receipt in receiptsPath)
            {
                var a = new Receipt(receipt);
                foreach (var item in a.GetReceiptInfo())
                {
                    Console.WriteLine(item);
                }
            }
        }
    }

    public class Receipt
    {
        //private string DirectoryPath { get; set; }
        protected string[] RecieptInfo { get; set; }
        protected string NameInDirectory { get; set; }

		public Receipt(string receiptPath)
        {
            NameInDirectory = receiptPath.Split('\\').Last();
            RecieptInfo = Parse(receiptPath);
        }

        public string GetName() => NameInDirectory;

        public string[] GetReceiptInfo() => RecieptInfo;

        public string[] Parse(string receiptPath)
        {
            return new StreamReader(receiptPath).ReadToEnd().Split("\n");
        }
	}
}
