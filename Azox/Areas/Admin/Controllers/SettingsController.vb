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
                Return RedirectToAction("about")
            End If
            ViewBag.Title = My.Settings.About
            Return View(setting)
        End Function

        <HttpGet>
        Public Async Function General() As Task(Of ActionResult)
            Dim model = New GeneralSetting

            Dim settings = Await Db.Settings.Where(Function(x) x.Name = NameOf(model.Title) Or x.Name = NameOf(model.Description)).ToListAsync

            model.Title = settings.SingleOrDefault(Function(x) x.Name = NameOf(model.Title))?.Value
            model.Description = settings.SingleOrDefault(Function(x) x.Name = NameOf(model.Description))?.Value
            Return View(model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function General(model As GeneralSetting) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim title = Await Db.Settings.SingleOrDefaultAsync(Function(x) x.Name = NameOf(model.Title))
                If IsNothing(title) Then
                    title = Db.Settings.Add(New Setting With {.Name = NameOf(model.Title)})
                End If
                title.Value = model.Title

                Dim description = Await Db.Settings.SingleOrDefaultAsync(Function(x) x.Name = NameOf(model.Description))
                If IsNothing(description) Then
                    description = Db.Settings.Add(New Setting With {.Name = NameOf(model.Description)})
                End If
                description.Value = model.Description

                Await Db.SaveChangesAsync
                Alert("Параметры сохранены.")
                Return Redirect(Request.Url.PathAndQuery)
            End If
            Return View(model)
        End Function
    End Class
End Namespace