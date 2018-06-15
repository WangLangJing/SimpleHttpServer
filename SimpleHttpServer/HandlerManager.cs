using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Concurrent;
namespace SimpleHttpServer
{
    public class HandlerManager
    {
        private HandlerConfig _handlerConfig;
        private Dictionary<String, Func<IHttpHandler>> _factoryCache;
        private Dictionary<String, IHttpHandler> _handlerCache;

        private HttpServerUtility _serverUtility;
        private Boolean _hasMatch;


        public HandlerManager(HandlerConfig config, HttpServerUtility serverUtility)
        {
            this._handlerConfig = config;
            this._factoryCache = new Dictionary<String, Func<IHttpHandler>>();
            this._handlerCache = new Dictionary<String, IHttpHandler>();
            _serverUtility = serverUtility;
            var handlerMatchs = _handlerConfig.Handlers;
            if (handlerMatchs != null && handlerMatchs.Length > 0)
            {
                foreach (var match in handlerMatchs)
                {
                    IHttpHandler handler = null;
                    String typeStr = match.Type;
                    String matchStr = match.Select;

                    Type handlerType = Type.GetType(typeStr);

                        //IHandler obj=(IHandler)(new T());
                        Type IhandlerType = typeof(IHttpHandler);
                        NewExpression exp = Expression.New(handlerType);

                        UnaryExpression unaryExp = Expression.Convert(exp, IhandlerType);
                        var factory = Expression.Lambda<Func<IHttpHandler>>(unaryExp).Compile();
                        handler = factory.Invoke();
                        this._factoryCache.Add(matchStr, factory);
                        if (handler.IsReusable)
                        {
                            this._handlerCache.Add(typeStr, handler);
                        }
                   
                }
                _hasMatch = true;
            }

        }



        public IHttpHandler SelectHandler(HttpContext context)
        {
            String uri = context.Request.Url.OriginalString;
            IHttpHandler handler = null;
            if (_hasMatch)
            {
                var handlerMatchs = _handlerConfig.Handlers;
                String typeStr = null;
                String matchStr = null;
                foreach (var match in handlerMatchs)
                {
                    String pattern = WildCardToRegular(match.Select);
                    if (Regex.IsMatch(uri, pattern))
                    {
                        typeStr = match.Type;
                        matchStr = match.Select;
                        break;
                    }
                }
                if (typeStr != null)
                {
                    //若对象已被缓存
                    if (this._handlerCache.TryGetValue(typeStr, out handler))
                    {
                        return handler;
                    }
                    Func<IHttpHandler> handlerFactory = null;
                    if (this._factoryCache.TryGetValue(matchStr, out handlerFactory))
                    {
                        return handlerFactory.Invoke();
                    }
                    throw new InvalidOperationException("not found handler object!");
                }
            }

            handler = new DefaultHandler();
            return handler;
        }

        private static String WildCardToRegular(String value)
        {
            return "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
        }
    }
}
