Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

''' <summary>
''' Модель данных телефона.
''' </summary>
Public Class Phone
    Inherits Setting
End Class

Partial Public Class ApplicationDbContext
    Public Property Phones As DbSet(Of Phone)
End Class
