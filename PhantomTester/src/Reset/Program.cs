using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reset.Models;

namespace Reset
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (PhantomTesterContext db = new PhantomTesterContext())
                {
                    foreach (Token token in db.Tokens)
                    {
                        token.Usages = 0;
                        Console.WriteLine(token.Id + " Token usages reset");
                    }
                    db.SaveChanges();
                    Console.WriteLine("----- Changes to the database were saved -----");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("///////////////// EXCEPTION trown: " + e.Message);
            }
        }
    }
}
