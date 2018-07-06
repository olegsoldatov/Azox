Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Runtime.CompilerServices

''' <summary>
''' Предоставляет методы расширения для визуализации гиперссылки, ведущей на домашнюю страницу сайта.
''' </summary>
''' <remarks>
''' Гиперссылка всегда ведет на домашнюю страницу сайта. При этом, если страница, с которой вызывается метод расширения, является домашней, то гиперссылка становиться неактивной, чтобы страница не вела на саму себя.
''' </remarks>
Public Module HomeLinkHtmlHelper
    ''' <summary>
    ''' Отображает гиперссылку, ведущую на домашнюю страницу сайта. Если гиперссылка размещена на домашней странице, то гиперссылка неактивна.
    ''' </summary>
    ''' <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <param name="linkText">Текст гиперссылки.</param>
    ''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку для элемента <c>a</c>.</returns>
    <Extension()>
    Public Function HomeLink(html As HtmlHelper, linkText As String) As MvcHtmlString
        Return HomeLink(html, linkText, String.Empty, Nothing)
    End Function

    ''' <summary>
    ''' Отображает гиперссылку, ведущую на домашнюю страницу сайта. Если гиперссылка размещена на домашней странице, то гиперссылка неактивна.
    ''' </summary>
    ''' <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <param name="linkText">Текст гиперссылки.</param>
    ''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
    ''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку для элемента <c>a</c>.</returns>
    <Extension()>
    Public Function HomeLink(html As HtmlHelper, linkText As String, htmlAttributes As Object) As MvcHtmlString
        Return HomeLink(html, linkText, String.Empty, htmlAttributes)
    End Function

    ''' <summary>
    ''' Отображает гиперссылку, ведущую на домашнюю страницу сайта. Если гиперссылка размещена на домашней странице, то гиперссылка неактивна.
    ''' </summary>
    ''' <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <param name="linkText">Текст гиперссылки.</param>
    ''' <param name="virtualImageUrl">Виртуальный путь к файлу изображения.</param>
    ''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку для элемента <c>a</c>.</returns>
    <Extension()>
    Public Function HomeLink(html As HtmlHelper, linkText As String, virtualImageUrl As String) As MvcHtmlString
        Return HomeLink(html, linkText, virtualImageUrl, Nothing)
    End Function

    ''' <summary>
    ''' Отображает гиперссылку, ведущую на домашнюю страницу сайта, в виде изображения. Если гиперссылка размещена на домашней странице, то гиперссылка неактивна.
    ''' </summary>
    ''' <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <param name="linkText">Текст гиперссылки.</param>
    ''' <param name="virtualImageUrl">Виртуальный путь к файлу изображения.</param>
    ''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
    ''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку для элемента <c>a</c>.</returns>
    <Extension()>
    Public Function HomeLink(html As HtmlHelper, linkText As String, virtualImageUrl As String, htmlAttributes As Object) As MvcHtmlString
        Dim rootUrl As String = VirtualPathUtility.ToAbsolute("~/")

        If Not html.ViewContext.HttpContext.Request.Url.AbsolutePath.Equals(rootUrl) Then
            Dim aTag As New TagBuilder("a")

            With aTag
                .MergeAttributes(New RouteValueDictionary(htmlAttributes))
                .Attributes.Add("href", rootUrl)
                .Attributes.Add("rel", "home")
                .InnerHtml = GetLinkInnerHtml(linkText, virtualImageUrl)
            End With

            Return New MvcHtmlString(aTag.ToString(TagRenderMode.Normal))
        Else
            Dim spanTag As New TagBuilder("span")

            With spanTag
                .MergeAttributes(New RouteValueDictionary(htmlAttributes))
                .InnerHtml = GetLinkInnerHtml(linkText, virtualImageUrl)
            End With

            Return New MvcHtmlString(spanTag.ToString(TagRenderMode.Normal))
        End If
    End Function

    Private Function GetLinkInnerHtml(linkText As String, virtualImageUrl As String) As String
        If String.IsNullOrEmpty(virtualImageUrl) Then
            Return linkText
        Else
            Dim imgTag As New TagBuilder("img")

            imgTag.Attributes.Add("src", VirtualPathUtility.ToAbsolute(virtualImageUrl))
            imgTag.Attributes.Add("alt", linkText)

            Return imgTag.ToString(TagRenderMode.SelfClosing)
        End If
    End Function
End Module
