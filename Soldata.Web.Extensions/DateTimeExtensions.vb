Imports System.Runtime.CompilerServices

Public Module DateTimeExtensions

    ''' <summary>
    ''' Преобразует значение текущего объекта <see cref="DateTime" /> в эквивалентное ему короткое строковое представление даты.
    ''' </summary>
    ''' <param name="dateTime">Экземпляр <see cref="DateTime" />, который расширяется данным методом.</param>
    ''' <param name="todayName">Строковое представление текущего дня. Например, "Сегодня".</param>
    ''' <returns>Строка, содержащая короткое строковое представление даты текущего объекта <see cref="DateTime" />.</returns>
    ''' <remarks></remarks>
    <Extension>
    Public Function ToShortDateString(dateTime As DateTime, todayName As String) As String
        Return ToShortDateString(dateTime, todayName, dateTime.ToShortDateString, dateTime.ToShortDateString)
    End Function

    ''' <summary>
    ''' Преобразует значение текущего объекта <see cref="DateTime" /> в эквивалентное ему короткое строковое представление даты.
    ''' </summary>
    ''' <param name="dateTime">Экземпляр <see cref="DateTime" />, который расширяется данным методом.</param>
    ''' <param name="todayName">Строковое представление текущего дня. Например, "Сегодня".</param>
    ''' <param name="yesterdayName">Строковое представление предыдущего дня. Например, "Вчера".</param>
    ''' <returns>Строка, содержащая короткое строковое представление даты текущего объекта <see cref="DateTime" />.</returns>
    ''' <remarks></remarks>
    <Extension>
    Public Function ToShortDateString(dateTime As DateTime, todayName As String, yesterdayName As String) As String
        Return ToShortDateString(dateTime, todayName, yesterdayName, dateTime.ToShortDateString)
    End Function

    ''' <summary>
    ''' Преобразует значение текущего объекта <see cref="DateTime" /> в эквивалентное ему короткое строковое представление даты.
    ''' </summary>
    ''' <param name="dateTime">Экземпляр <see cref="DateTime" />, который расширяется данным методом.</param>
    ''' <param name="todayName">Строковое представление текущего дня. Например, "Сегодня".</param>
    ''' <param name="yesterdayName">Строковое представление предыдущего дня. Например, "Вчера".</param>
    ''' <param name="tomorrowName">Строковое представление следующего дня. Например, "Завтра".</param>
    ''' <returns>Строка, содержащая короткое строковое представление даты текущего объекта <see cref="DateTime" />.</returns>
    ''' <remarks></remarks>
    <Extension>
    Public Function ToShortDateString(dateTime As DateTime, todayName As String, yesterdayName As String, tomorrowName As String) As String
        Select Case dateTime.Date.Subtract(Date.Today).Days
            Case 1
                Return tomorrowName
            Case 0
                Return todayName
            Case -1
                Return yesterdayName
            Case Else
                Return dateTime.ToShortDateString
        End Select
    End Function

    ''' <summary>
    ''' Преобразует значение текущего объекта <see cref="DateTime" /> в эквивалентное ему длинное строковое представление даты.
    ''' </summary>
    ''' <param name="dateTime">Экземпляр <see cref="DateTime" />, который расширяется данным методом.</param>
    ''' <param name="todayName">Строковое представление текущего дня. Например, "Сегодня".</param>
    ''' <returns>Строка, содержащая длинное строковое представление даты текущего объекта <see cref="DateTime" />.</returns>
    ''' <remarks></remarks>
    <Extension>
    Public Function ToLongDateString(dateTime As DateTime, todayName As String) As String
        Return ToLongDateString(dateTime, todayName, dateTime.ToLongDateString, dateTime.ToLongDateString)
    End Function

    ''' <summary>
    ''' Преобразует значение текущего объекта <see cref="DateTime" /> в эквивалентное ему длинное строковое представление даты.
    ''' </summary>
    ''' <param name="dateTime">Экземпляр <see cref="DateTime" />, который расширяется данным методом.</param>
    ''' <param name="todayName">Строковое представление текущего дня. Например, "Сегодня".</param>
    ''' <param name="yesterdayName">Строковое представление предыдущего дня. Например, "Вчера".</param>
    ''' <returns>Строка, содержащая длинное строковое представление даты текущего объекта <see cref="DateTime" />.</returns>
    ''' <remarks></remarks>
    <Extension>
    Public Function ToLongDateString(dateTime As DateTime, todayName As String, yesterdayName As String) As String
        Return ToLongDateString(dateTime, todayName, yesterdayName, dateTime.ToLongDateString)
    End Function

    ''' <summary>
    ''' Преобразует значение текущего объекта <see cref="DateTime" /> в эквивалентное ему длинное строковое представление даты.
    ''' </summary>
    ''' <param name="dateTime">Экземпляр <see cref="DateTime" />, который расширяется данным методом.</param>
    ''' <param name="todayName">Строковое представление текущего дня. Например, "Сегодня".</param>
    ''' <param name="yesterdayName">Строковое представление предыдущего дня. Например, "Вчера".</param>
    ''' <param name="tomorrowName">Строковое представление следующего дня. Например, "Завтра".</param>
    ''' <returns>Строка, содержащая длинное строковое представление даты текущего объекта <see cref="DateTime" />.</returns>
    ''' <remarks></remarks>
    <Extension>
    Public Function ToLongDateString(dateTime As DateTime, todayName As String, yesterdayName As String, tomorrowName As String) As String
        Select Case dateTime.Date.Subtract(Date.Today).Days
            Case 1
                Return tomorrowName
            Case 0
                Return todayName
            Case -1
                Return yesterdayName
            Case Else
                Return dateTime.ToLongDateString
        End Select
    End Function

End Module
