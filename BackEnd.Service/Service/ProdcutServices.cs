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
   public class ProdcutServices : BaseServices, IProdcutServices
    {

        #region ServicesProdcut(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public ProdcutServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork, responseDTO, mapper)
        {


        }
        #endregion

        #region GetAll()
        public IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.Prodcut.Get(x => x.IsDelete == false,includeProperties: "ProductImages", page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<ShowProductDto>>(result);
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

 
        #region Remove(ProdcutDto model)
        public IResponseDTO Remove(ProductDto model)
        {
            try
            {
                var DBmodel = _mapper.Map<Product>(model);
                _unitOfWork.Prodcut.Delete(DBmodel);
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
                var DBmodel = _unitOfWork.Prodcut.Get(x => x.Id == id && x.IsDelete == false).FirstOrDefault();
                if (DBmodel != null)
                {
                    var ProdcutDto = _mapper.Map<ProductDto>(DBmodel);
                    _response.Data = ProdcutDto;
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

        #region InsertAsync(ProdcutDto model)
        public  IResponseDTO Insert(ProductDto model)
        {
            try
            {
                var Dto = _mapper.Map<Product>(model);
              //  Dto.CreationDate = DateTime.Now;

                var DBmodel =  _unitOfWork.Prodcut.Insert(Dto);

                var save =  _unitOfWork.Save();

                if (save == "200")
                {
                    var ProdcutDto = _mapper.Map<ProductDto>(Dto);
                    _response.Data = ProdcutDto;
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

        #region Update(ProdcutDto model)
        public IResponseDTO Update(ProductDto model)
        {
            try
            {
                
                var DbProdcut = _mapper.Map<Product>(model);
                DbProdcut.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Prodcut.Update(DbProdcut);
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

        #region Delete(ProdcutDto model)
        public IResponseDTO Delete(int id)
        {
            try
            {
               
                var DbProdcut = _unitOfWork.Prodcut.GetByID(id);
                DbProdcut.IsDelete = true;
                DbProdcut.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Prodcut.Delete(DbProdcut);
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

       

       
        public IResponseDTO GetAvailableProdcutWithSupProdcut()
        {
            throw new NotImplementedException();
        }

       

       
        #endregion
    }
}
