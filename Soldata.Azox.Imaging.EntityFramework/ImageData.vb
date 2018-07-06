''' <summary>
''' Предоставляет модель данных изображения.
''' </summary>
<Table("Images")>
<ObsoleteAttribute>
Public Class ImageData
    Implements IImageData

    ''' <summary>
    ''' Инициализирует новый экземпляр класса <see cref="ImageData" />.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Инициализирует новый экземпляр класса <see cref="ImageData" />.
    ''' </summary>
    Sub New(stream As Stream, largeWidth As Integer, mediumWidth As Integer, smallWidth As Integer, contentType As String)
        MyClass.Load(stream, largeWidth, mediumWidth, smallWidth, contentType)
    End Sub

    ''' <summary>
    ''' Устанавливает или возвращает уникальный идентификатор.
    ''' </summary>
    ''' <value>Структура <see cref="Guid" />.</value>
    ''' <returns>Структура <see cref="Guid" />.</returns>
    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property Id As Guid Implements IImageData.Id

    ''' <summary>
    ''' Устанавливает или возвращает большое изображение.
    ''' </summary>
    ''' <value>Массив класса <see cref="Byte" />, содержащий данные изображения.</value>
    ''' <returns>Массив класса <see cref="Byte" />, содержащий данные изображения.</returns>
    <Column(TypeName:="Image")>
    Public Property Large As Byte() Implements IImageData.Large

    ''' <summary>
    ''' Устанавливает или возвращает среднее изображение.
    ''' </summary>
    ''' <value>Массив класса <see cref="Byte" />, содержащий данные изображения.</value>
    ''' <returns>Массив класса <see cref="Byte" />, содержащий данные изображения.</returns>
    <Column(TypeName:="Image")>
    Public Property Medium As Byte() Implements IImageData.Medium

    ''' <summary>
    ''' Устанавливает или возвращает оригинал изображения.
    ''' </summary>
    ''' <value>Массив класса <see cref="Byte" />, содержащий данные изображения.</value>
    ''' <returns>Массив класса <see cref="Byte" />, содержащий данные изображения.</returns>
    <Column(TypeName:="Image")>
    Public Property Original As Byte() Implements IImageData.Content

    ''' <summary>
    ''' Устанавливает или возвращает малое изображение.
    ''' </summary>
    ''' <value>Массив класса <see cref="Byte" />, содержащий данные изображения.</value>
    ''' <returns>Массив класса <see cref="Byte" />, содержащий данные изображения.</returns>
    <Column(TypeName:="Image")>
    Public Property Small As Byte() Implements IImageData.Small

    ''' <summary>
    ''' Устанавливает или возвращает формат файла изображения.
    ''' </summary>
    ''' <value>Строка, содержащее условное обозначение формата файла изображения.</value>
    ''' <returns>Строка, содержащее условное обозначение формата файла изображения.</returns>
    Public Property ContentType As String Implements IImageData.ContentType

    ''' <summary>
    ''' Загружает из потока новое изображение.
    ''' </summary>
    ''' <param name="stream"></param>
    ''' <param name="contentType"></param>
    ''' <param name="largeWidth"></param>
    ''' <param name="mediumWidth"></param>
    ''' <param name="smallWidth"></param>
    ''' <exception cref="ArgumentNullException"></exception>
    ''' <exception cref="ArgumentException">Значение должно быть положительным числом.</exception>
    Public Sub Load(stream As Stream, largeWidth As Integer, mediumWidth As Integer, smallWidth As Integer, contentType As String)
        If IsNothing(stream) Then Throw New ArgumentNullException("stream")
        If largeWidth < 1 Then Throw New ArgumentException("Значение должно быть положительным числом.", "largeWidth")
        If mediumWidth < 1 Then Throw New ArgumentException("Значение должно быть положительным числом.", "mediumWidth")
        If smallWidth < 1 Then Throw New ArgumentException("Значение должно быть положительным числом.", "smallWidth")

        Me._ContentType = contentType

        Dim format As ImageFormat = ImageFormat.Jpeg

        Select Case contentType
            Case "image/png"
                format = ImageFormat.Png
            Case "image/gif"
                format = ImageFormat.Gif
            Case "image/bmp"
                format = ImageFormat.Bmp
        End Select

        Dim newImage = System.Drawing.Image.FromStream(stream, True, True)

        Dim originalWidth As Integer = newImage.Width
        Dim originalHeight As Integer = newImage.Height

        Dim largeHeight As Integer = originalHeight / (originalWidth / largeWidth)
        Dim mediumHeight As Integer = originalHeight / (originalWidth / mediumWidth)
        Dim smallHeight As Integer = originalHeight / (originalWidth / smallWidth)

        'Оригинальный размер.
        Using mStream As New MemoryStream
            newImage.Save(mStream, format)
            _Original = mStream.ToArray()
        End Using

        'Большое изображение.
        Using mStream As New MemoryStream
            Using b As New Bitmap(newImage, largeWidth, largeHeight)
                Using encoderParams As New EncoderParameters(1)
                    encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 95)
                    b.Save(mStream, GetEncoder(format), encoderParams)
                End Using
            End Using
            _Large = mStream.ToArray()
        End Using

        'Среднее изображение.
        Using mStream As New MemoryStream
            Using b As New Bitmap(newImage, mediumWidth, mediumHeight)
                Using encoderParams As New EncoderParameters(1)
                    encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 95)
                    b.Save(mStream, GetEncoder(format), encoderParams)
                End Using
            End Using
            _Medium = mStream.ToArray()
        End Using

        'Малое изображение.
        Using mStream As New MemoryStream
            Using b As New Bitmap(newImage, smallWidth, smallHeight)
                Using encoderParams As New EncoderParameters(1)
                    encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 95)
                    b.Save(mStream, GetEncoder(format), encoderParams)
                End Using
            End Using
            _Small = mStream.ToArray()
        End Using
    End Sub

    Private Function GetEncoder(format As ImageFormat) As ImageCodecInfo
        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageDecoders()

        Dim codec As ImageCodecInfo
        For Each codec In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next codec
        Return Nothing
    End Function
End Class
