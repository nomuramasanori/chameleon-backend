using System;
using System.Collections.Generic;
using Chameleon;
using System.Data;

namespace BusinessClass
{
	public class FinancialStatementDisplay
	{
        [ListItem(ListItemType.Body1)]
        [Selector(nameof(SetDropdownAccount), nameof(SetDropdownAccount), Name = "勘定科目", Width = 6)]
        public string Account { get; set; }

        [ListItem(ListItemType.Title)]
        [Selector(nameof(SetAllDropdownCompany), nameof(SetDropdownCompany), Name = "会社", Required = true, Width = 6)]
        public string Company { get; set; }

		[Number(Name = "1st")]
		public Int16 AmountMonth1 { get; set; }

		[Number(Name = "2nd", ReadOnly = true)]
		public Int64 AmountMonth2 { get; set; }

		[Number(Name = "3rd", Required = true)]
		public Int64 AmountMonth3 { get; set; }

		[Number(Name = "4th", Min = 100)]
		public Int64 AmountMonth4 { get; set; }

		[Number(Name = "5th")]
		public Int64 AmountMonth5 { get; set; }

		[Number(Name = "合計")]
		public Int64 Total { get; set; }

        [Text(Name = "備考1", MaxLength = 30, MinLength = 0)]
        [ListItem(ListItemType.Title)]
		public string desription1 { get; set; }

        [Text(Name = "備考2", Length = 50)]
		public string desription2 { get; set; }

        [Number(Name = "Float", DisplayDigit = 2, Min = 20.1)]
        public float floatNumber { get; set; }

        [Number(Name = "Double")]
        public double doubleNumber { get; set; }

        [Number(Name = "Decimal")]
        public decimal decimalNumber { get; set; }

        //[Column("読み取り専用")]
        //public bool ReadOnly { get; set; }

        [Image(Name = "画像", SaveMethod = nameof(UploadPicture))]
        [ListItem(ListItemType.Image)]
        public string Picture { get; set; }

        [MultipleSelector(nameof(GetDropdownItems), Name = "複数選択")]
        public string[] MultipleSelect { get; set; }

        public string UploadPicture(PostedFile image)
        {
            var type = image.Type.Replace("image/", "");
            var fileName = $"{this.Account}.{type}";
            var path = System.IO.Path.Combine(image.WebRootPath, "image", fileName);
            var fileUrl = $"{image.ServerUrl}/image/{fileName}";

            using (var file = new System.IO.FileStream(path, System.IO.FileMode.Create))
            {
                image.MemoryStream.CopyTo(file);
            }

            return fileUrl;
        }

        public List<DropdownItem> GetDropdownItems(IDbConnection connection)
        {
            var items = new List<DropdownItem>();
            items.Add(new DropdownItem("001", "hoge"));
            items.Add(new DropdownItem("002", "fuga"));
            return items;
        }

        public List<DropdownItem> SetAllDropdownCompany(IDbConnection connection)
        {
            var items = new List<DropdownItem>();

            items.Add(new DropdownItem { Id = "xxxxx", Name = "XXXXX" });
            items.Add(new DropdownItem { Id = "yyyyy", Name = "YYYYY" });
            items.Add(new DropdownItem { Id = "zzzzz", Name = "ZZZZZ" });
            items.Add(new DropdownItem { Id = "aaaaa", Name = "AAAAA" });

            return items;
        }

        public List<DropdownItem> SetDropdownCompany(IDbConnection connection)
        {
            var items = new List<DropdownItem>();

            if (this.Account == "4444444")
            {
                items.Add(new DropdownItem { Id = "yyyyy", Name = "YYYYY" });
                items.Add(new DropdownItem { Id = "xxxxx", Name = "XXXXX" });
            }
            if (this.Account == "5555555") items.Add(new DropdownItem { Id = "zzzzz", Name = "ZZZZZ" });
            if (this.Account == "1111111") items.Add(new DropdownItem { Id = "yyyyy", Name = "YYYYY" });

            return items;
        }

        public List<DropdownItem> SetDropdownAccount(IDbConnection connection)
        {
            var items = new List<DropdownItem>();
            items.Add(new DropdownItem { Id = "4444444", Name = "hoge" });
            items.Add(new DropdownItem { Id = "5555555", Name = "fuga" });
            items.Add(new DropdownItem { Id = "1111111", Name = "foo" });

            return items;
        }
    }
}
