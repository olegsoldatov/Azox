Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

''' <summary>
''' Базовый менеджер указанного типа сущности.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public MustInherit Class EntityManager(Of TEntity As {Class, IEntity})
    Implements IDisposable

    Private disposedValue As Boolean

    ''' <summary>
    ''' Устанавливает или возвращает контекст данных.
    ''' </summary>
    Protected ReadOnly Property Context As DbContext

    Protected Sub New(context As DbContext)
        Me.Context = context
    End Sub

    ''' <summary>
    ''' Возвращает перечисление сущностей с возможностью запроса.
    ''' </summary>
    Public Overridable ReadOnly Property Entities As IQueryable(Of TEntity)
        Get
            Return Context.Set(Of TEntity)
        End Get
    End Property

    ''' <summary>
    ''' Добавляет сущность.
    ''' </summary>
    ''' <param name="entity">Сущность.</param>
    Public Overridable Async Function CreateAsync(entity As TEntity) As Task(Of ManagerResult)
        If entity Is Nothing Then
            Throw New ArgumentNullException(NameOf(entity))
        End If
        entity.Id = Guid.NewGuid
        entity.LastUpdateDate = Now
        Context.Set(Of TEntity).Add(entity)
        Await Context.SaveChangesAsync
        Return ManagerResult.Success
    End Function

    ''' <summary>
    ''' Находит сущность по идентифиактору.
    ''' </summary>
    ''' <param name="entityId">Идентификатор сущности.</param>
    Public Overridable Async Function FindByIdAsync(entityId As Guid?) As Task(Of TEntity)
        Return Await Context.Set(Of TEntity).FindAsync(entityId)
    End Function

    ''' <summary>
    ''' Обновляет сущность.
    ''' </summary>
    ''' <param name="entity">Сущность.</param>
    Public Overridable Async Function UpdateAsync(entity As TEntity) As Task(Of ManagerResult)
        entity.LastUpdateDate = Now
        Context.Entry(entity).State = EntityState.Modified
        Await Context.SaveChangesAsync
        Return ManagerResult.Success
    End Function

    ''' <summary>
    ''' Обновляет перечисление сущности.
    ''' </summary>
    ''' <param name="entities">Перечисление сущности.</param>
    Public Overridable Async Function UpdateRangeAsync(entities As IEnumerable(Of TEntity)) As Task(Of ManagerResult)
        For Each item In entities
            item.LastUpdateDate = Now
            Context.Entry(item).State = EntityState.Modified
        Next
        Await Context.SaveChangesAsync
        Return ManagerResult.Success
    End Function

    ''' <summary>
    ''' Удаляет сущность.
    ''' </summary>
    ''' <param name="entity">Сущность.</param>
    Public Overridable Async Function DeleteAsync(entity As TEntity) As Task
        Context.Set(Of TEntity).Remove(entity)
        Await Context.SaveChangesAsync
    End Function

    ''' <summary>
    ''' Удаляет перечисление сущностей.
    ''' </summary>
    ''' <param name="entities">Перечисление сущностей.</param>
    Public Overridable Async Function DeleteRangeAsync(entities As IEnumerable(Of TEntity)) As Task
        Context.Set(Of TEntity).RemoveRange(entities)
        Await Context.SaveChangesAsync
    End Function

#Region "IDisposable"

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
