Imports System.Net
Imports Microsoft.AspNet.Identity

Namespace Controllers
	Public Class ContactsController
		Inherits Controller

		Function Index() As ActionResult
			Return View()
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Function ContactForm(model As ContactFormViewModel) As ActionResult
			If ModelState.IsValid Then
				Dim emailService As New EmailService
				emailService.Send(New IdentityMessage With {.Body = ContactFormBody(model), .Destination = "company@soldata.ru", .Subject = "Сообщение с сайта"})

				Return New HttpStatusCodeResult(HttpStatusCode.OK)
			End If

			Return New HttpStatusCodeResult(HttpStatusCode.InternalServerError)
		End Function

		Private Function ContactFormBody(model As ContactFormViewModel) As String
			Dim body As New StringBuilder

			body.AppendFormat("От: {0} <{1}>", model.Name, model.Email).AppendLine()
			body.AppendFormat("Тема: {0}", model.Subject).AppendLine().AppendLine()
			body.AppendLine("Сообщение:")
			body.AppendLine(model.Message)

			Return body.ToString
		End Function
	End Class
End Namespace
