using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SocketsT1_T2.Tier2.Commands;

namespace SocketsT1_T2.Tier2.Util
{
    public class CommandsReflection
    {
        public static IList<ICommand> Commands { get; } = new List<ICommand>();
        
        public void Init()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(t => t.GetCustomAttributes<MyCommandAttribute>().Any());
            foreach (var type in types)
            {
                ICommand test = (ICommand)Activator.CreateInstance(type);
                Commands.Add(test);
            }

            Console.WriteLine("Test type: " + Commands[0].GetType().Name);
        }
    }
}