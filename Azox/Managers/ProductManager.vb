Imports System.Data.Entity

Namespace Managers
    Public Class ProductManager
        Inherits EntityManager(Of Product)

        Public Sub New(context As DbContext)
            MyBase.New(context)
        End Sub
    End Class
End Namespace
