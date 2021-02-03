using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.DAL.Entities;
using BackEnd.Service.DTO.Companies;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
   public class CompanyServices : BaseServices, ICompanyServices
    {
        private IidentityServices identityServices;
        #region ServicesCompany(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public CompanyServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper, IidentityServices _identityServices)
            : base(unitOfWork, responseDTO, mapper)
        {
            identityServices = _identityServices;

        }
        #endregion

        #region GetAll()
        public IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.Company.Get(x => x.IsDelete == false,includeProperties: "User", page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<ShowCompanyDto>>(result);
                    _response.Data = resultList;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "No Data";
                }
            }
            catch (Exception ex)
            {
             
                _response.Data = null;
               _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

 
        #region Remove(CompanyDto model)
        public IResponseDTO Remove(CompanyDto model)
        {
            try
            {
                var DBmodel = _mapper.Map<Company>(model);
                _unitOfWork.Company.Delete(DBmodel);
                var save = _unitOfWork.Save();
                if (save == "200")
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                    _response.Message = save;
                   _response.Code = 400;
                   
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region GetByIdAsync(Guid? id)
        public IResponseDTO GetByIdAsync(int? id)
        {
            try
            {
                var DBmodel = _unitOfWork.Company.Get(x => x.Id == id && x.IsDelete == false).FirstOrDefault();
                if (DBmodel != null)
                {
                    var CompanyDto = _mapper.Map<CompanyDto>(DBmodel);
                    _response.Data = CompanyDto;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "Not Data";
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region InsertAsync(CompanyDto model)
        public async Task<IResponseDTO> Insert(CompanyDto model)
        {
            try
            {
                model.User.Role = "Company";
                var insertUser=await identityServices.
                    RegisterAsync(model.User.Role, model.User.FullName, model.User.UserName, model.User.Email, model.User.Password, model.User.Image, model.User.PhoneNumber);
             if(insertUser.Code == 200)
                {

                    var Dto = new Company();
                    Dto.ApplicationUserId = insertUser.Data;
                    Dto.CompanyDescription = model.CompanyDescription;
                    Dto.Latitude = model.Latitude;
                    Dto.Longitude = model.Longitude;

                    var DBmodel = _unitOfWork.Company.Insert(Dto);

                    var save = _unitOfWork.Save();

                    if (save == "200")
                    {
                        var CompanyDto = model;
                        _response.Data = CompanyDto;
                        _response.Code = 200;
                        _response.Message = "OK";
                    }
                    else
                    {
                        _response.Data = null;
                        _response.Message = save;
                        _response.Code = 400;

                    }
                }
               
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Update(CompanyDto model)
        public IResponseDTO Update(CompanyDto model)
        {
            try
            {
                
                var DbCompany = _mapper.Map<Company>(model);
                DbCompany.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Company.Update(DbCompany);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    _response.Data = model;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                
                   _response.Code = 400;
                    _response.Message = save;
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Delete(CompanyDto model)
        public IResponseDTO Delete(int id)
        {
            try
            {
               
                var DbCompany = _unitOfWork.Company.GetByID(id);
                DbCompany.IsDelete = true;
                DbCompany.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Company.Delete(DbCompany);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    _response.Data = id;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                  
                   _response.Code = 400;
                    _response.Message = save;
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }

       

       
        public IResponseDTO GetAvailableCompanyWithSupCompany()
        {
            throw new NotImplementedException();
        }

       

       
        #endregion
    }
}
