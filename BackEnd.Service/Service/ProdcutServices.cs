using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.DAL.Entities;
using BackEnd.Service.DTO.Prodcuts;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.IO;
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
                var result = _unitOfWork.Prodcut.Get(x => x.IsDelete == false,includeProperties: "ProductImages,Company.User,Category", page: pageNumber, Take: pageSize).ToList();
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
               _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        public IResponseDTO GetAllArchive(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.Prodcut.Get(x => x.IsDelete == true, includeProperties: "ProductImages,Company.User,Category", page: pageNumber, Take: pageSize).ToList();
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
                _response.Code = 404;
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
                var DBmodel = _unitOfWork.Prodcut.Get(x => x.Id == id && x.IsDelete == false, includeProperties: "ProductImages,Company.User,Category").FirstOrDefault();
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
                _response.Code = 404;
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

        #region Update(ProdcutDto model)
        public IResponseDTO Update(ProductDto model)
        {
            try
            {
                var Product= _unitOfWork.Prodcut.Get(x => x.Id == model.Id, includeProperties: "ProductImages,Company.User,Category").FirstOrDefault();
                if (Product.ProductImages.Count >= 1)
                {
                    foreach (var Image in Product.ProductImages)
                    {
                        _unitOfWork.ProductImages.Delete(Image.Id);
                    }
                }
               
                var DbProdcut = _mapper.Map(model, Product);
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
        public IResponseDTO GetAllProdcutUser(int pageNumber = 0, int pageSize = 0,string ApplicationUserId="")
        {
            try
            {

                var result = _unitOfWork.ProductFavourite.Get(x=>x.ApplicationUserId==ApplicationUserId
                    && x.IsDelete == false
                ,
                    includeProperties: "Discounts.Product.ProductImages,Discounts.Product.Category,Discounts.Product.Company.User",
                    page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var Prodcut = result.Select(x=>x.Discounts).ToList();
                    var resultList = _mapper.Map<List<ShowListProductDto>>(Prodcut);

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
        private bool favouriteCheck(string ApplicationUserId, int? DiscountId) 
        {

            var favouriteList = _unitOfWork.ProductFavourite.Get(x => x.ApplicationUserId == ApplicationUserId && x.DiscountId == DiscountId).ToList();
            if (favouriteList != null && favouriteList.Count >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public IResponseDTO favourite(ProductsFavouriteVm ProductFavVm)
        {
            if (favouriteCheck(ProductFavVm.ApplicationUserId, ProductFavVm.DiscountId))
            {
                var Favourite = new ProductFavourite();
                Favourite.DiscountId = ProductFavVm.DiscountId;
                Favourite.ApplicationUserId = ProductFavVm.ApplicationUserId;
                _unitOfWork.ProductFavourite.Insert(Favourite);
             var Res=_unitOfWork.Save();
                if (Res == "200")
                {
                    return new ResponseDTO
                    {

                        Code = 200,
                        Message = "favourite offer success",
                        Data = ProductFavVm
                    };
                }
                else
                {
                    return new ResponseDTO
                    {

                        Code = 404,
                        Message = "Not Saved",
                        Data = ProductFavVm
                    };
                }
              
            }
            else
            {
                return new ResponseDTO
                {
                    Code = 404,
                    Message = "User Before Choose This Product as favourite",
                    Data = ProductFavVm
                };

            }

        }
        public IResponseDTO DeleteProductFavourite(string ApplicationUserId = "", int DiscountId = 0) 
        {
            try
            {

                var DbProdcut = _unitOfWork.ProductFavourite.Get(x=>x.ApplicationUserId==ApplicationUserId&&x.DiscountId==DiscountId).FirstOrDefault();
                DbProdcut.IsDelete = true;
                DbProdcut.LastEditDate = DateTime.UtcNow.AddHours(2);
                if(DbProdcut!=null)
                _unitOfWork.ProductFavourite.Delete(DbProdcut);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    _response.Data = DiscountId;
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
        #region Delete(ProdcutDto model)
        public IResponseDTO Delete(int id)
        {
            try
            {
               
                var DbProdcut = _unitOfWork.Prodcut.GetByID(id);
                DbProdcut.IsDelete = true;
                DbProdcut.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Prodcut.Update(DbProdcut);
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

        public IResponseDTO RemoveFromDB(int id) 
        {
            try
            {

                var DbProdcut = _unitOfWork.Prodcut.GetByID(id);
                DbProdcut.IsDelete = true;
                DbProdcut.LastEditDate = DateTime.UtcNow.AddHours(2);
                var Images = _unitOfWork.ProductImages.Get(x => x.ProductId == id);
                var ProductFavourites = _unitOfWork.ProductFavourite.Get(x => x.Discounts.ProductId == id);
                var DisCount = _unitOfWork.Discount.Get(x=>x.ProductId==id);
                if (DisCount.Count()>0)
                {
                    foreach (var Dis in DisCount)
                    {
                        _unitOfWork.Discount.Delete(Dis.Id);

                    }

                }
                if (ProductFavourites.Count() > 0)
                {
                    foreach (var Favourite in ProductFavourites)
                    {
                        _unitOfWork.ProductFavourite.Delete(Favourite.Id);

                    }
                }
                if (Images.Count() > 0)
                {
                    var folderName = Path.Combine("wwwroot/UploadFiles");
                    foreach (var Image in Images)
                    {
                        _unitOfWork.ProductImages.Delete(Image.Id);
                      
                      
                        var file = System.IO.Path.Combine(folderName, Image.ProductImage);
                        try
                        {
                            System.IO.File.Delete(file);
                        }
                        catch { }
                    }
                }
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
    }
}
