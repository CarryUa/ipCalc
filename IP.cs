using System;
using System.ComponentModel;
using System.Net;
class IP()
{
    public byte maskBitsNumber = 0;
    public string minHostAddress = "null";
    public string maxHostAddress = "null";
    public static string BitwiseOR(string binStr1, string binStr2)
    {
        string result = "";
        for (int i = 0; i < binStr1.Length; i++)
        {
            if (binStr1[i] == '1' || binStr2[i] == '1')
                result += '1';
            else if (binStr1[i] == '.' || binStr2[i] == '.')
                result += '.';
            else
                result += '0';
        }
        return result;
    }
    public static string BitwiseAND(string binStr1, string binStr2)
    {
        string result = "";
        for (int i = 0; i < binStr1.Length; i++)
        {
            if (binStr1[i] == '1' && binStr2[i] == '1')
                result += "1";
            else if (binStr1[i] == '.' || binStr2[i] == '.')
                result += ".";
            else
                result += "0";
        }
        return result;
    }
    public static string[] ParseIPString(string IP)
    {
        return IP.Split(".");
    }
    public static string InvertBinaryIPString(string str)
    {
        string[] parsedString = ParseIPString(str);
        string nstr = "";
        foreach (string currentSygment in parsedString)
        {
            foreach (char c in currentSygment)
            {
                if (c == '0')
                    nstr += '1';
                else if (c == '1')
                    nstr += '0';
            }
            nstr += '.';
        }
        nstr = nstr.Remove(nstr.Length - 1);
        return nstr;
    }
    public static string InvertString(string str)
    {
        string nstr = "";
        for (int i = str.Length - 1; i >= 0; i--)
        {
            nstr += str[i];
        }
        return nstr;
    }
    public static string ToDecimal(string IP)
    {
        byte IPAsByte;
        string IPAsByteAsString = "";
        string[] parsedIP = ParseIPString(IP);
        int n;
        foreach (string currentSygment in parsedIP)
        {
            n = 0;
            IPAsByte = 0;
            for (int i = currentSygment.Length - 1; i >= 0; i--)
            {
                IPAsByte += Convert.ToByte(Math.Pow(2, n) * Convert.ToDouble(Convert.ToString(currentSygment[i])));
                n++;
            }
            IPAsByteAsString += Convert.ToString(IPAsByte);
            IPAsByteAsString += ".";
        }
        IPAsByteAsString = IPAsByteAsString.Remove(IPAsByteAsString.Length - 1);
        return IPAsByteAsString;
    }
    public static string ToBinary(string IP)
    {
        byte currentSygment;
        string binIP = "";
        string[] parsedIP = ParseIPString(IP);
        foreach (string Sygment in parsedIP)
        {
            string binIPTransit = "";
            currentSygment = Convert.ToByte(Sygment);
            do
            {
                binIPTransit += Convert.ToString(currentSygment % 2);
                currentSygment /= 2;
            } while (currentSygment >= 1);

            binIP += InvertString(binIPTransit);
            binIP += ".";
        }
        binIP = binIP.Remove(binIP.Length - 1);
        parsedIP = binIP.Split(".");
        binIP = "";
        for (int i = 0; i < parsedIP.Length; i++)
        {
            if (parsedIP[i].Length < 8)
            {
                int diference = 8 - parsedIP[i].Length;
                for (int j = 0; j < diference; j++)
                {
                    parsedIP[i] = "0" + parsedIP[i];
                }
            }
            binIP += parsedIP[i];
            if (i != 3)
                binIP += ".";
        }
        return binIP;
    }
    public static IPAddress CalculateNetAddress(string ip, string mask)
    {
        string nNetAddress;
        string binIP = ToBinary(ip);
        string binMask = ToBinary(mask);
        nNetAddress = BitwiseAND(binIP, binMask);
        return IPAddress.Parse(ToDecimal(nNetAddress));
    }
    public static IPAddress CalculateBroadcastAddress(string mask, string networkAddress)
    {
        string broadcatsAddressStr;
        string netIP = ToBinary(networkAddress);
        string invertedMask = InvertBinaryIPString(ToBinary(mask));
        broadcatsAddressStr = BitwiseOR(netIP, invertedMask);
        return IPAddress.Parse(ToDecimal(broadcatsAddressStr));
    }
    public static int CalculateNumberOfHosts(int maskBitsNumber)
    {
        return (int)Math.Pow(2, 32 - maskBitsNumber) - 2;
    }
    public static IPAddress CalculateMinHostAddress(IPAddress networkAddress)
    {
        string[] parsedIP = ParseIPString(networkAddress.ToString());
        string nIP = "";
        byte lastSygment = Convert.ToByte(parsedIP[3]);
        lastSygment++;
        parsedIP[3] = Convert.ToString(lastSygment);
        foreach (string Sygment in parsedIP)
        {
            nIP += Sygment;
            nIP += '.';
        }
        nIP = nIP.Remove(nIP.Length - 1);
        return IPAddress.Parse(nIP);
    }
    public static IPAddress CalculateMaxHostAddress(IPAddress broadcastAddress)
    {
        string[] parsedIP = ParseIPString(broadcastAddress.ToString());
        string nIP = "";
        byte lastSygment = Convert.ToByte(parsedIP[3]);
        lastSygment--;
        parsedIP[3] = Convert.ToString(lastSygment);
        foreach (string Sygment in parsedIP)
        {
            nIP += Sygment;
            nIP += '.';
        }
        nIP = nIP.Remove(nIP.Length - 1);
        return IPAddress.Parse(nIP);
    }
}
