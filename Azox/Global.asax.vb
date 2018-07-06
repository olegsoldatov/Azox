Imports System.Web.Optimization

Public Class MvcApplication
    Inherits HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    Protected Sub Application_Error()
        Dim exception = Server.GetLastError
        Response.Clear()
        Dim httpException As HttpException = exception
        Dim routeData = New RouteData
        routeData.Values.Add("controller", "ErrorController")

        If Not IsNothing(httpException) AndAlso httpException.GetHttpCode = 404 Then
            routeData.Values.Add("action", "NotFound")

            Server.ClearError()
            Dim errorController As IController = New Controllers.ErrorController()
            errorController.Execute(New RequestContext(New HttpContextWrapper(Context), routeData))
        End If
    End Sub
End Class
