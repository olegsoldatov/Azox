Namespace Controllers
    Public Class AboutController
        Inherits Controller

        <OutputCache(CacheProfile:="Pages")>
        Public Function V1() As ActionResult
            Return View()
        End Function

        <OutputCache(CacheProfile:="Pages")>
        Public Function V2() As ActionResult
            Return View()
        End Function
    End Class
End Namespace