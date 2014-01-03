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


namespace dc
{
	using std = System.Collections;

	enum TokId
	{
		NUMBER,
		PLUS_OP,
        MINUS_OP,
		MUL_OP,
		EOS
	}
	class Token
	{
		public Token(TokId tokId, string text)
		{
			myId = tokId;
			myText = text;
		}
		public TokId Id 
		{
			get { return myId; }
		}
		public string Text
		{
			get { return myText; }
		}
		TokId myId;
		string myText;
	}

	class ExprLexer
	{
		public ExprLexer(string expr)
		{
			myIndex = 0;
			myExpr = expr;
			myCurrentToken = null;
		}

		public Token getNextToken()
		{
			myCurrentToken = doGetNextToken();
			return myCurrentToken;
		}

		public Token getCurrentToken()
		{
			if( myCurrentToken == null ) {
				myCurrentToken = getNextToken();
			}
			return myCurrentToken;
		}

		Token doGetNextToken()
		{
			if( myIndex >= myExpr.Length )
				return new Token(TokId.EOS, string.Empty);
			TokId currentTokenId = TokId.EOS;
			string currentTokenText = string.Empty;
			bool enteredToken = false, endOfToken = false;
			for( ; myIndex < myExpr.Length && !endOfToken; ++myIndex ) {
				switch( myExpr[myIndex] ) {
					case '+': 
						if( enteredToken ) {
							return new Token(currentTokenId, currentTokenText);
						} else {
							++myIndex;
							return new Token(TokId.PLUS_OP, "+");
						}
                    case '-':
                        if( enteredToken ) {
                            return new Token(currentTokenId, currentTokenText);
                        } else {
                            ++myIndex;
                            return new Token(TokId.MINUS_OP, "-");
                        }
					case '*':
						if( enteredToken ) {
							return new Token(currentTokenId, currentTokenText);
						} else {
							++myIndex;
							return new Token(TokId.MUL_OP, "*");
						}
					case '0': case '1': case '2': case '3': case '4':
					case '5': case '6': case '7': case '8': case '9':
						enteredToken = true;
						currentTokenId = TokId.NUMBER;
						currentTokenText += myExpr[myIndex];
						break;
					case ' ': {
							if( enteredToken ) { 
								endOfToken = true;
							} else {
								continue;
							}
						}
						break;
				}
			}
			return new Token(currentTokenId, currentTokenText);
		}

		int myIndex;
		string myExpr;
		Token myCurrentToken;
	}

	/// <summary>
	/// Summary description for ExprParser.
	/// </summary>
	class ExprParser
	{
		public ExprNode parseExpr(string expr)
		{
			myLexer = new ExprLexer(expr);
	
			return parseExpr();
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
						throw new System.Exception("Parse exception, unexpected token");
				}
			}
			return left;
		}

		ExprNode parseMulExpr()
		{
			ExprNode left = parseNumber();
			for(Token tok = myLexer.getCurrentToken(); tok.Id == TokId.MUL_OP; tok = myLexer.getCurrentToken()) {
				myLexer.getNextToken();
				ExprNode right = parseNumber();
				left = new MulExprNode(left, right);
			}
			return left;
		}

		ExprNode parseNumber()
		{
			Token tok = myLexer.getCurrentToken();
			if( tok.Id != TokId.NUMBER )
			{
				throw new System.Exception("Parse exception, a number was expected");
			}
			myLexer.getNextToken();
			return new NumberNode(tok.Text);
		}

		ExprLexer myLexer;
	}


}
