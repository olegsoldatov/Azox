''' <summary>
''' Предоставляет определение содержания изображения.
''' </summary>
Public Interface IImage
    ''' <summary>
    ''' Устанавливает или возвращает уникальный идентификатор.
    ''' </summary>
    ''' <value>Структура <see cref="Guid" />.</value>
    ''' <returns>Структура <see cref="Guid" />.</returns>
    Property Id As Guid

    ''' <summary>
    ''' Устанавливает или возвращает формат файла изображения.
    ''' </summary>
    ''' <value>Строка, содержащее MIME-тип формата файла изображения.</value>
    ''' <returns>Строка, содержащее MIME-тип формата файла изображения.</returns>
    Property ContentType As String

    ''' <summary>
    ''' Устанавливает или возвращает оригинальное изображение.
    ''' </summary>
    ''' <value>Изображение в виде массива <see cref="Byte" />.</value>
    ''' <returns>Изображение в виде массива <see cref="Byte" />.</returns>
    Property Original As Byte()

    ''' <summary>
    ''' Устанавливает или возвращает миниатюру изображения.
    ''' </summary>
    ''' <value>Изображение в виде массива <see cref="Byte" />.</value>
    ''' <returns>Изображение в виде массива <see cref="Byte" />.</returns>
    Property Thumbnail As Byte()
End Interface
