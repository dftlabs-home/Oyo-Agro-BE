using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class AssociationService : IAssociationService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AssociationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<TData<Association>> SaveEntity(AssociationParam param)
        {
            var obj = new TData<Association>();
            if (param == null)
            {
                obj.Message = "pls provide required data";
                obj.Tag = 0;
                return obj;
            }

            if (string.IsNullOrEmpty(param.Name))
            {
                obj.Message = " Name is required";
                obj.Tag = 0;
                return obj;
            }

             if (string.IsNullOrEmpty(param.Registrationno))
            {
                obj.Message = " Reg No is required";
                obj.Tag = 0;
                return obj;
            }


            var association = new Association()
            {
                Name = param.Name,
                Registrationno = param.Registrationno,
            };
            await _unitOfWork.AssociationRepository.SaveForm(association);
            obj.Tag = 1;
            obj.Data = association;
            return obj;
        }


        public async Task<TData<Association>> UpdateEntity(AssociationParam param)
        {
            var obj = new TData<Association>();

            if (param.Associationid == 0)
            {
                obj.Tag = 0;
                obj.Message = "Associaion ID is required";
            }

            var Entity = await _unitOfWork.AssociationRepository.GetEntity(param.Associationid);
            if (Entity == null)
            {
                obj.Tag = 0;
                obj.Message = "Associaion not found";
            }
            if (string.IsNullOrEmpty(param.Name))
            {
                obj.Message = " Name is required";
                obj.Tag = 0;
                return obj;
            }


            var association = new Association()
            {
                Associationid = param.Associationid,
                Name = param.Name,
                Registrationno = param.Registrationno,
            };
            await _unitOfWork.AssociationRepository.SaveForm(association);
            obj.Tag = 1;
            obj.Data = association;
            return obj;
        }





        public async Task<TData<List<Association>>> GetList(AssociationParam param)
        {
            var response = new TData<List<Association>>();
            var obj = await _unitOfWork.AssociationRepository.GetList(param);
            response.Data = obj;
            return response;
        }
        public async Task<TData<List<Association>>> GetList()
        {
            var response = new TData<List<Association>>();
            var obj = await _unitOfWork.AssociationRepository.GetList();
            response.Data = obj;
            return response;
        }

        public async Task<TData<Association>> GetEntity(int associationId)
        {
            var response = new TData<Association>();
            var obj = await _unitOfWork.AssociationRepository.GetEntity(associationId);
            response.Data = obj;
            return response;
        }
        public async Task<TData<Association>> DeleteEntity(int associationId)
        {
            var response = new TData<Association>();
            await _unitOfWork.AssociationRepository.DeleteForm(associationId);
            response.Tag = 1;
            return response;
        }


    }
}
