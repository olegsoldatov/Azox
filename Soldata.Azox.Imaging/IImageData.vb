''' <summary>
''' Предоставляет определение модели данных изображения.
''' </summary>
<Obsolete>
Public Interface IImageData
    Inherits IImageContent

    ''' <summary>
    ''' Устанавливает или возвращает уникальный идентификатор.
    ''' </summary>
    ''' <value>Структура <see cref="Guid" />.</value>
    ''' <returns>Структура <see cref="Guid" />.</returns>
    Property Id As Guid

    ' ''' <summary>
    ' ''' Устанавливает или возвращает оригинал изображения.
    ' ''' </summary>
    ' ''' <value>Массив класса <see cref="Byte" />, содержащий данные изображения.</value>
    ' ''' <returns>Массив класса <see cref="Byte" />, содержащий данные изображения.</returns>
    'Property Content As Byte()

    ''' <summary>
    ''' Устанавливает или возвращает большое изображение.
    ''' </summary>
    ''' <value>Массив класса <see cref="Byte" />, содержащий данные изображения.</value>
    ''' <returns>Массив класса <see cref="Byte" />, содержащий данные изображения.</returns>
    Property Large As Byte()

    ''' <summary>
    ''' Устанавливает или возвращает среднее изображение.
    ''' </summary>
    ''' <value>Массив класса <see cref="Byte" />, содержащий данные изображения.</value>
    ''' <returns>Массив класса <see cref="Byte" />, содержащий данные изображения.</returns>
    Property Medium As Byte()

    ''' <summary>
    ''' Устанавливает или возвращает малое изображение.
    ''' </summary>
    ''' <value>Массив класса <see cref="Byte" />, содержащий данные изображения.</value>
    ''' <returns>Массив класса <see cref="Byte" />, содержащий данные изображения.</returns>
    Property Small As Byte()

    ' ''' <summary>
    ' ''' Устанавливает или возвращает формат файла изображения.
    ' ''' </summary>
    ' ''' <value>Строка, содержащее MIME-тип формата файла изображения.</value>
    ' ''' <returns>Строка, содержащее MIME-тип формата файла изображения.</returns>
    'Property ContentType As String
End Interface
