using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.Service.DTO.Client;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackEnd.DAL.Entities;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BackEnd.Service.Service

{
  public  class PurchasesServices : BaseServices, IServicesPurchases
    {
        private readonly Random _random = new Random();
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
        public IResponseDTO GetPurchases(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.
                    Purchases.
                    Get( includeProperties: "Client.User,Product.Company.User,Product.ProductImages", page: pageNumber, Take: pageSize).ToList();
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
                    Client = new Client();
                    Client.ApplicationUserId = entity.ApplicationUserId;
                    Client.Code = _random.Next().ToString() ;
                   //_unitOfWork.Client.Insert(Client);
                    Client.Id = _unitOfWork.Client.Add(Client);
                }
                else
                {
                    Client.Code = _random.Next().ToString();
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
                   // quantity = entity.quantity,
                    ClientId= Client.Id,
                    NewPrice= entity.NewPrice ,
                    DiscountId=entity.DiscountId,
                    StatusReceived=false
                };
              //  Purchases
                var DBmodel = _unitOfWork.Purchases.Insert(Purchases);
                var save = _unitOfWork.Save();
                if (save == "200")
                {

                    var Respones = SendMessage(entity.PhoneNumber, Client.Code);
                    if (Respones.Code == 200)
                    {

                        _response.Data = entity;
                        _response.Message = "Saved";
                        _response.Code = 200;
                       

                    }
                    else
                    {
                        _response.Data = entity;
                        _response.Message = "Saved";
                        _response.Code = 200;

                    }
                    return _response;

                }

                else
                {
                    var Respones = SendMessage(entity.PhoneNumber, Client.Code);
                    if (Respones.Code == 200)
                    {

                        _response.Data = entity;
                        _response.Message = "Not Saved";
                        _response.Code = 404;


                    }
                    else
                    {
                        _response.Data = entity;
                        _response.Message = "Not Saved";
                        _response.Code = 404;

                    }
                    return _response;

                }
               


            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 404;
                _response.Message = ex.Message;
                return _response;
            }
          
        }
        #endregion
        #region GetAll()
        public IResponseDTO SendMessage(string PhoneNumber, string Code)
        {
            string myURI = "https://api.bulksms.com/v1/messages";

            // change these values to match your own account
            string myUsername = "dealsapp2";
            string myPassword = "DealsApp123";

            // the details of the message we want to send
            // String strPost = "?user=9030888111&password=password&msg=" + message + "&sender=OPTINS" + "&mobile=mobnum" + "&type=1";
            string myData = "{to: " + AddDoubleQuotes(PhoneNumber) + ", body:"+AddDoubleCode(Code)+"}";
            //string myData = "" +
            //    "{to: \"+'PhoneNumber'+\"," +
            //    " body:\"تم تاكيد شراء العرض  توجه الي التاجر لاستلام العرض  ومعك كود العرض:+'Code'+ \"}";
            //          
            var request = WebRequest.Create(myURI);

            // supply the credentials
            request.Credentials = new NetworkCredential(myUsername, myPassword);
            request.PreAuthenticate = true;
            // we want to use HTTP POST
            request.Method = "POST";
            // for this API, the type must always be JSON
            request.ContentType = "application/json";

            // Here we use Unicode encoding, but ASCIIEncoding would also work
            var encoding = new UnicodeEncoding();
            var encodedData = encoding.GetBytes(myData);

            // Write the data to the request stream
            var stream = request.GetRequestStream();
            stream.Write(encodedData, 0, encodedData.Length);
            stream.Close();

            // try ... catch to handle errors nicely
            try
            {
                // make the call to the API
                var response = request.GetResponse();

                // read the response and print it to the console
                var reader = new StreamReader(response.GetResponseStream());
                Console.WriteLine(reader.ReadToEnd());
                return new ResponseDTO()
                {
                    Data = reader.ReadToEnd(),
                    Code = 200,
                    Message = "Sent Successfully"
                };
            }
            catch (WebException ex)
            {
                // show the general message
                Console.WriteLine("An error occurred:" + ex.Message);

                // print the detail that comes with the error
                var reader = new StreamReader(ex.Response.GetResponseStream());
                Console.WriteLine("Error details:" + reader.ReadToEnd());
                
               // dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(res);

                _response.Data = reader.ReadToEnd();
                _response.Message = ex.Message;
                _response.Code = 404;
                return _response;
            } 

        }
        public  string AddDoubleQuotes( string value)
    {
        return "\"" + value + "\"";
    }
        public string AddDoubleCode(string value)
        {
            return "\"تم تاكيد شراء العرض  توجه الي التاجر لاستلام العرض  ومعك كود العرض:" + value + "\"";
        }
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
