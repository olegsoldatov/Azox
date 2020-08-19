CKEDITOR.editorConfig = function (config) {
	config.height = '369';
	config.toolbar = [
		{ name: 'editable', items: ['Undo', 'Redo'] },
		{ name: 'formatstyles', items: ['Font', 'FontSize'] },
		{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Superscript', 'Subscript', 'BGColor', 'TextColor', 'RemoveFormat'] },
		{ name: 'paragraph', items: ['BulletedList', 'NumberedList', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
		{ name: 'including', items: ['Link', 'Unlink', 'Image'] },
		{ name: 'document', items: ['Source'] }
	];
	config.removePlugins = 'elementspath';
	config.entities = false;
	config.protectedSource.push(/<(style)[^>]*>.*<\/style>/ig);
	config.protectedSource.push(/<(script)[^>]*>.*<\/script>/ig);
	config.allowedContent = true;
	config.contentsCss = '/Content/bootstrap.min.css';
};

CKEDITOR.on('dialogDefinition', function (ev) {
	var dialogName = ev.data.name;
	var dialogDefinition = ev.data.definition;
	if (dialogName === 'link') { dialogDefinition.removeContents('advanced'); dialogDefinition.removeContents('target'); }
	if (dialogName === 'image') { dialogDefinition.removeContents('advanced'); dialogDefinition.removeContents('Link'); }
	if (dialogName === 'flash') { dialogDefinition.removeContents('advanced'); }
});