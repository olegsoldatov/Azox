Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class ManageController
		Inherits Controller
		Public Sub New()
		End Sub

		Public Sub New(manager As ApplicationUserManager)
			UserManager = manager
		End Sub

		Private _userManager As ApplicationUserManager
		Public Property UserManager() As ApplicationUserManager
			Get
				Return If(_userManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
			End Get
			Private Set(value As ApplicationUserManager)
				_userManager = value
			End Set
		End Property

		'
		' GET: /Manage/Index
		Public Function Index(message As ManageMessageId?) As ActionResult
			ViewData!StatusMessage = If(message = ManageMessageId.ChangePasswordSuccess, "Ваш пароль изменен.", If(message = ManageMessageId.SetPasswordSuccess, "Пароль задан.", If(message = ManageMessageId.SetTwoFactorSuccess, "Настроен поставщик двухфакторной проверки подлинности.", If(message = ManageMessageId.[Error], "Произошла ошибка.", If(message = ManageMessageId.AddPhoneSuccess, "Ваш номер телефона добавлен.", If(message = ManageMessageId.RemovePhoneSuccess, "Ваш номер телефона удален.", ""))))))
			Return View()
		End Function

		'
		' GET: /Manage/RemoveLogin
		Public Function RemoveLogin() As ActionResult
			Dim linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId())
			ViewBag.ShowRemoveButton = HasPassword() OrElse linkedAccounts.Count > 1
			Return View(linkedAccounts)
		End Function

		'
		' POST: /Manage/RemoveLogin
		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function RemoveLogin(loginProvider As String, providerKey As String) As Task(Of ActionResult)
			Dim message As System.Nullable(Of ManageMessageId)
			Dim result = Await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), New UserLoginInfo(loginProvider, providerKey))
			If result.Succeeded Then
				Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
				If userInfo IsNot Nothing Then
					Await SignInAsync(userInfo, isPersistent:=False)
				End If
				message = ManageMessageId.RemoveLoginSuccess
			Else
				message = ManageMessageId.[Error]
			End If
			Return RedirectToAction("ManageLogins", New With {
			  .Message = message
		})
		End Function

		'
		' GET: /Manage/AddPhoneNumber
		Public Function AddPhoneNumber() As ActionResult
			Return View()
		End Function

		'
		' POST: /Manage/AddPhoneNumber
		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function AddPhoneNumber(model As AddPhoneNumberViewModel) As Task(Of ActionResult)
			If Not ModelState.IsValid Then
				Return View(model)
			End If
			' Создание и отправка маркера
			Dim code = Await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number)
			If UserManager.SmsService IsNot Nothing Then
				Dim message = New IdentityMessage() With {
				.Destination = model.Number,
				.Body = "Ваш код безопасности: " & Convert.ToString(code)
			}
				Await UserManager.SmsService.SendAsync(message)
			End If
			Return RedirectToAction("VerifyPhoneNumber", New With {
			  .PhoneNumber = model.Number
		})
		End Function

		'
		' POST: /Manage/EnableTwoFactorAuthentication
		<HttpPost>
		Public Async Function EnableTwoFactorAuthentication() As Task(Of ActionResult)
			Await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), True)
			Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
			If userInfo IsNot Nothing Then
				Await SignInAsync(userInfo, isPersistent:=False)
			End If
			Return RedirectToAction("index", "manage")
		End Function

		'
		' POST: /Manage/DisableTwoFactorAuthentication
		<HttpPost>
		Public Async Function DisableTwoFactorAuthentication() As Task(Of ActionResult)
			Await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), False)
			Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
			If userInfo IsNot Nothing Then
				Await SignInAsync(userInfo, isPersistent:=False)
			End If
			Return RedirectToAction("index", "manage")
		End Function

		'
		' GET: /Manage/VerifyPhoneNumber
		Public Async Function VerifyPhoneNumber(phoneNumber As String) As Task(Of ActionResult)
			Dim code = Await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber)
			' Отправка SMS через поставщик SMS для проверки номера телефона
			Return If(phoneNumber Is Nothing, View("Error"), View(New VerifyPhoneNumberViewModel() With {
			.PhoneNumber = phoneNumber
		}))
		End Function

		'
		' POST: /Manage/VerifyPhoneNumber
		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function VerifyPhoneNumber(model As VerifyPhoneNumberViewModel) As Task(Of ActionResult)
			If Not ModelState.IsValid Then
				Return View(model)
			End If
			Dim result = Await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code)
			If result.Succeeded Then
				Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
				If userInfo IsNot Nothing Then
					Await SignInAsync(userInfo, isPersistent:=False)
				End If
				Return RedirectToAction("index", New With {
				.Message = ManageMessageId.AddPhoneSuccess
			})
			End If
			' Это сообщение означает наличие ошибки; повторное отображение формы
			ModelState.AddModelError("", "Не удалось проверить телефон")
			Return View(model)
		End Function

		'
		' GET: /Manage/RemovePhoneNumber
		Public Async Function RemovePhoneNumber() As Task(Of ActionResult)
			Dim result = Await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), Nothing)
			If Not result.Succeeded Then
				Return RedirectToAction("index", New With {
				.Message = ManageMessageId.[Error]
			})
			End If
			Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
			If userInfo IsNot Nothing Then
				Await SignInAsync(userInfo, isPersistent:=False)
			End If
			Return RedirectToAction("index", New With {
			.Message = ManageMessageId.RemovePhoneSuccess
		})
		End Function

		'
		' GET: /Manage/ChangePassword
		Public Function ChangePassword() As ActionResult
			Return View()
		End Function

		'
		' POST: /Manage/ChangePassword
		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function ChangePassword(model As ChangePasswordViewModel) As Task(Of ActionResult)
			If Not ModelState.IsValid Then
				Return View(model)
			End If
			Dim result = Await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword)
			If result.Succeeded Then
				Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
				If userInfo IsNot Nothing Then
					Await SignInAsync(userInfo, isPersistent:=False)
				End If
				Return RedirectToAction("index", New With {
				.message = ManageMessageId.ChangePasswordSuccess
			})
			End If
			AddErrors(result)
			Return View(model)
		End Function

		'
		' GET: /Manage/SetPassword
		Public Function SetPassword() As ActionResult
			Return View()
		End Function

		'
		' POST: /Manage/SetPassword
		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function SetPassword(model As SetPasswordViewModel) As Task(Of ActionResult)
			If ModelState.IsValid Then
				Dim result = Await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword)
				If result.Succeeded Then
					Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
					If userInfo IsNot Nothing Then
						Await SignInAsync(userInfo, isPersistent:=False)
					End If
					Return RedirectToAction("Index", New With {
					.Message = ManageMessageId.SetPasswordSuccess
				})
				End If
				AddErrors(result)
			End If

			' Это сообщение означает наличие ошибки; повторное отображение формы
			Return View(model)
		End Function

		'
		' GET: /Manage/ManageLogins
		Public Async Function ManageLogins(message As System.Nullable(Of ManageMessageId)) As Task(Of ActionResult)
			ViewBag.StatusMessage = If(message = ManageMessageId.RemoveLoginSuccess, "Внешнее имя входа удалено.", If(message = ManageMessageId.[Error], "Произошла ошибка.", ""))
			Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
			If userInfo Is Nothing Then
				Return View("Error")
			End If
			Dim userLogins = Await UserManager.GetLoginsAsync(User.Identity.GetUserId())
			Dim otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(Function(auth) userLogins.All(Function(ul) auth.AuthenticationType <> ul.LoginProvider)).ToList()
			ViewBag.ShowRemoveButton = userInfo.PasswordHash IsNot Nothing OrElse userLogins.Count > 1
			Return View(New ManageLoginsViewModel() With {
			.CurrentLogins = userLogins,
			.OtherLogins = otherLogins
		})
		End Function

		'
		' POST: /Manage/LinkLogin
		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Function LinkLogin(provider As String) As ActionResult
			' Запрос перенаправления к внешнему поставщику входа для связывания имени входа текущего пользователя
			Return New AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId())
		End Function

		'
		' GET: /Manage/LinkLoginCallback
		Public Async Function LinkLoginCallback() As Task(Of ActionResult)
			Dim loginInfo = Await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId())
			If loginInfo Is Nothing Then
				Return RedirectToAction("ManageLogins", New With {
				.Message = ManageMessageId.[Error]
			})
			End If
			Dim result = Await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login)
			Return If(result.Succeeded, RedirectToAction("ManageLogins"), RedirectToAction("ManageLogins", New With {
			.Message = ManageMessageId.[Error]
		}))
		End Function

