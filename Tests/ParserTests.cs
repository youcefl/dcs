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

namespace dc.Tests
{
	/// <summary>
	/// Summary description for ParserTest.
	/// </summary>
	[TestClass]
	public class ParserTests
	{
		public ParserTests()
		{
		}

        [TestMethod]
        void parseNumberTest()
        {
            ExprParser parser = new ExprParser();
            ExprNode exprAST = parser.parseExpr("127");

            Assert.isTrue(exprAST.GetType() == typeof(NumberNode));
            Assert.areEq(new Number("127"), (exprAST as NumberNode).Value);
        }

        [TestMethod]
        void parseAddExpr()
        {
            ExprParser parser = new ExprParser();
            ExprNode exprAST = parser.parseExpr("2+3");

            Assert.isTrue(exprAST.GetType() == typeof(AddExprNode));
            AddExprNode addExpr = exprAST as AddExprNode;
            Assert.isTrue(addExpr.LeftOperand as NumberNode != null);
            Assert.areEq(new Number("2"), (addExpr.LeftOperand as NumberNode).Value);
            Assert.isTrue(addExpr.RightOperand as NumberNode != null);
            Assert.areEq(new Number("3"), (addExpr.RightOperand as NumberNode).Value);
        }

        [TestMethod]
        void parseAddExprWithThreeTerms()
        {
            ExprParser parser = new ExprParser();
            ExprNode exprAST = parser.parseExpr("1 + 3 + 7");

            Assert.isOfType(exprAST, typeof(AddExprNode));
            AddExprNode add_7 = exprAST as AddExprNode;
            Assert.isOfType(add_7.LeftOperand, typeof(AddExprNode));
            Assert.isOfType(add_7.RightOperand, typeof(NumberNode));
            Assert.areEq(new Number("7"), (add_7.RightOperand as NumberNode).Value);
            AddExprNode add_3 = add_7.LeftOperand as AddExprNode;
            Assert.isOfType(add_3.LeftOperand, typeof(NumberNode));
            Assert.isOfType(add_3.RightOperand, typeof(NumberNode));
            Assert.areEq(new Number("1"), (add_3.LeftOperand as NumberNode).Value);
            Assert.areEq(new Number("3"), (add_3.RightOperand as NumberNode).Value);
        }

        [TestMethod]
        void parseSubExprWithThreeTerms()
        {
            ExprParser parser = new ExprParser();
            ExprNode exprAST = parser.parseExpr("1 - 3 - 7");

            Assert.isOfType(exprAST, typeof(SubExprNode));
            SubExprNode add_7 = exprAST as SubExprNode;
            Assert.isOfType(add_7.LeftOperand, typeof(SubExprNode));
            Assert.isOfType(add_7.RightOperand, typeof(NumberNode));
            Assert.areEq(new Number("7"), (add_7.RightOperand as NumberNode).Value);
            SubExprNode add_3 = add_7.LeftOperand as SubExprNode;
            Assert.isOfType(add_3.LeftOperand, typeof(NumberNode));
            Assert.isOfType(add_3.RightOperand, typeof(NumberNode));
            Assert.areEq(new Number("1"), (add_3.LeftOperand as NumberNode).Value);
            Assert.areEq(new Number("3"), (add_3.RightOperand as NumberNode).Value);
        }

        [TestMethod]
        void parseMulExpr()
        {
            ExprParser parser = new ExprParser();
            ExprNode exprAST = parser.parseExpr("2 * 3");

            Assert.isOfType(exprAST, typeof(MulExprNode));
            MulExprNode mulExpr = exprAST as MulExprNode;
            Assert.isTrue(mulExpr.LeftOperand as NumberNode != null);
            Assert.areEq(new Number("2"), (mulExpr.LeftOperand as NumberNode).Value);
            Assert.isTrue(mulExpr.RightOperand as NumberNode != null);
            Assert.areEq(new Number("3"), (mulExpr.RightOperand as NumberNode).Value);
        }

        [TestMethod]
        void parseDivExpr()
        {
            ExprParser parser = new ExprParser();
            ExprNode exprAST = parser.parseExpr("4 / 2");

            Assert.isOfType(exprAST, typeof(DivExprNode));
            DivExprNode divExpr = exprAST as DivExprNode;
            Assert.isTrue(divExpr.LeftOperand as NumberNode != null);
            Assert.areEq(new Number("4"), (divExpr.LeftOperand as NumberNode).Value);
            Assert.isTrue(divExpr.RightOperand as NumberNode != null);
            Assert.areEq(new Number("2"), (divExpr.RightOperand as NumberNode).Value);
        }

