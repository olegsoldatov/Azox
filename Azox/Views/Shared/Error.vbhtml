@ModelType HandleErrorInfo
@Code
    ViewBag.Title = "500 — Внутренняя ошибка сервера"
End Code

<div class="container pt-5">
    <div class="alert alert-danger" role="alert">
        <h4 class="alert-heading">@ViewBag.Title</h4>
        <p>Произошла ошибка при обработке запроса.</p>
        <hr />
        <p class="mb-0">
            <small>
                Попробуйте зайти на эту страницу позднее. Если ошибка повториться, то обратитесь к администратору сайта.
            </small>
        </p>
    </div>
</div>