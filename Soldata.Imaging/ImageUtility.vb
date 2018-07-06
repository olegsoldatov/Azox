Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

''' <summary>
''' Предоставляет методы обработки изображений.
''' </summary>
Public Class ImageUtility
	Private Sub New()
	End Sub

	''' <summary>
	''' Преобразует файл изображения в байтовый массив.
	''' </summary>
	''' <param name="stream">Объект <see cref="Stream"/>, который указывает на файл изображения для подготовки прочтения его содержимого.</param>
	''' <param name="contentType">MIME-тип содержимого файла изображения.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function FileToBytes(stream As Stream, contentType As String) As Byte()
		Return FileToBytes(stream, contentType, 0, 0, StretchMode.Uniform, Color.Transparent)
	End Function

	''' <summary>
	''' Преобразует файл изображения в байтовый массив, применяя к нему преобразование размера.
	''' </summary>
	''' <param name="stream">Объект <see cref="Stream"/>, который указывает на файл изображения для подготовки прочтения его содержимого.</param>
	''' <param name="contentType">MIME-тип содержимого файла изображения.</param>
	''' <param name="width">Ширина.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function FileToBytes(stream As Stream, contentType As String, width As Integer) As Byte()
		Return FileToBytes(stream, contentType, width, 0, StretchMode.Uniform, Color.Transparent)
	End Function

	''' <summary>
	''' Преобразует файл изображения в байтовый массив, применяя к нему преобразование размера.
	''' </summary>
	''' <param name="stream">Объект <see cref="Stream"/>, который указывает на файл изображения для подготовки прочтения его содержимого.</param>
	''' <param name="contentType">MIME-тип содержимого файла изображения.</param>
	''' <param name="width">Ширина.</param>
	''' <param name="height">Высота.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function FileToBytes(stream As Stream, contentType As String, width As Integer, height As Integer) As Byte()
		Return FileToBytes(stream, contentType, width, height, StretchMode.Uniform, Color.Transparent)
	End Function

	''' <summary>
	''' Преобразует файл изображения в байтовый массив, применяя к нему преобразование размера и растягивание.
	''' </summary>
	''' <param name="stream">Объект <see cref="Stream"/>, который указывает на файл изображения для подготовки прочтения его содержимого.</param>
	''' <param name="contentType">MIME-тип содержимого файла изображения.</param>
	''' <param name="width">Ширина.</param>
	''' <param name="height">Высота.</param>
	''' <param name="stretchMode">Режим растягивания изображения.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function FileToBytes(stream As Stream, contentType As String, width As Integer, height As Integer, stretchMode As StretchMode) As Byte()
		Return FileToBytes(stream, contentType, width, height, stretchMode, Color.Transparent)
	End Function

	''' <summary>
	''' Преобразует файл изображения в байтовый массив, применяя к нему преобразование размера, растягивание и заполнение фона.
	''' </summary>
	''' <param name="stream">Объект <see cref="Stream"/>, который указывает на файл изображения для подготовки прочтения его содержимого.</param>
	''' <param name="contentType">MIME-тип содержимого файла изображения.</param>
	''' <param name="width">Ширина.</param>
	''' <param name="height">Высота.</param>
	''' <param name="stretchMode">Режим растягивания изображения.</param>
	''' <param name="backgroundColor">Цвет фона.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function FileToBytes(stream As Stream, contentType As String, width As Integer, height As Integer, stretchMode As StretchMode, backgroundColor As Color) As Byte()
		If IsNothing(stream) Then
			Throw New ArgumentNullException(NameOf(stream))
		End If

		If Not contentType.Contains("image") Then
			Throw New ArgumentException("Тип не является файлом изображением.", NameOf(contentType))
		End If

		If width < 0 Then
			Throw New ArgumentException("Ширина должна быть положительным числом.", NameOf(width))
		End If

		If height < 0 Then
			Throw New ArgumentException("Высота должна быть положительным числом.", NameOf(height))
		End If

		Using memoryStream As New MemoryStream
			Using image = Drawing.Image.FromStream(stream, True, True)
				If width = 0 And height = 0 Then
					image.Save(memoryStream, GetImageFormat(contentType))
				Else
					Dim imageWidth As Integer = width
					Dim imageHeight As Integer = height
					Dim x As Integer = 0
					Dim y As Integer = 0

					If width > 0 And height = 0 Then
						height = image.Height / (image.Width / width)
					ElseIf height > 0 And width = 0 Then
						width = image.Width / (image.Height / height)
					Else
						Select Case stretchMode
							Case StretchMode.Uniform
								'Если новая высота больше, чем высота области, то изменим пропорционально ширину и сместим изображение по оси X.
								If image.Height / (image.Width / width) > height Then
									imageWidth = image.Width / (image.Height / height)
									x = (width - imageWidth) / 2
								Else
									imageHeight = image.Height / (image.Width / width)
									y = (height - imageHeight) / 2
								End If
							Case StretchMode.UniformToFill
								'Если новая высота меньше, чем высота области, то изменим пропорционально ширину и сместим изображение по оси X.
								If image.Height / (image.Width / width) < height Then
									imageWidth = image.Width / (image.Height / height)
									x = (width - imageWidth) / 2
								Else
									imageHeight = image.Height / (image.Width / width)
									y = (height - imageHeight) / 2
								End If
							Case StretchMode.None
								imageWidth = image.Width
								imageHeight = image.Height
								x = (width - imageWidth) / 2
								y = (height - imageHeight) / 2
						End Select
					End If

					Using bitmap As New Bitmap(width, height)
						Using g = Graphics.FromImage(bitmap)
							g.FillRectangle(New SolidBrush(backgroundColor), New RectangleF(0, 0, width, height))
							g.DrawImage(image, x, y, imageWidth, imageHeight)
						End Using
						Using encoderParams As New EncoderParameters(1)
							encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 85)
							bitmap.Save(memoryStream, GetEncoder(GetImageFormat(contentType)), encoderParams)
						End Using
					End Using
				End If
			End Using

			Return memoryStream.ToArray
		End Using
	End Function

	Private Shared Function GetImageFormat(contentType As String) As ImageFormat
		Select Case contentType
			Case "image/png"
				Return ImageFormat.Png
			Case "image/gif"
				Return ImageFormat.Gif
			Case "image/bmp"
				Return ImageFormat.Bmp
			Case Else
				Return ImageFormat.Jpeg
		End Select
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
