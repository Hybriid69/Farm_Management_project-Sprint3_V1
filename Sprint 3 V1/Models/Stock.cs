using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Collections;
using Sprint_3_V1.ViewModels;
using Sprint_3_V1.Models;

namespace Sprint_3_V1.Models
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        public int StockID { get; set; }

        [Display(Name = "Current Quantity of Crops(Kg)")]
        public double CurQuantity { get; set; }

        [Display(Name = "Quantity of Crops Harvested(Kg)")]
        public double HarQuantity { get; set; }

        [Display(Name = "Harvested Date")]
        [DataType(DataType.Date)]
        public DateTime Harvested { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime Expiery { get; set; }

        public bool ExpFlag { get; set; }

        [Display(Name = "Price Per Kg")]
        public double Price { get; set; }

        public bool Disabled { get; set; }

        //[Display(Name = "Image")]
        //public byte[] StockImage { get; set; }


        public int CropID { get; set; }

        [ForeignKey("CropID")]
        public virtual CropInfo CropInfo { get; set; }

        public virtual ICollection<StockOrder> StockOrder { get; set; }

        public virtual ICollection<StocksImage> StocksImage { get; set; }


        public void stockDeduct(double quantity, int? stockid)
        {
            Sprint_3_V1Context db = new Sprint_3_V1Context();

            var stockGet = (from x in db.Stocks
                            where x.StockID == stockid
                            select x.CurQuantity).Single();

            Stock stock = db.Stocks.Find(stockid);


            stock.CurQuantity = Convert.ToDouble(stockGet) - quantity;

            db.Entry(stock).State = EntityState.Modified;
            db.SaveChanges();
        }
        public int idMethod()
        {
            return StockID;
        }


        public List<StockViewModel> sortValue(string sort)
        {
            Sprint_3_V1Context db = new Sprint_3_V1Context();

            List<StockViewModel> searchStockList = new List<StockViewModel>();

            if (sort != null)
            {
                if (sort == "Name Ascending")
                {
                    var mergedList = (from c in db.CropInfoes
                                      join x in db.Stocks
                                      on c.CropID equals x.CropID
                                      join y in db.StocksImages
                                      on x.StockID equals y.StockID
                                      orderby c.Name ascending
                                      select new
                                      {
                                          x.StockID,
                                          c.CropID,
                                          c.Name,
                                          x.CurQuantity,
                                          x.Expiery,
                                          x.Price,
                                          y.StockImage
                                      }).ToList();

                    if (mergedList.Count() > 0)
                    {
                        foreach (var item in mergedList)
                        {
                            StockViewModel stk = new StockViewModel();
                            stk.StockID = item.StockID;
                            stk.CropID = item.CropID;
                            stk.CropName = item.Name;
                            stk.CurQuantity = item.CurQuantity;
                            stk.Expiery = item.Expiery;
                            stk.StockImage = item.StockImage;
                            stk.Price = item.Price;
                            searchStockList.Add(stk);
                        }
                    }

                }
                else if (sort == "Name Descending")
                {
                    var mergedList = (from c in db.CropInfoes
                                      join x in db.Stocks
                                      on c.CropID equals x.CropID
                                      join y in db.StocksImages
                                      on x.StockID equals y.StockID
                                      orderby c.Name descending
                                      select new
                                      {
                                          x.StockID,
                                          c.CropID,
                                          c.Name,
                                          x.CurQuantity,
                                          x.Expiery,
                                          x.Price,
                                          y.StockImage
                                      }).ToList();

                    if (mergedList.Count() > 0)
                    {
                        foreach (var item in mergedList)
                        {
                            StockViewModel stk = new StockViewModel();
                            stk.StockID = item.StockID;
                            stk.CropID = item.CropID;
                            stk.CropName = item.Name;
                            stk.CurQuantity = item.CurQuantity;
                            stk.Expiery = item.Expiery;
                            stk.StockImage = item.StockImage;
                            stk.Price = item.Price;
                            searchStockList.Add(stk);
                        }
                    }
                }
                else if (sort == "Price Ascending")
                {
                    var mergedList = (from c in db.CropInfoes
                                      join x in db.Stocks
                                      on c.CropID equals x.CropID
                                      join y in db.StocksImages
                                      on x.StockID equals y.StockID
                                      orderby x.Price ascending
                                      select new
                                      {
                                          x.StockID,
                                          c.CropID,
                                          c.Name,
                                          x.CurQuantity,
                                          x.Expiery,
                                          x.Price,
                                          y.StockImage
                                      }).ToList();

                    if (mergedList.Count() > 0)
                    {
                        foreach (var item in mergedList)
                        {
                            StockViewModel stk = new StockViewModel();
                            stk.StockID = item.StockID;
                            stk.CropID = item.CropID;
                            stk.CropName = item.Name;
                            stk.CurQuantity = item.CurQuantity;
                            stk.Expiery = item.Expiery;
                            stk.StockImage = item.StockImage;
                            stk.Price = item.Price;
                            searchStockList.Add(stk);
                        }
                    }

                }
                else if (sort == "Price Descending")
                {
                    var mergedList = (from c in db.CropInfoes
                                      join x in db.Stocks
                                      on c.CropID equals x.CropID
                                      join y in db.StocksImages
                                      on x.StockID equals y.StockID
                                      orderby x.Price descending
                                      select new
                                      {
                                          x.StockID,
                                          c.CropID,
                                          c.Name,
                                          x.CurQuantity,
                                          x.Expiery,
                                          x.Price,
                                          y.StockImage
                                      }).ToList();

                    if (mergedList.Count() > 0)
                    {
                        foreach (var item in mergedList)
                        {
                            StockViewModel stk = new StockViewModel();
                            stk.StockID = item.StockID;
                            stk.CropID = item.CropID;
                            stk.CropName = item.Name;
                            stk.CurQuantity = item.CurQuantity;
                            stk.Expiery = item.Expiery;
                            stk.StockImage = item.StockImage;
                            stk.Price = item.Price;
                            searchStockList.Add(stk);
                        }
                    }

                }

            }
            return searchStockList;
        }

    }
}