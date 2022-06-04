Imports System.Data.Entity
Imports System.Threading.Tasks

Public Class SettingManager
    Private Const aboutName = "About"

    Private ReadOnly Db As ApplicationDbContext

    Public Sub New(context As ApplicationDbContext)
        Db = context
    End Sub

    Public Async Function GetAboutAsync() As Task(Of AboutSetting)
        Dim about = Await Db.Settings.SingleOrDefaultAsync(Function(x) x.Name = aboutName)
        Return New AboutSetting With {.Content = about?.Value, .Description = about?.Description}
    End Function

    Public Async Function SaveAboutAsync(setting As AboutSetting) As Task
        Dim about = Await Db.Settings.SingleOrDefaultAsync(Function(x) x.Name = aboutName)
        If IsNothing(about) Then
            about = Db.Settings.Add(New Setting With {.Name = aboutName})
        End If
        about.Value = setting.Content
        about.Description = setting.Description
        Await Db.SaveChangesAsync
    End Function
End Class
