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
		Dim httpException As HttpException = exception

		If Not IsNothing(httpException) Then
			Response.Clear()
			Dim routeData = New RouteData
			routeData.Values.Add("controller", "ErrorController")

			If httpException.GetHttpCode = 404 Then
				routeData.Values.Add("action", "NotFound")
			ElseIf httpException.WebEventCode = Management.WebEventCodes.RuntimeErrorPostTooLarge Then
				routeData.Values.Add("action", "MaxRequest")
			End If

			Server.ClearError()
			Dim errorController As IController = New Controllers.ErrorController()
			errorController.Execute(New RequestContext(New HttpContextWrapper(Context), routeData))
		End If
	End Sub
End Class
