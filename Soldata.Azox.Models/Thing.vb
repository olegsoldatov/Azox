Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports Soldata.Web

''' <summary>
''' Предоставляет базовый класс сущности.
''' </summary>
Public MustInherit Class Thing
	Implements IThing

	<Key>
	<HiddenInput(DisplayValue:=False)>
	Property Id As Guid Implements IThing.Id

	<Required(ErrorMessage:="Укажите название.")>
	<Display(Name:="Название")>
	Property Title As String Implements IThing.Title

	''' <summary>
	''' Заголовок.
	''' </summary>
	<Display(Name:="Заголовок")>
	Property Heading As String Implements IThing.Heading

	''' <summary>
	''' Содержание.
	''' </summary>
	<AllowHtml>
	<DataType(DataType.MultilineText)>
	<UIHint("WYSIWYG")>
	<Display(Name:="Содержание")>
	Property Content As String Implements IThing.Content

	''' <summary>
	''' Описание.
	''' </summary>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Описание")>
	Property Description As String Implements IThing.Description

	''' <summary>
	''' Ключевые слова.
	''' </summary>
	<Display(Name:="Ключевые слова")>
	Property Keywords As String Implements IThing.Keywords

	''' <summary>
	''' Уникальный идентификатор связанного изображения.
	''' </summary>
	''' <returns></returns>
	<DataType(DataType.ImageUrl)>
	<Display(Name:="Изображение")>
	Property ImageId As Guid Implements IThing.ImageId

	<NotMapped>
	<FileTypeValidation("jpeg, jpg, png, gif, bmp")>
	<DataType(DataType.Upload)>
	<Display(Name:="Файл изображения")>
	Property ImageFile As HttpPostedFileWrapper Implements IThing.ImageFile

	''' <summary>
	''' Ярлык.
	''' </summary>
	<Display(Name:="Ярлык")>
	Property ActionName As String Implements IThing.ActionName

	''' <summary>
	''' Устанавливает или возвращает порядок в перечислении.
	''' </summary>
	<UIHint("Order")>
	<Display(Name:="Порядок")>
	Property Order As Integer Implements IThing.Order

	''' <summary>
	''' Устанавливает или возвращает отметку о публикации.
	''' </summary>
	<UIHint("IsPublished")>
	<Display(Name:="Опубликовано")>
	Property IsPublished As Boolean Implements IThing.IsPublished
End Class
