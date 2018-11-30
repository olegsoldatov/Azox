Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class Image
	Inherits Soldata.Entity.Entity

	Public Property ContentType As String

	<Column(TypeName:="Image")>
	Public Property Original As Byte()

	<Column(TypeName:="Image")>
	Public Property Thumbnail As Byte()

	<Column(TypeName:="Image")>
	Public Property Large As Byte()

	<Column(TypeName:="Image")>
	Public Property Medium As Byte()

	<Column(TypeName:="Image")>
	Public Property Small As Byte()
End Class

Partial Public Class ApplicationDbContext
	Public Property Images As DbSet(Of Image)
End Class

Public Class ImageManager
	Inherits Soldata.Entity.EntityManager(Of Image)

	Public Sub New()
		MyBase.New(New ApplicationDbContext)
	End Sub
End Class

Public Class ChangeImageViewModel
	<HiddenInput(DisplayValue:=False)>
	Public Property Id As Guid

	<Display(Name:="Файл изображения")>
	Public Property ImageFile As HttpPostedFileWrapper
End Class