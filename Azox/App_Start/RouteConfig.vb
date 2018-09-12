Public Module RouteConfig
    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

		routes.MapRoute(
			name:="Default",
			url:="{controller}/{action}/{id}",
			defaults:=New With {.controller = "home", .action = "index", .id = UrlParameter.Optional},
			namespaces:={"Azox.Controllers"}
		)
	End Sub
End Module