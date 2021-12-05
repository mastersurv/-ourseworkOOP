using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Сoursework
{
    class Supplies //Поставки
    {
        Suppliers sup;
        Details det;
        int count;
        string date;
        
        private List<Suppliers> SuppliersList = new List<Suppliers>();
        private List<Details> DetailsList = new List<Details>();
		
        public Supplies(string filepathsuppliers, string filepathdetails)
        {
            string supplier = null;
            string name_detail = null;
            string address = null;
            string phone_number = null;
            StreamReader info = new StreamReader(filepathsuppliers);

            while (!(info.EndOfStream))
            {
                string[] arrsupinfo = info.ReadLine().Split('\t');
                supplier = arrsupinfo[0];
                name_detail = arrsupinfo[1];
                address = arrsupinfo[2];
                phone_number = arrsupinfo[3];
                sup = new Suppliers(supplier, name_detail, address, phone_number);
                SuppliersList.Add(sup);
            }
            info.Close();
			
            string detail = null;
            int article = 0; 
            int price = 0;
            string remark = null;
            info = new StreamReader(filepathdetails);
			
            while (!(info.EndOfStream))
            {
                string[] arrdetailinfo = info.ReadLine().Split('\t');
                detail = arrdetailinfo[0];
                article = Convert.ToInt32(arrdetailinfo[1]);
                price = Convert.ToInt32(arrdetailinfo[2]);
                remark = arrdetailinfo[3];
                det = new Details(detail, article, price, remark);
                DetailsList.Add(det);
            }
            info.Close();
        }
        
        public void ShowSuppliersForDetail(string namedetail)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{namedetail} возможно купить у следующих поставщиков:\n");
            for (int i = 0; i < SuppliersList.Count; i++)
            {
                if (SuppliersList[i].NameDetail == namedetail)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Поставщик    {SuppliersList[i].Supplier}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Адрес        {SuppliersList[i].Address}");
                    Console.WriteLine($"Номер        {SuppliersList[i].PhoneNumber}");
                    Console.WriteLine();
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ShowAllDetails()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Список доступных деталей: ");
            for (int i = 0; i < DetailsList.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Деталь      {DetailsList[i].NameDetail}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Артикул     {DetailsList[i].Article}");
                Console.WriteLine($"Цена        {DetailsList[i].Price}");
                Console.WriteLine($"Примечание  {DetailsList[i].Remark}");
                Console.WriteLine();
            }
        }
    }
}
