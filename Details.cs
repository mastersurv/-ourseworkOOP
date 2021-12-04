using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Сoursework
{
    class Details : Suppliers
    {
        List<string> name_detail = new List<string>();
        protected List<int> article = new List<int>(); //артикул (англ. от изделие/вещь)
        List<int> price = new List<int>();
        List<string> remark = new List<string>(); //примечание
        private int n; //Количество деталей

        public void ReadDetailFromFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            int i = 0;
            while (true)
            {
                string detail_info = sr.ReadLine();
                if (detail_info == null)
                {
                    break;
                }
                string[] arrdetailinfo = detail_info.Split('\t');
                name_detail.Add(arrdetailinfo[0]);
                article.Add(Convert.ToInt32(arrdetailinfo[1]));
                price.Add(Convert.ToInt32(arrdetailinfo[2]));
                remark.Add(arrdetailinfo[3]);
                i++;
            }
            sr.Close();
            n = i;
        }
        
        public override void WriteToFile(string path)
        {
            StreamWriter sw = new StreamWriter(path, true);
            Console.WriteLine("Добавьте новую деталь:");
            Console.WriteLine("Название: ");
            string name = Console.ReadLine();
            sw.Write(name + '\t');

            Console.WriteLine("Поставляемые детали: ");
            string inputdetail = Console.ReadLine();
            sw.Write(inputdetail + '\t');

            Console.WriteLine("Адрес: ");
            string inputadres = Console.ReadLine();
            sw.Write(inputadres + '\t');

            Console.WriteLine("Телефон: ");
            string inputphone = Console.ReadLine();
            sw.Write(inputphone + '\t');

            sw.Close();
        }
        public  void Show()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--------------------------Детали--------------------------\n");
            for (int i = 0; i < n; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Деталь      {name_detail[i]}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Артикул     {article[i]}");
                Console.WriteLine($"Цена        {price[i]}");
                Console.WriteLine($"Примечание  {remark[i]}");
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('-', 63));
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
