Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

''' <summary>
''' Предоставляет модель данных изображения.
''' </summary>
Public Class Image
	Inherits Image(Of Guid)
	Implements IImage, IImage(Of Guid)

	Public Sub New()
		Id = Guid.NewGuid
	End Sub
End Class

''' <summary>
''' Предоставляет модель данных изображения.
''' </summary>
''' <typeparam name="TKey">Тип ключевого поля.</typeparam>
Public Class Image(Of TKey)
	Implements IImage(Of TKey)

	''' <summary>
	''' Возвращает или устанавливает идентификатор.
	''' </summary>
	''' <value>
	''' Идентификатор.
	''' </value>
	<Key>
	Public Overridable Property Id As TKey Implements IImage(Of TKey).Id

	''' <summary>
	''' Возвращает или устанавливает MIME-тип изображения.
	''' </summary>
	''' <value>
	''' Строка, содержащая условное обозначение MIME-типа.
	''' </value>
	Public Overridable Property ContentType As String Implements IImage(Of TKey).ContentType

	''' <summary>
	''' Возвращает или устанавливает бинарное содержание изображения.
	''' </summary>
	''' <value>
	''' Бинарное содержание изображения в виде массива байтов.
	''' </value>
	<Column(TypeName:="Image")>
	Public Overridable Property Content As Byte() Implements IImage(Of TKey).Content

	''' <summary>
	''' Устанавливает или возвращает бинарное содержание миниатюры изображения.
	''' </summary>
	''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
	<Column(TypeName:="Image")>
	Public Overridable Property Thumbnail As Byte() Implements IImage(Of TKey).Thumbnail
End Class
