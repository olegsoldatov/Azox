Imports System.Web.Optimization

Public Module BundleConfig
	Public Sub RegisterBundles(ByVal bundles As BundleCollection)

		bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
					"~/Scripts/jquery-{version}.js"))

		bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
					"~/Scripts/jquery.validate*",
					"~/Scripts/methods_ru.js"))

		bundles.Add(New ScriptBundle("~/bundles/ajax").Include(
					"~/Scripts/jquery.unobtrusive-ajax.js"))

		bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
					"~/Scripts/popper.js",
					"~/Scripts/bootstrap.js"))

		bundles.Add(New ScriptBundle("~/bundles/scripts").Include(
					"~/Scripts/site.js"))

		bundles.Add(New ScriptBundle("~/bundles/dashboard").Include(
					"~/Scripts/bootstrap-datepicker.js",
					"~/Scripts/bootstrap-datepicker.ru.js",
					"~/Scripts/dashboard.js"))

		bundles.Add(New StyleBundle("~/Content/css").Include(
					"~/Content/bootstrap.css",
					"~/Content/font-awesome.css",
					"~/Content/animate.css",
					"~/Content/azox.css",
					"~/Content/site.css"))

		bundles.Add(New StyleBundle("~/Content/dashboard").Include(
					"~/Content/bootstrap.css",
					"~/Content/datepicker.css",
					"~/Content/font-awesome.css",
					"~/Content/animate.css",
					"~/Content/dashboard.css"))

		bundles.Add(New StyleBundle("~/Content/dashboard/bar").Include(
					"~/Content/dashboard-bar.css"))

		bundles.Add(New StyleBundle("~/Content/account").Include(
					"~/Content/bootstrap.css",
					"~/Content/account.css"))
	End Sub
End Module

