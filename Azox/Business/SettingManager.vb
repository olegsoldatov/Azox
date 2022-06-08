Imports System.Data.Entity
Imports System.Threading.Tasks

Public Class SettingManager
    Private Const generalName = "General"
    Private Const aboutName = "About"

    Private ReadOnly Db As ApplicationDbContext

    Public Sub New(context As ApplicationDbContext)
        Db = context
    End Sub

    Public Async Function GetGeneralAsync() As Task(Of GeneralSetting)
        Dim general = Await Db.Settings.SingleOrDefaultAsync(Function(x) x.Name = generalName)
        Return New GeneralSetting With {.Title = general?.Value, .Description = general?.Description}
    End Function

    Public Async Function SaveGeneralAsync(setting As GeneralSetting) As Task
        Dim general = Await Db.Settings.SingleOrDefaultAsync(Function(x) x.Name = generalName)
        If IsNothing(general) Then
            general = Db.Settings.Add(New Setting With {.Name = generalName})
        End If
        general.Value = setting.Title
        general.Description = setting.Description
        Await Db.SaveChangesAsync
    End Function

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
