using Autofac;
using Autofac.Integration.Wcf;
using DotNetNuke.ComponentModel;
using System;
using System.Web.Mvc;

namespace Host.WcfService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Autofac.IContainer container = MainModule.BuildContainer();
            AutofacHostFactory.Container = container;
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}