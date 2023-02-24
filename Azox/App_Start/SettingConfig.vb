Imports System.Threading.Tasks
Imports System.Data.Entity
Imports Soldata.Azox

''' <summary>
''' Предоставляет структурированные параметры приложения.
''' </summary>
Public Class Settings
    Private Shared _SiteName As SiteName
    Private Shared _Author As Author

    Private Shared Function GetSetting(Of T As {Setting, New})(ByRef setting As T) As T
        If IsNothing(setting) Then
            Using manager As New SettingManager
                setting = Task.Run(Function() manager.GetSettingAsync(Of T)).Result
            End Using
        End If
        Return setting
    End Function

    Private Shared Function SetSetting(Of T As {Setting})(setting As T) As T
        Using manager As New SettingManager
            Task.Run(Function() manager.UpdateAsync(setting))
            Return setting
        End Using
    End Function

    Public Shared Property SiteName As SiteName
        Get
            Return GetSetting(_SiteName)
        End Get
        Set(value As SiteName)
            _SiteName = SetSetting(value)
        End Set
    End Property

    Public Shared Property Author As Author
        Get
            Return GetSetting(_Author)
        End Get
        Set(value As Author)
            _Author = SetSetting(value)
        End Set
    End Property
End Class

Public Class SettingManager
    Inherits EntityManager(Of Setting)

    Public Sub New()
        MyBase.New(New EntityStore(Of Setting)(New ApplicationDbContext))
    End Sub

    Public Async Function GetSettingAsync(Of T As {Setting, New})() As Task(Of T)
        Dim setting = Await Store.Entities.OfType(Of T).FirstOrDefaultAsync
        If IsNothing(setting) Then
            setting = New T
            Await CreateAsync(setting)
        End If
        Return setting
    End Function
End Class
