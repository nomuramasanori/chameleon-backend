using System;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chameleon
{
	public abstract class Grid<TCore, TDisplay> : Block, IGridOption
	{
        public bool RowEditable { get; }

        public int FrozenRows { get; }

        public int FrozenColumns { get; }

        public Grid(
            string name,
            string id = null,
            string blockType = "grid",
            bool rowEditable = true,
            int frozenRows = 0,
            int frozenColumns = 0) : base(blockType, name, id)
        {
            this.RowEditable = rowEditable;
            this.FrozenRows = frozenRows;
            this.FrozenColumns = frozenColumns;
        }

		public List<TCore> CoreData { get; private set; }

		protected List<TDisplay> DisplayData { get; set; }

        public Difference<TDisplay> Difference { get; set; }

        internal override void MapJsonToObject(string json)
		{
            var jArray = (JObject)JsonConvert.DeserializeObject(json);

            this.DisplayData = JsonConvert.DeserializeObject<List<TDisplay>>(jArray["data"].ToString());

            //if (jArray["difference"].HasValues)
            //{
            //    var differenceJArray = jArray["difference"];
            //    var addedRows = JsonConvert.DeserializeObject<List<AddedRow>>(differenceJArray["_addedRows"].ToString());
            //    var updatedRows = JsonConvert.DeserializeObject<List<UpdatedRow>>(differenceJArray["_updatedRows"].ToString());
            //    var deletedRows = JsonConvert.DeserializeObject<List<TDisplay>>(differenceJArray["_deletedRows"].ToString());
            //    this.Difference = new Difference<TDisplay>(addedRows, updatedRows, deletedRows);
            //}
        }

        internal override string CreateJsonFromObject()
		{
			return JsonConvert.SerializeObject(this.DisplayData);
		}
		internal override Array GetDisplayData()
		{
			var result = new TDisplay[this.DisplayData.Count];
			this.DisplayData.CopyTo(result, 0);
			return result;
		}

		internal override void SetCoreDataFromDisplayData()
		{
			this.CoreData = this.ConvertDisplayDataToCoreData(this.DisplayData);
        }
		protected abstract List<TCore> ConvertDisplayDataToCoreData(List<TDisplay> displayData);

		internal override void SetDisplayDataFromCoreData()
		{
			this.DisplayData = this.ConvertCoreDataToDisplayData(this.CoreData);
        }
		protected abstract List<TDisplay> ConvertCoreDataToDisplayData(List<TCore> coreData);

		internal override void SetCoreData(IDbTransaction transaction)
		{
			this.CoreData = this.GetData(transaction);
		}

		protected abstract List<TCore> GetData(IDbTransaction dbTransaction);

        public override List<FieldProperty> GetColumnProperty(IDbConnection connection)
		{
			return this.GetColumnProperty(typeof(TDisplay), connection);
		}

        public override Type GetRecordType()
        {
            return typeof(TDisplay);
        }

        public override List<DropdownItem> GetListItem(string column, JObject rowData, IDbConnection connection)
        {
            return this.GetListItem(typeof(TDisplay), column, rowData, connection);
        }
    }
}
