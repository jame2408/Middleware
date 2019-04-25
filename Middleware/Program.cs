using System;

namespace Middleware
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // 原始程式
            //var result = CallMethod(123); // 沒攔截
            // 攔截 方法一
            //var result = Exec(CallMethod, 123, DateTime.Now); // 攔截 
            // 攔截 方法二
            var result = Middleware()(CallMethod)(123, DateTime.Now); // 攔截

            Console.WriteLine(result);
            Console.ReadLine();
        }

        private static string CallMethod(int item)
        {
            return $"{item}木頭人";
        }

        //private static Func<Func<int, string>, Func<int, DateTime, string>> Middleware()
        //    => next // Func<int, string> --> CallMethod
        //        => (i, time) // (int, DateTime) --> (123, DateTime.Now)
        //            => next.Exec(i, time); // Task

        private static Func<Func<int, string>, Func<int, DateTime, string>> Middleware()
            => next // Func<int, string> --> CallMethod
                => next.Exec; // Task, Use "Method Group"

        private static string Exec(this Func<int, string> method, int arg, DateTime t)
        {
            Console.WriteLine($"攔截:{t.ToShortDateString()}");
            return method(arg);
        }
    }
}
