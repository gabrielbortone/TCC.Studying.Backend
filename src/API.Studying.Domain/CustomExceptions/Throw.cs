using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Studying.Domain.CustomExceptions
{
    public static class Throw
    {
        /// <summary>
        /// Throw ArgumentNullException if value is null
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="paramName">Param name</param>
        /// <param name="ex">Custom Exception</param>
        public static void IfIsNull(object value, string paramName = "", Exception ex = null)
        {
            if (value == null)
                if (ex == null)
                    throw new ArgumentNullException(paramName);
                else
                    throw ex;
        }

        /// <summary>
        /// Throw ArgumentOutOfRangeException if value is Equals to Zero
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="paramName">Param name</param>
        /// <param name="ex">Custom Exception</param>
        public static void IfEqZero(long value, string paramName = "", Exception ex = null)
        {
            if (value == 0)
                if (ex == null)
                    throw new ArgumentOutOfRangeException(paramName);
                else
                    throw ex;
        }

        /// <summary>
        /// Throw ArgumentOutOfRangeException if value is Less Than Zero 
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="paramName">Param name</param>
        /// <param name="ex">Custom Exception</param>
        public static void IfLessThanZero(long value, string paramName = "", Exception ex = null)
        {
            if (value < 0)
                if (ex == null)
                    throw new ArgumentOutOfRangeException(paramName);
                else
                    throw ex;
        }

        /// <summary>
        /// Throw ArgumentOutOfRangeException if value is Less Than or Equals to Zero 
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="paramName">Param name</param>
        /// <param name="ex">Custom Exception</param>
        public static void IfLessThanOrEqZero(long value, string paramName = "", Exception ex = null)
        {
            if (value <= 0)
                if (ex == null)
                    throw new ArgumentOutOfRangeException(paramName);
                else
                    throw ex;
        }


        /// <summary>
        /// Throw ArgumentNullException if value is Null or Empty
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="paramName">Param name</param>
        /// <param name="ex">Custom Exception</param>
        public static void IfIsNullOrEmpty(string value, string paramName = "", Exception ex = null)
        {
            if (string.IsNullOrEmpty(value))
                if (ex == null)
                    throw new ArgumentNullException(paramName);
                else
                    throw ex;
        }

        /// <summary>
        /// Throw ArgumentNullException if value is Null or White Space
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="paramName">Param name</param>
        /// <param name="ex">Custom Exception</param>
        public static void IfIsNullOrWhiteSpace(string value, string paramName = "", Exception ex = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                if (ex == null)
                    throw new ArgumentNullException(paramName);
                else
                    throw ex;
        }

        /// <summary>
        /// Throw ArgumentOutOfRangeException if value is Null or Empty. 
        /// Throw ArgumentOutOfRangeException if value length is Bigger Than length value parameter.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="length">Length</param>
        /// <param name="paramName">Param name</param>
        /// <param name="ex">Custom Exception</param>
        public static void IfStringLengthBiggerThan(string value, long length, string paramName = "", Exception ex = null)
        {
            IfIsNullOrEmpty(value, paramName);

            if (value.Length > length)
                if (ex == null)
                    throw new ArgumentOutOfRangeException(paramName);
                else
                    throw ex;
        }

        /// <summary>
        /// Throw ArgumentOutOfRangeException if value list items count is Equals to Zero
        /// </summary>
        /// <typeparam name="T">List Type</typeparam>
        /// <param name="value">Value</param>
        /// <param name="paramName">Param name</param>
        /// <param name="ex">Custom Exception</param>
        public static void IfIsEmpty<T>(IEnumerable<T> value, string paramName = "", Exception ex = null)
        {
            if (value == null || (value != null) && value.Any())
                if (ex == null)
                    throw new ArgumentOutOfRangeException(paramName);
                else
                    throw ex;
        }

        /// <summary>
        /// Throw InvalidOperationException if value is False
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="paramName">Param name</param>
        /// <param name="ex">Custom Exception</param>
        public static void IfIsFalse(bool value, string paramName = "", Exception ex = null)
        {
            if (!value)
                if (ex == null)
                    throw new InvalidOperationException(paramName);
                else
                    throw ex;
        }

        /// <summary>
        /// Throw InvalidOperationException if value is True
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="paramName">Param name</param>
        /// <param name="ex">Custom Exception</param>
        public static void IfIsTrue(bool value, string paramName = "", Exception ex = null)
        {
            if (value)
                if (ex == null)
                    throw new InvalidOperationException(paramName);
                else
                    throw ex;
        }
    }
}
