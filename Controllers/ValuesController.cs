using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using WebAPI.Transform;
using StatsQuery = WebAPI.Transform.Stats.StatsQuery;
using stat = WebAPI.Transform.Stats.stat;
using dimension = WebAPI.Transform.Dimension.dimension;
using DimensionQuery = WebAPI.Transform.Dimension.DimensionQuery;
using quantity = WebAPI.Transform.Quantity.quantity;
using QuantityQuery = WebAPI.Transform.Quantity.QuantityQuery;

namespace MvcApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        [ActionName("stats")]
        public DataTable GetStatQuery([FromUri]stat json)
        {
            //json = "{\"query\": [{\"SELECT\": [\"geo\",\"geo.name\",\"time\",\"pop\"], \"WHERE\": {\"geo\": [\"swe\",\"nor\"],\"time\": \"2000-2015\",\"ind\":\"1\"},\"FROM\": \"humnum\"}],\"lang\": \"en\"}";
            //var json = "{\"query\": [{\"SELECT\": [\"geo\",\"geo.name\",\"time\",\"pop\"],\"FROM\": \"humnum\"}],\"lang\": \"en\"}";
            //var a = JsonConvert.DeserializeObject<root>(json);
            StatsQuery sQ = new StatsQuery(json);
            var str = sQ.getData();
            return str;

            //SELECT s = new SELECT();
            //s.select = new List<string> { "geo" };
            //FROM f = new FROM();
            //f.from = "dimTable";
            //WHERE w = new WHERE();
            //w.geo = new List<string> { "swe", "nor" };
            //w.time = "2001-2010";
            //query q = new query
            //{
            //    SELECT = new List<string> { "geo", "geo.name" },
            //    WHERE =  w ,
            //    FROM = "directtable"
            //};

            //var r = new root { query = new[] { q }, lang = "en" };
            //var j = JsonConvert.SerializeObject(r);
            return null;
            
        }

        [HttpGet]
        [ActionName("dimension")]
        public DataTable GetDimQuery([FromUri]dimension json)
        {
            //json = "{\"query\": [{\"SELECT\": [\"geo\",\"geo.name\",\"time\",\"pop\"], \"WHERE\": {\"geo\": [\"swe\",\"nor\"],\"time\": \"2000-2015\",\"ind\":\"1\"},\"FROM\": \"humnum\"}],\"lang\": \"en\"}";
            //var json = "{\"query\": [{\"SELECT\": [\"geo\",\"geo.name\",\"time\",\"pop\"],\"FROM\": \"humnum\"}],\"lang\": \"en\"}";
            //var a = JsonConvert.DeserializeObject<root>(json);
            DimensionQuery sQ = new DimensionQuery(json);
            var str = sQ.getData();
            return str;

            //SELECT s = new SELECT();
            //s.select = new List<string> { "geo" };
            //FROM f = new FROM();
            //f.from = "dimTable";
            //WHERE w = new WHERE();
            //w.geo = new List<string> { "swe", "nor" };
            //w.time = "2001-2010";
            //query q = new query
            //{
            //    SELECT = new List<string> { "geo", "geo.name" },
            //    WHERE =  w ,
            //    FROM = "directtable"
            //};

            //var r = new root { query = new[] { q }, lang = "en" };
            //var j = JsonConvert.SerializeObject(r);
            return null;

        }

        [HttpGet]
        [ActionName("quantity")]
        public DataTable GetDimQuery([FromUri]quantity json)
        {
            QuantityQuery sQ = new QuantityQuery(json);
            var str = sQ.getData();
            return str;
        }

    }

}