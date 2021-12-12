using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Сoursework
{
    class Details
    {
        string name_detail;
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

        public string NameDetail
        {
            get
            {
                return (name_detail);
            }
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
