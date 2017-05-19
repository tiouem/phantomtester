using System;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Worker.Commands;
using Worker.Converters;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var msgHandler = new MessageHandler();
        }
    }
}
