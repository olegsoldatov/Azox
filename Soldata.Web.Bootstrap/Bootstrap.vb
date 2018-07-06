Imports System.Runtime.CompilerServices
Imports System.Web.Routing

''' <summary>
''' Предосталвяет методы расширения для визуализации элементов Bootstrap.
''' </summary>
Public Module Bootstrap
    Private _ParentUrl As String

#Region " Breadcrumbs "

    ''' <summary>
    ''' Предоставляет разметку навигации типа "хлебные крошки".
    ''' </summary>
    ''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function Breadcrumbs(helper As HtmlHelper) As MvcHtmlString
        Return Breadcrumbs(helper, Nothing)
    End Function

    ''' <summary>
    ''' Предоставляет разметку навигации типа "хлебные крошки".
    ''' </summary>
    ''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
    ''' <returns>Объект <see cref="MvcHtmlString" />.</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function Breadcrumbs(helper As HtmlHelper, htmlAttributes As Object) As MvcHtmlString
        _ParentUrl = helper.ViewBag.ParentUrl
        Dim node = SiteMap.CurrentNode

        If node IsNot Nothing AndAlso node Is SiteMap.RootNode Then
            Return New MvcHtmlString(String.Empty)
        ElseIf node Is Nothing And Not String.IsNullOrEmpty(_ParentUrl) Then
            node = SiteMap.Provider.FindSiteMapNode(_ParentUrl)
        End If

        If node Is Nothing Then
            node = SiteMap.RootNode
        End If

        Dim olTag As New TagBuilder("ol")
        With olTag
            .MergeAttributes(New RouteValueDictionary(htmlAttributes))
            .InnerHtml = BuildBreadcrumb(olTag, node)
        End With

        Return MvcHtmlString.Create(olTag.ToString(TagRenderMode.Normal))
    End Function

    Private Function BuildBreadcrumb(ul As TagBuilder, node As SiteMapNode) As String
        If Not String.IsNullOrEmpty(node.Url) Then
            ul.InnerHtml = BuildListItem(node) & ul.InnerHtml
        End If
        'Если узел имеет родительский узел, то повторно вызывается функция BuildBreadcrumb.
        If node.ParentNode IsNot Nothing Then
            BuildBreadcrumb(ul, node.ParentNode)
        End If
        Return ul.InnerHtml
    End Function

    Private Function BuildListItem(node As SiteMapNode) As String
        Dim liTag As New TagBuilder("li")
        With liTag
            If node.Equals(SiteMap.CurrentNode) Then
                .AddCssClass("active")
                .InnerHtml = node.Title
            Else
                .InnerHtml = BuildBreadcrumbAnchor(node)
            End If
        End With
        Return liTag.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildBreadcrumbAnchor(node As SiteMapNode) As String
        Dim aTag As New TagBuilder("a")
        With aTag
            .InnerHtml = node.Title
            If Not node.Equals(SiteMap.CurrentNode) Then
                .Attributes.Add("href", VirtualPathUtility.ToAbsolute(node.Url))
                'Если узел имеет описание, то добавим к тэгу аттрибут title.
                If Not String.IsNullOrEmpty(node.Description) Then .Attributes.Add("title", node.Description)
            End If
        End With
        Return aTag.ToString(TagRenderMode.Normal)
    End Function

#End Region

