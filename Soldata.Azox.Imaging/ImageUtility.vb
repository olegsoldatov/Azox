Imports System.IO
Imports System.Web
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.CompilerServices
Imports System.Net.Mime

''' <summary>
''' Предоставляет методы для работы с изображениями.
''' </summary>
Public NotInheritable Class ImageUtility
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Преобразует изображение из файла в массив байтов.
    ''' </summary>
    ''' <param name="filename">Имя файла.</param>
    ''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
    Public Shared Function ToBytes(fileName As String) As Byte()
        Return ToBytes(fileName, 0)
    End Function

    ''' <summary>
    ''' Преобразует загруженный файл изображения в массив байтов.
    ''' </summary>
    ''' <param name="postedFile">Загруженный файл.</param>
    ''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
    Public Shared Function ToBytes(postedFile As HttpPostedFileBase) As Byte()
        Return ToBytes(postedFile, 0)
    End Function

    ''' <summary>
    ''' Преобразует изображение из файла в массив байтов.
    ''' </summary>
    ''' <param name="fileName">Имя файла.</param>
    ''' <param name="width">Ширина изображения.</param>
    ''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
    Public Shared Function ToBytes(fileName As String, width As Integer) As Byte()
        Return ToBytes(fileName, width, 0, ImageTransformMode.Default)
    End Function

    ''' <summary>
    ''' Преобразует загруженный файл изображения в массив байтов.
    ''' </summary>
    ''' <param name="postedFile">Загруженный файл.</param>
    ''' <param name="width">Ширина изображения.</param>
    ''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
    Public Shared Function ToBytes(postedFile As HttpPostedFileBase, width As Integer) As Byte()
        Return ToBytes(postedFile, width, 0, ImageTransformMode.Default)
    End Function

    ''' <summary>
    ''' Преобразует изображение из файла в массив байтов.
    ''' </summary>
    ''' <param name="fileName">Имя файла.</param>
    ''' <param name="width">Ширина изображения.</param>
    ''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
    Public Shared Function ToBytes(fileName As String, width As Integer, height As Integer, transformMode As ImageTransformMode) As Byte()
        Return ToBytes(fileName, width, height, transformMode, Color.White)
    End Function

    ''' <summary>
    ''' Преобразует загруженный файл изображения в массив байтов.
    ''' </summary>
    ''' <param name="postedFile">Загруженный файл.</param>
    ''' <param name="width">Ширина изображения.</param>
    ''' <param name="height">Высота изображения.</param>
    ''' <param name="transformMode">Тип преобразования.</param>
    ''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
    Public Shared Function ToBytes(postedFile As HttpPostedFileBase, width As Integer, height As Integer, transformMode As ImageTransformMode) As Byte()
        Return ToBytes(postedFile, width, height, transformMode, Color.White)
    End Function

    ''' <summary>
    ''' Преобразует изображение из файла в массив байтов.
    ''' </summary>
    ''' <param name="fileName">Имя файла.</param>
    ''' <param name="width">Ширина изображения.</param>
    ''' <param name="height">Высота изображения.</param>
    ''' <param name="transformMode">Тип преобразования.</param>
    ''' <param name="backgroundColor">Цвет фоновой заливки.</param>
    ''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
    Public Shared Function ToBytes(fileName As String, width As Integer, height As Integer, transformMode As ImageTransformMode, backgroundColor As Color) As Byte()
        If String.IsNullOrEmpty(fileName) Then Throw New ArgumentNullException("filename")

        Dim format As ImageFormat = ImageFormat.Jpeg
        Select Case IO.Path.GetExtension(fileName)
            Case ".png"
                format = ImageFormat.Png
            Case ".gif"
                format = ImageFormat.Gif
            Case ".bmp"
                format = ImageFormat.Bmp
        End Select

        Return ImageToBytes(System.Drawing.Image.FromFile(fileName), format, width, height, transformMode, backgroundColor)
    End Function

    ''' <summary>
    ''' Преобразует загруженный файл изображения в массив байтов.
    ''' </summary>
    ''' <param name="postedFile">Загруженный файл.</param>
    ''' <param name="width">Ширина изображения.</param>
    ''' <param name="height">Высота изображения.</param>
    ''' <param name="transformMode">Тип преобразования.</param>
    ''' <param name="backgroundColor">Цвет фоновой заливки.</param>
    ''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
    Public Shared Function ToBytes(postedFile As HttpPostedFileBase, width As Integer, height As Integer, transformMode As ImageTransformMode, backgroundColor As Color) As Byte()
        If Not postedFile.ContentType.Contains("image") Then Throw New Exception("Файл не является изображением.")

        Dim format As ImageFormat = ImageFormat.Jpeg
        Select Case postedFile.ContentType
            Case "image/png"
                format = ImageFormat.Png
            Case "image/gif"
                format = ImageFormat.Gif
            Case "image/bmp"
                format = ImageFormat.Bmp
        End Select

        Return ImageToBytes(System.Drawing.Image.FromStream(postedFile.InputStream, True, True), format, width, height, transformMode, backgroundColor)
    End Function

    Private Shared Function ImageToBytes(image As Image, format As ImageFormat, width As Integer, height As Integer, transformMode As ImageTransformMode, backgroundColor As Color) As Byte()
        If width < 0 Then Throw New ArgumentException("Ширина должна быть положительным числом.", "width")
        If height < 0 Then Throw New ArgumentException("Высота должна быть положительным числом.", "height")

        Dim originalWidth As Integer = image.Width
        Dim originalHeight As Integer = image.Height

        If width > 0 Then
            Dim x As Integer = 0
            Dim y As Integer = 0

            'Установим новую ширину и пропорционально высоту изображения.
            Dim newWidth As Integer = width
            Dim newHeight As Integer = originalHeight / (originalWidth / width)

            If height > 0 Then
                Select Case transformMode
                    Case ImageTransformMode.Fill
                        'Если новая высота больше, чем установленный размер, то уменьшим пропорционально ширину - впишем изображение в рамки размера.
                        If newHeight > height Then
                            newWidth = originalWidth / (originalHeight / height)
                            newHeight = height
                        End If
                    Case ImageTransformMode.FillWithBackground
                        'Если новая высота больше, чем установленный размер, то уменьшим пропорционально ширину - впишем изображение в рамки размера.
                        If newHeight > height Then
                            x = ((originalWidth / (originalHeight / height)) - width) / 2
                        Else
                            y = (newHeight - height) / 2
                        End If
                        newHeight = height
                    Case ImageTransformMode.Cut
                        'Если новая высота меньше, чем установленный размер, то установим смещение по ширине за рамки размера - обрежем изображение под новый размер.
                        If newHeight < height Then
                            x = ((originalWidth / (originalHeight / height)) - width) / 2
                        Else
                            y = (newHeight - height) / 2
                        End If
                        newHeight = height
                End Select
            End If

            'Получим измененное изображение.
            Return GetThumbnail(image, format, x, y, newWidth, newHeight, backgroundColor)
        Else
            'Получим оригинальное изображение.
            Return GetOriginal(image, format)
        End If
    End Function

    ''' <summary>
    ''' Возвращает сопоставление MIME для заданного имени файла.
    ''' </summary>
    ''' <param name="filename">Имя файла, используемое для определения типа MIME.</param>
    ''' <returns>Строка, содержащая MIME тип файла.</returns>
    Public Shared Function GetContentType(fileName As String) As String
        Return MimeMapping.GetMimeMapping(fileName)
    End Function

    Private Shared Function GetThumbnail(image As Image, format As ImageFormat, x As Integer, y As Integer, width As Integer, height As Integer, backgroundColor As Color) As Byte()
        Using mStream As New MemoryStream
            Using b As New Bitmap(width, height)
                Using g = Graphics.FromImage(b)
                    g.FillRectangle(New SolidBrush(backgroundColor), New RectangleF(0, 0, width, height))
                    g.DrawImage(image, x * -1, y * -1, (x * 2) + width, (y * 2) + height)
                End Using
                Using encoderParams As New EncoderParameters(1)
                    encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 95)
                    b.Save(mStream, GetEncoder(format), encoderParams)
                End Using
            End Using
            Return mStream.ToArray()
        End Using
    End Function

    Private Shared Function GetOriginal(image As Image, format As ImageFormat) As Byte()
        Using mStream As New MemoryStream
            image.Save(mStream, Format)
            Return mStream.ToArray()
        End Using
    End Function

    Private Shared Function GetEncoder(format As ImageFormat) As ImageCodecInfo
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

''' <summary>
''' Тип преобразования изображения.
''' </summary>
Public Enum ImageTransformMode
    ''' <summary>
    ''' По умолчанию. Изображение трансформируется только по ширине; высота трансформируется пропорционально.
    ''' </summary>
    [Default] = 0
    ''' <summary>
    ''' Изображение вписывается шириной и высотой в заданный размер, не изменяя пропорций.
    ''' </summary>
    Fill = 1
    ''' <summary>
    ''' Изображение вписывается шириной и высотой в заданный размер, не изменяя пропорций, но заполняя пространство цветом.
    ''' </summary>
    FillWithBackground = 2
    ''' <summary>
    ''' Изображение вписывается в заданный размер, подрезая выходы за края.
    ''' </summary>
    Cut = 3
End Enum
