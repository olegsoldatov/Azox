Imports System.Runtime.CompilerServices
Imports System.Text

''' <summary>
''' Предоставляет методы-расширения для типа данных <see cref="Integer" />.
''' </summary>
''' <remarks></remarks>
Public Module IntegerExtensions
    ''' <summary>
    ''' Преобразует числовое значение данного экземпляра в эквивалентное ему строковое представление с добавлением единицы измерения, представленной в именительном и родительном падежах, а также во множественном числе.
    ''' </summary>
    ''' <param name="base">Экземпляр типа <see cref="Integer" />, который расширяется данным методом.</param>
    ''' <param name="nominativ">Единица измерения в именительном падеже.</param>
    ''' <param name="genetiv">Единица измерения в родительном падеже.</param>
    ''' <param name="plural">Единица измерения во множественном числе.</param>
    ''' <returns>Тип данных <see cref="String" />.</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function ToString(base As Integer, nominativ As String, genetiv As String, plural As String) As String
        Dim result As New StringBuilder(base.ToString & " ")
        Dim number = base Mod 100
        If number >= 11 And number <= 19 Then Return result.Append(plural).ToString
        Select Case base Mod 10
            Case 1
                Return result.Append(nominativ).ToString
            Case 2
                Return result.Append(genetiv).ToString
            Case 3
                Return result.Append(genetiv).ToString
            Case 4
                Return result.Append(genetiv).ToString
            Case Else
                Return result.Append(plural).ToString
        End Select
    End Function

    ''' <summary>
    ''' Преобразует числовое значение данного экземпляра в эквивалентное ему строковое представление с добавлением единицы измерения, представленной в именительном и родительном падежах, во множественном числе, а также замещающая фраза при значении равном нулю.
    ''' </summary>
    ''' <param name="base">Экземпляр типа <see cref="Integer" />, который расширяется данным методом.</param>
    ''' <param name="nominativ">Единица измерения в именительном падеже.</param>
    ''' <param name="genetiv">Единица измерения в родительном падеже.</param>
    ''' <param name="plural">Единица измерения во множественном числе.</param>
    ''' <param name="zero">Замещающая фраза при значении равном нулю.</param>
    ''' <returns>Тип данных <see cref="String" />.</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function ToString(base As Integer, nominativ As String, genetiv As String, plural As String, zero As String) As String
        If base = 0 Then Return zero
        Return ToString(base, nominativ, genetiv, plural)
    End Function
End Module
