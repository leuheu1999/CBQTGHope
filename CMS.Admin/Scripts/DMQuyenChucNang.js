var DMQuyenChucNang = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
DMQuyenChucNang.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea, select").val("");
            Common.DMQuyenChucNang.SubmitForm();
        });
        $("#btnAddForm").unbind("click").click(function () {
            $("#form-update").find("input[name='Id']").val("");
            $("#form-update").find("input[type=text], textarea").val("");
            $("#CongKhai").prop("checked", true);
            $(".hidden-custom").show();
            $("#Controller").val("");
            $(".field-validation-error").html("");
            $(".field-validation-error").hide();
            $("#Action").html("<option>--- Chọn Action ---</option>");
            $("#dialogAddDanhMuc .modal-title").text("Thêm mới quyền chức năng");
            Common.DMQuyenChucNangThemMoi.ShowDialog();
            return false;
        });

        $(".btnUpdate").unbind("click").click(function () {
            var id = $.trim($(this).closest("tr").data("id"));
            Common.Ajax({
                type: "POST",
                async: false,
                cache: false,
                url: Common.DMQuyenChucNang.Url.Update,
                dataType: 'html',
                data: { id: id }
            }, function (result) {
                if (!Common.Empty(result)) {
                    $("#form-update").html(result);
                    $(".hidden-custom").hide();
                    $("#dialogAddDanhMuc .modal-title").text("Cập nhật quyền chức năng");
                    Common.DMQuyenChucNangThemMoi.ShowDialog();
                }
            });
            return false;
        });

        $(".btnDelete").unbind("click").click(function (e) {
            var id = $(this).closest("tr").data("id");
            that.Delete(id);
        });
        $("#recent-orders [data-type='Category']").unbind("click").click(function () {
            var id = $(this).data().id;
            if ($(this).data("status") == "minus") {
                $(this).find(".icon").html("<i class='fa fa-plus-square' aria-hidden='true'></i>");
                $(this).closest("tbody").find("tr[data-category='" + id + "']").hide();
                $(this).data("status", "plus");
            } else {
                $(this).find(".icon").html("<i class='fa fa-minus-square' aria-hidden='true'></i>");
                $(this).data("status", "minus");
                $(this).closest("tbody").find("tr[data-category='" + id + "']").show();
            }
        });
    },
    TemplateHtmlCBO(nameHienThi, idControl, nameControl, dataObj) {
        var html = "<label class='font-weight-bold'>" + nameHienThi + " <span class='text-danger'>(*)</span> </label>";
        html += "<select class='form-control form-control-sm select-share' id='" + idControl + "' name='" + nameControl + "'>";
        html += "<option  value=''>--- Chọn action ---</option>";
        for (var i = 0; i < dataObj.length; i++) {
            html += "<option value='" + dataObj[i].Value + "'>" + dataObj[i].Text + "</option>";
        }
        html += "</select>";
        html += "<div class='field-validation-error thongbaoAction invalid-feedback'></div>";
        return html;
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
                            type: "POST",
                            async: false,
                            cache: false,
                            url: Common.DMQuyenChucNang.Url.Delete,
                            dataType: 'JSON',
                            data: { id: id }
                        }, function (result) {
                            Common.ShowLoading(false);
                            if (!Common.Empty(result)) {
                                Common.HideAlert();
                                if (result.status == true) {
                                    $.notify({
                                        // options
                                        message: 'Xóa dữ liệu thành công.!!!'
                                    }, {
                                            // settings
                                        delay: 1000,
                                        timer: 1000, type: 'success'
                                        });
                                    Common.DMQuyenChucNang.SetPage(1);
                                    Common.DMQuyenChucNang.SubmitForm();
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
                        });
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
        Common.DMQuyenChucNang.SetPage(page);
        Common.DMQuyenChucNang.SubmitForm();
        return false;
    },
    SetPageSize: function (page) {
        $("#IdformSearch").find("input[name='PageSize']").val(page);
    },

    ChangePageSize: function (e) {
        Common.DMQuyenChucNang.SetPageSize($(e).val());
        Common.DMQuyenChucNang.SubmitForm();
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
    BeforeSend: function () {
        Common.ShowLoading(true);
        Common.DMQuyenChucNang.SetPage(1);
        // Common.DMQuyenChucNang.SubmitForm();
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
    SubmitForm: function () {
        $("#IdformSearch").submit();
    },
    Success: function (data) {
        Common.ShowLoading(false);
        $("body,html").animate({
            scrollTop: 180
        }, 1000);
        Common.DMQuyenChucNang.RegisterEvent();
    }
};
// create DMQuyenChucNang Object
var DMQuyenChucNang = DMQuyenChucNang;
DMQuyenChucNang.constructor = DMQuyenChucNang;
