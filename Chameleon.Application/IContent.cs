using System;
using System.Data;
using System.Collections.Generic;

namespace Chameleon
{
    public interface IContent
    {
        Layout GetLayout();

        List<FieldProperty> GetColumnProperty(string blockId, IDbConnection connection);

        object GetLinkers(string id);

        dynamic Recalculate(string editedData, IDbConnection connection);

        List<DropdownItem> GetListItem(string blockId, string columnName, string rowData, IDbConnection connection);

        List<DropdownItem> GetListItem(string blockId, string columnName, IDbConnection connection);

        dynamic GetData(IDbConnection connection);

        void ConfigureBlock(string condition, string host);

        dynamic Save(IDbConnection connection, string editedData);

        Block GetBlock(string id);

        //IDbConnection Connection { get; set; }

        string Name { get; }

        string Description { get; }

        bool ShowAsMenu { get; }
    }
}
