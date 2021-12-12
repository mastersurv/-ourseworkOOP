using System;
using System.IO;
using ConsoleTables;

namespace Сoursework
{
	class Account //Вывод отчёта
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
		// public Account(string filepathsuppliers, string filepathdetails) : base(filepathsuppliers, filepathdetails)
		// {
		// 	
		// }
		
		public void printLinewithColor(string str, string message)
		{
			Console.Write(message);
			Console.ForegroundColor = ConsoleColor.Green;
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

				int currentprice, count;
				Int32.TryParse(arrline[4], out currentprice);
				Int32.TryParse(arrline[2], out count);
				int sum = currentprice * count;
				table.AddRow(arrline[1], arrline[2], arrline[0], arrline[4], arrline[3], Convert.ToString(sum));
				TotalSum += sum;
			}
			Console.WriteLine();
			table.Write(Format.Alternative);
			sr.Close();
			string totalsum = Convert.ToString(TotalSum);
			TotalSum = 0;
			printLinewithColor(totalsum,"Итого: ");
		}
	}
}