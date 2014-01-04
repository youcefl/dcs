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
 * Creation date: 2013.01.04
 **/

namespace dc
{
    enum TokId
    {
        NUMBER,     //!< A number
        PLUS_OP,    //!< Plus sign '+'
        MINUS_OP,   //!< Minus sign '-'
        MUL_OP,     //!< Multiplication operator '*'
        DIV_OP,     //!< Division operator '/'
        LPAR,       //!< Left parenthesis
        RPAR,       //!< Right parenthesis
        EOS         //!< End of stream
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
        public override string ToString()
        {
            return string.Format("[{0},\"{1}\"]", myId, myText);
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
            if( myCurrentToken == null ) 
            {
                myCurrentToken = getNextToken();
            }
            return myCurrentToken;
        }

        Token buildRecognizedToken(TokId tokenId, string tokenText)
        {
            if( myEnteredToken ) 
            {
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

            for( ; myIndex < myExpr.Length; ++myIndex ) 
            {
                switch( myExpr[myIndex] ) 
                {
                    case '+': return buildRecognizedToken(TokId.PLUS_OP,  "+");
                    case '-': return buildRecognizedToken(TokId.MINUS_OP, "-");
                    case '*': return buildRecognizedToken(TokId.MUL_OP,   "*");
                    case '/': return buildRecognizedToken(TokId.DIV_OP,   "/");
                    case '(': return buildRecognizedToken(TokId.LPAR,     "(");
                    case ')': return buildRecognizedToken(TokId.RPAR,     ")");
                    case '0': case '1': case '2': case '3': case '4':
                    case '5': case '6': case '7': case '8': case '9': 
                    {
                        myEnteredToken = true;
                        myCurrentTokenId = TokId.NUMBER;
                        myCurrentTokenText += myExpr[myIndex];
                        break;
                    }
                    case ' ': 
                    {
                        if( myEnteredToken ) 
                        { 
                            myEnteredToken = true;
                        } 
                        else 
                        {
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
}