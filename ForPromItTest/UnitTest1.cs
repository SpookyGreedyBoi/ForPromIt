using forPromIt;

namespace ForPromItTest
{
	public class Tests
	{
		string[] receiptsPaths = Directory.GetFiles(@"C:\Users\user\source\repos\ForPromIt\Receipts\");
		IEnumerable<Receipt> receipts;

		[SetUp]
		public void Setup()
		{
			receipts = receiptsPaths.Select(x => new Receipt(x));
		}


		[Test]
		public void Test2()
		{
			Assert.Multiple(() =>
			{
				foreach (var receipt in receipts)
				{
					Assert.AreEqual(20, receipt.GetReceiptInfo().Length, "{0} : Ошибка формата", receipt.GetName());
					Assert.IsTrue(IsDateValide(receipt.GetReceiptInfo()[2]), "{0} : Некорректная дата", receipt.GetName());
					Assert.IsTrue(IsFromValide(receipt.GetReceiptInfo()[3]), "{0} : Некорректный пункт отправления", receipt.GetName());
					Assert.IsTrue(IsToValide(receipt.GetReceiptInfo()[4]), "{0} : Некорректный пункт назначения", receipt.GetName());
					Assert.IsTrue(IsNumberOfTicketValide(receipt.GetReceiptInfo()[5]), "{0} : Некорректный номер/а билета", receipt.GetName());
					Assert.IsTrue(Perevozka(receipt.GetReceiptInfo()[6]), "{0} : ошибка в строке перевозки", receipt.GetName());
					Assert.IsTrue(IsCostValide(receipt.GetReceiptInfo()[7]), "{0} : Некорректная стоймость по тарифу", receipt.GetName());
					Assert.IsTrue(IsTotalCostValide(receipt.GetReceiptInfo()[8]), "{ 0} : Некорректная итоговая стоймость", receipt.GetName());
				}
			});
		}


		public bool IsDateValide(string s)
		{
			DateTime temp;

			if (s.Substring(0, 2) != "на") return false;

			if (!DateTime.TryParse(s.Replace(" ", "").Substring(2,10), out temp)) return false;

			return true;
		}

		public bool IsFromValide(string s)
		{
			if (s.Length < 5) return false;
			if (s.Substring(0, 2) != "от") return false;
			return true;
		}

		public bool IsToValide(string s)
		{
			if (s.Length < 5) return false;
			if (s.Substring(0, 2) != "до") return false;
			return true;
		}

		public bool IsNumberOfTicketValide(string s)
		{
			var temp = s.Split().Where(x => x.Length > 1).ToArray();
			int temp1;

			if (temp.Length != 5) return false;
			if (temp[0] != "Билет" || temp[1] != "N:" || temp[3] != "Сист.N:") return false;
			if (temp[2].Length != 5 || !int.TryParse(temp[2], out temp1)) return false;
			if (temp[4].Length != 13 || !int.TryParse(temp[4], out temp1)) return false;
			return true;
		}

		public bool Perevozka(string s) //idk
		{
			var temp = s.Split().Where(x => x.Length > 1).ToArray(); ;

			if (temp.Length < 2) return false;
			if (temp[0] != "Перевозка" || temp.Length < 2) return false;
			return true;
		}

		public bool IsCostValide(string s)
		{
			var temp = s.Split().Where(x => x.Length > 1).ToArray();
			double temp1;

			if (temp.Length != 4) return false;
			if (temp[0] != "Стоимость" || temp[1] != "по" || temp[2] != "тарифу:") return false;
			if (temp[3][0] != '=') return false;
			if (!double.TryParse(temp[3].Substring(1), out temp1));
			return true;
		}

		public bool IsTotalCostValide(string s)
		{
			var temp = s.Split().Where(x => x.Length > 1).ToArray();
			double temp1;

			if (temp.Length != 2) return false;
			if (temp[0] != "ИТОГ:") return false;
			if (!double.TryParse(temp[1], out temp1)) return false;
			return true;
		}
	}
}