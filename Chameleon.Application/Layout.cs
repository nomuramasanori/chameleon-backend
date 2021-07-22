using System;
using System.Collections.Generic;

namespace Chameleon
{
	public class Layout
	{
        public bool Registerable = false;
        public List<Row> Rows = new List<Row>();
        public int ContentCount = 0;

        public Layout(bool registerable = false)
        {
            this.Registerable = registerable;
        }
        public Layout(List<Block> blocks)
        {
            var column = this.AddRow().AddColumn(12);
            blocks.ForEach(block => {
                column.AddContent(block);
            });
        }

        public Row AddRow()
        {
            return this.AddRow(12);
        }
        public Row AddRow(int size)
        {
            var newRow = new Row(this, size);
            this.Rows.Add(newRow);
            return newRow;
        }

        public void IncrementContentCount()
        {
            this.ContentCount++;
        }
	}

	public class Row
	{
        private Layout layout;
        public int size;
		public List<Column> Columns { get; } = new List<Column>();

		internal Row(Layout layout, int size)
        {
            this.layout = layout;
            this.size = size;
        }

		public Column AddColumn(int size)
		{
            var newColumn = new Column(this.layout, size);
            this.Columns.Add(newColumn);
            return newColumn;
		}
	}

	public class Column
	{
        private Layout layout;
        public List<Block> Contents { get; } = new List<Block>();
        public int Count { get { return this.Contents.Count; } }
        public int size;
        public List<Row> Rows { get; } = new List<Row>();

        internal Column(Layout layout, int size)
        {
            this.layout = layout;
            this.size = size;
        }

		public void AddContent(Block content)
		{
			this.Contents.Add(content);
            this.layout.IncrementContentCount();
		}

        public Row AddRow(int size)
        {
            Row result = new Row(this.layout, size);
            this.Rows.Add(result);
            return result;
        }
	}
}
