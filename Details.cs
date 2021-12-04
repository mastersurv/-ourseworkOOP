using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Сoursework
{
    class Details : Suppliers
    {
        string name_detail;
        private int article; //артикул (англ. от изделие/вещь)
        private int price;
        string remark; //примечание
        
        public Details(){}

        public Details(string name_detail, int article, int price, string remark)
        {
            this.name_detail = name_detail;
            this.article = article;
            this.price = price;
            this.remark = remark;
        }
        
        
        
        public void ReadDetailFromFileAndShow(string path)
        {
            StreamReader sr = new StreamReader(path);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n----------------------------Детали-----------------------------\n");
            while (true)
            {
                string detail_info = sr.ReadLine();
                if (detail_info == null)
                {
                    break;
                }
                string[] arrdetailinfo = detail_info.Split('\t');
                name_detail = arrdetailinfo[0];
                article = Convert.ToInt32(arrdetailinfo[1]);
                price = Convert.ToInt32(arrdetailinfo[2]);
                remark = arrdetailinfo[3];
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Деталь      {name_detail}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Артикул     {article}");
                Console.WriteLine($"Цена        {price}");
                Console.WriteLine($"Примечание  {remark}");
                Console.WriteLine();
                
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('-', 63));
            Console.ForegroundColor = ConsoleColor.White;
            sr.Close();
        }
    }
}
