using System;
using System.Collections.Generic;
using System.IO;

namespace Сoursework
{
	class Purchasing : Supplies//Закупка
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
		public Purchasing(string filepathsuppliers, string filepathdetails) : base(filepathsuppliers, filepathdetails)
		{
			
		}

		public void CostOfEachSupplie(string filesupplies, string accountfile)
		{
			string supplier;
			string namedetail;
			int count;
			string date;

			StreamReader info = new StreamReader(filesupplies);
			
			while (!(info.EndOfStream))
			{
				string[] arrsupinfo = info.ReadLine().Split('\t');
				supplier = arrsupinfo[0];
				namedetail = arrsupinfo[1];
				Int32.TryParse(arrsupinfo[2], out count);
				date = arrsupinfo[3];
				
				int indneeddetail = SearchDetailByName(namedetail);
				int price = DetailsList[indneeddetail].Price;
				
				StreamReader sr1 = new StreamReader(accountfile);
				string checkfile = sr1.ReadLine();
				sr1.Close();

				StreamWriter sw = new StreamWriter(accountfile, true);
				if (checkfile == null)
				{
					sw.Write($"{namedetail}	был заказан в количестве	{count}	у поставщика	{supplier}	по цене" +
					         $"	{price}	поставка на	{date}	Итого	{count * price}");
				}
				else
				{
					sw.WriteLine();
					sw.Write($"{namedetail}	был заказан в количестве	{count}	у поставщика	{supplier}	по цене" +
					         $"	{price}	поставка на	{date}	Итого	{count * price}");
				}
				sw.Close();
			}
			info.Close();
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
			while (!(sr.EndOfStream))
			{
				string print = sr.ReadLine();
				string[] arrline = print.Split('\t');
				Console.ForegroundColor = ConsoleColor.DarkCyan;
				Console.WriteLine(arrline[0]);
				Console.ForegroundColor = ConsoleColor.White;
				
				printLinewithColor(arrline[2],"Был заказан в количестве ");
				printLinewithColor(arrline[4],"У поставщика ");
				printLinewithColor(arrline[6],"По цене ");
				printLinewithColor(arrline[8],"Поставка на ");
				Console.WriteLine();

				int currentprice, count;
				Int32.TryParse(arrline[6], out currentprice);
				Int32.TryParse(arrline[2], out count);
				TotalSum += currentprice * count;
			}
			sr.Close();
			string totalsum = Convert.ToString(TotalSum);
			printLinewithColor(totalsum,"Итого: ");
		}
	}
}