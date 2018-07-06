Public Module RouteConfig
    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

		routes.MapRoute(
			name:="Default",
			url:="{controller}/{action}/{id}",
			defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional},
			namespaces:={"Azox.Controllers"}
		)
	End Sub
End Module