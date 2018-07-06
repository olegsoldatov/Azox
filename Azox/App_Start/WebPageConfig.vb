Imports Soldata.Azox.Models

Public Class WebPageManager
    Inherits ContentManager(Of WebPageContent)

    Public Sub New(store As IContentStore(Of WebPageContent))
        MyBase.New(store)
    End Sub
End Class
