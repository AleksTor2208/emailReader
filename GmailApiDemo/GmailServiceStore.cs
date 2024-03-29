﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;


namespace GmailApiDemo
{
   public class GmailServiceStore
   {
      // If modifying these scopes, delete your previously saved credentials
      // at ~/.credentials/gmail-dotnet-quickstart.json
      static string[] Scopes = { GmailService.Scope.GmailReadonly };
      static string ApplicationName = "Gmail API .NET Quickstart";

      public static GmailService GetGmailService()
      {
         UserCredential credential;

         using (var stream =
             new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
         {
            // The file token.json stores the user's access and refresh tokens, and is created
            // automatically when the authorization flow completes for the first time.
            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            Console.WriteLine("Credential file saved to: " + credPath);
         }

         // Create Gmail API service.
         var service = new GmailService(new BaseClientService.Initializer()
         {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
         });
         return service;
      }
   }
}
