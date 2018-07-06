''' <summary>
''' Предоставляет класс изображения.
''' </summary>
Public Class Image
    Implements IImage

    <Key>
    Public Overridable Property Id As Guid Implements IImage.Id

    Public Overridable Property ContentType As String Implements IImage.ContentType

    <Column(TypeName:="Image")>
    Public Overridable Property Original As Byte() Implements IImage.Original

    <Column(TypeName:="Image")>
    Public Overridable Property Thumbnail As Byte() Implements IImage.Thumbnail
End Class
