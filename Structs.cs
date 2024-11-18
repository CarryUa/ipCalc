using System;
using System.ComponentModel;
using System.Net;
class IP()
{
    public string ip = "null";
    public string mask = "null";
    public byte maskBitsNumber = 0;
    public string networkAddress = "null";
    public string broadcatsAddress = "null";
    public string minHostAddress = "null";
    public string maxHostAddress = "null";
    public int numberOfHosts;
    public string[] ParseIPString(string IP)
    {
        return IP.Split(".");
    }
    public string InvertBinaryIPString(string str)
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
    public string InvertString(string str)
    {
        string nstr = "";
        for (int i = str.Length - 1; i >= 0; i--)
        {
            nstr += str[i];
        }
        return nstr;
    }
    public string ToDecimal(string IP)
    {
        byte IPAsByte;
        string IPAsByteAsString = "";
        string[] parsedIP = ParseIPString(IP);
        string currentSygment;
        int n;
        for (int i = 0; i < parsedIP.Length; i++)
        {
            n = 0;
            IPAsByte = 0;
            currentSygment = parsedIP[i];
            for (int j = parsedIP[i].Length - 1; j >= 0; j--)
            {

                IPAsByte += Convert.ToByte(Math.Pow(2, n) * Convert.ToDouble(Convert.ToString(currentSygment[j])));
                n++;
            }
            IPAsByteAsString += Convert.ToString(IPAsByte);
            if (i != 3)
                IPAsByteAsString += ".";
        }
        return IPAsByteAsString;

    }
    public string ToBinary(string IP)
    {
        byte currentSygment;
        string binIP = "";
        string[] parsedIP = ParseIPString(IP);
        for (int i = 0; i < parsedIP.Length; i++)
        {
            string binIPTransit = "";
            currentSygment = Convert.ToByte(parsedIP[i]);
            do
            {
                binIPTransit += Convert.ToString(currentSygment % 2);
                currentSygment /= 2;
            } while (currentSygment >= 1);

            binIP += InvertString(binIPTransit);

            if (i != 3)
                binIP += ".";
        }
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
    public void SetIP(string nIP)
    {
        ip = "";
        string[] parsednIP = nIP.Split(".");
        bool errors = parsednIP.Length != 4;
        foreach (string Sygment in parsednIP)
        {
            ip += Sygment;
            ip += '.';
        }
        ip = ip.Remove(ip.Length - 1);
        Console.Write("-------------------------------------------------------------------------------------------" + '\n');
        if (!errors)
        {
            ip = nIP;
            Console.Write("     Finished! " + "Tryed seting IP to \"" + nIP + "\"" + "\n");
        }
        else
            Console.Write("     [ERROR]Wrong IP format!! " + "Tryed seting IP to \"" + nIP + "\"" + "\n");
        Console.Write("     Mask as binary: " + ToBinary(ip) + "\n");
        Console.Write("     IP as String: \"" + ip + "\"" + "\n");
        Console.Write("-------------------------------------------------------------------------------------------" + "\n \n");


    }
    public void SetMask(string nIP)
    {
        mask = "";
        string[] parsednIP = nIP.Split(".");
        bool errors = parsednIP.Length != 4;
        maskBitsNumber = 0;
        foreach (string Sygment in parsednIP)
        {
            mask += Sygment;
            mask += '.';
        }
        mask = mask.Remove(mask.Length - 1);
        Console.Write("-------------------------------------------------------------------------------------------" + '\n');
        if (!errors)
        {
            mask = nIP;
            foreach (char c in ToBinary(mask))
            {
                if (c == '1')
                    maskBitsNumber++;
            }
            Console.Write("     Finished! " + "Tryed seting mask to \"" + nIP + "\"" + "\n");
        }

        else
            Console.Write("     [ERROR]Wrong mask format! " + "Tryed seting mask to \"" + nIP + "\"" + "\n");

        Console.Write("     Mask as binary: " + ToBinary(mask) + "\n");
        Console.Write("     Mask as String: \"" + ip + "\" \n");
        Console.Write("-------------------------------------------------------------------------------------------" + "\n \n");


    }
    public void CalculateNetAddress()
    {
        string nNetAddress = "";
        for (int i = 0; i < ToBinary(ip).Length; i++)
        {
            if (ToBinary(ip).ToCharArray()[i] == '1' && ToBinary(mask).ToCharArray()[i] == '1')
                nNetAddress += "1";
            else if (ToBinary(ip).ToCharArray()[i] == '.' || ToBinary(mask).ToCharArray()[i] == '.')
                nNetAddress += ".";
            else
                nNetAddress += "0";
        }
        networkAddress = ToDecimal(nNetAddress);
    }
    public void CalculateBroadcastAddress()
    {
        broadcatsAddress = "";
        string netIP = ToBinary(networkAddress);
        string invertedMask = InvertBinaryIPString(ToBinary(mask));
        for (int i = 0; i < netIP.Length; i++)
        {
            if (netIP[i] == '1' || invertedMask[i] == '1')
                broadcatsAddress += '1';
            else if (netIP[i] == '.' || invertedMask[i] == '.')
                broadcatsAddress += '.';
            else
                broadcatsAddress += '0';
        }
        broadcatsAddress = ToDecimal(broadcatsAddress);
    }
    public void CalculateNumberOfHosts()
    {
        numberOfHosts = (int)Math.Pow(2, 32 - maskBitsNumber) - 2;
    }
    public void CalculateMinHostAddress()
    {
        string[] parsedIP = networkAddress.Split(".");
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
        minHostAddress = nIP;
    }
    public void CalculateMaxHostAddress()
    {
        string[] parsedIP = broadcatsAddress.Split(".");
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
        maxHostAddress = nIP;
    }
    public void CalculateAll()
    {
        CalculateNetAddress();
        CalculateBroadcastAddress();
        CalculateNumberOfHosts();
        CalculateMinHostAddress();
        CalculateMaxHostAddress();
    }
}
