var DVC_QuyenLienQuan = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
DVC_QuyenLienQuan.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
        this.AgainRegister();
    },
    AgainRegister: function () {
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#search-form").find("input[type=text], textarea").val("");
            Common.DVC_QuyenLienQuan.SetPage(1);
            Common.DVC_QuyenLienQuan.SubmitSearchForm();
        });
        $("#btn-exportExcel").unbind("click").click(function () {
            var form = $("#search-form");
            var _model = '"TuKhoa":"' + form.find("input[name='TuKhoa']").val() + '",';
            _model += '"SoGCN":"' + form.find("input[name='SoGCN']").val() + '",';
            _model += '"NgayCapTu":"' + form.find("input[name='NgayCapTu']").val() + '",';
            _model += '"NgayCapDen":"' + form.find("input[name='NgayCapDen']").val() + '",';
            _model += '"TenNguoiBieuDien":"' + form.find("input[name='TenNguoiBieuDien']").val() + '",';
            _model += '"TenChuSoHuu":"' + form.find("input[name='TenChuSoHuu']").val() + '",';
            _model += '"CreatedUser":"' + form.find("input[name='CreatedUser']").val() + '",';
            _model += '"VungMienID":"' + form.find("select[name='VungMienID']").val() + '",';
            _model += '"TrangThaiID":"' + form.find("select[name='TrangThaiID']").val() + '"';
            Common.Ajax({
                type: "POST",
                url: Common.DVC_QuyenLienQuan.Url.ExportExcel,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportDVC_QuyenLienQuanList_Excel' }
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
        $(".btnXoa").unbind("click").click(function () {
            var id = $(this).data("id");
            if (Common.Empty(id)) {
                $.notify({
                    message: 'Dữ liệu cần xóa không tồn tại.!!!'
                }, {
                    delay: 1000,
                    timer: 1000, type: 'danger'
                });
                return false;
            }
            Common.ShowAlert("Thông báo", "Xóa dữ liệu đang chọn?", {
                Close: {
                    Display: true
                },
                Items: {
                    Cancel: {
                        Name: "Cancel",
                        Data: $(this),
                        Class: "btn-info ",
                        OnClick: function (target) {
                            Common.HideAlert();
                            Common.Ajax({
                                type: "POST",
                                url: Common.DVC_QuyenLienQuan.Url.DeleteQuyenLienQuan,
                                async: false,
                                cache: false,
                                data: {
                                    id: id
                                }
                            }, function (data) {
                                if (data) {
                                    Common.HideAlert();
                                    $.notify({
                                        // options
                                        message: 'Xóa dữ liệu thành công!!!'
                                    }, {
                                        // settings
                                        delay: 1000,
                                        timer: 1000, type: 'success'
                                    });
                                    Common.DVC_QuyenLienQuan.SetPage(1);
                                    Common.DVC_QuyenLienQuan.SubmitSearchForm();
                                } else {
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
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("a[data-action='reload'],  #btnLamMoi").unbind("click").click(function () {
            $("#search-form").find("input[type=text], textarea").val("");
            $("#search-form").find("select").val(null).trigger('change');
            Common.DVC_QuyenLienQuan.SubmitSearchForm();
        });
        $("#typeseach").unbind("click").click(function () {
            if ($(".searhnc").hasClass("hidden")) {
                $(".searhnc").removeClass("hidden");
                $(this).html("<i class='fas fa-search mr-1'></i> Tìm kiếm cơ bản");
            }
            else {
                $(".searhnc").addClass("hidden");
                $(this).html("<i class='fas fa-search mr-1'></i> Tìm kiếm nâng cao");
            }
            $("#search-form").find("input[type=text], textarea").val("");
            $("#search-form").find("select").val(null).trigger('change');
        });
        $("#btnFormSearchCoSo").unbind("click").click(function () {
            Common.DVC_QuyenLienQuan.SetPage(1);
            Common.DVC_QuyenLienQuan.SubmitSearchForm();
            return false;
        });
    },
    Paging: function (page) {
        Common.DVC_QuyenLienQuan.SetPage(page);
        Common.DVC_QuyenLienQuan.SubmitSearchForm();
        return false;
    },
    ChangePageSize: function (e) {
        Common.DVC_QuyenLienQuan.SetPageSize($(e).val());
        Common.DVC_QuyenLienQuan.SubmitSearchForm();
        return false;
    },
    GetCurrentPage: function (target) {
        try {
            page = 1;
            if (parseInt($(target).text()) > 0) {
                page = parseInt($(target).text());
            } else {
                page = parseInt($(".paging-top .pagination ul li.active").filter(function () {
                    return !($(this).hasClass("previous") || $(this).hasClass("next"));
                }).text());
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
        if (this.nodeName == "A") {
            page = Common.DVC_QuyenLienQuan.GetCurrentPage($(this));
            Common.DVC_QuyenLienQuan.SetPage(page);
            return false;
        } else {
            Common.DVC_QuyenLienQuan.SetPage(1);
        }
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
    SubmitSearchForm: function () {
        $("#search-form").submit();
        Common.DVC_QuyenLienQuan.RegisterEvent();
    },
    Success: function () {
        Common.ShowLoading(false);
        Common.DVC_QuyenLienQuan.RegisterEvent();
        Common.DVC_QuyenLienQuan.AgainRegister();
    },
    SetPage: function (page) {
        $("#search-form").find("input[name='PageIndex']").val(page);
    },
    SetPageSize: function (page) {
        $("#search-form").find("input[name='PageSize']").val(page);
    },
};
var DVC_QuyenLienQuan = DVC_QuyenLienQuan;
DVC_QuyenLienQuan.constructor = DVC_QuyenLienQuan;