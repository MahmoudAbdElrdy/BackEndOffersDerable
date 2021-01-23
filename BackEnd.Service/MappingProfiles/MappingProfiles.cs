using AutoMapper;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
        }

        string getPath()
        {
            return "/wwwroot/UploadFiles/";
        }


    }
}