Namespace Models

    Public Class PageStore
        Implements IStore(Of Page)

        Private _Context As DbContext

        Sub New(context As DbContext)
            _Context = context
        End Sub

        Public ReadOnly Property Context As DbContext
            Get
                Return _Context
            End Get
        End Property

        Public Overridable Function CreateAsync(entity As Page) As Task Implements IStore(Of Page).CreateAsync
            _Context.Set(Of Page).Add(entity)
            Return _Context.SaveChangesAsync
        End Function

        Public Overridable Function DeleteAsync(entity As Page) As Task Implements IStore(Of Page).DeleteAsync
            _Context.Set(Of Page).Remove(entity)
            Return _Context.SaveChangesAsync
        End Function

        Public Overridable Function FindByIdAsync(id As Guid) As Task(Of Page) Implements IStore(Of Page).FindByIdAsync
            Return _Context.Set(Of Page).FindAsync(id)
        End Function

        Public Overridable Function UpdateAsync(entity As Page) As Task Implements IStore(Of Page).UpdateAsync
            _Context.Entry(entity).State = EntityState.Modified
            Return _Context.SaveChangesAsync
        End Function

#Region "IDisposable Support"
        Public Property DisposeContext As Boolean ' Чтобы обнаружить избыточные вызовы

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.DisposeContext Then
                If disposing Then
                    _Context.Dispose()
                End If

                _Context = Nothing
            End If
            Me.DisposeContext = True
        End Sub

        ' Этот код добавлен редактором Visual Basic для правильной реализации шаблона высвобождаемого класса.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Не изменяйте этот код.  Разместите код очистки выше в методе Dispose(disposing As Boolean).
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

End Namespace
