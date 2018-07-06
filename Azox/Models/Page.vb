Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Page
	<Key, HiddenInput(DisplayValue:=False)>
	Public Property Id As Guid

	<HiddenInput(DisplayValue:=False)>
	Public Property ActionName As String

	<HiddenInput(DisplayValue:=False)>
	Public Property ControllerName As String

	<Display(Name:="Название"), Required(ErrorMessage:="Укажите название.")>
	Public Property Title As String

	<Display(Name:="Заголовок (h1)")>
	Public Property Heading As String

	<Display(Name:="Содержание"), DataType(DataType.MultilineText), UIHint("Content"), AllowHtml>
	Public Property Content As String

	<Display(Name:="SEO описание"), DataType(DataType.MultilineText)>
	Public Property Description As String

	<Display(Name:="SEO ключевые слова")>
	Public Property Keywords As String

	<Display(Name:="Изображение")>
	Public Property ImageId As Guid?
End Class

Partial Public Class ApplicationDbContext
	Public Property Pages As DbSet(Of Page)
End Class
