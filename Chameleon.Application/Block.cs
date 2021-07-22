using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Newtonsoft.Json.Linq;

namespace Chameleon
{
    public abstract class Block
    {
        public string Id { get; }
        //public abstract string Name { get; }
        public string Name { get; }

        public string BlockType { get; }

        public bool NoFrame { get; }

        public string Explain { get; }

        public Block(string blockType, string name, string id = null, bool noFrame = false, string explain = null)
        {
            this.Id = id ?? this.GetType().FullName;

            this.BlockType = blockType;

            this.Name = name;

            this.NoFrame = noFrame;

            this.Explain = explain;
        }

        internal abstract void SetCoreData(IDbTransaction tracsaction);
        internal abstract void MapJsonToObject(string json);

        internal abstract string CreateJsonFromObject();
        internal abstract Array GetDisplayData();

        internal abstract void SetCoreDataFromDisplayData();
        internal abstract void SetDisplayDataFromCoreData();

        public virtual void ValidateBeforeCalculation(ref ValidationResults results) { }
        public virtual void ValidateAfterCalculation(ref ValidationResults results, IDbConnection connection)
        {
            foreach (var prop in this.GetColumnProperty(connection))
            {
                if (prop.Type != "List") return;

                for (int i = 0; i < this.GetDisplayData().Length; i++)
                {
                    var rowJObject = JObject.FromObject(this.GetDisplayData().GetValue(i));

                    var validationValue = rowJObject[prop.Id].Value<string>();

                    if (validationValue == "") continue;

                    var expected = this.GetListItem(prop.Id, rowJObject, connection);

                    if (!expected.Exists(item => { return item.Id == validationValue; }))
                    {
                        results.Add(this.Id, prop.Id, i, "Not exists in the list.", ValidationResults.ValidationResultLevel.Error);
                    }
                }
            }

        }

        public abstract List<DropdownItem> GetListItem(string column, JObject rowData, IDbConnection connection);

        public List<DropdownItem> GetListItem(Type target, string column, JObject rowData, IDbConnection connection)
        {
            var col = target.GetProperties().Where(prop => prop.Name == column).First();
            var selectorAttribute = System.Attribute.GetCustomAttribute(col, typeof(SelectorAttribute)) as SelectorAttribute;
            var parameter = new object[] { connection };

            object instance = null;
            string method = "";

            if(rowData == null)
            {
                instance = Activator.CreateInstance(target);
                method = selectorAttribute.GetAllItemMethod;
            }
            else
            {
                instance = rowData.ToObject(target);
                method = selectorAttribute.GetItemMethod;
            }

            return target.GetMethod(method).Invoke(instance, parameter) as List<DropdownItem>;
        }

        internal List<DropdownItem> GetAllListItem(string column, IDbConnection connection)
        {
            return this.GetListItem(column, null, connection);
        }

        public virtual void Calculate(IDbTransaction transaction) { }

        virtual protected internal void Save(IDbTransaction transaction) { }

        public abstract List<FieldProperty> GetColumnProperty(IDbConnection connection);

