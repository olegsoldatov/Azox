Imports System.Web.Optimization
Imports Azox.Controllers

Public Class MvcApplication
    Inherits HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        ModelBinders.Binders.Add(GetType(Decimal), New DecimalModelBinder())
        ModelBinders.Binders.Add(GetType(Decimal?), New DecimalModelBinder())
    End Sub

    Protected Sub Application_Error()
        Dim extension = Server.GetLastError

        If TypeOf extension Is HttpException Then
            If CType(extension, HttpException).GetHttpCode() = 404 Then
                Response.Clear()
                Dim routeData = New RouteData
                routeData.Values.Add("controller", "ErrorController")
                routeData.Values.Add("action", "NotFound")
                Server.ClearError()
                Dim errorController As IController = New ErrorController()
                errorController.Execute(New RequestContext(New HttpContextWrapper(Context), routeData))
            End If
        End If
    End Sub
End Class
