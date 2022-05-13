/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For complete reference see:
	// https://ckeditor.com/docs/ckeditor4/latest/api/CKEDITOR_config.html

	// The toolbar groups arrangement, optimized for two toolbar rows.
	config.toolbarGroups = [
		{ name: 'clipboard',   groups: [ 'clipboard', 'undo' ] },
		{ name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ] },
		{ name: 'links' },
		{ name: 'insert' },
		{ name: 'forms' },
		{ name: 'tools' },
		{ name: 'document',	   groups: [ 'mode', 'document', 'doctools' ] },
		{ name: 'others' },
		'/',
		{ name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
		{ name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align', 'bidi' ] },
		{ name: 'styles' },
		{ name: 'colors' },
		{ name: 'about' }
	];

	// Remove some buttons provided by the standard plugins, which are
	// not needed in the Standard(s) toolbar.
	config.removeButtons = 'Underline,Subscript,Superscript';

	// Set the most common block elements.
	config.format_tags = 'p;h1;h2;h3;pre';

	// Simplify the dialog windows.
	config.removeDialogTabs = 'image:advanced;link:advanced';

	config.filebrowserImageUploadUrl = '/UploadFile.ashx';
	config.filebrowserUploadMethod = 'form';
	config.wordcount = {

	    // Whether or not you want to show the Paragraphs Count
	    showParagraphs: false,

	    // Whether or not you want to show the Word Count
	    showWordCount: false,

	    // Whether or not you want to show the Char Count
	    showCharCount: true,

	    // Whether or not you want to count Spaces as Chars
	    countSpacesAsChars: true,

	    // Whether or not to include Html chars in the Char Count
	    countHTML: false,

	    // Maximum allowed Word Count, -1 is default for unlimited
	    maxWordCount: -1,

	    // Maximum allowed Char Count, -1 is default for unlimited
	    maxCharCount: 7000,

	};
};
CKEDITOR.on('dialogDefinition', function (ev) {
    // Take the dialog name and its definition from the event data
    var dialogName = ev.data.name;
    var dialogDefinition = ev.data.definition;
    var iframe = document.getElementById(this._.frameId);
    var iframeWindow = '';
    var editor = ev.editor;
    if (dialogName == 'image') {
        var infoTab = dialogDefinition.getContents('info');
        console.log(infoTab);
        txtWidth = infoTab.get('txtWidth');
        txtHeight = infoTab.get('txtHeight');
        txtAlt = infoTab.get('txtAlt');
        txtBorder = infoTab.get('txtBorder');
        txtHSpace = infoTab.get('txtHSpace');
        txtVSpace = infoTab.get('txtVSpace');
        cmbAlign = infoTab.get('cmbAlign');
        var sAlt = '';
        var sWidth = '';
        var sHeight = '';
        var sBorder = '';
        var sHSpace = '';
        var sVSpace = '';
        var sCmbAlign = '';

        txtWidth.onChange = function (e) { sWidth = e.data.value; }
        txtHeight.onChange = function (e) { sHeight = e.data.value; }
        txtAlt.onChange = function (e) { sAlt = e.data.value; }
        txtBorder.onChange = function (e) { sBorder = e.data.value; }
        txtHSpace.onChange = function (e) { sHSpace = e.data.value; }
        txtVSpace.onChange = function (e) { sVSpace = e.data.value; }
        cmbAlign.onChange = function (e) { sCmbAlign = e.data.value; }

        dialogDefinition.onOk = function (e) {

            var imageSrcUrl = e.sender.originalElement.$.src;
            var vArrCheck = ['.JPEG', '.TIFF', '.PNG', '.GIF', '.JPG', '.BMP'];
            if (imageSrcUrl != '') {
                var vDinhDang = '';
                var vFrom = imageSrcUrl.lastIndexOf('.')
                var vFrom_ = -1;
                var vCheck = false;
                if (vFrom != -1) {
                    for (var i = 0; i < 6; i++) {
                        vFrom_ = imageSrcUrl.toUpperCase().lastIndexOf(vArrCheck[i]);
                        if (vFrom_ === vFrom) {
                            vCheck = true;
                        }
                    }
                    if (vCheck == false) {
                        alert_error('Định dạng ảnh không hợp lệ. File hợp lệ có đuôi: .JPEG, .TIFF, .PNG, .GIF, .JPG, .BMP');
                        return false;
                    }
                    else {
                        var width = e.sender.originalElement.$.width;
                        var height = e.sender.originalElement.$.height;

                        var imgHtml = CKEDITOR.dom.element.createFromHtml('<img border="' + sBorder +
                          '" hspace="' + sHSpace +
                          '" vspace="' + sVSpace +
                          '" align="' + sCmbAlign +
                          '" src="' + imageSrcUrl +
                          '" width="' + sWidth +
                          '" height="' + sHeight +
                          '" />');
                        editor.insertElement(imgHtml);
                    }
                }
            }

        };
    }
});