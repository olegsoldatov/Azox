Imports System.Threading.Tasks

Namespace Controllers
	Public Class AboutController
		Inherits Controller

        Private ReadOnly SettingManager As SettingManager

        Public Sub New(pageManager As PageManager, settingManager As SettingManager)
            Me.SettingManager = settingManager
        End Sub

        <HttpGet>
		Public Async Function Index() As Task(Of ActionResult)
            ViewBag.Title = My.Settings.About
            Return View(Await SettingManager.GetAboutAsync())
        End Function
	End Class
End Namespace