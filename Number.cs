/*
* The MIT License (MIT)
*
* Copyright (c) 2014 Youcef Lemsafer
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of
* this software and associated documentation files (the "Software"), to deal in
* the Software without restriction, including without limitation the rights to
* use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
* the Software, and to permit persons to whom the Software is furnished to do so,
* subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
* FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
* COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
* IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
* CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
/**
 * Creator: Youcef Lemsafer
 * Creation date: 2013.06.25
 **/
using System;

namespace dcs
{
	/// <summary>
	/// Summary description for Number.
	/// </summary>
	class Number
	{
		public Number(string number)
		{
			myValue = int.Parse(number);
		}

		Number(int val)
		{
			myValue = val;
		}
        public static Number operator +(Number a, Number b)
        {
            return new Number(a.myValue + b.myValue);
        }
        public static Number operator -(Number a, Number b)
        {
            return new Number(a.myValue - b.myValue);
        }
        public static Number operator *(Number a, Number b)
        {
            return new Number(a.myValue * b.myValue);
        }
        public static Number operator /(Number a, Number b)
        {
            return new Number(a.myValue / b.myValue);
        }

		public override string ToString()
		{
			return myValue.ToString();
		}

        public override bool Equals(object obj)
        {
            return (obj.GetType() == typeof(Number))
                    && ((obj as Number).myValue == myValue);
        }

        public override int GetHashCode()
        {
            return myValue.GetHashCode ();
        }

		int myValue;
	}
}
