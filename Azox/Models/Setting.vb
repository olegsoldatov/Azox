Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

''' <summary>
''' Настройка.
''' </summary>
Public MustInherit Class Setting
    Inherits Entity

    <AllowHtml>
    <Display(Name:="Значение")>
    Public Property Value As String

    <AllowHtml>
    <Display(Name:="Описание")>
    Public Property Description As String
End Class

Partial Public Class ApplicationDbContext
    Public Property Settings As DbSet(Of Setting)
End Class

Public Class SiteName
    Inherits Setting
End Class

Public Class Author
    Inherits Setting
End Class
