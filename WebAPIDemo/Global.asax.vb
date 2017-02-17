Imports System.Web.Http
Imports System.Web.Http.Filters
Imports System.Web.Optimization

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    'Public Shared Sub RegisterGlobalFilters(filters As GlobalFilterCollection)
    '    filters.Add(New RequireHttpsAttribute())
    'End Sub

End Class
