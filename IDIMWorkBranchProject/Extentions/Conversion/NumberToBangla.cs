using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace IDIMWorkBranchProject.Extentions.Conversion
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    public class NumberToBangla
    {
        // Store words in an array to convert number to Bangla words
        protected static string[] words = new string[100]
        {
            "শ\u09c2ন\u09cdয", "এক", "দ\u09c2ই", "ত\u09bfন", "চ\u09beর", "প\u09be\u0981চ", "ছয়", "স\u09beত", "আট", "নয়",
            "দশ", "এগ\u09beর\u09cb", "ব\u09beর\u09cb", "ত\u09c7র\u09cb", "চ\u09ccদ\u09cdদ", "পন\u09c7র\u09cb", "ষ\u09cbল", "সত\u09c7র\u09cb", "আঠ\u09beর\u09cb", "উন\u09bfশ",
            "ব\u09bfশ", "এক\u09c1শ", "ব\u09beইশ", "ত\u09c7ইশ", "চব\u09cdব\u09bfশ", "প\u0981চ\u09bfশ", "ছ\u09beব\u09cdব\u09bfশ", "স\u09beত\u09beশ", "আঠ\u09beশ", "ঊনত\u09cdর\u09bfশ",
            "ত\u09cdর\u09bfশ", "একত\u09cdর\u09bfশ", "বত\u09cdর\u09bfশ", "ত\u09c7ত\u09cdর\u09bfশ", "চ\u09ccত\u09cdর\u09bfশ", "প\u0981য\u09bcত\u09cdর\u09bfশ", "ছত\u09cdর\u09bfশ", "স\u09be\u0981ইত\u09cdর\u09bfশ", "আটত\u09cdর\u09bfশ", "ঊনচল\u09cdল\u09bfশ",
            "চল\u09cdল\u09bfশ", "একচল\u09cdল\u09bfশ", "ব\u09bfয\u09bc\u09beল\u09cdল\u09bfশ", "ত\u09c7ত\u09beল\u09cdল\u09bfশ", "চ\u09c1য\u09bc\u09beল\u09cdল\u09bfশ", "প\u0981য\u09bcত\u09beল\u09cdল\u09bfশ", "ছ\u09c7চল\u09cdল\u09bfশ", "স\u09beতচল\u09cdল\u09bfশ", "আটচল\u09cdল\u09bfশ", "ঊনপঞ\u09cdচ\u09beশ",
            "পঞ\u09cdচ\u09beশ", "এক\u09beন\u09cdন", "ব\u09beহ\u09beন\u09cdন", "ত\u09bfপ\u09cdপ\u09beন\u09cdন", "চ\u09c1য\u09bc\u09beন\u09cdন", "পঞ\u09cdচ\u09beন\u09cdন", "ছ\u09beপ\u09cdপ\u09beন\u09cdন", "স\u09beত\u09beন\u09cdন", "আট\u09beন\u09cdন", "ঊনষ\u09beট",
            "ষ\u09beট", "একষট\u09cdট\u09bf", "ব\u09beষট\u09cdট\u09bf", "ত\u09c7ষট\u09cdট\u09bf", "চ\u09ccষট\u09cdট\u09bf", "প\u0981য\u09bcষট\u09cdট\u09bf", "ছ\u09c7ষট\u09cdট\u09bf", "স\u09beতষট\u09cdট\u09bf", "আটষট\u09cdট\u09bf", "ঊনসত\u09cdতর",
            "সত\u09cdতর", "এক\u09beত\u09cdতর", "ব\u09beহ\u09beত\u09cdতর", "ত\u09bfয\u09bc\u09beত\u09cdতর", "চ\u09c1য\u09bc\u09beত\u09cdতর", "প\u0981চ\u09beত\u09cdতর", "ছ\u09bfয\u09bc\u09beত\u09cdতর", "স\u09beত\u09beত\u09cdতর", "আট\u09beত\u09cdতর", "ঊনআশ\u09bf",
            "আশ\u09bf", "এক\u09beশ\u09bf", "ব\u09bfর\u09beশ\u09bf", "ত\u09bfর\u09beশ\u09bf", "চ\u09c1র\u09beশ\u09bf", "প\u0981চ\u09beশ\u09bf", "ছ\u09bfয\u09bc\u09beশ\u09bf", "স\u09beত\u09beশ\u09bf", "আট\u09beশ\u09bf", "ঊননব\u09cdবই",
            "নব\u09cdবই", "এক\u09beনব\u09cdবই", "ব\u09bfর\u09beনব\u09cdবই", "ত\u09bfর\u09beনব\u09cdবই", "চ\u09c1র\u09beনব\u09cdবই", "প\u0981চ\u09beনব\u09cdবই", "ছ\u09bfয\u09bc\u09beনব\u09cdবই", "স\u09beত\u09beনব\u09cdবই", "আট\u09beনব\u09cdবই", "ন\u09bfর\u09beনব\u09cdবই"
        };

        public static string GetTakaInWord(object number)
        {
            IsValid(number);
            double num = Convert.ToDouble(number); // Convert the number to double for consistency
            if (num == 0.0)
            {
                return "শ\u09c2ন\u09cdয ট\u09beক\u09be";
            }

            if (num.ToString().Contains("."))
            {
                return ConvertFloatNumberToMoneyFormat(num);
            }

            return GetBanglaWord(num) + " ট\u09beক\u09be ";
        }

        public static string GetCommaSeparateBanglaDigit(object number)
        {
            IsValid(number);
            string banglaDigits = GetBanglaDigits(Convert.ToDouble(number));
            return string.Format("{0:n0}", number);
        }

        public static string GetBanglaDigits(object number)
        {
            IsValid(number);
            Dictionary<char, char> dictionary = new Dictionary<char, char>
            {
                { '0', '০' },
                { '1', '১' },
                { '2', '২' },
                { '3', '৩' },
                { '4', '৪' },
                { '5', '৫' },
                { '6', '৬' },
                { '7', '৭' },
                { '8', '৮' },
                { '9', '৯' }
            };

            var stringBuilder = new StringBuilder();
            string text = Convert.ToString(number);
            foreach (char c in text)
            {
                if (dictionary.ContainsKey(c))
                {
                    stringBuilder.Append(dictionary[c]);
                }
                else
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }

        public static string GetBanglaWord(object number)
        {
            IsValid(number);
            double num = Convert.ToDouble(number);
            int numInt = (int)Math.Truncate(num);
            int num2 = (int)((num - (double)numInt) * 100.0);
            string text = ToWord(numInt);
            if (num2 > 0)
            {
                text += " দশম\u09bfক ";
                text += ToWord(num2);
            }

            return text;
        }

        protected static string ToWord(int num)
        {
            string text = string.Empty;
            int num2 = num / 10000000;
            if (num2 != 0)
            {
                text += (num2 <= 99) ? (words[num2] + " ক\u09cbট\u09bf ") : (GetBanglaWord(num2) + " ক\u09cbট\u09bf ");
            }

            int num3 = num % 10000000;
            int num4 = num3 / 100000;
            if (num4 > 0)
            {
                text += words[num4] + " লক\u09cdষ ";
            }

            int num5 = num3 % 100000;
            int num6 = num5 / 1000;
            if (num6 > 0)
            {
                text += words[num6] + " হ\u09beজ\u09beর ";
            }

            int num7 = num5 % 1000;
            int num8 = num7 / 100;
            if (num8 > 0)
            {
                text += words[num8] + " শত ";
            }

            int num9 = num7 % 100;
            if (num9 > 0)
            {
                text += words[num9];
            }

            return text;
        }

        protected static string ConvertFloatNumberToMoneyFormat(double number)
        {
            string text = number.ToString("0.00", CultureInfo.InvariantCulture);
            string[] array = text.Split('.');
            string text2 = GetBanglaWord(array[0]) + " ট\u09beক\u09be ";
            if (array.Length > 1)
            {
                text2 += GetBanglaWord(array[1]) + " পয়স\u09be";
            }

            return text2;
        }

        public static string GetBanglaMonthName(int monthOfYear)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>
            {
                { 1, "জ\u09beন\u09c1য়\u09beর\u09bf" },
                { 2, "ফ\u09c7ব\u09cdর\u09c1য়\u09beর\u09bf" },
                { 3, "ম\u09beর\u09cdচ" },
                { 4, "এপ\u09cdর\u09bfল" },
                { 5, "ম\u09c7" },
                { 6, "জ\u09c1ন" },
                { 7, "জ\u09c1ল\u09beই" },
                { 8, "আগস\u09cdট" },
                { 9, "স\u09c7প\u09cdট\u09c7ম\u09cdবর" },
                { 10, "অক\u09cdট\u09cbবর" },
                { 11, "নভ\u09c7ম\u09cdবর" },
                { 12, "ড\u09bfস\u09c7ম\u09cdবর" }
            };

            if (monthOfYear >= 1 && monthOfYear <= 12)
            {
                return dictionary[monthOfYear];
            }

            throw new Exception("Month of year should be between 1 and 12");
        }

        public static string GetBanglaDayName(int dayOfWeek)
        {
            string[] array = new string[7] { "শ\u09c2ক\u09cdরব\u09beর", "শন\u09bfব\u09beর", "রব\u09bfব\u09beর", "স\u09cbমব\u09beর", "মঙ\u09cdগলব\u09beর", "ব\u09c1ধব\u09beর", "ব\u09c3হস\u09cdপত\u09bfব\u09beর" };
            if (dayOfWeek < 0 || dayOfWeek > 6)
            {
                throw new ArgumentOutOfRangeException("dayOfWeek", "Invalid day of week. Must be between 0 and 6.");
            }

            return array[dayOfWeek - 1];
        }

        protected static void IsValid(object number)
        {
            if (!(number is decimal || number is int || number is long || number is float || number is double))
            {
                throw new Exception("Invalid Number");
            }

            if (Math.Abs(Convert.ToDecimal(number)) > 999999999999999m)
            {
                throw new Exception("Number should be less than 999999999999999");
            }
        }
    }


}