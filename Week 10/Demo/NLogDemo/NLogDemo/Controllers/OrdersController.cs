using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NLogDemo
{
    public class OrdersController : ApiController
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        [HttpGet]
        public IHttpActionResult Get()
        {
            Logger.Info("Orders API called");

            return Ok("Orders fetched");
        }

        [HttpGet]
        [Route("api/orders/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Logger.Debug("Fetching order {0}", id);

                throw new Exception("Database failure");

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error fetching order {0}", id);
                return InternalServerError();
            }
        }
    }
}