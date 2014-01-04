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
 * Creation date: 2013.07.11
 **/

namespace dcs.Tests
{
	/// <summary>
	/// Summary description for Tests.
	/// </summary>
	class Tests
	{
        /// <summary>
        /// Searches for classes having the TestClassAttribute attribute
        /// and invokes the methods having attribute TestMethodAttribute
        /// in found classes
        /// </summary>
        public static int run()
        {
            int ret = 0;
            foreach(System.Type t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes())
            {
                if( t.GetCustomAttributes(typeof(TestClassAttribute), false).Length == 0 ) {
                    continue;
                }
                System.Console.Out.WriteLine("Running tests in {0}...", t.FullName);
                foreach( System.Reflection.MethodInfo m in t.GetMethods( System.Reflection.BindingFlags.DeclaredOnly
                                                                       | System.Reflection.BindingFlags.Public
                                                                       | System.Reflection.BindingFlags.NonPublic
                                                                       | System.Reflection.BindingFlags.Instance
                                                                       ) )
                {
                    if( m.GetCustomAttributes(typeof(TestMethodAttribute), false).Length == 0 ) {
                        continue;
                    }
                    System.Reflection.ConstructorInfo ctor = t.GetConstructor(new System.Type[]{});
                    object testClassInstance = ctor.Invoke(new object[]{});
                    int methodNameLength = m.Name.Length;
                    try {
                        System.Console.Out.Write("-> {0}", m.Name);
                        m.Invoke(testClassInstance, new object[]{});
                        printTestResult(true, methodNameLength);
                    } catch (System.Reflection.TargetInvocationException ex) {
                        printTestResult(false, methodNameLength);
                        System.Console.Error.WriteLine("   Test `{0}' failed: {1}", m.Name, ex.InnerException.Message);
                        ret += 1;
                    }
                }
            }
            return ret;
        }

        static void printTestResult(bool isTestSuccessful, int methodNameLength)
        {
            while(methodNameLength++ < 45) {
                System.Console.Write(' ');
            }
            System.Console.WriteLine(isTestSuccessful ? "OK" : "FAIL");
        }
	}

    /// <summary>
    /// Attribute to be put on test classes
    /// </summary>
    class TestClassAttribute : System.Attribute
    {
    }

    /// <summary>
    /// Attribute to be put on test methods
    /// </summary>
    class TestMethodAttribute : System.Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    class AssertionFailedException : System.Exception
    {
        public AssertionFailedException(string message)
            : base(message)
        {
        }
    }

    delegate void TestMethod();

    /// <summary>
    /// Assertions class
    /// </summary>
    class Assert
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void areEq(object a, object b)
        {
            if( !a.Equals(b) ) {
                throw new AssertionFailedException("Equality assertion failed.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cond"></param>
        public static void isTrue(bool cond)
        {
            if( !cond ) {
                throw new AssertionFailedException("Assertion failed.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public static void isOfType(object value, object type)
        {
            if( value.GetType() != type ) {
                throw new AssertionFailedException( string.Format( "Object has not the expected type, expected type: `{0}', actual type: `{1}'"
                                                                 , type, value.GetType() )
                                                  );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="type"></param>
        public static void throws(TestMethod m, object type)
        {
            try {
                m();
            } catch(System.Exception e) {
                if(type != e.GetType()) {
                    throw new AssertionFailedException(string.Format("An exception of type `{0}' was thrown whereas an exception of type `{1}' was expected", e.GetType(), type));
                }
                return;
            }
            throw new AssertionFailedException("Expected exception not thrown");
        }
    }
}