        public List<FieldProperty> GetColumnProperty(Type target, IDbConnection connection)
        {
            var result = new List<FieldProperty>();

            //Columnの属性を取得
            foreach (var column in target.GetProperties())
            {
                FieldProperty columnProperty = null;

                if (Attribute.IsDefined(column, typeof(TextAttribute)))
                {
                    var attribute = System.Attribute.GetCustomAttribute(column, typeof(TextAttribute)) as TextAttribute;
                    columnProperty = new FieldProperty(column.Name, attribute);
                }

                if (Attribute.IsDefined(column, typeof(NumberAttribute)))
                {
                    var attribute = System.Attribute.GetCustomAttribute(column, typeof(NumberAttribute)) as NumberAttribute;

                    if (attribute.Max == double.MaxValue)
                    {
                        switch (Type.GetTypeCode(column.PropertyType))
                        {
                            case TypeCode.Int16: attribute.Max = short.MaxValue; break;
                            case TypeCode.Int32: attribute.Max = int.MaxValue; break;
                            case TypeCode.Int64: attribute.Max = long.MaxValue; break;
                            case TypeCode.Single: attribute.Max = float.MaxValue; break;
                            case TypeCode.Double: attribute.Max = double.MaxValue; break;
                            case TypeCode.Decimal: attribute.Max = decimal.ToDouble(decimal.MaxValue); break;
                            case TypeCode.UInt16: attribute.Max = ushort.MaxValue; break;
                            case TypeCode.UInt32: attribute.Max = uint.MaxValue; break;
                            case TypeCode.UInt64: attribute.Max = ulong.MaxValue; break;
                            default: break;
                        }
                    }

                    if (attribute.Min == double.MinValue)
                    {
                        switch (Type.GetTypeCode(column.PropertyType))
                        {
                            case TypeCode.Int16: attribute.Min = short.MinValue; break;
                            case TypeCode.Int32: attribute.Min = int.MinValue; break;
                            case TypeCode.Int64: attribute.Min = long.MinValue; break;
                            case TypeCode.Single: attribute.Min = float.MinValue; break;
                            case TypeCode.Double: attribute.Min = double.MinValue; break;
                            case TypeCode.Decimal: attribute.Min = decimal.ToDouble(decimal.MinValue); break;
                            case TypeCode.UInt16: attribute.Min = ushort.MinValue; break;
                            case TypeCode.UInt32: attribute.Min = uint.MinValue; break;
                            case TypeCode.UInt64: attribute.Min = ulong.MinValue; break;
                            default: break;
                        }
                    }

                    columnProperty = new FieldProperty(column.Name, attribute);
                }

                if (Attribute.IsDefined(column, typeof(SelectorAttribute)))
                {
                    var attribute = System.Attribute.GetCustomAttribute(column, typeof(SelectorAttribute)) as SelectorAttribute;
                    columnProperty = new FieldProperty(column.Name, attribute);
                    columnProperty.SetListItems(this.GetAllListItem(column.Name, connection));
                }

                if (Attribute.IsDefined(column, typeof(MultipleSelectorAttribute)))
                {
                    var attribute = System.Attribute.GetCustomAttribute(column, typeof(MultipleSelectorAttribute)) as MultipleSelectorAttribute;
                    columnProperty = new FieldProperty(column.Name, attribute);

                    var instance = Activator.CreateInstance(target);
                    var parameter = new object[] { connection };
                    var items = target.GetMethod(attribute.GetItemMethod).Invoke(instance, parameter) as List<DropdownItem>;

                    columnProperty.SetListItems(items);
                }

                if (Attribute.IsDefined(column, typeof(ImageAttribute)))
                {
                    var attribute = System.Attribute.GetCustomAttribute(column, typeof(ImageAttribute)) as ImageAttribute;
                    columnProperty = new FieldProperty(column.Name, attribute);
                }

                if (Attribute.IsDefined(column, typeof(ListItemAttribute)))
                {
                    var attribute = System.Attribute.GetCustomAttribute(column, typeof(ListItemAttribute)) as ListItemAttribute;
                    columnProperty.ListItemType = attribute.Type;
                }

                if (Attribute.IsDefined(column, typeof(CheckboxAttribute)))
                {
                    var attribute = System.Attribute.GetCustomAttribute(column, typeof(CheckboxAttribute)) as CheckboxAttribute;
                    columnProperty = new FieldProperty(column.Name, attribute);
                }

                if (Attribute.IsDefined(column, typeof(HyperLinkAttribute)))
                {
                    var attribute = System.Attribute.GetCustomAttribute(column, typeof(HyperLinkAttribute)) as HyperLinkAttribute;
                    columnProperty = new FieldProperty(column.Name, attribute);
                }

                result.Add(columnProperty);
            }

            return result;
        }

        public List<ILinker> Linkers { get; set; } = new List<ILinker>();
        public void AddLinker(ILinker linker)
        {
            this.Linkers.Add(linker);
        }
        public ILinker GetLink(string id)
        {
            return this.Linkers.FirstOrDefault(linker => linker.Id == id);
        }

        public string AuthenticateUrl { get; set; }

        public abstract Type GetRecordType();
    }
}
