using FilesDistributor.Logger;
using System;

namespace FilesDistributor
{
    class Program
    {
        static Distributor _distributor;

        static void Main(string[] args)
        {
            Initialization();
            bool isActive = true;
            while (isActive)
            {
                Console.Write("Enter path to sort out files: ");
                string path = Console.ReadLine();

                _distributor.DistributeFiles(path);
                if (ContinueWorking())
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        static void Initialization()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleLogger cl = new ConsoleLogger();
            cl.OnMessageLogged += delegate { Console.ForegroundColor = ConsoleColor.White; };
            Distributor distributor = new Distributor(cl);
            _distributor = distributor;
        }

        static bool ContinueWorking()
        {
            Console.Write("Continue sorting with another path? [y/n]: ");
            
            try
            {
                char result = Convert.ToChar(Console.ReadLine());
                return char.ToLower(result) == 'y';
            }
            catch(Exception) 
            {
                return false;
            }
        }
    }
}
