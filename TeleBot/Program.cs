using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;

namespace TeleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var _bot = new TelegramBotClient("470752321:AAFf1wjTmKuomaQlZ07QAhkCgSwMwh-D6TI");
            var myBot = new AlexBot(_bot);
            Console.ReadKey();
        }
    }
}
