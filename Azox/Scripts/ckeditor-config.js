CKEDITOR.editorConfig = function (config) {
	config.height = '392';
	config.toolbar = [
		{ name: 'file', items: ['Save'] },
		{ name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
		{ name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
		{ name: 'including', items: ['Link', 'Unlink', 'Anchor', '-', 'Image', 'Flash', 'Table', 'HorizontalRule'] },
		{ name: 'editortools', items: ['Maximize', 'ShowBlocks'] },
		{ name: 'editorhelp', items: ['About'] },
		'/',
		{ name: 'document', items: ['Source'] },
		{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
		{ name: 'formatstyles', items: ['Format', 'Font', 'FontSize'] },
		{ name: 'textstyles', items: ['TextColor', 'BGColor'] },
		{ name: 'correction', items: ['Find', 'Replace', '-', 'Scayt'] }
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
	if (dialogName == 'link') { dialogDefinition.removeContents('advanced'); dialogDefinition.removeContents('target'); }
	if (dialogName == 'image') { dialogDefinition.removeContents('advanced'); dialogDefinition.removeContents('Link'); }
	if (dialogName == 'flash') { dialogDefinition.removeContents('advanced'); }
});