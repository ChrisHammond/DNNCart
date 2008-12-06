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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

namespace DnnCart
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewDnnCart class displays the content
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    partial class ViewProduct : PortalModuleBase, IActionable
    {

        #region Private Members

        private string strTemplate;

        #endregion

        #region Public Methods

        public bool DisplayAudit()
        {
            bool retValue = false;

            if ((string)Settings["auditinfo"] == "Y")
            {
                retValue = true;
            }

            return retValue;
        }

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
                if (!Page.IsPostBack)
                {
                    if (((string)Settings["template"] != null) && ((string)Settings["template"] != ""))
                        strTemplate = (string)Settings["template"];
                    else
                        strTemplate = Localization.GetString("Template.Text", LocalResourceFile);

                    ProductController objProducts = new ProductController();
                    List<ProductInfo> colProducts;

                    //get the content from the DnnCart table
                    colProducts = objProducts.GetProducts(ModuleId);

                    if (colProducts.Count == 0)
                    {
                        //add a default product the content to the DnnCart table
                        ProductInfo objProduct = new ProductInfo();
                        objProduct.ModuleId = ModuleId;
                        objProduct.Name = Localization.GetString("DefaultName", LocalResourceFile);
                        objProduct.ShortDescription = Localization.GetString("DefaultShortDescription", LocalResourceFile);
                        objProduct.LongDescription = Localization.GetString("DefaultLongDescription", LocalResourceFile);
                        objProduct.Quantity = Convert.ToInt32(Localization.GetString("DefaultQuantity", LocalResourceFile));
                        objProduct.Cost = Convert.ToDecimal(Localization.GetString("DefaultCost", LocalResourceFile));
                        objProduct.Price = Convert.ToDecimal(Localization.GetString("DefaultPrice", LocalResourceFile));
                        objProduct.CreatedByUser = this.UserId;
                        objProducts.AddProduct(objProduct);

                        //get the content from the DnnCart table
                        colProducts = objProducts.GetProducts(ModuleId);
                    }

                    //bind the content to the repeater
                    lstContent.DataSource = colProducts;
                    lstContent.DataBind();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }

        protected void lstContent_ItemDataBound(System.Object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            string strContent = strTemplate;
            string strValue = Null.NullString;

            //add content to template
            if (Settings.Contains("template"))
            {
                strContent = (string)Settings["template"];
            }
            
            ArrayList objProperties = CBO.GetPropertyInfo(typeof(ProductInfo));
            int intProperty;
            foreach (PropertyInfo objPropertyInfo in objProperties)
            {
                if (strContent.IndexOf("[" + objPropertyInfo.Name.ToUpper() + "]") != -1)
                {
                    strValue = Server.HtmlDecode(DataBinder.Eval(e.Item.DataItem, objPropertyInfo.Name).ToString());
                    strContent = strContent.Replace("[" + objPropertyInfo.Name.ToUpper() + "]", strValue);
                }
            }

            //assign the content
            Label lblContent = (Label)e.Item.FindControl("lblContent");
            lblContent.Text = strContent;
        }

        #endregion

        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Registers the module actions required for interfacing with the portal framework
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                Actions.Add(this.GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile), ModuleActionType.AddContent, "", "", this.EditUrl(), false, SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

        #endregion

    }
}

