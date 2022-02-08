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
    public class PropertyController : Controller
    {

        private readonly IConfiguration _configuration;
        public PropertyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from property";
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
        public JsonResult Post(Property property)
        {
            string query = @"insert into property (title, addres, description, created_at, updated_at, disabled_at, status)
	values (@title, @addres, @description, @created_at, NULL, NULL, @status)";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn2");
            
            //JsonConverter<Answers> cadena = JsonConverter<Answers>;

            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@title", property.title);
                    comm.Parameters.AddWithValue("@addres", property.addres);
                    comm.Parameters.AddWithValue("@description", property.description);
                    comm.Parameters.AddWithValue("@created_at", property.created_at);
                    
                    comm.Parameters.AddWithValue("@status", property.status);

                    myRead = comm.ExecuteReader();
                    table.Load(myRead);

                    myRead.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Se inserto correctamente");
        }

        [HttpPut]
        public JsonResult PutActualizacion(Property property)
        {
            string query;
            if (property.dis==false) 
            {
                query = @"update property
            set title=@title, addres=@addres, description=@description, updated_at=@updated_at, status=@status
            where idproperty = @idproperty";
            }
            else
            {
                query = @"update property
            set disabled_at = @disabled_at
            where idproperty = @idproperty";
            }

            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn2");
            
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {

                    comm.Parameters.AddWithValue("@idproperty", property.idproperty);
                    comm.Parameters.AddWithValue("@title", property.title);
                    comm.Parameters.AddWithValue("@addres", property.addres);
                    comm.Parameters.AddWithValue("@description", property.description);
                    comm.Parameters.AddWithValue("@created_at", property.created_at);
                    comm.Parameters.AddWithValue("@updated_at", property.updated_at);
                    comm.Parameters.AddWithValue("@disabled_at", property.disabled_at);                    
                    comm.Parameters.AddWithValue("@status", property.status);
                    myRead = comm.ExecuteReader();
                    table.Load(myRead);

                    myRead.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Se actualizo correctamente");
        }

        //[HttpPut]
        //public JsonResult PutDesactivar(Property property)
        //{
        //    string query = @"update property
        //    set disabled_at=@disabled_at
        //    where idproperty = @idproperty";
        //    DataTable table = new DataTable();
        //    string sqldatasource = _configuration.GetConnectionString("EvConn2");

        //    NpgsqlDataReader myRead;
        //    using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
        //    {
        //        myConn.Open();
        //        using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
        //        {

        //            comm.Parameters.AddWithValue("@idproperty", property.idproperty);
                    

        //            comm.Parameters.AddWithValue("@disabled_at", property.disabled_at);

                    
        //            myRead = comm.ExecuteReader();
        //            table.Load(myRead);

        //            myRead.Close();
        //            myConn.Close();
        //        }
        //    }
        //    return new JsonResult("Se desactivo correctamente");
        //}

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
            delete from property
            where idproperty = @idproperty";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn2");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@idproperty", id);

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
