// See https://aka.ms/new-console-template for more information
using Laba3;
using Laba3.commands;
using System.Text;


try
{
    new ConsoleInteractor("C:\\Users\\alex\\source\\repos\\Laba3\\Laba3\\output.txt").Run();
}
catch (Exception ex)
{
    Console.WriteLine("Программа экстренно завершилась!");
    Console.WriteLine(ex.StackTrace);
}
