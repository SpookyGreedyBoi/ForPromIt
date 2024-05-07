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
					Assert.AreEqual(20, receipt.GetReceiptInfo().Length, "{0} : ������ �������", receipt.GetName());
					Assert.IsTrue(IsDateValide(receipt.GetReceiptInfo()[2]), "{0} : ������������ ����", receipt.GetName());
					Assert.IsTrue(IsFromValide(receipt.GetReceiptInfo()[3]), "{0} : ������������ ����� �����������", receipt.GetName());
					Assert.IsTrue(IsToValide(receipt.GetReceiptInfo()[4]), "{0} : ������������ ����� ����������", receipt.GetName());
					Assert.IsTrue(IsNumberOfTicketValide(receipt.GetReceiptInfo()[5]), "{0} : ������������ �����/� ������", receipt.GetName());
					Assert.IsTrue(Perevozka(receipt.GetReceiptInfo()[6]), "{0} : ������ � ������ ���������", receipt.GetName());
					Assert.IsTrue(IsCostValide(receipt.GetReceiptInfo()[7]), "{0} : ������������ ��������� �� ������", receipt.GetName());
					Assert.IsTrue(IsTotalCostValide(receipt.GetReceiptInfo()[8]), "{ 0} : ������������ �������� ���������", receipt.GetName());
				}
			});
		}


		public bool IsDateValide(string s)
		{
			DateTime temp;

			if (s.Substring(0, 2) != "��") return false;

			if (!DateTime.TryParse(s.Replace(" ", "").Substring(2,10), out temp)) return false;

			return true;
		}

		public bool IsFromValide(string s)
		{
			if (s.Length < 5) return false;
			if (s.Substring(0, 2) != "��") return false;
			return true;
		}

		public bool IsToValide(string s)
		{
			if (s.Length < 5) return false;
			if (s.Substring(0, 2) != "��") return false;
			return true;
		}

		public bool IsNumberOfTicketValide(string s)
		{
			var temp = s.Split().Where(x => x.Length > 1).ToArray();
			int temp1;

			if (temp.Length != 5) return false;
			if (temp[0] != "�����" || temp[1] != "N:" || temp[3] != "����.N:") return false;
			if (temp[2].Length != 5 || !int.TryParse(temp[2], out temp1)) return false;
			if (temp[4].Length != 13 || !int.TryParse(temp[4], out temp1)) return false;
			return true;
		}

		public bool Perevozka(string s) //idk
		{
			var temp = s.Split().Where(x => x.Length > 1).ToArray(); ;

			if (temp.Length < 2) return false;
			if (temp[0] != "���������" || temp.Length < 2) return false;
			return true;
		}

		public bool IsCostValide(string s)
		{
			var temp = s.Split().Where(x => x.Length > 1).ToArray();
			double temp1;

			if (temp.Length != 4) return false;
			if (temp[0] != "���������" || temp[1] != "��" || temp[2] != "������:") return false;
			if (temp[3][0] != '=') return false;
			if (!double.TryParse(temp[3].Substring(1), out temp1));
			return true;
		}

		public bool IsTotalCostValide(string s)
		{
			var temp = s.Split().Where(x => x.Length > 1).ToArray();
			double temp1;

			if (temp.Length != 2) return false;
			if (temp[0] != "����:") return false;
			if (!double.TryParse(temp[1], out temp1)) return false;
			return true;
		}
	}
}