Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports Soldata.Azox

Public Class Image
	Inherits Entity

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
	Inherits EntityManager(Of Image)

	Public Sub New()
		MyClass.New(New ApplicationDbContext)
	End Sub

	Public Sub New(context As DbContext)
		MyBase.New(context)
	End Sub
End Class

Public Class ChangeImageViewModel
	<HiddenInput(DisplayValue:=False)>
	Public Property Id As Guid

	<Display(Name:="Файл изображения")>
	Public Property ImageFile As HttpPostedFileWrapper
End Class
