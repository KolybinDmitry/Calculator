using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorOfTruthsTables
{
    // VIEW_MODEL
    internal class Calculus
    {
        // метод Compute работает с выражениями вида 10^ - обратная польская запись (или  1^0 в обычной записи)
        static bool Compute(string str) 
        {
            if (str == string.Empty)
                return false;

            var binaryOperations = new Dictionary<char, Func<bool, bool, bool>>();
            binaryOperations.Add('^', (y, x) => x && y);                                        // коньюнкция
            binaryOperations.Add('v', (y, x) => x || y);                                        // дизъюнкция
            binaryOperations.Add('>', (y, x) => Convert.ToUInt16(x) <= Convert.ToUInt16(y));    // импликация
            binaryOperations.Add('=', (y, x) => x == y);                                        // эквивалентность
            binaryOperations.Add('@', (y, x) => !x && y || x && !y);                            // xor

            var unaryOperation = new Dictionary<char, Func<bool, bool>>();
            unaryOperation.Add('!', (x) => !x);                                                 // отрицание

            var stack = new Stack<bool>();
            foreach (var e in str)
            {
                if (e <= '1' && e >= '0')
                    stack.Push(Convert.ToBoolean(e - '0'));
                else if (unaryOperation.ContainsKey(e))
                    stack.Push(unaryOperation[e](stack.Pop()));
                else if (binaryOperations.ContainsKey(e))
                    stack.Push(binaryOperations[e](stack.Pop(), stack.Pop()));
                else
                    throw new ArgumentException();
            }
            return stack.Pop();
        }

        // метод Function ожидает str вида x1x2^ (или x1^x2)
        static public void Function(List<RowInTableOfTruth> variables, string str)
        {
            // этот цикл преобразует x1x2^ к 00^, 01^, 10^, 11^ записям, на каждой итерации соответственно
            for (int i = 0; i < variables.Count; i++)
            {
                var expression = str;
                
                expression = expression.Replace("x1", variables[i].x1.ToString());
                expression = expression.Replace("x2", variables[i].x2.ToString());
                expression = expression.Replace("x3", variables[i].x3.ToString());
                expression = expression.Replace("x4", variables[i].x4.ToString());
                expression = expression.Replace("x5", variables[i].x5.ToString());
                expression = expression.Replace("x6", variables[i].x6.ToString());
                expression = expression.Replace("x7", variables[i].x7.ToString());
                expression = expression.Replace("x8", variables[i].x8.ToString());

                variables[i].Function = Convert.ToInt32(Compute(expression));
            }
        }
    }
}
