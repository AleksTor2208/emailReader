using GmailApiDemo;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GmailQuickstart
{
   class Program
   {
      static void Main(string[] args)
      {
         GmailService service = GmailServiceStore.GetGmailService();
         MessageReader.ProcessMessages(service);
         Console.Read();
      }

      

     // static void Deprecated()
      //{
      //   // Define parameters of request.
      //   UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

      //   var requestForMessages = service.Users.Messages.List("me");
      //   var firstMessageRequest = service.Users.Messages.Get("me", "16e667e327236390");

      //   firstMessageRequest.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Full;
      //   //request.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Raw;
      //   var executed = request.Execute();
      //   // List labels.
      //   //HERE WE GO TO GET RAW MESSAGE
      //   Message firstMessage = firstMessageRequest.Execute();
      //   //MessagePart messagePart = firstMessage.

      //   var a = firstMessage.Payload.Body.Data;
      //   string data = DecodeBase64String(a);
      //   File.WriteAllText(@"C:\Users\ASUS\Documents\FX\mail.html", data);
      //   Console.WriteLine(data);


      //   var messages = requestForMessages.Execute();

      //   IList<Label> labels = executed.Labels;
      //   Console.WriteLine("Labels:");
      //   if (labels != null && labels.Count > 0)
      //   {
      //      foreach (var labelItem in labels)
      //      {
      //         Console.WriteLine("{0}", labelItem.Name);
      //      }
      //   }
      //   else
      //   {
      //      Console.WriteLine("No labels found.");
      //   }
      //}
   }
}