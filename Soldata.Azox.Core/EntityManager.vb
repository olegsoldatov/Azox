''' <summary>
''' Предоставляет API для управления сущностями.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public Class EntityManager(Of TEntity As {Class, IEntity(Of Guid)})
	Inherits EntityManager(Of TEntity, Guid)

	''' <summary>
	''' Инициализирует новый экземпляр класса <see cref="EntityManager(Of TEntity, Guid)"/>.
	''' </summary>
	''' <param name="store">Экземпляр хранилища данных.</param>
	Public Sub New(store As IEntityStore(Of TEntity, Guid))
		MyBase.New(store)
	End Sub
End Class

''' <summary>
''' Предоставляет API для управления сущностями.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
''' <typeparam name="TKey">Тип ключевого поля.</typeparam>
Public Class EntityManager(Of TEntity As {Class, IEntity(Of TKey)}, TKey As IEquatable(Of TKey))
	Implements IDisposable

	''' <summary>
	''' Устанавливает или возвращает хранилище данных.
	''' </summary>
	''' <returns>Экземпляр хранилища данных.</returns>
	Protected Friend Property Store As IEntityStore(Of TEntity, TKey)

	''' <summary>
	''' Инициализирует новый экземпляр класса <see cref="EntityManager(Of TEntity, TKey)"/>.
	''' </summary>
	''' <param name="store">Экземпляр хранилища данных.</param>
	Public Sub New(store As IEntityStore(Of TEntity, TKey))
		If store Is Nothing Then
			Throw New ArgumentNullException(NameOf(store))
		End If
		Me.Store = store
	End Sub

	''' <summary>
	''' Возвращает перечисление сущностей с возможностью расчета запроса.
	''' </summary>
	Public Overridable ReadOnly Property Entities As IQueryable(Of TEntity)
		Get
			Return Store.Entities
		End Get
	End Property

	''' <summary>
	''' Асинхронно добавляет экземпляр сущности в хранилище данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, добавляемый в хранилище данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Overloads Async Function CreateAsync(entity As TEntity) As Task(Of ManagerResult)
		If entity Is Nothing Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Await Store.CreateAsync(entity)
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Асинхронно добавляет перечисление сущностей в хранилище данных.
	''' </summary>
	''' <param name="entities">Перечисление сущностей, добавляемых в хранилище.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Overloads Async Function CreateAsync(entities As IEnumerable(Of TEntity)) As Task(Of ManagerResult)
		If entities Is Nothing Then
			Throw New ArgumentNullException(NameOf(entities))
		End If
		Await Store.CreateAsync(entities)
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Добавляет экземпляр сущности в хранилище данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, добавляемый в хранилище данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Overloads Sub Create(entity As TEntity)
		If entity Is Nothing Then
			Throw New ArgumentNullException(NameOf(entity))
		End If

		Store.Create(entity)
	End Sub

	''' <summary>
	''' Добавляет перечисление сущностей в хранилище данных.
	''' </summary>
	''' <param name="entities">Перечисление сущностей, добавляемых в хранилище.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Overloads Sub Create(entities As IEnumerable(Of TEntity))
		If entities Is Nothing Then
			Throw New ArgumentNullException(NameOf(entities))
		End If

		Store.Create(entities)
	End Sub

	''' <summary>
	''' Асинхронно удаляет экземпляр сущности из хранилища данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, удаляемый из хранилища данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Overloads Async Function DeleteAsync(entity As TEntity) As Task(Of ManagerResult)
		If entity Is Nothing Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Await Store.DeleteAsync(entity)
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Асинхронно удаляет перечисление сущностей из хранилища данных.
	''' </summary>
	''' <param name="entities">Перечисление сущностей, удаляемых из хранилища данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Overloads Async Function DeleteAsync(entities As IEnumerable(Of TEntity)) As Task(Of ManagerResult)
		If entities Is Nothing Then
			Throw New ArgumentNullException(NameOf(entities))
		End If
		Await Store.DeleteAsync(entities)
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Удаляет экземпляр сущности из хранилища данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, удаляемый из хранилища данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Overloads Sub Delete(entity As TEntity)
		If entity Is Nothing Then
			Throw New ArgumentNullException(NameOf(entity))
		End If

		Store.Delete(entity)
	End Sub

	''' <summary>
	''' Удаляет перечисление сущностей из хранилища данных.
	''' </summary>
	''' <param name="entities">Перечисление сущностей, удаляемых из хранилища данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Overloads Sub Delete(entities As IEnumerable(Of TEntity))
		If entities Is Nothing Then
			Throw New ArgumentNullException(NameOf(entities))
		End If

		Store.Delete(entities)
	End Sub

	''' <summary>
	''' Асинхронно находит сущность по уникальному идентификатору.
	''' </summary>
	''' <param name="id">Уникальный идентификатор.</param>
	Public Overridable Async Function FindByIdAsync(id As TKey) As Task(Of TEntity)
		Return Await Store.FindByIdAsync(id)
	End Function

	''' <summary>
	''' Находит сущность по уникальному идентификатору.
	''' </summary>
	''' <param name="id">Уникальный идентификатор.</param>
	Public Overridable Function FindById(id As Guid) As TEntity
		Return Store.FindById(id)
	End Function

	''' <summary>
	''' Асинхронно обновляет сущность в хранилище данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, обновляемый в хранилище данных.</param>
	''' <exception cref="ArgumentNullException"></exception>
	Public Overridable Async Function UpdateAsync(entity As TEntity) As Task(Of ManagerResult)
		If entity Is Nothing Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Await Store.UpdateAsync(entity)
		Return ManagerResult.Success
	End Function

	''' <summary>
	''' Обновляет сущность в хранилище данных.
	''' </summary>
	''' <param name="entity">Экземпляр сущности, обновляемый в хранилище данных.</param>
	Public Overridable Sub Update(entity As TEntity)
		If entity Is Nothing Then
			Throw New ArgumentNullException(NameOf(entity))
		End If

		Store.Update(entity)
	End Sub

#Region "IDisposable Support"
	Private disposedValue As Boolean

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				Store.Dispose()
			End If
			_Store = Nothing
		End If
		disposedValue = True
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
	End Sub
#End Region
End Class
