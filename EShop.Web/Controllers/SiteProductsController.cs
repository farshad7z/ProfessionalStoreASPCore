using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.Services.Site;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.Controllers
{
    public class SiteProductsController : Controller
    {
        private readonly ISiteProductServices _siteProductServices;
        public SiteProductsController(ISiteProductServices siteProductServices)
        {
            _siteProductServices = siteProductServices;
           
        }


        public async Task<IActionResult> Details(int id)
        {
            var product = await _siteProductServices.GetProductByIdAsync(id);
            ViewBag.ImagePath = "~/uploads/products/";

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        //--** Start Comments Cods **--
        //#region Comments


        //public ActionResult ShowComments(int id)
        //{

        //    return PartialView(db.Product_Comments.Where(c => c.ProductID == id).ToList());
        //}


        //public ActionResult CreateComment(int id)
        //{
        //    int? userID = null;
        //    string userName = null;
        //    string email = null;

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var user = db.Users.Single(u => u.UserName == User.Identity.Name);
        //        userID = user.UserID;
        //        userName = user.UserName.Trim(); ;
        //        email = user.Email.Trim();
        //    }
        //    ViewBag.userName = userName;
        //    ViewBag.email = email;

        //    return PartialView(new Product_Comments()
        //    {

        //        UserID = userID,
        //        ProductID = id,


        //    });
        //}



        //[HttpPost]
        //public ActionResult CreateComment(Product_Comments producComment)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        Product_Comments comment = new Product_Comments()
        //        {
        //            ProductID = producComment.ProductID,
        //            UserID = producComment.UserID,
        //            Name = producComment.Name,
        //            Email = producComment.Email,
        //            Comment = producComment.Comment,
        //            CreateDate = DateTime.Now,
        //            AdminConfirmation = true,
        //            ParentID = producComment.ParentID,

        //        };

        //        db.Product_Comments.Add(comment);
        //        db.SaveChanges();


        //        int? userID = null;
        //        string userName = null;
        //        string email = null;


        //        if (User.Identity.IsAuthenticated)
        //        {
        //            var user = db.Users.Single(u => u.UserName == User.Identity.Name);
        //            userID = user.UserID;
        //            userName = user.UserName.Trim();
        //            email = user.Email.Trim();
        //        }

        //        ViewBag.userName = userName;
        //        ViewBag.email = email;



        //        return PartialView("ShowComments", db.Product_Comments.Where(c => c.ProductID == producComment.ProductID).ToList());


        //    }
        //    return View(producComment);
        //}



        ////#endregion
        //--** End Comments Cods **--



        //--** Start Meno Cods **--
        #region Meno
        //public ActionResult ShowMenoMain()
        //{
        //    return PartialView(db.Product_Groups.ToList());
        //}

        //public ActionResult ShowMenoGroups()
        //{
        //    return PartialView(db.Product_Groups.ToList());
        //}

        //--** End Meno Cods **--
        #endregion Meno



        //--** Start Archive Cods **--
        #region Archive
        //[Route("Archive")]
        //public ActionResult ArchiveProduct(int pageId = 1, string title = "", int? maxPrice = null, int? minPrice = null, List<int> selectedGroups = null)
        //{
        //    ViewBag.productName = title;
        //    ViewBag.Groups = db.Product_Groups.ToList();
        //    ViewBag.maxPrice = maxPrice;
        //    ViewBag.minPrice = minPrice;
        //    ViewBag.pageID = pageId;
        //    ViewBag.selectGroup = selectedGroups;
        //    List<Products> list = new List<Products>();
        //    //IQueryable<Products> list;

        //    if (selectedGroups != null && selectedGroups.Any())
        //    {
        //        foreach (var group in selectedGroups)
        //        {
        //            list.AddRange(db.Product_Select_Groups.Where(g => g.Product_GroupID == group).Select(p=>p.Products).ToList());   
        //        }
        //        list=list.Distinct().ToList();  
        //    }
        //    else
        //    {
        //        list.AddRange(db.Products.ToList());
        //    }

        //    if (title != null)
        //    {
        //        list = list.Where(p => p.Title.Contains(title)).ToList();
        //    }


        //    if (minPrice > 0)
        //    {
        //        list = list.Where(p => p.Price >= minPrice).ToList();
        //    }

        //    if (maxPrice > 0)
        //    {
        //        list = list.Where(p => p.Price <= maxPrice).ToList();
        //    }

        //    //--** Start Pagging **--
        //    int take = 24;
        //    int skip = (pageId - 1) * take;
        //    //--** End Pagging **--

        //    ViewBag.pageCount=list.Count()/take;
        //    ViewBag.productCount = list.Count();
        //    return View(list.OrderByDescending(p=>p.CreateDate).Skip(skip).Take(take).ToList());
        //}

        #endregion
        //--** End Archive Cods **--

    }
}