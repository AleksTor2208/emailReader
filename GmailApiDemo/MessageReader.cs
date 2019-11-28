using System;
using System.Text;
using GmailApiDemo;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System.IO;
using System.Linq;

namespace GmailQuickstart
{
   internal class MessageReader
   {
      public static void ProcessMessages(GmailService service)
      {
         //todo:
         //- 
         string lastMessageId = string.Empty;
         while (true)
         {
            var messages = service.Users.Messages.List("me").Execute();
            var id = messages.Messages.ToList().First().Id;
            if (lastMessageId == id)
            {
               System.Threading.Thread.Sleep(1000);
               continue;
            }
            var lastMessageRequest = service.Users.Messages.Get("me", id);
            lastMessageRequest.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Full;
            var lastMesResult = lastMessageRequest.Execute();

            if (IsOldMessage(lastMesResult))
            {

            }
            else
            {
            }

            if (IsOldMessage(lastMesResult))
            {
               System.Threading.Thread.Sleep(1000);
               continue;
            }
            if (MessageIsSignal(lastMesResult) && MessageIsValid(lastMesResult))
            {
               var decryptedMessage1 = DecodeBase64String(lastMesResult.Payload.Parts[0].Body.Data);
               var decryptedMessage2 = DecodeBase64String(lastMesResult.Payload.Parts[1].Body.Data);
               WriteMessage(decryptedMessage1);
            }
            lastMessageId = id;
         }
      }

      private static bool IsOldMessage(Message message)
      {
         //var to_date = DateTimeOffset.FromUnixTimeMilliseconds(lastMesResult.InternalDate.Value).DateTime;
         //var deliveryTime = new DateTime(message.InternalDate.Value);
         var deliveryTime = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(message.InternalDate.Value.ToString()));
         var timeSlippage = DateTime.Now.AddHours(-1).AddSeconds(-5);
         return deliveryTime <= timeSlippage;
      }

      private static bool MessageIsSignal(Message message)
      {
         return message.Snippet.Contains("signal");
      }

      private static void WriteMessage(string message)
      {
         var dirPpath = @"C:\Users\ASUS\Documents\FX\Messages";
         if (!Directory.Exists(dirPpath))
         {
            Directory.CreateDirectory(dirPpath);
         }

         string currentFormattedDate = string.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now);
         var filePath = $"{dirPpath}\\{currentFormattedDate}_TSignal.txt";
         File.WriteAllText(filePath, message);
      }

      private static bool MessageIsValid(Message message)
      {
         //what is a valid message?
         //As a start - check snippet if it contains word 'valid'
         return message.Snippet.Contains("valid");
      }

      static string DecodeBase64String(string s)
      {
         var ts = s.Replace("-", "+");
         ts = ts.Replace("_", "/");
         var bc = Convert.FromBase64String(ts);
         var tts = Encoding.UTF8.GetString(bc);

         return tts;
      }
   }
}