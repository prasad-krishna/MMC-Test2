/**
 * @license Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

//CKEDITOR.editorConfig = function( config ) {
//	// Define changes to defau<<lt configuration here. For example:
//	// config.language = 'fr';
//    // config.uiColor = '#AADC6E';
//    config.toolbar = [
//		{ name: 'document', items: ['Save', 'NewPage', 'Preview', 'Print', '-', 'Templates'] },
//		{ name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
//		{ name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
//		{ name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton'] },
//		'/',
//		{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'CopyFormatting', 'RemoveFormat'] },
//		{ name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
//		{ name: 'links', items: ['Link', 'Unlink'] },
//		{ name: 'insert', items: ['Image', 'Table', 'Smiley', 'SpecialChar'] },
//		'/',
//		{ name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
//		{ name: 'colors', items: ['TextColor', 'BGColor'] },
//		{ name: 'tools', items: ['Maximize', 'ShowBlocks'] },
//		{ name: 'about', items: ['About'] }
//    ];
//};

CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'forms', groups: ['forms'] },
		'/',
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		'/',
		{ name: 'links', groups: ['links'] },
		{ name: 'insert', groups: ['insert'] },
		'/',
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'others', groups: ['others'] },
		{ name: 'about', groups: ['about'] }
    ];

    config.removeButtons = 'Source,Iframe,PageBreak,SpecialChar,HorizontalRule,Flash,Anchor,Smiley,Table';
};