Imports System.IO
Imports System.Threading.Tasks
Imports Soldata.Azox

''' <summary>
''' Базовый менеджер указанного иллюстрированного типа сущности.
''' </summary>
''' <typeparam name="TEntity">Тип сущности.</typeparam>
Public MustInherit Class PictorialEntityManager(Of TEntity As {Class, IEntity, IPictorial})
    Inherits Soldata.Azox.EntityManager(Of TEntity)

    Private ReadOnly ImageService As IImageService

    Protected Sub New(store As IEntityStore(Of TEntity), imageService As IImageService)
        MyBase.New(store)
        Me.ImageService = imageService
    End Sub

    Public Overridable Function UploadImageAsync(entity As IPictorial, imageFile As (InputStream As Stream, ContentType As String)) As Task
        Return ImageService.UploadAsync(entity, imageFile)
    End Function

    Public Overrides Async Function DeleteAsync(entity As TEntity) As Task(Of Soldata.Azox.EntityResult)
        Await ImageService.DeleteAsync(entity)
        Return Await MyBase.DeleteAsync(entity)
    End Function

    Public Overrides Async Function DeleteRangeAsync(entities As IEnumerable(Of TEntity)) As Task(Of Soldata.Azox.EntityResult)
        Await ImageService.DeleteRangeAsync(entities.Where(Function(x) x.ImageId IsNot Nothing).Select(Function(x) x.ImageId))
        Return Await MyBase.DeleteRangeAsync(entities)
    End Function
End Class