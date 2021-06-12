Imports System.Data.Entity
Imports System.Threading.Tasks

Public Class PageManager
	Inherits EntityManager(Of Page)

	Public Sub New(context As DbContext)
		MyBase.New(context)
	End Sub

	Public ReadOnly Property Pages As IQueryable(Of Page)
		Get
			Return Context.Set(Of Page)
		End Get
	End Property

	''' <summary>
	''' Находит страницу по абсолютному пути.
	''' </summary>
	''' <param name="absolutePath">Абсолютный путь.</param>
	Public Async Function FindByAbsolutePathAsync(absolutePath As String) As Task(Of Page)
		If String.IsNullOrEmpty(absolutePath) Then
			Return Nothing
		End If
		Return Await Pages.FirstOrDefaultAsync(Function(x) x.AbsolutePath = absolutePath)
	End Function
End Class
