@Imports Microsoft.AspNet.Identity
@Code
	Dim area = Request.RequestContext.RouteData.Values("area")
	Dim controllerName = Request.RequestContext.RouteData.Values("controller")
	Dim actionName = Request.RequestContext.RouteData.Values("action")
End Code
<!DOCTYPE html>
<html lang="ru">
<head>
	<title>@ViewBag.Title</title>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<meta name="description" content="Панель управления" />
	<meta name="format-detection" content="telephone=no" />
	<link href="~/favicon.png" rel="icon" type="image/png" />
	@Styles.Render("~/Content/dashboard")
	@RenderSection("Head", required:=False)
</head>
<body>
    <div class="layout">
        <div class="sidebar" id="sidebar">
            <button class="sidebar-toggle" data-toggle="collapse" aria-expanded="false" data-target="#sidebar">
                <span class="fa fa-bars"></span>
            </button>
            <ul class="nav flex-column">
                <li @If actionName.Equals("index") And controllerName.Equals("dashboard") Then @<text> class="active" </text> End If>
                    <a href="@Url.Action("index", "dashboard")">
                        <span class="fa fa-tachometer"></span>
                        <span>Панель управления</span>
                    </a>
                </li>
                <li @If controllerName.Equals("products") Or controllerName.Equals("categories") Then @<text> class="active" </text> End If>
                    <button aria-controls="productsMenu" aria-expanded="@If controllerName.Equals("products") Or controllerName.Equals("categories") Then@<text>true</text>Else@<text>false</text>End if">
                        <span class="fa fa-folder-o"></span>
                        <span>Каталог</span>
                    </button>
                    <ul id="productsMenu" aria-hidden="@If controllerName.Equals("products") Or controllerName.Equals("categories") Then@<text>false</text>Else@<text>true</text>End if">
                        <li @If controllerName.Equals("products") Then @<text> class="active" </text> End If>
                            <a href="@Url.Action("index", "products")">Товары</a>
                        </li>
                        <li @If controllerName.Equals("categories") Then @<text> class="active" </text> End If>
                            <a href="@Url.Action("index", "categories")">Категории</a>
                        </li>
                    </ul>
                </li>
                <li @If controllerName.Equals("brands") Then @<text> class="active" </text> End If>
                    <a href="@Url.Action("index", "brands")">
                        <span class="fa fa-folder-o"></span>
                        <span>Бренды</span>
                    </a>
                </li>
                <li @If controllerName.Equals("warehouses") Then @<text> class="active" </text> End If>
                    <a href="@Url.Action("index", "warehouses")">
                        <span class="fa fa-folder-o"></span>
                        <span>Магазины / Склады</span>
                    </a>
                </li>
                <li @If controllerName.Equals("pages") Or (actionName.Equals("files") And controllerName.Equals("dashboard")) Then @<text> class="active" </text> End If>
                    <button aria-controls="contentMenu" aria-expanded="@If controllerName.Equals("pages") Or (actionName.Equals("files") And controllerName.Equals("dashboard")) Then@<text>true</text>Else@<text>false</text>End if">
                        <span class="fa fa-folder-o"></span>
                        <span>Содержание</span>
                    </button>
                    <ul id="contentMenu" aria-hidden="@If controllerName.Equals("pages") Or (actionName.Equals("files") And controllerName.Equals("dashboard")) Then@<text>false</text>Else@<text>true</text>End if">
                        <li @If controllerName.Equals("pages") Then @<text> class="active" </text> End If>
                            <a href="@Url.Action("index", "pages")">Страницы</a>
                        </li>
                        <li @If actionName.Equals("files") And controllerName.Equals("dashboard") Then @<text> class="active" </text> End If>
                            <a href="@Url.Action("files", "dashboard")">Файлы</a>
                        </li>
                    </ul>
                </li>
                <li @If controllerName.Equals("customers") Then @<text> class="active" </text> End If>
                    <a href="@Url.Action("index", "customers")">
                        <span class="fa fa-folder-o"></span>
                        <span>Клиенты <sup>&beta;</sup></span>
                    </a>
                </li>
                <li @If controllerName.Equals("settings") Then @<text> class="active" </text> End If>
                    <button aria-controls="settingMenu" aria-expanded="@If controllerName.Equals("settings") Then@<text>true</text>Else@<text>false</text>End if">
                        <span class="fa fa-gear"></span>
                        <span>Настройки</span>
                    </button>
                    <ul id="settingMenu" aria-hidden="@If controllerName.Equals("settings") Then@<text>false</text>Else@<text>true</text>End if">
                        <li @If actionName.Equals("site") And controllerName.Equals("settings") Then @<text> class="active" </text> End If>
                            <a href="@Url.Action("site", "settings")">Сайт</a>
                        </li>
                    </ul>
                </li>
                <li title="@String.Format("Доступно памяти: {0} МБ ({1}%)", Cache.EffectivePrivateBytesLimit / 1048576, Cache.EffectivePercentagePhysicalMemoryLimit)">
                    <a href="@Url.Action("uploadCache", "dashboard")">
                        <span class="fa fa-microchip"></span>
                        <span>Обновить кэш</span>
                    </a>
                </li>
            </ul>
        </div>

        <main class="main" id="main">
            <div class="toolbar" id="toolbar">
                <div>
                    @RenderSection("Toolbar", required:=False)
                </div>

                <div>
                    <a href="~/" class="btn" title="Переход на сайт" target="_blank">
                        <span class="fa fa-globe"></span>
                        <span>Сайт</span>
                    </a>
                    <a href="~/manage" class="btn" title="Управление учетной записью">
                        <span class="fa fa-user"></span>
                        <span>@User.Identity.Name</span>
                    </a>
                    @Using Html.BeginForm("logoff", "account", New With {.area = ""}, FormMethod.Post, New With {.id = "logoutForm"})
                        @Html.AntiForgeryToken()
                        @<button class="btn" title="Выход"><span class="fa fa-sign-out"></span></button>
                    End Using
                </div>
            </div>

            <section class="content" id="content">
                <div style="position: absolute; right: 24px; z-index: 100;">
                    <div class="toast fade hide bg-success" id="toast-message" role="alert" aria-live="assertive" aria-atomic="true" data-delay="10000">
                        <div class="toast-header">
                            <span class="fa fa-info-circle text-success mr-2"></span>
                            <strong class="text-success mr-auto">Сообщение</strong>
                            <small class="ml-5">@Now.ToShortTimeString</small>
                            <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Закрыть">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="toast-body text-white">
                            @TempData("Message")
                        </div>
                    </div>

                    <div class="toast fade hide bg-warning" id="toast-warning" role="alert" aria-live="assertive" aria-atomic="true" data-delay="10000">
                        <div class="toast-header">
                            <span class="fa fa-info-circle text-warning mr-2"></span>
                            <strong class="text-warning mr-auto">Предупреждение</strong>
                            <small class="ml-5">@Now.ToShortTimeString</small>
                            <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Закрыть">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="toast-body text-white">
                            @TempData("Warning")
                        </div>
                    </div>

                    <div class="toast fade hide bg-danger" id="toast-error" role="alert" aria-live="assertive" aria-atomic="true" data-delay="10000">
                        <div class="toast-header">
                            <span class="fa fa-info-circle text-danger mr-2"></span>
                            <strong class="text-danger mr-auto">Ошибка</strong>
                            <small class="ml-5">@Now.ToShortTimeString</small>
                            <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Закрыть">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="toast-body text-white">
                            @TempData("Error")
                        </div>
                    </div>
                </div>

                @RenderBody()
            </section>
        </main>
    </div>

    <a href="#" class="back-to-top" aria-disabled="true" data-toggle="toTop" title="Наверх">
        <div class="fa fa-arrow-up" aria-label="Наверх"></div>
    </a>

    @RenderSection("Modals", required:=False)
    @Scripts.Render("~/bundles/jquery", "~/Scripts/bootstrap.bundle.js")
    @Scripts.Render("~/bundles/dashboard")
    @RenderSection("Scripts", required:=False)
    @If Not IsNothing(TempData("Message")) Then
        @<script>
             $("#toast-message").toast("show");
        </script>
    ElseIf Not IsNothing(TempData("Warning")) Then
        @<script>
             $("#toast-warning").toast("show");
        </script>
    ElseIf Not IsNothing(TempData("Error")) Then
        @<script>
             $("#toast-error").toast("show");
        </script>
    End If
</body>
</html>
<!-- Софт Бизнес https://soft.business -->
