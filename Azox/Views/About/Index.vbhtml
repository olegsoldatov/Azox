﻿@ModelType IPage
@Code
    ViewBag.Title = Model.Title
    ViewBag.Description = Model.Description
    ViewBag.EditUrl = Url.Action("edit", "pages", New With {.area = "admin", Model.Id})
End Code

<div class="container">
    <article>
        <h1>@ViewBag.Title</h1>
        <p class="lead">
            @Model.Description
        </p>
        @Html.Raw(Model.Content)
        <p>
            Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi.
        </p>
    </article>
</div>
