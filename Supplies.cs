using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ConsoleTables;

namespace Сoursework
{
    class Supplies //Поставки
    {
        Suppliers sup;
        Details det;
        int count;
        string date;
        
        protected List<Suppliers> SuppliersList = new List<Suppliers>();
        protected List<Details> DetailsList = new List<Details>();

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
            int in_warehouse = 0;
            info = new StreamReader(filepathdetails);
			
            while (!(info.EndOfStream))
            {
                string[] arrdetailinfo = info.ReadLine().Split('\t');
                detail = arrdetailinfo[0];
                Int32.TryParse(arrdetailinfo[1], out article);
                Int32.TryParse(arrdetailinfo[2], out price);
                Int32.TryParse(arrdetailinfo[4], out in_warehouse);
                remark = arrdetailinfo[3];
                det = new Details(detail, article, price, remark, in_warehouse);
                DetailsList.Add(det);
            }
            info.Close();
        }
        
        public void ShowDetail(Details det)
        {
            Console.WriteLine($"Деталь      {det.NameDetail}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Артикул     {det.Article}");
            Console.WriteLine($"Цена        {det.Price}");
            Console.WriteLine($"Примечание  {det.Remark}");
            Console.WriteLine($"На складе   {det.InWarehouse}");
            Console.WriteLine();
        }
        public void ShowSuppliersForDetail(string namedetail)
        {
            int indexdetail = SearchDetailByName(namedetail);
            Console.ForegroundColor = ConsoleColor.Cyan;
            ShowDetail(DetailsList[indexdetail]);
            Console.ForegroundColor = ConsoleColor.Green;
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

        public void FindArticleOfDetail(string name, out int article)
        {
            article = DetailsList[SearchDetailByName(name)].Article;
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

        public string SearchDetailByArticle(int article)
        {
            for (int i = 0; i < DetailsList.Count; i++)
            {
                if (DetailsList[i].Article == article)
                {
                    return (DetailsList[i].NameDetail);
                }
            }

            return "";
        }

        public bool CheckDetailAvailability(string name)
        {
            for (int i = 0; i < SuppliersList.Count; i++)
            {
                if (SuppliersList[i].NameDetail == name)
                {
                    return (true);
                }
            }

            return (false);
        }
        public void ShowAllDetails() 
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Список доступных деталей: ");
            Console.ForegroundColor = ConsoleColor.White;
            
            var table = new ConsoleTable("Деталь", "Артикул", "Цена", "На складе");
            for (int i = 0; i < DetailsList.Count; i++)
            {
                table.AddRow(DetailsList[i].NameDetail, DetailsList[i].Article, DetailsList[i].Price, DetailsList[i].InWarehouse);
            }
            table.Write(Format.Alternative);
        }

        static void IncreaseCount(string path_detail, int add_deatils, string namedetail)
        {
            StreamReader sr = new StreamReader(path_detail);
            string need_replace = null;
            string new_line = null;

            while (!(sr.EndOfStream))
            {
                string line = sr.ReadLine();
                string[] arrinfo = line.Split('\t');

                if (arrinfo[0] == namedetail)
                {
                    sr.Close();
                    need_replace = line;
                    int currentcount = Convert.ToInt32(arrinfo[4]);
                    currentcount += add_deatils;
                    new_line = $"{arrinfo[0]}\t{arrinfo[1]}\t{arrinfo[2]}\t{arrinfo[3]}\t{Convert.ToString(currentcount)}";
                    break;
                }
            }
            sr.Close();
            var fileContents = System.IO.File.ReadAllText(path_detail);
            fileContents = fileContents.Replace(need_replace, new_line);
            System.IO.File.WriteAllText(path_detail, fileContents);
        }
        
        public void BuyDetail(int article, string filepath, string path_detail)
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

                        bool flag = false;
                        for (int j = 0; j < SuppliersList.Count; j++)
                        {
                            if (SuppliersList[j].NameDetail == purchasedDetails.NameDetail)
                            {
                                if (j == number)
                                    flag = true;
                            }
                        }

                        if (!flag)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nНеправильно введён номер поставщика.");
                            Console.ForegroundColor = ConsoleColor.White;
                            return;
                        }
                        Console.Write("Сколько деталей желаете приобрести?: ");
                        int detcount = Convert.ToInt32(Console.ReadLine());
                        Count = detcount;
                        IncreaseCount(path_detail, Count, DetailsList[i].NameDetail);
                        purchasedDetails.InWarehouse += Count;
                        
                        AddSupplieToFile(purchasedDetails, number, filepath);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
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

            Console.WriteLine("Введите дату, когда нужно доставить деталь: ");
            Console.Write("Год (2021): ");
            int year = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Месяц (1-12): ");
            int month = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("День (1-31): ");
            int day = Convert.ToInt32(Console.ReadLine());

            try
            {
                DateTime inputdate = new DateTime(year, month, day);
                DateTime today = DateTime.Today;
                sw.Write(inputdate.ToLongDateString() + '\t');
                if (inputdate < today)
                {
                    throw new Exception("Дата не может быть раньше текущей!");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nЗапчасти были успешно заказаны!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ошибка: {e.Message}"); 
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            sw.Write(det.Price);
            
            sw.Close();
        }
    }
}
