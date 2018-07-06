''' <summary>
''' Структура, содержащая данные изображения.
''' </summary>
Public Class ImageContent
	''' <summary>
	''' Устанавливает или возвращает бинарное содержание изображения.
	''' </summary>
	''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
	Property Content As Byte()

	''' <summary>
	''' Устанавливает или вовзращает MIME-тип изображения.
	''' </summary>
	''' <returns>Строка, содержащая условное обозначение MIME-типа.</returns>
	Property ContentType As String
End Class
