' © Софт Бизнес, ООО. Все права защищены.

''' <summary>
''' Предоставляет базовый класс менеджера сущностей.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public MustInherit Class EntityManager(Of TEntity As {Class, IEntity})
    Implements IDisposable

    Private disposedValue As Boolean

    Protected Friend Property Store As IEntityStore(Of TEntity)

    ''' <summary>
    ''' Инициализирует экземпляр класса <see cref="EntityManager(Of TEntity)"/>.
    ''' </summary>
    ''' <param name="store">Хранилище.</param>
    Public Sub New(store As IEntityStore(Of TEntity))
        If IsNothing(store) Then
            Throw New ArgumentNullException(NameOf(store))
        End If
        _Store = store
    End Sub

    ''' <summary>
    ''' Находит сущность по уникальному идентификатору.
    ''' </summary>
    ''' <param name="id">Уникальный идентификатор.</param>
    Public Overridable Async Function FindByIdAsync(id As Guid?) As Task(Of TEntity)
        Return Await Store.FindByIdAsync(id)
    End Function

    ''' <summary>
    ''' Добавляет экземпляр сущности в источник данных.
    ''' </summary>
    ''' <param name="entity">Экземпляр сущности, добавляемый в источник данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function CreateAsync(entity As TEntity) As Task(Of ManagerResult)
        If IsNothing(entity) Then
            Throw New ArgumentNullException(NameOf(entity))
        End If
        entity.Id = Guid.NewGuid
        entity.LastUpdateDate = Now
        Try
            Await Store.CreateAsync(entity)
            Return ManagerResult.Success
        Catch ex As Exception
            Return New ManagerResult(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Добавляет перечисление сущностей в источник данных.
    ''' </summary>
    ''' <param name="entities">Перечисление сущностей, добавляемых в источник данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function CreateRangeAsync(entities As IEnumerable(Of TEntity)) As Task(Of ManagerResult)
        If IsNothing(entities) Then
            Throw New ArgumentNullException(NameOf(entities))
        End If
        For Each item In entities
            item.Id = Guid.NewGuid
            item.LastUpdateDate = Now
        Next
        Try
            Await Store.CreateRangeAsync(entities)
            Return ManagerResult.Success
        Catch ex As Exception
            Return New ManagerResult(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Обновляет сущность в источнике данных.
    ''' </summary>
    ''' <param name="entity">Экземпляр сущности, обновляемый в источнике данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function UpdateAsync(entity As TEntity) As Task(Of ManagerResult)
        If IsNothing(entity) Then
            Throw New ArgumentNullException(NameOf(entity))
        End If
        entity.LastUpdateDate = Now
        Try
            Await Store.UpdateAsync(entity)
            Return ManagerResult.Success
        Catch ex As Exception
            Return New ManagerResult(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Обновляет перечисление сущностей в источнике данных.
    ''' </summary>
    ''' <param name="entities">Перечисление сущностей, обновляемые в источнике данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function UpdateRangeAsync(entities As IEnumerable(Of TEntity)) As Task(Of ManagerResult)
        If IsNothing(entities) Then
            Throw New ArgumentNullException(NameOf(entities))
        End If
        For Each item In entities
            item.LastUpdateDate = Now
        Next
        Try
            Await Store.UpdateRangeAsync(entities)
            Return ManagerResult.Success
        Catch ex As Exception
            Return New ManagerResult(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Удаляет экземпляр сущности из источника данных.
    ''' </summary>
    ''' <param name="entity">Экземпляр сущности, удаляемый из источника данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function DeleteAsync(entity As TEntity) As Task(Of ManagerResult)
        If IsNothing(entity) Then
            Throw New ArgumentNullException(NameOf(entity))
        End If
        Try
            Await Store.DeleteAsync(entity)
            Return ManagerResult.Success
        Catch ex As Exception
            Return New ManagerResult(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Удаляет перечисление сущностей из источника данных.
    ''' </summary>
    ''' <param name="entities">Перечисление сущностей, удаляемые из источника данных.</param>
    ''' <exception cref="ArgumentNullException"></exception>
    Public Overridable Async Function DeleteRangeAsync(entities As IEnumerable(Of TEntity)) As Task(Of ManagerResult)
        If IsNothing(entities) Then
            Throw New ArgumentNullException(NameOf(entities))
        End If
        Try
            Await Store.DeleteRangeAsync(entities)
            Return ManagerResult.Success
        Catch ex As Exception
            Return New ManagerResult(ex.Message)
        End Try
    End Function

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
