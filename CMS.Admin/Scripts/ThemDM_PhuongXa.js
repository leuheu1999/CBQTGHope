var ThemDM_PhuongXa = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
ThemDM_PhuongXa.prototype = {
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

        $("#TinhThanhID").unbind("change").change(function () {
            var val = $("#TinhThanhID option:selected").val();
            Common.ShowLoading(true);
            Common.ThemDM_PhuongXa.AjaxCBOQuanHuyen(val);
            return false;
        });

        $("#btnLuuDV").unbind("click").click(function () {
            var ma = $.trim($("input[name='Ma']").val());
            var malienthong = $.trim($("input[name='MaLienThong']").val());
            var ten = $.trim($("input[name='Ten']").val());
            var tinhThanhId = $.trim($('#dialogAddDanhMuc select[name="TinhThanhID"]').val());
            var quanHuyenId = $.trim($('#dialogAddDanhMuc select[name="QuanHuyenID"]').val());

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
                    if (ma.length > 51) {
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
                    if (ten.length > 501) {
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

            //kiem tra co chon tinh thanh id
            if (Common.Empty(tinhThanhId)) {
                $(".thongbaoTinhThanh").html(that.Message.TinhThanh);
                $(".thongbaoTinhThanh").show();
                $(".thongbaoTinhThanh").addClass("show");
                $('#dialogAddDanhMuc select[id=TinhThanhID]').focus();
                return false;
            }
            $(".thongbaoTinhThanh").hide();

            //kiem tra co chon quan huyện
            if (Common.Empty(quanHuyenId) || parseInt(quanHuyenId) === 0) {
                $(".thongbaoQuanHuyen").html(that.Message.QuanHuyen);
                $(".thongbaoQuanHuyen").show();
                $(".thongbaoQuanHuyen").addClass("show");
                $('#dialogAddDanhMuc select[id=QuanHuyenID]').focus();
                return false;
            }
            $(".thongbaoQuanHuyen").hide();
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


            var data = $("#form-update").serialize();
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.DMPhuongXa.Url.ThemMoiDMPhuongXa,
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
                                message: 'Mã ' + ma + ' đã tồn tại. Vui lòng nhập mã khác.!!!'
                            }, {
                                // settings
                                delay: 1000, timer: 1000, type: 'danger'
                            });
                        }
                        else if (result.status == true) {
                            if ($.trim($("#ID").val()) > 0) {
                                $.notify({
                                    // options
                                    message: 'Cập nhật thành công.!!!'
                                }, {
                                    // settings
                                    delay: 1000, timer: 1000, type: 'success'
                                });
                            }
                            else {
                                $.notify({
                                    // options
                                    message: 'Thêm mới thành công.!!!'
                                }, {
                                    // settings
                                    delay: 1000, timer: 1000, type: 'success'
                                });
                            }
                            Common.ThemDM_PhuongXa.HideDialog();
                            Common.DMPhuongXa.SetPage(1);
                            Common.DMPhuongXa.SubmitForm();
                        }
                    }
                }
            );
            return false;
        });
    },
    AjaxCBOQuanHuyen: function (id) {
        Common.Ajax({
            url: Common.DMPhuongXa.Url.CBOQuanHuyenById,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { id: id }
            },function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    var html = Common.ThemDM_PhuongXa.TemplateHtmlCBO("Quận huyện", "QuanHuyenID", "QuanHuyenID", result.data);
                    $('#dialogAddDanhMuc select[id=QuanHuyenID]').empty().append(html);
                    Common.ThemDM_PhuongXa.RegisterEvent();
                }
            }
        );
    },
    ShowDialog: function () {
        $("#dialogAddDanhMuc").modal("show")
        Common.ThemDM_PhuongXa.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogAddDanhMuc").modal("hide");
    },
    TemplateHtmlCBO(nameHienThi, idControl, nameControl, dataObj) {
       // var html = "<label class='font-weight-bold'>" + nameHienThi + " <span class='text-danger'>(*)</span></label>";
        //html += "<select class='form-control form-control-sm select-share' id='" + idControl + "' name='" + nameControl + "'>";
        var html = "<option value='0'>--- Chọn quận huyện ---</option>";
        for (var i = 0; i < dataObj.length; i++) {
            html += "<option value='" + dataObj[i].Value + "'>" + dataObj[i].Text + "</option>";
        }
        html += "</select>";
        return html;
    },
};
// create ThemDM_PhuongXa Object
var ThemDM_PhuongXa = ThemDM_PhuongXa;
ThemDM_PhuongXa.constructor = ThemDM_PhuongXa;
