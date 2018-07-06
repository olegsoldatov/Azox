Imports System.IO
Imports System.Web
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.CompilerServices

''' <summary>
''' Расширяет методы класса <see cref="HttpPostedFileBase" />.
''' </summary>
Public Module HttpPostedFileBaseExtensions
    ''' <summary>
    ''' Сохраняет файл изображения в массив байтов.
    ''' </summary>
    ''' <param name="base">Экземпляр класса <see cref="HttpPostedFileBase" />, расширяемый данным методом.</param>
    ''' <param name="bytes">Массив байтов, в который записывается файл изображения.</param>
    <Obsolete>
    <Extension>
    Public Sub SaveImage(base As HttpPostedFileBase, ByRef bytes As Byte())
        SaveImage(base, bytes, 0)
    End Sub

    ''' <summary>
    ''' Сохраняет файл изображения в массив байтов, изменяя размер изображения пропорционально заданной ширине.
    ''' </summary>
    ''' <param name="base">Экземпляр класса <see cref="HttpPostedFileBase" />, расширяемый данным методом.</param>
    ''' <param name="bytes">Массив байтов, в который записывается файл изображения.</param>
    ''' <param name="width">Ширина.</param>
    <Obsolete>
    <Extension>
    Public Sub SaveImage(base As HttpPostedFileBase, ByRef bytes As Byte(), ByVal width As Integer)
        If Not base.ContentType.Contains("image") Then Throw New Exception("Файл не является изображением.")
        If width < 0 Then Throw New ArgumentException("Ширина должна быть положительным числом.", "width")

        Dim format As ImageFormat = ImageFormat.Jpeg
        Select Case base.ContentType
            Case "image/png"
                format = ImageFormat.Png
            Case "image/gif"
                format = ImageFormat.Gif
            Case "image/bmp"
                format = ImageFormat.Bmp
        End Select

        Dim newImage = System.Drawing.Image.FromStream(base.InputStream, True, True)

        If width > 0 Then
            Dim originalWidth As Integer = newImage.Width
            Dim originalHeight As Integer = newImage.Height

            'Рассчитаем пропорционально высоту изображения.
            Dim height As Integer = originalHeight / (originalWidth / width)

            'Сохраним измененное изображение.
            Using mStream As New MemoryStream
                Using b As New Bitmap(newImage, width, height)
                    Using encoderParams As New EncoderParameters(1)
                        encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 95)
                        b.Save(mStream, GetEncoder(format), encoderParams)
                    End Using
                End Using
                bytes = mStream.ToArray()
            End Using
        Else
            'Сохраним оригинальное изображение.
            Using mStream As New MemoryStream
                newImage.Save(mStream, format)
                bytes = mStream.ToArray()
            End Using
        End If
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
End Module
