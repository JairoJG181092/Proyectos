using EvaluacionTecnica.Models;
using Microsoft.AspNetCore.Http;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select DepartmentId as ""DepartmentId"",
            DepartmentName as ""DepartmentName"" from Department order by DepartmentId";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query,myConn))
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
        public JsonResult Post(Department dep)
        {
            string query = @"insert into Department(DepartmentName)
            values (@DepartmentName)";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myRead = comm.ExecuteReader();
                    table.Load(myRead);

                    myRead.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Se inserto correctamente");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"update Department
            set DepartmentName = @DepartmentName
            where DepartmentId = @DepartmentId";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    comm.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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
            delete from Department
            where DepartmentId = @DepartmentId";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@DepartmentId", id);
                   
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
