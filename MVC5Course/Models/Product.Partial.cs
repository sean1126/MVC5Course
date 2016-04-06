namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ProductId == default(int)){
                //Create
            }
            else {
                //Update


            }


            if (this.Price <100)
            {
                yield return new ValidationResult("價格設定錯誤", new string[] { "Price" });
            }
            if (this.Stock < 5)
            {
                yield return new ValidationResult("庫存量過低，無法新增商品", new string[] { "Stock" });
            }
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]

        /*
        [產品名稱必須至少包含兩個空白]
        */

        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
