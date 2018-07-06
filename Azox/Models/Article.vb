Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Article
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<ScaffoldColumn(False)>
	Public Property LastUpdateDate As Date = Date.Now.Date

	<Required(ErrorMessage:="Укажите название.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<AllowHtml>
	<DataType(DataType.MultilineText), UIHint("Content")>
	<Display(Name:="Содержание")>
	Public Property Content As String

	<Display(Name:="Ярлык")>
	Public Property Slug As String

	<UIHint("Order")>
	<Display(Name:="Порядок")>
	Public Property Order As Integer

	<UIHint("Draft")>
	<Display(Name:="Черновик")>
	Public Property Draft As Boolean
End Class

Partial Public Class ApplicationDbContext
	Public Property Articles As DbSet(Of Article)
End Class
