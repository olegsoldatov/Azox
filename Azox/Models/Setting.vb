Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

''' <summary>
''' Модель данных параметра.
''' </summary>
Public Class Setting
    <Key>
    <MaxLength(128, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Имя")>
    Public Property Name As String

    <AllowHtml>
    <Display(Name:="Значение")>
    Public Property Value As String

    <MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Описание")>
    Public Property Description As String
End Class

Partial Public Class ApplicationDbContext
    Public Property Settings As DbSet(Of Setting)
End Class

Public Class GeneralSetting
    <Required(ErrorMessage:="Укажите название.")>
    <Display(Name:="Название")>
    Public Property Title As String

    <MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    <DataType(DataType.MultilineText)>
    <Display(Name:="Описание")>
    Public Property Description As String
End Class

Public Class AboutSetting
    <AllowHtml>
    <DataType(DataType.MultilineText)>
    <Display(Name:="Содержание")>
    <UIHint("Content")>
    Public Property Content As String

    <MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Описание")>
    Public Property Description As String
End Class
