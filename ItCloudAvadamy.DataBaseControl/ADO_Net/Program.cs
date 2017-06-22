using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library(@"Data Source = ЯЗЬ-ПК; Initial Catalog = Library; Integrated Security = True");

            Console.ReadKey();
        }
    }
}
