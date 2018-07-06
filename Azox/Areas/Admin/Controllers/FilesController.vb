Namespace Areas.Admin.Controllers
    <Authorize>
    Public Class FilesController
        Inherits Controller

        Function Index() As ActionResult
            Return View()
        End Function
    End Class
End Namespace