/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Mvc.Attribute;
using Easy.Mvc.Authorize;
using Easy.Mvc.Controllers;
using Easy.ViewPort.jsTree;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ZKEACMS.Common.Models;
using ZKEACMS.Common.Service;

namespace ZKEACMS.Controllers
{
    [DefaultAuthorize]
    public class NavigationController : BasicController<NavigationEntity, string, INavigationService>
    {
        public NavigationController(INavigationService service)
            : base(service)
        {

        }
        [NonAction]
        public override ActionResult Create()
        {
            return base.Create();
        }

        public ActionResult Create(string ParentID)
        {
            var navication = new NavigationEntity
            {
                ParentId = ParentID,
                DisplayOrder = Service.Count(m => m.ParentId == ParentID) + 1
            };
            return View(navication);
        }
        public JsonResult GetNavTree()
        {
            var navs = Service.GetAll().OrderBy(m => m.DisplayOrder);
            var node = new Tree<NavigationEntity>().Source(navs).ToNode(m => m.ID, m => m.Title, m => m.ParentId, "#");
            return Json(node);
        }

        public JsonResult GetSelectNavTree()
        {
            var navs = Service.GetAll().OrderBy(m => m.DisplayOrder);
            var node = new Tree<NavigationEntity>().Source(navs).ToNode(m => m.ID, m => m.Title, m => m.ParentId, "#");
            Node root = new Node { id = "root", text = "����", children = node, state = new State { opened = true }, a_attr = new { id = "root" } };
            return Json(root);
        }

        [HttpPost]
        public JsonResult MoveNav(string id, string parentId, int position, int oldPosition)
        {
            Service.Move(id, parentId, position, oldPosition);
            return Json(true);
        }
        public ActionResult Select(string selected)
        {
            ViewBag.Selected = selected;
            return View();
        }
    }
}
