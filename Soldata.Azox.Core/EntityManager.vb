Imports System.Data.Entity

''' <summary>
''' Диспетчер сущностей.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public Class EntityManager(Of TEntity As {Class, IEntity})
	Implements IDisposable

	Private disposedValue As Boolean

	''' <summary>
	''' Возвращает контекст данных.
	''' </summary>
	''' <value>
	''' Контекст данных.
	''' </value>
	Protected ReadOnly Property Context As DbContext

	''' <summary>
	''' Инициализирует новый экземпляр класса <see cref="EntityManager(Of TEntity)"/>.
	''' </summary>
	''' <param name="context">Экземпляр контекста данных.</param>
	Public Sub New(context As DbContext)
		_Context = context
	End Sub

	''' <summary>
	''' Асинхронно находит сущность по уникальному идентификатору.
	''' </summary>
	''' <param name="id">Уникальный идентификатор.</param>
	Public Overridable Async Function FindByIdAsync(id As Guid?) As Task(Of TEntity)
		Return Await Context.Set(Of TEntity).FindAsync(id)
	End Function

	''' <summary>
	''' Находит сущность по уникальному идентификатору.
	''' </summary>
	''' <param name="id">Уникальный идентификатор.</param>
	''' <returns>Экземпляр класса сущности.</returns>
	Public Overridable Function FindById(id As Guid?) As TEntity
		Return Task.Run(Function() FindById(id)).Result
	End Function

	''' <summary>
	''' Асинхронно добавляет экземпляр сущности в источник данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, добавляемый в источник данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Async Function CreateAsync(entity As TEntity) As Task(Of TEntity)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		entity.Id = Guid.NewGuid()
		Context.Set(Of TEntity).Add(entity)
		Await Context.SaveChangesAsync()
		Return entity
	End Function

	''' <summary>
	''' Добавляет экземпляр сущности в источник данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, добавляемый в источник данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Function Create(entity As TEntity) As TEntity
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Return Task.Run(Function() CreateAsync(entity)).Result
	End Function

	''' <summary>
	''' Асинхронно обновляет сущность в источнике данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, обновляемый в источнике данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Async Function UpdateAsync(entity As TEntity) As Task(Of TEntity)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Context.Entry(entity).State = EntityState.Modified
		Await Context.SaveChangesAsync()
		Return entity
	End Function

	''' <summary>
	''' Обновляет сущность в источнике данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, обновляемый в источнике данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Function Update(entity As TEntity) As TEntity
		Return Task.Run(Function() UpdateAsync(entity)).Result
	End Function

	''' <summary>
	''' Асинхронно удаляет экземпляр сущности из источника данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, удаляемый из источника данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Async Function DeleteAsync(entity As TEntity) As Task
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Context.Set(Of TEntity).Remove(entity)
		Await Context.SaveChangesAsync()
	End Function

	''' <summary>
	''' Удаляет экземпляр сущности из источника данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, удаляемый из источника данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Sub Delete(entity As TEntity)
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Task.Run(Function() DeleteAsync(entity))
	End Sub

#Region "IDisposable Support"

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				_Context.Dispose()
			End If
			_Context = Nothing
		End If
		disposedValue = True
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
	End Sub
#End Region
End Class

