var DMDungChung = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
DMDungChung.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
        this.AgainRegister();
    },
    AgainRegister: function () {
        $("#form-update-result").unbind('submit').submit(function (e) {
            e.preventDefault();  // prevent standard form submission
            Common.ShowLoading(false);
            var ma = $.trim($("input[name='Ma']").val());
            var ten = $.trim($("input[name='Ten']").val());
            var moTa = $.trim($("textarea[name='MoTa']").val());
            var id = $.trim($("input[name='ID']").val());
            var form = $(this);
            //kiem tra ma
            if (Common.Empty(ma)) {
                $(".thongbaoma").html(Common.DMDungChung.Message.Ma);
                $(".thongbaoma").show();
                $(".thongbaoma").addClass("show");
                $("input[name='Ma']").focus();
                return false;
            }
            else {
                if (ma.length > 0) {
                    ma = Common.RemoveScript(ma);
                    if (ma === '') {
                        $(".thongbaoma").html("Mã " + Common.DMDungChung.Message.MaJavaCript);
                        $(".thongbaoma").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                    if (ma.length > 51) {
                        $(".thongbaoma").html(Common.DMDungChung.Message.LengthMa);
                        $(".thongbaoma").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ma) || (/<[/s/S][a-z][\s\S]*>/i.test(ma))) {
                $(".thongbaoma").html("Mã " + Common.DMDungChung.Message.MaHtml);
                $(".thongbaoma").show();
                $("input[name='Ma']").focus();
                return false;
            }
            $(".thongbaoma").hide();
            //kiem tra ten
            if (Common.Empty(ten)) {
                $(".thongbaoTen").html(Common.DMDungChung.Message.Ten);
                $(".thongbaoTen").show();
                $(".thongbaoTen").addClass("show");
                $("input[name='Ten']").focus();
                return false;
            }
            else {
                if (ten.length > 0) {
                    ten = Common.RemoveScript(ten);
                    if (ten === '') {
                        $(".thongbaoTen").html("Tên " + Common.DMDungChung.Message.MaJavaCript);
                        $(".thongbaoTen").addClass("show");
                        $("input[name='Ten']").focus();
                        return false;
                    }
                    if (ten.length > 501) {
                        $(".thongbaoTen").html(Common.DMDungChung.Message.LengthTen);
                        $(".thongbaoTen").addClass("show");
                        $("input[name='Ten']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ten) || (/<[/s/S][a-z][\s\S]*>/i.test(ten))) {
                $(".thongbaoTen").html("Tên " + Common.DMDungChung.Message.MaHtml);
                $(".thongbaoTen").show();
                $("input[name='Ten']").focus();
                return false;
            }
            $(".thongbaoTen").hide();

            //mo ta
            if (!Common.Empty(moTa)) {
                if (moTa.length > 0) {
                    moTa = Common.RemoveScript(moTa);
                    if (moTa === '') {
                        $(".thongbaoMoTa").html("Mô tả " + Common.DMDungChung.Message.MaJavaCript);
                        $(".thongbaoMoTa").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                    if (moTa.length > 4001) {
                        $(".thongbaoMoTa").html(Common.DMDungChung.Message.LengthMota);
                        $(".thongbaoMoTa").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                }
                if (/<[a-z][\s\S]*>/i.test(moTa) || (/<[/s/S][a-z][\s\S]*>/i.test(moTa))) {
                    $(".thongbaoMoTa").html("Mô tả " + Common.DMDungChung.Message.MaHtml);
                    $(".thongbaoMoTa").show();
                    $("textarea[name='MoTa']").focus();
                    return false;
                }
            }
            $(".thongbaoMoTa").hide();
            Common.ShowLoading(true);
            Common.Ajax({
                type: form.attr("method"),
                url: form.attr("action"),
                async: false,
                cache: false,
                data: form.serialize()
            }, function (data) {
                if (!Common.Empty(data)) {
                    if (data.status == false) {
                        if (!Common.Empty(data.thongbao)) {
                            $.notify({
                                // options
                                message: data.thongbao
                            }, {
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                        }
                        else if (parseInt(id) > 0) {
                            $.notify({
                                // options
                                message: 'Cập nhật không thành công.!!!'
                            }, {
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                        }
                        else {
                            $.notify({
                                // options
                                message: 'Thêm mới không thành công.!!!'
                            }, {
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                        }
                        Common.ShowLoading(false);
                        return false;
                    }
                    else if (data.status == true) {
                        Common.ShowLoading(false);
                        if (parseInt(id) > 0) {
                            $.notify({
                                // options
                                message: 'Cập nhật thành công.!!!'
                            }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'success'
                            });
                        }
                        else {
                            $.notify({
                                // options
                                message: 'Thêm mới thành công.!!!'
                            }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'success'
                            });
                        }
                        $("#ModelAdd").modal("hide");
                        Common.DMDungChung.SetPage(1);
                        Common.DMDungChung.SubmitForm();
                    }
                    else {
                        Common.ShowLoading(false);
                        $.notify({
                            // options
                            message: 'Thêm mới không thành công.!!!'
                        }, {
                            // settings
                            delay: 1000,
                            timer: 1000, type: 'danger'
                        });
                        return false;
                    }
                }
            });
        });
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#result-search-form").find("input[type=text], textarea").val("");
            Common.DMDungChung.SubmitForm();
        });
        //Common.SetInputFilter(document.getElementById("ThuTuHienThi"), function (value) {
        //    return /^\d*$/.test(value);
        //});
        $("#btnFormSearch").unbind("click").click(function () {
            Common.DMDungChung.SetPage(1);
            Common.DMDungChung.SubmitForm();
            return false;
        });
        //show dialog t
        $(".btnAddNew").unbind("click").click(function () {
            $("#ModelAdd").modal("show");
            $("#form-update-result").find("input[type='text'], input[type='hidden']").val("");
            $("textarea").val("");
            $(".field-validation-error").html("");
            $(".field-validation-error").hide();
            $('#ModelAdd').on('shown.bs.modal', function (event) {
                var modal = $(this);
                var DanhMuc = $('#DanhMuc').val();
                modal.find('.modal-title').text('Thêm mới ' + (Common.Empty(DanhMuc) ? Common.DMDungChung.Message.DanhMuc : DanhMuc));
            });
        });

        $("#btn-exportExcel").unbind("click").click(function () {
            var key = 'Export' + $("#Search_Table").val() + 'List_Excel';
            var _model = '"TuKhoa":"' + $("#Search_TuKhoa").val() + '"';
            Common.Ajax({
                type: "POST",
                url: Common.DMDungChung.Url.ExportExcel,
                async: false,
                cache: false,
                data: { Param: _model, Key: key }
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

        //show dialog update cau hoi
        $(".btnEditLV").unbind("click").click(function () {
            Common.ShowLoading(true);
            var id = $(this).closest("tr").data("id");
            Common.Ajax({
                type: "GET",
                url: Common.DMDungChung.Url.Update,
                async: false,
                cache: false,
                dataType: 'html',
                data: {
                    id: id
                }
            }, function (data) {
                if (data) {
                    Common.ShowLoading(false);
                    $("#show-content-dialogAdd").html(data);
                    $("#ModelAdd").modal("show");
                    $('#ModelAdd').on('shown.bs.modal', function (event) {
                        var modal = $(this);
                        var DanhMuc = $('#DanhMuc').val();
                        modal.find('.modal-title').text('Chỉnh sửa ' + (Common.Empty(DanhMuc) ? Common.DMDungChung.Message.DanhMuc : DanhMuc));
                    });
                    Common.DMDungChung.AgainRegister();
                }
                else {
                    Common.DMDungChung.Error();
                    return false;
                }
            });
        });

        $(".btnDelRowLV").unbind("click").click(function (event) {
            event.preventDefault();
            var id = $(this).closest("tr").data("id");
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
                                url: Common.DMDungChung.Url.Delete,
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
                                    Common.DMDungChung.SetPage(1);
                                    Common.DMDungChung.SubmitForm();
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
    },
    Paging: function (page) {
        Common.DMDungChung.SetPage(page);
        Common.DMDungChung.SubmitForm();
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
        if (this.nodeName == "A") {
            page = Common.DMDungChung.GetCurrentPage($(this));
            Common.DMDungChung.SetPage(page);
            Common.DMDungChung.SubmitForm();
            return false;
        }
        else {
            Common.DMDungChung.SetPage(1);
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
    SubmitForm: function () {
        $("#result-search-form").submit();
    },
    Success: function () {
        Common.ShowLoading(false);
        Common.DMDungChung.RegisterEvent();
        Common.DMDungChung.AgainRegister();
    },
    SetPage: function (page) {
        $("#result-search-form").find("input[name='Search.PageIndex']").val(page);
    }
};
var DMDungChung = DMDungChung;
DMDungChung.constructor = DMDungChung;