#Region " Pagination "

    Private _PageIndex As Integer
    Private _PageCount As Integer
    Private _StartPageIndex As Integer
    Private _ViewContext As ViewContext
    Private _pageIndexName As String = "pageIndex"

    ''' <summary>
    ''' Отображает немаркированный список постраничной навигации.
    ''' </summary>
    ''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку элементов постраничной навигации.</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function Pagination(helper As HtmlHelper) As MvcHtmlString
        Return Pagination(helper, Nothing)
    End Function

    ''' <summary>
    ''' Отображает немаркированный список постраничной навигации.
    ''' </summary>
    ''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
    ''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку элементов постраничной навигации.</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function Pagination(helper As HtmlHelper, htmlAttributes As Object) As MvcHtmlString
        Return Pagination(helper, helper.ViewBag.PageIndex, helper.ViewBag.PageCount, htmlAttributes)
    End Function

    ''' <summary>
    ''' Отображает немаркированный список постраничной навигации.
    ''' </summary>
    ''' <param name="helper">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
    ''' <param name="pageIndex">Индекс страницы.</param>
    ''' <param name="pageCount">Количество страниц.</param>
    ''' <param name="htmlAttributes">Объект, который содержит HTML-аттрибуты для элемента.</param>
    ''' <returns>Объект <see cref="MvcHtmlString" />, содержащий HTML-разметку элементов постраничной навигации.</returns>
    <Extension()>
    Public Function Pagination(helper As HtmlHelper, pageIndex As Integer, pageCount As Integer, htmlAttributes As Object) As MvcHtmlString
        If pageCount <= 1 Then Return MvcHtmlString.Empty

        _PageIndex = pageIndex
        _PageCount = pageCount
        _ViewContext = helper.ViewContext

        Return MvcHtmlString.Create(BuildUl(htmlAttributes))
    End Function

    Private Function BuildUl(htmlAttributes As Object) As String
        Dim ul As New TagBuilder("ul")

        With ul
            .MergeAttributes(New RouteValueDictionary(htmlAttributes))

            'Ссылка на предыдущую страницу.
            .InnerHtml += BuildLi(_PageIndex - 1, "&laquo;", Not (_PageIndex > 0))

            'Ссылки на страницы.
            Dim limit = 10
            If _PageCount < limit Then limit = _PageCount

            Dim mid = CInt(Math.Floor(CDbl(limit) / 2))

            If _PageIndex > (_StartPageIndex + mid) And (_StartPageIndex + limit) < _PageCount Then
                _StartPageIndex += 1
            ElseIf _PageIndex < (_StartPageIndex + mid) And _StartPageIndex > 0 Then
                _StartPageIndex -= 1
            End If

            For i = _StartPageIndex To (_StartPageIndex + limit) - 1
                .InnerHtml += BuildLi(i, (i + 1).ToString(), False)
            Next

            'Ссылка на следующую страницу.
            .InnerHtml += BuildLi(_PageIndex + 1, "&raquo;", Not (_PageIndex < (_PageCount - 1)))
        End With

        Return ul.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildLi(pageIndex As Integer, text As String, disabled As Boolean) As String
        Dim li As New TagBuilder("li")

        Dim routeValues As New RouteValueDictionary(_ViewContext.RouteData.Values)
        Dim queryString = _ViewContext.HttpContext.Request.QueryString
        For Each key In queryString.AllKeys
            If Not key = _pageIndexName Then
                Dim value = queryString(key)
                routeValues.Add(key, value)
            End If
        Next
        If pageIndex > 0 Then
            routeValues.Add(_pageIndexName, pageIndex)
        End If

        With li
            If disabled Then
                .AddCssClass("disabled")
                .InnerHtml = BuildA("#", text)
            ElseIf pageIndex = _PageIndex Then
                .AddCssClass("active")
                .InnerHtml = BuildA("#", text & " " & BuildSpan("(current)"))
            Else
                Dim urlHelper As New UrlHelper(_ViewContext.RequestContext)
                .InnerHtml = BuildA(urlHelper.RouteUrl(routeValues), text)
            End If
        End With

        Return li.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildA(href As String, text As String) As String
        Dim aTag As New TagBuilder("a")

        aTag.InnerHtml = text
        aTag.Attributes.Add("href", href)

        Return aTag.ToString(TagRenderMode.Normal)
    End Function

    Private Function BuildSpan(text As String) As String
        Dim spanTag As New TagBuilder("span")

        spanTag.AddCssClass("sr-only")
        spanTag.InnerHtml = text

        Return spanTag.ToString(TagRenderMode.Normal)
    End Function

#End Region

End Module
