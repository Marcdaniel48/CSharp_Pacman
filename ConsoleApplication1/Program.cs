using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Classes;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {


            string[] lines = File.ReadAllLines("TestLevel.csv");
            int newLine = lines.Length;

            string[,] arr = new string[lines.Length, lines.Length];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < lines.GetLength(0); j++)
                {
                    arr[i, j] = lines[i].Split(',')[j];
                }
            }

            for(int i=0; i < arr.GetLength(0); i++)
            {
                for(int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j]);
                }
                Console.WriteLine();
            }
            
            Console.ReadLine();
            //GameState lel = GameState.Parse("../../levels.csv");


            
            //GameState k = GameState.Parse("../../levels.csv");
        }
    }

}