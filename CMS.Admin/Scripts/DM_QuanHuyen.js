var DMQuanHuyen = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
DMQuanHuyen.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea, select").val("");
            Common.DMQuanHuyen.SubmitForm();
        });

        $("#btnAddForm").unbind("click").click(function () {
            $("#form-update").find("input[name='Id']").val("");
            $("#form-update").find("input[type=text], textarea").val("");
            $("#CongKhai").prop("checked", true);
            $(".field-validation-error").html("");
            $(".field-validation-error").hide();
            $("#dialogAddDanhMuc .modal-title").text("Thêm mới danh mục quận huyện");
            Common.ThemDM_QuanHuyen.ShowDialog();
            return false;
        });
        $(".btnExportQuanHuyen").unbind("click").click(function () {
            var _model = '"TuKhoa":"' + $('input[name="TuKhoa"]').val() + '",';
            _model += '"TinhThanhID":"' + (parseInt($('select[name="TinhThanhID"]').val()) > 0 ? parseInt($('select[name="TinhThanhID"]').val()) : '') + '"';
            Common.Ajax({
                type: "POST",
                url: Common.DMQuanHuyen.Url.ExportExcel,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportQuanHuyen_Excel' }
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
        $(".btnUpdate").unbind("click").click(function () {
            var id = $(this).closest("tr").data("id");
            Common.Ajax({
                url: Common.DMQuanHuyen.Url.Update,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'html',
                data: { id: id }
                },function (result) {
                    Common.ShowLoading(false);
                    if (!Common.Empty(result)) {
                        $("#form-update").html(result);
                        $("#dialogAddDanhMuc .modal-title").text("Cập nhật danh mục quận huyện");
                        Common.ThemDM_QuanHuyen.ShowDialog();
                    }
                }
            );
            return false;
        });
        $(".btnDelete").unbind("click").click(function (e) {
            var id = $(this).closest("tr").data("id");
            Common.DMQuanHuyen.Delete(id);
        });
    },
    Delete: function (id) {
        Common.ShowAlert("Thông báo", "Bạn có muốn xóa thông tin này không?", {
            Close: { Display: true },
            Items: {
                Cancel: {
                    Name: "Cancel",
                    Data: $(this),
                    Class: "btn-info ",
                    OnClick: function (target) {
                        Common.Ajax({
                            url: Common.DMQuanHuyen.Url.Delete,
                            type: "POST",
                            ContentType: 'application/json; charset=utf-8',
                            async: false,
                            cache: false,
                            dataType: 'JSON',
                            data: { id: id }
                            },function (result) {
                                if (!Common.Empty(result)) {
                                    Common.HideAlert();
                                    if (result.status == true) {
                                        $.notify({
                                            // options
                                            message: 'Xóa dữ liệu thành công.!!!'
                                        }, {
                                            // settings
                                            delay: 2000,
                                            timer: 1000,
                                            type: 'success' });
                                        Common.DMQuanHuyen.SetPage(1);
                                        Common.DMQuanHuyen.SubmitForm();
                                    }
                                    else {
                                        $.notify({
                                            // options
                                            message: 'Xóa dữ liệu không thành công.!!!'
                                        }, {
                                                // settings
                                            delay: 1000,
                                            timer: 1000, type: 'danger'
                                            });
                                    }
                                }
                            }
                        );
                    },
                    Value: "Xóa"
                }
            }
        }, "Cancel");
        return false;
    },
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.DMQuanHuyen.SetPage(page);
        Common.DMQuanHuyen.SubmitForm();
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
    Error: function () {
        $.notify({
            // options
            message: 'Có lỗi xảy ra khi lấy dữ liệu. Xin vui lòng thử lại.!!!'
        }, {
                // settings
            delay: 1000,
            timer: 1000, type: 'danger'
            });
    },
    BeforeSend: function () {
        Common.ShowLoading(true);
        Common.DMQuanHuyen.SetPage(1);
        // Common.DMQuyen.SubmitForm();
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
        Common.DMQuanHuyen.RegisterEvent();
    }
};
// create DMQuanHuyen Object
var DMQuanHuyen = DMQuanHuyen;
DMQuanHuyen.constructor = DMQuanHuyen;
