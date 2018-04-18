﻿#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using Noesis;
#else
using System.Windows.Data;
#endif
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#endregion

namespace Assets.Scripts.Converters
{
    // Does a math equation on the bound value.
    // Use @VALUE in your mathEquation as a substitute for bound value
    // Operator order is parenthesis first, then Left-To-Right (no operator precedence)
    public class MathConverter : IValueConverter
    {
        #region Fields

        #region Static Fields and Constants

        private static readonly char[] AllOperators = {'+', '-', '*', '/', '%', '(', ')'};
        private static readonly List<string> Grouping = new List<string> {"(", ")"};
        private static readonly List<string> Operators = new List<string> {"+", "-", "*", "/", "%"};

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        // Evaluates a mathematical string and keeps track of the results in a List<double> of numbers
        private void EvaluateMathString(ref string mathEquation, ref List<double> numbers, int index)
        {
            // Loop through each mathemtaical token in the equation
            string token = GetNextToken(mathEquation);
            while(token != string.Empty)
            {
                // Remove token from mathEquation
                mathEquation = mathEquation.Remove(0, token.Length);

                // If token is a grouping character, it affects program flow
                if(Grouping.Contains(token))
                    switch(token)
                    {
                        case "(":
                            EvaluateMathString(ref mathEquation, ref numbers, index);
                            break;
                        case ")":
                            return;
                    }

                // If token is an operator, do requested operation
                if(Operators.Contains(token))
                {
                    // If next token after operator is a parenthesis, call method recursively
                    string nextToken = GetNextToken(mathEquation);
                    if(nextToken == "(")
                        EvaluateMathString(ref mathEquation, ref numbers, index + 1);

                    // Verify that enough numbers exist in the List<double> to complete the operation
                    // and that the next token is either the number expected, or it was a ( meaning
                    // that this was called recursively and that the number changed
                    if(numbers.Count > index + 1 && (Math.Abs(double.Parse(nextToken) - numbers[index + 1]) < double.Epsilon || nextToken == "("))
                    {
                        switch(token)
                        {
                            case "+":
                                numbers[index] = numbers[index] + numbers[index + 1];
                                break;
                            case "-":
                                numbers[index] = numbers[index] - numbers[index + 1];
                                break;
                            case "*":
                                numbers[index] = numbers[index] * numbers[index + 1];
                                break;
                            case "/":
                                numbers[index] = numbers[index] / numbers[index + 1];
                                break;
                            case "%":
                                numbers[index] = numbers[index] % numbers[index + 1];
                                break;
                        }
                        numbers.RemoveAt(index + 1);
                    }
                    else
                    {
                        // Handle Error - Next token is not the expected number
                        throw new FormatException("Next token is not the expected number");
                    }
                }
                token = GetNextToken(mathEquation);
            }
        }

        // Gets the next mathematical token in the equation
        private string GetNextToken(string mathEquation)
        {
            // If we're at the end of the equation, return string.empty
            if(mathEquation == string.Empty)
                return string.Empty;

            // Get next operator or numeric value in equation and return it
            var tmp = "";
            foreach(char c in mathEquation)
            {
                if(AllOperators.Contains(c))
                    return tmp == "" ? c.ToString() : tmp;
                tmp += c;
            }
            return tmp;
        }

        #endregion

        #endregion

        #region Interface Implementations

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Parse value into equation and remove spaces
            var mathEquation = parameter as string;
            if(mathEquation == null)
                return null;
            mathEquation = mathEquation.Replace(" ", "");
            mathEquation = mathEquation.Replace("@VALUE", value.ToString());

            // Validate values and get list of numbers in equation
            var numbers = new List<double>();
            double tmp;
            foreach(string s in mathEquation.Split(AllOperators))
            {
                if(s != string.Empty)
                    if(double.TryParse(s, out tmp))
                        numbers.Add(tmp);
                    else
                        throw new InvalidCastException();
            }

            // Begin parsing method
            EvaluateMathString(ref mathEquation, ref numbers, 0);

            // After parsing the numbers list should only have one value - the total
            return numbers[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}