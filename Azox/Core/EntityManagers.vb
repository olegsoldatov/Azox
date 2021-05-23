Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

''' <summary>
''' Предоставляет API для указанного типа сущности.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public Class EntityManager(Of TEntity As {Class, IEntity})
	Implements IDisposable

	Protected Friend ReadOnly Property Context As DbContext = New ApplicationDbContext

	''' <summary>
	''' Добавляет сущность.
	''' </summary>
	''' <param name="entity">Сущность.</param>
	Public Overridable Async Function CreateAsync(entity As TEntity) As Task(Of ManagerResult)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Context.Entry(entity).State = Data.Entity.EntityState.Added
		Await Context.SaveChangesAsync
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Удаляет сущность.
	''' </summary>
	''' <param name="entity">Сущность.</param>
	Public Overridable Async Function DeleteAsync(entity As TEntity) As Task(Of ManagerResult)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Context.Set(Of TEntity).Remove(entity)
		Await Context.SaveChangesAsync
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Удаляет перечисление сущности.
	''' </summary>
	''' <param name="entities">Перечисление сущности.</param>
	Public Overridable Async Function DeleteAsync(entities As IEnumerable(Of TEntity)) As Task(Of ManagerResult)
		If IsNothing(entities) Then
			Throw New ArgumentNullException(NameOf(entities))
		End If
		Context.Set(Of TEntity).RemoveRange(entities)
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
	''' Обновляет сущность.
	''' </summary>
	''' <param name="entity">Сущность.</param>
	Public Overridable Async Function UpdateAsync(entity As TEntity) As Task(Of ManagerResult)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Context.Entry(entity).State = Data.Entity.EntityState.Modified
		Await Context.SaveChangesAsync
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Возвращает перечисление сущностей с возможностью запросов.
	''' </summary>
	Public ReadOnly Property Items As IQueryable(Of TEntity)
		Get
			Return Context.Set(Of TEntity)
		End Get
	End Property

#Region "IDisposable"

	Private disposedValue As Boolean

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				Context.Dispose()
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

''' <summary>
''' Предоставляет API для указанного типа сущности с поддержкой абсолютного пути.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public Class PathableEntityManager(Of TEntity As {Class, IPathable})
	Inherits EntityManager(Of TEntity)

	''' <summary>
	''' Находит сущность по абсолютному пути.
	''' </summary>
	''' <param name="absolutePath">Абсолютный путь.</param>
	Public Overridable Async Function FindByAbsolutePathAsync(absolutePath As String) As Task(Of TEntity)
		If String.IsNullOrEmpty(absolutePath) Then
			Return Nothing
		End If
		Return Await Context.Set(Of TEntity).FirstOrDefaultAsync(Function(x) x.AbsolutePath = absolutePath)
	End Function
End Class
