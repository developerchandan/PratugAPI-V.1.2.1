using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Prayug.Infrastructure.Extensions
{
    public static class CommonExtensions
    {
        #region Object extension methods
        /// <summary>
        /// Returns TRUE if an objects value is <cref="System.null" /> and FALSE if the object has been initiated.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNull(this object o)
        {
            return o == null;
        }
        public static bool IsNullOrValue(this int? value, int valueToCheck)
        {
            return (value ?? valueToCheck) == valueToCheck;
        }
        public static bool IsNullOrValue(this double? value, double valueToCheck)
        {
            return (value ?? valueToCheck) == valueToCheck;
        }
        public static bool IsNullOrValue(this decimal? value, decimal valueToCheck)
        {
            return (value ?? valueToCheck) == valueToCheck;
        }
        #endregion Object extension methods

        #region String extension methods
        public static string ToAddQuoteCommaStrings(this string stringData)
        {
            if (!string.IsNullOrEmpty(stringData))
            {
                //stringData.Replace(",", "','");
                return string.Join(",", stringData.Split(',').Select(x => string.Format("'{0}'", x)).ToList());
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToGetStringValue(this object objTest)
        {
            try
            {
                return (objTest != null) && (!object.ReferenceEquals(objTest, System.DBNull.Value)) ? objTest.ToString() : "";
            }
            catch (Exception)
            {
            }
            return "";
        }
        #endregion


        /// <summary>
        /// Replaces the format item in a specified System.String with the text 
        /// equivelent of the value from a corresponding System.Object reference in a specified array
        /// </summary>
        /// <param name="formatProvider">a composite format string</param>
        /// <param name="t">An System.Object to format</param>
        /// <exception cref="System.ArgumentNullException" />
        /// <exception cref="System.FormatException" />
        /// <returns>a compiled string</returns>
        public static string Combine(this string formatProvider, params object[] args)
        {
            return String.Format(formatProvider, args);
        }

        /// <summary>
        /// Executes a passed Regular Expression against a string and returns the result of the match
        /// </summary>
        /// <param name="s">the target string</param>
        /// <param name="RegEx">a System.String containing a regular expression</param>
        /// <returns>true if the regular expression matches the target string and false if it does not</returns>
        public static bool IsMatch(this string s, string RegEx)
        {
            return new Regex(RegEx).IsMatch(s);
        }

        /// <summary>
        /// Executes a passed Regular Expression against a string and returns the result of the match
        /// </summary>
        /// <param name="s">the target string</param>
        /// <param name="RegEx">any Regular Expression object</param>
        /// <returns>true if the regular expression matches the target string and false if it does not</returns>
        public static bool IsMatch(this string s, Regex RegEx)
        {
            return RegEx.IsMatch(s);
        }

        /// <summary>
        /// Checks to see if the string contains a valid email address.
        /// </summary>
        /// <param name="s">the target string</param>
        /// <returns>Returns TRUE if the string contains an email address and it is valid or FALSE if the string 
        /// has no email address or it is invalid</returns>
        public static bool IsValidEmail(this string s)
        {
            return s.IsMatch(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }

        /// <summary>
        /// Checks to see if the string is all numbers
        /// </summary>
        /// <param name="s">the target string</param>
        /// <returns>Returns TRUE if the string is all numbers or FALSE if the string has no numbers or is all alpha characters.</returns>
        public static bool IsInteger(this string s)
        {
            return !s.IsMatch("[^0-9-]") && s.IsMatch("^-[0-9]+$|^[0-9]+$");
        }

        /// <summary>
        /// Tests a string to see if it is a Positive Integer
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNaturalNumber(this string strNumber)
        {
            return !strNumber.IsMatch("[^0-9]") && strNumber.IsMatch("0*[1-9][0-9]*");
        }

        /// <summary>
        /// Tests a string to see if it is a Positive Integer with zero inclusive
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsWholeNumber(this string strNumber)
        {
            return !strNumber.IsMatch("[^0-9]");
        }

        /// <summary>
        /// Tests a string to see if it a Positive number both Integer & Real
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsPositiveNumber(this string strNumber)
        {
            return !strNumber.IsMatch("[^0-9.]") && strNumber.IsMatch("^[.][0-9]+$|[0-9]*[.]*[0-9]+$") && !strNumber.IsMatch("[0-9]*[.][0-9]*[.][0-9]*");
        }

        /// <summary>
        /// Tests a string to see if it is a number
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNumber(this string strNumber)
        {
            string strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            return !strNumber.IsMatch("[^0-9.-]") && !strNumber.IsMatch("[0-9]*[.][0-9]*[.][0-9]*") &&
                !strNumber.IsMatch("[0-9]*[-][0-9]*[-][0-9]*") && strNumber.IsMatch("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
        }

        /// <summary>
        /// Tests a string to see if it contains alpha characters
        /// </summary>
        /// <param name="strToCheck"></param>
        /// <returns></returns>
        public static bool IsAlpha(this string strToCheck)
        {
            return !strToCheck.IsMatch("[^a-zA-Z]");
        }
        /// <summary>
        ///  converts an object to string value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>string</returns>
        public static string ToString(object value)
        {
            string retValue = "";

            if (value != null && value != DBNull.Value)
            {
                retValue = value.ToString();
            }
            else
            {
                retValue = string.Empty;
            }

            return retValue;
        }
        /// <summary>
        /// Tests a string to see if it contains alpha and numeric characters
        /// </summary>
        /// <param name="strToCheck"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string strToCheck)
        {
            return !strToCheck.IsMatch("[^a-zA-Z0-9]");
        }

        /// <summary>
        /// Checks to see if a System.String value is a string representation of a boolean value.
        /// </summary>
        /// <param name="s">the target string</param>
        /// <returns>true if the System.String value is truthy or false if not</returns>
        public static bool IsBoolean(this string s)
        {
            string _lower = s.ToLower();
            return _lower.IsMatch("[true]|[false]|[0]|[1]");
        }


        /// <summary>
        /// Converts a System.String value to a boolean type.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Returns the boolean representation of the strings value or FALSE if the string is not a valid boolean.</returns>
        /// <exception cref="System.FormatException" />
        /// <remarks>Calls extension IsBoolean()</remarks>
        public static bool InterpretAsBoolean(this string s)
        {
            if (s.IsBoolean())
            {
                return Convert.ToBoolean(s);
            }
            return false;
        }

        /// <summary>
        /// Converts a System.String value to an integer type.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Returns the integer representation of the strings value or -1 if the string is not a valid integer.</returns>
        /// <exception cref="System.FormatException" />
        /// <remarks>Calls extension IsNumeric()</remarks>
        public static int InterpretAsInteger(this string s)
        {
            if (s.IsInteger())
            {
                return Convert.ToInt32(s);
            }
            return -1;
        }

        /// <summary>
        /// Converts a System.String to proper case.
        /// </summary>
        /// <param name="s">the target string</param>
        /// <exception cref="System.FormatException" />
        /// <returns>a properly cased System.String</returns>
        public static string Capitalize(this string s)
        {
            string result = String.Empty;
            foreach (string p in s.Split(' '))
            {
                if (p != String.Empty)
                    result += p.Substring(0, 1).ToUpper() + p.Substring(1) + " ";
            }
            return result;
        }
        public static int ToInteger(this bool value)
        {
            if (value == true)
                return 1;
            else
                return 0;
        }
        /// <summary>
        ///  converts an object to integer value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>int</returns>
        public static int ToInteger(this object value)
        {
            int retValue = 0;

            if (value != DBNull.Value)
            {
                int.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }
    }
}

