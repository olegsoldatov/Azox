Imports Soldata.Azox.Models
Imports System.Runtime.CompilerServices
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Imaging

Public Module HttpPostedFileExtension

    ''' <summary>
    ''' Создает миниатюру загруженного изображения, возвращая экземпляр класса <see cref="Thumbnail" />.
    ''' </summary>
    ''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <param name="largeWidth">Ширина большого изображения.</param>
    ''' <param name="mediumWidth">Ширина среднего изображения.</param>
    ''' <param name="smallWidth">Ширина малого изображения.</param>
    ''' <returns>Экземпляр класса <see cref="Thumbnail" />, содержащий сведения о миниатюре загруженного изображения.</returns>
    ''' <remarks>
    ''' Метод вычисляет три размера изображений (большое, среднее и малое), сохраняемые в объект <see cref="Thumbnail" />, которые вычисляются относительно ширины, передаваемой в соответствующих параметрах: <paramref name="largeWidth" />, <paramref name="mediumWidth" />, <paramref name="smallWidth" />. Высота изображения вычисляется пропорционально.
    ''' </remarks>
    ''' <exception cref="ArgumentException">Если загружаемый файл не является файлом изображения.</exception>
    <Extension()>
    Public Function CreateThumbnail(helper As HttpPostedFileBase, largeWidth As Integer, mediumWidth As Integer, smallWidth As Integer) As Thumbnail
        If Not helper.ContentType.Contains("image") Then Throw New ArgumentException("Загружаемый файл должен быть изображением.")

        Dim format As ImageFormat = ImageFormat.Jpeg

        Select Case helper.ContentType
            Case "image/png"
                format = ImageFormat.Png
            Case "image/gif"
                format = ImageFormat.Gif
            Case "image/bmp"
                format = ImageFormat.Bmp
        End Select

        Return FillThumbnail(New Thumbnail With {.Id = Guid.NewGuid}, Image.FromStream(helper.InputStream, True, True), format, largeWidth, mediumWidth, smallWidth)
    End Function

    Private Function FillThumbnail(thumbnail As Thumbnail, image As Image, format As ImageFormat, largeWidth As Integer, mediumWidth As Integer, smallWidth As Integer) As Thumbnail
        Dim originalWidth As Integer = image.Width
        Dim originalHeight As Integer = image.Height

        Dim largeHeight As Integer = originalHeight / (originalWidth / largeWidth)
        Dim mediumHeight As Integer = originalHeight / (originalWidth / mediumWidth)
        Dim smallHeight As Integer = originalHeight / (originalWidth / smallWidth)

        'Оригинальный размер.
        Using stream As New MemoryStream
            image.Save(stream, format)
            thumbnail.Original = stream.ToArray()
        End Using

        'Большое изображение.
        Using stream As New MemoryStream
            Using b As New Bitmap(image, largeWidth, largeHeight)
                Using encoderParams As New EncoderParameters(1)
                    encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 95)
                    b.Save(stream, GetEncoder(format), encoderParams)
                End Using
            End Using
            thumbnail.Large = stream.ToArray()
        End Using

        'Среднее изображение.
        Using stream As New MemoryStream
            Using b As New Bitmap(image, mediumWidth, mediumHeight)
                Using encoderParams As New EncoderParameters(1)
                    encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 95)
                    b.Save(stream, GetEncoder(format), encoderParams)
                End Using
            End Using
            thumbnail.Medium = stream.ToArray()
        End Using

        'Малое изображение.
        Using stream As New MemoryStream
            Using b As New Bitmap(image, smallWidth, smallHeight)
                Using encoderParams As New EncoderParameters(1)
                    encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 95)
                    b.Save(stream, GetEncoder(format), encoderParams)
                End Using
            End Using
            thumbnail.Small = stream.ToArray()
        End Using

        Return thumbnail
    End Function

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

End Module
