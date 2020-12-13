using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    using StructureMap;
    using StructureMap.Attributes;

    static public class Main
    {
        static Main()
        {
            try
            {
                Load();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("ERROR");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("The following exception was caught: " + Environment.NewLine + ex);
            }
        }

        // dummy to call the constructor
        static public void Start()
        {
        }

        static public void Load()
        {
        }

        static public void Unload()
        {
        }
    }
}
