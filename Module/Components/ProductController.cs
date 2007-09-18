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
    public class ProductController : ISearchable, IPortable
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

    #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public SearchItemInfoCollection GetSearchItems(ModuleInfo ModInfo)
        {
            SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();
            List<ProductInfo> colDnnCarts  = GetProducts(ModInfo.ModuleID);

            foreach (ProductInfo objProduct in colDnnCarts)
            {
                if (objProduct != null)
                {
                    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objProduct.ShortDescription, objProduct.CreatedByUser, objProduct.CreatedDate, ModInfo.ModuleID, objProduct.ProductId.ToString(), objProduct.LongDescription, "ProductId=" + objProduct.ProductId.ToString());
                    SearchItemCollection.Add(SearchItem);
                }
            }

            return SearchItemCollection;
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public string ExportModule(int ModuleID)
        {
            string strXML = "";
            List<ProductInfo> colDnnCarts  = GetProducts(ModuleID);

            if (colDnnCarts.Count != 0)
            {
                strXML += "<Products>";
                foreach (ProductInfo objProductInfo in colDnnCarts)
                {
                    //TODO: use stringbuilder or XML builder
                    strXML += "<Product>";
                    //TODO: add RSS feed information
                    //name
                    strXML += "<name>" + XmlUtils.XMLEncode(objProductInfo.Name) + "</name>";
                    //price
                    //short description
                    strXML += "<shortDescription>" + XmlUtils.XMLEncode(objProductInfo.ShortDescription) + "</shortDescription>";

                    //long description
                    strXML += "<longDescription>" + XmlUtils.XMLEncode(objProductInfo.LongDescription) + "</longDescription>";

                    //quantity
                    strXML += "<quantity>" + XmlUtils.XMLEncode(objProductInfo.Quantity.ToString()) + "</quantity>";

                    //cost
                    strXML += "<cost>" + XmlUtils.XMLEncode(objProductInfo.Cost.ToString()) + "</cost>";

                    //price
                    strXML += "<price>" + XmlUtils.XMLEncode(objProductInfo.Price.ToString()) + "</price>";
                    strXML += "</Product>";
                }
                strXML += "</Products>";
            }

            return strXML;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void ImportModule(int ModuleID, string Content, string Version, int UserId)
        {
            XmlNode xmlProducts = Globals.GetContent(Content, "Products");

            foreach (XmlNode xmlDnnCart in xmlProducts.SelectNodes("Product"))
            {
                ProductInfo objProduct = new ProductInfo();

                objProduct.ModuleId = ModuleID;
                //TODO: get xml fields, name, short desc, long desc, etc
                objProduct.Name = xmlDnnCart.SelectSingleNode("name").InnerText;
                objProduct.ShortDescription = xmlDnnCart.SelectSingleNode("shortDescription").InnerText;
                objProduct.LongDescription = xmlDnnCart.SelectSingleNode("longDescription").InnerText;
                objProduct.Quantity = Convert.ToInt32(xmlDnnCart.SelectSingleNode("quantity").InnerText);
                objProduct.Cost = Convert.ToDecimal(xmlDnnCart.SelectSingleNode("cost").InnerText);
                objProduct.Price = Convert.ToDecimal(xmlDnnCart.SelectSingleNode("price").InnerText);
                objProduct.CreatedByUser = UserId;
                AddProduct(objProduct);
            }
        }

    #endregion

    }
}

