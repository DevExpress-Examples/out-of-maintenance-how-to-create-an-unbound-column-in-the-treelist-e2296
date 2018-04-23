using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraTreeList;

namespace WindowsApplication1
{
    public class MyTreeList : TreeList
    {

        public MyTreeList()
        {

        }

        protected override string GetColumnError(DevExpress.XtraTreeList.Columns.TreeListColumn column, DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
          if (column == null || column.FieldName == "Unbound") return string.Empty;// your condition 
            return base.GetColumnError(column, node);
        }

    }
}
