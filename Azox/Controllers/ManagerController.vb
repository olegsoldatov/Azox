Imports Soldata.Azox

Namespace Controllers
	''' <summary>
	''' Предоставялет поддержку диспетчера сущности.
	''' </summary>
	''' <typeparam name="TManager">Диспетчер сущности.</typeparam>
	Public MustInherit Class ManagerController(Of TManager As EntityManager(Of TEntity), TEntity As {Class, IEntity})
		Inherits Controller

		''' <summary>
		''' Инициализирует экземпляр класса <see cref="ManagerController(Of TManager, TEntity)" />.
		''' </summary>
		''' <param name="manager">Диспетчер сущности.</param>
		Protected Sub New(manager As TManager)
			_Manager = manager
		End Sub

		''' <summary>
		''' Диспетчер сущности.
		''' </summary>
		Protected Friend ReadOnly Property Manager As TManager

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				Manager.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace