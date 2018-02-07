using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linq3HW
{
    class Program
    {

        public static string conStr = @"Data Source = DESKTOP-NESB2OL\SQLEXPRESS; Initial Catalog = Linq1; User Id = Natalya; Password = 12345";
        static void Main(string[] args)
        {
            //Task5();
            //Task6();
            //Task7();
            //Task8();
            //Task9();
            //Task10();
            //Task10Part2();
            Task11();
        }

        public static void Task2(ref List<Area> areas)
        {

            SqlConnection con = new SqlConnection(conStr);
            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select * from Area", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                PropertyInfo[] prop = typeof(Area).GetProperties();

                foreach (DataRow row in dt.Rows)
                {
                    int i = 0;
                    Area temp = new Area();
                    foreach (PropertyInfo item in prop)
                    {

                        if (row.ItemArray[i].GetType() == typeof(System.DBNull))
                        {
                            item.SetValue(temp, null);
                        }
                        else if (row.ItemArray[i].GetType() == typeof(System.Boolean))
                        {
                            if ((Boolean)row.ItemArray[i])
                            {
                                item.SetValue(temp, 1);


                            }
                            else
                            {
                                item.SetValue(temp, 0);
                            }

                        }
                        else
                        {
                            item.SetValue(temp, row.ItemArray[i]);
                        }
                        i++;
                    }

                    areas.Add(temp);
                }
                //foreach (Area item in areas)
                //{
                //    Console.WriteLine(item.FullName);
                //    Console.WriteLine();
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Array Task3()
        {
            List<Area> areas = new List<Area>();
            Task2(ref areas);
            return areas.ToArray();
        }

        public static List<Area> Task4()
        {
            List<Area> areas = new List<Area>();
            Task2(ref areas);
            return areas;
        }

        public static void Task5()
        {
            Console.WriteLine("Введите TypeArea");
            int t = Int32.Parse(Console.ReadLine());
            List<Area> areas = Task4();
            var res = areas.Where(w => w.TypeArea == t &&!string.IsNullOrEmpty(w.IP)).Select(s=>new
            {
                s.AreaId,
                s.IP
            });

            foreach (var item in res)
            {
                Console.WriteLine("ID - "+item.AreaId+" IP - "+item.IP);
                Console.WriteLine();
            }
        }

        public static void Task6()
        {
            Console.WriteLine("Введите TypeArea");
            int t = Int32.Parse(Console.ReadLine());
            List<Area> areas = Task4();
            var res = areas.Where(w => w.TypeArea == t && !string.IsNullOrEmpty(w.IP)&&w.ParentId!=0).Select(s => new
            {
                s.IP
            });

            foreach (var item in res)
            {
                Console.WriteLine("IP - " + item.IP);
                Console.WriteLine();
            }
        }

        public static ILookup<string,Area> Task7()
        {
            List<Area> areas = Task4();
            ILookup<string, Area> lareas = areas.ToLookup(k=>k.IP);

            Console.WriteLine("Введите IP адрес");
            string ip = Console.ReadLine();

            IEnumerable<Area> ars = lareas[ip];

            foreach (Area item in ars)
            {
                Console.WriteLine("AreaId - {0}, Name = {1}",item.AreaId,item.FullName);
            }

            return lareas;
            
        }

        public static void Task8()
        {
            List<Area> areas = Task4();
            Area a = areas.FirstOrDefault(f => f.HiddenArea == 1);
            Console.WriteLine(a.AreaId + " "+a.FullName+" "+a.HiddenArea);
        }

        public static void Task9()
        {
            List<Area> areas = Task4();
            Area a = areas.LastOrDefault(f => f.PavilionId == 1);
            Console.WriteLine(a.AreaId + " " + a.FullName + " " + a.PavilionId);
        }

        public static void Task10()
        {
            List<Area> areas = Task4();
            string[] ips = new string[] { "10.53.34.85", "10.53.34.77", "10.53.34.53" };
            var res = areas.Where(w => w.PavilionId == 1 && ips.Contains(w.IP));
            foreach (Area item in res.ToList())
            {
                Console.WriteLine(item.FullName+" "+item.IP+" "+item.PavilionId);
            }
        }

        public static void Task10Part2()
        {
            List<Area> areas = Task4();
            string[] name = new string[] { "PT disassembly", "Engine testing" };
            var res = areas.Where(w=>name.Contains(w.Name));
            foreach (Area item in res.ToList())
            {
                Console.WriteLine(item.FullName + " " + item.IP + " " + item.PavilionId);
            }
        }

        public static void Task11()
        {
            List<Area> areas = Task4();
            string[] name = new string[] { "PT disassembly", "Engine testing" };
            var res = areas.Sum(s => s.WorkingPeople);
            Console.WriteLine(res);

        }

    }


    public class Area
    {
        public int AreaId { get; set; }
        public int? TypeArea { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? NoSplit { get; set; }
        public int? AssemblyArea { get; set; }
        public string FullName { get; set; }
        public int? MultipleOrders { get; set; }
        public int? HiddenArea { get; set; }
        public string IP { get; set; }
        public int PavilionId { get; set; }
        public int TypeId { get; set; }
        public int? OrderExecution { get; set; }
        public int? Dependence { get; set; }
        public int? WorkingPeople { get; set; }
        public int? ComponentTypeId { get; set; }
        public int? GroupId { get; set; }
        public int? Segment { get; set; }
    }

}

