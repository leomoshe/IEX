using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using IEX.Utilities.Collections;

namespace IEX.Lab.App
{
    public class SortingGrid
    {
        public static void SortGrid<T>(ref DataGridViewColumn lastCol, T[] list, DataGridView grid, DataGridViewCellMouseEventArgs e)
        {
            if (list == null || list.Length == 0) return;
            
            DataGridViewColumn newCol = grid.Columns[e.ColumnIndex];
            try
            {
                if (lastCol != null && lastCol != newCol)
                    lastCol.HeaderCell.SortGlyphDirection = SortOrder.None;

                newCol.SortMode = DataGridViewColumnSortMode.Automatic;
                newCol.HeaderCell.SortGlyphDirection = newCol.HeaderCell.SortGlyphDirection == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

                List<T> selectedItems = new List<T>();
                foreach (DataGridViewRow row in grid.SelectedRows)
                    selectedItems.Add(list[row.Index]);

                Array.Sort<T>(list, (new PropertyComparer<T>(newCol.DataPropertyName, newCol.HeaderCell.SortGlyphDirection == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending)));

                grid.Refresh();

                grid.ClearSelection();

                foreach (T item in selectedItems)
                    grid.Rows[Array.IndexOf<T>(list, item)].Selected = true;

                lastCol = newCol;
            }
            catch
            {
                newCol.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }       
    }    
}
