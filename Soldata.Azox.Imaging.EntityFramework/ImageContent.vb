''' <summary>
''' Предоставляет структуру содержания данных изображения.
''' </summary>
<ObsoleteAttribute>
Public Class ImageContent
    Implements IImageContent

    ''' <summary>
    ''' Устанавливает или возвращает содержание изображения.
    ''' </summary>
    ''' <value>Байтовый массив, содержащий байты изображения.</value>
    ''' <returns>Байтовый массив, содержащий байты изображения.</returns>
    Public Property Content As Byte() Implements IImageContent.Content

    ''' <summary>
    ''' Устанавливает или возвращает MIME-тип изображения.
    ''' </summary>
    ''' <value>Строка, содержащее обозначение заголовка ответа MIME-типа изображения.</value>
    ''' <returns>Строка, содержащее обозначение заголовка ответа MIME-типа изображения.</returns>
    Public Property ContentType As String Implements IImageContent.ContentType
End Class
