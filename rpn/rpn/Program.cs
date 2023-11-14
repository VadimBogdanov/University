using System;
using System.Collections.Generic;

class InfixToRPNCalculator
{
    private static int GetOperatorPriority(string op)
    {
        switch (op)
        {
            case "+":
            case "-":
                return 1;
            case "*":
            case "/":
                return 2;
            default:
                return 0;
        }
    }

    public static string ConvertToRPN(string infixExpression)
    {
        string[] infixTokens = infixExpression.Split(' ');
        Stack<string> operatorStack = new Stack<string>();
        List<string> rpnTokens = new List<string>();
        foreach (string token in infixTokens)
        {
            if (double.TryParse(token, out _))
                rpnTokens.Add(token);
            else if (token == "(")
                operatorStack.Push(token);
            else if (token == ")")
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    rpnTokens.Add(operatorStack.Pop());
                operatorStack.Pop();
            }
            else
            {
                while (operatorStack.Count > 0 && GetOperatorPriority(operatorStack.Peek()) >= GetOperatorPriority(token))
                    rpnTokens.Add(operatorStack.Pop());
                operatorStack.Push(token);
            }
        }
        while (operatorStack.Count > 0)
            rpnTokens.Add(operatorStack.Pop());
        return string.Join(" ", rpnTokens.ToArray());
    }

    public static double EvaluateRPN(string rpnExpression)
    {
        string[] rpnTokens = rpnExpression.Split(' ');
        Stack<double> stack = new Stack<double>();
        foreach (string token in rpnTokens)
        {
            if (double.TryParse(token, out double operand))
                stack.Push(operand);
            else
            {
                double operand2 = stack.Pop();
                double operand1 = stack.Pop();
                double result = PerformOperation(token, operand1, operand2);
                stack.Push(result);
            }
        }

        return stack.Pop();
    }

    private static double PerformOperation(string operatorToken, double operand1, double operand2)
    {
        switch (operatorToken)
        {
            case "+":
                return operand1 + operand2;
            case "-":
                return operand1 - operand2;
            case "*":
                return operand1 * operand2;
            case "/":
                if (operand2 == 0)
                    throw new DivideByZeroException("Попытка деления на ноль.");
                
                return operand1 / operand2;
            default:
                throw new ArgumentException("Недопустимый оператор: " + operatorToken);
        }
    }

    static void Main(string[] args)
    {
        string infixExpression = "1,5 * ( 1250 - 0,7 ) + ( 25 - 2 * ( 5 - 2 ) ) - 6 / 2";
        string rpnExpression = ConvertToRPN(infixExpression);
        Console.WriteLine($"Выражение в ОПЗ: {rpnExpression}");
        Console.WriteLine($"Результат: {EvaluateRPN(rpnExpression)}");
    }
}
