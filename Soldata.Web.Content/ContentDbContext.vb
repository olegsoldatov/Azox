Imports System.Data.Entity

Partial Public Class ContentDbContext
    Inherits DbContext

    Protected Sub New(nameOrConnectionString As String)
        MyBase.New(nameOrConnectionString)
    End Sub

    Public Property Contents As DbSet(Of Content)
End Class
