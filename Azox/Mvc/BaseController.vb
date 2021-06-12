Namespace Mvc
    Public MustInherit Class BaseController
        Inherits Controller

        Protected Sub Pagination(count As Integer, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50)
            ViewBag.Count = count
            ViewBag.PageIndex = pageIndex
            ViewBag.PageSize = pageSize
            ViewBag.PageCount = CInt(Math.Ceiling(count / pageSize))
        End Sub
    End Class
End Namespace