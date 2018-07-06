Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Owin

Partial Public Class Startup
	' Дополнительные сведения о настройке проверки подлинности см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301864
	Public Sub ConfigureAuth(app As IAppBuilder)
		' Настройка контекста базы данных, диспетчера пользователей и диспетчера входа для использования одного экземпляра на запрос
		app.CreatePerOwinContext(AddressOf ApplicationDbContext.Create)
		app.CreatePerOwinContext(Of ApplicationUserManager)(AddressOf ApplicationUserManager.Create)
		app.CreatePerOwinContext(Of ApplicationSignInManager)(AddressOf ApplicationSignInManager.Create)

		' Включение использования файла cookie, в котором приложение может хранить информацию для пользователя, выполнившего вход
		' и использование файла cookie для временного хранения информации о входах пользователя с помощью стороннего поставщика входа
		' Настройка файла cookie для входа
		' OnValidateIdentity позволяет приложению проверять метку безопасности при входе пользователя.
		' Эта функция безопасности используется, когда вы меняете пароль или добавляете внешнее имя входа в свою учетную запись.
		app.UseCookieAuthentication(New CookieAuthenticationOptions() With {
			.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
			.Provider = New CookieAuthenticationProvider() With {
				.OnValidateIdentity = SecurityStampValidator.OnValidateIdentity(Of ApplicationUserManager, ApplicationUser)(
					validateInterval:=TimeSpan.FromMinutes(30),
					regenerateIdentity:=Function(manager, user) user.GenerateUserIdentityAsync(manager))},
			.LoginPath = New PathString("/admin/account/login")})

		app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)

		' Позволяет приложению временно хранить информацию о пользователе, пока проверяется второй фактор двухфакторной проверки подлинности.
		app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5))

		' Позволяет приложению запомнить второй фактор проверки имени входа. Например, это может быть телефон или почта.
		' Если выбрать этот параметр, то на устройстве, с помощью которого вы входите, будет сохранен второй шаг проверки при входе.
		' Точно так же действует параметр RememberMe при входе.
		app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie)
	End Sub
End Class
