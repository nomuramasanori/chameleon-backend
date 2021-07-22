using System;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chameleon
{
    public abstract class Form<TEntity> : Block where TEntity : new()
    {
        public TEntity Entity { get; set; }

        public Difference<TEntity> Difference{ get; set; }

		public Form(string name, string id = null, bool noFrame = false, string explain = null) : base("form", name, id, noFrame, explain)
        {
            this.Entity = new TEntity();
        }

		//public override string Name
		//{
		//	get
		//	{
		//		var formAttribute = System.Attribute.GetCustomAttribute(this.GetType(), typeof(FormAttribute)) as FormAttribute;
		//		return formAttribute.name;
		//	}
		//}

		public override List<FieldProperty> GetColumnProperty(IDbConnection connection)
		{
			return this.GetColumnProperty(typeof(TEntity), connection);
		}

		internal override string CreateJsonFromObject()
		{
			return JsonConvert.SerializeObject(this.Entity);
		}

		internal override Array GetDisplayData()
		{
			var result = new TEntity[1] { this.Entity };
			return result;
		}

        internal override void MapJsonToObject(string json)
		{
            var jArray = (JObject)JsonConvert.DeserializeObject(json);
            
            this.Entity = JsonConvert.DeserializeObject<TEntity>(jArray["data"][0].ToString());

            //if (jArray["difference"].HasValues)
            //{
            //    var differenceJArray = jArray["difference"];
            //    var addedRows = new List<AddedRow>();
            //    var updatedRows = JsonConvert.DeserializeObject<List<UpdatedRow>>(differenceJArray["_updatedRows"].ToString());
            //    var deletedRows = new List<TEntity>();
            //    this.Difference = new Difference<TEntity>(addedRows, updatedRows, deletedRows);
            //}
        }

        internal override void SetCoreData(IDbTransaction transaction)
		{
			this.Entity = this.GetData(transaction);
		}
		protected abstract TEntity GetData(IDbTransaction transaction);

		internal override void SetCoreDataFromDisplayData(){}

		internal override void SetDisplayDataFromCoreData(){}

        public override Type GetRecordType()
        {
            return typeof(TEntity);
        }

        public override List<DropdownItem> GetListItem(string column, JObject rowData, IDbConnection connection)
        {
            return this.GetListItem(typeof(TEntity), column, rowData, connection);
        }
    }
}
