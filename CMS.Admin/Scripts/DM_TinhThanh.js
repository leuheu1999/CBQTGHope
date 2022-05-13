var DMTinhThanh = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
DMTinhThanh.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea, select").val("");
            Common.DMTinhThanh.SubmitForm();
        });

        $("#btnFormSearch").unbind("click").click(function () {
            Common.DMTinhThanh.SetPage(1);
            Common.DMTinhThanh.SubmitForm();
            return false;
        });
        $("input[name='TuKhoa']").unbind("keypress").bind("keypress", function (e) {
            if (e.which === 13) {
                Common.DMTinhThanh.SetPage(1);
                Common.DMTinhThanh.SubmitForm();
                return false;
            }
        });
        $("#btnAddForm").unbind("click").click(function () {
            $("#form-update").find("input[name='ID']").val("");
            $("#form-update").find("input[type=text], textarea").val("");
            $("#CongKhai").prop("checked", true);
            $(".field-validation-error").html("");
            $(".field-validation-error").hide();
            $("#dialogAddDanhMuc .modal-title").text("Thêm mới danh mục tỉnh thành");
            Common.ThemDM_TinhThanh.ShowDialog();
            return false;
        });
        $(".btnExportTinhThanh").unbind("click").click(function () {
            var _model = '"TuKhoa":"' + $('input[name="TuKhoa"]').val() + '",';
            _model += '"QuocGiaID":"' + (parseInt($('select[name="QuocGiaID"]').val()) > 0 ? parseInt($('select[name="QuocGiaID"]').val()) : '') + '"';
            Common.Ajax({
                type: "POST",
                url: Common.DMTinhThanh.Url.ExportExcel,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportTinhThanh_Excel' }
            }, function (data) {
                if (data) {
                    window.open('/admin' + data.data);
                    window.close();
                }
                else {
                    $.notify({
                        message: 'Xuất file không thành công!!!'
                    }, {
                        delay: 1000,
                        timer: 1000, type: 'danger'
                        });
                    return false;
                }
            });
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
                        type: 'danger'
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
                                url: Common.DMTinhThanh.Url.Delete,
                                async: false,
                                cache: false,
                                data: {
                                    id: id
                                }
                            }, function (data) {
                                if (data) {
                                    $.notify({
                                        // options
                                        message: 'Xóa dữ liệu thành công!!!'
                                    }, {
                                            // settings
                                            delay: 1000,
                                            timer: 1000, type: 'success'
                                        });
                                    Common.HideAlert();
                                    Common.DMTinhThanh.SetPage(1);
                                    Common.DMTinhThanh.SubmitForm();
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
        $(".update").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.DMTinhThanh.Url.CapNhatTinhThanh,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'html',
                data: { id: id }
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    $("#form-update").html(result);
                    $("#dialogAddDanhMuc .modal-title").text("Cập nhật danh mục tỉnh thành");
                    Common.ThemDM_TinhThanh.ShowDialog();
                }
            }
            );
            return false;
        });
        $('#btnExport').unbind('click').click(function () {
            Common.Ajax({
                url: Common.DMTinhThanh.Url.CapNhatTinhThanh,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'html',
                data: { id: id }
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    $("#form-update").html(result);
                    Common.ThemDM_TinhThanh.ShowDialog();
                }
            }
            );
        });
    },
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.DMTinhThanh.SetPage(page);
        Common.DMTinhThanh.SubmitForm();
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
        Common.ShowLoading(true);
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
        Common.DMTinhThanh.RegisterEvent();
    }
};
// create DMTinhThanh Object
var DMTinhThanh = DMTinhThanh;
DMTinhThanh.constructor = DMTinhThanh;
