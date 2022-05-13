
var DM_CoQuanCapThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};

DM_CoQuanCapThemMoi.prototype = {
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
                $(".thongbaomaCoQuanCap").html("Nhập mã!!!");
                $(".thongbaomaCoQuanCap").show();
                $(".thongbaomaCoQuanCap").addClass("show");
                $("input[name='Ma']").focus();
                return false;
            }
            else {
                if (ma.length > 0) {
                    ma = Common.RemoveScript(ma);
                    if (ma === '') {
                        $(".thongbaomaCoQuanCap").html("Mã " + that.Message.MaJavaCript);
                        $(".thongbaomaCoQuanCap").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                    if (ma.length > 101) {
                        $(".thongbaomaCoQuanCap").html(that.Message.LengthMa);
                        $(".thongbaomaCoQuanCap").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ma) || (/<[/s/S][a-z][\s\S]*>/i.test(ma))) {
                $(".thongbaomaCoQuanCap").html("Ma " + that.Message.MaHtml);
                $(".thongbaomaCoQuanCap").show();
                $("input[name='Ma']").focus();
                return false;
            }

            $(".thongbaomaCoQuanCap").hide();

            if (Common.Empty(ten)) {
                $(".thongbaotenCoQuanCap").html(that.Message.Ten);
                $(".thongbaotenCoQuanCap").show();
                $(".thongbaotenCoQuanCap").addClass("show");
                $("input[name='Ten']").focus();
                return false;
            }
            else {
                if (ten.length > 0) {
                    ten = Common.RemoveScript(ten);
                    if (ten === '') {
                        $(".thongbaotenCoQuanCap").html("Tên " + that.Message.MaJavaCript);
                        $(".thongbaotenCoQuanCap").addClass("show");
                        $("input[name='Ten']").focus();
                        return false;
                    }
                    if (ten.length > 301) {
                        $(".thongbaotenCoQuanCap").html(that.Message.LengthTen);
                        $(".thongbaotenCoQuanCap").addClass("show");
                        $("input[name='Ten']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ten) || (/<[/s/S][a-z][\s\S]*>/i.test(ten))) {
                $(".thongbaotenCoQuanCap").html("Tên " + that.Message.MaHtml);
                $(".thongbaotenCoQuanCap").show();
                $("input[name='Ten']").focus();
                return false;
            }
            $(".thongbaotenCoQuanCap").hide();
            if (!Common.Empty(moTa)) {
                if (moTa.length > 0) {
                    moTa = Common.RemoveScript(moTa);
                    if (moTa === '') {
                        $(".thongbaomotaCoQuanCap").html("Mô tả " + that.Message.MaJavaCript);
                        $(".thongbaomotaCoQuanCap").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                    if (moTa.length > 501) {
                        $(".thongbaomotaCoQuanCap").html(that.Message.LengthTen);
                        $(".thongbaomotaCoQuanCap").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                }
                if (/<[a-z][\s\S]*>/i.test(moTa) || (/<[/s/S][a-z][\s\S]*>/i.test(moTa))) {
                    $(".thongbaomotaCoQuanCap").html("Mô tả " + that.Message.MaHtml);
                    $(".thongbaomotaCoQuanCap").show();
                    $("textarea[name='MoTa']").focus();
                    return false;
                }
            }
            $(".thongbaomotaCoQuanCap").hide();
            var data = $("#form-update").serialize();

            Common.Ajax({
                type: "POST",
                async: false,
                cache: false,
                url: Common.DM_CoQuanCap.Url.Create,
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
                        Common.DM_CoQuanCapThemMoi.HideDialog();
                        Common.DM_CoQuanCap.SetPage(1);
                        Common.DM_CoQuanCap.SubmitForm();
                    }
                }
            });
            return false;
        });
    },
    ShowDialog: function () {
        $("#dialogAddCoQuanCap").modal("show");
        Common.DM_CoQuanCapThemMoi.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogAddCoQuanCap").modal("hide");
    }
};

// create DM_CoQuanCapThemMoi Object
var DM_CoQuanCapThemMoi = DM_CoQuanCapThemMoi;
DM_CoQuanCapThemMoi.constructor = DM_CoQuanCapThemMoi;
