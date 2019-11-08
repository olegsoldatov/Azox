Namespace Areas.Admin
	Public Class AdminAreaRegistration
		Inherits AreaRegistration

		Public Overrides ReadOnly Property AreaName() As String
			Get
				Return "admin"
			End Get
		End Property

		Public Overrides Sub RegisterArea(ByVal context As AreaRegistrationContext)
			context.MapRoute(
				"Dashboard",
				"admin",
				New With {.controller = "dashboard", .action = "index"}
			)

			context.MapRoute(
				"Admin_default",
				"admin/{controller}/{action}/{id}",
				New With {.action = "index", .id = UrlParameter.Optional},
				{"Azox.Areas.Admin.Controllers"}
			)
		End Sub
	End Class
End Namespace