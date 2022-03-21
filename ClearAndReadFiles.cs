using System;
using System.IO;

namespace Сoursework
{
	class ClearAndReadFiles
	{
		public void ClearFile(string path, string message)
		{
			int password;
			Console.Write("Введите пароль: ");
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
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Неверный пароль");
				Console.ForegroundColor = ConsoleColor.White;
				return;
			}
		}
		public void AddInfoFromFile(string basepath, string pathwhere, string message)
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
	}
}