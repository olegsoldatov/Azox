@ModelType ProductFilter
@Using Html.BeginForm(Nothing, Nothing, FormMethod.Get, New With {.data_toggle = "filter"})
    @<div class="form-group">
        <div class="input-group">
            @Html.TextBoxFor(Function(model) model.SearchText, New With {.class = "form-control form-control-sm", .placeholder = "Поиск"})
            <div class="input-group-append">
                <button class="btn btn-outline-secondary btn-sm" type="submit" title="Искать"><span class="fa fa-search"></span></button>
                <button class="btn btn-outline-secondary btn-sm" type="button" data-toggle="modal" data-target="#filterModal" title="Расширенный фильтр"><span class="fa fa-sliders"></span></button>
                <a href="@Request.Url.AbsolutePath" class="btn btn-outline-secondary btn-sm" title="Сбросить"><span class="fa fa-times"></span></a>
            </div>
        </div>
    </div>

    @<div class="modal fade" id="filterModal" tabindex="-1" role="dialog" aria-labelledby="filterModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="filterModalLabel">Расширенный фильтр</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Закрыть"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.BrandId, New With {.class = "form-label"})
                        @Html.DropDownListFor(Function(model) model.BrandId, Nothing, "Все", New With {.class = "custom-select"})
                    </div>
                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.CategoryId, New With {.class = "form-label"})
                        @Html.DropDownListFor(Function(model) model.CategoryId, Nothing, "Все", New With {.class = "custom-select"})
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-primary">Применить</button>
                    <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">Отменить</button>
                </div>
            </div>
        </div>
    </div>
End Using

