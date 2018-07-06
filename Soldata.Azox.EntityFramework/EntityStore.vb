Imports System.Data.Entity

Public Class EntityStore(Of TEntity As Entity)
	Implements IEntityStore(Of TEntity)

	Public ReadOnly Property Context As DbContext

	Public Sub New(context As DbContext)
		_Context = context
	End Sub

	Public ReadOnly Property Entities As IQueryable(Of TEntity) Implements IEntityStore(Of TEntity, Guid).Entities
		Get
			Return Context.Set(Of TEntity)
		End Get
	End Property

	Public Overridable Overloads Async Function CreateAsync(entity As TEntity) As Task Implements IEntityStore(Of TEntity, Guid).CreateAsync
		Context.Set(Of TEntity).Add(entity)
		Await Context.SaveChangesAsync
	End Function

	Public Overridable Overloads Async Function CreateAsync(entities As IEnumerable(Of TEntity)) As Task Implements IEntityStore(Of TEntity, Guid).CreateAsync
		Context.Set(Of TEntity).AddRange(entities)
		Await Context.SaveChangesAsync
	End Function

	Public Overridable Async Function FindByIdAsync(id As Guid) As Task(Of TEntity) Implements IEntityStore(Of TEntity, Guid).FindByIdAsync
		Return Await Context.Set(Of TEntity).FindAsync(id)
	End Function

	Public Overridable Async Function UpdateAsync(entity As TEntity) As Task Implements IEntityStore(Of TEntity, Guid).UpdateAsync
		Context.Entry(entity).State = EntityState.Modified
		Await Context.SaveChangesAsync
	End Function

	Public Overridable Overloads Async Function DeleteAsync(entity As TEntity) As Task Implements IEntityStore(Of TEntity, Guid).DeleteAsync
		Context.Set(Of TEntity).Remove(entity)
		Await Context.SaveChangesAsync
	End Function

	Public Overridable Overloads Async Function DeleteAsync(entities As IEnumerable(Of TEntity)) As Task Implements IEntityStore(Of TEntity, Guid).DeleteAsync
		Context.Set(Of TEntity).RemoveRange(entities)
		Await Context.SaveChangesAsync
	End Function

	Public Overridable Overloads Sub Create(entity As TEntity) Implements IEntityStore(Of TEntity, Guid).Create
		Context.Set(Of TEntity).Add(entity)
		Context.SaveChanges()
	End Sub

	Public Overridable Overloads Sub Create(entities As IEnumerable(Of TEntity)) Implements IEntityStore(Of TEntity, Guid).Create
		Context.Set(Of TEntity).AddRange(entities)
		Context.SaveChanges()
	End Sub

	Public Overridable Function FindById(id As Guid) As TEntity Implements IEntityStore(Of TEntity, Guid).FindById
		Return Context.Set(Of TEntity).Find(id)
	End Function

	Public Overridable Sub Update(entity As TEntity) Implements IEntityStore(Of TEntity, Guid).Update
		Context.Entry(entity).State = EntityState.Modified
		Context.SaveChanges()
	End Sub

	Public Overridable Overloads Sub Delete(entity As TEntity) Implements IEntityStore(Of TEntity, Guid).Delete
		Context.Set(Of TEntity).Remove(entity)
		Context.SaveChanges()
	End Sub

	Public Overridable Overloads Sub Delete(entitis As IEnumerable(Of TEntity)) Implements IEntityStore(Of TEntity, Guid).Delete
		Context.Set(Of TEntity).RemoveRange(entitis)
		Context.SaveChanges()
	End Sub

#Region "IDisposable Support"
	Private disposedValue As Boolean

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				Context.Dispose()
			End If
		End If
		disposedValue = True
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
	End Sub
#End Region
End Class
