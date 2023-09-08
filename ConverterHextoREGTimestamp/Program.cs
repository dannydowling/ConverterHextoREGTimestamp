using System;
using System.Linq;

public class LittleEndianHexToTimestampConverter
{
    public static DateTime ConvertHexToTimestamp(string hexValue)
    {
        if (hexValue.Length % 2 != 0)
        {
            throw new ArgumentException("Hex value must have an even number of characters.");
        }

        byte[] bytes = new byte[hexValue.Length / 2];

        for (int i = 0; i < hexValue.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexValue.Substring(i, 2), 16);
        }

        long ticks = BitConverter.ToInt64(bytes, 0);
        TimeSpan offset = TimeSpan.FromTicks(ticks);
        DateTime baseDate = new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        return baseDate + offset;
    }

    public static void Main(string[] args)
    {
        string hexValue = args.First(); 
        DateTime timestamp = ConvertHexToTimestamp(hexValue);

        Console.WriteLine("Timestamp: " + timestamp);
    }
}
