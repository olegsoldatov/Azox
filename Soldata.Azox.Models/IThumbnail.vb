''' <summary>
''' Предоставляет определение миниатюры.
''' </summary>
Public Interface IThumbnail
    Property Id As Guid
    Property Original As Byte()
    Property Large As Byte()
    Property Medium As Byte()
    Property Small As Byte()
    Property ContentType As String
End Interface
