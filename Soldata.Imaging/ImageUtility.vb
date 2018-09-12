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
	<Obsolete("Используйте метод Generate().")>
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
	<Obsolete("Используйте метод Generate().")>
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
	<Obsolete("Используйте метод Generate().")>
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
	<Obsolete("Используйте метод Generate().")>
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
	<Obsolete("Используйте метод Generate().")>
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

	''' <summary>
	''' Создает изображение из последовательности байтов.
	''' </summary>
	''' <param name="stream"></param>
	''' <param name="contentType">Тип содержания существующего файла.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function Generate(stream As Stream, contentType As String) As Byte()
		Return Generate(stream, contentType, 0, 0, StretchMode.Uniform, Color.Transparent)
	End Function

	''' <summary>
	''' Создает изображение из последовательности байтов.
	''' </summary>
	''' <param name="stream"></param>
	''' <param name="contentType">Тип содержания существующего файла.</param>
	''' <param name="width">Ширина.</param>
	''' <param name="height">Высота.</param>
	''' <param name="stretchMode">Тип преобразования изображения.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function Generate(stream As Stream, contentType As String, width As Integer, height As Integer, stretchMode As StretchMode) As Byte()
		Return Generate(stream, contentType, width, height, stretchMode, Color.Transparent)
	End Function

	''' <summary>
	''' Создает изображение из последовательности байтов.
	''' </summary>
	''' <param name="stream"></param>
	''' <param name="contentType">Тип содержания существующего файла.</param>
	''' <param name="width">Ширина.</param>
	''' <param name="height">Высота.</param>
	''' <param name="stretchMode">Тип преобразования изображения.</param>
	''' <param name="backgroundColor">Цвет фона.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function Generate(stream As Stream, contentType As String, width As Integer, height As Integer, stretchMode As StretchMode, backgroundColor As Color) As Byte()
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

		Return StreamToBytes(stream, GetImageFormat(contentType), width, height, stretchMode, backgroundColor)
	End Function

	''' <summary>
	''' Переделывает существующее изображение с новыми параметрами.
	''' </summary>
	''' <param name="source">Массив <see cref="Byte" />, содержащий байты существующего изображения.</param>
	''' <param name="contentType">Тип содержания существующего файла.</param>
	''' <param name="width">Ширина.</param>
	''' <param name="height">Высота.</param>
	''' <param name="stretchMode">Тип преобразования изображения.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function Regenerate(source As Byte(), contentType As String, width As Integer, height As Integer, stretchMode As StretchMode) As Byte()
		Return Regenerate(source, contentType, width, height, stretchMode, Color.Transparent)
	End Function

	''' <summary>
	''' Переделывает существующее изображение с новыми параметрами.
	''' </summary>
	''' <param name="source">Массив <see cref="Byte" />, содержащий байты существующего изображения.</param>
	''' <param name="contentType">Тип содержания существующего файла.</param>
	''' <param name="width">Ширина.</param>
	''' <param name="height">Высота.</param>
	''' <param name="stretchMode">Тип преобразования изображения.</param>
	''' <param name="backgroundColor">Цвет фона.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function Regenerate(source As Byte(), contentType As String, width As Integer, height As Integer, stretchMode As StretchMode, backgroundColor As Color) As Byte()
		If IsNothing(source) Then
			Throw New ArgumentNullException(NameOf(source))
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

		Using stream As New MemoryStream(source)
			Return StreamToBytes(stream, GetImageFormat(contentType), width, height, stretchMode, backgroundColor)
		End Using
	End Function

	''' <summary>
	''' Добавляет водяной знак на существующее изображение.
	''' </summary>
	''' <param name="source">Массив <see cref="Byte" />, содержащий байты существующего изображения.</param>
	''' <param name="contentType">Тип содержания существующего файла.</param>
	''' <param name="fileName">Имя файла изображения, которое будет накладываться на существующее изображение, как водяной знак.</param>
	''' <returns>Массив <see cref="Byte" />, содержащий байты изображения.</returns>
	Public Shared Function Watermark(source As Byte(), contentType As String, fileName As String) As Byte()
		If IsNothing(source) Then
			Throw New ArgumentNullException(NameOf(source))
		End If

		If String.IsNullOrEmpty(fileName) Then
			Throw New ArgumentNullException(NameOf(fileName))
		End If

		Using stream As New MemoryStream
			Using sourceStream As New MemoryStream(source)
				Using bitmap As New Bitmap(Image.FromStream(sourceStream, True, True))
					Using g = Graphics.FromImage(bitmap)
						Using img = Image.FromFile(fileName)
							g.DrawImage(img, CInt((bitmap.Width - img.Width) / 2), CInt((bitmap.Height - img.Height) / 2))
						End Using
					End Using

					Using encoderParams As New EncoderParameters(1)
						encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 100)
						bitmap.Save(stream, GetEncoder(GetImageFormat(contentType)), encoderParams)
					End Using
				End Using
			End Using
			Return stream.ToArray
		End Using
	End Function

	Private Shared Function StreamToBytes(stream As Stream, format As ImageFormat, width As Integer, height As Integer, stretchMode As StretchMode, backgroundColor As Color) As Byte()
		Using result As New MemoryStream
			Using image = Drawing.Image.FromStream(stream, True, True)
				If width = 0 And height = 0 Then
					'Получим оригинальное изображение.
					image.Save(result, format)
				Else
					Dim newWidth As Integer = width
					Dim newHeight As Integer = height
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
									newWidth = image.Width / (image.Height / height)
									x = (width - newWidth) / 2
								Else
									newHeight = image.Height / (image.Width / width)
									y = (height - newHeight) / 2
								End If
							Case StretchMode.UniformToFill
								'Если новая высота меньше, чем высота области, то изменим пропорционально ширину и сместим изображение по оси X.
								If image.Height / (image.Width / width) < height Then
									newWidth = image.Width / (image.Height / height)
									x = (width - newWidth) / 2
								Else
									newHeight = image.Height / (image.Width / width)
									y = (height - newHeight) / 2
								End If
							Case StretchMode.None
								newWidth = image.Width
								newHeight = image.Height
								x = (width - newWidth) / 2
								y = (height - newHeight) / 2
						End Select
					End If

					'Получим измененное изображение.
					Using bitmap As New Bitmap(width, height)
						Using g = Graphics.FromImage(bitmap)
							g.FillRectangle(New SolidBrush(backgroundColor), New RectangleF(0, 0, width, height))
							g.DrawImage(image, x, y, newWidth, newHeight)
						End Using
						Using encoderParams As New EncoderParameters(1)
							encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 85)
							bitmap.Save(result, GetEncoder(format), encoderParams)
						End Using
					End Using
				End If
			End Using

			Return result.ToArray
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
