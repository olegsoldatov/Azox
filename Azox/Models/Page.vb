Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports Soldata.Azox

Public Class Page
	Inherits Entity
	Implements IPage

	<Required(ErrorMessage:="Укажите имя.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String Implements IPage.Title

	<AllowHtml>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	Public Property Content As String Implements IPage.Content

	<RegularExpression("^\/[-\w/]+$", ErrorMessage:="Используется недопустимый формат.")>
	<Remote("Exists", "Pages", "Admin", AdditionalFields:="Id", ErrorMessage:="Такой путь уже существует.")>
	<Display(Name:="Абсолютный путь")>
	Public Property AbsolutePath As String

	<ScaffoldColumn(False)>
	Public Property LastUpdateDate As Date

	<DataType(DataType.MultilineText)>
	<Display(Name:="Описание")>
	Public Property Description As String Implements IPage.Description

	<Display(Name:="Ключевые слова")>
	Public Property Keywords As String Implements IPage.Keywords
End Class

Partial Public Class ApplicationDbContext
	Public Property Pages As DbSet(Of Page)
End Class
