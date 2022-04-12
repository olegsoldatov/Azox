Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Page
	Inherits Entity

	<Required(ErrorMessage:="Укажите имя.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<AllowHtml>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	Public Property Content As String

	<Required(ErrorMessage:="Укажите абсолютный путь.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Абсолютный путь")>
	Public Property AbsolutePath As String

	<Display(Name:="Описание")>
	Public Property Description As String

	<Display(Name:="Ключевые слова")>
	Public Property Keywords As String
End Class

Partial Public Class ApplicationDbContext
	Public Property Pages As DbSet(Of Page)
End Class
