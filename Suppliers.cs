using System;
using System.IO;
using System.Collections.Generic;
using ConsoleTables;

namespace Сoursework
{
	class Suppliers //Поставщики
	{
		string supplier;
		protected string name_detail;
		string address;
		string phone_number;
		
		public Suppliers(){}

		public Suppliers(string supplier, string name_detail, string address, string phone_number)
		{
			this.supplier = supplier;
			this.name_detail = name_detail;
			this.address = address;
			this.phone_number = phone_number;
		}

		public string Supplier
		{
			get
			{
				return (supplier);
			}
		}

		public string NameDetail
		{
			get
			{
				return (name_detail);
			}
		}

		public string Address
		{
			get
			{
				return (address);
			}
		}

		public string PhoneNumber
		{
			get
			{
				return (phone_number);
			}
		}
		
		public void ReadSupplierFromFileAndShow(string path)
		{
			StreamReader sr = new StreamReader(path);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine('\n' + new string('-', 50) + "Поставщики" + new string('-', 50) + '\n');
			Console.ForegroundColor = ConsoleColor.White;
			var table = new ConsoleTable("Поставщик", "Деталь", "Адрес", "Номер");
			while (true)
			{
				string supinfo = sr.ReadLine();
				if (supinfo == null)
				{
					break;
				}
				string[] arrsupinfo = supinfo.Split('\t');
				supplier = arrsupinfo[0];
				name_detail = arrsupinfo[1];
				address = arrsupinfo[2];
				phone_number = arrsupinfo[3];
				table.AddRow(supplier, name_detail, address, phone_number);
			}
			table.Write(Format.Alternative);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(new string('-', 110));
			Console.ForegroundColor = ConsoleColor.White;
			sr.Close();
		}

		public static void DeleteLine(string filepath, int number, out bool check_can_delete)
		{
			check_can_delete = true;
			StreamReader sr = new StreamReader(filepath);
			string alllines = sr.ReadToEnd();
			List <string> arrstring = new List<string>(alllines.Split('\n'));
			if (number > arrstring.Count)
            {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Нет такой записи.");
				Console.ForegroundColor = ConsoleColor.White;
				check_can_delete = false;
				return;
            }
			arrstring.RemoveAt(number);
			alllines = String.Join("\n", arrstring.ToArray());
			sr.Close();
			StreamWriter sw = new StreamWriter(filepath);
			sw.Write(alllines);
			sw.Close();
		}

		public static int NumberLastSupplier(string filepath)
		{
			StreamReader sr = new StreamReader(filepath);
			string alllines = sr.ReadToEnd();
			sr.Close();
			
			List <string> arrstring = new List<string>(alllines.Split('\n'));
			int n = arrstring.Count - 1;
			return (n);
		}

		public virtual void DeleteOneEntry(string path)
		{
			Console.Write($"Введите номер удаляемоего поставщика (номер последнего - {NumberLastSupplier(path)}): ");
			int number;
			Int32.TryParse(Console.ReadLine(), out number);
			
			Console.Write($"Удалить {number}-ого поставщика? ");
			string certainty = Console.ReadLine();
			StreamReader sr = new StreamReader(path);
			string checkfile = sr.ReadLine();
			sr.Close();

			if (checkfile != null)
			{
				if (certainty == "Да" || certainty == "да" || certainty == "д" || certainty == "Д" || certainty == "y")
				{
					bool check_can_delete = true;
					DeleteLine(path, number, out check_can_delete);
					if (!check_can_delete)
						return;
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine($"\nПоставщик {number} был удалён");
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Введите Да/да/д/Д/y чтобы удалить поставщика");
					Console.ForegroundColor = ConsoleColor.White;
					return;
				}
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Файл пуст!");
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
		
		public void WriteToFile(string path)
		{
			StreamReader sr = new StreamReader(path);
			string checkfile = sr.ReadLine();
			sr.Close();
			
			StreamWriter sw = new StreamWriter(path, true);
			Console.WriteLine("Введите нового поставщика:");
			Console.WriteLine("Поставщик: ");
			string inputsup = Console.ReadLine();
			if (checkfile == null || checkfile == "\n")
			{
				sw.Write(inputsup + '\t');
			}
			else
			{
				sw.Write('\n' + inputsup + '\t');
			}

			Console.WriteLine("Поставляемые детали: ");
			string inputdetail = Console.ReadLine();
			sw.Write(inputdetail + '\t');

			Console.WriteLine("Адрес: ");
			string inputadres = Console.ReadLine();
			sw.Write(inputadres + '\t');

			Console.WriteLine("Телефон: ");
			string inputphone = Console.ReadLine();
			sw.Write(inputphone + '\t');

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("Поставщик был успешно добавлен.");

			sw.Close();
		}
	}
}