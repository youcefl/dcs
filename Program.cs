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
 * Creation date: 2013.06.24 
 **/
using System;

namespace dcs
{
	/// <summary>
	/// Summary description for Program.
	/// </summary>
	class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			if( args.Length == 0 ) {
				System.Console.WriteLine("Command line expression evaluator.");
				string help =   "Usage: dc [options] expression\n"
				              + "Available options:\n"
							  + "   -v verbose output, prints the tree built for the expression before its evaluation.\n"
                              ;
				System.Console.Write(help);

                return 0;
			}

            if( (args.Length == 1) && (args[0] == "-t") )
            {
                return Tests.Tests.run();
            }

            bool isVerbose = false;
            string expression = string.Empty;
            for( int i = 0; i < args.Length; ++i ) {
                if( args[i].StartsWith("-") ) {
                    if( args[i] == "-v" ) {
                        isVerbose = true;
                    } else {
                        System.Console.Error.WriteLine("Unknown option: {0}", args[i]);
                        return 1;
                    }
                } else {
                    expression = args[i];
                }
            }

            try {
			    ExprParser parser = new ExprParser();
			    ExprNode exprAST = parser.parseExpr(expression);

                if( isVerbose ) {
                    ExprPrinter printer = new ExprPrinter();
                    printer.printExpr(exprAST, System.Console.Out);
                }
			    ExprEvaluator evaluator = new ExprEvaluator();
			    Number result = evaluator.evaluate(exprAST);
			    System.Console.WriteLine(result.ToString());
            } catch (Exception e) {
                System.Console.Error.WriteLine(e.Message);
            }

			return 0;
		}
	}
}
