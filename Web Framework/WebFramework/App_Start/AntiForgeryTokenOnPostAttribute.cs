﻿namespace WebFramework.App_Start
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AntiForgeryTokenOnPostAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ShouldValidateAntiForgeryTokenManually(filterContext))
            {
                var authorizationContext = new AuthorizationContext(filterContext.Controller.ControllerContext, filterContext.ActionDescriptor);

                //Use the authorization of the anti forgery token, 
                //which can't be inhereted from because it is sealed
                new ValidateAntiForgeryTokenAttribute().OnAuthorization(authorizationContext);
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// We should validate the anti forgery token manually if the following criteria are met:
        /// 1. The http method must be POST
        /// 2. There is not an existing [ValidateAntiForgeryToken] attribute on the action
        /// 3. There is no [BypassAntiForgeryToken] attribute on the action
        /// </summary>
        private static bool ShouldValidateAntiForgeryTokenManually(ActionExecutingContext filterContext)
        {
            //1. The http method must be POST
            var httpMethod = filterContext.HttpContext.Request.HttpMethod;
            if (httpMethod != "POST") return false;

            // 2. There is not an existing anti forgery token attribute on the action
            var antiForgeryAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false);
            if (antiForgeryAttributes.Length > 0) return false;

            // 3. There is no [BypassAntiForgeryToken] attribute on the action
            var ignoreAntiForgeryAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(BypassAntiForgeryTokenAttribute), false);
            if (ignoreAntiForgeryAttributes.Length > 0) return false;

            return true;
        }
    }
}