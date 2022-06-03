@Code
    ViewBag.Title = "404 — Страница не найдена"
End Code

<div class="container pt-5">
    <div class="alert alert-danger" role="alert">
        <h4 class="alert-heading">@ViewBag.Title</h4>
        <p>@ViewBag.Message</p>
    </div>
</div>
