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

using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

namespace DnnCart
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditDnnCart class is used to manage content
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    partial class EditProduct : PortalModuleBase
    {

    #region Private Members

        private int ProductId = Null.NullInteger;

    #endregion

    #region Event Handlers

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(System.Object sender, System.EventArgs e)
        {
            try
            {
                //Determine ItemId of DnnCart to Update
                if(this.Request.QueryString["ProductId"] !=null)
                {
                    ProductId = Int32.Parse(this.Request.QueryString["ProductId"]);
                }

                //If this is the first visit to the page, bind the role data to the datalist
                if (Page.IsPostBack == false)
                {
                    cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");

                    if(ProductId != -1)
                    {
                        //get content
                        ProductController objProducts = new ProductController();
                        ProductInfo objProduct = objProducts.GetProduct(ModuleId, ProductId);
                        if (objProduct != null)
                        {
                            txtProductName.Text = objProduct.Name;
                            txtProductCost.Text = objProduct.Cost.ToString();
                            txtProductPrice.Text = objProduct.Price.ToString();
                            txtShortDescription.Text = objProduct.ShortDescription;
                            txtLongDescription.Text = objProduct.LongDescription;
                            //ctlAudit.CreatedByUser = objProduct.CreatedByUser.ToString();
                            //ctlAudit.CreatedDate = objProduct.CreatedDate.ToString();
                        }
                        else
                        {
                            Response.Redirect(Globals.NavigateURL(), true);
                        }
                    }
                    else
                    {
                        cmdDelete.Visible = false;
                        ctlAudit.Visible = false;
                    }
                }
           }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// cmdCancel_Click runs when the cancel button is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void cmdCancel_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                this.Response.Redirect(Globals.NavigateURL(this.TabId), true);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// cmdDelete_Click runs when the delete button is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void cmdDelete_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                //Only attempt to delete the item if it exists already
                if (!Null.IsNull(ProductId))
                {
                    ProductController objProducts = new ProductController();
                    objProducts.DeleteProduct(ModuleId, ProductId);
 
                    //refresh cache
                    SynchronizeModule();
                }

                this.Response.Redirect(Globals.NavigateURL(this.TabId), true);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// cmdUpdate_Click runs when the update button is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void cmdUpdate_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                ProductController objProducts = new ProductController();
                ProductInfo objProduct = new ProductInfo();

                objProduct.ModuleId = ModuleId;
                objProduct.ProductId = ProductId;
                objProduct.Name = txtProductName.Text.Trim();
                objProduct.ShortDescription = txtShortDescription.Text;
                objProduct.LongDescription = txtLongDescription.Text;
                objProduct.Cost = Convert.ToDecimal(txtProductCost.Text.ToString());
                objProduct.Price = Convert.ToDecimal(txtProductPrice.Text.ToString());

                objProduct.CreatedByUser = this.UserId;
                objProduct.LastUpdatedByUser = this.UserId;

                //Update the content within the DnnCart table
                if(Null.IsNull(ProductId))
                {
                    objProducts.AddProduct(objProduct);
                }
                else
                {
                    objProducts.UpdateProduct(objProduct);
                }

                //refresh cache
                SynchronizeModule();

                //Redirect back to the portal home page
                this.Response.Redirect(Globals.NavigateURL(this.TabId), true);
             }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

    #endregion

    }
}

