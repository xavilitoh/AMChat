using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AMChat.Helpers
{
    public class NotificationHelper
    {
        public static string ExcutePushNotification(string title, string msg, string fcmToken, object data, string tipo)
        {

            var serverKey = "AAAAJwBhwUw:APA91bEwB1FDM7pNVLdsqptBuYuCSZdjl8iczZ5btwRkAlZ1uq4cuQeDiEudo-ah8LsfeWhYL-ZBZscp-KBcc9Nn8I2W05jGHKVGlwumJWS6ALNp7QFHihDvRqR16CAgADPewpxSTeg5";
            var senderId = "167510131020";

            var result = "-1";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            httpWebRequest.Method = "POST";


            var payload = new
            {
                notification = new
                {
                    title = title,
                    body = msg,
                    sound = "default"
                },
                data = new
                {
                    info = data,
                    click_action = "FLUTTER_NOTIFICATION_CLICK",
                    tipo = tipo
                },
                to = fcmToken,
                priority = "high",
                content_available = true,

            };


            var serializer = new JavaScriptSerializer();

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = serializer.Serialize(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}
