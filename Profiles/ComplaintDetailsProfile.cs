﻿using AutoMapper;
using ComplaintLoggingSystem.DataModels;
using ComplaintLoggingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Profiles
{
    public class ComplaintDetailsProfile : Profile
    {
        public ComplaintDetailsProfile()
        {
            CreateMap<ComplaintDetailsData, ComplaintDetailsDomain>();
        }
    }
}