Namespace Models.Managers

    Public Class PageManager
        Inherits Soldata.Azox.Manager(Of Page)

        Sub New(store As PageStore)
            MyBase.New(store)
        End Sub

        Public Function GetAllAsync() As Task(Of List(Of Page))
            Return CType(MyBase.Store, PageStore).Context.Set(Of Page) _
                .ToListAsync()
        End Function

        Public Function GetPageAsync(actionName As String, controllerName As String) As Task(Of Page)
            Return CType(MyBase.Store, PageStore).Context.Set(Of Page) _
                .Where(Function(m) m.ActionName = actionName And m.ControllerName = controllerName) _
                .SingleOrDefaultAsync()
        End Function
    End Class

End Namespace
