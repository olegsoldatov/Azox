@Html.TextArea("", CStr(Model), 20, 20, New With {.class = "form-control"})

<script src="//cdn.ckeditor.com/4.9.2/full/ckeditor.js"></script>
<script src="~/CKFinder/ckfinder.js"></script>

<script type="text/javascript">
    CKEDITOR.replace('Content',
    {
        customConfig: '@Url.Content("~/Scripts/ckeditor-config.js")',
        filebrowserBrowseUrl: '@Url.Content("~/CKFinder/ckfinder.html")',
        filebrowserImageBrowseUrl: '@Url.Content("~/CKFinder/ckfinder.html?type=Images")',
        filebrowserFlashBrowseUrl: '@Url.Content("~/CKFinder/ckfinder.html?type=Flash")',
        filebrowserUploadUrl: '@Url.Content("~/CKFinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files")',
        filebrowserImageUploadUrl: '@Url.Content("~/CKFinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images")',
        filebrowserFlashUploadUrl: '@Url.Content("~/CKFinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash")',
        filebrowserWindowWidth: '940',
        filebrowserWindowHeight: '700'
    });
</script>
