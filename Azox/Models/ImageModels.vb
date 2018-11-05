Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports Soldata.Azox.EntityFramework

Public Class Image
	Inherits Soldata.Azox.EntityFramework.Entity

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
	Inherits Soldata.Azox.EntityManager(Of Image)

	Public Sub New()
		MyBase.New(New EntityStore(Of Image)(New ApplicationDbContext))
	End Sub

	Public ReadOnly Property Images As IQueryable(Of Image)
		Get
			Return CType(Store, EntityStore(Of Image)).Context.Set(Of Image)
		End Get
	End Property
End Class
