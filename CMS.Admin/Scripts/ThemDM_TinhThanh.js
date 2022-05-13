var ThemDM_TinhThanh = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
ThemDM_TinhThanh.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;
        Common.SetInputFilter(document.getElementById("ThuTuHienThi"), function (value) {
            return /^\d*$/.test(value);
        });
        $("input[name='CongKhai']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });

        $("#btnLuuDV").unbind("click").click(function () {
            var ma = $.trim($("input[name='Ma']").val());
            var malienthong = $.trim($("input[name='MaLienThong']").val());
            var ten = $.trim($("input[name='Ten']").val());
            var quocGiaID = $.trim($("#dialogAddDanhMuc select[name='QuocGiaID']").val());
            var moTa = $.trim($("textarea[name='MoTa']").val());
            if ($('#CongKhai').prop('checked')) {
                $("#CongKhai").val(true);
            }
            else {
                $("#CongKhai").val(false);
            }
           
            //kiem tra ma
            if (Common.Empty(ma)) {
                $(".thongbaoma").html(that.Message.Ma);
                $(".thongbaoma").show();
                $(".thongbaoma").addClass("show");
                $("input[name='MaTinhThanh']").focus();
                return false;
            }
            else {
                if (ma.length > 0) {
                    ma = Common.RemoveScript(ma);
                    if (ma === '') {
                        $(".thongbaoma").html("Mã " + that.Message.MaJavaCript);
                        $(".thongbaoma").addClass("show");
                        $("input[name='MaTinhThanh']").focus();
                        return false;
                    }
                    if (ma.length > 51) {
                        $(".thongbaoma").html(that.Message.LengthMa);
                        $(".thongbaoma").addClass("show");
                        $("input[name='MaTinhThanh']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ma) || (/<[/s/S][a-z][\s\S]*>/i.test(ma))) {
                $(".thongbaoma").html("Mã " + that.Message.MaHtml);
                $(".thongbaoma").show();
                $("input[name='MaTinhThanh']").focus();
                return false;
            }

            $(".thongbaoma").hide();

            //kiem tra ten
            if (Common.Empty(ten)) {
                $(".thongbaoTen").html(that.Message.Ten);
                $(".thongbaoTen").show();
                $(".thongbaoTen").addClass("show");
                $("input[name='TenTinhThanh']").focus();
                return false;
            }
            else {
                if (ten.length > 0) {
                    ten = Common.RemoveScript(ten);
                    if (ten === '') {
                        $(".thongbaoTen").html("Tên " + that.Message.MaJavaCript);
                        $(".thongbaoTen").addClass("show");
                        $("input[name='TenTinhThanh']").focus();
                        return false;
                    }
                    if (ten.length > 501) {
                        $(".thongbaoTen").html(that.Message.LengthTen);
                        $(".thongbaoTen").addClass("show");
                        $("input[name='TenTinhThanh']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ten) || (/<[/s/S][a-z][\s\S]*>/i.test(ten))) {
                $(".thongbaoTen").html("Tên " + that.Message.MaHtml);
                $(".thongbaoTen").show();
                $("input[name='TenTinhThanh']").focus();
                return false;
            }
            $(".thongbaoTen").hide();
            //mo ta
            if (!Common.Empty(moTa)) {
                if (moTa.length > 0) {
                    moTa = Common.RemoveScript(moTa);
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
            //kiem tra co chon quoc gia id
            if (Common.Empty(quocGiaID)) {
                $(".thongbaoQuocGia").html(that.Message.QuocGia);
                $(".thongbaoQuocGia").show();
                $(".thongbaoQuocGia").addClass("show");
                $("select[name='QuocGiaID']").focus();
                return false;
            }
            $(".thongbaoQuocGia").hide();

            var data = $("#form-update").serialize();
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.DMTinhThanh.Url.Update,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: data
                }, function (result) {
                    Common.ShowLoading(false);
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
                            if ($.trim($("#ID").val()) > 0) {
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
                            Common.ThemDM_TinhThanh.HideDialog();
                            Common.DMTinhThanh.SetPage(1);
                            Common.DMTinhThanh.SubmitForm();
                        }
                    }
                }
            );
            return false;
        });
    },
    ShowDialog: function () {
        $("#dialogAddDanhMuc").modal("show")
        Common.ThemDM_TinhThanh.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogAddDanhMuc").modal("hide");
    }
};
// create ThemDM_TinhThanh Object
var ThemDM_TinhThanh = ThemDM_TinhThanh;
ThemDM_TinhThanh.constructor = ThemDM_TinhThanh;
