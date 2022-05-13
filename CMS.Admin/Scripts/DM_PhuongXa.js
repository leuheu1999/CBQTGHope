var DMPhuongXa = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
DMPhuongXa.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea, select").val("");
            Common.DMPhuongXa.SubmitForm();
        });
        $("input[name='CongKhai']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });
        $("#btnFormSearch").unbind("click").click(function () {
            Common.DMPhuongXa.SetPage(1);
            Common.DMPhuongXa.SubmitForm();
            return false;
        });
        $("input[name='TuKhoa']").unbind("keypress").bind("keypress", function (e) {
            if (e.which === 13) {
                Common.DMPhuongXa.SetPage(1);
                Common.DMPhuongXa.SubmitForm();
                return false;
            }
        });
        $("#btnAddForm").unbind("click").click(function () {
            $("#form-update").find("input[name='PhuongXaID']").val("");
            $("#form-update").find("input[type=text], textarea").val("");
            $("#TinhThanhID").val("");
            $("#QuanHuyenID").html("<option>--- Chọn quận huyện ---</option>");
            $("#CongKhai").prop("checked", true);
            $(".field-validation-error").html("");
            $(".field-validation-error").hide();
            $("#dialogAddDanhMuc .modal-title").text("Thêm mới danh mục phường xã");
            $('#dialogAddDanhMuc input[id="ID"]').val(0);
            Common.ThemDM_PhuongXa.ShowDialog();
            return false;
        });
        $(".btnExportPhuongXa").unbind("click").click(function () {
            var _model = '"TuKhoa":"' + $('input[name="TuKhoa"]').val() + '",';
            _model += '"TinhThanhID":"' + (parseInt($('select[name="TinhThanhID"]').val()) > 0 ? parseInt($('select[name="TinhThanhID"]').val()) : '') + '",';
            _model += '"QuanHuyenID":"' + (parseInt($('select[name="QuanHuyenID"]').val()) > 0 ? parseInt($('select[name="QuanHuyenID"]').val()) : '') + '"';
            Common.Ajax({
                type: "POST",
                url: Common.DMPhuongXa.Url.ExportExcel,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportPhuongXa_Excel' }
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
        $(".update").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.DMPhuongXa.Url.CapNhatPhuongXa,
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
                        var val = $("#TinhThanhID option:selected").val();
                        $("#dialogAddDanhMuc .modal-title").text("Cập nhật danh mục phường xã");
                        Common.ThemDM_PhuongXa.ShowDialog();
                    }
                }
            );
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
                                url: Common.DMPhuongXa.Url.Delete,
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
                                    Common.DMPhuongXa.SetPage(1);
                                    Common.DMPhuongXa.SubmitForm();
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
        $("#TinhThanhIDS").unbind("change").change(function () {
            var val = $("#TinhThanhIDS option:selected").val();
            Common.ShowLoading(true);
            Common.DMPhuongXa.AjaxCBOQuanHuyen(val);
            return false;
        });
    },
    AjaxCBOQuanHuyen: function (id) {
        Common.Ajax({
            url: Common.DMPhuongXa.Url.CBOQuanHuyenById,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { id: id }
            },function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    var html = Common.DMPhuongXa.TemplateHtmlCBO("Quận huyện", "QuanHuyenIDS", "QuanHuyenID", result.data);
                    $(".divQuanHuyenFormResult").html(html);
                    Common.DMPhuongXa.RegisterEvent();
                }
            }
        );
    },
    TemplateHtmlCBO(nameHienThi, idControl, nameControl, dataObj) {
        html = "<select class='form-control form-control-sm' id='" + idControl + "' name='" + nameControl + "'>";
        html += "<option>--- Chọn quận huyện ---</option>";
        for (var i = 0; i < dataObj.length; i++) {
            html += "<option value='" + dataObj[i].Value + "'>" + dataObj[i].Text + "</option>";
        }
        html += "</select>";
        return html;
    },
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.DMPhuongXa.SetPage(page);
        Common.DMPhuongXa.SubmitForm();
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
        Common.DMPhuongXa.RegisterEvent();
    }
};
// create DMPhuongXa Object
var DMPhuongXa = DMPhuongXa;
DMPhuongXa.constructor = DMPhuongXa;
