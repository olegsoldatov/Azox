@Code
	Dim area = Request.RequestContext.RouteData.Values("area")
	Dim controller = Request.RequestContext.RouteData.Values("controller")
	Dim action = Request.RequestContext.RouteData.Values("action")
End Code
<ul class="nav flex-column">
	<li @If controller.Equals("Dashboard") Then @<text> class="active" </text> End If>
		<a href="@Url.Action("Index", "Dashboard")"><span class="fas fa-tachometer-alt"></span>Панель управления</a>
	</li>
	<li @If controller.Equals("Products") Then @<text> class="active" </text> End If>
		<a href="@Url.Action("Index", "Products")"><span class="fas fa-folder"></span>Продукция</a>
	</li>
	<li @If controller.Equals("Services") Then @<text> class="active" </text> End If>
		<a href="@Url.Action("Index", "Services")"><span class="fas fa-folder"></span>Услуги</a>
	</li>
	<li @If controller.Equals("Articles") Then @<text> class="active" </text> End If>
		<a href="@Url.Action("Index", "Articles")"><span class="fas fa-folder"></span>Статьи</a>
	</li>
	<li @If controller.Equals("Files") Then @<text> class="active" </text> End If>
		<a href="@Url.Action("Index", "Files")"><span class="fas fa-folder"></span>Файлы</a>
	</li>
	<li @If controller.Equals("Pages") Then @<text> class="active" </text> End If>
		<a href="@Url.Action("Index", "Pages")"><span class="fas fa-folder"></span>Страницы</a>
	</li>
</ul>

