//Model For Confirmation Popup
function showConfirmation(confirmationTitle, confirmationMsg, okMethod, cancelMethod, okMethodParam, cancelMethodParam, isSmallBox, istopHeight, okButtonText, cancelButtonText, isButtonleftalign) {
    $('.custom-modal').remove();
    var closeButton = '<button type="button" class="close btnConfirmationClose" data-dismiss="modal" aria-label="Close">' +
                                        '<span aria-hidden="true">&times;</span>' +
                                    '</button>';
    var closeButtonHtml = (okButtonText != undefined && okButtonText.length > 0 && cancelButtonText != undefined && cancelButtonText.length > 0) ? closeButton : "";
    var modalBase = '<div class="custom-modal custmodal modal fade ConfirmationDialogLength modal-confirmation" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard=false style="z-index:999999;">' +
                        '<div class="modal-dialog ' + (isSmallBox == 1 ? 'modal-sm' : '') + '"' + (istopHeight == 1 ? 'Style="top:7%"' : '') + '><div class="modal-content">' +
                            '<div class="modal-header content-header pl-5">' + closeButtonHtml +
                                '<h5 class="modal-title">' + confirmationTitle + '</h5>' +
                            '</div>' +
                                '<div class="modal-body p-10"><div style="max-height:400px;overflow:auto;" class="dvmsg">' + confirmationMsg + '</div>' +
                                //'<hr class="soften my-5"><div class="' + (isButtonleftalign ? "" : "fr") + '">' +

                                '</div>' +
                                '<div class="modal-footer px-10 py-5">' +
                                    '<div class="text-left">' +
                                    '<button id="btnYes" class="btn btn-sm btn-dark btn-ok" style="margin-right: 5px !important;width: auto;">' + ((okButtonText != "" && okButtonText != undefined) ? okButtonText : "Yes") + '</button>' +
                                    '<button id="btnNo" class="btn btn-sm btn-gray btn-cancel" style="width: auto;">' + ((cancelButtonText != "" && cancelButtonText != undefined) ? cancelButtonText : "No") + '</button>' +
                                    '</div>' +
                                '</div>' +
                                '</div>' +
                            '</div>' +
                         '</div>' +
                    '</div>';
    var customModal = $(modalBase);
    $('body').append(customModal);
    $('.custom-modal .btn-ok').bind('click', function () {
        $('.custom-modal').modal('hide');
        if (okMethodParam != undefined && okMethodParam != '')
            okMethod(okMethodParam);
        else
            okMethod();
    });
    $('.custom-modal .btn-cancel').bind('click', function () {
        $('.custom-modal').modal('hide');
        if (cancelMethodParam != undefined && cancelMethodParam != "")
            cancelMethod(cancelMethodParam)
        else if (cancelMethod)
            cancelMethod();
    });
    $('.custom-modal').modal({
        //backdrop: false
        //keyboard: false
    }).modal('show');

    $('.custom-modal').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
        $(this).remove();
        //if (window.location.href.toLowerCase().indexOf("patient/demographics_config") >= 0) {
        //    if ($(".modal.fade.in").length > 0) {
        //        $("body").addClass("modal-open");
        //    }
        //    else {
        //        $("body").removeClass("modal-open");
        //    }
        //}
    });
}

function blockPage() {
    $.blockUI({
        message: '<div style="padding: 0px; background: transparent;"><i class="fa fa-spinner fa-pulse fa-3x" ></i></div>', //'<div style="color:#337ab7;"><div><img src="../../Images/loader.gif"/></div><strong>Loading...</strong></div>',
        css: {
            border: 'none',
            width: '100px',
            left: '36%',
            color: '#337ab7',
            background: 'transparent'
        },
        baseZ: 999999,
        overlayCSS: {
        }
    });
}
function unblockPage() {
    $.unblockUI();
}