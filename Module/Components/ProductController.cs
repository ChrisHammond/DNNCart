/*
' DnnCart - http://www.dnncart.com
' Copyright (c) 2008
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
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Web;
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace DnnCart
{
    /// -----------------------------------------------------------------------------
    ///<summary>
    /// The Controller class for the DnnCart
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    public class ProductController 
    {

    #region Constructors

        public ProductController()
        {
        }

    #endregion

    #region Public Methods

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// adds an object to the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="objProduct">The ProductInfo object</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void AddProduct(ProductInfo objProduct)
        {
            if (objProduct.ShortDescription.Trim() != "")
            {
                DataProvider.Instance().AddProduct(objProduct.ModuleId, objProduct.Name, objProduct.ShortDescription, objProduct.LongDescription, objProduct.Quantity, objProduct.Cost, objProduct.Price, objProduct.CreatedByUser);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// deletes an object from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModuleId">The Id of the module</param>
        /// <param name="ItemId">The Id of the item</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void DeleteProduct(int ModuleId, int ProductId) 
        {
            DataProvider.Instance().DeleteProduct(ModuleId, ProductId);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// gets an object from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="moduleId">The Id of the module</param>
        /// <param name="ItemId">The Id of the item</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public ProductInfo GetProduct(int ModuleId, int ProductId)
        {
            return CBO.FillObject<ProductInfo>(DataProvider.Instance().GetProduct(ModuleId, ProductId));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// gets an object from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="moduleId">The Id of the module</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public List<ProductInfo> GetProducts(int ModuleId)
        {
            return CBO.FillCollection< ProductInfo >(DataProvider.Instance().GetProducts(ModuleId));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// saves an object to the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="objProduct">The ProductiNfo object</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void UpdateProduct(ProductInfo objProduct)
        {
            //TODO: check for a valid product
            if (objProduct.ShortDescription.Trim() != "")
            {
                DataProvider.Instance().UpdateProduct(objProduct.ModuleId, objProduct.ProductId, objProduct.Name, objProduct.ShortDescription, objProduct.LongDescription, objProduct.Quantity, objProduct.Cost, objProduct.Price, objProduct.CreatedByUser);
            }
        }

    #endregion

    }
}

