Namespace Areas.Admin.Controllers
    Public Class SettingsController
        Inherits AdminController

        <HttpGet>
        Public Function Site() As ActionResult
            Return View((Settings.SiteName, Settings.Author))
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function Site(item1 As SiteName, item2 As Author) As ActionResult
            If ModelState.IsValid Then
                Settings.SiteName = item1
                Settings.Author = item2
                Alert("Настройки сайта сохранены.")
                Return RedirectToAction("site")
            End If
            Return View((item1, item2))
        End Function
    End Class
End Namespace