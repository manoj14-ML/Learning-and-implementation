using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using System.ComponentModel;

namespace Test_webapplication
{
    public class CustomExcelProperties
    {

        public CustomExcelProperties()
        {
           
            //COLUMN HEADER 
            ColumnHeaderFontColor = XLColor.NoColor;
            ColumnHeaderBackgroundColor = XLColor.NoColor;
            ColumnHeaderFontSize = 11;
            ColumnHeaderStrikeFont = false;
            ColumnHeaderFontBold = false;
            ColumnHeaderFontItalic = false;
            ColumnHeaderFontStyle = "Cambria";
            ColumnHeaderWrapText = false;
            ColumnHeaderAlignmentHorizontal = XLAlignmentHorizontalValues.Left;

            //ROW
            RowFontColor = XLColor.NoColor;
            RowBackgroundColor = XLColor.NoColor;
            RowStrikeFont = false;
            RowFontBold = false;
            RowFontSize = 11;
            RowFontItalic = false;
            RowFontStyle = "Cambria";
            RowWrapText = false;
            RowAlignmentHorizontal = XLAlignmentHorizontalValues.Left;

            TabColor = XLColor.NoColor;
            HighlightCellRequired = false;
            HighlightCellColor = XLColor.NoColor;
            HighlightCellRownumberList = new List<CustomHighlightCellRownumber>();
        }
        /// <summary>
        /// This property specifies for RowFontColor
        /// </summary>
       
        public XLColor RowFontColor
        {
            get; // get method
            set;  // set method
        }
        /// <summary>
        /// This property specifies for RowBackgroundColor
        /// </summary>
       
        public XLColor RowBackgroundColor
        {
            get; // get method
            set;  // set method
        }

        
        /// <summary>
        /// This property specifies for RowFontStyle
        /// </summary>
        [DefaultValue("")]
        public string RowFontStyle
        {
            get; // get method
            set;  // set method
        }
        /// <summary>
        /// This property specifies for RowFontSize
        /// </summary>
        [DefaultValue(11)]
        public int RowFontSize
        {
            get; // get method
            set;  // set method
        }
      
        /// <summary>
        /// 
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool RowStrikeFont
        {
            get; // get method
            set;  // set method
        }
        /// <summary>
        /// 
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool RowFontBold
        {
            get; // get method
            set;  // set method
        }
        /// <summary>
        /// 
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool RowFontItalic
        {
            get; // get method
            set;  // set method
        }
        [System.ComponentModel.DefaultValue(false)]
        public bool RowWrapText
        {
            get; // get method
            set;  // set method
        }
        public XLAlignmentHorizontalValues RowAlignmentHorizontal { get; set; }
       
        /// <summary>
        /// This property specifies for ColumnHeaderFontColor
        /// </summary>
        
        public XLColor ColumnHeaderFontColor
        {
            get; // get method
            set;  // set method
        }
        /// <summary>
        /// This property specifies for ColumnHeaderBackgroundColor
        /// </summary>
       
        public XLColor ColumnHeaderBackgroundColor
        {
            get; // get method
            set;  // set method
        }
        /// <summary>
        /// This property specifies for ColumnHeaderFontStyle
        /// </summary>
        public string ColumnHeaderFontStyle
        {
            get; // get method
            set;  // set method
        }
        [System.ComponentModel.DefaultValue(11)]
        public int ColumnHeaderFontSize
        {
            get; // get method
            set;  // set method
        }
        [System.ComponentModel.DefaultValue(false)]
        public bool ColumnHeaderFontItalic
        {
            get; // get method
            set;  // set method
        }
        [System.ComponentModel.DefaultValue(false)]
        public bool ColumnHeaderStrikeFont
        {
            get; // get method
            set;  // set method
        }
        [System.ComponentModel.DefaultValue(false)]
        public bool ColumnHeaderFontBold
        {
            get; // get method
            set;  // set method
        }

        [System.ComponentModel.DefaultValue(false)]
        public bool ColumnHeaderWrapText
        {
            get; // get method
            set;  // set method
        }
        public XLAlignmentHorizontalValues ColumnHeaderAlignmentHorizontal { get; set; }

        [DefaultValue(typeof(IXLNumberFormat), "")]
        public IXLNumberFormat ColumnHeaderNumberFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        
        public XLColor TabColor
        {
            get; // get method
            set;  // set method
        }

        /// <summary>
        /// if additional header is required then please set value as  true 
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool AddtionalHeaderRequired
        {
            get; // get method
            set;  // set method
        }
        [System.ComponentModel.DefaultValue(-1)]
        public int[] AddtionalHeaderRownumbers
        {
            get; // get method
            set;  // set method
        }


        /// <summary>
        /// if high
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool HighlightCellRequired
        {
            get; // get method
            set;  // set method
        }
        [System.ComponentModel.DefaultValue(-1)]
        public List<CustomHighlightCellRownumber> HighlightCellRownumberList
        {
            get; // get method
            set;  // set method
        }
        public XLColor HighlightCellColor
        {
            get; // get method
            set;  // set method
        }
        
    }

    public class CustomHighlightCellRownumber
    {
        [System.ComponentModel.DefaultValue(-1)]
        public int HighlightCellRownumber
        {
            get; // get method
            set;  // set method
        }
        [System.ComponentModel.DefaultValue(-1)]
        public int HighlightCellColumnnumber
        {
            get; // get method
            set;  // set method
        }
      
    }
}