Imports System.Data.Entity
''' <summary>
''' Базовый контроллер приложения с поддержкой доступа к базе данных с помощью EntityFramework.
''' </summary>
Public MustInherit Class DbController
	Inherits DbController(Of ApplicationDbContext)

	Public Sub New()
		MyBase.New(New ApplicationDbContext)
	End Sub
End Class

''' <summary>
''' Базовый контроллер приложения с поддержкой доступа к базе данных с помощью EntityFramework.
''' </summary>
Public MustInherit Class DbController(Of TContext As DbContext)
	Inherits Controller

	Protected Friend ReadOnly Property Db As TContext

	Public Sub New(context As TContext)
		_Db = context
	End Sub

	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If _Db IsNot Nothing Then
				_Db.Dispose()
				_Db = Nothing
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	Protected Friend Function RedirectToLocal(returnUrl As String) As ActionResult
		If Url.IsLocalUrl(returnUrl) Then
			Return Redirect(returnUrl)
		End If
		Return RedirectToAction("Index")
	End Function
End Class
