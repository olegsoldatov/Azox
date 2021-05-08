Imports System.Data.Entity
Imports System.Threading.Tasks

Public Class CategoryManager
	Implements IDisposable

	Private ReadOnly db As New ApplicationDbContext

	Public ReadOnly Property Categories As IQueryable(Of Category)
		Get
			Return db.Categories
		End Get
	End Property

	Public Async Function GetByIdAsync(id As Guid?) As Task(Of Category)
		Return Await db.Categories.FindAsync(id)
	End Function

	Public Async Function AddAsync(entity As Category) As Task
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		entity.Id = Guid.NewGuid
		entity.Path = Await CreatePathAsync(entity)
		entity.LastUpdateDate = Now
		db.Categories.Add(entity)
		Await db.SaveChangesAsync
	End Function

	Public Async Function UpdateAsync(entity As Category) As Task
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		entity.Path = Await CreatePathAsync(entity)
		entity.LastUpdateDate = Now
		db.Entry(entity).State = EntityState.Modified
		Await db.SaveChangesAsync
	End Function

	Public Async Function UpdateAsync(entities As IEnumerable(Of Category)) As Task
		If IsNothing(entities) Then
			Throw New ArgumentNullException(NameOf(entities))
		End If
		For Each item In entities
			item.Path = Await CreatePathAsync(item)
			item.LastUpdateDate = Now
			db.Entry(item).State = EntityState.Modified
		Next
		Await db.SaveChangesAsync
	End Function

	Public Async Function DeleteAsync(entity As Category) As Task
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Dim entityPath = entity.Id.ToString & "/"
		Await db.Categories.Where(Function(x) x.Path.Contains(entityPath)).ForEachAsync(Sub(c As Category) c.Path = c.Path.Replace(entityPath, ""))
		entity.Products.Clear()
		entity.Childs.Clear()
		db.Categories.Remove(entity)
		Await db.SaveChangesAsync
	End Function

	Public Async Function DeleteAsync(entities As IEnumerable(Of Category)) As Task
		If IsNothing(entities) Then
			Throw New ArgumentNullException(NameOf(entities))
		End If
		For Each item In entities
			Dim entityPath = item.Id.ToString & "/"
			Await db.Categories.Where(Function(x) x.Path.Contains(entityPath)).ForEachAsync(Sub(c As Category) c.Path = c.Path.Replace(entityPath, ""))
			item.Products.Clear()
			item.Childs.Clear()
		Next
		db.Categories.RemoveRange(entities)
		Await db.SaveChangesAsync
	End Function

	Public Async Function NameExistsAsync(entity As Category) As Task(Of Boolean)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		If String.IsNullOrEmpty(entity.Name) Then
			Return False
		End If
		Return Await db.Categories.AsNoTracking().AnyAsync(Function(x) x.Name = entity.Name And Not x.Id = entity.Id)
	End Function

	Private Async Function CreatePathAsync(entity As Category) As Task(Of String)
		Dim newPath = If(entity.ParentId Is Nothing, entity.Id.ToString, String.Join("/", (Await db.Categories.SingleAsync(Function(x) x.Id = entity.ParentId)).Path, entity.Id.ToString))
		Await db.Categories _
					.Where(Function(x) Not x.Id = entity.Id And x.Path.Contains(entity.Path)) _
					.ForEachAsync(Sub(c As Category)
									  c.Path = c.Path.Replace(entity.Path, newPath)
								  End Sub)
		Return newPath
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
