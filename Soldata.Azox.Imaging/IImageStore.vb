<Obsolete>
Public Interface IImageStore(Of TImageContent As IImageContent)
    Inherits IDisposable

    Function FindAsync(imageId As Guid, size As ImageSize) As Task(Of TImageContent)
End Interface
