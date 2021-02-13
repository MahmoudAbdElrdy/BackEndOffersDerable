using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.DAL.Entities;
using BackEnd.Service.DTO.Prodcuts;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
   public class RatingServices : BaseServices, IRatingServices
    {

        #region ServicesRating(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public RatingServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork, responseDTO, mapper)
        {


        }
        #endregion

        #region GetAll()
        public IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.Rating.Get(x => x.IsDelete == false,includeProperties: "Client.User", page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<ShowRatingDto>>(result);
                    _response.Data = resultList;
                    _response.Code = 200;
                    _response.Message = "OK";
                    _response.totalRowCount = _unitOfWork.Rating.Get(x => x.IsDelete == false).Count();
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "No Data";
                    _response.totalRowCount = 0;
                }
            }
            catch (Exception ex)
            {
             
                _response.Data = null;
               _response.Code = 400;
                _response.Message = ex.Message;
                _response.totalRowCount = 0;
            }
            return _response;
        }

        public IResponseDTO GetAllRatingByDiscountId(int pageNumber = 0, int pageSize = 0,int DiscountId = 0) 
        {
            try
            {
                var result = _unitOfWork.Rating.Get(x => x.IsDelete == false&&x.DiscountId== DiscountId, includeProperties: "Client.User", page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<ShowRatingDto>>(result);
                    _response.Data = resultList;
                    _response.Code = 200;
                    _response.Message = "OK";
                    _response.totalRowCount = _unitOfWork.Rating.Get(x => x.IsDelete == false).Count();
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "No Data";
                    _response.totalRowCount = 0;
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
                _response.totalRowCount = 0;
            }
            return _response;
        }
        #endregion


        #region Remove(RatingDto model)
        public IResponseDTO Remove(RatingDto model)
        {
            try
            {
                var DBmodel = _mapper.Map<Rating>(model);
                _unitOfWork.Rating.Delete(DBmodel);
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
                var DBmodel = _unitOfWork.Rating.Get(x => x.Id == id && x.IsDelete == false, includeProperties: "Client.User").FirstOrDefault();
                if (DBmodel != null)
                {
                    var RatingDto = _mapper.Map<ShowRatingDto>(DBmodel);
                    _response.Data = RatingDto;
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

        #region InsertAsync(RatingDto model)
        public  IResponseDTO Insert(RatingDto model)
        {
            try
            {
                var Dto = _mapper.Map<Rating>(model);
              //  Dto.CreationDate = DateTime.Now;

                var DBmodel =  _unitOfWork.Rating.Insert(Dto);

                var save =  _unitOfWork.Save();

                if (save == "200")
                {
                    var RatingDto = _mapper.Map<RatingDto>(Dto);
                    _response.Data = RatingDto;
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

        #region Update(RatingDto model)
        public IResponseDTO Update(RatingDto model)
        {
            try
            {
                
                var DbRating = _mapper.Map<Rating>(model);
                DbRating.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Rating.Update(DbRating);
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

        #region Delete(RatingDto model)
        public IResponseDTO Delete(int id)
        {
            try
            {
               
                var DbRating = _unitOfWork.Rating.GetByID(id);
                DbRating.IsDelete = true;
                DbRating.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Rating.Update(DbRating);
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

       

       
        public IResponseDTO GetAvailableRatingWithSupRating()
        {
            throw new NotImplementedException();
        }

       

       
        #endregion
    }
}
