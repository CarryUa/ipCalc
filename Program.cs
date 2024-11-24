﻿using System;
class Program
{
    public static void Main(string[] args)
    {
        // Console.BackgroundColor = ConsoleColor.White;
        bool errors = false;
        IP ip = new IP();
        Console.Write("Set IP: ");
        ip.SetIP(Console.ReadLine());
        Console.Write("\nSet mask: ");
        ip.SetMask(Console.ReadLine());
        try
        {
            ip.CalculateAll();
        }
        catch
        {
            errors = true;
        }
        if (!errors)
        {
            Console.Write("-------------------------------------------------------------------------------------------" + "\n");
            Console.Write("     >Results:" + '\n');
            Console.Write("     IP Address: " + ip.ip);
            Console.SetCursorPosition(50, Console.GetCursorPosition().Top);
            Console.Write("(" + ip.ToBinary(ip.ip) + ")" + '\n');
            Console.Write("     IP Mask: " + ip.mask + "/" + ip.maskBitsNumber);
            Console.SetCursorPosition(50, Console.GetCursorPosition().Top);
            Console.Write("(" + ip.ToBinary(ip.mask) + ")" + '\n');
            Console.Write("     Network Address: " + ip.networkAddress);
            Console.SetCursorPosition(50, Console.GetCursorPosition().Top);
            Console.Write("(" + ip.ToBinary(ip.networkAddress) + ")" + '\n');
            Console.Write("     Broadcast Address: " + ip.broadcatsAddress);
            Console.SetCursorPosition(50, Console.GetCursorPosition().Top);
            Console.Write("(" + ip.ToBinary(ip.broadcatsAddress) + ")" + '\n');
            Console.Write("     Host Number: " + ip.numberOfHosts + '\n');
            Console.Write("     Min Host: " + ip.minHostAddress);
            Console.SetCursorPosition(50, Console.GetCursorPosition().Top);
            Console.Write("(" + ip.ToBinary(ip.minHostAddress) + ")" + '\n');
            Console.Write("     Max Host: " + ip.maxHostAddress);
            Console.SetCursorPosition(50, Console.GetCursorPosition().Top);
            Console.Write("(" + ip.ToBinary(ip.maxHostAddress) + ")" + '\n');
            Console.Write("-------------------------------------------------------------------------------------------");
        }
        else
            Console.Write("[ERROR] Something went wrong during ip calculations");

        while (true)
        {
        }
    }
}