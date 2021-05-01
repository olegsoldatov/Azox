Imports Microsoft.AspNet.Identity.EntityFramework

Public Class ApplicationDbContext
	Inherits IdentityDbContext(Of ApplicationUser)

	Public Sub New()
		MyBase.New("DefaultConnection", throwIfV1Schema:=False)
	End Sub

	Public Shared Function Create() As ApplicationDbContext
		Return New ApplicationDbContext()
	End Function
End Class
