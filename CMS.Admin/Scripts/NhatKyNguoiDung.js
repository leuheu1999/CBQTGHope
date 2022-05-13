var NhatKyNguoiDung = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
var lstid = [];
var arrayquyen = [];
NhatKyNguoiDung.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $(".datepicker-share_iconTuNgay").click(function () {
            $("#TuNgay").focus();
        });
        $(".datepicker-share_iconDenNgay").click(function () {
            $("#DenNgay").focus();
        });
        $(".btnloainhatky").unbind("click").click(function () {
            window.open($(this).data("url"), '_blank');
        });
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea, select").val("");
            Common.NhatKyNguoiDung.SubmitForm();
        });
        $("#btnLamMoi").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea, select").val("");
            Common.NhatKyNguoiDung.SubmitForm();
        });
        $("#selectAll").click(function () {
            $('input:checkbox').not(this).prop('checked', this.checked);
            $('#recent-orders input[name="chkCustbl[]"]').each(function () {
                if($(this).is(":checked")){
                    if (!Common.NhatKyNguoiDung.f_CheckExistInArray(lstid, $(this).attr('data-id'))) {
                        lstid.push($(this).attr('data-id'));
                    }
                }
                else {
                    lstid = Common.NhatKyNguoiDung.f_RemoveItemArray(lstid, $(this).attr('data-id'));
                }
            });
        });
        $("#btnFormSearch").unbind("click").click(function () {
            Common.NhatKyNguoiDung.SetPage(1);
            Common.NhatKyNguoiDung.SubmitForm();
            return false;
        });
        $(".btnDelRow").unbind("click").click(function (event) {
            event.preventDefault();
            var id = $(this).data("id");
            if (Common.Empty(id)) {
                $.notify({
                    // options
                    message: 'Dữ liệu cần xóa không tồn tại.!!!'
                }, {
                    // settings
                    delay: 1000,
                    timer: 1000, type: 'danger'
                    });
                return false;
            }
            Common.ShowAlert("Thông báo", "Xóa dữ liệu đang chọn?", {
                Close: { Display: true },
                Items: {
                    Cancel: {
                        Name: "Cancel",
                        Data: $(this),
                        Class: "btn-info ",
                        OnClick: function (target) {
                            Common.HideAlert();
                            Common.Ajax({
                                type: "POST",
                                url: Common.NhatKyNguoiDung.Url.Delete,
                                async: false,
                                cache: false,
                                data: {
                                    id: id
                                }
                            }, function (data) {
                                if (data) {
                                    $.notify({
                                        // options
                                        message: 'Xóa thành công.!!!'
                                    }, {
                                            // settings
                                        delay: 1000,
                                        timer: 1000, type: 'success'
                                        });

                                    Common.HideAlert();
                                    Common.NhatKyNguoiDung.SubmitForm();
                                }
                                else {
                                    $.notify({
                                        // options
                                        message: 'Xóa không thành công.!!!'
                                    }, {
                                            // settings
                                        delay: 1000,
                                        timer: 1000, type: 'danger'
                                        });

                                    return false;
                                }
                            });
                        },
                        Value: "Xóa"
                    }
                }
            }, "Cancel");
            return false;
        });
        $(".btnDeleteAll").unbind("click").click(function () {
            if ($('input[name="chkCustbl[]"]:checked').length > 0) {
                $('input[name="chkCustbl[]"]').each(function () {
                    if ($(this).prop("checked")) {
                        var itemid = $(this).data("id");
                        lstid.push(itemid);
                    }
                });
                if (lstid.length > 0) {
                    Common.ShowAlert("Thông báo", "Xóa dữ liệu đang chọn?", {
                        Close: { Display: true },
                        Items: {
                            Cancel: {
                                Name: "Cancel",
                                Data: $(this),
                                Class: "btn-info ",
                                OnClick: function (target) {
                                    Common.HideAlert();
                                    Common.Ajax({
                                        type: "POST",
                                        url: Common.NhatKyNguoiDung.Url.DeleteLst,
                                        async: false,
                                        cache: false,
                                        data: {
                                            ids: lstid
                                        }
                                    }, function (data) {
                                        if (data) {
                                            Common.NhatKyNguoiDung.SetPage(1);
                                            Common.NhatKyNguoiDung.SubmitForm();
                                        }
                                        else {
                                            $.notify({
                                                // options
                                                message: 'Xóa không thành công.!!!'
                                            }, {
                                                    // settings
                                                delay: 1000,
                                                timer: 1000, type: 'danger'
                                                });

                                            return false;
                                        }
                                    });
                                },
                                Value: "Xóa"
                            }
                        }
                    }, "Cancel");
                }
            }
            else {
                $.notify({
                    // options
                    message: 'Vui lòng chọn dữ liệu để xóa!!!'
                }, {
                        // settings
                    delay: 1000,
                    timer: 1000, type: 'danger'
                    });
            }
            return false;
        });

        $("#TuNgay").datepicker({
            isRTL: false,
            format: 'dd/mm/yyyy',
            autoclose: true,
            orientation: 'bottom auto',
            useCurrent: false,
            language: 'vn'
        }).on('changeDate', function (e) {
            if (e.date) {
                var date = new Date(e.date);
                $('#DenNgay').datepicker('setStartDate', date);
                var temp = $("#DenNgay").datepicker('getDate');
                if (!temp || temp < date) {
                    $('#DenNgay').datepicker("setDate", null);
                }
            }
            else {
                $('#DenNgay').datepicker('setStartDate', null);
            }
        });

        $("#DenNgay").datepicker({
            isRTL: false,
            format: 'dd/mm/yyyy',
            autoclose: true,
            useCurrent: false,
            orientation: 'bottom auto',
            language: 'vn'
        });
        $("#btn-exportWord").unbind("click").click(function () {
            var form = $("#IdformSearch");
            var _model = '"TuKhoa":"' + form.find("input[name='TuKhoa']").val() + '",';
            _model += '"TuNgay":"' + form.find("input[name='TuNgay']").val() + '",';
            _model += '"DenNgay":"' + form.find("input[name='DenNgay']").val() + '",';
            _model += '"LogTypeID":"' + (form.find("select[name='LogTypeID']").val() || "0") + '"';
            Common.Ajax({
                type: "POST",
                url: Common.NhatKyNguoiDung.Url.ExportWordPdf,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'Export_NhatKyNguoiDung_Word' }
            }, function (result) {
                if (result) {
                    let a = document.createElement('a')
                    a.href = 'data:application/octet-stream;base64,' + result.data;
                    a.download = result.fileName;
                    document.body.appendChild(a)
                    a.click();
                    document.body.removeChild(a);
                }
                else {
                    $.notify({
                        // options
                        message: "Xuất file không thành công!!!",
                    }, {
                        // settings
                        delay: 1000,
                        timer: 1000, type: 'danger'
                    });
                    return false;
                }
            });
        });
        $("#btn-exportExcel").unbind("click").click(function () {
            var form = $("#IdformSearch");
            var _model = '"TuKhoa":"' + form.find("input[name='TuKhoa']").val() + '",';
            _model += '"TuNgay":"' + form.find("input[name='TuNgay']").val() + '",';
            _model += '"DenNgay":"' + form.find("input[name='DenNgay']").val() + '",';
            _model += '"LogTypeID":"' + (form.find("select[name='LogTypeID']").val() || "0") + '"';
            Common.Ajax({
                type: "POST",
                url: Common.NhatKyNguoiDung.Url.ExportExcel,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'Export_NhatKyNguoiDung_Excel' }
            }, function (result) {
                if (result) {
                    let a = document.createElement('a')
                    a.href = 'data:application/octet-stream;base64,' + result.data;
                    a.download = result.fileName;
                    document.body.appendChild(a)
                    a.click();
                    document.body.removeChild(a);
                }
                else {
                    $.notify({
                        // options
                        message: "Xuất file không thành công!!!",
                    }, {
                        // settings
                        delay: 1000,
                        timer: 1000, type: 'danger'
                    });
                    return false;
                }
            });
        });
        $("#btn-exportPdf").unbind("click").click(function () {
            var form = $("#IdformSearch");
            var _model = '"TuKhoa":"' + form.find("input[name='TuKhoa']").val() + '",';
            _model += '"TuNgay":"' + form.find("input[name='TuNgay']").val() + '",';
            _model += '"DenNgay":"' + form.find("input[name='DenNgay']").val() + '",';
            _model += '"LogTypeID":"' + (form.find("select[name='LogTypeID']").val() || "0") + '"';
            Common.Ajax({
                type: "POST",
                url: Common.NhatKyNguoiDung.Url.ExportWordPdf,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'Export_NhatKyNguoiDung_Pdf' }
            }, function (result) {
                if (result) {
                    let a = document.createElement('a')
                    a.href = 'data:application/octet-stream;base64,' + result.data;
                    a.download = result.fileName;
                    document.body.appendChild(a)
                    a.click();
                    document.body.removeChild(a);
                }
                else {
                    $.notify({
                        // options
                        message: "Xuất file không thành công!!!",
                    }, {
                        // settings
                        delay: 1000,
                        timer: 1000, type: 'danger'
                    });
                    return false;
                }
            });
        });
    },
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.NhatKyNguoiDung.SetPage(page);
        Common.NhatKyNguoiDung.SubmitForm();
        return false;
    },
    SetPageSize: function (page) {
        $("#IdformSearch").find("input[name='PageSize']").val(page);
    },

    ChangePageSize: function (e) {
        Common.NhatKyNguoiDung.SetPageSize($(e).val());
        Common.NhatKyNguoiDung.SubmitForm();
        return false;
    },
    GetCurrentPage: function (target) {
        try {
            page = 1;
            if (parseInt($(target).text()) > 0) {
                page = parseInt($(target).text());
            } else {
                page = parseInt($(".paging-top .pagination ul li.active").filter(function () { return !($(this).hasClass("previous") || $(this).hasClass("next")); }).text());
                if ($(target).closest("li").hasClass("previous")) {
                    page -= 1;
                } else {
                    page += 1;
                }
            }
            if (page > 0) {
                page = 1;
            } else {
                page = 1;
            }
        } catch (ex) {
            page = 1;
        }
        return page;
    },
    BeforeSend: function (xhr) {
    },
    SubmitForm: function () {
        Common.ShowLoading(true);
        $("#IdformSearch").submit();
    },
    Success: function (data) {
        Common.ShowLoading(false);
        $("body,html").animate({
            scrollTop: 180
        }, 1000);
        Common.NhatKyNguoiDung.RegisterEvent();
    },
    CheckNull: function (str, labelthongbao, Nameforcus) {
        $("." + labelthongbao).html(Common.NhatKyNguoiDung.Message.Nhap + " " + str);
        $("." + labelthongbao).show();
        $("input[name='" + Nameforcus + "']").focus();
        return false;
    },
    checklength(str, labelthongbao, nameforcus, maxlength, nameshow) {
        if (str.length > 0) {
            str = Common.RemoveScript(str);
            if (str === '') {
                $("." + labelthongbao).html(nameshow + " " + Common.NhatKyNguoiDung.Message.MaJavaCript);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
            if (str.length > maxlength) {
                $("." + labelthongbao).html(Common.NhatKyNguoiDung.Message.Length + " " + nameshow + " không quá " + maxlength);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
        }
    },
    checkhtml(str, labelthongbao, nameforcus, nameshow) {

        $("." + labelthongbao).html(nameshow + " " + Common.NhatKyNguoiDung.Message.MaHtml);
        $("." + labelthongbao).show();
        $("input[name='" + nameforcus + "']").focus();
        return false;
    },
    f_CheckedItemNhatKy: function (e) {
        if ($(e).is(":checked")) {
            if (!Common.NhatKyNguoiDung.f_CheckExistInArray(lstid, $(e).attr('data-id'))) {
                lstid.push($(e).attr('data-id'));
            }
        }
        else {
            lstid = Common.NhatKyNguoiDung.f_RemoveItemArray(lstid, $(e).attr('data-id'));
        }
    },
    f_RemoveItemArray: function(arr, item) {
        for (var i in arr) {
            if (arr[i] == item) {
                arr.splice(i, 1);
                break;
            }
        }
        return arr;
    },
    f_CheckExistInArray: function (arr, item) {
        var _result = false;
        for (var i in arr) {
            if (arr[i].toString() == item.toString()) {
                _result = true;
                break;
            }
        }
        return _result;
    },
    f_CheckedItemInArray: function () {
        if(lstid.length > 0){
           // console.log(item)
            for (var i in lstid) {
                //console.log(array[i]);
                $('input[data-id=' + lstid[i] + ']').prop('checked',true);
            }
           
        }
    }
};
// create NhatKyNguoiDung Object
var NhatKyNguoiDung = NhatKyNguoiDung;
NhatKyNguoiDung.constructor = NhatKyNguoiDung;

