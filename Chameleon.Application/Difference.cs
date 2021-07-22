using System;
using System.Collections.Generic;

namespace Chameleon
{
    public class Difference<T>
    {
        private List<AddedRow> _addedRows;
        private List<UpdatedRow> _updatedRows;
        private List<T> _deletedRows;

        public Difference(List<AddedRow> addedRows, List<UpdatedRow> updatedRows, List<T> deletedRows)
        {
            this._addedRows = addedRows;
            this._updatedRows = updatedRows;
            this._deletedRows = deletedRows;
        }
    }

    public class AddedRow
    {
        private int Row { get; }

        public AddedRow(int _row)
        {
            this.Row = _row;
        }
    }

    public class UpdatedRow
    {
        public int Row { get; set; }
        public List<string> UpdateColumns { get; set; }

        public UpdatedRow(int _row, List<string> _updateColumns)
        {
            this.Row = _row;
            this.UpdateColumns = _updateColumns;
        }
    }
}
