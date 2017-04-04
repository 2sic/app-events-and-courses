using DotNetNuke.Security;
using DotNetNuke.Web.Api;
using System.Web.Http;
using ToSic.SexyContent.WebApi;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Compilation;
using System.Runtime.CompilerServices;
using DotNetNuke.Services.Mail;

public class FormController : SxcApiController
{
    
	[HttpPost]
    [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Anonymous)]
    [ValidateAntiForgeryToken]
    public void ProcessForm([FromBody]Dictionary<string,object> contactFormRequest)
    {
        // 1. add IP / host, and save all fields
        // if you add fields to your content-type, just make sure they are 
        // in the request with the correct name, they will be added automatically
        contactFormRequest.Add("SubmitDate", DateTime.Now);
        contactFormRequest.Add("SenderIp", System.Web.HttpContext.Current.Request.UserHostAddress);
		contactFormRequest.Add("Status", "registered");
        App.Data.Create("CourseRegistration", contactFormRequest);


        // 2. assemble all settings to send the mail
        // background: some settings are made in this module,
        // but if they are missing we use fallback settings 
        // which are taken from the App.Settings
		var settings = new {
			MailFrom = App.Settings.MailFrom,
			OwnerMail = App.Settings.OwnerMail,
			OwnerMailCC = "",
			OwnerMailTemplateFile = App.Settings.OwnerMailTemplateFile,
			CustomerMailCC = "",
			CustomerMailTemplateFile = App.Settings.CustomerMailTemplateFile
		};
		

        // 3. Send Mail to owner
        var ownerMailFile = TemplateInstance(settings.OwnerMailTemplateFile);
        var ownerMail = ownerMailFile.Message(contactFormRequest, this).ToString();

        Mail.SendMail(settings.MailFrom, settings.OwnerMail, settings.OwnerMailCC, "", contactFormRequest["Mail"].ToString(), MailPriority.Normal,
            ownerMailFile.Subject(contactFormRequest, this), MailFormat.Html, System.Text.Encoding.UTF8, ownerMail, new string[0], "", "", "", "", false);

        //Mail.SendMail(settings.MailFrom, settings.OwnerMail, Content.OwnerMailCC, "", custMail, MailPriority.Normal,
        //        ownerSubj, MailFormat.Html, System.Text.Encoding.UTF8, ownerBody, new string[0], "", "", "", "", false);

        // 4. Send Mail to customer
        var customerMailFile = TemplateInstance(settings.CustomerMailTemplateFile);
        var customerMail = customerMailFile.Message(contactFormRequest, this).ToString();

        Mail.SendMail(settings.MailFrom, contactFormRequest["Mail"].ToString(), settings.CustomerMailCC, "", settings.OwnerMail, MailPriority.Normal,
            customerMailFile.Subject(contactFormRequest, this), MailFormat.Html, System.Text.Encoding.UTF8, customerMail, new string[0], "", "", "", "", false);
    }

    private dynamic TemplateInstance(string fileName)
    {
        var compiledType = BuildManager.GetCompiledType(System.IO.Path.Combine("~", App.Path, "email-templates", fileName));
        object objectValue = null;
        if (compiledType != null)
        {
            objectValue = RuntimeHelpers.GetObjectValue(Activator.CreateInstance(compiledType));
            return ((dynamic)objectValue);
        }
        throw new Exception("Error while creating mail template instance.");
    }

}