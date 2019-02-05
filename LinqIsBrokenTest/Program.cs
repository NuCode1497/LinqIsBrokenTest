using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqIsBrokenTest
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var items = new List<Item>
            {
                new Item { Id = 1, Name = "Cub", TimeStamp = DateTime.Today.AddDays(-2) },
                new Item { Id = 1, Name = "Lion", TimeStamp = DateTime.Today },
                new Item { Id = 2, Name = "Puppy", TimeStamp = DateTime.Today.AddDays(-2) }
            };

            var firstItems =
                from e in items
                group e by e.Id into g
                select (from e in g
                        orderby e.TimeStamp
                        select e).First();

            Console.WriteLine("Testing query syntax linq:\n");

            TestLinq(firstItems);

            Console.WriteLine("Testing method syntax linq:\n");

            var firstItems2 = items
                .GroupBy(i => i.Id)
                .Select(g => g.OrderBy(i => i.TimeStamp)).First();

            TestLinq(firstItems2);

            Console.ReadLine();
        }

        private static void TestLinq(IEnumerable<Item> firstItems)
        {
            var firstItemsDict = new Dictionary<int, (string, DateTime)>();
            try
            {
                foreach (var item in firstItems)
                {
                    firstItemsDict.Add(item.Id, (item.Name, item.TimeStamp));
                }
                Console.WriteLine("Linq query works!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Linq query had error...");
                Console.WriteLine(e.Message);
            }
        }
    }
}
