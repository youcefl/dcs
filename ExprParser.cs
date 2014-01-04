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


namespace dcs
{
	/// <summary>
	/// Summary description for ExprParser.
	/// </summary>
	class ExprParser
	{
		public ExprNode parseExpr(string expr)
		{
			myLexer = new ExprLexer(expr);
	
			return parseRootExpr();
		}

		ExprNode parseRootExpr()
		{
			ExprNode exprNode = parseExpr();
            if( myLexer.getCurrentToken().Id != TokId.EOS )
            {
                throw new ParseException(string.Format("Unexpected token `{0}' encountered", myLexer.getCurrentToken().ToString()));
            }
            return exprNode;
		}

        ExprNode parseExpr()
        {
            return parseAddExpr();
        }

		ExprNode parseAddExpr()
		{
			ExprNode left = parseMulExpr();
			for(Token tok = myLexer.getCurrentToken(); tok.Id != TokId.EOS; tok = myLexer.getCurrentToken()) {
				switch (tok.Id) {
					case TokId.PLUS_OP: {
						myLexer.getNextToken();
						ExprNode right = parseMulExpr();
						left = new AddExprNode(left, right);
                        break;
					}
                    case TokId.MINUS_OP: {
                        myLexer.getNextToken();
                        ExprNode right = parseMulExpr();
                        left = new SubExprNode(left, right);
                        break;
                    }
					default:
						return left;
				}
			}
			return left;
		}

		ExprNode parseMulExpr()
		{
			ExprNode left = parseTerm();
            bool endLoop = false;
			for(Token tok = myLexer.getCurrentToken(); !endLoop; tok = myLexer.getCurrentToken()) {
                switch( tok.Id ) {
                    case TokId.MUL_OP: {
                        myLexer.getNextToken();
                        ExprNode right = parseTerm();
                        left = new MulExprNode(left, right);
                        break;
                    }
                    case TokId.DIV_OP: {
                        myLexer.getNextToken();
                        ExprNode right = parseTerm();
                        left = new DivExprNode(left, right);
                        break;
                    }
                    default:
                        endLoop = true;
                        break;
                }
			}
            return left;
		}

        ExprNode parseTerm()
        {
            Token tok = myLexer.getCurrentToken();
            switch(tok.Id ) {
                case TokId.LPAR: {
                    myLexer.getNextToken();
                    ExprNode term = parseExpr();
                    Token rpar = myLexer.getCurrentToken();
                    if( rpar.Id != TokId.RPAR ) {
                        throw new ParseException(string.Format("Unexpected token `{0}' encountered", rpar.ToString()));
                    }
                    myLexer.getNextToken();
                    return term;
                }
                case TokId.NUMBER: return parseNumber();
                default:
                    throw new ParseException(string.Format("Unexpected token `{0}' encountered", tok.ToString()));
            }
        }

		ExprNode parseNumber()
		{
			Token tok = myLexer.getCurrentToken();
			if( tok.Id != TokId.NUMBER )
			{
				throw new ParseException("a number was expected");
			}
			myLexer.getNextToken();
			return new NumberNode(tok.Text);
		}

		ExprLexer myLexer;
	}


}
