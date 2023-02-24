Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Article
	Inherits ApplicationEntity

	<Required(ErrorMessage:="Укажите заголовок.")>
    <Display(Name:="Заголовок")>
    Public Property Heading As String

    <DataType(DataType.MultilineText)>
    <AllowHtml>
    <Display(Name:="Содержание")>
    <UIHint("Content")>
    Public Property Content As String

    '<MaxLength(200, ErrorMessage:="Не более {1} символов.")>
    '<Display(Name:="Название")>
    'Public Property Title As String

    '<MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    '<DataType(DataType.MultilineText)>
    '<Display(Name:="Описание")>
    'Public Property Description As String

    ''<Remote("SlugValid", "Articles", AdditionalFields:="Id", ErrorMessage:="Такой путь уже существует.")>
    '<RegularExpression("^(\/[a-z0-9_-]+)+$", ErrorMessage:="Неверный путь.")>
    '<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
    '<Display(Name:="Абсолютный путь")>
    'Public Property AbsolutePath As String
End Class

Partial Public Class ApplicationDbContext
	Public Property Articles As DbSet(Of Article)
End Class

Public Class ArticleAdminViewModel
	Public Property Id As Guid

	<Display(Name:="Имя")>
	Public Property Name As String

	<Display(Name:="Путь")>
	Public Property Slug As String
End Class

Public Class ArticleCreateViewModel
	<Required(ErrorMessage:="Укажите имя.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Имя")>
	Public Property Name As String

	<AllowHtml>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	Public Property Content As String

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<Display(Name:="Описание")>
	Public Property Description As String

	<Display(Name:="Ключевые слова")>
	Public Property Keywords As String

	<RegularExpression("^(\/[a-z0-9_-]+)+$", ErrorMessage:="Неверный путь.")>
	<Remote("SlugValid", "Articles", AdditionalFields:="Id", ErrorMessage:="Такой путь уже существует.")>
	<Display(Name:="Путь")>
	Public Property Slug As String
End Class

Public Class ArticleEditViewModel
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите имя.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Имя")>
	Public Property Name As String

	<AllowHtml>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	Public Property Content As String

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<Display(Name:="Описание")>
	Public Property Description As String

	<Display(Name:="Ключевые слова")>
	Public Property Keywords As String

	<RegularExpression("^(\/[a-z0-9_-]+)+$", ErrorMessage:="Неверный путь.")>
	<Remote("SlugValid", "Articles", AdditionalFields:="Id", ErrorMessage:="Такой путь уже существует.")>
	<Display(Name:="Путь")>
	Public Property Slug As String
End Class

