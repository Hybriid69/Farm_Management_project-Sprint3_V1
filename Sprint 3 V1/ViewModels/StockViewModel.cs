using Sprint_3_V1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.ViewModels
{
    [NotMapped]
    public partial class StockViewModel
    {
        [NotMapped]
        public string Search { get; set; }

        [Key]
        public int StockID { get; set; }

        [Display(Name = "Image")]
        public byte[] StockImage { get; set; }

        [Display(Name ="Name")]
        public string CropName { get; set; }

        [Display(Name = "Quantity available(Kg)")]
        public double CurQuantity { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime Expiery { get; set; }

        [Display(Name = "Price Per Kg")]
        public double Price { get; set; }


        [Display(Name = "Quantity")]
        //[QuantityCheck(ErrorMessage = "Selected Quantity not available")]
        public int QuantityWanted { get; set; }

        public int CropID { get; set; }

        public int idMethod()
        {
            return StockID;
        }
        public int quantityMethod()
        {
            return QuantityWanted;
        }

        public bool lowQuantitycheck(int quantity)
        {
            Sprint_3_V1Context db = new Sprint_3_V1Context();

            int id = Convert.ToInt16(StockID);

            var checkQty = db.Stocks.Where(x => x.StockID == id).Select(a => a.CurQuantity).First();
            if (checkQty >= 0)
            {
                if (checkQty >= quantity)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

    }
}