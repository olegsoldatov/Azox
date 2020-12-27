Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Brand
	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите имя.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Имя")>
	Public Property Name As String

	<Required(ErrorMessage:="Укажите название.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<Display(Name:="Изображение")>
	Public Property ImageId As Guid?

	<Display(Name:="Порядок")>
	Public Property Order As Integer?

	<Display(Name:="Черновик")>
	Public Property Draft As Boolean

	<Display(Name:="Продукция")>
	Public Overridable Property Products As ICollection(Of Product)
End Class

Partial Public Class ApplicationDbContext
	Public Property Brands As DbSet(Of Brand)
End Class

Public Class BrandAdminItem
	Public Property Id As Guid
	Public Property Name As String
	<Display(Name:="Название")>
	Public Property Title As String
	<Display(Name:="Товары")>
	Public Property Products As Integer
	<Display(Name:="Изображение")>
	Public Property ImageId As Guid?
	<Display(Name:="Порядок")>
	Public Property Order As Integer?
	<UIHint("Draft")>
	<Display(Name:="Черновик")>
	Public Property Draft As Boolean
End Class
