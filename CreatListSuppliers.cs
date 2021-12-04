using System.Collections.Generic;
using System.IO;
using System;

namespace Сoursework
{
	public class CreatListSuppliers
	{
		private List<Suppliers> SuppliersList = new List<Suppliers>();

		public CreatListSuppliers(string filepath)
		{
			string supplier = null;
			string name_detail = null;
			string address = null;
			string phone_number = null;
			StreamReader info = new StreamReader(filepath);

			while (!(info.EndOfStream))
			{
				string[] arrsupinfo = info.ReadLine().Split('\t');
				supplier = arrsupinfo[0];
				name_detail = arrsupinfo[1];
				address = arrsupinfo[2];
				phone_number = arrsupinfo[3];
				
				SuppliersList.Add(new Suppliers(supplier, name_detail, address, phone_number));
			}
		} 
	}
}