        [TestMethod]
        void parseAddExprWithThreeFactors()
        {
            ExprParser parser = new ExprParser();
            ExprNode exprAST = parser.parseExpr("3 * 5 * 7");

            Assert.isOfType(exprAST, typeof(MulExprNode));
            MulExprNode mul_7 = exprAST as MulExprNode;
            Assert.isOfType(mul_7.LeftOperand, typeof(MulExprNode));
            Assert.isOfType(mul_7.RightOperand, typeof(NumberNode));
            Assert.areEq(new Number("7"), (mul_7.RightOperand as NumberNode).Value);
            MulExprNode mul_5 = mul_7.LeftOperand as MulExprNode;
            Assert.isOfType(mul_5.LeftOperand, typeof(NumberNode));
            Assert.isOfType(mul_5.RightOperand, typeof(NumberNode));
            Assert.areEq(new Number("3"), (mul_5.LeftOperand as NumberNode).Value);
            Assert.areEq(new Number("5"), (mul_5.RightOperand as NumberNode).Value);
        }

        [TestMethod]
        void parseDivExprWithThreeFactors()
        {
            ExprParser parser = new ExprParser();
            ExprNode exprAST = parser.parseExpr("1024 / 256 / 2");

            Assert.isOfType(exprAST, typeof(DivExprNode));
            DivExprNode div_2 = exprAST as DivExprNode;
            Assert.isOfType(div_2.LeftOperand, typeof(DivExprNode));
            Assert.isOfType(div_2.RightOperand, typeof(NumberNode));
            Assert.areEq(new Number("2"), (div_2.RightOperand as NumberNode).Value);
            DivExprNode div_256 = div_2.LeftOperand as DivExprNode;
            Assert.isOfType(div_256.LeftOperand, typeof(NumberNode));
            Assert.isOfType(div_256.RightOperand, typeof(NumberNode));
            Assert.areEq(new Number("1024"), (div_256.LeftOperand as NumberNode).Value);
            Assert.areEq(new Number( "256"), (div_256.RightOperand as NumberNode).Value);
        }

        [TestMethod]
        void parseMulExprDivExpr()
        {
            ExprParser parser = new ExprParser();
            ExprNode exprAST = parser.parseExpr("6 * 8 / 2");

            Assert.isOfType(exprAST, typeof(DivExprNode));
            DivExprNode div_2 = exprAST as DivExprNode;
            Assert.isOfType(div_2.LeftOperand, typeof(MulExprNode));
            Assert.isOfType(div_2.RightOperand, typeof(NumberNode));
            Assert.areEq(new Number("2"), (div_2.RightOperand as NumberNode).Value);
            MulExprNode mul_8 = div_2.LeftOperand as MulExprNode;
            Assert.isOfType(mul_8.LeftOperand, typeof(NumberNode));
            Assert.isOfType(mul_8.RightOperand, typeof(NumberNode));
            Assert.areEq(new Number("6"), (mul_8.LeftOperand as NumberNode).Value);
            Assert.areEq(new Number("8"), (mul_8.RightOperand as NumberNode).Value);
        }

        [TestMethod]
        void parseAddAndMulExpr()
        {
            ExprParser parser = new ExprParser();
            ExprNode exprAST = parser.parseExpr("1 + 2 * 3");

            Assert.isOfType(exprAST, typeof(AddExprNode));
            AddExprNode addExpr = exprAST as AddExprNode;
            Assert.isOfType(addExpr.LeftOperand, typeof(NumberNode));
            Assert.isOfType(addExpr.RightOperand, typeof(MulExprNode));
            MulExprNode mulExpr = addExpr.RightOperand as MulExprNode;
            Assert.isTrue(mulExpr.LeftOperand as NumberNode != null);
            Assert.areEq(new Number("2"), (mulExpr.LeftOperand as NumberNode).Value);
            Assert.isTrue(mulExpr.RightOperand as NumberNode != null);
            Assert.areEq(new Number("3"), (mulExpr.RightOperand as NumberNode).Value);
        }
    }
}
