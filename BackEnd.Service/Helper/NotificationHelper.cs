using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BackEnd.Service.Helper
{
    public class NotificationHelper
    {
        //private static string FirebaseApplicationID = "AAAALeyliNM:APA91bH0CTQajX1fVlbSd0HN-4Hf4VyiQEzwgj3C8Tzk1Gc0FAfxmi3AE-uFgDmg8u_53kaRenxWhCeE4okyjzsRLkmxexhcanxmbgz81s1sDDSUL_z7_1r3IRDCNiyVAh27nGPeZfwK";
        //private static string FirebaseSenderId = "197243799763";
        private static string FirebaseApplicationID = "AAAAo1KtTy0:APA91bFYOVOt6bneOun3YM5AbTlWse60qJlcQQBHfKD6BDJZoTowgULp54tszIwHJ6pUstR_uznuMIPHLZd37kzOX9my2TLFPF6LK8pBrsRAyEcIwCBQGvgqXyyvW6LQnpiWZvfQ3_9O";
        private static string FirebaseSenderId = "701466758957";
        public static void PushNotificationByFirebase(string englishMessage, string title, List<string> player_Id, Dictionary<string, object> AdditionalData, int second = 0)
        {
            try
            {
                if (AdditionalData == null)
                {
                    AdditionalData = new Dictionary<string, object>()
                    {
                        { "message" , englishMessage },
                        { "other_key" , true },
                        { "title" , title },
                        { "body", englishMessage },
                        { "badge" , 1 },
                        { "sound" ,"default" },
                        { "content_available" , true },
                        { "timestamp" , DateTime.UtcNow.AddHours(2).ToString() }
                    };
                }
                else
                {
                    AdditionalData.Add("message", englishMessage);
                    AdditionalData.Add("other_key", true);
                    AdditionalData.Add("title", title);
                    AdditionalData.Add("body", englishMessage);
                    AdditionalData.Add("badge", 1);
                    AdditionalData.Add("sound", "default");
                    AdditionalData.Add("content_available", true);
                    AdditionalData.Add("timestamp", DateTime.UtcNow.AddHours(2).ToString());
                }
                player_Id = player_Id.Where(pId => pId != null && pId.Length > 9).ToList();
                foreach (var deviceId in player_Id)
                {
                    try
                    {
                        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                        tRequest.Method = "post";
                        tRequest.ContentType = "application/json";
                        //AdditionalData.Add("userToken", deviceId);
                        var data = new
                        {
                            to = deviceId,
                            priority = "high",
                            content_available = true,
                            notification = new
                            {
                                body = englishMessage,
                                title = title,
                                badge = 1,
                                sound = "default",
                                content_available = true
                            },
                            data = AdditionalData,
                            apns = new
                            {
                                payload = new
                                {
                                    aps = new
                                    {
                                        sound = "default",
                                        content_available = true,
                                        body = englishMessage,
                                        message = englishMessage,
                                        title = title,
                                        badge = 1,
                                    },
                                },
                                customKey = "test app",
                            }

                        };
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                        tRequest.Headers.Add(string.Format("Authorization: key={0}", FirebaseApplicationID));
                        tRequest.Headers.Add(string.Format("Sender: id={0}", FirebaseSenderId));
                        tRequest.ContentLength = byteArray.Length;
                        using (Stream dataStream = tRequest.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);
                            using (WebResponse tResponse = tRequest.GetResponse())
                            {
                                using (Stream dataStreamResponse = tResponse.GetResponseStream())
                                {
                                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                    {
                                        String sResponseFromServer = tReader.ReadToEnd();
                                        string str = sResponseFromServer;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string Error = string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                        //System.Diagnostics.Debug.WriteLine(Error);
                        string tokens = "tokens is : (" + deviceId + ")";
                        System.Diagnostics.Debug.WriteLine(string.Format("{0} :::: {1}", Error, tokens), DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                string Error = string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                //System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : ""));
                string tokens = "tokens is : (";
                foreach (var item in player_Id)
                {
                    tokens += "{" + item + "}   ";
                }
                tokens += "  )";
                System.Diagnostics.Debug.WriteLine(string.Format("{0} :::: {1}", Error, tokens), DateTime.Now);
            }
        }
    }
}