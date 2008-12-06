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
using System.Configuration;
using System.Data;

namespace DnnCart
{
    /// -----------------------------------------------------------------------------
    ///<summary>
    /// The ProductInfo class for the DnnCart
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    public class CategoryInfo
    {

    #region Private Members

        private int _ModuleId;
        private int _CategoryId;
        private string _Name;
        private string _ShortDescription;
        private string _LongDescription;
        private bool _IsDeleted;
        private int _CreatedByUser;
        private DateTime _CreatedDate;
        private int _LastUpdatedByUser;
        private DateTime _LastUpdated;
        

    #endregion

    #region Constructors

        // initialization
        public CategoryInfo()
        {
        }

    #endregion

    #region Public Properties

        /// <summary>
        /// Gets and sets the Module Id
        /// </summary>
        public int ModuleId
        {
            get
            {
                return _ModuleId;
            }
            set
            {
                _ModuleId = value;
            }
        }

        /// <summary>
        /// Gets and sets the Item Id
        /// </summary>
        public int CategoryId
        {
            get
            {
                return _CategoryId;
            }
            set
            {
                _CategoryId = value;
            }
        }

        /// <summary>
        /// gets and sets the name
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        /// <summary>
        /// gets and sets the productshortdescription
        /// </summary>
        public string ShortDescription
        {
            get
            {
                return _ShortDescription;
            }
            set
            {
                _ShortDescription = value;
            }
        }



        /// <summary>
        /// gets and sets the productlongdescription
        /// </summary>
        public string LongDescription
        {
            get
            {
                return _LongDescription;
            }
            set
            {
                _LongDescription = value;
            }
        }

        /// <summary>
        /// Gets and sets if the category is deleted
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                return _IsDeleted;
            }
            set
            {
                _IsDeleted = value;
            }
        }

        /// <summary>
        /// Gets and sets the User Id who Created/Updated the content
        /// </summary>
        public int CreatedByUser
        {
            get
            {
                return _CreatedByUser;
            }
            set
            {
                _CreatedByUser = value;
            }
        }

        /// <summary>
        /// Gets and sets the Date when Created
        /// </summary>
        public DateTime CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
            }
        }


        /// <summary>
        /// Gets and sets the User Id who Updated this Product
        /// </summary>
        public int LastUpdatedByUser
        {
            get
            {
                return _LastUpdatedByUser;
            }
            set
            {
                _LastUpdatedByUser = value;
            }
        }


        /// <summary>
        /// Gets and sets the Date when LastUpdated
        /// </summary>
        public DateTime LastUpdated
        {
            get
            {
                return _LastUpdated;
            }
            set
            {
                _LastUpdated = value;
            }
        }


    #endregion
    
    }
}
