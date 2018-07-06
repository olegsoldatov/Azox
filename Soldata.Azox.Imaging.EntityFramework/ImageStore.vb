<ObsoleteAttribute>
Public Class ImageStore(Of TImageContent As IImageContent)
    Implements IImageStore(Of TImageContent)

    Private _Context As DbContext

    Sub New()
        Me.New(New DbContext("DefaultConnection"))
    End Sub

    Sub New(context As DbContext)
        _Context = context
    End Sub

    Public Function FindAsync(imageId As Guid, size As ImageSize) As Task(Of TImageContent) Implements IImageStore(Of TImageContent).FindAsync
        Dim images As IQueryable(Of ImageData) = _Context.Set(Of ImageData).Where(Function(i) i.Id.Equals(imageId)).AsNoTracking
        Dim imageContents As IQueryable(Of TImageContent)

        Select Case size
            Case ImageSize.Large
                imageContents = images.Select(Function(i) New ImageContent With {.Content = i.Large, .ContentType = i.ContentType})
            Case ImageSize.Medium
                imageContents = images.Select(Function(i) New ImageContent With {.Content = i.Medium, .ContentType = i.ContentType})
            Case ImageSize.Small
                imageContents = images.Select(Function(i) New ImageContent With {.Content = i.Small, .ContentType = i.ContentType})
            Case Else
                imageContents = images.Select(Function(i) New ImageContent With {.Content = i.Original, .ContentType = i.ContentType})
        End Select

        Dim result As Task(Of TImageContent) = imageContents.SingleOrDefaultAsync

        Return If(IsNothing(result), Nothing, result)
    End Function

    Public ReadOnly Property Context As DbContext
        Get
            Return _Context
        End Get
    End Property

#Region "IDisposable Support"

    Public DisposeContext As Boolean ' Чтобы обнаружить избыточные вызовы

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not Me.DisposeContext Then
            If disposing Then
                _Context.Dispose()
            End If

            _Context = Nothing
        End If
        Me.DisposeContext = True
    End Sub

    ' Этот код добавлен редактором Visual Basic для правильной реализации шаблона высвобождаемого класса.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Не изменяйте этот код.  Разместите код очистки выше в методе Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

