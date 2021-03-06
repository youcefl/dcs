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


namespace dcs
{
	/// <summary>
	/// Summary description for ExprPrinter.
	/// </summary>
	class ExprPrinter
	{
		public ExprPrinter()
		{
		}

		public void printExpr(ExprNode expr, System.IO.TextWriter writer)
		{
			ExprPrinterImpl thePrinter = new ExprPrinterImpl(writer);
			expr.accept(thePrinter);
		}

		class ExprPrinterImpl : ExprVisitor
		{
			public ExprPrinterImpl(System.IO.TextWriter writer)
			{
				myTabs = string.Empty;
				myWriter = writer;
			}

			public override void visitOut(NumberNode n)
			{
				myWriter.WriteLine("{0}<Number value=\"{1}\"/>", myTabs, n.Value);
			}

			public override void visitIn(AddExprNode n)
			{
				myWriter.WriteLine("{0}<Add>", myTabs);
				indent();
			}
			public override void visitOut(AddExprNode n)
			{
                unIndent();
                myWriter.WriteLine("{0}</Add>", myTabs);
			}

            public override void visitIn(SubExprNode n)
            {
                myWriter.WriteLine("{0}<Sub>", myTabs);
                indent();
            }
            public override void visitOut(SubExprNode n)
            {
                unIndent();
                myWriter.WriteLine("{0}</Sub>", myTabs);
            }

            public override void visitIn(MulExprNode n)
            {
                myWriter.WriteLine("{0}<Mul>", myTabs);
                indent();
            }
            public override void visitOut(MulExprNode n)
            {
                unIndent();
                myWriter.WriteLine("{0}</Mul>", myTabs);
            }
            public override void visitIn(DivExprNode n)
            {
                myWriter.WriteLine("{0}<Div>", myTabs);
                indent();
            }
            public override void visitOut(DivExprNode n)
            {
                unIndent();
                myWriter.WriteLine("{0}</Div>", myTabs);
            }
            public override void visitIn(PowExprNode n)
            {
                myWriter.WriteLine("{0}<Pow>", myTabs);
                indent();
            }
            public override void visitOut(PowExprNode n)
            {
                unIndent();
                myWriter.WriteLine("{0}</Pow>", myTabs);
            }

			void indent()
			{
				myTabs += "  ";
			}
			void unIndent()
			{
				if(myTabs.Length >= 2) {
					myTabs = myTabs.Remove(0, 2);
				}
			}

			string myTabs;
			System.IO.TextWriter myWriter;
		}
	}
}
