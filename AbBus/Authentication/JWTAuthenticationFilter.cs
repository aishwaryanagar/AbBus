﻿using AbBus.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AbBus
{

    //reference: http://blogs.quovantis.com/json-web-token-jwt-with-web-api/#:~:text=JSON%20Web%20Token%20(JWT)%20is,%3A%20Header%2C%20Payloads%20and%20Signature.
    public class JWTAuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
                if (!IsUserAuthorized(actionContext))
                {
                    ShowAuthenticationError(actionContext);
                    return;
                }
                base.OnAuthorization(actionContext);
        }

        public bool IsUserAuthorized(HttpActionContext actionContext)
        {
            var authHeader = FetchFromHeader(actionContext); //fetch authorization token from header
            if (authHeader != null)
            {
                var auth = new AuthenticationModule();
                JwtSecurityToken userPayloadToken = auth.GenerateUserClaimFromJWT(authHeader);

                if (userPayloadToken != null)
                {
                    JWTAuthenticationIdentity identity = auth.PopulateUserIdentity(userPayloadToken);
                    string[] roles = { "All" };
                    var genericPrincipal = new GenericPrincipal(identity, roles);
                    Thread.CurrentPrincipal = genericPrincipal;
                    var authenticationIdentity = Thread.CurrentPrincipal.Identity as JWTAuthenticationIdentity;
                    if (authenticationIdentity != null && !string.IsNullOrEmpty(authenticationIdentity.UserEmail))
                    {
                        authenticationIdentity.UserId = identity.UserId;
                        authenticationIdentity.UserName = identity.UserName;
                        authenticationIdentity.UserEmail = identity.UserEmail;
                    }
                    return true;
                }
            }
            return false;
        }
        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null)
            {
                requestToken = authRequest.Parameter;
            }

            return requestToken;
        }

        private static void ShowAuthenticationError(HttpActionContext filterContext)
        {
            filterContext.Response =
            filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { Code = 401, Message = "Unable to access, Please login again" });
        }
    }
}