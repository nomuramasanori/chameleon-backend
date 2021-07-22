using System;
using System.Collections.Generic;

namespace Chameleon
{
    public class ValidationResults
    {
        public bool IsValid
        {
            get
            {
                bool isValid = true;
                foreach (var values in this.Results.Values)
                {
                    isValid = !values.Exists(result => result.Level == ValidationResultLevel.Error);
                    if (!isValid) break;
                }

                return isValid;
            }
        }

        public Dictionary<string, List<ValidationResult>> Results { get; set; } = new Dictionary<string, List<ValidationResult>>();

        public class ValidationResult
        {
            public ValidationResult(string columnID, int rowIndex, string message, ValidationResultLevel level)
            {
                this.ColumnID = columnID;
                this.RowIndex = rowIndex;
                this.Message = message;
                this.Level = level;
            }

            public string Message { get; set; }
            public ValidationResultLevel Level { get; set; }
            public string ColumnID { get; set; }
            public int RowIndex { get; set; }
        }

        public enum ValidationResultLevel
        {
            Warning, Error 
        }

        public void Add(string blockID, string columnID, int rowIndex, string message, ValidationResultLevel level)
        {
            if (!this.Results.ContainsKey(blockID))
            {
                this.Results.Add(blockID, new List<ValidationResult>());
            }

            //check whether the target cell already has an error.
            //error count of each cell must be one at once. 
            var isExists = false;
            foreach(var validationResult in this.Results[blockID])
            {
                if (validationResult.RowIndex == rowIndex && validationResult.ColumnID == columnID)
                {
                    //update if target is at a higher level
                    if(validationResult.Level < level)
                    {
                        validationResult.Message = message;
                        validationResult.Level = level;
                    }

                    isExists = true;
                    break;
                }
            }

            if(!isExists) this.Results[blockID].Add(new ValidationResult(columnID, rowIndex, message, level));
        }
    }
}
