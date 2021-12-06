using System;

namespace Сoursework
{
	class Program
	{
		static void PressAnyKeyToContinue()
		{
			Console.Write("\nНажмите любую клавишу, чтобы продолжить...");
			Console.ReadKey();
			Console.Clear();
		}
		static void Main()
		{
			string path_suppliers = "suppliers.txt";
			string path_details = "details.txt";
			string base_suppliers = "basesuppliers.txt";
			string base_details = "basedetails.txt";
			string path_supplies = "supplies.txt";
			string accountfile = "account.txt";
			Suppliers sup = new Suppliers();
			Details detail = new Details();
			Supplies supplies = new Supplies(path_suppliers, path_details);
			Purchasing purchoice = new Purchasing(path_suppliers, path_details);
			
			while (true) 
            {
	            Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("		╔════════════════════╗");
				Console.WriteLine("		║        Меню        ║");
				Console.WriteLine("		╚════════════════════╝\n");
				Console.ForegroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine("|-----Поставщики-----|");
				Console.WriteLine("Введите 1, чтобы добавить поставщика (вручную):");
				Console.WriteLine("Введите 2, чтобы добавить поставщиков (считать из файла):");
				Console.WriteLine("Введите 3, чтобы вывести информацию о поставщиках");
				Console.WriteLine("Введите 4 для удаления данных о поставщиках");
				Console.WriteLine("Введите 5 для удаления последнего поставщика");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("\n|-----Детали--------|");
				Console.WriteLine("Введите 6, чтобы добавить детали (считать из файла):");
				Console.WriteLine("Введите 7, чтобы вывести информацию о деталях");
				Console.WriteLine("Введите 8 для удаления данных о деталях");
				Console.ForegroundColor = ConsoleColor.DarkGreen;
				Console.WriteLine("\n|-----Закупка автозапчастей--------|");
				Console.WriteLine("Введите 11, чтобы перейти в меню покупки деталей:");
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.WriteLine("\n|-----$Отчёт$--------|");
				Console.WriteLine("Введите 12, чтобы добавить данные о закупках в отчёт:");
				Console.WriteLine("Введите 13, чтобы вывести отчёт о закупке:");
				Console.WriteLine("Введите 14 для удаления данных о поставках");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.Write("\nХ");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write(" Введите 0 для завершения программы");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.Write(" Х\n");
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("~ ");
				int choose;
				bool check = Int32.TryParse(Console.ReadLine(), out choose);
				if (!check)
				{
					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.WriteLine("Нет такого пункта меню");
					Console.ForegroundColor = ConsoleColor.White;
					continue;
				}
				if (choose == 0)
                {
					Console.WriteLine("Программа завершена.");
					break;
                }
				if (choose == 1)
                {
	                Console.Clear();
					sup.WriteToFile(path_suppliers);
                }
				else if (choose == 2)
				{
					Console.Clear();
					sup.AddInfoFromFile(base_suppliers, path_suppliers, "Данные о поставщиках" +
					                                                    " были успешно добавлены");
				}
				else if (choose == 3)
				{
					Console.Clear();
					sup.ReadSupplierFromFileAndShow(path_suppliers);
					PressAnyKeyToContinue();
				}
				else if (choose == 4)
				{
					Console.Clear();
					sup.ClearFile(path_suppliers, "Все данные о поставщиках стёрты.");
				}
				else if (choose == 5)
				{
					Console.Clear();
					sup.ClearLastSupplier(path_suppliers);
				}
				else if (choose == 6)
				{
					Console.Clear();
					detail.AddInfoFromFile(base_details, path_details, "Данные о деталях" +
					                                                   " были успешно добавлены");
				}
				else if (choose == 7)
				{
					Console.Clear();
					detail.ReadDetailFromFileAndShow(path_details);
					PressAnyKeyToContinue();
				}
				else if (choose == 8)
				{
					Console.Clear();
					detail.ClearFile(path_details, "Все данные о деталях стёрты.");
				}
				else if (choose == 11)
				{
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					int purchasechoice;
					Console.WriteLine("Введите 1 если хотите конкретную деталь");
					Console.WriteLine("Введите 2, чтобы увидеть список всех доступных деталей");
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write("~ ");
					purchasechoice = Convert.ToInt32(Console.ReadLine());
					if (purchasechoice == 1)
					{
						Console.Write("Введите название детали, которую хотите приобрести: ");
						string namedetail = Console.ReadLine();
						int article = 0;
						supplies.FindArticleOfDetail(namedetail, out article);
						Console.Clear();
						supplies.BuyDetail(article, path_supplies);
						PressAnyKeyToContinue();
					}
					else if (purchasechoice == 2)
					{
						Console.Clear();
						supplies.ShowAllDetails();
						Console.Write("Введите артикул детали, которую хотите купить: ");
						int article = Convert.ToInt32(Console.ReadLine());
						Console.Clear();
						supplies.BuyDetail(article, path_supplies);
						PressAnyKeyToContinue();
					}
				}
				else if (choose == 12)
				{
					Console.Clear();
					purchoice.CostOfEachSupplie(path_supplies, accountfile);
				}
				else if (choose == 13)
				{
					Console.Clear();
					purchoice.PrintAccount(accountfile);
					PressAnyKeyToContinue();
				}
				else if (choose == 14)
				{
					Console.Clear();
					purchoice.ClearFile(accountfile, "Все данные о поставках стёрты.");
				}
            }
		}
	}
}
