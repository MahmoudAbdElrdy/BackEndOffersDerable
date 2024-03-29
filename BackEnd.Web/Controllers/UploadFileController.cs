﻿
using BackEnd.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController :Controller
    {
        private readonly IWebHostEnvironment _env;

        public UploadFileController(IWebHostEnvironment env)
        {
            _env = env;


        }
        [HttpPost("UploadImage")]
        public IActionResult Upload()
        {
            ResponseDTO res;
            try
            {
                var name = Helper.UploadHelper.SaveFile(Request.Form.Files[0], "File");
                //string path = xx[0];
                res = new ResponseDTO()
                {
                    Code = 200,
                    Message = "",
                    Data = name,
                };
            }
            catch (Exception ex)
            {
                res = new ResponseDTO()
                {
                    Code = 400,
                    Message = "Error " + ex.Message,
                    Data = null,
                };
            }
            return Ok(res);
        }
        [HttpPost("FileUpload")]
        public async Task<IActionResult> index(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Content("file not selected");
            long size = files.Sum(f => f.Length);
            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var folderName = Path.Combine("wwwroot/UploadFiles");
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }

                    string extension = Path.GetExtension(formFile.FileName);

                   
                    string fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                    var fileNameWithDate = fileName + DateTime.Now.Ticks;
                    var filepath = Path.Combine(folderName, fileNameWithDate + extension);
                  

                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    } 
                    filePaths.Add(fileNameWithDate + extension);
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size, filePaths });
        }
        [HttpGet("Base")]
        public string ServerRootPath()
        {

            return $"{Request.Scheme}://{Request.Host}{Request.PathBase}"+ "/wwwroot/UploadFiles/";
        }
    }
}
