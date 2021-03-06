﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace ServerApp
{

    class Program
    {
        private static System.Timers.Timer interruptGenerator;

        public static List<outputForm> objListOut = new List<outputForm>();

        private static AdamCNT AdamComponent;

        static void Main(string[] args)
        {
            List<config> objListIn = new List<config>();

            using (var reader = new StreamReader("input.txt"))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<config>();
                objListIn = records.ToList();
            }
            int time = objListIn[0].Id;

            AdamComponent = new AdamCNT();


            AdamComponent.createCounterSocket();
            //AdamComponent.createSwitchSocket();
            AdamComponent.createButtonSocket();
            AdamComponent.counterStart();
           

            setTimer(time);
            Console.Write("Press ESC to exit...\n");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                AdamComponent.resetCounter();
                Environment.Exit(0);
            }
        }

        private static void setTimer(int time) {
            interruptGenerator = new System.Timers.Timer(time * 1000);
            interruptGenerator.Elapsed += OnSignal;
            interruptGenerator.AutoReset = true;
            interruptGenerator.Enabled = true;
        }
        private static void OnSignal(Object source, ElapsedEventArgs e)
        {
            AdamComponent.buttonRead();
            AdamComponent.counterRead();
            Console.WriteLine("Entered timer... ");
            objListOut.Add(new outputForm(AdamComponent.getCnt(), "OFF")); //napravi geter za cnt
            using (var writer = new StreamWriter("output2.csv"))          
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(objListOut);
            }
        }
    }
}