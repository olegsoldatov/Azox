Imports System.Data.Entity
Imports System.Threading.Tasks

Public Class BrandManager
	Implements IDisposable

	Private ReadOnly db As New ApplicationDbContext

	Public ReadOnly Property Brands As IQueryable(Of Brand)
		Get
			Return db.Brands
		End Get
	End Property

	Public Async Function GetByIdAsync(id As Guid?) As Task(Of Brand)
		Return Await db.Brands.FindAsync(id)
	End Function

	Public Async Function AddAsync(entity As Brand) As Task
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		entity.Id = Guid.NewGuid
		entity.LastUpdateDate = Now
		db.Brands.Add(entity)
		Await db.SaveChangesAsync
	End Function

	Public Async Function UpdateAsync(entity As Brand) As Task
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		entity.LastUpdateDate = Now
		db.Entry(entity).State = EntityState.Modified
		Await db.SaveChangesAsync
	End Function

	Public Async Function DeleteAsync(entity As Brand) As Task
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		entity.Products.Clear()
		db.Brands.Remove(entity)
		Await db.SaveChangesAsync
	End Function

	Public Async Function DeleteAsync(entities As IEnumerable(Of Brand)) As Task
		If IsNothing(entities) Then
			Throw New ArgumentNullException(NameOf(entities))
		End If
		For Each item In entities
			item.Products.Clear()
		Next
		db.Brands.RemoveRange(entities)
		Await db.SaveChangesAsync
	End Function

	Public Async Function NameExistsAsync(entity As Brand) As Task(Of Boolean)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		If String.IsNullOrEmpty(entity.Name) Then
			Return False
		End If
		Return Await db.Brands.AsNoTracking().AnyAsync(Function(x) x.Name = entity.Name And Not x.Id = entity.Id)
	End Function

#Region "IDisposable"

	Private disposedValue As Boolean

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				db.Dispose()
			End If
			disposedValue = True
		End If
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(disposing:=True)
		GC.SuppressFinalize(Me)
	End Sub

#End Region
End Class
