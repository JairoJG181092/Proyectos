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
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select EmployeeId as ""EmployeeId"",
            EmployeeName as ""EmployeeName"",
            Department as ""Department"",
            to_char(dateofjoining, 'DD-MM-YYYY') as ""dateofjoining"",
            photofilename as ""photofilename""
            from Employee order by EmployeeId";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn");
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
        public JsonResult Post(Employee emp)
        {
            string query = @"
            insert into Employee(EmployeeName, Department, dateofjoining, photofilename)
            values (@EmployeeName, @Department, @dateofjoining, @photofilename)";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    comm.Parameters.AddWithValue("@Department", emp.Department);
                    comm.Parameters.AddWithValue("@dateofjoining", emp.dateofjoining);
                    comm.Parameters.AddWithValue("@photofilename", emp.photofilename);
                    myRead = comm.ExecuteReader();
                    table.Load(myRead);

                    myRead.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Se inserto correctamente");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"
            update Employee
            set EmployeeName = @EmployeeName,
            Department = @EmployeeName,
            dateofjoining = @dateofjoining,
            photofilename = @photofilename
            where EmployeeId = @EmployeeId";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    comm.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    comm.Parameters.AddWithValue("@Department", emp.Department);
                    comm.Parameters.AddWithValue("@dateofjoining", emp.dateofjoining);
                    comm.Parameters.AddWithValue("@photofilename", emp.photofilename);
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
            delete from Employee
            where EmployeeId = @EmployeeId";
            DataTable table = new DataTable();
            string sqldatasource = _configuration.GetConnectionString("EvConn");
            NpgsqlDataReader myRead;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqldatasource))
            {
                myConn.Open();
                using (NpgsqlCommand comm = new NpgsqlCommand(query, myConn))
                {
                    comm.Parameters.AddWithValue("@EmployeeId", id);

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
