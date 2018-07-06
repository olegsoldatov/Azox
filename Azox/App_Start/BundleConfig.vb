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

		bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
					"~/Scripts/modernizr-*"))

		bundles.Add(New ScriptBundle("~/bundles/popper").Include(
					"~/Scripts/popper.js"))

		bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
					"~/Scripts/bootstrap.js",
					"~/Scripts/bootstrap-support-ie10-width.js",
					"~/Scripts/bootstrap-azox.js",
					"~/Scripts/respond.js"))

		bundles.Add(New ScriptBundle("~/bundles/dashboard").Include(
					"~/Scripts/bootstrap-datepicker.js",
					"~/Scripts/bootstrap-datepicker.ru.js",
					"~/Scripts/tablesorter/jquery.tablesorter.combined.js",
					"~/Scripts/dashboard.js"))

		bundles.Add(New StyleBundle("~/Content/css").Include(
					"~/Content/bootstrap.css",
					"~/Content/bootstrap-support-ie10-width.css",
					"~/Content/bootstrap-azox.css",
					"~/Content/fontawesome-all.css",
					"~/Content/animate.css",
					"~/Content/site.css"))

		bundles.Add(New StyleBundle("~/Content/dashboard").Include(
					"~/Content/bootstrap.css",
					"~/Content/bootstrap-support-ie10-width.css",
					"~/Content/datepicker.css",
					"~/Content/fontawesome-all.css",
					"~/Content/animate.css",
					"~/Content/dashboard.css"))

		bundles.Add(New StyleBundle("~/Content/signin").Include(
					"~/Content/bootstrap.css",
					"~/Content/bootstrap-support-ie10-width.css",
					"~/Content/signin.css"))

		bundles.Add(New StyleBundle("~/Content/dashboard/bar").Include(
					"~/Content/dashboard-bar.css"))
	End Sub
End Module

