using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Assignment3TryItWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            // Initialize session variables
            Session.Clear();
            Session["Address"] = "Boise Center, ID";
            Session["WsdlUrl"] = "https://digital.weather.gov/xml/SOAP_server/ndfdXMLserver.php?wsdl";
        }
    }
}
