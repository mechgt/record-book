namespace RecordBook
{
    using ZoneFiveSoftware.Common.Visuals;
    using RecordBook.Data;
    using System.Drawing;
    using RecordBook.Resources;

    class RecordBookLabelProvider : TreeList.DefaultLabelProvider
    {
        public RecordBookLabelProvider()
        {
        }

        public override Image GetImage(object element, TreeList.Column column)
        {
            RecordNode node = element as RecordNode;
            if (column.Id == "Rank" && node != null)
            {
                if (node.IsRecord)
                {
                    // Images need to be 16 x 16 pixels and 96 dpi resolution
                    if (node.Record.Rank.Equals(1))
                        return Images.gold_wing;

                    else if (node.Record.Rank.Equals(2))
                        return Images.silver_feather;

                    else if (node.Record.Rank.Equals(3))
                        return Images.bronze_horse;
                }
                else if (node.IsRecordSet)
                {
                    return node.RecSet.Category.GetImage();
                }
            }

            return base.GetImage(element, column);
        }

        public override string GetText(object element, TreeList.Column column)
        {
            RecordNode node = element as RecordNode;
            if (node != null)
            {
                if (node.IsRecord)
                    return node.Record.GetFormattedText(column.Id, true);

                else if (node.IsRecordSet)
                    return node.RecSet.GetFormattedText(column.Id);
            }

            return base.GetText(element, column);
        }
    }
}
