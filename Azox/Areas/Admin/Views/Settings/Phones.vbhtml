@ModelType IEnumerable(Of PhoneEditViewModel)

@Section Toolbar
    <button class="btn" form="model-form">
        <span class="fa fa-save"></span>
        <span>Сохранить</span>
    </button>
End Section

<header>
    <h1>@ViewBag.Title</h1>
</header>

<article>
    @Using Html.BeginForm(Nothing, Nothing, FormMethod.Post, New With {.id = "model-form"})
        @Html.AntiForgeryToken
        @<div class="row">
            <div class="col-lg-4">
                @For Each item In Model
                    @<div class="form-group">
                        <div class="input-group">
                            <input type="tel" name="Phone" class="form-control" value="@item.Phone" placeholder="Номер телефона" />
                            <input type="text" name="Ext" class="form-control" value="@item.Ext" placeholder="Добавочный" />
                            <input type="text" name="Description" class="form-control" value="@item.Description" placeholder="Описание" />
                        </div>
                    </div>
                Next
                <div class="form-group">
                    <div class="input-group">
                        <input type="tel" name="Phone" class="form-control" placeholder="Номер телефона" />
                        <input type="text" name="Ext" class="form-control" placeholder="Добавочный" />
                        <input type="text" name="Description" class="form-control" placeholder="Описание" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <input type="tel" name="Phone" class="form-control" placeholder="Номер телефона" />
                        <input type="text" name="Ext" class="form-control" placeholder="Добавочный" />
                        <input type="text" name="Description" class="form-control" placeholder="Описание" />
                    </div>
                </div>
                <button type="button" class="btn btn-outline-secondary">Добавить</button>
            </div>
        </div>
    End Using
</article>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section

