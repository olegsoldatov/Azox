Imports System.Data.Entity

''' <summary>
''' Предоставляет базовый API для управления сущностями.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public MustInherit Class EntityManager(Of TEntity As Entity)
	Implements IDisposable

	''' <summary>
	''' Инициализирует новый экзепляр <see cref="EntityManager(Of TEntity)"/>.
	''' </summary>
	''' <param name="context">Экземпляр контекста данных.</param>
	Public Sub New(context As DbContext)
		_Context = context
		_Items = _Context.Set(Of TEntity)
	End Sub

	''' <summary>
	''' Возвращает экземпляр контекста данных.
	''' </summary>
	''' <returns>Экземпляр <see cref="DbContext"/>.</returns>
	Protected Friend ReadOnly Property Context As DbContext

	''' <summary>
	''' Возвращает перечисление сущностей с возможностью расчета запроса.
	''' </summary>
	Public ReadOnly Property Items As IQueryable(Of TEntity)

	''' <summary>
	''' Находит сущность по уникальному идентификатору.
	''' </summary>
	''' <param name="id">Уникальный идентификатор.</param>
	Public Overridable Function FindByIdAsync(id As Guid) As Task(Of TEntity)
		Return Context.Set(Of TEntity).FindAsync(id)
	End Function

	''' <summary>
	''' Добавляет экземпляр сущности в источнике данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, добавляемый в источник данных.</param>
	Public Overridable Function CreateAsync(entity As TEntity) As Task(Of EntityResult)
		Context.Set(Of TEntity).Add(entity)
		Context.SaveChanges()
		Return Task.FromResult(EntityResult.Success)
	End Function

	''' <summary>
	''' Обновляет сущность в источнике данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, обновляемый в источнике данных.</param>
	Public Overridable Function UpdateAsync(entity As TEntity) As Task(Of EntityResult)
		Context.Entry(entity).State = EntityState.Modified
		Context.SaveChanges()
		Return Task.FromResult(EntityResult.Success)
	End Function

	''' <summary>
	''' Удаляет экземпляр сущности из источника данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, удаляемый из источника данных.</param>
	Public Overridable Function DeleteAsync(entity As TEntity) As Task(Of EntityResult)
		Context.Set(Of TEntity).Remove(entity)
		Context.SaveChanges()
		Return Task.FromResult(EntityResult.Success)
	End Function

#Region "IDisposable Support"
	Private disposedValue As Boolean

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				_Context.Dispose()
			End If
		End If
		disposedValue = True
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
	End Sub
#End Region
End Class
