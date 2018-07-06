Imports System.Data.Entity

''' <summary>
''' Предоставляет диспетчер изображений.
''' </summary>
''' <typeparam name="TContext">Тип контекста данных.</typeparam>
Public Class ImageManager(Of TContext As {DbContext, IImageDbContext})
    Implements IDisposable

    Private _Db As TContext

    ''' <summary>
    ''' Инициализирует экземпляр класса.
    ''' </summary>
    ''' <param name="context">Контекст данных.</param>
    Public Sub New(context As TContext)
        _Db = context
    End Sub

    ''' <summary>
    ''' Возвращает контекст данных.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Context As TContext
        Get
            Return _Db
        End Get
    End Property

    Public ReadOnly Property Images As IQueryable(Of Soldata.Azox.Images.Image)
        Get
            Return _Db.Images.AsQueryable
        End Get
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _Db.Dispose()
            End If
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
#End Region

    ''' <summary>
    ''' Получает экземпляр класса <see cref="ImageContent"/>, содержащий данные оригинального изображения для вывода в поток HTTP.
    ''' </summary>
    ''' <param name="id">Уникальный идентификатор изображения.</param>
    ''' <returns>Экземпляр класса <see cref="ImageContent"/>, содержащий данные изображения для вывода в поток HTTP.</returns>
    Public Function GetImageContent(id As Guid) As ImageContent
        Return GetImageContent(id, ImageSize.Original)
    End Function

    ''' <summary>
    ''' Получает экземпляр класса <see cref="ImageContent"/>, содержащий данные изображения для вывода в поток HTTP.
    ''' </summary>
    ''' <param name="id">Уникальный идентификатор изображения.</param>
    ''' <param name="size">Размер изображения.</param>
    ''' <returns>Экземпляр класса <see cref="ImageContent"/>, содержащий данные изображения для вывода в поток HTTP.</returns>
    Public Function GetImageContent(id As Guid, size As ImageSize) As ImageContent
        Dim images = _Db.Images.Where(Function(m) m.Id = id)

        Select Case size
            Case ImageSize.Thumbnail
                Return images.Select(Function(m) New ImageContent With {.Content = m.Thumbnail, .ContentType = m.ContentType}).SingleOrDefault()
            Case ImageSize.Large
                Return images.Select(Function(m) New ImageContent With {.Content = m.Large, .ContentType = m.ContentType}).SingleOrDefault()
            Case ImageSize.Medium
                Return images.Select(Function(m) New ImageContent With {.Content = m.Medium, .ContentType = m.ContentType}).SingleOrDefault()
            Case ImageSize.Small
                Return images.Select(Function(m) New ImageContent With {.Content = m.Small, .ContentType = m.ContentType}).SingleOrDefault()
            Case Else
                Return images.Select(Function(m) New ImageContent With {.Content = m.Original, .ContentType = m.ContentType}).SingleOrDefault()
        End Select
    End Function

    Public Overridable Function FindAsync(id As Guid) As Task(Of Image)
        Return _Db.Images.FindAsync(id)
    End Function

    Public Overridable Function UpdateAsync(image As Image) As Task(Of Integer)
        _Db.Entry(image).State = EntityState.Modified
        Return _Db.SaveChangesAsync
    End Function
End Class
