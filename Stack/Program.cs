using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            IStack<int> intSequenceStack = new SequenceStack<int>(50);
            IStack<char> charSequenceStack = new SequenceStack<char>(50);
            IStack<double> doubleSequenceStack = new SequenceStack<double>(50);

            IStack<int> intLinkStack = new LinkStack<int>();
            IStack<char> charLinkStack = new LinkStack<char>();
            IStack<double> doubleLinkStack = new LinkStack<double>();

            #region 验证括号是否匹配
            Console.WriteLine(IsValidExpression("(()[]{}", charSequenceStack));
            charSequenceStack.Clear();
            Console.WriteLine(IsValidExpression("()[]{}(", charSequenceStack));
            charSequenceStack.Clear();
            Console.WriteLine(IsValidExpression("([{}])", charSequenceStack));
            charSequenceStack.Clear();
            Console.WriteLine(IsValidExpression("()[{}", charLinkStack));
            charLinkStack.Clear();
            Console.WriteLine(IsValidExpression("[)[]{}", charLinkStack));
            charLinkStack.Clear();
            Console.WriteLine(IsValidExpression("([]{})]", charLinkStack));
            charLinkStack.Clear();
            Console.WriteLine(IsValidExpression("([]{})", charLinkStack));
            charLinkStack.Clear();
            #endregion

            #region 十进制转n进制
            Console.WriteLine("十进制的8转为二进制的值为: {0}", DecimalConvert(8, 2, intLinkStack));
            intLinkStack.Clear();
            #endregion

            #region 根据表达式求后缀表达式，和根据后缀表达式计算其值
            //示例：a*b+(c-d/e)*f 转为 ab*cde/-f*+
            Console.WriteLine();
            Console.WriteLine("原表达式为：\n{0}", "a*b+(c-d/e)*f");
            Console.WriteLine("转换为后缀表达式为：\n{0}", GetPostFixExpression("a*b+(c-d/e)*f", charLinkStack));
            charLinkStack.Clear();

            Console.WriteLine();
            string postFixExpression = "3 7 * 4 17 3 / - 8 * +".Trim();
            Console.WriteLine("后缀表达式“{0}”的计算结果为：\n{1}", postFixExpression, GetPostFixExpValue(postFixExpression, doubleLinkStack));
            doubleLinkStack.Clear();
            #endregion

            Console.ReadLine();
        }

        //验证括号是否匹配,如输入"()[]{}("返回false，"()[]{}"返回true
        private static bool IsValidExpression(string s, IStack<char> stack)
        {
            bool result = true;
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case '(':
                    case '[':
                    case '{':
                        stack.Push(s[i]);
                        break;
                    case ')':
                        if (stack.IsEmpty() || stack.Pop() != '(')
                        {
                            return false;
                        }
                        break;
                    case ']':
                        if (stack.IsEmpty() || stack.Pop() != '[')
                        {
                            return false;
                        }
                        break;
                    case '}':
                        if (stack.IsEmpty() || stack.Pop() != '{')
                        {
                            return false;
                        }
                        break;
                }
            }
            if (result)
            {
                result = stack.IsEmpty();
            }
            return result;
        }

        //十进制转n进制，因为其原理为除n取余然后逆序输出余数
        //所以正好符合栈的先进后出
        private static int DecimalConvert(int num, int decimalTo, IStack<int> stack)
        {
            if (decimalTo < 1)
            {
                Console.WriteLine("要转换的进制不合理！");
                return -1;
            }
            string strResult = null;
            //num一开始就为0时，下面两个while都跳过，Convert.ToInt32(null)返回0
            while (num != 0)
            {
                stack.Push(num % decimalTo);
                num = num / decimalTo;
            }
            while (!stack.IsEmpty())
            {
                strResult += stack.Pop();
            }
            return Convert.ToInt32(strResult);
        }

        //根据表达式求后缀表达式
        private static string GetPostFixExpression(string originExp, IStack<char> stack)
        {
            string result = null;
            if (string.IsNullOrEmpty(originExp))
            {
                Console.WriteLine("原表达式为空！");
            }
            else
            {
                string operators = "+-*/()";
                char[] originExpArray = originExp.ToArray();
                //遍历原表达式中每一个字符，是运算数则给结果表达式
                //是运算符则判断，如果是)则从栈顶开始遍历，把最近的(之上的运算符都弹出给结果表达式,()本身不要管
                //对于其他运算符如果当前运算符优先级低于栈顶运算符则把栈顶运算符弹出并给结果表达式
                //如果优先级高于或等于，则当前运算符压入栈；(直接压入
                foreach (char item in originExpArray)
                {
                    if (operators.Contains(item))
                    {
                        if (item == '+' || item == '-')
                        {
                            char topElem = stack.GetTop();
                            if (topElem == '*' || topElem == '/')
                            {
                                result += stack.Pop();
                                stack.Push(item);
                            }
                            else
                            {
                                stack.Push(item);
                            }
                        }
                        else if (item == ')')
                        {
                            while (true)
                            {
                                char topElem = stack.Pop();
                                if (topElem == '(')
                                {
                                    break;
                                }
                                else
                                {
                                    result += topElem;
                                }
                            }
                        }
                        else
                        {
                            stack.Push(item);
                        }
                    }
                    else
                    {
                        result += item;
                    }
                }
                //遍历完后如果栈中还有运算符则全部依次弹出给表达式
                while (!stack.IsEmpty())
                {
                    result += stack.Pop();
                }
            }
            return result;
        }

        //根据后缀表达式求表达式的值
        private static double GetPostFixExpValue(string expression, IStack<double> stack)
        {
            double result = -1.0;
            if (string.IsNullOrEmpty(expression))
            {
                Console.WriteLine("表达式为空！");
            }
            else
            {
                string[] expArray = expression.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string operators = "+-*/";
                //遍历后缀表达式，如果是运算数则压栈，是运算符则把栈顶两个元素弹栈并按运算符和顺序进行运算
                //再把运算结果压栈，遍历完后，栈中仅剩的就是运算结果
                foreach (string item in expArray)
                {
                    if (!operators.Contains(item))//是运算数(没对运算数非数字处理)
                    {
                        double currentNum = Convert.ToDouble(item);
                        stack.Push(currentNum);
                    }
                    else//是运算符
                    {
                        //注意运算数的顺序
                        double latterNum = stack.Pop();
                        double preNum = stack.Pop();
                        switch (item)
                        {
                            case "+":
                                stack.Push(preNum + latterNum);
                                break;
                            case "-":
                                stack.Push(preNum - latterNum);
                                break;
                            case "*":
                                stack.Push(preNum * latterNum);
                                break;
                            case "/":
                                stack.Push(preNum / latterNum);
                                break;
                        }
                    }
                }
                result = stack.Pop();
            }
            return result;
        }
    }
}
