using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItTalkServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR;

namespace ItTalkServer.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string _cacheKey;
        private readonly IHubContext _hub;

        public ApiController(IMemoryCache memoryCache, IConnectionManager connectionManager)
        {
            _memoryCache = memoryCache;
            _cacheKey = "nodes";
            _hub = connectionManager.GetHubContext<ItTalkHub>();
        }

        [HttpGet]
        public dynamic GetNodes()
        {
            var nodes = _memoryCache.GetOrCreate<IList<Node>>(_cacheKey, x => new List<Node>());
            return nodes;
        }
        
        [HttpPost("register/{id:int}")]
        public void Register(int id, [FromBody]string imageBase64)
        {
            var node = new Node(id, imageBase64);
            var nodes = _memoryCache.GetOrCreate<IList<Node>>(_cacheKey, x => new List<Node>());
            nodes.Add(node);
            _memoryCache.Set(_cacheKey, nodes);
            _hub.Clients.All.ClientRegistration(node);
        }

        [HttpGet("hello")]
        public void Hello()
        {
            _hub.Clients.All.SayHello("Howdy ho!");
        }
    }
}
