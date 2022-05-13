
var DM_LoaiSoThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};

DM_LoaiSoThemMoi.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;

        $("#btnLuuDM").unbind("click").click(function () {
            var ten = $.trim($("input[name='Ten']").val());
            var soHienTai = $.trim($("input[name='SoHienTai']").val());
            var nam = $.trim($("input[name='Nam']").val());
            var loaiNghiepVu = $.trim($("select[name='LoaiNghiepVuID']").val());
            var prefix = $.trim($("input[name='Prefix']").val());
            
            if ($('#check-ResetTheoNam').prop('checked')) {
                $("#check-ResetTheoNam").val(true);
            } else {
                $("#check-ResetTheoNam").val(false);
            }

            if ($('#check-TuTang').prop('checked')) {
                $("#check-TuTang").val(true);
            } else {
                $("#check-TuTang").val(false);
            }

            if (Common.Empty(ten)) {
                $(".thongbaotenLoaiSo").html(that.Message.Ten);
                $(".thongbaotenLoaiSo").show();
                $(".thongbaotenLoaiSo").addClass("show");
                $("input[name='Ten']").focus();
                return false;
            } else {
                if (ten.length > 0) {
                    ten = Common.RemoveScript(ten);
                    if (ten === '') {
                        $(".thongbaotenLoaiSo").html("Tên " + that.Message.MaJavaCript);
                        $(".thongbaotenLoaiSo").addClass("show");
                        $("input[name='Ten']").focus();
                        return false;
                    }
                    if (ten.length > 301) {
                        $(".thongbaotenLoaiSo").html(that.Message.LengthTen);
                        $(".thongbaotenLoaiSo").addClass("show");
                        $("input[name='Ten']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ten) || (/<[/s/S][a-z][\s\S]*>/i.test(ten))) {
                $(".thongbaotenLoaiSo").html("Tên " + that.Message.MaHtml);
                $(".thongbaotenLoaiSo").show();
                $("input[name='Ten']").focus();
                return false;
            }
            $(".thongbaotenLoaiSo").hide();

            // check so hien tai
            if (!Common.Empty(soHienTai)) {
                if (soHienTai.length > 0) {
                    if (isNaN(Number(soHienTai))) {
                        $(".thongbaosohientaiLoaiSo").html("Nhập một số !!!");
                        $(".thongbaosohientaiLoaiSo").show();
                        return false;
                    }
                }
            }
            $(".thongbaosohientaiLoaiSo").hide();

            // check nam
            if (!Common.Empty(nam)) {
                if (soHienTai.length > 0) {
                    if (isNaN(Number(nam))) {
                        $(".thongbaonamLoaiSo").html("Nhập một số !!!");
                        $(".thongbaonamLoaiSo").show();
                        return false;
                    }
                }
            }
            $(".thongbaonamLoaiSo").hide();

            if (!Common.Empty(prefix)) {
                if (prefix.length > 0) {
                    prefix = Common.RemoveScript(prefix);
                    if (prefix === '') {
                        $(".thongbaotientoLoaiSo").html("Tiền tố " + that.Message.MaJavaCript);
                        $(".thongbaotientoLoaiSo").addClass("show");
                        $("input[name='Prefix']").focus();
                        return false;
                    }
                    if (prefix.length > 501) {
                        $(".thongbaotientoLoaiSo").html(that.Message.LengthTen);
                        $(".thongbaotientoLoaiSo").addClass("show");
                        $("input[name='Prefix']").focus();
                        return false;
                    }
                }
                if (/<[a-z][\s\S]*>/i.test(prefix) || (/<[/s/S][a-z][\s\S]*>/i.test(prefix))) {
                    $(".thongbaotientoLoaiSo").html("Tiền tố " + that.Message.MaHtml);
                    $(".thongbaotientoLoaiSo").show();
                    $("input[name='Prefix']").focus();
                    return false;
                }
            }
            $(".thongbaotientoLoaiSo").hide();

            //if (Common.Empty(loaiNghiepVu)) {
            //    $(".thongbaoloainghiepvuLoaiSo").html("Chọn loại nghiệp vụ");
            //    $(".thongbaoloainghiepvuLoaiSo").show();
            //    return false;
            //}
            //$(".thongbaoloainghiepvuLoaiSo").hide();

            var data = $("#form-update").serialize();

            Common.Ajax({
                type: "POST",
                async: false,
                cache: false,
                url: Common.DM_LoaiSo.Url.Create,
                dataType: 'JSON',
                data: data
            }, function (result) {
                if (!Common.Empty(result)) {
                    if (result.checkMa == true && result.status == false) {
                        $.notify({
                            // options
                            message: 'Loại hình nghiệp vụ đã tồn tại. Vui lòng chọn loại hình khác.!!!'
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
                        Common.DM_LoaiSoThemMoi.HideDialog();
                        Common.DM_LoaiSo.SetPage(1);
                        Common.DM_LoaiSo.SubmitForm();
                    }
                }
            });
            return false;
        });
    },
    ShowDialog: function () {
        $("#dialogAddLoaiSo").modal("show");
        Common.DM_LoaiSoThemMoi.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogAddLoaiSo").modal("hide");
    }
};

// create DM_LoaiSoThemMoi Object
var DM_LoaiSoThemMoi = DM_LoaiSoThemMoi;
DM_LoaiSoThemMoi.constructor = DM_LoaiSoThemMoi;
