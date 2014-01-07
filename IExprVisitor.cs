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

namespace dcs
{
	/// <summary>
	/// Summary description for IExprVisitor.
	/// </summary>
	interface IExprVisitor
	{
		void visitIn(NumberNode n);
		void visitOut(NumberNode n);
		void visitIn(AddExprNode n);
		void visitOut(AddExprNode n);
        void visitIn(SubExprNode n);
        void visitOut(SubExprNode n);
        void visitIn(MulExprNode n);
        void visitOut(MulExprNode n);
        void visitIn(DivExprNode n);
        void visitOut(DivExprNode n);
        void visitIn(PowExprNode n);
        void visitOut(PowExprNode n);
    }
}

