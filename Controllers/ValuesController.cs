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
        }

        [HttpGet]
        [ActionName("datasource")]
        public DataTable GetDataSource()
        {
            var str = WebAPI.Transform.Dimension.Utility.getData("select * from DimDataSource");
            return str;
        }

        [HttpGet]
        [ActionName("indicators")]
        public IEnumerable<Object> Get()
        {
            DataContext dbcontext = new DataContext();
            return dbcontext.indicator.ToList();
        }

        [HttpGet]
        [ActionName("indicatorswdi")]
        public IEnumerable<WDI_Indicator> GetWDI()
        {
            DataContext dbcontext = new DataContext();
            return dbcontext.indicatorwdi.ToList();
        }

        [HttpGet]
        [ActionName("indicatorssub")]
        public IEnumerable<SubNationalIndicator> GetSub()
        {
            DataContext dbcontext = new DataContext();
            return dbcontext.indicatorsub.ToList();
        }

        [HttpGet]
        [ActionName("details")]
        public DataTable Get([FromUri]Model model)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(
                System.Configuration.ConfigurationManager.
                    ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("getAllData", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@type", model.type));
                cmd.Parameters.Add(new SqlParameter("@id", model.productId));
                cmd.Parameters.Add(new SqlParameter("@startDate", (model.startDate == 0 ? 1000 : model.startDate)));
                cmd.Parameters.Add(new SqlParameter("@endDate", (model.endDate == 0 ? 3000 : model.endDate)));

                // execute the command
                //List<DetailsList> dList = new List<DetailsList>();
                
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    dt.Load(rdr);
                    //// iterate through results, printing each to console
                    //while (rdr.Read())
                    //{
                    //    //yield return DetailsList.Create(rdr);
                    //    yield return new DynamicSqlRow(rdr);
                    //}
                }
                return dt;
            }

            //List<Model> list = new List<Model>();
            //list.Add(new Model { productId = 1, startDate = 2, endDate = 3 });
            //list.Add(new Model { productId = 3, startDate = 4, endDate = 5 });
            //return list;

        }


        [HttpGet]
        [ActionName("getindicatorsofsoruce")]
        public DataTable GetDataSource([FromUri]string source)
        {
            var str = WebAPI.Transform.Dimension.Utility.getData("select * from [dbo].[DimIndicators] where [DataSourceID] =" + source);
            return str;
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

    public class Model
    {
        public int type { get; set; }
        public int productId { get; set; }
        public int startDate { get; set; }
        public int endDate { get; set; }
    }


}