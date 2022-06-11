Imports System.Net
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity

Namespace Controllers
	Public Class ContactsController
		Inherits Controller

        Private ReadOnly PageManager As PageManager

        Public Sub New(pageManager As PageManager)
            Me.PageManager = pageManager
        End Sub

        <HttpGet>
		Public Async Function Index() As Task(Of ActionResult)
            Return View(Await PageManager.GetPageAsync(Of ContactsPage))
        End Function

        <HttpPost>
		<ValidateAntiForgeryToken>
		Public Function ContactForm(model As ContactFormViewModel) As ActionResult
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
			body.AppendFormat("Телефон: {0}", model.Phone).AppendLine().AppendLine()
			body.AppendLine("Сообщение:")
			body.AppendLine(model.Message)

			Return body.ToString
		End Function
	End Class
End Namespace
