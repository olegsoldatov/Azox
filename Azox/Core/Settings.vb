''' <summary>
''' Предоставляет структурированные параметры приложения.
''' </summary>
Public Class Settings
    Private Shared _Items As IEnumerable(Of Setting)

    Public Shared ReadOnly Property Items(name As String) As Setting
        Get
            Initialize()
            Return _Items.SingleOrDefault(Function(x) x.Name = name)
        End Get
    End Property

    Private Shared Sub Initialize()
        _Items = New List(Of Setting) From {New Setting With {.Name = "Test", .Value = "Test", .Description = "Description"}}
    End Sub
End Class
