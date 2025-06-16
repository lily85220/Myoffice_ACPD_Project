using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;
using Dapper;
using System.Data.SqlClient;


namespace Myoffice_ACPD_Project.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AcpdController : Controller
    {
        private readonly IConfiguration _config;
        public AcpdController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JsonElement jsonElement)
        {
            var json = jsonElement.GetRawText();

            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await conn.ExecuteAsync("usp_Myoffice_ACPD_Create", new { json }, commandType: CommandType.StoredProcedure);

            return Ok("Created");
        }

        [HttpPost]
        public async Task<IActionResult> Read([FromBody] JsonElement jsonElement)
        {
            var json = jsonElement.GetRawText();

            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var result = await conn.QueryAsync<dynamic>(
                "usp_Myoffice_ACPD_Read",
                new { json },
                commandType: CommandType.StoredProcedure
            );
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] JsonElement jsonElement)
        {
            var json = jsonElement.GetRawText();

            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await conn.ExecuteAsync("usp_Myoffice_ACPD_Update", new { json }, commandType: CommandType.StoredProcedure);

            return Ok("Updated");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] JsonElement jsonElement)
        {
            var json = jsonElement.GetRawText();

            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await conn.ExecuteAsync("usp_Myoffice_ACPD_Delete", new { json }, commandType: CommandType.StoredProcedure);

            return Ok("Deleted");
        }
    }
}
