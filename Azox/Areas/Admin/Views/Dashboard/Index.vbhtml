@Code
	ViewBag.Title = "Панель управления"
End Code

@Section Toolbar
	<a href="@Url.Action("ClearCache")" class="btn" title="@String.Format("Доступно памяти: {0} МБ ({1}%)", Cache.EffectivePrivateBytesLimit / 1048576, Cache.EffectivePercentagePhysicalMemoryLimit)">
		<span class="fa fa-microchip"></span>
		<span>Очистить кэш</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>
