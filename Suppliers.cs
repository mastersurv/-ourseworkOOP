using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Сoursework
{
	class Suppliers //Поставщики
	{
		List<string> supplier = new List<string>();
		List<string> name_detail = new List<string>();
		List<string> address = new List<string>();
		List<string> phone_number = new List<string>();
		private int count_suppliers; //количество поставщиков
		private int lastreadstring;

		public void ReadSupplierFromFile(string path)
		{
			StreamReader sr = new StreamReader(path);
			int c = 0;
			while (true)
			{
				string supinfo = sr.ReadLine();
				if (supinfo == null)
				{
					break;
				}
				string[] arrsupinfo = supinfo.Split('\t');
				supplier.Add(arrsupinfo[0]);
				name_detail.Add(arrsupinfo[1]);
				address.Add(arrsupinfo[2]);
				phone_number.Add(arrsupinfo[3]);
				c++;
			}
			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.WriteLine("\n--------------------------Поставщики--------------------------\n");
			for (int i = 0; i < supplier.Count; i++)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Поставщик    {supplier[i]}");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine($"Деталь       {name_detail[i]}");
				Console.WriteLine($"Адрес        {address[i]}");
				Console.WriteLine($"Номер        {phone_number[i]}");
				Console.WriteLine();
			}
			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.WriteLine(new string('-', 63));
			Console.ForegroundColor = ConsoleColor.White;
			sr.Close();
			count_suppliers = c;
		}

		public void ClearFileSuppliers(string path)
		{
			int password;
			Console.Write("Введите пароль: ");
			password = Convert.ToInt32(Console.ReadLine());
			if (password == 2240)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Все данные о поставщиках были успешно стёрты");
				Console.ForegroundColor = ConsoleColor.White;
				File.WriteAllText(path, String.Empty);
				supplier.Clear();
				name_detail.Clear();
				address.Clear();
				phone_number.Clear();
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine("Неверный пароль");
				Console.ForegroundColor = ConsoleColor.White;
				return;
			}
			
		}

		public virtual void DeleteLastString(string path)
		{
			StreamReader sr = new StreamReader(path);
			string allstrings = sr.ReadToEnd();
			Console.WriteLine('\n' + allstrings + '\n');
			string[] arrstrings = allstrings.Split('\n');
			sr.Close();

			StreamWriter sw = new StreamWriter(path);
			for (int i = 0; i < arrstrings.Length - 1; i++)
			{
				if (i == arrstrings.Length - 2)
				{
					sw.Write(arrstrings[i]);
				}
				else
				{
					sw.WriteLine(arrstrings[i]);
				}
			}
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
					DeleteLastString(path);
					supplier.RemoveAt(supplier.Count - 1);
					name_detail.RemoveAt(name_detail.Count - 1);
					address.RemoveAt(address.Count - 1);
					phone_number.RemoveAt(phone_number.Count - 1);
				}
				else
					return;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine("Файл пуст!");
				Console.ForegroundColor = ConsoleColor.White;
			}
		}

		public void AddSupplierFromFile(string basepath, string pathwhere)
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
					sw.Write(readbase + '\t');
				}
				else
				{
					sw.Write('\n' + readbase + '\t');
				}
				checkfile = "Файл уже не пуст";
			}
			sw.Close();
		}
		public virtual void WriteToFile(string path)
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

			sr.Close();
			sw.Close();
		}
		public virtual void Show()
		{
			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.WriteLine("\n--------------------------Поставщики--------------------------\n");
			for (int i = 0; i < count_suppliers; i++)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Поставщик    {supplier[i]}");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine($"Деталь       {name_detail[i]}");
				Console.WriteLine($"Адрес        {address[i]}");
				Console.WriteLine($"Номер        {phone_number[i]}");
				Console.WriteLine();
			}
			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.WriteLine(new string('-', 63));
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}