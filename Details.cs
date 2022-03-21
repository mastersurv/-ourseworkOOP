using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;

namespace Сoursework
{
    class Details : Suppliers
    {
        //string name_detail;
        private int article; //артикул (англ. от изделие/вещь)
        private int price;
        string remark; //примечание
        private int in_warehouse; //количество на складе (купленных)
        
        public Details(){}

        public Details(string name_detail, int article, int price, string remark, int in_warehouse)
        {
            this.name_detail = name_detail;
            this.article = article;
            this.price = price;
            this.remark = remark;
            this.in_warehouse = in_warehouse;
        }
        
        public int Article
        {
            get
            {
                return (article);
            }
        }

        public int Price
        {
            get
            {
                return (price);
            }
        }

        public string Remark
        {
            get
            {
                return (remark);
            }
        }

        public int InWarehouse
        {
            get
            {
                return (in_warehouse);
            }
            set
            {
                in_warehouse = value;
            }
        }

        void PrintTableDetails(string path)
        {
            StreamReader sr = new StreamReader(path);
            var table = new ConsoleTable("Номер", "Деталь", "Цена");
            int i = 0;
            while (true)
            {
                string supinfo = sr.ReadLine();
                if (supinfo == null)
                {
                    break;
                }
                
                string[] arrsupinfo = supinfo.Split('\t');
                table.AddRow(i, arrsupinfo[0], arrsupinfo[2]);
                i++;
            }
            sr.Close();
            table.Write(Format.Alternative);
        }

        public override void DeleteOneEntry(string path)
        {
            PrintTableDetails(path);
            int number = 0;
            try
            {
                Console.Write($"Введите номер удаляемой детали: ");
                number = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.Write($"Удалить {number}-ую деталь? ");
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
                    Console.WriteLine($"\nДеталь {number} была удалена");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введите Да/да/д/Д/y чтобы удалить деталь");
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

        public void ReadDetailFromFileAndShow(string path)
        {
            StreamReader sr = new StreamReader(path);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine('\n' + new string('-', 50) + "Детали" + new string('-', 50) + '\n');
            while (true)
            {
                string detail_info = sr.ReadLine();
                if (detail_info == null)
                {
                    break;
                }
                string[] arrdetailinfo = detail_info.Split('\t');
                name_detail = arrdetailinfo[0];
                Int32.TryParse(arrdetailinfo[1], out article);
                Int32.TryParse(arrdetailinfo[2], out price);
                remark = arrdetailinfo[3];
                in_warehouse = 0;
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Деталь      {name_detail}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Артикул     {article}");
                Console.WriteLine($"Цена        {price}");
                Console.WriteLine($"Примечание  {remark}");
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('-', 110));
            Console.ForegroundColor = ConsoleColor.White;
            sr.Close();
        }
    }
}
