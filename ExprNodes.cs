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
 * Creation date: 2013.07.10
 * What it is: Expression AST nodes (some where moved from ExprParser.cs)
 **/


namespace dc
{
	/// <summary>
	/// Abstract base class for all expression nodes.
	/// </summary>
	abstract class ExprNode 
	{
		public abstract void accept(ExprVisitor v);
	}

	class NumberNode : ExprNode
	{
		public NumberNode(string number)
		{
			myValue = new Number(number);
		}

		public Number Value 
		{
			get { return myValue; }
		}

		public override void accept(ExprVisitor v)
		{
			v.visitIn(this);
			v.visitOut(this);
		}

		Number myValue;
	}

	/// <summary>
	/// Abstract base class for binary expression nodes
	/// </summary>
	abstract class BinaryExprNode : ExprNode
	{
		protected BinaryExprNode(ExprNode leftOp, ExprNode rightOp)
		{
			myLeftOp = leftOp;
			myRightOp = rightOp;
		}

		public ExprNode LeftOperand 
		{
			get { return myLeftOp; }
		}
		public ExprNode RightOperand 
		{
			get { return myRightOp; }
		}

		ExprNode myLeftOp;
		ExprNode myRightOp;
	}

	/// <summary>
	/// Additive expression node
	/// </summary>
	class AddExprNode : BinaryExprNode
	{
		public AddExprNode(ExprNode leftOp, ExprNode rightOp)
			: base(leftOp, rightOp)
		{
		}

		public override void accept(ExprVisitor v)
		{
			v.visitIn(this);
			LeftOperand.accept(v);
			RightOperand.accept(v);
			v.visitOut(this);
		}
	}

    /// <summary>
    /// Subtraction expression node
    /// </summary>
    class SubExprNode : BinaryExprNode
    {
        public SubExprNode(ExprNode leftOp, ExprNode rightOp)
            : base(leftOp, rightOp)
        {
        }

        public override void accept(ExprVisitor v)
        {
            v.visitIn(this);
            LeftOperand.accept(v);
            RightOperand.accept(v);
            v.visitOut(this);
        }
    }

	/// <summary>
	/// Multiplicative expression node
	/// </summary>
	class MulExprNode : BinaryExprNode
	{
		public MulExprNode(ExprNode leftOp, ExprNode rightOp)
			: base(leftOp, rightOp)
		{
		}

		public override void accept(ExprVisitor v)
		{
			v.visitIn(this);
			LeftOperand.accept(v);
			RightOperand.accept(v);
			v.visitOut(this);
		}
	}

    /// <summary>
    /// Division expression node
    /// </summary>
    class DivExprNode : BinaryExprNode
    {
        public DivExprNode(ExprNode leftOp, ExprNode rightOp)
            : base(leftOp, rightOp)
        {
        }

        public override void accept(ExprVisitor v)
        {
            v.visitIn(this);
            LeftOperand.accept(v);
            RightOperand.accept(v);
            v.visitOut(this);
        }
    }
}
