using EvaluacionTecnica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace EvaluacionTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : Controller
    {

        private readonly IConfiguration _configuration;
        public SurveyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"	select * from survey inner join activity on survey.idactivity = activity.idactivity";
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
        public JsonResult Post(Survey survey)
        {
            string query = @"INSERT INTO survey(idactivity, answers, created_at)
	VALUES (@idactivity, '@answers', '@created_at');";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn2");
            var arreglo = survey.Answer;
            var cadena = Newtonsoft.Json.JsonConvert.SerializeObject(arreglo).ToString();
            //JsonConverter<Answers> cadena = JsonConverter<Answers>;
            
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@idactivity", survey.IdActivity);
                    comm.Parameters.AddWithValue("@answers", cadena);
                    comm.Parameters.AddWithValue("@created_at", survey.created_at);
                    
                    myRead = comm.ExecuteReader();
                    table.Load(myRead);

                    myRead.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Se inserto correctamente");
        }

        [HttpPut]
        public JsonResult Put(Survey survey)
        {
            string query = @"update survey
            set idactivity = @idactivity, answers= @answers, created_at = @created_at
            where idSurvey = @idSurvey";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn2");
            var arreglo = survey.Answer;
            var cadena = Newtonsoft.Json.JsonConvert.SerializeObject(arreglo).ToString();
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {

                    comm.Parameters.AddWithValue("@idSurvey", survey.IdSurvey);
                    comm.Parameters.AddWithValue("@idactivity", survey.IdActivity);
                    comm.Parameters.AddWithValue("@answers", cadena);
                    comm.Parameters.AddWithValue("@created_at", survey.created_at);
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
            delete from survey
            where idSurvey = @idSurvey";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn2");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@idSurvey", id);

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
