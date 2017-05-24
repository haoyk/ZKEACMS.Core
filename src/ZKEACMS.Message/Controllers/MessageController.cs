﻿using ZKEACMS.Message.Models;
using ZKEACMS.Message.Service;
using Easy.Mvc.Authorize;
using Easy.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ZKEACMS.Message.Controllers
{
    [DefaultAuthorize(Policy = PermissionKeys.ViewMessage)]
    public class MessageController : BasicController<MessageEntity, int, IMessageService>
    {
        public MessageController(IMessageService service) : base(service)
        {
        }
        [DefaultAuthorize(Policy = PermissionKeys.ManageMessage)]
        public override ActionResult Create()
        {
            return base.Create();
        }
        [DefaultAuthorize(Policy = PermissionKeys.ManageMessage)]
        public override ActionResult Create(MessageEntity entity)
        {
            return base.Create(entity);
        }
        [DefaultAuthorize(Policy = PermissionKeys.ManageMessage)]
        public override ActionResult Edit(int Id)
        {
            return base.Edit(Id);
        }
        [DefaultAuthorize(Policy = PermissionKeys.ManageMessage)]
        public override ActionResult Edit(MessageEntity entity)
        {
            return base.Edit(entity);
        }
    }
}
