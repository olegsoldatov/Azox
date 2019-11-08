Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class Page
	<Key>
	<DatabaseGenerated(DatabaseGeneratedOption.Identity)>
	Public Property Id As Guid

	<Display(Name:="Название")>
	<Required(ErrorMessage:="Укажите название.")>
	<StringLength(255, ErrorMessage:="Длина строки не более {1} символов.")>
	Public Overridable Property Name As String

	<Display(Name:="Заголовок (title)")>
	Public Overridable Property Title As String

	<Display(Name:="Заголовок (h1)")>
	Public Overridable Property Heading As String

	<Display(Name:="Описание")>
	Public Overridable Property Description As String

	<Display(Name:="Ключевые слова")>
	Public Overridable Property Keywords As String

	<AllowHtml>
	<UIHint("Content")>
	<Display(Name:="Содержание")>
	<DataType(DataType.MultilineText)>
	Public Overridable Property Content As String

	<Required>
	<HiddenInput(DisplayValue:=False)>
	Public Property ActionName As String

	<Required>
	<HiddenInput(DisplayValue:=False)>
	Public Property ControllerName As String
End Class

Partial Public Class ApplicationDbContext
	Public Property Pages As DbSet(Of Page)
End Class

Public Class PageFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchText As String
End Class

