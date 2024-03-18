﻿using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab7_1
{
    public class Sportwoman
    {
        private string _surname;
        private int _group;
        private string _trainerName;
        private int _sec;

        public int Sec
        {
            get { return _sec; }
            set
            {
                if (value > 0)
                    _sec = value;
            }
        }

        public Sportwoman(string surname, int group, string trainerName, int sec)
        {
            _surname = surname;
            _group = group;
            _trainerName = trainerName;
            _sec = sec;
        }
        public void ShowInfo()
        {
            Console.WriteLine($"Фамилия: {_surname}, группа: {_group}, имя тренера: {_trainerName}, результат: {_sec} секунд");
        }

    }
    abstract class Competition
    {
        protected string _competitionName;
        protected Sportwoman[] _sportwomen;
        public Competition(string competitionName, Sportwoman[] sportwomen)
        {
            _competitionName = competitionName;
            _sportwomen = sportwomen;
        }
        private static void quickSort(Sportwoman[] table, int left, int right)
        {
            if (left >= right) return;
            int p = table[(left + right) / 2].Sec;
            int i = left;
            int j = right;
            while (i <= j)
            {
                while (table[i].Sec < p) i++;
                while (table[j].Sec > p) j--;
                if (i <= j)
                {
                    Sportwoman a = table[i];
                    table[i] = table[j];
                    table[j] = a;
                    i++;
                    j--;
                }
            }
            quickSort(table, left, j);
            quickSort(table, i, right);
        }
        public void Sort()
        {
            quickSort(_sportwomen, 0, _sportwomen.Length - 1);
        }
        public virtual void Print()
        {;}
        public virtual void PrintCounter() {; }
        public void HoldCompetition()
        {
            Console.WriteLine($"Название соревнования:{_competitionName}");
            Console.WriteLine();
            Sort();
            Print();
            Console.WriteLine();
            PrintCounter(); 
            Console.WriteLine();
        }
    }
    class Distance100 : Competition
    {
        private static int _norma = 25;
        private static int _counter;
        public Distance100(string competitionName, Sportwoman[] sportwomen) : base(competitionName, sportwomen)
        {

        }
        public override void Print()
        {
            foreach (var sportwoman in _sportwomen)
            {
                if (sportwoman.Sec <= _norma) _counter++;
                sportwoman.ShowInfo();
            }
        }
        public override void PrintCounter() => Console.WriteLine($"Количество спортсменок, сдавших норматив({_norma} секунд): {_counter}");
    }
    class Distance500 : Competition
    {
        private static int _norma = 110;
        private static int _counter;
        public Distance500(string competitionName, Sportwoman[] sportwomen) : base(competitionName, sportwomen)
        {

        }
        public override void Print()
        {
            foreach (var sportwoman in _sportwomen)
            {
                if (sportwoman.Sec <= _norma) _counter++;
                sportwoman.ShowInfo();
            }
        }
        public override void PrintCounter() => Console.WriteLine($"Количество спортсменок, сдавших норматив({_norma} секунд): {_counter}");
    }
    class Program
    {
        static void Main()
        {
            Sportwoman[] sportwomen1 = {
                new Sportwoman("Кукушкина", 1, "Овечкин", 111),
                new Sportwoman("Соловьёва", 1, "Зверев", 107),
                new Sportwoman("Воробьёва", 2, "Козлов", 112),
                new Sportwoman("Синицына", 2, "Зайцев", 109),
                new Sportwoman("Орлова", 1, "Волков", 110)
            };
            Sportwoman[] sportwomen2 = {
                new Sportwoman("Кукушкина", 1, "Овечкин", 25),
                new Sportwoman("Соловьёва", 1, "Зверев", 27),
                new Sportwoman("Воробьёва", 2, "Козлов",24),
                new Sportwoman("Синицына", 2, "Зайцев", 26)
            };
            Distance100 competition1 = new Distance100("Международные соревнования на 100 метров", sportwomen2);
            competition1.HoldCompetition();
            
            Distance500 competition2 = new Distance500("Всероссийские соревнования на 500 метров", sportwomen1);
            competition2.HoldCompetition();

            Console.ReadKey();
        }
    }
}