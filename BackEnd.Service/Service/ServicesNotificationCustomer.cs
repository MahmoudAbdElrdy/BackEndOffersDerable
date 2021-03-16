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
                var result = _unitOfWork.NotificationClient.Get(x => x.IsDelete == false,includeProperties : "Client").ToList();
                if (result != null && result.Count > 0)
                {
                    var list = new List<NotificationDto>();
                    var titles = result.GroupBy(x => x.Title).Select(x => x.FirstOrDefault()).ToList();
                  
                    var Lists = _mapper.Map<List<NotificationDto>>(titles);
                    var resultList =new List<NotificationDto>();
                   
                    foreach (var item in Lists)
                    {
                        var Res2 = result.GroupBy(a => new ClientGroupingKey(a.ClientId, a.Title))
                .Where(a => a.Key.Title ==item.Title )
                .Select(a => a.Key.ClientId)
                .ToList();
                        var Res3 = result.GroupBy(a => new ClientGroupingKey2(a.ClientId, a.Title,a.Client.Token))
              .Where(a => a.Key.Title == item.Title)
              .Select(a => a.Key.Token)
              .ToList();
                        item.Clients = Res2;
                        item.Tokens = Res3;
                    }
                    _response.Data = resultList;
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

        #region GetAvailable()
        public IResponseDTO GetAvailable()
        {
            try
            {
                var result = _unitOfWork.NotificationClient.Get(x => x.IsAvailable == true && x.IsDelete == false).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<NotificationClientDto>>(result);
                    _response.Data = resultList;
                    _response.Code = 200;
                    _response.Message = "تمت العملية بنجاح";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "عفوا : لا توجد بيانات متاحة في الوقت الحالي";
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
        public IResponseDTO Remove(NotificationClientDto model)
        {
            try
            {
                var DBmodel = _mapper.Map<NotificationClient>(model);
                _unitOfWork.NotificationClient.Delete(DBmodel.NotificationCid);
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

        #region GetByIdAsync(Guid? id)
        public IResponseDTO GetByIdAsync(int? id)
        {
            try
            {
                var DBmodel = _unitOfWork.NotificationClient.Get(x => x.NotificationCid == id && x.IsDelete == false, includeProperties: "Client").FirstOrDefault();
                if (DBmodel != null)
                {
                    var NotificationClientDto = _mapper.Map<NotificationClientDto>(DBmodel);
                    _response.Data = NotificationClientDto;
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

        #region GetByClient(Guid id)
        public IResponseDTO GetByClient(int id)
        {
            try
            {
                var DBmodel = _unitOfWork.NotificationClient.Get(x => x.ClientId == id && x.IsDelete == false);
                if (DBmodel != null)
                {
                    var ConstructionLicenseDto = _mapper.Map<List<NotificationClient>>(DBmodel);
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
        public async Task<IResponseDTO> InsertAsync(NotificationClientDto model)
        {
            try
            {
                var DBClient = _unitOfWork.Client.Get(x => x.Id == model.ClientId).FirstOrDefault();
                var Dto = _mapper.Map<NotificationClient>(model);
                var DBmodel = await _unitOfWork.NotificationClient.InsertAsync(Dto);
                Helper.NotificationHelper.PushNotificationByFirebase(model.Content, model.Title, new List<string>() { DBClient.Code }, null);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    var NotificationClientDto = _mapper.Map<NotificationClientDto>(DBmodel);
                    _response.Data = NotificationClientDto;
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
        public async Task<IResponseDTO> InsertAsync(NotificationDto model)
        {
            try
            {
                foreach(var item in model.Clients)
                {
                    var DBClient = _unitOfWork.Client.Get(x => x.Id == item).FirstOrDefault();
                    var Dto = _mapper.Map<NotificationClient>(model);
                    var DBmodel = await _unitOfWork.NotificationClient.InsertAsync(Dto);
                    Helper.NotificationHelper.PushNotificationByFirebase(model.Content, model.Title, new List<string>() { DBClient.Token }, null);

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
        public IResponseDTO Update(NotificationClientDto model)
        {
            try
            {
                model.LastEditDate = DateTime.UtcNow.AddHours(2);
                var DbNotificationClient = _mapper.Map<NotificationClient>(model);
                _unitOfWork.NotificationClient.Update(DbNotificationClient);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
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

        #region Delete(NotificationClientDto model)
        public IResponseDTO Delete(int id) 
        {
            try
            {
              
              
                _unitOfWork.NotificationClient.Delete(id);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    _response.Data = id;
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
    public class ClientGroupingKey 
    {
        public ClientGroupingKey(int? ClientId, string Title) 
        {
            ClientId = ClientId;
            Title = Title;
        }

        public int? ClientId { get; }

        public string Title { get; }
    }
    public class ClientGroupingKey2
    {
        public ClientGroupingKey2(int? ClientId, string Title,string Token)
        {
            ClientId = ClientId;
            Title = Title;
            Token = Token;
        }

        public int? ClientId { get; }

        public string Title { get; }
        public string Token { get; } 
    }
}