#Region "Вспомогательные приложения"
		' Используется для защиты от XSRF-атак при добавлении внешних имен входа
		Private Const XsrfKey As String = "XsrfId"

		Private ReadOnly Property AuthenticationManager() As IAuthenticationManager
			Get
				Return HttpContext.GetOwinContext().Authentication
			End Get
		End Property

		Private Async Function SignInAsync(user As ApplicationUser, isPersistent As Boolean) As Task
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie)
			AuthenticationManager.SignIn(New AuthenticationProperties() With {
			.IsPersistent = isPersistent
		}, Await user.GenerateUserIdentityAsync(UserManager))
		End Function

		Private Sub AddErrors(result As IdentityResult)
			For Each [error] In result.Errors
				ModelState.AddModelError("", [error])
			Next
		End Sub

		Private Function HasPassword() As Boolean
			Dim userInfo = UserManager.FindById(User.Identity.GetUserId())
			If userInfo IsNot Nothing Then
				Return userInfo.PasswordHash IsNot Nothing
			End If
			Return False
		End Function

		Private Function HasPhoneNumber() As Boolean
			Dim userInfo = UserManager.FindById(User.Identity.GetUserId())
			If userInfo IsNot Nothing Then
				Return userInfo.PhoneNumber IsNot Nothing
			End If
			Return False
		End Function

		Public Enum ManageMessageId
			AddPhoneSuccess
			ChangePasswordSuccess
			SetTwoFactorSuccess
			SetPasswordSuccess
			RemoveLoginSuccess
			RemovePhoneSuccess
			[Error]
		End Enum

#End Region
	End Class
End Namespace
