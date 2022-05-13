var DMNhomQuyen = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
DMNhomQuyen.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea, select").val("");
            Common.DMNhomQuyen.SubmitForm();
        });
        $("#btnAddForm").unbind("click").click(function () {
            $("#form-update").find("input[name='Id']").val("");
            $("#form-update").find("input[type=text], textarea").val("");
            $("#CongKhai").prop("checked", true);
            $(".field-validation-error").html("");
            $(".field-validation-error").hide();
            $("#dialogAddDanhMuc .modal-title").text("Thêm mới nhóm quyền");
            Common.DMNhomQuyenThemMoi.ShowDialog();
            return false;
        });

        $(".btnUpdate").unbind("click").click(function () {
            var id = $.trim($(this).closest("tr").data("id"));
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.DMNhomQuyen.Url.Update,
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
                    $("#dialogAddDanhMuc .modal-title").text("Cập nhật nhóm quyền");
                    Common.DMNhomQuyenThemMoi.ShowDialog();
                }
            }
            );
            return false;
        });

        $(".btnDelete").unbind("click").click(function (e) {
            var id = $(this).closest("tr").data("id");
            Common.DMNhomQuyen.Delete(id);
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
                            type: "POST",
                            async: false,
                            cache: false,
                            url: Common.DMNhomQuyen.Url.Delete,
                            dataType: 'JSON',
                            data: { id: id }
                        }, function (result) {
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
                                    Common.DMNhomQuyen.SetPage(1);
                                    Common.DMNhomQuyen.SubmitForm();
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
        Common.DMNhomQuyen.SetPage(page);
        Common.DMNhomQuyen.SubmitForm();
        return false;
    },
    SetPageSize: function (page) {
        $("#IdformSearch").find("input[name='PageSize']").val(page);
    },

    ChangePageSize: function (e) {
        Common.DMNhomQuyen.SetPageSize($(e).val());
        Common.DMNhomQuyen.SubmitForm();
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
        Common.DMNhomQuyen.SetPage(1);
       // Common.DMNhomQuyen.SubmitForm();
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
        Common.DMNhomQuyen.RegisterEvent();
    }
};
// create DMNhomQuyen Object
var DMNhomQuyen = DMNhomQuyen;
DMNhomQuyen.constructor = DMNhomQuyen;
