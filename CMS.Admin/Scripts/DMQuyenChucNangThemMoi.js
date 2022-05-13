var DMQuyenChucNangThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
DMQuyenChucNangThemMoi.prototype = {
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
        $("input[name='IsDefault']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });
        $("#Controller").unbind().change(function () {
            var val = $("#Controller option:selected").val();
            $("#Ten").val("");
            Common.Ajax({
                type: "POST",
                async: false,
                cache: false,
                url: Common.DMQuyenChucNang.Url.CBOAction,
                dataType: 'JSON',
                data: { id: val }
            }, function (result) {
                if (!Common.Empty(result)) {
                    var html = Common.DMQuyenChucNang.TemplateHtmlCBO("Action", "Action", "Action", result.data);
                    $(".divActionFormResult").html(html);
                    that.RegisterEvent();
                }
            });
        });
        $("#Action").unbind().change(function () {
            var controller = $("#Controller option:selected").val();
            var action = $("#Action option:selected").val();
            $("#Ten").val(controller + '_' + action);
        });

        $("#btnLuuDV").unbind("click").click(function () {
            var ma = $.trim($("input[name='Ma']").val());
            var ten = $.trim($("input[name='Ten']").val());
            var tenht = $.trim($("input[name='TenHienThi']").val());
            var moTa = $.trim($("textarea[name='MoTa']").val());
            var quyenId = $.trim($("#dialogAddDanhMuc select[name='QuyenID']").val());
            var controllerId = $.trim($("#dialogAddDanhMuc select[name='Controller']").val());
            var actionId = $.trim($("#dialogAddDanhMuc select[name='Action']").val());
            if ($('#CongKhai').prop('checked')) {
                $("#CongKhai").val(true);
            }
            else {
                $("#CongKhai").val(false);
            }
            if ($('#IsDefault').prop('checked')) {
                $("#IsDefault").val(true);
            }
            else {
                $("#IsDefault").val(false);
            }
            //kiem tra ma
            if (Common.Empty(ma)) {
                $(".thongbaoma").html(that.Message.Ma);
                $(".thongbaoma").show();
                $(".thongbaoma").addClass("show");
                $("input[name='Ma']").focus();
                return false;
            }
            else {
                if (ma.length > 0) {
                    ma = Common.RemoveScript(ma);
                    if (ma === '') {
                        $(".thongbaoma").html("Mã " + that.Message.MaJavaCript);
                        $(".thongbaoma").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                    if (ma.length > 101) {
                        $(".thongbaoma").html(that.Message.LengthMa);
                        $(".thongbaoma").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ma) || (/<[/s/S][a-z][\s\S]*>/i.test(ma))) {
                $(".thongbaoma").html("Mã " + that.Message.MaHtml);
                $(".thongbaoma").show();
                $("input[name='Ma']").focus();
                return false;
            }

            $(".thongbaoma").hide();
            //kiem tra co chon quyen
            if (Common.Empty(quyenId)) {
                $(".thongbaoQuyen").html(that.Message.Quyen);
                $(".thongbaoQuyen").show();
                $(".thongbaoQuyen").addClass("show");
                $("select[name='QuyenID']").focus();
                return false;
            }
            $(".thongbaoQuyen").hide();
            //kiem tra ten ht
            if (Common.Empty(tenht)) {
                $(".thongbaoTenHT").html(that.Message.Ten);
                $(".thongbaoTenHT").show();
                $(".thongbaoTenHT").addClass("show");
                $("input[name='TenHienThi']").focus();
                return false;
            }
            else {
                if (tenht.length > 0) {
                    tenht = Common.RemoveScript(tenht);
                    if (tenht === '') {
                        $(".thongbaoTenHT").html("Tên " + that.Message.MaJavaCript);
                        $(".thongbaoTenHT").addClass("show");
                        $("input[name='TenHienThi']").focus();
                        return false;
                    }
                    if (tenht.length > 301) {
                        $(".thongbaoTenHT").html(that.Message.LengthTen);
                        $(".thongbaoTenHT").addClass("show");
                        $("input[name='TenHienThi']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(tenht) || (/<[/s/S][a-z][\s\S]*>/i.test(tenht))) {
                $(".thongbaoTenHT").html("Tên " + that.Message.MaHtml);
                $(".thongbaoTenHT").show();
                $("input[name='TenHienThi']").focus();
                return false;
            }
            $(".thongbaoTenHT").hide();

            if (Common.Empty(controllerId)) {
                $(".thongbaoController").html("Vui lòng chọn controller");
                $(".thongbaoController").show();
                $(".thongbaoController").addClass("show");
                $("select[name='Controller']").focus();
                return false;
            }
            $(".thongbaoController").hide();
            if (Common.Empty(actionId)) {
                $(".thongbaoAction").html("Vui lòng chọn action");
                $(".thongbaoAction").show();
                $(".thongbaoAction").addClass("show");
                $("select[name='Action']").focus();
                return false;
            }
            $(".thongbaoAction").hide();
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
                    if (moTa.length > 501) {
                        $(".thongbaoMoTa").html(that.Message.LengthTen);
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
            //kiem tra ten
            if (Common.Empty(ten)) {
                $(".thongbaoTen").html(that.Message.Ten);
                $(".thongbaoTen").show();
                $(".thongbaoTen").addClass("show");
                $("input[name='Ten']").focus();
                return false;
            }
            else {
                if (ten.length > 0) {
                    ten = Common.RemoveScript(ten);
                    if (ten === '') {
                        $(".thongbaoTen").html("Tên " + that.Message.MaJavaCript);
                        $(".thongbaoTen").addClass("show");
                        $("input[name='Ten']").focus();
                        return false;
                    }
                    if (ten.length > 301) {
                        $(".thongbaoTen").html(that.Message.LengthTen);
                        $(".thongbaoTen").addClass("show");
                        $("input[name='Ten']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ten) || (/<[/s/S][a-z][\s\S]*>/i.test(ten))) {
                $(".thongbaoTen").html("Tên " + that.Message.MaHtml);
                $(".thongbaoTen").show();
                $("input[name='Ten']").focus();
                return false;
            }
            $(".thongbaoTen").hide();

            var data = $("#form-update").serialize();
            Common.ShowLoading(true);
            Common.Ajax({
                type: "POST",
                async: false,
                cache: false,
                url: Common.DMQuyenChucNang.Url.Create,
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
                        Common.DMQuyenChucNangThemMoi.HideDialog();
                        Common.DMQuyenChucNang.SetPage(1);
                        Common.DMQuyenChucNang.SubmitForm();
                    }
                }
            });
            return false;
        });


    },
    ShowDialog: function () {
        $("#dialogAddDanhMuc").modal("show");
        Common.DMQuyenChucNangThemMoi.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogAddDanhMuc").modal("hide");
    }
};
// create DMQuyenChucNangThemMoi Object
var DMQuyenChucNangThemMoi = DMQuyenChucNangThemMoi;
DMQuyenChucNangThemMoi.constructor = DMQuyenChucNangThemMoi;
