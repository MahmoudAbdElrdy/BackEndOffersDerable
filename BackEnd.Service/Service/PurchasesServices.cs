using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.Service.DTO.Client;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackEnd.DAL.Entities;

namespace BackEnd.Service.Service
{
  public  class PurchasesServices : BaseServices, IServicesPurchases
    {

        #region ServicesDiscount(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public PurchasesServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork, responseDTO, mapper)
        {


        }

        public IResponseDTO GetPurchasesByUserId(int pageNumber = 0, int pageSize = 0, string ApplicationUserId = "")
        {
            try
            {
                var result = _unitOfWork.
                    Purchases.
                    Get(x => x.Client.ApplicationUserId==ApplicationUserId, includeProperties: "Client.User,Product.Company.User", page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<ShowPurchasesDto>>(result);
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

        public IResponseDTO Insert(PurchasesDto entity)
        {
            try
            {
                var Dto = _unitOfWork.Discount.Get(x => x.Id == entity.DiscountId).FirstOrDefault();
                var Client = _unitOfWork.Client.GetEntity(x=>x.ApplicationUserId==entity.ApplicationUserId);
                if (Client == null)
                {
                    Client.ApplicationUserId = entity.ApplicationUserId;
                    Client.Code = entity.RandomCode;
                   _unitOfWork.Client.Insert(Client);
                }
                else
                {
                    Client.Code = entity.RandomCode;
                    _unitOfWork.Client.Update(Client);

                    
                }
                Purchases Purchases = new Purchases() 
                {
                    PurchaseDate=DateTime.Now,
                    DiscountRate=Dto.DiscountRate,
                    DiscountDescription = Dto.DiscountDescription,
                    DiscountType = Dto.DiscountType,
                    DiscountValue = Dto.DiscountValue,
                    EndDate = Dto.EndDate,
                    SatrtDate = Dto.SatrtDate,
                    ProductId = Dto.ProductId,
                    quantity = entity.quantity,
                    ClientId= Client.Id,
                    NewPrice= entity.NewPrice ,
                    DiscountId=entity.DiscountId
                };
              //  Purchases
                var DBmodel = _unitOfWork.Purchases.Insert(Purchases);

                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    
                    _response.Data = entity;
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
        #region GetAll()
        public IResponseDTO GetAllClient(int pageNumber = 0, int pageSize = 0) 
        {
            try
            {
                var result = _unitOfWork.Client.Get(x => x.IsDelete == false, includeProperties: "User", page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<ClientDto>>(result);
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
    }
}
