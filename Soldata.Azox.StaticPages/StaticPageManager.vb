Imports System.Data.Entity

''' <summary>
''' Предоставляет диспетчер статических страниц приложения.
''' </summary>
''' <typeparam name="TContext">Тип контекста данных.</typeparam>
Public Class StaticPageManager(Of TContext As {DbContext, IStaticPageDbContext})
    Implements IDisposable

    Private _Db As TContext

    ''' <summary>
    ''' Инициализирует экземпляр класса.
    ''' </summary>
    ''' <param name="context">Контекст данных.</param>
    Public Sub New(context As TContext)
        _Db = context
    End Sub

    ''' <summary>
    ''' Возвращает контекст данных.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Context As TContext
        Get
            Return _Db
        End Get
    End Property

    Public ReadOnly Property StaticPages As IQueryable(Of StaticPage)
        Get
            Return _Db.StaticPages.AsQueryable
        End Get
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _Db.Dispose()
            End If
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
#End Region

    ''' <summary>
    ''' Получает из базы данных экземпляр статичной страницы. Если статичная страница отсутствует, то создает новый экземпляр. Новый экземпляр не сохраняется в базе данных.
    ''' </summary>
    ''' <param name="actionName">Имя действия. При создании нового экземпляра также становится названием страницы.</param>
    ''' <param name="controllerName">Имя контроллера.</param>
    ''' <returns>Существующий или новый экземпляр класса <see cref="StaticPage"/>.</returns>
    Public Overridable Function GetStaticPageOrCreate(actionName As String, controllerName As String) As StaticPage
        Dim staticPage = _Db.StaticPages.FirstOrDefault(Function(m) m.ActionName = actionName And m.ControllerName = controllerName)
        If IsNothing(staticPage) Then
            staticPage = New StaticPage With {.Id = Guid.NewGuid, .ActionName = actionName, .ControllerName = controllerName, .Title = actionName}
        End If
        Return staticPage
    End Function

    Public Overridable Function FindAsync(id As Guid) As Task(Of StaticPage)
        Return _Db.StaticPages.FindAsync(id)
    End Function

    Public Overridable Function UpdateAsync(staticPage As StaticPage) As Task(Of Integer)
        _Db.Entry(staticPage).State = EntityState.Modified
        Return _Db.SaveChangesAsync
    End Function
End Class
