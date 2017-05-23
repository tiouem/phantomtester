using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reset.Models;

namespace Reset
{
    class Program
    {
        static void Main(string[] args)
        {
            using (PhantomTesterContext db = new PhantomTesterContext())
            {
                try
                {
                    foreach (Token token in db.Tokens)
                    {
                        token.Usages = 0;
                        Console.WriteLine(token.Id + " Token was reset");
                    }
                    db.SaveChanges();
                    Console.WriteLine("----- Changes to the database were saved -----");
                }
                catch (Exception e)
                {
                    Console.WriteLine("////////////////// EXCEPTION trown: " + e.Message);
                }
            }
        }
    }
}
