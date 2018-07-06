Imports System.Threading.Tasks

''' <summary>
''' Управляет содержанием.
''' </summary>
Public Class ContentManager(Of TContent As {Class, IContent})
    Implements IDisposable

    ''' <summary>
    ''' Инициализирует новый экземпляр класса <see cref="ContentManager(Of TContent)" />.
    ''' </summary>
    ''' <param name="store">Экземпляр хранилища, реализующий интерфейс <see cref="IContentStore(Of TContent)" /> с универсальным типом элемента.</param>
    Public Sub New(store As IContentStore(Of TContent))
        _Store = store
    End Sub

    Protected Friend Property Store As IContentStore(Of TContent)

    Public Overridable ReadOnly Property Contents As IQueryable(Of TContent)
        Get
            Return CType(_Store, IQueryableContentStore(Of TContent)).Contents
        End Get
    End Property

    Public Overridable Function CreateAsync(entity As TContent) As Task(Of Integer)
        Return _Store.CreateAsync(entity)
    End Function

    Public Overridable Function FindByIdAsync(id As Guid) As Task(Of TContent)
        Return _Store.FindByIdAsync(id)
    End Function

    Public Overridable Function UpdateAsync(entity As TContent) As Task(Of Integer)
        Return _Store.UpdateAsync(entity)
    End Function

    Public Overridable Function DeleteAsync(entity As TContent) As Task(Of Integer)
        Return _Store.DeleteAsync(entity)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Чтобы обнаружить избыточные вызовы

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                _Store.Dispose()
            End If

            _Store = Nothing
        End If
        Me.disposedValue = True
    End Sub

    ' Этот код добавлен редактором Visual Basic для правильной реализации шаблона высвобождаемого класса.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Не изменяйте этот код.  Разместите код очистки выше в методе Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
