' © Софт Бизнес. Все права защищены.

''' <summary>
''' Менеджер сущностей.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public MustInherit Class EntityManager(Of TEntity As {Class, IEntity})
    Inherits EntityManager(Of TEntity, Guid)

    Protected Sub New(store As IEntityStore(Of TEntity))
        MyBase.New(store)
    End Sub
End Class

''' <summary>
''' Менеджер сущностей.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
''' <typeparam name="TKey">Тип ключевого поля.</typeparam>
Public MustInherit Class EntityManager(Of TEntity As {Class, IEntity(Of TKey)}, TKey As IEquatable(Of TKey))
    Implements IDisposable

    Private disposedValue As Boolean

    Protected Friend ReadOnly Property Store As IEntityStore(Of TEntity, TKey)

    ''' <summary>
    ''' Устанавливает или возвращает валидатор сущности.
    ''' </summary>
    Protected Overridable Property Validator As IEntityValidator(Of TEntity, TKey)

    ''' <summary>
    ''' Инициализирует экземпляр класса <see cref="EntityManager(Of TEntity, TKey)"/>.
    ''' </summary>
    ''' <param name="store">Хранилище.</param>
    Public Sub New(store As IEntityStore(Of TEntity, TKey))
        Me.Store = store
    End Sub

    ''' <summary>
    ''' Находит сущность по уникальному идентификатору.
    ''' </summary>
    ''' <param name="id">Уникальный идентификатор.</param>
    Public Overridable Function FindByIdAsync(id As TKey) As Task(Of TEntity)
        Return Store.FindByIdAsync(id)
    End Function

    ''' <summary>
    ''' Находит перечисление сущностей по перечислению идентификаторов.
    ''' </summary>
    ''' <param name="id">Перечисление идентификаторов.</param>
    Public Overridable Function FindByIdRangeAsync(id As IEnumerable(Of TKey)) As Task(Of IEnumerable(Of TEntity))
        Return Store.FindByIdRangeAsync(id)
    End Function

    ''' <summary>
    ''' Добавляет экземпляр сущности в источник данных.
    ''' </summary>
    ''' <param name="entity">Экземпляр сущности, добавляемый в источник данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function CreateAsync(entity As TEntity) As Task(Of EntityResult)
        ThrowIfDisposed()
        If IsNothing(entity) Then
            Throw New ArgumentNullException(NameOf(entity))
        End If

        If Validator IsNot Nothing Then
            Dim entityResult = Await Validator.ValidateAsync(entity)
            If Not entityResult.Succeeded Then
                Return entityResult
            End If
        End If

        Await Store.CreateAsync(entity)
        Return EntityResult.Success
    End Function

    ''' <summary>
    ''' Добавляет перечисление сущностей в источник данных.
    ''' </summary>
    ''' <param name="entities">Перечисление сущностей, добавляемых в источник данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function CreateRangeAsync(entities As IEnumerable(Of TEntity)) As Task(Of EntityResult)
        ThrowIfDisposed()
        If IsNothing(entities) Then
            Throw New ArgumentNullException(NameOf(entities))
        End If
        Await Store.CreateRangeAsync(entities)
        Return EntityResult.Success
    End Function

    ''' <summary>
    ''' Обновляет сущность в источнике данных.
    ''' </summary>
    ''' <param name="entity">Экземпляр сущности, обновляемый в источнике данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function UpdateAsync(entity As TEntity) As Task(Of EntityResult)
        ThrowIfDisposed()
        If IsNothing(entity) Then
            Throw New ArgumentNullException(NameOf(entity))
        End If

        If Validator IsNot Nothing Then
            Dim entityResult = Await Validator.ValidateAsync(entity)
            If Not entityResult.Succeeded Then
                Return entityResult
            End If
        End If

        Await Store.UpdateAsync(entity)
        Return EntityResult.Success
    End Function

    ''' <summary>
    ''' Обновляет перечисление сущностей в источнике данных.
    ''' </summary>
    ''' <param name="entities">Перечисление сущностей, обновляемые в источнике данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function UpdateRangeAsync(entities As IEnumerable(Of TEntity)) As Task(Of EntityResult)
        ThrowIfDisposed()
        If IsNothing(entities) Then
            Throw New ArgumentNullException(NameOf(entities))
        End If
        Await Store.UpdateRangeAsync(entities)
        Return EntityResult.Success
    End Function

    ''' <summary>
    ''' Удаляет экземпляр сущности из источника данных.
    ''' </summary>
    ''' <param name="entity">Экземпляр сущности, удаляемый из источника данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function DeleteAsync(entity As TEntity) As Task(Of EntityResult)
        ThrowIfDisposed()
        Await Store.DeleteAsync(entity)
        Return EntityResult.Success
    End Function

    ''' <summary>
    ''' Удаляет перечисление сущностей из источника данных.
    ''' </summary>
    ''' <param name="entities">Перечисление сущностей, удаляемые из источника данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function DeleteRangeAsync(entities As IEnumerable(Of TEntity)) As Task(Of EntityResult)
        ThrowIfDisposed()
        Await Store.DeleteRangeAsync(entities)
        Return EntityResult.Success
    End Function

    Private Sub ThrowIfDisposed()
        If disposedValue Then
            Throw New ObjectDisposedException([GetType].Name)
        End If
    End Sub

#Region "IDisposable"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                Store.Dispose()
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
