using EvaluacionTecnica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : Controller
    {

        private readonly IConfiguration _configuration;
        public ActivityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select * from activity inner join property on activity.idproperty = property.idproperty";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn2");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    myRead = comm.ExecuteReader();
                    table.Load(myRead);

                    myRead.Close();
                    myConn.Close();
                }
            }
            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(Activity activity)
        {
            string query = @"INSERT INTO activity(idproperty, shedule, title, created_at, updated_at, status)
	VALUES (@idproperty, '@shedule', '@title', '@created_at', '@updated_at', '@status');";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn2");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@idproperty", activity.idProperty);
                    comm.Parameters.AddWithValue("@shedule", activity.shedule);
                    comm.Parameters.AddWithValue("@title", activity.title);
                    comm.Parameters.AddWithValue("@created_at", activity.created_at);
                    comm.Parameters.AddWithValue("@updated_at", activity.updated_at);
                    comm.Parameters.AddWithValue("@status", activity.status);
                    myRead = comm.ExecuteReader();
                    table.Load(myRead);

                    myRead.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Se inserto correctamente");
        }

        [HttpPut]
        public JsonResult Put(Activity activity)
        {
            string query = @"update activity
            set idproperty = @idproperty, shedule= @shedule, title = @title, created_at= @created_at, updated_at =@updated_at, status = @status
            where DepartmentId = @idactivity";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn2");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {

                    comm.Parameters.AddWithValue("@idactivity", activity.idactivity);
                    comm.Parameters.AddWithValue("@idproperty", activity.idProperty);
                    comm.Parameters.AddWithValue("@shedule", activity.shedule);
                    comm.Parameters.AddWithValue("@title", activity.title);
                    comm.Parameters.AddWithValue("@created_at", activity.created_at);
                    comm.Parameters.AddWithValue("@updated_at", activity.updated_at);
                    comm.Parameters.AddWithValue("@status", activity.status);
                    myRead = comm.ExecuteReader();
                    table.Load(myRead);

                    myRead.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Se actualizo correctamente");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
            delete from Activity
            where idActivity = @idActivity";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn2");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@idActivity", id);

                    myRead = comm.ExecuteReader();
                    table.Load(myRead);

                    myRead.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Se elimino correctamente");
        }
    }
}
