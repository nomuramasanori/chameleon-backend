using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Chameleon
{
	public abstract class Content<TCondition>: IContent where TCondition: new()
	{
        public string Name { get; }

        public string Description { get; }

        public bool ShowAsMenu { get; }

        public List<Block> Blocks { get; } = new List<Block>();

        public IDbConnection Connection { get; set; }

        protected Content(string name, string description = "", bool showAsMenu = true)
        {
            this.Name = name;
            this.Description = description;
            this.ShowAsMenu = showAsMenu;
        }

        public void ConfigureBlock(string condition, string host)
        {
            var conditionObject = string.IsNullOrEmpty(condition) ? new TCondition() : JsonConvert.DeserializeObject<TCondition>(condition);
            this.ConfigureBlock(this.Blocks, host, conditionObject);
        }
        protected abstract void ConfigureBlock(List<Block> container, string host, TCondition condition);

        public List<FieldProperty> GetColumnProperty(string blockId, IDbConnection connection)
        {
            return this.Blocks.First(block => block.Id == blockId).GetColumnProperty(connection);
        }

        public object GetLinkers(string blockId)
        {
            return this.Blocks.First(block => block.Id == blockId).Linkers.Select(linker => new { id = linker.Id, name = linker.Name } );
        }

        public dynamic GetData(IDbConnection connection)
        {
            dynamic result2 = new System.Dynamic.ExpandoObject();
            var result = new List<dynamic>();

            this.DataAccess(connection, transaction => {
                foreach (Block data in this.Blocks)
                {
                    data.SetCoreData(transaction);
                }

                this.Calculate(transaction);
            });
            
            foreach (Block data in this.Blocks)
            {
                data.SetDisplayDataFromCoreData();
                result.Add(new { id = data.Id, data = data.GetDisplayData() });
            }

            var validationResults = new ValidationResults();
            result2.RecalculatedData = result;
            result2.ValidationResults = this.ValidateAfterCalculation(validationResults, connection).Results;

            return result2;
        }

        private void DataAccess(IDbConnection connection, Action<IDbTransaction> execute)
        {
            try
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        execute(transaction);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        virtual protected ValidationResults ValidateBeforeCalculation()
        {
            var validationResults = new ValidationResults();
            this.Blocks.ForEach(block => block.ValidateBeforeCalculation(ref validationResults));

            return validationResults;
        }

        virtual protected ValidationResults ValidateAfterCalculation(ValidationResults validationResults, IDbConnection connection)
        {
            this.Blocks.ForEach(block => block.ValidateAfterCalculation(ref validationResults, connection));
            return validationResults;
        }

        virtual protected void Calculate(IDbTransaction transaction)
        {
            this.Blocks.ForEach(block => block.Calculate(transaction));
        }

        public dynamic Recalculate(string editedData, IDbConnection connection)
        {
            JArray editedDataJArray = (JArray)JsonConvert.DeserializeObject(editedData);
            dynamic result = new System.Dynamic.ExpandoObject();

            foreach (Block data in this.Blocks)
            {
                string json = editedDataJArray.Where(token => token["gridId"].ToString() == data.Id).First().ToString();
                data.MapJsonToObject(json);
                data.SetCoreDataFromDisplayData();
            }

            var validationResults = this.ValidateBeforeCalculation();

            if (validationResults.IsValid)
            {
                var recalculatedData = new List<dynamic>();

                this.DataAccess(connection, transaction =>
                {
                    this.Calculate(transaction);
                });

                foreach (Block data in this.Blocks)
                {
                    data.SetDisplayDataFromCoreData();
                    recalculatedData.Add(new { id = data.Id, data = data.GetDisplayData() });
                }

                result.IsValid = true;
                result.RecalculatedData = recalculatedData;
                result.ValidationResults = this.ValidateAfterCalculation(validationResults, connection).Results;
            }
            else
            {
                result.IsValid = false;
                result.ValidationResults = validationResults.Results;
            }

            return result;
        }

        public dynamic Save(IDbConnection connection, string editedData)
        {
            JArray editedDataJArray = (JArray)JsonConvert.DeserializeObject(editedData);

            foreach (Block data in this.Blocks)
            {
                var editedDataJson = editedDataJArray.Where(token => token["gridId"].ToString() == data.Id).First().ToString();
                data.MapJsonToObject(editedDataJson);
                data.SetCoreDataFromDisplayData();
            }

            this.ValidateBeforeCalculation();

            this.DataAccess(connection, transaction =>
            {
                this.Calculate(transaction);

                foreach (Block data in this.Blocks)
                {
                    data.Save(transaction);
                }
            });

            return this.GetData(connection);
        }

        public List<DropdownItem> GetListItem(string blockId, string columnName, string rowData, IDbConnection connection)
        {
            if (rowData == null) return null;

            JObject rowDataJArray = (JObject)JsonConvert.DeserializeObject(rowData);

            var targetBlock = this.Blocks.First(block => { return block.Id == blockId; });

            return targetBlock.GetListItem(columnName, (JObject)JsonConvert.DeserializeObject(rowData), connection);
        }

        public List<DropdownItem> GetListItem(string blockId, string columnName, IDbConnection connection)
        {
            var targetBlock = this.Blocks.First(block => { return block.Id == blockId; });

            return targetBlock.GetAllListItem(columnName, connection);
        }

        public virtual Layout GetLayout()
        {
            return new Layout(this.Blocks);
        }

        public Block GetBlock(string id)
        {
            return this.Blocks.FirstOrDefault(b => b.Id == id);
        }

        //public ContentAttribute GetFunctionAttibute()
        //{
        //    var result = System.Attribute.GetCustomAttribute(this.GetType(), typeof(ContentAttribute)) as ContentAttribute;
        //    result.id = this.GetType().ToString();
        //    return result;
        //}

        //public string Name
        //{
        //    get
        //    {
        //        var faceAttribute = System.Attribute.GetCustomAttribute(this.GetType(), typeof(ContentAttribute)) as ContentAttribute;
        //        return faceAttribute.name;
        //    }
        //}
    }
}
