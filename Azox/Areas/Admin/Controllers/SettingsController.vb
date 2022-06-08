Imports System.Data.Entity
Imports System.Threading.Tasks

Namespace Areas.Admin.Controllers
    Public Class SettingsController
        Inherits AdminController

        Public ReadOnly Property Db As ApplicationDbContext
        Private ReadOnly SettingManager As SettingManager

        Public Sub New(settingManager As SettingManager, context As ApplicationDbContext)
            Me.SettingManager = settingManager
            Db = context
        End Sub

        Public Async Function Index() As Task(Of ActionResult)
            Dim settings = Await Db.Settings.OrderBy(Function(x) x.Name).ToListAsync
            Return View(settings)
        End Function

        <HttpGet>
        Public Async Function About() As Task(Of ActionResult)
            ViewBag.Title = My.Settings.About
            Return View(Await SettingManager.GetAboutAsync())
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function About(setting As AboutSetting) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Await SettingManager.SaveAboutAsync(setting)
                Alert("Сохранено.")
                Return Redirect(Request.Url.PathAndQuery)
            End If
            ViewBag.Title = My.Settings.About
            Return View(setting)
        End Function

        <HttpGet>
        Public Async Function General() As Task(Of ActionResult)
            ViewBag.Title = My.Settings.General
            Return View(Await SettingManager.GetGeneralAsync())
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function General(setting As GeneralSetting) As Task(Of ActionResult)
            If ModelState.IsValid Then
                If ModelState.IsValid Then
                    Await SettingManager.SaveGeneralAsync(setting)
                    Alert("Сохранено.")
                    Return Redirect(Request.Url.PathAndQuery)
                End If
                ViewBag.Title = My.Settings.General
                Return Redirect(Request.Url.PathAndQuery)
            End If
            Return View(setting)
        End Function

        <HttpGet>
        Public Async Function Phones() As Task(Of ActionResult)
            ViewBag.Title = "Телефоны"
            Return View(Await Db.Phones.Select(Function(x) New PhoneEditViewModel With {.Phone = x.Value, .Ext = "", .Description = x.Description}).ToListAsync)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function Phones(form As FormCollection, phone As IEnumerable(Of String), ext As IEnumerable(Of String), description As IEnumerable(Of String)) As ActionResult

            Return Redirect(Request.Url.PathAndQuery)
        End Function
    End Class
End Namespace