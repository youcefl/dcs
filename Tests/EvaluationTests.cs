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
 * Creation date: 2014.01.07
 **/


namespace dcs.Tests
{
	/// <summary>
	/// Summary description for EvaluationTests.
	/// </summary>
    [TestClass]
    public class EvaluationTests
	{
        [TestMethod]
        void evaluateExpr001()
        {
            Assert.areEq(new Number("27"), evaluate("1+2*3+4*5"));
        }

        [TestMethod]
        void evaluateExpr002()
        {
            Assert.areEq(new Number("47"), evaluate("1+2*(3+4*5)"));
        }

        [TestMethod]
        void evaluateExpr003()
        {
            Assert.areEq(new Number("3628800"), evaluate("2*3*4*5*6*7*8*9*10"));
        }

        [TestMethod]
        void evaluateExpr004()
        {
            Assert.areEq(new Number("16"), evaluate("1+2*3*4/2+3"));
        }

        [TestMethod]
        void evaluateExpr005()
        {
            Assert.areEq(new Number("231"), evaluate("1+2+3+4+5+6+7+8+9+10+11+12+13+14+15+16+17+18+19+20+21"));
        }

        [TestMethod]
        void evaluateExpr006()
        {
            Assert.areEq(new Number("1"), evaluate("3*2*4/2/3/2/2"));
        }


        Number evaluate(string expression)
        {
			ExprParser parser = new ExprParser();
			ExprNode exprAST = parser.parseExpr(expression);

			ExprEvaluator evaluator = new ExprEvaluator();
			return evaluator.evaluate(exprAST);
        }
	}
}
