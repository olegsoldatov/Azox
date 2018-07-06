﻿Imports System.Runtime.CompilerServices
Imports System.Web.Mvc
Imports System.Text

Public Module SlugUrlHelper
    ''' <summary>
    ''' Возвращает строку приведенную в адресный идентификатор, из которой исключены знаки препинания, а пробелы и прочие небуквенно-цифровые символы замены на дефис.
    ''' </summary>
    ''' <param name="url">Экземпляр вспомогательного метода URL, который расширяется данным методом.</param>
    ''' <param name="srt">Строка текста, которую нужно преобразовать.</param>
    <Extension>
    Public Function Slug(url As UrlHelper, srt As String) As String
        Dim resultChars() = srt.ToCharArray
        Dim resultString As New StringBuilder()
        For Each singleChar In resultChars
            If Not Char.IsLetterOrDigit(singleChar) And Not Char.IsPunctuation(singleChar) Then
                singleChar = "-"
                resultString.Append(singleChar)
            ElseIf Char.IsLetterOrDigit(singleChar) Or singleChar = "-" Then
                resultString.Append(singleChar)
            End If
        Next
        Return Translit(resultString.ToString).ToLower
    End Function

    ''' <summary>
    ''' Транслитерирует кириллические символы в латинские, заменяя пробелы и прочие небуквенно-цифровые символы на дефис.
    ''' </summary>
    ''' <remarks>
    ''' Функция преобразовывает все кириллические символы в латинские. Все пробелы заменяются короткой чертой, а все знаки пунктуации удаляются. В итоге получается неразрывная строка.
    ''' Транслитерация по ГОСТ Р 52535.1-2006.
    ''' </remarks>
    Private Function Translit(source As String) As String
        Dim result = source.Replace("А", "A").Replace("а", "a") _
                .Replace("Б", "B").Replace("б", "b") _
                .Replace("В", "V").Replace("в", "v") _
                .Replace("Г", "G").Replace("г", "g") _
                .Replace("Д", "D").Replace("д", "d") _
                .Replace("Е", "E").Replace("е", "e") _
                .Replace("Ё", "E").Replace("ё", "e") _
                .Replace("Ж", "Zh").Replace("ж", "zh") _
                .Replace("З", "Z").Replace("з", "z") _
                .Replace("И", "I").Replace("и", "i") _
                .Replace("Й", "I").Replace("й", "i") _
                .Replace("К", "K").Replace("к", "k") _
                .Replace("Л", "L").Replace("л", "l") _
                .Replace("М", "M").Replace("м", "m") _
                .Replace("Н", "N").Replace("н", "n") _
                .Replace("О", "O").Replace("о", "o") _
                .Replace("П", "P").Replace("п", "p") _
                .Replace("Р", "R").Replace("р", "r") _
                .Replace("С", "S").Replace("с", "s") _
                .Replace("Т", "T").Replace("т", "t") _
                .Replace("У", "U").Replace("у", "u") _
                .Replace("Ф", "F").Replace("ф", "f") _
                .Replace("Х", "Kh").Replace("х", "kh") _
                .Replace("Ц", "Ts").Replace("ц", "ts") _
                .Replace("Ч", "Ch").Replace("ч", "ch") _
                .Replace("Ш", "Sh").Replace("ш", "sh") _
                .Replace("Щ", "Shch").Replace("щ", "shch") _
                .Replace("Ъ", "Ie").Replace("ъ", "ie") _
                .Replace("Ы", "Y").Replace("ы", "y") _
                .Replace("Ь", "").Replace("ь", "") _
                .Replace("Э", "E").Replace("э", "e") _
                .Replace("Ю", "Iu").Replace("ю", "iu") _
                .Replace("Я", "Ia").Replace("я", "ia")
        Return result
    End Function
End Module
