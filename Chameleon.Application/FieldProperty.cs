using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Chameleon
{
    public class FieldProperty
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "readOnly")]
        public bool ReadOnly { get; set; }

        [JsonProperty(PropertyName = "visible")]
        public bool Visible { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool Required { get; set; }

        [JsonProperty(PropertyName = "textOption")]
        public ITextOption TextOption { get; set; }

        [JsonProperty(PropertyName = "numberOption")]
        public INumberOption NumberOption { get; set; }

        [JsonProperty(PropertyName = "hyperLinkOption")]
        public IHyperLinkOption HyperLinkOption { get; set; }

        [JsonProperty(PropertyName = "multipleSelectOption")]
        public IMultipleSelectOption MultipleSelectOption { get; set; }

        [JsonProperty(PropertyName = "listItems")]
        public List<DropdownItem> ListItems { get; private set; }

        [JsonProperty(PropertyName = "listItemType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ListItemType ListItemType { get; set; } = ListItemType.None;

        public FieldProperty(string id, TextAttribute attribute)
        {
            this.Id = id;
            this.Type = "Text";

            this.TextOption = attribute;

            this.SetCommonProperties(attribute);
        }

        public FieldProperty(string id, NumberAttribute attribute)
        {
            this.Id = id;
            this.Type = "Number";

            this.NumberOption = attribute;

            this.SetCommonProperties(attribute);
        }

        public FieldProperty(string id, SelectorAttribute attribute)
        {
            this.Id = id;
            this.Type = "List";

            this.SetCommonProperties(attribute);
        }

        public FieldProperty(string id, MultipleSelectorAttribute attribute)
        {
            this.Id = id;
            this.Type = "MultipleSelect";

            this.MultipleSelectOption = attribute;

            this.SetCommonProperties(attribute);
        }

        public FieldProperty(string id, ImageAttribute attribute)
        {
            this.Id = id;
            this.Type = "Image";

            this.SetCommonProperties(attribute);
        }

        public FieldProperty(string id, CheckboxAttribute attribute)
        {
            this.Id = id;
            this.Type = "Checkbox";

            this.SetCommonProperties(attribute);
        }

        public FieldProperty(string id, HyperLinkAttribute attribute)
        {
            this.Id = id;
            this.Type = "HyperLink";

            this.HyperLinkOption = attribute;

            this.SetCommonProperties(attribute);
        }

        private void SetCommonProperties(FieldAttribute attribute)
        {
            this.Name = attribute.Name;
            this.ReadOnly = attribute.ReadOnly;
            this.Visible = attribute.Visible;
            this.Width = attribute.Width;
            this.Required = attribute.Required;
        }

        public void SetListItems(List<DropdownItem> listItems)
        {
            this.ListItems = listItems;
        }
    }
}
