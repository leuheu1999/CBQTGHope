
var DM_VungMienThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};

DM_VungMienThemMoi.prototype = {
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
            var tiento = $.trim($("input[name='Prefix']").val());
            var ten = $.trim($("input[name='TenVungMien']").val());
            var moTa = $.trim($("textarea[name='MoTa']").val());
            
            if ($('#IsActive').prop('checked')) {
                $("#IsActive").val(true);
            }
            else {
                $("#IsActive").val(false);
            }

            //kiem tra ma
            if (Common.Empty(ma)) {
                $(".thongbaomaVungMien").html("Nhập mã !!!");
                $(".thongbaomaVungMien").show();
                $(".thongbaomaVungMien").addClass("show");
                $("input[name='Ma']").focus();
                return false;
            }
            else {
                if (ma.length > 0) {
                    ma = Common.RemoveScript(ma);
                    if (ma === '') {
                        $(".thongbaomaVungMien").html("Mã " + that.Message.MaJavaCript);
                        $(".thongbaomaVungMien").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                    if (ma.length > 101) {
                        $(".thongbaomaVungMien").html(that.Message.LengthMa);
                        $(".thongbaomaVungMien").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ma) || (/<[/s/S][a-z][\s\S]*>/i.test(ma))) {
                $(".thongbaomaVungMien").html("Ma " + that.Message.MaHtml);
                $(".thongbaomaVungMien").show();
                $("input[name='Ma']").focus();
                return false;
            }

            $(".thongbaomaVungMien").hide();
            
            //kiem tra prefix
            if (Common.Empty(tiento)) {
                $(".thongbaotientoVungMien").html("Nhập tiền tố!!!");
                $(".thongbaotientoVungMien").show();
                $(".thongbaotientoVungMien").addClass("show");
                $("input[name='Prefix']").focus();
                return false;
            } else {
                if (tiento.length > 0) {
                    tiento = Common.RemoveScript(tiento);
                    if (tiento === '') {
                        $(".thongbaotientoVungMien").html("Tiền tố " + that.Message.MaJavaCript);
                        $(".thongbaotientoVungMien").addClass("show");
                        $("input[name='Prefix']").focus();
                        return false;
                    }
                    if (tiento.length > 5) {
                        $(".thongbaotientoVungMien").html(that.Message.LengthPrefix);
                        $(".thongbaotientoVungMien").addClass("show");
                        $("input[name='Prefix']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(tiento) || (/<[/s/S][a-z][\s\S]*>/i.test(tiento))) {
                $(".thongbaotientoVungMien").html("Tiền tố " + that.Message.MaHtml);
                $(".thongbaotientoVungMien").show();
                $("input[name='Prefix']").focus();
                return false;
            }

            $(".thongbaotientoVungMien").hide();

            if (Common.Empty(ten)) {
                $(".thongbaotenVungMien").html(that.Message.Ten);
                $(".thongbaotenVungMien").show();
                $(".thongbaotenVungMien").addClass("show");
                $("input[name='TenVungMien']").focus();
                return false;
            }
            else {
                if (ten.length > 0) {
                    ten = Common.RemoveScript(ten);
                    if (ten === '') {
                        $(".thongbaotenVungMien").html("Tên " + that.Message.MaJavaCript);
                        $(".thongbaotenVungMien").addClass("show");
                        $("input[name='TenVungMien']").focus();
                        return false;
                    }
                    if (ten.length > 301) {
                        $(".thongbaotenVungMien").html(that.Message.LengthTen);
                        $(".thongbaotenVungMien").addClass("show");
                        $("input[name='TenVungMien']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ten) || (/<[/s/S][a-z][\s\S]*>/i.test(ten))) {
                $(".thongbaotenVungMien").html("Tên " + that.Message.MaHtml);
                $(".thongbaotenVungMien").show();
                $("input[name='TenVungMien']").focus();
                return false;
            }
            $(".thongbaotenVungMien").hide();
            if (!Common.Empty(moTa)) {
                if (moTa.length > 0) {
                    moTa = Common.RemoveScript(moTa);
                    if (moTa === '') {
                        $(".thongbaomotaVungMien").html("Mô tả " + that.Message.MaJavaCript);
                        $(".thongbaomotaVungMien").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                    if (moTa.length > 501) {
                        $(".thongbaomotaVungMien").html(that.Message.LengthTen);
                        $(".thongbaomotaVungMien").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                }
                if (/<[a-z][\s\S]*>/i.test(moTa) || (/<[/s/S][a-z][\s\S]*>/i.test(moTa))) {
                    $(".thongbaomotaVungMien").html("Mô tả " + that.Message.MaHtml);
                    $(".thongbaomotaVungMien").show();
                    $("textarea[name='MoTa']").focus();
                    return false;
                }
            }
            $(".thongbaomotaVungMien").hide();
            var data = $("#form-update").serialize();

            Common.Ajax({
                type: "POST",
                async: false,
                cache: false,
                url: Common.DM_VungMien.Url.Create,
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
                        Common.DM_VungMienThemMoi.HideDialog();
                        Common.DM_VungMien.SetPage(1);
                        Common.DM_VungMien.SubmitForm();
                    }
                }
            });
            return false;
        });
    },
    ShowDialog: function () {
        $("#dialogAddVungMien").modal("show");
        Common.DM_VungMienThemMoi.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogAddVungMien").modal("hide");
    }
};

// create DM_VungMienThemMoi Object
var DM_VungMienThemMoi = DM_VungMienThemMoi;
DM_VungMienThemMoi.constructor = DM_VungMienThemMoi;
