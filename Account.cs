using System;
using System.IO;
using ConsoleTables;

namespace Сoursework
{
	class Account : Supplies//Закупка
	{
		int totalsum;

		public int TotalSum
		{
			get
			{
				return (totalsum);
			}
			set
			{
				totalsum = value;
			}
		}
		public Account(string filepathsuppliers, string filepathdetails) : base(filepathsuppliers, filepathdetails)
		{
			
		}
		

		public void printLinewithColor(string str, string message)
		{
			Console.Write(message);
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine(str);
			Console.ForegroundColor = ConsoleColor.White;
		}
		public void PrintAccount(string accountfile)
		{
			StreamReader sr = new StreamReader(accountfile);
			var table = new ConsoleTable("Деталь", "Количество", "Поставщик", "Цена", "Дата поставки", 
				"Итого по заказу");
			while (!(sr.EndOfStream))
			{
				string print = sr.ReadLine();
				string[] arrline = print.Split('\t');
				// Console.ForegroundColor = ConsoleColor.DarkCyan;
				// Console.WriteLine(arrline[1]);
				// Console.ForegroundColor = ConsoleColor.White;
				
				// printLinewithColor(arrline[2],"Был заказан в количестве ");
				// printLinewithColor(arrline[0],"У поставщика ");
				// printLinewithColor(arrline[4],"По цене ");
				// printLinewithColor(arrline[3],"Поставка на ");

				int currentprice, count;
				Int32.TryParse(arrline[4], out currentprice);
				Int32.TryParse(arrline[2], out count);
				int sum = currentprice * count;
				//printLinewithColor(Convert.ToString(sum), "Итого по заказу: ");
				table.AddRow(arrline[1], arrline[2], arrline[0], arrline[4], arrline[3], Convert.ToString(sum));
				Console.WriteLine();
				TotalSum += sum;
			}
			table.Write(Format.Default);
			sr.Close();
			string totalsum = Convert.ToString(TotalSum);
			TotalSum = 0;
			printLinewithColor(totalsum,"Итого: ");
		}
	}
}