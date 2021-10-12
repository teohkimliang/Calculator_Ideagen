using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator__Ideagen
{
    class Program
    {
        static void Main(string[] args)
        {
            //string sumString = "( 2 + 4 ) +  4 - 4";
            //string sumString = "54 + ( 19 - ( 2 * 5 ) )";
            //string sumString = "(  3 * ( 3 - 9 ) - 9 ) / 2";
            //string sumString = "1 + 2";
            //string sumString = "2 * 2";
            //string sumString = "1 + 2 + 3";
            //string sumString = "6 / 2";
            //string sumString = "11 + 23";
            //string sumString = "11.1 + 23";
            //string sumString = "1 + 2 * 3 * 3 + 3";
            //string sumString = "3 * 3 * 3";
            //string sumString = "11.1 + 23 + 3 / 3 * 3 + 1";
            //string sumString = "( 1 + 2 ) + 5 * 2";
            //string sumString = "( 11.5 + 15.4 ) + 10.1 + 2 * 5";
            //string sumString = " 23 - ( 29.3 - 12.5 )";
            //string sumString = "( 1 / 2 ) - 1 + 1";
            //string sumString = "10 - ( 2 + 3 * ( 7 - 5 ) ) ";
            //string sumString = "10 - ( 2 + 3 * ( 7 - 5 ) ) + 2 * 3 ";
            while (true)
            {
                Console.WriteLine("Enter the Sum String");
                string sumString = Console.ReadLine();
                double SumResult = Calculator(sumString);
                Console.WriteLine("\nYour Result is : " + SumResult.ToString());
            }
            //double SumResult = Calculator(sumString);
            //Console.WriteLine("Input :"+ sumString + "\nYour Result is : " + SumResult.ToString());
            //Console.ReadLine();
        }
        public static double Calculator(string sum)
        {
            Stack<double> NumberStack = new Stack<double>();
            Stack<string> OperatorStack = new Stack<string>();
            ArrayList ArrOperator = new ArrayList();
            ArrayList ArrNumber = new ArrayList();
            //Base on the question mentioned all the number (number = all will be linked together eg 12, 73),operator,bracket will be split by space
            string[] splitBySpace = sum.Split(' ');
            double result = 0;
            //Used to split out the the number and operation function (Start)
            for (int i = 0; i < splitBySpace.Length; i++)
            {
                if (splitBySpace[i] == "+" || splitBySpace[i] == "-" || splitBySpace[i] == "*" || splitBySpace[i] == "/" || splitBySpace[i] == "(" || splitBySpace[i] == ")")
                {
                    ArrOperator.Add(splitBySpace[i]);
                }
                else if (double.TryParse(splitBySpace[i], out result))
                {
                    if (result != 0)
                    {
                        ArrNumber.Add(result);
                    }
                }
            }
            //Used to split out the the number and operation function (End)

            //**Assumption here the number and the operator will never be same
            if (ArrNumber.Count > ArrOperator.Count)
            {
                result = CalculatorRecBaseOnNumber(ArrOperator, ArrNumber, 0);
            }
            else
            {
                result = CalculatorRecBaseOnOperator(ArrOperator, ArrNumber, 0, 0 , 0);
            }
            return result;
        }
        public static double CalculatorRecBaseOnNumber(ArrayList ArrOperator, ArrayList ArrNumber, int pointer)
        {
            // ** Assumption the operator must be less than the number (not including bracket) therefore this function will not handle any bracket
            // this will base on if the operator arr less than the number arr 
            double curValue = 0;
            double prevValue = 0;
            Stack<double> NumberStack = new Stack<double>();
            string prevOperator = "+";
            string curOperator = "";
            double tempvalue = 0;
            double finalTotalSum = 0;
            for (int Pointer = pointer; Pointer < ArrNumber.Count; Pointer++)
            {
                //Base on previous operation (default = 0) ;
                //Number > Operation so Operation might hit the index exceed
                curValue = (double)ArrNumber[Pointer];
                curOperator = Pointer < ArrOperator.Count ? (string)ArrOperator[Pointer] : "";
                switch (prevOperator)
                {
                    case "+":
                        NumberStack.Push(curValue);
                        break;
                    case "-":
                        NumberStack.Push(curValue * -1);
                        break;
                    case "*":
                        tempvalue = NumberStack.Pop();
                        tempvalue *= curValue;
                        NumberStack.Push(tempvalue);
                        break;
                    case "/":
                        tempvalue = NumberStack.Pop();
                        tempvalue /= curValue;
                        NumberStack.Push(tempvalue);
                        break;
                }
                if (curOperator == "")
                {
                    //exit
                    break;
                }
                prevValue = curValue;
                prevOperator = curOperator;

            }


            //in the end, it will add up all number to get. therefore if minus you change the number to negative instant of direct deduction. sum up all the stack
            while (NumberStack.Count != 0)
            {
                finalTotalSum += NumberStack.Pop();
            }
            return finalTotalSum;

        }
        public static double CalculatorRecBaseOnOperator(ArrayList ArrOperator, ArrayList ArrNumber, int pointer, int isRecursive,int nestedValue)
        {
            double curValue = 0;
            double prevValue = 0;
            Stack<double> NumberStack = new Stack<double>();
            string prevOperator = "+";
            string curOperator = "";
            double tempvalue = 0;
            double CalculatorRecSum = 0;
            double finalTotalSum = 0;
            int Brackcount = 0;
            int _nestedValue = nestedValue;
            int _isRecursive = isRecursive;
            for (int Pointer = pointer; Pointer <= ArrOperator.Count; Pointer++)
            {
                if(_isRecursive == 0)
                {
                    //the bracketcount here is put for incase handling previous brack had been calculator but at the back number might also need to align operator arr with the number arr;
                    Brackcount = CountPrevBracket(Pointer, ArrOperator);
                    curValue = Pointer - Brackcount < ArrNumber.Count ? (double)ArrNumber[Pointer - Brackcount] : 0;
                    curOperator = Pointer < ArrOperator.Count ? (string)ArrOperator[Pointer] : "";
                }
                else
                {
                    // the operator keyword in comment mean that (+ , - , * , /)
                    //if that inside recursive mean, it has the bracket and inside the bracket. therefore the operator will not effect but since got bracket need align with the operator.
                    Brackcount = CountPrevBracket(Pointer, ArrOperator);
                    curValue = Pointer - nestedValue < ArrNumber.Count ? (double)ArrNumber[Pointer - nestedValue] : 0;
                    curOperator = Pointer < ArrOperator.Count ? (string)ArrOperator[Pointer] : "";
                }
                if (curOperator == "(") // 
                {
                    //Get the Closing bracket for exiting the loop
                    int CloseBrackPos = GetClosingBracketPos(Pointer, ArrOperator);
                    //move the "(" to next operator
                    Pointer = Pointer + 1;
                    //use for revert back the curValue because the value and the operator split into different array therefore, this _nestedvalue for align the operator position and the number possition;
                    _nestedValue = _nestedValue + 1;
                    //Recursive and call itself enter bracket and process bracket return total value from the bracket;
                    CalculatorRecSum = CalculatorRecBaseOnOperator(ArrOperator, ArrNumber, Pointer, 1, _nestedValue);
                    //Pointer change to close;
                    Pointer = CloseBrackPos;

                    //10 + (12 + 2)
                    //suppose prev will be locate on the first +
                    //curOperator = Pointer < ArrOperator.Count ? (string)ArrOperator[Pointer] : "";

                    //direct insert into stack 
                    switch (prevOperator)
                    {
                        case "+":
                            NumberStack.Push(CalculatorRecSum);
                            break;
                        case "-":
                            NumberStack.Push(CalculatorRecSum * -1);
                            break;
                        case "*":
                            tempvalue = NumberStack.Pop();
                            tempvalue *= CalculatorRecSum;
                            NumberStack.Push(tempvalue);
                            break;
                        case "/":
                            tempvalue = NumberStack.Pop();
                            tempvalue /= CalculatorRecSum;
                            NumberStack.Push(tempvalue);
                            break;
                    }
                    prevOperator = (string)ArrOperator[Pointer];
                    //suppose previous will be the closing bracket;
                }
                else
                {
                    //base on previous operator then perform the first operation will alway be positive; 
                    switch (prevOperator)
                    {
                        case "+":
                            NumberStack.Push(curValue);
                            break;
                        case "-":
                            NumberStack.Push(curValue * -1);
                            break;
                        case "*":
                            tempvalue = NumberStack.Pop();
                            tempvalue *= curValue;
                            NumberStack.Push(tempvalue);
                            break;
                        case "/":
                            tempvalue = NumberStack.Pop();
                            tempvalue /= curValue;
                            NumberStack.Push(tempvalue);
                            break;
                    }
                    if (curOperator == ")")
                    {
                        // if current is the close bracket will be break the look
                        break;
                    }
                    //update the current operation for next processes
                    prevValue = curValue;
                    prevOperator = curOperator;
                }
            }

            //in the end, it will add up all number to get. therefore if minus you change the number to negative instant of direct deduction. sum up all the stack
            while (NumberStack.Count != 0)
            {
                finalTotalSum += NumberStack.Pop();
            }
            return finalTotalSum;

        }
        
        public static int GetClosingBracketPos(int OpenBracketPos, ArrayList ArrOperator)
        {
            int closeBracketPos = 0;
            Stack pos = new Stack();
            //loop though and use it for find the closing bracket pos 
            for (int startbracketidx = OpenBracketPos; startbracketidx < ArrOperator.Count; startbracketidx++)
            {
                string bracket = (string)ArrOperator[startbracketidx];
                if (bracket == "(")
                {
                    pos.Push(startbracketidx);//push into stack first
                }
                else if (bracket == ")")
                {
                    pos.Pop();
                    if (pos.Count == 0)
                    {
                        closeBracketPos = startbracketidx;
                        break;
                    }
                }
            }
            return closeBracketPos;
        }
        public static int CountPrevBracket(int Position, ArrayList ArrOperation)
        {
            //calculate the operator;
            int count = 0;
            for (int i = 0; i < Position; i++)
            {
                if ((string)ArrOperation[i] == "(" || (string)ArrOperation[i] == ")")
                {
                    count++;
                }
            }
            return count;
        }
    }
}
