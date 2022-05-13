
var DM_NguoiKyThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};

DM_NguoiKyThemMoi.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;

        $("input[name='IsActive']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });

        $("#btnLuuDM").unbind("click").click(function () {
            var ma = $.trim($("input[name='Ma']").val());
            var ten = $.trim($("input[name='Ten']").val());
            var moTa = $.trim($("textarea[name='MoTa']").val());
            if ($('#IsActive').prop('checked')) {
                $("#IsActive").val(true);
            }
            else {
                $("#IsActive").val(false);
            }

            //kiem tra ma
            if (Common.Empty(ma)) {
                $(".thongbaomaNguoiKy").html("Nhập mã!!!");
                $(".thongbaomaNguoiKy").show();
                $(".thongbaomaNguoiKy").addClass("show");
                $("input[name='Ma']").focus();
                return false;
            }
            else {
                if (ma.length > 0) {
                    ma = Common.RemoveScript(ma);
                    if (ma === '') {
                        $(".thongbaomaNguoiKy").html("Mã " + that.Message.MaJavaCript);
                        $(".thongbaomaNguoiKy").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                    if (ma.length > 101) {
                        $(".thongbaomaNguoiKy").html(that.Message.LengthMa);
                        $(".thongbaomaNguoiKy").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ma) || (/<[/s/S][a-z][\s\S]*>/i.test(ma))) {
                $(".thongbaomaNguoiKy").html("Ma " + that.Message.MaHtml);
                $(".thongbaomaNguoiKy").show();
                $("input[name='Ma']").focus();
                return false;
            }

            $(".thongbaomaNguoiKy").hide();

            if (Common.Empty(ten)) {
                $(".thongbaotenNguoiKy").html(that.Message.Ten);
                $(".thongbaotenNguoiKy").show();
                $(".thongbaotenNguoiKy").addClass("show");
                $("input[name='Ten']").focus();
                return false;
            }
            else {
                if (ten.length > 0) {
                    ten = Common.RemoveScript(ten);
                    if (ten === '') {
                        $(".thongbaotenNguoiKy").html("Tên " + that.Message.MaJavaCript);
                        $(".thongbaotenNguoiKy").addClass("show");
                        $("input[name='Ten']").focus();
                        return false;
                    }
                    if (ten.length > 301) {
                        $(".thongbaotenNguoiKy").html(that.Message.LengthTen);
                        $(".thongbaotenNguoiKy").addClass("show");
                        $("input[name='Ten']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ten) || (/<[/s/S][a-z][\s\S]*>/i.test(ten))) {
                $(".thongbaotenNguoiKy").html("Tên " + that.Message.MaHtml);
                $(".thongbaotenNguoiKy").show();
                $("input[name='Ten']").focus();
                return false;
            }
            $(".thongbaotenNguoiKy").hide();
            if (!Common.Empty(moTa)) {
                if (moTa.length > 0) {
                    moTa = Common.RemoveScript(moTa);
                    if (moTa === '') {
                        $(".thongbaomotaNguoiKy").html("Mô tả " + that.Message.MaJavaCript);
                        $(".thongbaomotaNguoiKy").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                    if (moTa.length > 501) {
                        $(".thongbaomotaNguoiKy").html(that.Message.LengthTen);
                        $(".thongbaomotaNguoiKy").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                }
                if (/<[a-z][\s\S]*>/i.test(moTa) || (/<[/s/S][a-z][\s\S]*>/i.test(moTa))) {
                    $(".thongbaomotaNguoiKy").html("Mô tả " + that.Message.MaHtml);
                    $(".thongbaomotaNguoiKy").show();
                    $("textarea[name='MoTa']").focus();
                    return false;
                }
            }
            $(".thongbaomotaNguoiKy").hide();
            var data = $("#form-update").serialize();

            Common.Ajax({
                type: "POST",
                async: false,
                cache: false,
                url: Common.DM_NguoiKy.Url.Create,
                dataType: 'JSON',
                data: data
            }, function (result) {
                if (!Common.Empty(result)) {
                    if (result.checkMa == true && result.status == false) {
                        $.notify({
                            // options
                            message: 'Mã ' + ma + ' đã tồn tại. Vui lòng nhập mã khác.!!!'
                        }, {
                            // settings
                            delay: 1000,
                            timer: 1000, type: 'danger'
                        });
                    }
                    else if (result.status == true) {
                        if ($.trim($("#Id").val()) > 0) {
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
                        Common.DM_NguoiKyThemMoi.HideDialog();
                        Common.DM_NguoiKy.SetPage(1);
                        Common.DM_NguoiKy.SubmitForm();
                    }
                }
            });
            return false;
        });
    },
    ShowDialog: function () {
        $("#dialogAddNguoiKy").modal("show");
        Common.DM_NguoiKyThemMoi.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogAddNguoiKy").modal("hide");
    }
};

// create DM_NguoiKyThemMoi Object
var DM_NguoiKyThemMoi = DM_NguoiKyThemMoi;
DM_NguoiKyThemMoi.constructor = DM_NguoiKyThemMoi;
