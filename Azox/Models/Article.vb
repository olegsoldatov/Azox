Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Article
	<Key>
	Public Property Id As Guid

	<Required>
	<MaxLength(128)>
	Public Property Name As String

	Public Property Content As String

	<DataType(DataType.Date)>
	Public Property LastUpdateDate As Date

	<MaxLength(128)>
	Public Property Title As String

	Public Property Description As String

	Public Property Keywords As String

	Public Property Slug As String
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

