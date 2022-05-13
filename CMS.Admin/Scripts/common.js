Common = function () {
    // this.AddCustomValidation();
    return this.Init();
};
/* Get the element you want displayed in fullscreen */
var elem = document.documentElement;
Common.prototype = {
    Init: function () {
        $(document).ready(function () {
            //of string concatenation
            String.prototype.format = function () {
                var st = this;
                for (i = 0; i < arguments.length; i++) {
                    var value = arguments[i];
                    var re = new RegExp("\{[" + i + "]\}", "g");
                    st = st.replace(re, value);
                }
                return st;
            };
            $.fn.datepicker.dates['vn'] = {
                days: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"],
                daysShort: ["CN", "T2", "T3", "T4", "T5", "T6", "T7", "CN"],
                daysMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7", "CN"],
                months: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"],
                monthsShort: ["Th1", "Th2", "Th3", "Th4", "Th5", "Th6", "Th7", "Th8", "Th9", "Th10", "Th11", "Th12"],
                today: "Hôm nay"
            };
            $(".datepicker-share").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn'
            });

            $(".datepicker-share-ttcn").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn',
                endDate: '+0d'
            });
            $(".datepicker-share-ngaysinh").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn',
                endDate: '-18y'
            });
            // Page full screen
            $('.nav-link-expand').on('click', function (e) {
                if ($('.nav-link-expand').find('.fa-compress').length > 0) {
                    Common.CloseFullscreen();
                } else {
                    Common.OpenFullscreen();
                }
                $('.nav-link-expand').find('i').toggleClass('fa-expand fa-compress');
            });

            //menu
            $(".nav-menu-main").click(function () {
                $("#main-menu-navigation").find("li.nav-item").toggleClass('menu-collapsed');
            });

            //menu mobile
            $('#trigger-mobile').click(function () {
                $("#navbarSupportedContent").toggleClass('show');
            });

            //card heading actions buttons small screen support
            $(".heading-elements-toggle").on("click", function () {
                $(this).parent().children(".heading-elements").toggleClass("visible");
            });

            //datepicker
            $(".datepicker").datepicker({
                format: 'dd/mm/yyyy'
            });

            // Collapsible Card
            $('a[data-action="collapse"]').on('click', function (e) {
                e.preventDefault();
                $(this).closest('.card').children('.card-content').collapse('toggle');
                $(this).closest('.card').find('[data-action="collapse"] i').toggleClass('fa-minus fa-plus');
            });

            // Toggle fullscreen
            $('a[data-action="expand"]').on('click', function (e) {
                e.preventDefault();
                $(this).closest('.card').find('[data-action="expand"] i').toggleClass('fa-expand fa-compress');
                $(this).closest('.card').toggleClass('card-fullscreen');
            });

            $('.sidebar-toggle').on('click', function () {
                let lst = document.getElementsByClassName("main-sidebar");
                if (lst && lst.length > 0) {
                    let sidebar = lst[0];
                    sidebar.removeEventListener('mouseover', Common.MenuBarHover);
                    sidebar.removeEventListener('mouseout', Common.MenuBarHover);
                    if (!$('body').hasClass("sidebar-collapse")) {
                        sidebar.addEventListener('mouseover', Common.MenuBarHover);
                        sidebar.addEventListener('mouseout', Common.MenuBarHover);
                    }
                    $("body").removeClass("sidebar-expanded-on-hover");
                    $('body').toggleClass('sidebar-collapse');
                }
                //remove

            });
            $('.nav-link').unbind("click").click(function () {
                $('.tab-pane').removeClass('default');
                $('.nav-link').unbind("click");
            });
            //select 2
            $.fn.select2.defaults.set("theme", "bootstrap");
            $(".select2-single").select2({
                width: null,
                containerCssClass: ':all:'
            });

            $("#selectWebsite").unbind("change").change(function () {
                $("#selectNgonNgu").change();
            });
            var currentWebsiteSelect = $("#selectNgonNgu option:selected").val();
            var websiteID = Common.getCookie("WebsiteID");
            var ngonNguID = Common.getCookie("NgonNguID");
            if (!Common.Empty($("#selectNgonNgu")) && !Common.Empty(ngonNguID)) {
                $("#selectNgonNgu").find("option[value='" + ngonNguID + "']").prop("selected", true);
                $("#selectWebsite").find("option[value='" + websiteID + "']").prop("selected", true);
            }
            $("#selectNgonNgu").unbind("change").change(function () {
                var id = $(this).val();
                Common.ShowAlert("Thông báo", "Chuyển ngôn ngữ có thể bị mất những dữ liệu mới vừa nhập, bạn có muốn tiếp tục không?", {
                    Items: {
                        No: {
                            Name: "Close",
                            Class: "btn-secondary",
                            Data: $(this),
                            OnClick: function (target) {
                                Common.HideAlert();
                                $("#selectNgonNgu").find("option[value='" + currentWebsiteSelect + "']").prop("selected", true);
                            },
                            Value: "Đóng"
                        },
                        Yes: {
                            Name: "Continue",
                            Data: $(this),
                            Class: "btn-info ",
                            OnClick: function (target) {
                                Common.HideAlert();
                                Common.Ajax({
                                    type: "POST",
                                    url: urlChangeWebsite,
                                    async: false,
                                    cache: false,
                                    data: {
                                        websiteId: $("#selectWebsite option:selected").val(),
                                        ngonNguId: id
                                    },
                                    dataType: 'json',
                                }, function (data) {
                                    currentWebsiteSelect = id;
                                    window.location = "";
                                });
                            },
                            Value: "Tiếp tục"
                        }
                    }
                }, "Continue");
                return false;
            });
            $(".remove-cache").unbind("click").click(function () {
                Common.Ajax({
                    type: "POST",
                    url: urlRemoveCache,
                    async: false,
                    cache: false,
                    data: {
                        key: ""
                    },
                    dataType: 'json'
                }, function (data) {
                    if (data.status == true) {
                        $.notify({
                            // options
                            message: 'Xóa cache thành công.!!!'
                        }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'success'
                            });
                        window.location = "";
                    }
                });
            });
            //Fix thead
            var $th = $('.scrollbar').find('thead tr')
            $('.scrollbar').on('scroll', function () {
                $th.css('transform', 'translateY(' + this.scrollTop + 'px)');
            });
        });
    },
    CheckLengInput: function (that, lens) {
        let jThis = $(that);
        let val = jThis.val();
        let resultLen = lens - val.length - 1;
        if ((lens - resultLen) == 1) {
            resultLen = resultLen + 1;
        }
        jThis.closest(".form-group").find(".lenDanger").html(resultLen);
        if (!Common.Empty(val) && val.length >= lens) {
            $(that).addClass("border-danger");
            jThis.closest(".form-group").find(".lenDanger").addClass("text-danger");
            jThis.closest(".form-group").find("label").addClass("text-danger");
        }
        else {
            $(that).removeClass("border-danger");
            jThis.closest(".form-group").find(".lenDanger").removeClass("text-danger");
            jThis.closest(".form-group").find("label").removeClass("text-danger");
        }
        return false;
    },
    MenuBarHover: function () {
        $('body').toggleClass('sidebar-expanded-on-hover');
        $('body').toggleClass('sidebar-collapse');
    },
    InitializeForm: function (form) {
        var that = this;

        if ($(form).data("user-guide") == true) {
            $(form).find("input,select").attr("title", "Sử dụng chuột phải để xóa");
            $(form).find(".action-popup .glyphicon-plus-sign").attr("title", "Thêm");
            $(form).find(".action-popup .glyphicon-minus-sign").attr("title", "Xóa những trường đã chọn");
        }
        $(form).find("input,select").bind("contextmenu", function (e) {
            $(this).val("");
            return false;
        });
        $(form).find("input[data-val-date]").datepicker({
            dateFormat: $.datepicker._defaults.dateFormat,
            onSelect: function () {
                var validator = $(this).closest("form").validate();
                validator.element($(this));
            }
        });
        $(form).find("input[data-val-date]").attr("readonly");
        $(form).find("select[data-popup]").each(function () {
            $(this).parent().find(".action-popup .glyphicon").data("element", this);
            $(this).parent().find(".action-popup .glyphicon-plus-sign").unbind("click").click(function () {
                var element = $(this).data("element");
                if (!Common.empty($(element).attr("disabled"))) {
                    return;
                }

                var action = $(element).data("popup");
                Common.ShowPopup({
                    Url: that.Url.PopupSearchDialog + "/" + action,
                    CallBack: function (response) {
                        var option = "<option value='#Id' selected>#Name</option>";
                        var html = [];
                        for (var index in response) {
                            var model = response[index];
                            if ($(element).find("option[value='" + model.Id + "']").length == 0) {
                                html.push(Common.BindHtml(option, model));
                            }
                        }
                        $(element).html($(element).html() + html.join(""));
                        $(element).change();
                    }
                });
            });
            $(this).parent().find(".action-popup .glyphicon-minus-sign").unbind("click").click(function () {
                var element = $(this).data("element");
                if (!Common.empty($(element).attr("disabled"))) {
                    return;
                }
                var selectedValues = $(element).find("option:selected");
                selectedValues.remove();
                $(element).change();
            });
        });
        this.RebindValidate(form);
    },
    ValidatorForm : (form) => {
        document.querySelectorAll(form + ' .field-validation-error').forEach(function (item, index) {
            item.parentNode.removeChild(item);
        });
        //input required
        document.querySelectorAll(form + ' input[type="text"][data-rule-required],' + form + ' textarea[data-rule-required]').forEach(function (item, index) {
            if (Common.Empty(item.value) && Common.Empty(item.hasAttribute('disabled'))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-required') + '</div>';
                if (!item.parentNode.classList.contains('input-group'))
                    item.parentNode.insertAdjacentHTML('beforeend', newChild);
                else {
                    item.parentNode.parentNode.insertAdjacentHTML('beforeend', newChild);
                }
            }
        });
        //select required
        document.querySelectorAll(form + ' select[data-rule-required]:not([multiple])').forEach(function (item, index) {
            if (Common.Empty(item.hasAttribute('disabled'))) {
                var selected = item.querySelectorAll('option:checked');
                if (selected.length == 0 || Common.Empty(selected[0].value)) {
                    var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-required') + '</div>';
                    item.parentNode.insertAdjacentHTML('beforeend', newChild);
                }
            }
        });
        //check ma javascript
        document.querySelectorAll(form + ' input[type="text"][data-rule-javascript],' + form + ' textarea[data-rule-javascript]')
            .forEach(function (item, index) {
                if (!Common.Empty(item.value)) {
                    let checkTemp = Common.RemoveScript(item.value);
                    if (checkTemp === '') {
                        var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-javascript') + '</div>';
                        if (!item.parentNode.classList.contains('input-group'))
                            item.parentNode.insertAdjacentHTML('beforeend', newChild);
                        else {
                            item.parentNode.parentNode.insertAdjacentHTML('beforeend', newChild);
                        }
                    }

                }
            });
        //check ma html
        document.querySelectorAll(form + ' input[type="text"][data-rule-maHtml],' + form + ' textarea[data-rule-maHtml]')
            .forEach(function (item, index) {
                if (!Common.Empty(item.value)) {
                    if (/<[a-z][\s\S]*>/i.test(item.value) || (/<[/s/S][a-z][\s\S]*>/i.test(item.value))) {
                        var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-maHtml') + '</div>';
                        if (!item.parentNode.classList.contains('input-group'))
                            item.parentNode.insertAdjacentHTML('beforeend', newChild);
                        else {
                            item.parentNode.parentNode.insertAdjacentHTML('beforeend', newChild);
                        }
                    }
                }
            });
        //input maxlenght :
        document.querySelectorAll(form + ' input[type="text"][data-rule-maxlength],' + form + ' textarea[data-rule-maxlength]')
            .forEach(function (item, index) {
                if (!Common.Empty(item.value)) {
                    var max = item.getAttribute('data-value-maxlength');
                    if (item.value.length > max) {
                        var newChild = '<div class="field-validation-error invalid-feedback show" style="display: block;">' + item.getAttribute('data-msg-maxlength') + '</div>';
                        if (!item.parentNode.classList.contains('input-group'))
                            item.parentNode.insertAdjacentHTML('beforeend', newChild);
                        else {
                            item.parentNode.parentNode.insertAdjacentHTML('beforeend', newChild);
                        }
                    }
                }
            });
        document.querySelectorAll(form + ' input[type="text"][data-rule-date-required],' + form + ' textarea[data-rule-date-required]').forEach(function (item, index) {
            if (Common.Empty(item.value) && Common.Empty(item.hasAttribute('disabled'))) {
                var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;">' + item.getAttribute('data-msg-date-required') + '</div>';
                item.parentNode.parentNode.insertAdjacentHTML('beforeend', newChild);
            }
        });
        // check cmnd/ho chieu
        document.querySelectorAll(form + ' input[type="text"][data-rule-cmnd]')
            .forEach(function (item, index) {
                if (!Common.Empty(item.value)) {
                    if (/[0-9]/i.test(item.value) == false) {
                        var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;"> CMND/Hộ chiếu không đúng định dạng</div>';
                        item.parentNode.insertAdjacentHTML('beforeend', newChild);
                    }
                    else if (item.value.length != 9 && item.value.length != 12) {
                        var newChild = '<div class="field-validation-error alert alert-danger fade show" role="alert" style="display: block;"> CMND/Hộ chiếu không đúng định dạng</div>';
                        item.parentNode.insertAdjacentHTML('beforeend', newChild);
                    }
                }
            });
        document.querySelectorAll(form + ' input[type="text"][data-rule-email]')
            .forEach(function (item, index) {
                if (!Common.Empty(item.value))
                    if (!validator.isEmail(item.value)) {
                        var newChild = '<div class="field-validation-error invalid-feedback show" style="display: block;">Email không đúng định dạng</div>';
                        item.parentNode.insertAdjacentHTML('beforeend', newChild);
                    }
            });
    },
    _validateRegex:  (_id, _re, _format) =>{
        document.getElementById(_id).parentNode.querySelectorAll('.field-validation-error.validation-regex').forEach(function (item, index) {
            item.parentNode.removeChild(item);
        });
        if (!document.getElementById(_id).value.match(_re)
            //&& !re.test(document.getElementById(id).value)
        ) {
            let text = document.querySelector('label[for="' + _id + '"]') ? document.querySelector('label[for="' + _id + '"]').firstChild.nodeValue : "";
            let newChild = '<div class="field-validation-error validation-regex invalid-feedback show" style="display: block;">' + text + ' không đúng định dạng' + '</div>';
            document.getElementById(_id).parentNode.insertAdjacentHTML('beforeend', newChild);
        }
    },
    RebindValidate: function (form) {
        var $form = $(form);
        if ($form.length > 0) {
            $form.unbind();
            $form.data("validator", null);

            $.validator.unobtrusive.parse($form);
            // Re add validation with changes
            $form.validate($form.data("unobtrusiveValidation").options);
            if ($form.data("validate-tooltip") == true) {
                this.EnableValidateToolTip($form);
            }
        }
    },
    RemoveScript: function (c) {
        if (!Common.Empty(c)) {
            r = c.replace(/(<script(.|\n)*<\/script>)|(&lt;script(.|\n)*&lt;\/script&gt;)/gi, "");
        }
        return r;
    },
    Keys: {
        8: true,
        35: true,
        36: true,
        37: true,
        39: true,
    },
    //cho phép nhập dấu phẩy
    ValidateDecimalTrueComma: function (event, target) {
        var regex = new RegExp(/[^0-9]/g);
        var keyCode = event.charCode || event.keyCode || event.which;
        if ((keyCode >= 48 && keyCode <= 57) || keyCode == 44) {
            var text = $(target).val();
            if (text.indexOf(",") != -1 && keyCode == 44)
                event.preventDefault();
        } else {
            if (!Common.Empty(String.fromCharCode(keyCode)) && !Common.Keys[keyCode]) {
                event.preventDefault();
            }
        }
    },
    //cho phép nhập dấu trừ
    ValidateDecimalTrueMinus: function (event, target) {
        var keyCode = event.charCode || event.keyCode || event.which;
        if ((keyCode >= 48 && keyCode <= 57) || keyCode == 45) {
            var text = $(target).val();
            if (text.indexOf("-") != -1 && keyCode == 45)
                event.preventDefault();
        } else {
            if (!Common.Empty(String.fromCharCode(keyCode)) && !Common.Keys[keyCode]) {
                event.preventDefault();
            }
        }
    },
    ValidateDecimal: function (event, target) {
        var keyCode = event.charCode || event.keyCode || event.which || event.keyCode;
        if (keyCode >= 48 && keyCode <= 57) {
            var text = $(target).val();
        } else {
            if (!Common.Empty(String.fromCharCode(keyCode)) && !Common.Keys[keyCode]) {
                event.preventDefault();
            }

        }
    },
    ValidateInteger: function (event, target) {
        var keyCode = event.charCode || event.keyCode || event.which;
        if ((keyCode < 48 && keyCode > 57)) {
            if (!Common.Empty(String.fromCharCode(keyCode)) && !Common.Keys[keyCode]) {
                event.preventDefault();
            }
        }
    },
    SetInputFilter: function (textbox, inputFilter) {
        ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
            textbox.addEventListener(event, function () {
                if (inputFilter(this.value)) {
                    this.oldValue = this.value;
                    this.oldSelectionStart = this.selectionStart;
                    this.oldSelectionEnd = this.selectionEnd;
                } else if (this.hasOwnProperty("oldValue")) {
                    this.value = this.oldValue;
                    this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                }
            });
        });
    },
    SetInputReportFilter: function (textbox, inputFilter) {
        ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
            Array.from(document.getElementsByClassName(textbox)).forEach(function (element) {
                $(element)[0].addEventListener(event, function () {
                    if (inputFilter(this.value)) {
                        this.oldValue = this.value;
                        this.oldSelectionStart = this.selectionStart;
                        this.oldSelectionEnd = this.selectionEnd;
                    } else if (this.hasOwnProperty("oldValue")) {
                        this.value = this.oldValue;
                        this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                    }
                });
            });
        });
    },
    ShowForm: function (option) {
        var that = this;
        if (Common.Empty(this.LoadingPages)) {
            this.LoadingPages = new Object();
        }
        if (Common.Empty(this.LoadingPages[option.url])) {
            this.LoadingPages[option.url] = true;
            Common.ShowLoading(true);
            $.ajax({
                url: option.url,
                data: option.data,
                complete: function () {
                    setTimeout(function () {
                        delete that.LoadingPages[option.url];
                    }, 1000);
                },
                success: function (html) {
                    $(option.element).html(html);
                    if (option.callback != undefined && typeof (option.callback) == "function") {
                        option.callback();
                    }
                },

            });
        }
    },
    Empty: function (target) {
        if (target == null || target == undefined || target == "" || target.length == 0 || jQuery.trim(target).length == 0)
            return true;
        return false;
    },
    Serialize: function (el) {
        var serialized = $(el).serialize();
        if (!serialized) // not a form
            serialized = $(el).
                find('input[name],select[name],textarea[name]').serialize();
        return serialized;
    },
    ShowLoading: function (isShow) {
        if (isShow) {
            $(".loadingCommon").show();
        } else {
            $(".loadingCommon").hide();
        }
    },
    ShowLoading: function (isShow, text) {
        if (isShow) {
            $(".loadingCommon").show();
        } else {
            $(".loadingCommon").hide();
        }
        if (text) {
            $(".loadingCommonText").html(text);
        }
        else {
            $(".loadingCommonText").html('');
        }
    },
    GetDateFormat: function (date) {
        return $.datepicker.formatDate($.datepicker._defaults.dateFormat, date);
    },
    // Common.DisableButton("#btn_previous_calendar", true);
    DisableButton: function (selector, isDisable) {
        if (isDisable) {
            $(selector).prop('disabled', true);
        } else {
            $(selector).prop('disabled', false);
        }
    },
    ShowAlert: function (title, body, buttons, className, modalSize) {
        target = $("#modal-alert");
        function HideButtons() {
            $(target).find(".modal-footer .btn").hide();
        }
        function ShowButtons(buttons) {
            var footer = $(target).find(".modal-footer");
            $(footer).find(".new-btn").remove();
            if (buttons.Close != undefined) {
                if (buttons.Close.Display) {
                    $(footer).find(".btn-close").show();
                } else {
                    $(footer).find(".btn-close").hide();
                }
                if (buttons.Close.OnClick) {
                    $(footer).find(".btn-close").click(function (e, v) {
                        buttons.Close.OnClick(e, v);
                    });
                }
            }
            if (!Common.Empty(buttons.Items)) {
                for (var index in buttons.Items) {
                    var button = buttons.Items[index];
                    if (button.Name == undefined) continue;
                    var classs = "";
                    var dataDismiss = ""
                    if (button.Class == undefined) {
                        classs = "btn-primary";
                    }
                    else {
                        if (!Common.Empty(button.Class)) {
                            classs = button.Class;
                        }
                    }
                    if (button.DataDismiss == undefined) {
                        dataDismiss = "";
                    }
                    else {
                        if (!Common.Empty(button.DataDismiss)) {
                            dataDismiss = button.DataDismiss;
                        }
                    }

                    $(footer).prepend('<button type="button" class="btn btn-sm ' + classs + ' new-btn btn-' + button.Name + '" ' + dataDismiss + ' ></button');
                    var element = $(footer).find(".new-btn.btn-" + button.Name);
                    if (button.Value != undefined) {
                        element.html(button.Value);

                    }
                    $(element).data("this", button);
                    if (button.OnClick != undefined) {
                        element.click(button.OnClick);
                    }
                }
            }
        }
        $(target).find(".modal-title").html(title);
        $(target).find(".modal-body").html(body);
        if (!Common.Empty(modalSize)) {
            $(".modal-dialog").addClass(modalSize);
        }
        HideButtons();
        if (buttons == undefined) {
            $(target).find(".modal-footer .btn-close").show();
        } else {
            ShowButtons(buttons);
        }
        if (typeof className != 'undefined') {
            FocusButton(className);
        }
        function FocusButton(className) {
            $(target).unbind("shown.bs.modal").off("shown.bs.modal").on('shown.bs.modal', function () {
                $(target).off("shown.bs.modal");
                $('.btn-' + className).focus();
            });
        }
        $(target).modal("show");
        Common.ShowLoading(false);
    },
    HideAlert: function (close, timeout) {
        target = $("#modal-alert");
        target.modal("hide");
        //$(".modal-backdrop").unbind("click");
        //$(".modal-backdrop").remove();
        $(target).unbind("hidden.bs.modal").off("hidden.bs.modal").on('hidden.bs.modal', function () {
            // $('.modal:visible').length && $(document.body).addClass('modal-open');//scroll
            $(target).off("hidden.bs.modal");
            if (typeof close == 'function') {
                close();
            }
        });
    },
    ShowPopup: function (option) {
        var that = this;
        if (!Common.Empty(option)) {
            $('<div class="modal-backdrop fade in"></div>').appendTo(document.body);
            var params = !Common.Empty(option.Data) ? "?" + $.param(option.Data, true) : "";
            var target = window.open(option.Url + params, '', "width=1024,height=600,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes");
            $(target).focusout(function () {
                alert("abc");
            });
            var myEvent = target.attachEvent || target.addEventListener;
            var chkevent = target.attachEvent ? 'onbeforeunload' : 'beforeunload'; /// make IE7, IE8 compitable
            $(".modal-backdrop").unbind("click").click(function () {
                target.focus();
            });
            myEvent(chkevent, function (e) { // For >=IE7, Chrome, Firefox
                $(".modal-backdrop").unbind("click");
                $(".modal-backdrop").remove();
                if (typeof (option.CallBack) == "function") {
                    option.CallBack(this.Response);
                }
            });
        }
    },
    LoadingCount: 0, //Count loading request
    Ajax: function (setting, done) {
        Common.ShowLoading(true);
        //There is one more loading request
        Common.LoadingCount++;
        $('.loading').stop().fadeToggle(Common.LoadingCount > 0);
        //Add default setting
        setting = $.extend({
            type: "POST",
            async: true,
            cache: false,
            dataType: 'json'
        }, setting);

        return $.ajax(setting).done(function (data, textStatus, jqXHR) {
            Common.ShowLoading(false);
            //Call done callback 
            done(data);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            Common.ShowLoading(false);
            //Server not respond
            if (!jqXHR) return;

            switch (jqXHR.status) {

                case 403:
                    //Not login error
                    alert(jqXHR.statusText);
                    //Redirect to login page
                    if (window && window.location) {
                        window.location.href = "/";
                    }
                    break;
                case 500:
                    alert(jqXHR);
                    alert(textStatus);
                    alert(errorThrown);
                    break;
                case 401:

                    //Redirect to login page
                    if (window && window.location) {
                        //window.location.href = "/Error/Permission";
                        //window.location.href = jqXHR.Data.LogOnUrl;
                        var d = $.parseJSON(jqXHR.responseText);
                        //alert("login failed");  // Let's redirect
                        window.location.href = d.LogOnUrl;
                    }
                    else {
                        //Not login error
                        $.notify({
                            // options
                            message: 'Bạn không có quyền thực hiện chức năng này.!!!'
                        }, {
                                // settings
                                delay: 1000,
                                timer: 1000,
                                type: 'danger'
                            });
                    }
                    break;
                //default:
                //    //Other error
                //    alert(jqXHR.statusText);
                //    break;
            }
        }).always(function () {
            // There is one less loading request
            Common.LoadingCount--;
            $('.loading').stop().fadeToggle(Common.LoadingCount > 0);
            Common.ShowLoading(false);
        });
    },

    FormatDecimal: function (fr) {
        temp = fr.value;
        if (temp == undefined) return;
        fr.value = temp.replace(/\D/g, '');

    },
    FormatCurrency: function (fr) {
        temp = fr.value;
        //fr.value = temp.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".");

        if (temp == undefined) return;
        for (i = 0; i < temp.length; i++) {
            for (k = i; k < temp.length; k++) {
                if (temp.charAt(k) == '.') {
                    temp = temp.replace('.', '');
                }
            }
        }
        var j = 0;
        var s = "";
        var s1 = "";
        var s2 = "";
        for (i = temp.length - 1; i >= 0; i--) {
            j = j + 1;
            if (j == 3) {
                j = 0;
                s1 = temp.substring(0, i);
                s2 = temp.substring(i, i + 3);
                s = "." + s2 + s;
            }
        }
        if (s1.length > 0) {
            s = s1 + s;
            fr.value = s;
        } else if (s.length > 0 && s2.length > 0) {
            fr.value = s.substring(1, s.length);
        }
        fr.value = fr.value.replace("-.", '-');
    },
    ReplaceFormatCurrency: function (num) {
        if (!Common.Empty(num)) {
            var str = num.toString();
            var decPoint = ".";
            if (str.indexOf(decPoint) >= 0) {
                str = str.replace(decPoint, "");
                return str;
            }
        }
        return num;
    },
    //format number(money)
    FormatCurrency2: function (num) {
        if (!Common.Empty(num) && num != null) {
            num = Common.ReplaceFormatCurrency(num);
            var str = Math.round(num).toString();
            var parts = false;
            var output = [];
            var i = 1;
            var sub = "";
            var formatted = null;
            if (str.indexOf(",") > 0) {
                parts = str.split(",");
                str = parts[0];
            }
            if (str.indexOf("-") != -1) {
                sub = str.substr(0, 1);
                str = str.substr(1);
            }
            str = str.split("").reverse();
            for (var j = 0, len = str.length; j < len; j++) {
                if (str[j] != ".") {
                    output.push(str[j]);
                    if (i % 3 == 0 && j < (len - 1)) {
                        output.push(".");
                    }
                    i++;
                }
            }
            formatted = output.reverse().join("");
            if (!Common.Empty(sub)) {
                return (sub + formatted + ((parts) ? "." + parts[1].substr(0, 2) : ""));
            }
            else {
                return (formatted + ((parts) ? "." + parts[1].substr(0, 2) : ""));
            }
        }
        return 0;
    },
    CheckPopupBlocked: function (poppedWindow) {
        setTimeout(function () {
            Common.DoCheckPopupBlocked(poppedWindow);
        }, 500);
    },
    DoCheckPopupBlocked: function (poppedWindow) {
        var result = false;

        try {
            if (typeof poppedWindow == 'undefined') {
                // Safari with popup blocker... leaves the popup window handle undefined
                result = true;
            }
            else if (poppedWindow && poppedWindow.closed) {
                // This happens if the user opens and closes the client window...
                // Confusing because the handle is still available, but it's in a "closed" state.
                // We're not saying that the window is not being blocked, we're just saying
                // that the window has been closed before the test could be run.
                result = false;
            }
            else if (poppedWindow && poppedWindow.outerWidth == 0) {
                // This is usually Chrome's doing. The outerWidth (and most other size/location info)
                // will be left at 0, EVEN THOUGH the contents of the popup will exist (including the
                // test function we check for next). The outerWidth starts as 0, so a sufficient delay
                // after attempting to pop is needed.
                result = true;
            }
            else if (poppedWindow && poppedWindow.test) {
                // This is the actual test. The client window should be fine.
                result = false;
            }
            else {
                // Else we'll assume the window is not OK
                result = true;
            }

        } catch (err) {
            //if (console) {
            //    console.warn("Could not access popup window", err);
            //}
        }

        if (result) {
            //tiến hành lưu rồi in phiếu bán hàng và giấy bảo hành
            Common.ShowAlert("Thông Báo", "Chức năng in không thể thực hiện do trình duyệt không cho phép hiển thị Popup. Hãy cho phép hiển thị Popup để sử dụng chức năng này.", {
                Close: { Display: false },
                Items: {
                    Yes: {
                        Name: "Close",
                        Data: $(this),
                        OnClick: function (target) {
                            Common.HideAlert();
                            $(".modal-backdrop").unbind("click");
                            $(".modal-backdrop").remove();
                        },
                        Value: "Đóng"
                    }
                }
            });
            window.setTimeout(function () { $("#modal-alert").find(".close-modal.btn-Close").focus() }, 500);
        }
    },
    AddZero: function (i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    },
    NewGuild: function () {
        var d = new Date().getTime();
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (d + Math.random() * 16) % 16 | 0;
            d = Math.floor(d / 16);
            return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
        });
        return uuid;
    },
    GuidEmpty: '00000000-0000-0000-0000-000000000000',
    getCookie: function (cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1);
            if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
        }
        return "";
    },
    setCookie: function (cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + "; " + expires;
    },
    dateTimeReviver: function (key, value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                var date = new Date(+a[1]);
                return (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();
            }
        }
        return value;
    },
    OpenFullscreen: function () {
        if (elem.requestFullscreen) {
            elem.requestFullscreen();
        } else if (elem.mozRequestFullScreen) { /* Firefox */
            elem.mozRequestFullScreen();
        } else if (elem.webkitRequestFullscreen) { /* Chrome, Safari & Opera */
            elem.webkitRequestFullscreen();
        } else if (elem.msRequestFullscreen) { /* IE/Edge */
            elem.msRequestFullscreen();
        }
    },
    CloseFullscreen: function () {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        }
    },
    RedirectLoginUrl: function () {
        window.location.href = '/Error/Permission';

    },
    convertStringToDateTime: function (_val) {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0! 
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        var today = dd + '/' + mm + '/' + yyyy;
        _val = (!!_val) ? _val.trim() : today;
        var dateForm = _val.split('/');
        dateTime = dateForm[2] + '-' + dateForm[1] + '-' + dateForm[0];

        var dateResult = new Date(dateTime);
        return dateResult;
    },
    EscapeHtml: function (string) {
        return string
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;")
            .replace(/>/g, "&gt;")
            .replace(/</g, "&lt;");
    },
    getTypeFile: (_filename) =>{
        var _name = '';
        var _f = (_filename != '') ? _filename.lastIndexOf('.') : -1;
        var _t = (_filename != '') ? _filename.length : -1;
        if (_filename != '' && _f > -1) {
            _name = _filename.substring((_f + 1), _t);
        } else {
            _name = '';
        }
        _name = (_name != '') ? _name.toLowerCase() : _name;
        return _name;
    },
    UnescapeHtml: function (string) {
        return string
            .replace("&amp;", /&/g)
            .replace("&lt;", /</g)
            .replace("&gt;", />/g)
            .replace("&quot;", /"/g)
            .replace("&#039;", /'/g)
            .replace("&gt;", />/g)
            .replace("&lt;", /</g);
    },
    Base64EncodeUnicode: function (str) {
        // First we escape the string using encodeURIComponent to get the UTF-8 encoding of the characters, 
        // then we convert the percent encodings into raw bytes, and finally feed it to btoa() function.
        utf8Bytes = encodeURIComponent(str).replace(/%([0-9A-F]{2})/g, function (match, p1) {
            return String.fromCharCode('0x' + p1);
        });
        return btoa(utf8Bytes);
    },
    Collage: function (id, e) {
        var div = $(id);
        if (div) {
            if (div.hasClass('hide')) {
                div.removeClass('hide');
                $('#btnTimKiemNangCao').text('Tìm đơn giản');
            }
            else {
                div.addClass('hide');
                $('#btnTimKiemNangCao').text('Tìm nâng cao');
            }
        }
    },
    SumMatrix: function (element, rowName, rowHeaderName, colName, colHeaderName) {
        //Sum row     
        element.unbind("blur").on("blur", function () {
            let sumRow = 0
            $(this).closest('tr').find('input[type=text]').each((index, item) => {
                var attr = $(item).attr('disabled');
                if (typeof attr === typeof undefined) {
                    sumRow += parseInt($(item).val() || 0);
                }
            });
            if ($(this).closest('tr').find('.' + rowName).find('input[type=text]').length > 0) {
                $(this).closest('tr').find('.' + rowName).find('input[type=text]').val(sumRow);
            }
            else {
                $(this).closest('tr').find('.' + rowName).text(sumRow);
            }

            if ($(this).closest('tr').find('.' + rowHeaderName).find('input[type=text]').length > 0) {
                $(this).closest('tr').find('.' + rowHeaderName).find('input[type=text]').val(sumRow);
            }
            else {
                $(this).closest('tr').find('.' + rowHeaderName).text(sumRow);
            }

            //Sum column
            let sumColumn = 0
            let indexCol = $(this).closest('td').index();
            let idTr = $(this).closest('tr').attr('data-groupid');
            $(this).closest('table').find('tr[data-groupid="' + idTr + '"]').each((index, element) => {
                sumColumn += parseInt($($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
            })
            let sumAll = 0;
            $(this).closest('table').find('tr[data-groupid="' + idTr + '"]').each((index, element) => {
                sumAll += parseInt($(element).find('td[name="TongSoRow"]').text() || 0);
            })
            $('#' + idTr).find('td[name=TongSo]').text(sumAll);
            if ($($(this).closest('table').find('#' + idTr + ' td')[indexCol]).length) {
                $($(this).closest('table').find('#' + idTr + ' td')[indexCol]).val(sumColumn)
            }
            if ($($(this).closest('table').find('#' + idTr + ' td')[indexCol]).length) {
                $($(this).closest('table').find('#' + idTr + ' td')[indexCol]).text(sumColumn)
            }

            //Sum header
            let sumColumnHeader = 0
            $(this).closest('table').find('tr[name="TongSoColumn"]').each((index, element) => {
                sumColumnHeader += parseInt($($(element).find('td')[indexCol]).text() || 0);
            })
            if ($($(this).closest('table').find('tr[name="TongSoColumnHeader"] td')[indexCol]).length) {
                $($(this).closest('table').find('tr[name="TongSoColumnHeader"] td')[indexCol]).text(sumColumnHeader)
            }
        })
    },

    SumMatrixMauSo1: function (element, rowName, rowHeaderName, colName, colHeaderName) {
        element.unbind("blur").on("blur", function () {
            //Sum rows
            let sumColumn = 0;
            let indexCol = $(this).closest('td').index();
            let idTr = $(this).closest('tr').attr('data-groupid');

            $(this).closest('table').find('tr[data-groupid="' + idTr + '"]').each((index, element) => {
                sumColumn += parseInt($($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
            })
            if ($($(this).closest('table').find('#' + idTr + ' td')[indexCol]).length) {
                $($(this).closest('table').find('#' + idTr + ' td')[indexCol]).text(sumColumn)
            }

            // Sum cap 3
            $(this).closest('table tbody').find('tr').each((index, element) => {
                let sumRow = 0;
                let roleId = $(element).attr('data-id');
                $(this).closest('table tbody').find('tr[data-roleid="' + roleId + '"][data-roletype="sum-cap-3"]').each((index, element) => {
                    sumRow += parseInt($($(element).find('td')[indexCol]).text() || 0);
                })
                if (sumRow > 0) {
                    if ($($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-cap-2"] td')[indexCol]).length) {
                        $($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-cap-2"] td')[indexCol]).text(sumRow)
                    }
                }
            })

            // Sum cap 2
            $(this).closest('table tbody').find('tr').each((index, element) => {
                let sumRow = 0;
                let roleId = $(element).attr('data-id');
                $(this).closest('table tbody').find('tr[data-roleid="' + roleId + '"][data-roletype="sum-cap-2"]').each((index, element) => {
                    sumRow += parseInt($($(element).find('td')[indexCol]).text() || 0);
                })
                if (sumRow > 0) {
                    if ($($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-cap-1"] td')[indexCol]).length) {
                        $($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-cap-1"] td')[indexCol]).text(sumRow)
                    }
                }
            })

            // Sum tong
            $(this).closest('table tbody').find('tr').each((index, element) => {
                let sumRow = 0;
                let roleId = $(element).attr('data-id');
                $(this).closest('table tbody').find('tr[data-roleid="' + roleId + '"][data-roletype="sum-cap-1"]').each((index, element) => {
                    sumRow += parseInt($($(element).find('td')[indexCol]).text() || 0);
                })
                if (sumRow > 0) {
                    if ($($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-tong"] td')[indexCol]).length) {
                        $($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-tong"] td')[indexCol]).text(sumRow)
                    }
                }
            })
        })
    },

    SumMatrixMauSo2: function (element, rowName, rowHeaderName, colName, colHeaderName) {
        //Sum row     
        element.unbind("blur").on("blur", function () {
            let sumRow1 = 0, sumRow2 = 0, sumRow3 = 0;
            $(this).closest('tr').find('input[type=text]').each((index, item) => {
                var attr = $(item).attr('disable');
                if (typeof attr === typeof undefined) {
                    if ($(item).hasClass('input-4') || $(item).hasClass('input-7') || $(item).hasClass('input-10') || $(item).hasClass('input-13')) {
                        sumRow1 += parseInt($(item).val() || 0);
                    }
                    if ($(item).hasClass('input-5') || $(item).hasClass('input-8') || $(item).hasClass('input-11') || $(item).hasClass('input-14')) {
                        sumRow2 += parseInt($(item).val() || 0);
                    }
                    if ($(item).hasClass('input-6') || $(item).hasClass('input-9') || $(item).hasClass('input-12') || $(item).hasClass('input-15')) {
                        sumRow3 += parseInt($(item).val() || 0);
                    }
                }
            });

            $(this).closest('tr').find('.input-1').val(sumRow1);
            $(this).closest('tr').find('.input-2').val(sumRow2);
            $(this).closest('tr').find('.input-3').val(sumRow3);

            //Sum column
            let sumColumn = 0, sumColumn1 = 0, sumColumn2 = 0, sumColumn3 = 0;
            let indexCol = $(this).closest('td').index();
            let idTr = $(this).closest('tr').attr('data-groupid');
            let idRole = $('#' + idTr).attr('data-roleid');
            let cpRow1 = $($(this).closest('table').find('#ParentID_101 td')[indexCol]).find('input[type=text]').val();
            let cpRow2 = $($(this).closest('table').find('tr[data-groupid="ParentID_102"] td')[indexCol]).find('input[type=text]').val();
            if (cpRow1 < cpRow2) {
                $.notify({
                    // options
                    message: 'Số lượng của hàng tổng số đang nhỏ hơn số lượng trong đó!!!'
                }, {
                    // settings
                    delay: 1000,
                    timer: 1000, type: 'danger'
                });
            }
            $(this).closest('table').find('tr[data-groupid="' + idTr + '"]').each((index, element) => {
                sumColumn += parseInt($($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
            })
            $(this).closest('table').find('tr[data-groupid="' + idTr + '"] td').each((index, element) => {
                if ($(element).find('input[type=text]').hasClass('input-1')) sumColumn1 += parseInt($(element).find('input[type=text]').val() || 0);
                if ($(element).find('input[type=text]').hasClass('input-2')) sumColumn2 += parseInt($(element).find('input[type=text]').val() || 0);
                if ($(element).find('input[type=text]').hasClass('input-3')) sumColumn3 += parseInt($(element).find('input[type=text]').val() || 0);
            })
            let sumAll = 0;
            $(this).closest('table').find('tr[data-groupid="' + idTr + '"]').each((index, element) => {
                sumAll += parseInt($(element).find('td[name="TongSoRow"]').text() || 0);
            })
            $('#' + idTr).find('td[name=TongSo]').text(sumAll);
            if ($($(this).closest('table').find('#' + idTr + ' td')[indexCol]).length) {
                $($(this).closest('table').find('#' + idTr + ' td')[indexCol]).val(sumColumn)
            }
            if ($($(this).closest('table').find('#' + idTr + ' td')[indexCol]).length) {
                $($(this).closest('table').find('#' + idTr + ' td')[indexCol]).text(sumColumn)
            }
            $(this).closest('table').find('#' + idTr + ' td').each((index, element) => {
                if ($(element).hasClass('text-1')) $(element).text(sumColumn1);
                if ($(element).hasClass('text-2')) $(element).text(sumColumn2);
                if ($(element).hasClass('text-3')) $(element).text(sumColumn3);
            });
            $(this).closest('table').find('#ParentID_' + idRole + ' td').each((index, element) => {
                if ($(element).hasClass('text-1')) $(element).text(sumColumn1);
                if ($(element).hasClass('text-2')) $(element).text(sumColumn2);
                if ($(element).hasClass('text-3')) $(element).text(sumColumn3);
            });

            // Sum cap 3
            $(this).closest('table tbody').find('tr').each((index, element) => {
                let sumRow = 0;
                let roleId = $(element).attr('data-id');
                $(this).closest('table tbody').find('tr[data-roleid="' + roleId + '"][data-roletype="sum-cap-3"]').each((index, element) => {
                    sumRow += parseInt($($(element).find('td')[indexCol]).val() || $($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
                })
                if (sumRow > 0) {
                    if ($($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-cap-2"] td')[indexCol]).length) {
                        $($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-cap-2"] td')[indexCol]).text(sumRow)
                    }
                } 
            })

            // Sum cap 2
            $(this).closest('table tbody').find('tr').each((index, element) => {
                let sumRow = 0;
                let roleId = $(element).attr('data-id');
                $(this).closest('table tbody').find('tr[data-roleid="' + roleId + '"][data-roletype="sum-cap-2"]').each((index, element) => {
                    sumRow += parseInt($($(element).find('td')[indexCol]).text() || $($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
                })
                if (sumRow >= 0) {
                    if ($($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-cap-1"] td')[indexCol]).length) {
                        $($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-cap-1"] td')[indexCol]).text(sumRow)
                    }
                }
            })

            // Sum tong
            //$(this).closest('table tbody').find('tr').each((index, element) => {
            //    let sumRow = 0;
            //    let roleId = $(element).attr('data-id');
            //    $(this).closest('table tbody').find('tr[data-roleid="' + roleId + '"][data-roletype="sum-cap-1"]').each((index, element) => {
            //        sumRow += parseInt($($(element).find('td')[indexCol]).text() || $($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
            //    })
            //    if (sumRow >= 0) {
            //        if ($($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-tong"] td')[indexCol]).length) {
            //            $($(this).closest('table tbody').find('tr[data-id="' + roleId + '"][data-roletype="sum-tong"] td')[indexCol]).text(sumRow)
            //        }
            //    }
            //})
        })
    },

    SumMatrixMauSo3: function (element, rowName, rowHeaderName, colName, colHeaderName) {
        //Sum row     
        element.unbind("blur").on("blur", function () {
            let sumRow1 = 0, sumRow2 = 0;
            $(this).closest('tr').find('input[type=text]').each((index, item) => {
                var attr = $(item).attr('disable');
                if (typeof attr === typeof undefined) {
                    if ($(item).hasClass('input-3') || $(item).hasClass('input-9') || $(item).hasClass('input-10')) {
                        sumRow1 += parseInt($(item).val() || 0);
                    }
                    if ($(item).hasClass('input-13') || $(item).hasClass('input-14') || $(item).hasClass('input-15') || $(item).hasClass('input-16')) {
                        sumRow2 += parseInt($(item).val() || 0);
                    }
                }
            });

            $(this).closest('tr').find('.input-1').val(sumRow1);
            //$(this).closest('tr').find('.input-12').val(sumRow2);

            //Sum column
            let sumColumn = 0, sumColumn1 = 0, sumColumn2 = 0;
            let indexCol = $(this).closest('td').index();
            let idTr = $(this).closest('tr').attr('data-groupid');
            $(this).closest('table').find('tr[data-groupid="' + idTr + '"]').each((index, element) => {
                sumColumn += parseInt($($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
            })
            $(this).closest('table').find('tr[data-groupid="' + idTr + '"] td').each((index, element) => {
                if ($(element).find('input[type=text]').hasClass('input-1')) sumColumn1 += parseInt($(element).find('input[type=text]').val() || 0);
                //if ($(element).find('input[type=text]').hasClass('input-12')) sumColumn2 += parseInt($(element).find('input[type=text]').val() || 0);
            })
            let sumAll = 0;
            $(this).closest('table').find('tr[data-groupid="' + idTr + '"]').each((index, element) => {
                sumAll += parseInt($(element).find('td[name="TongSoRow"]').text() || 0);
            })
            $('#' + idTr).find('td[name=TongSo]').text(sumAll);
            if ($($(this).closest('table').find('#' + idTr + ' td')[indexCol]).length) {
                $($(this).closest('table').find('#' + idTr + ' td')[indexCol]).val(sumColumn)
            }
            if ($($(this).closest('table').find('#' + idTr + ' td')[indexCol]).length) {
                if (idTr != 'ParentID_46' && idTr != 'ParentID_159')
                    $($(this).closest('table').find('#' + idTr + ' td')[indexCol]).text(sumColumn)
                else
                    $($(this).closest('table').find('#' + idTr + ' td')[indexCol]).find('input[type=text]').val(sumColumn)
            }
            $(this).closest('table').find('#' + idTr + ' td').each((index, element) => {
                if (idTr == 'ParentID_46' || idTr == 'ParentID_159')
                {
                    if ($(element).hasClass('text-1')) $(element).find('input[type=text]').val(sumColumn1);
                    //if ($(element).hasClass('text-12')) $(element).find('input[type=text]').val(sumColumn2);
                }
                else {
                    if ($(element).hasClass('text-1')) $(element).text(sumColumn1);
                    //if ($(element).hasClass('text-12')) $(element).text(sumColumn2);    
                }               
            });

            //Sum header
            let sumColumnHeader = 0, sumColumnHeader1 = 0, sumColumnHeader2 = 0;
            $(this).closest('table').find('tr[name="TongSoColumnHeaderChild"]').each((index, element) => {
                var idDanhMuc = $(element).attr('data-danhmucid');
                if (idDanhMuc == 46 || idDanhMuc == 159)
                    sumColumnHeader += parseInt($($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
                else
                    sumColumnHeader += parseInt($($(element).find('td')[indexCol]).text() || 0);
            })          
            if ($($(this).closest('table').find('tr[name="TongSoColumnHeader_1"] td')[indexCol]).length) {
                $($(this).closest('table').find('tr[name="TongSoColumnHeader_1"] td')[indexCol]).text(sumColumnHeader)
            }
            if ($($(this).closest('table').find('tr[name="TongSoColumnHeader"] td')[indexCol]).length) {
                $($(this).closest('table').find('tr[name="TongSoColumnHeader"] td')[indexCol]).text(sumColumnHeader)
            }
            $(this).closest('table').find('tr[name="TongSoColumnHeader"]').each((index, element) => {
                sumColumnHeader1 += parseInt($(element).find('td[name="DT1TongSo"]').text() || 0)
                    + parseInt($(element).find('td[name="DT2NguoiCanNgheo"]').text() || 0)
                    + parseInt($(element).find('td[name="DT3LDNTKhac"]').text() || 0);
                sumColumnHeader2 += parseInt($(element).find('td[name="DuocDNTuyenDung"]').text() || 0)
                    + parseInt($(element).find('td[name="DuocDNBaoTieuSP"]').text() || 0)
                    + parseInt($(element).find('td[name="ThanhLapToNhomHTXDN"]').text() || 0)
                    + parseInt($(element).find('td[name="TuTaoViecLam"]').text() || 0);
            })
            $(this).closest('table').find('tr[name="TongSoColumnHeader_1"] td').each((index, element) => {
                if ($(element).hasClass('text-1')) $(element).text(sumColumnHeader1);
                //if ($(element).hasClass('text-12')) $(element).text(sumColumnHeader2);
            });
            $(this).closest('table').find('tr[name="TongSoColumnHeader"] td').each((index, element) => {
                if ($(element).hasClass('text-1')) $(element).text(sumColumnHeader1);
                //if ($(element).hasClass('text-12')) $(element).text(sumColumnHeader2);
            });          
        })
    },

    SumMatrixMauSo4: function (element, rowName, rowHeaderName, colName, colHeaderName) {
        element.unbind("blur").on("blur", function () {
            //Sum rows
            let sumColumn = 0;
            let indexCol = $(this).closest('td').index();
            let idTr = $(this).closest('tr').attr('data-groupid');

            $(this).closest('table').find('tr[data-groupid="' + idTr + '"]').each((index, element) => {
                sumColumn += parseInt($($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
            })
            $('#' + idTr).find('td[name=TongSo]').text(sumColumn);

            //Sum header
            let sumColumnHeader1 = 0;
            $(this).closest('table').find('tr[name="von-su-nghiep"]').each((index, element) => {
                if ($($(element).find('td')[indexCol]).text() > 0)
                    sumColumnHeader1 += parseInt($($(element).find('td')[indexCol]).text() || 0);
                else
                    sumColumnHeader1 += parseInt($($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
            })
            if ($($(this).closest('table').find('tr[name="sum-von-su-nghiep"] td')[indexCol]).length) {
                $($(this).closest('table').find('tr[name="sum-von-su-nghiep"] td')[indexCol]).text(sumColumnHeader1)
            }

            //Sum header
            let sumColumnHeader2 = 0;
            $(this).closest('table').find('tr[name="sum-von-su-nghiep"]').each((index, element) => {
                if ($($(element).find('td')[indexCol]).text() > 0)
                    sumColumnHeader2 += parseInt($($(element).find('td')[indexCol]).text() || 0);
                else
                    sumColumnHeader2 += parseInt($($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
            })
            $(this).closest('table').find('tr[name="sum-von-dau-tu"]').each((index, element) => {
                if ($($(element).find('td')[indexCol]).text() > 0)
                    sumColumnHeader2 += parseInt($($(element).find('td')[indexCol]).text() || 0);
                else
                    sumColumnHeader2 += parseInt($($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
            })
            if ($($(this).closest('table').find('tr[name="sum-nguon-kinh-phi"] td')[indexCol]).length) {
                $($(this).closest('table').find('tr[name="sum-nguon-kinh-phi"] td')[indexCol]).text(sumColumnHeader2)
            }

            //Sum header
            let sumColumnHeader3 = 0;
            $(this).closest('table').find('tr[name="su-dung-kinh-phi"]').each((index, element) => {
                if ($($(element).find('td')[indexCol]).text() > 0)
                    sumColumnHeader3 += parseInt($($(element).find('td')[indexCol]).text() || 0);
                else
                    sumColumnHeader3 += parseInt($($(element).find('td')[indexCol]).find('input[type=text]').val() || 0);
            })
            if ($($(this).closest('table').find('tr[name="sum-su-dung-kinh-phi"] td')[indexCol]).length) {
                $($(this).closest('table').find('tr[name="sum-su-dung-kinh-phi"] td')[indexCol]).text(sumColumnHeader3)
            }
        })
    },

    DisabledCol: function () {
        $('#tableBaoCao tbody tr').each((index, element) => {
            var colDisabled = $.trim($(element).attr('data-coldisabled'));
            var arrCol = colDisabled.split(',');
            arrCol.forEach(item => {
                if (item != "") {
                    $($(element).find('td')[item]).find('input[type=text]').attr('readonly', true);
                    $($(element).find('td')[item]).find('input[type=text]').attr('disabled', 'disabled');
                }
            })
        })
    },

    formatNumber: (n) =>{
        return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
    },
    formatCurrency: (input, blur) => {
        var input_val = input.val();
        if (input_val === "") { return; }
        var original_len = input_val.length;
        var caret_pos = input.prop("selectionStart");
        if (input_val.indexOf(".") >= 0) {
            var decimal_pos = input_val.indexOf(".");
            var left_side = input_val.substring(0, decimal_pos);
            var right_side = input_val.substring(decimal_pos);
            left_side = Common.formatNumber(left_side);
            right_side = Common.formatNumber(right_side);
            if (blur === "blur") {
                right_side += "";
            }
            right_side = right_side.substring(0, 2);
            input_val = left_side + "." + right_side;

        } else {
            input_val = Common.formatNumber(input_val);
            input_val = input_val;
            if (blur === "blur") {
                input_val += "";
            }
        }
        input.val(input_val);
        var updated_len = input_val.length;
        caret_pos = updated_len - original_len + caret_pos;
        input.setSelectionRange(caret_pos, caret_pos);
    },
};
this.Common = window.Common = new Common();

