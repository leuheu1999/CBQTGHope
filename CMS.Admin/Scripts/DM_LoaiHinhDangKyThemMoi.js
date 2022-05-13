
var DM_LoaiHinhDangKyThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};

DM_LoaiHinhDangKyThemMoi.prototype = {
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
            var ten = $.trim($("input[name='TenLoaiHinh']").val());
            var moTa = $.trim($("textarea[name='MoTa']").val());
            if ($('#IsActive').prop('checked')) {
                $("#IsActive").val(true);
            }
            else {
                $("#IsActive").val(false);
            }

            //kiem tra ma
            if (Common.Empty(ma)) {
                $(".thongbaomaloaihinhdangky").html("Nhập mã!!!");
                $(".thongbaomaloaihinhdangky").show();
                $(".thongbaomaloaihinhdangky").addClass("show");
                $("input[name='Ma']").focus();
                return false;
            }
            else {
                if (ma.length > 0) {
                    ma = Common.RemoveScript(ma);
                    if (ma === '') {
                        $(".thongbaomaloaihinhdangky").html("Mã " + that.Message.MaJavaCript);
                        $(".thongbaomaloaihinhdangky").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                    if (ma.length > 101) {
                        $(".thongbaomaloaihinhdangky").html(that.Message.LengthMa);
                        $(".thongbaomaloaihinhdangky").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ma) || (/<[/s/S][a-z][\s\S]*>/i.test(ma))) {
                $(".thongbaomaloaihinhdangky").html("Ma " + that.Message.MaHtml);
                $(".thongbaomaloaihinhdangky").show();
                $("input[name='Ma']").focus();
                return false;
            }

            $(".thongbaomaloaihinhdangky").hide();

            if (Common.Empty(ten)) {
                $(".thongbaotenloaihinhdangky").html(that.Message.Ten);
                $(".thongbaotenloaihinhdangky").show();
                $(".thongbaotenloaihinhdangky").addClass("show");
                $("input[name='TenLoaiHinh']").focus();
                return false;
            }
            else {
                if (ten.length > 0) {
                    ten = Common.RemoveScript(ten);
                    if (ten === '') {
                        $(".thongbaotenloaihinhdangky").html("Tên " + that.Message.MaJavaCript);
                        $(".thongbaotenloaihinhdangky").addClass("show");
                        $("input[name='TenLoaiHinh']").focus();
                        return false;
                    }
                    if (ten.length > 301) {
                        $(".thongbaotenloaihinhdangky").html(that.Message.LengthTen);
                        $(".thongbaotenloaihinhdangky").addClass("show");
                        $("input[name='TenLoaiHinh']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ten) || (/<[/s/S][a-z][\s\S]*>/i.test(ten))) {
                $(".thongbaotenloaihinhdangky").html("Tên " + that.Message.MaHtml);
                $(".thongbaotenloaihinhdangky").show();
                $("input[name='TenLoaiHinh']").focus();
                return false;
            }
            $(".thongbaotenloaihinhdangky").hide();
            if (!Common.Empty(moTa)) {
                if (moTa.length > 0) {
                    moTa = Common.RemoveScript(moTa);
                    if (moTa === '') {
                        $(".thongbaomotaloaihinhdangky").html("Mô tả " + that.Message.MaJavaCript);
                        $(".thongbaomotaloaihinhdangky").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                    if (moTa.length > 501) {
                        $(".thongbaomotaloaihinhdangky").html(that.Message.LengthTen);
                        $(".thongbaomotaloaihinhdangky").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                }
                if (/<[a-z][\s\S]*>/i.test(moTa) || (/<[/s/S][a-z][\s\S]*>/i.test(moTa))) {
                    $(".thongbaomotaloaihinhdangky").html("Mô tả " + that.Message.MaHtml);
                    $(".thongbaomotaloaihinhdangky").show();
                    $("textarea[name='MoTa']").focus();
                    return false;
                }
            }
            $(".thongbaomotaloaihinhdangky").hide();
            var data = $("#form-update").serialize();

            Common.Ajax({
                type: "POST",
                async: false,
                cache: false,
                url: Common.DM_LoaiHinhDangKy.Url.Create,
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
                        if ($.trim($("#LoaiHinhId").val()) > 0) {
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
                        Common.DM_LoaiHinhDangKyThemMoi.HideDialog();
                        Common.DM_LoaiHinhDangKy.SetPage(1);
                        Common.DM_LoaiHinhDangKy.SubmitForm();
                    }
                }
            });
            return false;
        });
    },
    ShowDialog: function () {
        $("#dialogAddLoaiHinhDangKy").modal("show");
        Common.DM_LoaiHinhDangKyThemMoi.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogAddLoaiHinhDangKy").modal("hide");
    }
};

// create DM_LoaiHinhDangKyThemMoi Object
var DM_LoaiHinhDangKyThemMoi = DM_LoaiHinhDangKyThemMoi;
DM_LoaiHinhDangKyThemMoi.constructor = DM_LoaiHinhDangKyThemMoi;
