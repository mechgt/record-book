using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using System.Drawing;
using RecordBook.Data;

namespace RecordBook.UI.View
{
    class RecordBookRenderer : TreeList.DefaultRowDataRenderer
    {
        public RecordBookRenderer(TreeList tree)
            : base(tree)
        { }

        #region Overrides

        protected override StringFormat GetCellStringFormat(object rowElement, TreeList.Column column)
        {
            StringFormat format = base.GetCellStringFormat(rowElement, column);
            RecordNode node = rowElement as RecordNode;

            if (node.IsRecordSet && column.Id == "Rank")
            {
                format.Alignment = StringAlignment.Near;
            }
            else if (node.IsRecord && column.Id == "RecValue")
            {
                format.Alignment = StringAlignment.Far;
            }

            return format;
        }

        protected override TreeList.DefaultRowDataRenderer.RowDecoration GetRowDecoration(object element)
        {
            return base.GetRowDecoration(element);
        }

        protected override Brush GetCellTextBrush(TreeList.DrawItemState rowState, object element, TreeList.Column column)
        {
            return base.GetCellTextBrush(rowState, element, column);
        }

        protected override FontStyle GetCellFontStyle(object element, TreeList.Column column)
        {
            return base.GetCellFontStyle(element, column);
        }

        protected override void DrawCell(Graphics graphics, TreeList.DrawItemState rowState, object element, TreeList.Column column, Rectangle cellRect)
        {
            base.DrawCell(graphics, rowState, element, column, cellRect);
        }

        #endregion
    }
}
