@ModelType ChangeModel
<div class="modal fade" id="changeModal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Изменение брендов</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Закрыть">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="form-group row">
                    @Html.LabelFor(Function(model) model.IsPublished, New With {.class = "col-sm-3 control-label"})
                    <div class="col-sm-9">
                        @Html.DropDownListFor(Function(model) model.IsPublished, {New SelectListItem With {.Value = True, .Text = "Опубликовано"}, New SelectListItem With {.Value = False, .Text = "Черновик"}}, "", New With {.class = "form-control"})
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="submit" class="btn btn-primary" name="change" value="True">Изменить</button>
                <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">Отмена</button>
            </div>
        </div>
    </div>
</div>

