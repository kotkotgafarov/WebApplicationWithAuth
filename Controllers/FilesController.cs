using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationWithAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http.Headers;
using WebApplicationWithAuth.Models;
using WebApplicationWithAuth.Data;


namespace WebApplicationWithAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : Controller
    {

        private readonly ILogger<FilesController> _logger;
        private string[] _permittedExtensions;
        private readonly long _fileSizeLimit;
        private LKADbContext _db;

        public FilesController(ILogger<FilesController> logger, IConfiguration configuration, LKADbContext db)
        {
            _logger = logger;
            _permittedExtensions = configuration.GetSection("permittedExtensions").Get<string[]>();
           // _permittedExtensions.Concat(configuration.GetSection("permittedExtensions").Get<string[]>());
            _fileSizeLimit = configuration.GetValue<long>("FileSizeLimit");
            _db = db;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromQuery] int enrolleeId, [FromQuery] int docId) 
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var files = formCollection.Files;
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
                EnrolleesFile enrolleesFile;

                //foreach (var file in files)
                if (files.Count > 0)
                {
                    var file = files.First();
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        var ext = Path.GetExtension(fileName).ToLowerInvariant();

                        if (string.IsNullOrEmpty(ext) || !_permittedExtensions.Contains(ext))
                        {
                            return BadRequest();
                        }

                        if (file.Length > _fileSizeLimit)
                        {
                            return BadRequest();
                        }

                        var fileNameRandom = Path.GetRandomFileName();
                        var fullPath = Path.Combine(pathToSave, fileNameRandom);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        enrolleesFile = new EnrolleesFile();
                        enrolleesFile.EnrolleeID = enrolleeId;
                        enrolleesFile.EnrolleesDocID = docId;
                        enrolleesFile.Name = fileNameRandom;
                        enrolleesFile.UserName = fileName;
                        _db.EnrollesFiles.Add(enrolleesFile);

                        int affected = _db.SaveChanges();

                        if (affected > 0)
                        {
                            return Ok(enrolleesFile.Id);
                        }

                    }
                    else
                    {
                        return BadRequest();
                    }

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpGet("{id}")]
        public IActionResult DownloadFile(int id)
        {
            try
            {
                EnrolleesFile f = _db.EnrollesFiles.First(enrolleesfile => enrolleesfile.Id == id);

                var fileName = f.Name;//"1opksyhl.v1i";
                var fileNameToReturn = f.UserName;//"111.pdf";
                var pathToGet = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
               //if (fileName == null) return NotFound();

                var fullPath = Path.Combine(pathToGet, fileName);
                var fs = new FileStream(fullPath, FileMode.Open);

               // Return the file. A byte array can also be used instead of a stream
                return File(fs, "application/octet-stream", fileNameToReturn);
            }
            catch (Exception ex)
            {
               return StatusCode(500, $"Internal server error: {ex}");
            }
            return Ok();
        }

    }
}
