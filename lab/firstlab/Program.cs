using System;
using System.Collections.Generic;

namespace firstlab
{
    internal class Program
    {
        private static int Calculate(List<int> numbers, List<string> signs)
        {
            for (int i = 0; i < signs.Count; i++)
            {
                if (signs[i] == "*")
                {
                    numbers[i] *= numbers[i + 1];
                    numbers.RemoveAt(i + 1);
                    signs.RemoveAt(i);
                    i -= 1;
                }
                else if (signs[i] == "/")
                {
                    numbers[i] /=  numbers[i + 1];
                    signs.RemoveAt(i);
                    numbers.RemoveAt(i + 1);
                    i -= 1;
                }
            }
            for (int i = 0; i < signs.Count; i++)
            {
                if (signs[i] == "+")
                {
                    numbers[i] += numbers[i + 1];
                    signs.RemoveAt(i);
                    numbers.RemoveAt(i+1);
                    i -= 1;
                }
                else if (signs[i] == "-")
                {
                    numbers[i] -= numbers[i + 1];
                    signs.RemoveAt(i);
                    numbers.RemoveAt(i + 1);
                    i -= 1;
                }
            }
            return numbers[0];
        }

        private static void FindBrackets(string expression)
        {
            int openBracketIndex = expression.IndexOf('(');
            if (openBracketIndex != -1)
            {
                int closeBracketIndex = expression.LastIndexOf(')');
                if (closeBracketIndex != -1)
                {
                    string expressionInBrackets = expression.Substring(openBracketIndex + 1, closeBracketIndex - openBracketIndex - 1);
                    int resultInBrackets = CalculateExpressionInBrackets(expressionInBrackets);
                    expression = expression.Remove(openBracketIndex, closeBracketIndex - openBracketIndex + 1);
                    expression = expression.Insert(openBracketIndex, resultInBrackets.ToString());
                }
            }
        }
        private static int CalculateExpressionInBrackets(string expression)
        {
            List<int> numbers = new List<int>();
            List<string> signs = new List<string>();
            return Calculate(WriteNumbersInList(expression, numbers), WriteSignsInList(expression, signs));
        }
        private static List<int> WriteNumbersInList(string text, List<int> numbers)
        {
            string[] strNums = TextWithOutSigns(text).Split();
            foreach (var strNum in strNums)
                if (int.TryParse(strNum, out int num))
                    numbers.Add(num);
            return numbers;
        }

        private static string TextWithOutSigns(string texts)
        {
            foreach (var c in texts)
                if (char.IsSymbol(c) || char.IsPunctuation(c))
                    texts = texts.Replace(c, ' ');
            return texts;
        }
        private static List<string> WriteSignsInList(string expression, List<string> signs)
        {
            foreach (var c in expression)
                if ((char.IsSymbol(c) || char.IsPunctuation(c)) && (c != '(' && c != ')'))
                {
                    signs.Add(c.ToString());
                    expression = expression.Replace(c, ' ');
                }
            return signs;
        }
        public static void Main(string[] args)
        {
            string expression = Console.ReadLine();
            List<int> numbers = new List<int>();
            List<string> signs = new List<string>();
            expression = expression.Replace(" ", "");
            FindBrackets(expression);
            WriteSignsInList(expression, signs);
            WriteNumbersInList(expression, numbers);
            Console.Write("Список чисел: ");
            foreach (var number in numbers)
                Console.Write(number + " ");
            Console.WriteLine();
            Console.WriteLine("Список знаков: " + string.Join(" ", signs.ToArray()));
            Console.WriteLine("Результат: " + Calculate(numbers, signs));
            
        }
    }
}