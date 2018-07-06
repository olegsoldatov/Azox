Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

' Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
Public Class ApplicationUser
	Inherits IdentityUser
	Public Async Function GenerateUserIdentityAsync(manager As UserManager(Of ApplicationUser)) As Task(Of ClaimsIdentity)
		' Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
		Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
		' Здесь добавьте утверждения пользователя
		Return userIdentity
	End Function
End Class

Public Class ApplicationDbContext
	Inherits IdentityDbContext(Of ApplicationUser)

	Public Sub New()
		MyBase.New("DefaultConnection", throwIfV1Schema:=False)
	End Sub

	Public Shared Function Create() As ApplicationDbContext
		Return New ApplicationDbContext()
	End Function
End Class
