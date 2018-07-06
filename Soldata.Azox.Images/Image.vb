Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

''' <summary>
''' Предоставляет модель данных изображения.
''' </summary>
Public Class Image

    ''' <summary>
    ''' Устанавливает или возвращает уникальный идентификатор изображения.
    ''' </summary>
    ''' <returns>Структура <see cref="Guid"/>, содержащая уникальный идентификатор изображения.</returns>
    <Key>
    Public Property Id As Guid

    ''' <summary>
    ''' Устанавливает или вовзращает MIME-тип изображения.
    ''' </summary>
    ''' <returns>Строка, содержащая условное обозначение MIME-типа.</returns>
    Public Property ContentType As String

    ''' <summary>
    ''' Устанавливает или возвращает бинарное содержание оригинального изображения.
    ''' </summary>
    ''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
    <Column(TypeName:="Image")>
    Public Property Original As Byte()

    ''' <summary>
    ''' Устанавливает или возвращает бинарное содержание большого изображения.
    ''' </summary>
    ''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
    <Column(TypeName:="Image")>
    Public Property Large As Byte()

    ''' <summary>
    ''' Устанавливает или возвращает бинарное содержание среднего изображения.
    ''' </summary>
    ''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
    <Column(TypeName:="Image")>
    Public Property Medium As Byte()

    ''' <summary>
    ''' Устанавливает или возвращает бинарное содержание малого изображения.
    ''' </summary>
    ''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
    <Column(TypeName:="Image")>
    Public Property Small As Byte()

    ''' <summary>
    ''' Устанавливает или возвращает бинарное содержание миниатюры изображения.
    ''' </summary>
    ''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
    <Column(TypeName:="Image")>
    Public Property Thumbnail As Byte()
End Class
