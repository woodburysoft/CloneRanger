﻿using System;

namespace CloneRanger.Examples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var examples = new Examples();
            examples.CloneAnObject(new SimpleClass());

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}