Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class Brand
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<Required(ErrorMessage:="Укажите название."), StringLength(255, ErrorMessage:="Длина строки не более {1} символов."), Display(Name:="Название")>
	Public Property Name As String

	<Display(Name:="Ярлык")>
	Public Property Slug As String

	<DataType(DataType.ImageUrl), Display(Name:="Изображение")>
	Public Property ImageId As Guid?

	<NotMapped, DataType(DataType.Upload), Display(Name:="Файл изображения")>
	Public Property ImageFile As HttpPostedFileWrapper

	<Display(Name:="Продукция")>
	Public Overridable Property Products As ICollection(Of Product)
End Class

Partial Public Class ApplicationDbContext
	Public Property Brands As DbSet(Of Brand)
End Class

Public Class BrandFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchString As String
End Class

