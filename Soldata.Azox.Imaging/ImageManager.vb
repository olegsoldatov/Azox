<Obsolete>
Public Class ImageManager(Of TImageContent As IImageContent)
    Implements IDisposable

    Private _Store As IImageStore(Of TImageContent)

    Sub New(store As IImageStore(Of TImageContent))
        _Store = store
    End Sub

    Protected ReadOnly Property Store As IImageStore(Of TImageContent)
        Get
            Return _Store
        End Get
    End Property

    Public Overridable Function FindAsync(imageId As Guid, size As ImageSize) As Task(Of TImageContent)
        Return _Store.FindAsync(imageId, size)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Чтобы обнаружить избыточные вызовы

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                _Store.Dispose()
            End If

            _Store = Nothing
        End If
        Me.disposedValue = True
    End Sub

    ' Этот код добавлен редактором Visual Basic для правильной реализации шаблона высвобождаемого класса.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Не изменяйте этот код.  Разместите код очистки выше в методе Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
