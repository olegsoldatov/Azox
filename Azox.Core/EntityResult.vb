''' <summary>
''' Предоставляет результат действия диспетчера сущностей.
''' </summary>
Public Class EntityResult
	Public Sub New(errors As IEnumerable(Of String))
		_Errors = errors
	End Sub

	Public Sub New(ParamArray errors() As String)
		_Errors = errors.AsEnumerable
	End Sub

	Protected Sub New(success As Boolean)
		_Succeeded = success
	End Sub

	Public Shared ReadOnly Property Success As EntityResult
		Get
			Return New EntityResult(True)
		End Get
	End Property

	Public Shared Function Failed(ParamArray errors() As String) As EntityResult
		Return New EntityResult(errors)
	End Function

	Public ReadOnly Property Succeeded As Boolean

	Public ReadOnly Property Errors As IEnumerable(Of String)
End Class
