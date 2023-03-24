''' <summary>
''' Результат операции с сущностью.
''' </summary>
Public Class EntityResult
	''' <summary>
	''' Инициализирует результат, принимающий сообщения об ошибках в случае сбоя.
	''' </summary>
	''' <param name="errors">Массив сообщений об ошибках.</param>
	Public Sub New(ParamArray errors() As String)
		Me.New(errors.AsEnumerable())
	End Sub

	''' <summary>
	''' Инициализирует результат, принимающий сообщения об ошибках в случае сбоя.
	''' </summary>
	''' <param name="errors">Перечисление сообщений об ошибках.</param>
	Public Sub New(errors As IEnumerable(Of String))
		If IsNothing(errors) Then
			errors = {My.Resources.DefaultError}
		End If
		Succeeded = False
		Me.Errors = errors
	End Sub

	''' <summary>
	''' Инициализирует результат, принимающий значение успеха.
	''' </summary>
	''' <param name="success"></param>
	Protected Sub New(success As Boolean)
		Succeeded = success
		Errors = Array.Empty(Of String)()
	End Sub

	''' <summary>
	''' Истина, если успешный результат.
	''' </summary>
	''' <returns></returns>
	Public ReadOnly Property Succeeded As Boolean

	''' <summary>
	''' Список ошибок.
	''' </summary>
	''' <returns></returns>
	Public ReadOnly Property Errors As IEnumerable(Of String)

	''' <summary>
	''' Успешный результат.
	''' </summary>
	''' <returns></returns>
	Public Shared ReadOnly Property Success As EntityResult
		Get
			Return New EntityResult(True)
		End Get
	End Property

	''' <summary>
	''' Вспомогательный метод неудачного результата.
	''' </summary>
	''' <param name="errors"></param>
	''' <returns></returns>
	Public Shared Function Failed(ParamArray errors() As String) As EntityResult
		Return New EntityResult(errors)
	End Function
End Class