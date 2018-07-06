Imports System.Text.RegularExpressions
Imports System.Runtime.CompilerServices
Imports System.Web.Mvc
Imports System.Web

''' <summary>
''' Предоставляет поддержку для простого текста в представлении MVC ASP.NET.
''' </summary>
Public Module PlainHtmlHelper
    ''' <summary>
    ''' Преобразует текст, содержащий HTML элементы, в простой.
    ''' </summary>
    ''' <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <param name="value">Строка, содержащая исходный текст.</param>
    ''' <param name="wordCount">Количество слов в результирующей строке.</param>    
    ''' <param name="trimLastDot">Разрешает удалять последнюю точку в результирующей строке.</param>    
    ''' <param name="useSuspPoints">Разрешает ставить многоточие, если исходная строка больше результирующей.</param>    
    ''' <returns>Простой текст без HTML элементов.</returns>
    ''' <remarks>
    ''' Функция удаляет из исходной строки все HTML-тэги.
    ''' Заменяет мнемонические комбинации Unicode-символами.
    ''' Подрезает строку до последнего вхождения символа точки, удаляя ее тоже.
    ''' Ставит в конце результирующей строки символ многоточия, если исходная строка больше, чем длина результирующей.    
    ''' </remarks>  
    <Extension>
    Public Function Plain(html As HtmlHelper, ByVal value As String, ByVal wordCount As Integer, ByVal trimLastDot As Boolean, ByVal useSuspPoints As Boolean) As IHtmlString
        'Если передается пустая строка, то закончим выполнение процедуры.
        If String.IsNullOrEmpty(value) Then Return New HtmlString(String.Empty)

        'Разделим исходную строку на слова, удалив HTML-тэги и заменив мнемонические коды на символы Юникод.
        Dim words() As String = html.ViewContext.HttpContext.Server.HtmlDecode(RemoveHtml(value)).Trim.Split(" ")

        'Если количество слов в массиве меньше или равно, чем значение wordCount и при этом разрешено ставить многоточие в конце, то отменим добавление многоточия, поскольку в нем нет уже необходимости.
        If words.Length <= wordCount And useSuspPoints Then useSuspPoints = False

        'Если размер массива больше, чем значение переменной wordCount, то уменьшим размер этого массива до значения wordCount.
        If words.Length > wordCount Then Array.Resize(words, wordCount)

        'Если последнее слово имеет размер в одну букву и не является цифрой или буквой, то оно удаляется.
        If words.Last.ToCharArray.Length = 1 AndAlso Not Char.IsLetterOrDigit(words.Last.ToCharArray.First) Then Array.Resize(words, words.Length - 1)

        'Если длина массива стала равна 0, то закончим выполнение процедуры.
        If words.Length = 0 Then Return New HtmlString(String.Empty)

        'Соберем строку заново.
        value = String.Join(" ", words).Trim

        'Если строка стала пустой, то закончим выполнение процедуры.
        If String.IsNullOrEmpty(value) Then Return New HtmlString(String.Empty)

        'Если последним символом в строке является запятая, точка с запятой, двоеточие, то эти символы удаляются.
        If value.ToCharArray.Last = "," Or value.ToCharArray.Last = ";" Or value.ToCharArray.Last = ":" Then value = value.Remove(value.Length - 1, 1)

        'Если строка стала пустой, то закончим выполнение процедуры.
        If String.IsNullOrEmpty(value) Then Return New HtmlString(String.Empty)

        'Здесь подрежем строку до последней точки, если она присутствует в результирующей строке, и если это задано параметром trimLastDot.
        If trimLastDot Then
            Dim dot As Integer = value.LastIndexOf(".")
            If dot > 0 Then
                value = value.Remove(dot, value.Length - dot)
            End If
        ElseIf useSuspPoints And CType(value.Substring(value.Length - 1, 1), Char) <> "." Then
            value += "..."
        End If

        Return New HtmlString(value)
    End Function

    ''' <summary>
    ''' Преобразует текст, содержащий HTML элементы, в простой.
    ''' </summary>
    ''' <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <param name="value">Строка, содержащая исходный текст.</param>
    ''' <returns>Простой текст без HTML элементов.</returns>
    ''' <remarks></remarks>
    <Extension>
    Public Function Plain(html As HtmlHelper, value As String) As IHtmlString
        'Если передается пустая строка, то закончим выполнение процедуры.
        If String.IsNullOrEmpty(value) Then Return New HtmlString(String.Empty)

        'Разделим исходную строку на слова, удалив HTML-тэги и заменив мнемонические коды на символы Юникод.
        Dim words() As String = html.ViewContext.HttpContext.Server.HtmlDecode(RemoveHtml(value)).Trim.Split(" ")

        'Если длина массива стала равна 0, то закончим выполнение процедуры.
        If words.Length = 0 Then Return New HtmlString(String.Empty)

        'Соберем строку заново.
        value = String.Join(" ", words).Trim

        'Если строка стала пустой, то закончим выполнение процедуры.
        If String.IsNullOrEmpty(value) Then Return New HtmlString(String.Empty)

        Return New HtmlString(value)
    End Function

    ''' <summary>
    ''' Удаляет все HTML-тэги в исходной строке.
    ''' </summary>
    ''' <param name="value">Исходная строка.</param>
    Private Function RemoveHtml(value As String) As String
        Dim result As New Regex("<[^>]*>")
        Return result.Replace(value, " ")
    End Function
End Module
