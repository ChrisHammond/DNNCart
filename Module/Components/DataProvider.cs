/*
' DnnCart - http://www.dnncart.com
' Copyright (c) 2007
' by Christopher Hammond. ( http://www.chrishammond.com )
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
 */

using System;
using DotNetNuke;
using System.Data;

using DotNetNuke.Framework;

namespace DnnCart
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// An abstract class that provides the DAL contract
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    public abstract class DataProvider
    {

        #region Shared/Static Methods

        // singleton reference to the instantiated object 
        static DataProvider objProvider = null;

        // constructor
        static DataProvider()
        {
            CreateProvider();
        }

        // dynamically create provider
        private static void CreateProvider()
        {
            objProvider = (DataProvider)Reflection.CreateObject("data", "DnnCart", "");
        }

        // return the provider
        public static DataProvider Instance()
        {
            return objProvider;
        }

        #endregion

        #region Abstract methods

        #region add signatures

        //public abstract void AddDnnCart(int ModuleId, string Content, int UserId);
        public abstract void AddProduct(int ModuleId, string name, string ShortDescription, string LongDescription, int Quantity, decimal Cost, decimal Price, int createdUserId);
        #endregion

        #region get signatures
        public abstract IDataReader GetProduct(int ModuleId, int ProductId);
        public abstract IDataReader GetProducts(int ModuleId);
        #endregion

        #region update signatures
        /*updates*/
        //public abstract void UpdateDnnCart(int ModuleId, int ItemId, string Content, int UserId);
        public abstract void UpdateProduct(int ModuleId, int ProductId, string name, string ShortDescription, string LongDescription, int Quantity, decimal Cost, decimal Price, int lastUpdatedUserId);
        #endregion

        #region delete signatures
        /*deleted*/
        //public abstract void DeleteDnnCart(int ModuleId, int ItemId);
        public abstract void DeleteProduct(int ModuleId, int ProductId);
        #endregion


        #endregion

    }
}
