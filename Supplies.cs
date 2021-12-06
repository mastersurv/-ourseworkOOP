using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Сoursework
{
    class Supplies : Details //Поставки
    {
        Suppliers sup;
        Details det;
        int count;
        string date;
        
        private List<Suppliers> SuppliersList = new List<Suppliers>();
        private List<Details> DetailsList = new List<Details>();

        public int Count
        {
            get
            {
                return (count);
            }
            set
            {
                count = value;
            }
        }
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
            int indexdetail = SearchDetailByName(namedetail);
            Console.ForegroundColor = ConsoleColor.Blue;
            ShowDetail(DetailsList[indexdetail]);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{namedetail} возможно купить у следующих поставщиков:\n");
            for (int i = 0; i < SuppliersList.Count; i++)
            {
                if (SuppliersList[i].NameDetail == namedetail)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"№{i}");
                    Console.WriteLine($"Поставщик    {SuppliersList[i].Supplier}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Адрес        {SuppliersList[i].Address}");
                    Console.WriteLine($"Номер        {SuppliersList[i].PhoneNumber}");
                    Console.WriteLine();
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public int SearchDetailByName(string name)
        {
            for (int i = 0; i < DetailsList.Count; i++)
            {
                if (DetailsList[i].NameDetail == name)
                {
                    return (i);
                }
            }

            return (-1);
        }
        public void ShowAllDetails() 
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Список доступных деталей: ");
            for (int i = 0; i < DetailsList.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                ShowDetail(DetailsList[i]);
            }
        }

        public void BuyDetail(int article, string filepath)
        {
            Details purchasedDetails;
            for (int i = 0; i < DetailsList.Count; i++)
            {
                if (DetailsList[i].Article == article)
                {
                    purchasedDetails = DetailsList[i];
                    Console.ForegroundColor = ConsoleColor.Blue;
                    ShowSuppliersForDetail(purchasedDetails.NameDetail);
                    try //Проверка на правильность ввода
                    {
                        Console.Write("Введите номер поставщика: ");
                        int number = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Сколько деталей желаете приобрести?: ");
                        int detcount = Convert.ToInt32(Console.ReadLine());
                        Count = detcount;
                        AddSupplieToFile(purchasedDetails, number, filepath);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"Ошибка: {e.Message}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
        
        public void AddSupplieToFile(Details det, int number_supplier, string filepath)
        {
            StreamReader sr = new StreamReader(filepath);
            string checkfile = sr.ReadLine();
            sr.Close();

            StreamWriter sw = new StreamWriter(filepath, true);
            string sup = SuppliersList[number_supplier].Supplier;
            if (checkfile == null || checkfile == "\n")
            {
                sw.Write(sup + '\t');
            }
            else
            {
                sw.Write('\n' + sup + '\t');
            }

            string detail = det.NameDetail;
            sw.Write(detail + '\t');

            string detcount = Convert.ToString(Count);
            sw.Write(detcount + '\t');

            sw.Close();
        }
    }
}
