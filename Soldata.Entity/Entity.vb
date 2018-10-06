Imports System.ComponentModel.DataAnnotations

Public Class Entity
	Implements IEntity

	Public Sub New()
		_Id = Guid.NewGuid
	End Sub

	<Key>
	Public Property Id As Guid Implements IEntity.Id
End Class
