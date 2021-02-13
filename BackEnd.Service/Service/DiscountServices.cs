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
   public class DiscountServices : BaseServices, IServiceDiscount
    {

        #region ServicesDiscount(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public DiscountServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork, responseDTO, mapper)
        {


        }
        #endregion

        #region GetAll()
        public IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.Discount.Get(x => x.IsDelete == false, page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<DiscountDto>>(result);
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
               _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

 
        #region Remove(DiscountDto model)
        public IResponseDTO Remove(DiscountDto model)
        {
            try
            {
                var DBmodel = _mapper.Map<Discount>(model);
                _unitOfWork.Discount.Delete(DBmodel);
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
                   _response.Code = 404;
                   
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 404;
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
                var DBmodel = _unitOfWork.Discount.Get(x => x.Id == id && x.IsDelete == false).FirstOrDefault();
                if (DBmodel != null)
                {
                    var DiscountDto = _mapper.Map<DiscountDto>(DBmodel);
                    _response.Data = DiscountDto;
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
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region InsertAsync(DiscountDto model)
        public  IResponseDTO Insert(DiscountDto model)
        {
            try
            {
                var Dto = _mapper.Map<Discount>(model);
              //  Dto.CreationDate = DateTime.Now;

                var DBmodel =  _unitOfWork.Discount.Insert(Dto);

                var save =  _unitOfWork.Save();

                if (save == "200")
                {
                    var DiscountDto = _mapper.Map<DiscountDto>(Dto);
                    _response.Data = DiscountDto;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                    _response.Message = save;
                   _response.Code = 404;
                 
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Update(DiscountDto model)
        public IResponseDTO Update(DiscountDto model)
        {
            try
            {
                
                var DbDiscount = _mapper.Map<Discount>(model);
                DbDiscount.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Discount.Update(DbDiscount);
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
                
                   _response.Code = 404;
                    _response.Message = save;
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Delete(DiscountDto model)
        public IResponseDTO Delete(int id)
        {
            try
            {
               
                var DbDiscount = _unitOfWork.Discount.GetByID(id);
                DbDiscount.IsDelete = true;
                DbDiscount.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Discount.Delete(DbDiscount);
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
                  
                   _response.Code = 404;
                    _response.Message = save;
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }

       

       
        public IResponseDTO GetAvailableDiscountWithSupDiscount()
        {
            throw new NotImplementedException();
        }




        #endregion

        public IResponseDTO GetAllProdcut(int pageNumber = 0, int pageSize = 0, int CategoryId = 0)
        {
            try 
            {
               
                var result = _unitOfWork.Discount.Get(filter:
                    x => (CategoryId !=0? 
                    x.Product.CategoryId == (int?)CategoryId : true)
                    && x.IsDelete == false
                ,
                    includeProperties: "Product.ProductImages,Product.Category", 
                    page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<ShowListProductDto>>(result);
                    
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
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public IResponseDTO GetProdcutById(int? id) 
        {
            try
            {
                var DBmodel = _unitOfWork.Discount.Get(x => x.Id == id && x.IsDelete == false, includeProperties: "Product.ProductImages,Product.Category,Product.Company.User").FirstOrDefault();
                if (DBmodel != null)
                {
                    var DiscountDto = _mapper.Map<ShowDiscountDto>(DBmodel);
                    _response.Data = DiscountDto;
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
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        public IResponseDTO GetDiscountByProduct(int? ProductId)
        {
            try
            {
                var DBmodel = _unitOfWork.Discount.Get(x => x.ProductId == ProductId && x.IsDelete == false).FirstOrDefault();
                if (DBmodel != null)
                {
                    var DiscountDto = _mapper.Map<DiscountDto>(DBmodel);
                    _response.Data = DiscountDto;
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
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
