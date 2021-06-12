Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

''' <summary>
''' Предоставляет управление для указанного типа сущности.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public MustInherit Class EntityManager(Of TEntity As {Class, IEntity})
	Implements IDisposable

	Private disposedValue As Boolean

	Protected Sub New(context As DbContext)
		_Context = context
	End Sub

	''' <summary>
	''' Устанавливает или возвращает контекст данных.
	''' </summary>
	Protected ReadOnly Property Context As DbContext

	''' <summary>
	''' Добавляет сущность.
	''' </summary>
	''' <param name="entity">Сущность.</param>
	Public Overridable Async Function CreateAsync(entity As TEntity) As Task(Of ManagerResult)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		entity.Id = Guid.NewGuid
		Context.Set(Of TEntity).Add(entity)
		Await Context.SaveChangesAsync
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Находит сущность по идентифиактору.
	''' </summary>
	''' <param name="entityId">Идентификатор сущности.</param>
	Public Overridable Async Function FindByIdAsync(entityId As Guid) As Task(Of TEntity)
		Return Await Context.Set(Of TEntity).FindAsync(entityId)
	End Function

	''' <summary>
	''' Изменяет сущность.
	''' </summary>
	''' <param name="entity">Сущность.</param>
	Public Overridable Async Function UpdateAsync(entity As TEntity) As Task(Of ManagerResult)
		Context.Entry(entity).State = EntityState.Modified
		Await Context.SaveChangesAsync
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Удаляет сущность.
	''' </summary>
	''' <param name="entity">Сущность.</param>
	Public Overridable Async Function DeleteAsync(entity As TEntity) As Task(Of ManagerResult)
		Context.Set(Of TEntity).Remove(entity)
		Await Context.SaveChangesAsync
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Удаляет перечисление сущности.
	''' </summary>
	''' <param name="entities">Перечисление сущности.</param>
	Public Overridable Async Function DeleteRangeAsync(entities As IEnumerable(Of TEntity)) As Task(Of ManagerResult)
		Context.Set(Of TEntity).RemoveRange(entities)
		Await Context.SaveChangesAsync
		Return ManagerResult.Success
	End Function

#Region "IDisposable"

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing And _Context IsNot Nothing Then
				_Context.Dispose()
			End If
			_Context = Nothing
			disposedValue = True
		End If
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(disposing:=True)
		GC.SuppressFinalize(Me)
	End Sub

#End Region
End Class
