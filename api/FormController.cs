// Add namespaces to enable security in Oqtane & Dnn despite the differences
#if NETCOREAPP
using Microsoft.AspNetCore.Authorization; // .net core [AllowAnonymous] & [Authorize]
using Microsoft.AspNetCore.Mvc;           // .net core [HttpGet] / [HttpPost] etc.
#else
// 2sxclint:disable:no-dnn-namespaces 2sxclint:disable:no-web-namespaces
using System.Web.Http;
#endif
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[AllowAnonymous]	// define that all commands can be accessed without a login
public class FormController : Custom.Hybrid.Api12
{
  [HttpPost]
  public void ProcessForm([FromBody]Dictionary<string,object> contactFormRequest)
  {
    // 1. add IP / host, and save all fields
    // if you add fields to your content-type, just make sure they are 
    // in the request with the correct name, they will be added automatically
    contactFormRequest.Add("SubmitDate", DateTime.Now);
    contactFormRequest.Add("SenderIp", System.Web.HttpContext.Current.Request.UserHostAddress);
    contactFormRequest.Add("Status", "registered");
    App.Data.Create("Registration", contactFormRequest);

    // added feature to create a full-save of each request into a system-protocol content-type
    contactFormRequest.Add("Timestamp", DateTime.Now);
    contactFormRequest.Add("ModuleId", CmsContext.Module.Id);
    contactFormRequest.Add("Title", "Form " + DateTime.Now.ToString("s"));
    // add raw-data, in case the content-type has a "RawData" field
    contactFormRequest.Add("RawData", Convert.Json.ToJson(contactFormRequest));
    App.Data.Create("SystemProtocol", contactFormRequest);

    var sendMail = CreateInstance("parts/SendMail.cs");
    sendMail.sendMails(contactFormRequest);        
  }
}