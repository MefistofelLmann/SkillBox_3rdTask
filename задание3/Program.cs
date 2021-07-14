using System;
using System.Collections.Generic;
using System.Linq;
namespace задание3
{
     public class Pl 
    {
        public int AiL;
        public string name;
        public int pos;
        public int target;
        public ConsoleColor cl;
        public Pl (string n,int p,int t,int AL,ConsoleColor c)
        {
            name = n;
            pos = p;
            target = t;
            AiL = AL;
            cl = c;
        }
    }

    class Program
    {
        public static int posset(int n, ref bool []al)
        {
            if (al[n - 1] == false)
            {
                al[n - 1] = true;
                return n;
            }
            else
            {
                Console.Write("Данное место уже занято, а вот места/место №: ");
                for (int i = 0; i < al.Length; i++)
                    if (al[i] == false) Console.Write((i + 1) + ",");
                Console.WriteLine(" свободное.");
                int nn = Convert.ToInt32(Console.ReadLine());
                return posset(nn, ref al);
            }
        }

        public static ConsoleColor[] cl = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.DarkMagenta, ConsoleColor.DarkYellow, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.DarkBlue, ConsoleColor.DarkGreen, ConsoleColor.DarkRed, ConsoleColor.Cyan, ConsoleColor.Red };
        
        static void Main(string[] args)
        {
            Random r = new Random();
            int cp = 0; int cb = 0;
            do
            {
                Console.WriteLine("Сколько игроков?");
                cp = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Сколько ботов?");
                cb = Convert.ToInt32(Console.ReadLine());
                if (cp==0) Console.WriteLine("Игра рассчитана как минимум на одного ЖИВОГО игрока. Повторите ввод");
                else if (cb + cp < 2) Console.WriteLine("Игра рассчитана на 2+ игроков. Повторите ввод");
            } while ((cb + cp < 2)||(cp==0));
            int adj = 0;
            bool[] pl = new bool[cp + cb];
            List<Pl> All = new List<Pl>();
            for (int i = 1; i <= cp; i++)
            {
                ConsoleColor coltag = cl[r.Next(cl.Length)];
                Console.WriteLine("Привет игрок №" + i + ". Назовись:");
                string n = Console.ReadLine();
                Console.WriteLine("Выбери порядковый номер от 1 до " + (cp + cb));
                int p = Convert.ToInt32(Console.ReadLine());
                p = posset(p, ref pl);
                Console.WriteLine("Введите свой кастомный gameNumber, или напишите 0, чтобы получить рандомный");
                int gameNumber = Convert.ToInt32(Console.ReadLine());
                if (gameNumber == 0)
                {
                    gameNumber = r.Next(12, 121);
                }
                Console.WriteLine("Ваша цель: " + gameNumber);
                adj +=gameNumber;
                All.Add(new Pl(n, p, gameNumber, 0,coltag));
            }
            if (cb != 0)
            {
                Console.WriteLine("Какую сложность дать ботам: 1-легкие, 2-рандомние, 3-сложние");
                int A= Convert.ToInt32(Console.ReadLine());
                char[] alf = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                for (int i = 1; i <= cb; i++)
                {        
                    int p=0;
                    for (int i2 = 0; i2 < pl.Length; i2++)
                        if (pl[i2] == false)
                        {
                            string n = "";
                            for (int a = 0; a < r.Next(4, 8); a++)
                            {
                                n = n + alf[r.Next(alf.Length)];
                            }
                            pl[i2] = true;
                            p = i2+1;
                            int gameNumber = adj / cp;
                            All.Add(new Pl(n, p, gameNumber, A,ConsoleColor.White));
                        } 
                }
            }
            Console.WriteLine("Хотите пропускать ход,Если попытка больше остатка цели (1 - да, любой другой символ - нет) ?");
            bool rude = (Console.ReadLine() == "1") ? true : false;
            var sorted = All.OrderBy(x => x.pos);
            All = sorted.ToList();
            for (int i=0;i<All.Count;i++)
            {
                Console.ForegroundColor = All[i].cl;
                if (Move(All[i].name, All[i].AiL, ref All[i].target, rude) == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Winner is a:" + All[i].name);
                    Console.ResetColor();
                    break;
                }
                else Console.WriteLine("До победи осталось " + All[i].target + " очков");
                if (i == All.Count - 1) i = -1;
            }
        }

        public static int Move (string n,int L,ref int T,bool punch)
        {
            int m=0;
            switch (L)
            {
                case 0:
                    {
                        Console.Write("Ходит игрок " + n + ":");
                        m = Convert.ToInt32(Console.ReadLine());
                        if (punch == false)
                        {
                            while ((m < 1) || (m > 4))
                            {
                                Console.WriteLine("Нарушен диапазон выбор числа, однако вам дан шанс еше раз выбрать");
                                m = Convert.ToInt32(Console.ReadLine());
                            }
                            while (m > T)
                            {
                                Console.WriteLine("Ваш результат будет нарушать цель, однако вам дан шанс еше раз выбрать");
                                m = Convert.ToInt32(Console.ReadLine());
                            }
                        }
                        else
                        {
                            if ((m < 1) || (m > 4))
                            {
                                Console.WriteLine("Нарушен диапазон выбор числа");
                                m = 0;
                            }
                            if (m > T)
                            {
                                Console.WriteLine("Ваш результат будет нарушать цель");
                                m = 0;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("Ходит Бот " + n + ":1");
                        m = 1;
                        break;
                    }
                case 2:
                    {
                        Console.Write("Ходит Бот " + n + ":");
                        Random r = new Random();
                        m = r.Next(1,5);
                        if (T < 5) m = T;
                        Console.WriteLine(m);
                        break;
                    }
                case 3:
                    {
                        Console.Write("Ходит Бот " + n + ":");
                        m = 4;
                        if (T < 5) m = T;
                        Console.WriteLine(m);
                        break;
                    }      
            }
            return T -= m;
        }      
    }
}
