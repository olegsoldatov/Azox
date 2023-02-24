Imports System.Data.Entity
Imports System.Threading.Tasks

Public Class CategoryManager
    Inherits PictorialEntityManager(Of Category)

    Public Sub New(store As Soldata.Azox.IEntityStore(Of Category), imageService As CategoryImageService)
        MyBase.New(store, imageService)
    End Sub

    Public ReadOnly Property Categories As IQueryable(Of Category)
        Get
            Return Store.Entities
        End Get
    End Property

    Public Overrides Async Function CreateAsync(entity As Category) As Task(Of Soldata.Azox.EntityResult)
        entity.Path = Await CreatePathAsync(entity)
        Return Await MyBase.CreateAsync(entity)
    End Function

    Public Overrides Async Function UpdateAsync(entity As Category) As Task(Of Soldata.Azox.EntityResult)
        entity.Path = Await CreatePathAsync(entity)
        Return Await MyBase.UpdateAsync(entity)
    End Function

    Public Overrides Async Function UpdateRangeAsync(entities As IEnumerable(Of Category)) As Task(Of Soldata.Azox.EntityResult)
        For Each item In entities
            item.Path = Await CreatePathAsync(item)
        Next
        Return Await MyBase.UpdateRangeAsync(entities)
    End Function

    Public Overrides Async Function DeleteAsync(entity As Category) As Task(Of Soldata.Azox.EntityResult)
        Dim entityPath = entity.Id.ToString & "/"
        Await Store.Entities.Where(Function(x) x.Path.Contains(entityPath)).ForEachAsync(Sub(c As Category) c.Path = c.Path.Replace(entityPath, ""))
        entity.Products.Clear()
        entity.Childs.Clear()
        Return Await MyBase.DeleteAsync(entity)
    End Function

    Public Overrides Async Function DeleteRangeAsync(entities As IEnumerable(Of Category)) As Task(Of Soldata.Azox.EntityResult)
        For Each item In entities
            Dim entityPath = item.Id.ToString & "/"
            Await Store.Entities.Where(Function(x) x.Path.Contains(entityPath)).ForEachAsync(Sub(c As Category) c.Path = c.Path.Replace(entityPath, ""))
            item.Products.Clear()
            item.Childs.Clear()
        Next
        Return Await MyBase.DeleteRangeAsync(entities)
    End Function

    Private Async Function CreatePathAsync(entity As Category) As Task(Of String)
        Dim path = If(entity.ParentId Is Nothing, entity.Id.ToString, String.Join("/", (Await Store.Entities.SingleAsync(Function(x) x.Id = entity.ParentId)).Path, entity.Id.ToString))
        Await Store.Entities _
                .Where(Function(x) Not x.Id = entity.Id And x.Path.Contains(entity.Path)) _
                .ForEachAsync(Sub(c As Category) c.Path = c.Path.Replace(entity.Path, path))
        Return path
    End Function
End Class
