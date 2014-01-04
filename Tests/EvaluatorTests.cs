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
	/// Summary description for EvaluatorTests.
	/// </summary>
	[TestClass]
	public class EvaluatorTests
	{
		public EvaluatorTests()
		{
		}

        [TestMethod]
        void evaluteSingleNumber()
        {
            ExprEvaluator evaluator = new ExprEvaluator();
            Assert.areEq(new Number("127"), evaluator.evaluate(new NumberNode("127")));
        }

        [TestMethod]
        void evaluteAddExpression()
        {
            ExprEvaluator evaluator = new ExprEvaluator();
            Assert.areEq(new Number("127"), evaluator.evaluate(new AddExprNode(new NumberNode("97"), new NumberNode("30"))));
        }

        [TestMethod]
        void evaluteSubExpression()
        {
            ExprEvaluator evaluator = new ExprEvaluator();
            Assert.areEq(new Number("-2"), evaluator.evaluate(new SubExprNode(new NumberNode("29"), new NumberNode("31"))));
        }

        [TestMethod]
        void evaluteMulExpression()
        {
            ExprEvaluator evaluator = new ExprEvaluator();
            Assert.areEq(new Number("128"), evaluator.evaluate(new MulExprNode(new NumberNode("16"), new NumberNode("8"))));
        }

        [TestMethod]
        void evaluteAddAndMulExpression()
        {
            ExprEvaluator evaluator = new ExprEvaluator();
            Assert.areEq( new Number("7")
                        , evaluator.evaluate( new AddExprNode( new NumberNode("1")
                                                             , new MulExprNode( new NumberNode("2")
                                                                              , new NumberNode("3")
                                                                              )
                                                             ) 
                                            ) );
        }

        [TestMethod]
        void evaluteDivExpression()
        {
            ExprEvaluator evaluator = new ExprEvaluator();
            Assert.areEq(new Number("27"), evaluator.evaluate(new DivExprNode(new NumberNode("81"), new NumberNode("3"))));
        }
    }
}
