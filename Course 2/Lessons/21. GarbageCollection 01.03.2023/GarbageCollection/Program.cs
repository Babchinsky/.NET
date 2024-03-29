﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarbageCollection
{
    class UserInfo
    {
        public string Name { set; get; }
        public int Age { set; get; }

        public UserInfo(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("\nИНФОРМАЦИЯ О СБОРКЕ МУСОРА: \n" +
                "---------------------------\n");
            // GC.GetTotalMemory - возвращает информацию о том, какой объем памяти (в байтах) в настоящий момент занят в управляемой куче. 
            // Булевский параметр указывает, должен ли вызов сначала дождаться выполнения сборки мусора, прежде чем возвращать результат
            // GC.MaxGeneration возвращает информацию о том, сколько максимум поколений поддерживается в целевой системе.
            Console.WriteLine("Количество байт в куче: {0}\nМаксимальное количество поддерживаемых поколений объектов: {1}", 
                GC.GetTotalMemory(false), GC.MaxGeneration + 1);

            UserInfo user1 = new UserInfo("User", 20);
            // GC.GetGeneration возвращает информацию о том, к какому поколению в настоящий момент относится объект
            Console.WriteLine("Поколение объекта user1: " + GC.GetGeneration(user1));

            for (int i = 0; i < 50000; i++)
            {
                UserInfo user = new UserInfo("User", 30);
            }

            // Намеренно вызовем сборку мусора в нулевом поколении
            GC.Collect(0, GCCollectionMode.Forced);
            // Позволяет приостанавливать выполнение текущего потока до тех пор, пока не будут финализированы все объекты, 
            // предусматривающие финализацию. Обычно вызывается сразу же после вызова метода GC.Collect()
            GC.WaitForPendingFinalizers();
            Console.WriteLine("\nсборка мусора ...\n");

            Console.WriteLine("Теперь поколение объекта user1: " + GC.GetGeneration(user1));

        }
    }
}
