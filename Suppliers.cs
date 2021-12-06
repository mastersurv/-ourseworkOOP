using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Сoursework
{
	class Suppliers //Поставщики
	{
		string supplier;
		string name_detail;
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

		public virtual string NameDetail
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
			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.WriteLine("\n--------------------------Поставщики--------------------------\n");
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
				
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Поставщик    {supplier}");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine($"Деталь       {name_detail}");
				Console.WriteLine($"Адрес        {address}");
				Console.WriteLine($"Номер        {phone_number}");
				Console.WriteLine();
			}
			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.WriteLine(new string('-', 63));
			Console.ForegroundColor = ConsoleColor.White;
			sr.Close();
		}

		public void ClearFile(string path, string message)
		{
			int password;
			Console.Write("Введите пароль: ");
			// password = Convert.ToInt32(Console.ReadLine());
			Int32.TryParse(Console.ReadLine(), out password);
			if (password == 2240)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(message);
				Console.ForegroundColor = ConsoleColor.White;
				File.WriteAllText(path, String.Empty);
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine("Неверный пароль");
				Console.ForegroundColor = ConsoleColor.White;
				return;
			}
		}

		public static void DeleteLastLine(string filepath)
		{
			StreamReader sr = new StreamReader(filepath);
			string alllines = sr.ReadToEnd();
			List <string> arrstring = new List<string>(alllines.Split('\n'));
			int n = arrstring.Count - 1;
			arrstring.RemoveAt(n);
			alllines = String.Join("\n", arrstring.ToArray());
			sr.Close();
			StreamWriter sw = new StreamWriter(filepath);
			sw.Write(alllines);
			sw.Close();
		}

		public void ClearLastSupplier(string path)
		{
			Console.Write("Удалить последнего поставщика? ");
			string certainty = Console.ReadLine();
			StreamReader sr = new StreamReader(path);
			string checkfile = sr.ReadLine();
			sr.Close();

			if (checkfile != null)
			{
				if (certainty == "Да" || certainty == "да" || certainty == "д" || certainty == "Д" || certainty == "y")
				{
					DeleteLastLine(path);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Последний поставщик был удалён");
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.WriteLine("Введите Да/да/д/Д/y чтобы удалить последнего поставщика");
					Console.ForegroundColor = ConsoleColor.White;
					return;
				}
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine("Файл пуст!");
				Console.ForegroundColor = ConsoleColor.White;
			}
		}

		public virtual void AddInfoFromFile(string basepath, string pathwhere, string message)
		{
			StreamReader sr = new StreamReader(basepath);
			StreamReader sr1 = new StreamReader(pathwhere);
			string checkfile = sr1.ReadLine();
			sr1.Close();
			
			StreamWriter sw = new StreamWriter(pathwhere, true);
			while (true)
			{
				string readbase = sr.ReadLine();

				if (readbase == null)
				{
					break;
				}
				if (checkfile == null || checkfile == "\n")
				{
					sw.Write(readbase);
				}
				else
				{
					sw.Write('\n' + readbase);
				}
				checkfile = "Файл уже не пуст";
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.White;
			sw.Close();
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

			sw.Close();
		}
	}
}