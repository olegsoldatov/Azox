Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

''' <summary>
''' Модель данных телефона.
''' </summary>
Public Class Phone
    Inherits Setting

    <MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Описание")>
    Public Property Description As String
End Class

Partial Public Class ApplicationDbContext
    Public Property Phones As DbSet(Of Phone)
End Class
