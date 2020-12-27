Public Interface ICategory
	Property Id As Guid
	Property Name As String
	Property Title As String
	Property Path As String
	Property ImageId As Guid?
	Property Order As Integer?
	Property ParentId As Guid?
End Interface
