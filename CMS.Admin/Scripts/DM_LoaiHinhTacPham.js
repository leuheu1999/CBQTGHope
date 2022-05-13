
var DM_LoaiHinhTacPham = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};

DM_LoaiHinhTacPham.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {

        this.IsPaging = false;
        var that = this;

        $("#btnAddForm").unbind("click").click(function () {
            $("#form-update").find("input[name='LoaiHinhId']").val("");
            $("input[name='Ma']").val("");
            $("input[name='TenLoaiHinh']").val("");
            $("textarea[name='MoTa']").val("");
            $("#check-is-active").prop('checked', false);
            $("#dialogAddLoaiHinhTacPham .modal-title").text("Thêm mới loại hình tác phẩm");
            Common.DM_LoaiHinhTacPhamThemMoi.ShowDialog();
            return false;
        });

        $("#btnFormSearch").unbind("click").click(function () {
            Common.DM_LoaiHinhTacPham.SetPage(1);
            Common.DM_LoaiHinhTacPham.SubmitForm();
            return false;
        });

        $(".btnUpdate").unbind("click").click(function () {
            var id = $.trim($(this).closest("tr").data("id"));
            Common.Ajax({
                type: "POST",
                async: false,
                cache: false,
                url: Common.DM_LoaiHinhTacPham.Url.Update,
                dataType: 'html',
                data: { id: id }
            }, function (result) {
                if (!Common.Empty(result)) {
                    $("#form-update").html(result);
                    $("#dialogAddLoaiHinhTacPham .modal-title").text("Cập nhật loại hình tác phẩm");
                    Common.DM_LoaiHinhTacPhamThemMoi.ShowDialog();
                }
            });
            return false;
        });

        $(".btnXoaDM").unbind("click").click(function (e) {
            var id = $(this).closest("tr").data("id");
            Common.DM_LoaiHinhTacPham.Delete(id);
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
                            url: Common.DM_LoaiHinhTacPham.Url.Delete,
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
                                    Common.DM_LoaiHinhTacPham.SetPage(1);
                                    Common.DM_LoaiHinhTacPham.SubmitForm();
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
        Common.DM_LoaiHinhTacPham.SetPage(page);
        Common.DM_LoaiHinhTacPham.SubmitForm();
        return false;
    },
    SetPageSize: function (page) {
        $("#IdformSearch").find("input[name='PageSize']").val(page);
    },
    ChangePageSize: function (e) {
        Common.DM_LoaiHinhTacPham.SetPageSize($(e).val());
        Common.DM_LoaiHinhTacPham.SubmitForm();
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
        Common.DM_LoaiHinhTacPham.SetPage(1);
        // Common.DM_LoaiHinhTacPham.SubmitForm();
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
        Common.DM_LoaiHinhTacPham.RegisterEvent();
    }
};

// create DM_LoaiHinhTacPham Object
var DM_LoaiHinhTacPham = DM_LoaiHinhTacPham;
DM_LoaiHinhTacPham.constructor = DM_LoaiHinhTacPham;

