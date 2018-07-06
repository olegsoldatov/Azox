Imports System.Web.Mvc

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
				"Admin_default",
				"Admin/{controller}/{action}/{id}",
				New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional},
				{"WebApplication1.Areas.Admin.Controllers"}
			)
		End Sub
    End Class
End Namespace