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
using std = System.Collections;

namespace dc
{
	/// <summary>
	/// Summary description for ExprEvaluator.
	/// </summary>
	class ExprEvaluator
	{
		public ExprEvaluator()
		{
		}

		public Number evaluate(ExprNode expr)
		{
			ExprEvaluatorImpl evaluator = new ExprEvaluatorImpl();
			expr.accept(evaluator);
			return evaluator.Value;
		}

		class ExprEvaluatorImpl : ExprVisitor
		{
			public ExprEvaluatorImpl()
			{
				myValueStack = new std.Stack();
			}

			public override void visitOut(NumberNode n)
			{
				myValueStack.Push(n.Value);
			}

			public override void visitOut(AddExprNode n)
			{
				Number e2 = (Number)myValueStack.Pop();
				Number e1 = (Number)myValueStack.Pop();
				myValueStack.Push(e1 + e2);
			}

            public override void visitOut(SubExprNode n)
            {
                Number e2 = (Number)myValueStack.Pop();
                Number e1 = (Number)myValueStack.Pop();
                myValueStack.Push(e1 - e2);
            }

            public override void visitOut(MulExprNode n)
            {
                Number e2 = (Number)myValueStack.Pop();
                Number e1 = (Number)myValueStack.Pop();
                myValueStack.Push(e1 * e2);
            }

            public override void visitOut(DivExprNode n)
            {
                Number e2 = (Number)myValueStack.Pop();
                Number e1 = (Number)myValueStack.Pop();
                myValueStack.Push(e1 / e2);
            }

			public Number Value 
			{
				get { return (Number)myValueStack.Peek(); }
			}

			std.Stack  myValueStack;
		}
	}
}
