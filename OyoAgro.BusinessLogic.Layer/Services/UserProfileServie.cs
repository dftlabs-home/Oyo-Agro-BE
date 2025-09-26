using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OyoAgro.BusinessLogic.Layer.Helpers;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class UserProfileServie : IUserProfileServie
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileServie(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

    }
}
