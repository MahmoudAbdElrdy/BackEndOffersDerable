using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.Service.DTO;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Service.DTO.Client;

namespace BackEnd.Service.Service
{
    public class ServicesNotificationCustomer : BaseServices, IServicesNotificationCustomer
    {

        #region ServicesNotificationClient(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public ServicesNotificationCustomer(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork, responseDTO, mapper)
        {


        }
        #endregion

        #region GetAll()
        public IResponseDTO GetAll()
        {
            try
            {
                var result = _unitOfWork.NotificationClient.Get(includeProperties : "Client").ToList();
                if (result != null && result.Count > 0)
                {
                    var list = new List<NotificationDto>();
                    var titles = result.GroupBy(x => x.Title).Select(x => x.FirstOrDefault()).ToList();
                  
                    var Lists = _mapper.Map<List<NotificationDto>>(titles);
                    var resultList =new List<NotificationDto>();
                   
                    foreach (var item in Lists)
                    {
                        var Users = result.Select(x => x).Where(x=>x.Title==item.Title).ToList();
                        var Clients = Users.Select(x => x.ClientId).ToList();
                        var Tokenes = Users.Select(x => x.Client.Token).ToList();
              
                        item.Clients = Clients;
                        item.Tokens = Tokenes;
                    }
                    _response.Data = Lists;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 404;
                    _response.Message = "No Data";
                }                
            }
            catch (Exception ex)
            { 
                var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                _response.Data = null;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

      
        #region Remove(NotificationClientDto model)
        public IResponseDTO Remove(int NotificationCid)
        {
            try
            {
               
                _unitOfWork.NotificationClient.Delete(NotificationCid);
                var save = _unitOfWork.Save();
                if (save == "200")
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "تمت العملية بنجاح";
                }
                else
                {
                    _response.Data = null;
                         _response.Code = 404;
                    _response.Message = "عفوا : حدث خطأ حاول مرة اخرى  !!!";
                }
            }
            catch (Exception ex)
            {
                var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                _response.Data = null;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

      
        #region GetByClient(Guid id)
        public IResponseDTO GetByClient(string ApplicationUserId)
        {
            try
            {
                var DBmodel = _unitOfWork.NotificationClient.Get(x => x.Client.ApplicationUserId == ApplicationUserId);
                if (DBmodel != null)
                {
                    var ConstructionLicenseDto = _mapper.Map<List<NotificationClientDto>>(DBmodel);
                    _response.Data = ConstructionLicenseDto;
                    _response.Code = 200;
                    _response.Message = "تمت العملية بنجاح";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "عفوا : البيانات المرسلة غير صحيحة أو غير كافية لم يتم العثور على المطلوب !!!";
                }
            }
            catch (Exception ex)
            {
                var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                _response.Data = null;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region InsertAsync(NotificationClientDto model)
        //public async Task<IResponseDTO> InsertAsync(NotificationClientDto model)
        //{
        //    try
        //    {
        //        var DBClient = _unitOfWork.Client.Get(x => x.Id == model.ClientId).FirstOrDefault();
        //        var Dto = _mapper.Map<NotificationClient>(model);
        //        var DBmodel = await _unitOfWork.NotificationClient.InsertAsync(Dto);
        //        Helper.NotificationHelper.PushNotificationByFirebase(model.Content, model.Title,0,  DBClient.Token , null);
        //        var save = _unitOfWork.Save();

        //        if (save == "200")
        //        {
        //            var NotificationClientDto = _mapper.Map<NotificationClientDto>(DBmodel);
        //            _response.Data = NotificationClientDto;
        //            _response.Code = 200;
        //            _response.Message = "تمت العملية بنجاح";
        //        }
        //        else
        //        {
        //            _response.Data = null;
        //             _response.Code = 404;
        //            _response.Message = "عفوا : حدث خطأ حاول مرة اخرى  !!!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
        //        _response.Data = null;
        //        _response.Code = 404;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}
        public async Task<IResponseDTO> InsertAsync(NotificationDto model)
        {
            try
            {
                foreach(var item in model.Clients)
                {
                    var DBClient = _unitOfWork.Client.Get(x => x.Id == item).FirstOrDefault();
                    var Dto =new NotificationClient();
                    Dto.ClientId = item;
                    Dto.Content = model.Content;
                    Dto.Title = model.Title;
                    Dto.CreationDate = DateTime.Now;
                    var DBmodel = await _unitOfWork.NotificationClient.InsertAsync(Dto);
                    Helper.NotificationHelper.PushNotificationByFirebase(model.Content, model.Title,0, DBClient.Token, null);

                }
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                   // var NotificationClientDto = _mapper.Map<NotificationClientDto>(DBmodel);
                    _response.Data = model;
                    _response.Code = 200;
                    _response.Message = "تمت العملية بنجاح";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 404;
                    _response.Message = "عفوا : حدث خطأ حاول مرة اخرى  !!!";
                }
            }
            catch (Exception ex)
            {
                var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                _response.Data = null;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }

        #endregion

        #region Update(NotificationClientDto model)
        //public IResponseDTO Update(NotificationClientDto model)
        //{
        //    try
        //    {
        //        model.LastEditDate = DateTime.UtcNow.AddHours(2);
        //        var DbNotificationClient = _mapper.Map<NotificationClient>(model);
        //        _unitOfWork.NotificationClient.Update(DbNotificationClient);
        //        var save = _unitOfWork.Save();

        //        if (save == "200")
        //        {
        //            _response.Data = model;
        //            _response.Code = 200;
        //            _response.Message = "تمت العملية بنجاح";
        //        }
        //        else
        //        {
        //            _response.Data = null;
        //               _response.Code = 404;
        //            _response.Message = "عفوا : حدث خطأ حاول مرة اخرى  !!!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
        //        _response.Data = null;
        //        _response.Code = 404;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}
        #endregion

        #region Delete(NotificationClientDto model)
        public IResponseDTO Delete(int NotificationCid) 
        {
            try
            {
              
              
                _unitOfWork.NotificationClient.Delete(NotificationCid);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    _response.Data = NotificationCid;
                    _response.Code = 200;
                    _response.Message = "تمت العملية بنجاح";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 404;
                     _response.Message = "عفوا : حدث خطأ حاول مرة اخرى  !!!";
                }
            }
            catch (Exception ex)
            {
                var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                _response.Data = null;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        public IResponseDTO DeleteNotification(NotificationDto model)  
        {
            try
            {
                foreach (var item in model.Clients)
                {
                    var DBClient = _unitOfWork.NotificationClient.Get(x => x.ClientId == item&&x.Title==model.Title).FirstOrDefault();
                    if(DBClient!=null)
                    _unitOfWork.NotificationClient.Delete(DBClient);
                  
                }
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    // var NotificationClientDto = _mapper.Map<NotificationClientDto>(DBmodel);
                    _response.Data = model;
                    _response.Code = 200;
                    _response.Message = "تمت العملية بنجاح";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 404;
                    _response.Message = "عفوا : حدث خطأ حاول مرة اخرى  !!!";
                }
            }
            catch (Exception ex)
            {
                var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                _response.Data = null;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion
    }
  
}

