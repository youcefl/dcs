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
        DIV_OP,
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
            myEnteredToken = false;
            myCurrentTokenId = TokId.EOS;
            myCurrentTokenText = string.Empty;
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

        Token buildRecognizedToken(TokId tokenId, string tokenText)
        {
			if( myEnteredToken ) {
				return new Token(myCurrentTokenId, myCurrentTokenText);
			}
			++myIndex;
			return new Token(tokenId, tokenText);
        }

		Token doGetNextToken()
		{
			if( myIndex >= myExpr.Length )
				return new Token(TokId.EOS, string.Empty);
			myCurrentTokenId = TokId.EOS;
			myCurrentTokenText = string.Empty;
			myEnteredToken = false;

            for( ; myIndex < myExpr.Length; ++myIndex ) {
				switch( myExpr[myIndex] ) {
					case '+': return buildRecognizedToken(TokId.PLUS_OP,  "+");
                    case '-': return buildRecognizedToken(TokId.MINUS_OP, "-");
					case '*': return buildRecognizedToken(TokId.MUL_OP,   "*");
					case '/': return buildRecognizedToken(TokId.DIV_OP,   "/");
                    case '0': case '1': case '2': case '3': case '4':
                    case '5': case '6': case '7': case '8': case '9': {
                        myEnteredToken = true;
                        myCurrentTokenId = TokId.NUMBER;
                        myCurrentTokenText += myExpr[myIndex];
                        break;
                    }
					case ' ': {
						if( myEnteredToken ) { 
							myEnteredToken = true;
						} else {
							continue;
						}
                        break;
					}
                    default:
                        throw new ParseException(string.Format("character `{0}' was unexpected.", myExpr[myIndex]));
				}
			}
			return new Token(myCurrentTokenId, myCurrentTokenText);
		}

		int myIndex;
		string myExpr;
		Token myCurrentToken;
        bool myEnteredToken;
        TokId myCurrentTokenId;
        string myCurrentTokenText;
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
						throw new ParseException(string.Format("unexpected token `{0}'", tok.ToString()));
				}
			}
			return left;
		}

		ExprNode parseMulExpr()
		{
			ExprNode left = parseNumber();
            bool endLoop = false;
			for(Token tok = myLexer.getCurrentToken(); !endLoop; tok = myLexer.getCurrentToken()) {
                switch( tok.Id ) {
                    case TokId.MUL_OP: {
                        myLexer.getNextToken();
                        ExprNode right = parseNumber();
                        left = new MulExprNode(left, right);
                        break;
                    }
                    case TokId.DIV_OP: {
                        myLexer.getNextToken();
                        ExprNode right = parseNumber();
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
