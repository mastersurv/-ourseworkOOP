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

		static bool ErrorCheckAvailDetail(bool canwebuydetail)
		{
			try
			{
				if (!canwebuydetail)
				{
					throw new Exception("Данную деталь невозможно заказать, у неё нет поставщиков.");
				}
			}
			catch (Exception e)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Ошибка: {e.Message}"); 
				Console.ForegroundColor = ConsoleColor.White;
				return (false);
			}

			return (true);
		}
		static void Main()
		{
			string path_suppliers = "suppliers.txt";
			string path_details = "details.txt";
			string base_suppliers = "basesuppliers.txt";
			string base_details = "basedetails.txt";
			string path_supplies = "supplies.txt";
			Suppliers sup = new Suppliers();
			Details detail = new Details();
			Account purchoice = new Account();
			ClearAndReadFiles carf = new ClearAndReadFiles();
			bool version_account = false;
			
			while (true)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("		╔════════════════════╗");
				Console.WriteLine("		║        Меню        ║");
				Console.WriteLine("		╚════════════════════╝\n");
				
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("|-----Поставщики-----|");
				Console.WriteLine("1 - добавить поставщика (вручную):");
				Console.WriteLine("2 - добавить поставщиков (считать из файла):");
				Console.WriteLine("3 - вывести информацию о поставщиках");
				Console.WriteLine("4 - удалить данные о поставщиках");
				Console.WriteLine("5 - удалить поставщика");
				
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("\n|-----Детали--------|");
				Console.WriteLine("6 - добавить детали (считать из файла):");
				Console.WriteLine("7 - вывести информацию о деталях");
				Console.WriteLine("8 - удалить данных о деталях");
				
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("\n|-----Закупка автозапчастей--------|");
				Console.WriteLine("11 - перейти в меню покупки деталей:");
				
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.WriteLine("\n|-----$Отчёт$--------|");
				Console.WriteLine("12 - вывести отчёт о заказах:");
				Console.WriteLine("13 - удалить данные о заказах");
				
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("\nХ 0 - завершить программу Х\n");
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("~ ");
				int choose;
				bool check = Int32.TryParse(Console.ReadLine(), out choose);
				if (!check)
				{
					Console.ForegroundColor = ConsoleColor.Red;
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
					Console.ForegroundColor = ConsoleColor.White;
					PressAnyKeyToContinue();
                }
				else if (choose == 2)
				{
					Console.Clear();
					carf.AddInfoFromFile(base_suppliers, path_suppliers, "Данные о поставщиках" +
					                                                    " были успешно добавлены");
					PressAnyKeyToContinue();
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
					carf.ClearFile(path_suppliers, "Все данные о поставщиках стёрты.");
					PressAnyKeyToContinue();
				}
				else if (choose == 5)
				{
					Console.Clear();
					sup.ClearLastSupplier(path_suppliers);
					PressAnyKeyToContinue();
				}
				else if (choose == 6)
				{
					Console.Clear();
					carf.AddInfoFromFile(base_details, path_details, "Данные о деталях" +
					                                                   " были успешно добавлены");
					PressAnyKeyToContinue();
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
					carf.ClearFile(path_details, "Все данные о деталях стёрты.");
					PressAnyKeyToContinue();
				}
				else if (choose == 11)
				{
					Supplies supplies = new Supplies(path_suppliers, path_details);
					
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Green;
					int purchasechoice;
					Console.WriteLine("Введите 1 если хотите конкретную деталь");
					Console.WriteLine("Введите 2, чтобы увидеть список всех доступных деталей");
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write("~ ");
					bool checkpur = Int32.TryParse(Console.ReadLine(), out purchasechoice);
					if (!checkpur)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Нет такого пункта меню");
						Console.ForegroundColor = ConsoleColor.White;
						PressAnyKeyToContinue();
						continue;
					}
					if (purchasechoice == 1)
					{
						Console.Write("Введите название детали, которую хотите приобрести: ");
						string namedetail = Console.ReadLine();
						
						bool canwebuydetail = supplies.CheckDetailAvailability(namedetail);
						bool availability = ErrorCheckAvailDetail(canwebuydetail);
						if (!availability)
						{
							PressAnyKeyToContinue();
							continue;
						}
						
						int article = 0;
						supplies.FindArticleOfDetail(namedetail, out article);
						Console.Clear();
						supplies.BuyDetail(article, path_supplies, path_details);
						PressAnyKeyToContinue();
					}
					else if (purchasechoice == 2)
					{
						Console.Clear();
						supplies.ShowAllDetails();
						Console.Write("Введите артикул детали, которую хотите купить: ");
						int article = 0;
						try
						{
							article = Convert.ToInt32(Console.ReadLine());
						}
						catch (Exception e)
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine($"Ошибка: {e.Message}");
							Console.ForegroundColor = ConsoleColor.White;
							PressAnyKeyToContinue();
							continue;
						}
						
						string namedetail = supplies.SearchDetailByArticle(article);
						
						bool canwebuydetail = supplies.CheckDetailAvailability(namedetail);
						bool availability = ErrorCheckAvailDetail(canwebuydetail);
						if (!availability)
						{
							PressAnyKeyToContinue();
							continue;
						}
						
						Console.Clear();
						supplies.BuyDetail(article, path_supplies, path_details);
						PressAnyKeyToContinue();
					}
				}
				else if (choose == 12)
				{
					Console.Clear();
					purchoice.PrintAccount(path_supplies);
					PressAnyKeyToContinue();
				}
				else if (choose == 13)
				{
					Console.Clear();
					carf.ClearFile(path_supplies, "Все данные о заказах стёрты.");
					PressAnyKeyToContinue();
				}
            }
		}
	}
}
