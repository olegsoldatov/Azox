Namespace Areas.Admin
	Public Class AdminAreaRegistration
		Inherits AreaRegistration

		Public Overrides ReadOnly Property AreaName() As String
			Get
				Return "Admin"
			End Get
		End Property

		Public Overrides Sub RegisterArea(ByVal context As AreaRegistrationContext)
			context.MapRoute(
				"Admin",
				"admin/{controller}/{action}/{id}",
				New With {.controller = "dashboard", .action = "index", .id = UrlParameter.Optional},
				{"Azox.Areas.Admin.Controllers"}
			)
		End Sub
	End Class
End Namespace