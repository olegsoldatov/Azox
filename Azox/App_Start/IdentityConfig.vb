Imports System.Security.Claims
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports System.Net.Mail
Imports System.Threading.Tasks

Public Class EmailService
	Implements IIdentityMessageService

	Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
		Using mailMessage As New MailMessage
			mailMessage.From = New MailAddress("company@soldata.ru", "Soldata")
			mailMessage.To.Add(message.Destination)
			mailMessage.Subject = message.Subject
			mailMessage.Body = message.Body
			mailMessage.IsBodyHtml = True

			Using client As New SmtpClient
				client.Send(mailMessage)
			End Using
		End Using

		Return Task.FromResult(0)
	End Function
End Class

Public Class SmsService
	Implements IIdentityMessageService

	Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
		' Подключите здесь службу SMS, чтобы отправить текстовое сообщение.
		Return Task.FromResult(0)
	End Function
End Class

' Настройка диспетчера пользователей приложения. UserManager определяется в ASP.NET Identity и используется приложением.
Public Class ApplicationUserManager
	Inherits UserManager(Of ApplicationUser)

	Public Sub New(store As IUserStore(Of ApplicationUser))
		MyBase.New(store)
	End Sub

	Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationUserManager), context As IOwinContext)
		Dim manager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(context.Get(Of ApplicationDbContext)()))

		' Настройка логики проверки имен пользователей
		manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
			.AllowOnlyAlphanumericUserNames = False,
			.RequireUniqueEmail = True
		}

		' Настройка логики проверки паролей
		manager.PasswordValidator = New PasswordValidator With {
			.RequiredLength = 6,
			.RequireNonLetterOrDigit = False,
			.RequireDigit = False,
			.RequireLowercase = False,
			.RequireUppercase = False
		}

		' Настройка параметров блокировки по умолчанию
		manager.UserLockoutEnabledByDefault = True
		manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
		manager.MaxFailedAccessAttemptsBeforeLockout = 5

		' Регистрация поставщиков двухфакторной проверки подлинности. Для получения кода проверки пользователя в данном приложении используется телефон и сообщения электронной почты
		' Здесь можно указать собственный поставщик и подключить его.
		manager.RegisterTwoFactorProvider("Код, полученный по телефону", New PhoneNumberTokenProvider(Of ApplicationUser) With {
										  .MessageFormat = "Ваш код безопасности: {0}"
									  })
		manager.RegisterTwoFactorProvider("Код из сообщения", New EmailTokenProvider(Of ApplicationUser) With {
										  .Subject = "Код безопасности",
										  .BodyFormat = "Ваш код безопасности: {0}"
										  })
		manager.EmailService = New EmailService()
		manager.SmsService = New SmsService()
		Dim dataProtectionProvider = options.DataProtectionProvider
		If (dataProtectionProvider IsNot Nothing) Then
			manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity"))
		End If

		Return manager
	End Function

End Class

' Настройка диспетчера входа для приложения.
Public Class ApplicationSignInManager
	Inherits SignInManager(Of ApplicationUser, String)
	Public Sub New(userManager As ApplicationUserManager, authenticationManager As IAuthenticationManager)
		MyBase.New(userManager, authenticationManager)
	End Sub

	Public Overrides Function CreateUserIdentityAsync(user As ApplicationUser) As Task(Of ClaimsIdentity)
		Return user.GenerateUserIdentityAsync(DirectCast(UserManager, ApplicationUserManager))
	End Function

	Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationSignInManager), context As IOwinContext) As ApplicationSignInManager
		Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
	End Function
End Class
