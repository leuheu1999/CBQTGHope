var ThemDM_QuocGia = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
ThemDM_QuocGia.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;

        $("input[name='Used']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });
        Common.SetInputFilter(document.getElementById("ThuTuHienThi"), function (value) {
            return /^\d*$/.test(value);
        });
        $("#btnLuuDV").unbind("click").click(function () {
            var maQuocGia = $.trim($("input[name='MaQuocGia']").val());
            var tenQuocGia = $.trim($("input[name='TenQuocGia']").val());
            var moTa = $.trim($("textarea[name='MoTa']").val());
            if ($('#Used').prop('checked')) {
                $("#Used").val(true);
            }
            else {
                $("#Used").val(false);
            }
            //kiem tra ma
            if (Common.Empty(maQuocGia)) {
                $(".thongbaoma").html(that.Message.Ma);
                $(".thongbaoma").show();
                $(".thongbaoma").addClass("show");
                $("input[name='MaQuocGia']").focus();
                return false;
            }
            else {
                if (maQuocGia.length > 0) {
                    //maQuocGia = Common.RemoveScript(maQuocGia);
                    if (maQuocGia === '') {
                        $(".thongbaoma").html("Mã quốc gia " + that.Message.MaJavaCript);
                        $(".thongbaoma").show();
                        $(".thongbaoma").addClass("show");
                        $("input[name='MaQuocGia']").focus();
                        return false;
                    }
                    if (maQuocGia.length > 51) {
                        $(".thongbaoma").html(that.Message.LengthMa);
                        $(".thongbaoma").show();
                        $(".thongbaoma").addClass("show");
                        $("input[name='MaQuocGia']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(maQuocGia) || (/<[/s/S][a-z][\s\S]*>/i.test(maQuocGia))) {
                $(".thongbaoma").html("Mã quốc gia " + that.Message.MaHtml);
                $(".thongbaoma").show();
                $("input[name='MaQuocGia']").focus();
                return false;
            }

            $(".thongbaoma").hide();

            //kiem tra ten
            if (Common.Empty(tenQuocGia)) {
                $(".thongbaoTen").html(that.Message.Ten);
                $(".thongbaoTen").show();
                $(".thongbaoTen").addClass("show");
                $("input[name='TenQuocGia']").focus();
                return false;
            }
            else {
                if (tenQuocGia.length > 0) {
                    //tenQuocGia = Common.RemoveScript(tenQuocGia);
                    if (tenQuocGia === '') {
                        $(".thongbaoTen").html("Tên quốc gia " + that.Message.MaJavaCript);
                        $(".thongbaoTen").addClass("show");
                        $("input[name='TenQuocGia']").focus();
                        return false;
                    }
                    if (tenQuocGia.length > 501) {
                        $(".thongbaoTen").html(that.Message.LengthTen);
                        $(".thongbaoTen").addClass("show");
                        $("input[name='TenQuocGia']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(tenQuocGia) || (/<[/s/S][a-z][\s\S]*>/i.test(tenQuocGia))) {
                $(".thongbaoTen").html("Tên quốc gia " + that.Message.MaHtml);
                $(".thongbaoTen").show();
                $("input[name='TenQuocGia']").focus();
                return false;
            }
            $(".thongbaoTen").hide();
            //mo ta
            if (!Common.Empty(moTa)) {
                if (moTa.length > 0) {
                    //moTa = Common.RemoveScript(moTa);
                    if (moTa === '') {
                        $(".thongbaoMoTa").html("Mô tả " + that.Message.MaJavaCript);
                        $(".thongbaoMoTa").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                    if (moTa.length > 4001) {
                        $(".thongbaoMoTa").html(that.Message.LengthMota);
                        $(".thongbaoMoTa").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                }
                if (/<[a-z][\s\S]*>/i.test(moTa) || (/<[/s/S][a-z][\s\S]*>/i.test(moTa))) {
                    $(".thongbaoMoTa").html("Mô tả " + that.Message.MaHtml);
                    $(".thongbaoMoTa").show();
                    $("textarea[name='MoTa']").focus();
                    return false;
                }
            }
            $(".thongbaoMoTa").hide();
            var data = $("#form-update").serialize();
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.DMQuocGia.Url.Update,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: data
                },function (result) {
                    Common.ShowLoading(false);
                    if (!Common.Empty(result)) {
                        if (result.checkMa == true && result.status == false) {
                            $.notify({
                                // options
                                message: 'Mã ' + maQuocGia + ' đã tồn tại. Vui lòng nhập mã khác.!!!'
                            }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                        }
                        else if (result.status == true) {
                            if ($.trim($("#QuocGiaID").val()) > 0) {
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
                            Common.ThemDM_QuocGia.HideDialog();
                            Common.DMQuocGia.SetPage(1);
                            Common.DMQuocGia.SubmitForm();
                        }
                    }
                }
            );
            return false;
        });
    },
    ShowDialog: function () {
        $("#dialogAddDmQuocGia").modal("show")
        Common.ThemDM_QuocGia.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogAddDmQuocGia").modal("hide");
    }
};
// create ThemDM_QuocGia Object
var ThemDM_QuocGia = ThemDM_QuocGia;
ThemDM_QuocGia.constructor = ThemDM_QuocGia;
