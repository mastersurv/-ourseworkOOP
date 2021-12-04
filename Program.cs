using System;

namespace Сoursework
{
	class Program
	{
		static void Main()
		{
			string path_suppliers = "suppliers.txt";
			string path_details = "details.txt";
			string base_suppliers = "basesuppliers.txt";
			string base_details = "basedetails.txt";
			Suppliers sup = new Suppliers();
			Details detail = new Details();
			
			while (true) 
            {
				int choose;

				Console.WriteLine("Меню");
				Console.WriteLine("Введите 1, чтобы добавить поставщика (вручную):");
				Console.WriteLine("Введите 2, чтобы добавить поставщиков (считать из файла):");
				Console.WriteLine("Введите 3, чтобы вывести информацию о поставщиках");
				Console.WriteLine("Введите 4 для удаления данных о поставщиках");
				Console.WriteLine("Введите 5 для удаления последнего поставщика");
				Console.WriteLine("Введите 6, чтобы вывести информацию о деталях");
				Console.WriteLine("Введите 0 для завершения программы");
				Console.Write("~ ");
				choose = Convert.ToInt32(Console.ReadLine());
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
					sup.AddSupplierFromFile(base_suppliers, path_suppliers);
				}
				else if (choose == 3)
				{
					Console.Clear();
					sup.ReadSupplierFromFileAndShow(path_suppliers);
					//sup.Show();
				}
				else if (choose == 4)
				{
					Console.Clear();
					sup.ClearFileSuppliers(path_suppliers);
				}
				else if (choose == 5)
				{
					Console.Clear();
					sup.ClearLastSupplier(path_suppliers);
				}
				else if (choose == 6)
				{
					detail.ReadDetailFromFile(path_details);
					detail.Show();
				}
            }
		}
	}
}
