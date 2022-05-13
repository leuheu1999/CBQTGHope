
var DM_LoaiHinhTacPhamThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};

DM_LoaiHinhTacPhamThemMoi.prototype = {
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
            var loaiHinhDKId = $("select[name='LoaiHinhDangKyId'] option:selected").val() || 0;
            var nhom = $.trim($("input[name='Nhom']").val());
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
                $(".thongbaomaLoaiHinhTacPham").html("Nhập mã !!!");
                $(".thongbaomaLoaiHinhTacPham").show();
                $(".thongbaomaLoaiHinhTacPham").addClass("show");
                $("input[name='Ma']").focus();
                return false;
            }
            else {
                if (ma.length > 0) {
                    ma = Common.RemoveScript(ma);
                    if (ma === '') {
                        $(".thongbaomaLoaiHinhTacPham").html("Mã " + that.Message.MaJavaCript);
                        $(".thongbaomaLoaiHinhTacPham").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                    if (ma.length > 101) {
                        $(".thongbaomaLoaiHinhTacPham").html(that.Message.LengthMa);
                        $(".thongbaomaLoaiHinhTacPham").addClass("show");
                        $("input[name='Ma']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ma) || (/<[/s/S][a-z][\s\S]*>/i.test(ma))) {
                $(".thongbaomaLoaiHinhTacPham").html("Ma " + that.Message.MaHtml);
                $(".thongbaomaLoaiHinhTacPham").show();
                $("input[name='Ma']").focus();
                return false;
            }

            $(".thongbaomaLoaiHinhTacPham").hide();

            //kiem tra loại hình dk
            if (loaiHinhDKId == 0) {
                $(".thongbaoloaihinhdkLoaiHinhTacPham").html("Chọn loại hình đăng ký !!!");
                $(".thongbaoloaihinhdkLoaiHinhTacPham").show();
                $(".thongbaoloaihinhdkLoaiHinhTacPham").addClass("show");
                return false;
            }

            $(".thongbaoloaihinhdkLoaiHinhTacPham").hide();

            //kiem tra nhóm
            if (Common.Empty(nhom)) {
                $(".thongbaonhomLoaiHinhTacPham").html("Nhập nhóm !!!");
                $(".thongbaonhomLoaiHinhTacPham").show();
                $(".thongbaonhomLoaiHinhTacPham").addClass("show");
                $("input[name='Nhom']").focus();
                return false;
            }
            else {
                if (nhom.length > 0) {
                    nhom = Common.RemoveScript(nhom);
                    if (nhom === '') {
                        $(".thongbaonhomLoaiHinhTacPham").html("Mã " + that.Message.MaJavaCript);
                        $(".thongbaonhomLoaiHinhTacPham").addClass("show");
                        $("input[name='Nhom']").focus();
                        return false;
                    }
                    if (nhom.length > 101) {
                        $(".thongbaonhomLoaiHinhTacPham").html(that.Message.LengthNhom);
                        $(".thongbaonhomLoaiHinhTacPham").addClass("show");
                        $("input[name='Nhom']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(nhom) || (/<[/s/S][a-z][\s\S]*>/i.test(nhom))) {
                $(".thongbaonhomLoaiHinhTacPham").html("Nhom " + that.Message.MaHtml);
                $(".thongbaonhomLoaiHinhTacPham").show();
                $("input[name='Nhom']").focus();
                return false;
            }

            $(".thongbaonhomLoaiHinhTacPham").hide();

            //kiem tra tên
            if (Common.Empty(ten)) {
                $(".thongbaotenLoaiHinhTacPham").html(that.Message.Ten);
                $(".thongbaotenLoaiHinhTacPham").show();
                $(".thongbaotenLoaiHinhTacPham").addClass("show");
                $("input[name='TenLoaiHinh']").focus();
                return false;
            }
            else {
                if (ten.length > 0) {
                    ten = Common.RemoveScript(ten);
                    if (ten === '') {
                        $(".thongbaotenLoaiHinhTacPham").html("Tên " + that.Message.MaJavaCript);
                        $(".thongbaotenLoaiHinhTacPham").addClass("show");
                        $("input[name='TenLoaiHinh']").focus();
                        return false;
                    }
                    if (ten.length > 301) {
                        $(".thongbaotenLoaiHinhTacPham").html(that.Message.LengthTen);
                        $(".thongbaotenLoaiHinhTacPham").addClass("show");
                        $("input[name='TenLoaiHinh']").focus();
                        return false;
                    }
                }
            }
            if (/<[a-z][\s\S]*>/i.test(ten) || (/<[/s/S][a-z][\s\S]*>/i.test(ten))) {
                $(".thongbaotenLoaiHinhTacPham").html("Tên " + that.Message.MaHtml);
                $(".thongbaotenLoaiHinhTacPham").show();
                $("input[name='TenLoaiHinh']").focus();
                return false;
            }
            $(".thongbaotenLoaiHinhTacPham").hide();
            if (!Common.Empty(moTa)) {
                if (moTa.length > 0) {
                    moTa = Common.RemoveScript(moTa);
                    if (moTa === '') {
                        $(".thongbaomotaLoaiHinhTacPham").html("Mô tả " + that.Message.MaJavaCript);
                        $(".thongbaomotaLoaiHinhTacPham").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                    if (moTa.length > 501) {
                        $(".thongbaomotaLoaiHinhTacPham").html(that.Message.LengthMoTa);
                        $(".thongbaomotaLoaiHinhTacPham").addClass("show");
                        $("textarea[name='MoTa']").focus();
                        return false;
                    }
                }
                if (/<[a-z][\s\S]*>/i.test(moTa) || (/<[/s/S][a-z][\s\S]*>/i.test(moTa))) {
                    $(".thongbaomotaLoaiHinhTacPham").html("Mô tả " + that.Message.MaHtml);
                    $(".thongbaomotaLoaiHinhTacPham").show();
                    $("textarea[name='MoTa']").focus();
                    return false;
                }
            }
            $(".thongbaomotaLoaiHinhTacPham").hide();
            var data = $("#form-update").serialize();

            Common.Ajax({
                type: "POST",
                async: false,
                cache: false,
                url: Common.DM_LoaiHinhTacPham.Url.Create,
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
                        Common.DM_LoaiHinhTacPhamThemMoi.HideDialog();
                        Common.DM_LoaiHinhTacPham.SetPage(1);
                        Common.DM_LoaiHinhTacPham.SubmitForm();
                    }
                }
            });
            return false;
        });
    },
    ShowDialog: function () {
        $("#dialogAddLoaiHinhTacPham").modal("show");
        Common.DM_LoaiHinhTacPhamThemMoi.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogAddLoaiHinhTacPham").modal("hide");
    }
};

// create DM_LoaiHinhTacPhamThemMoi Object
var DM_LoaiHinhTacPhamThemMoi = DM_LoaiHinhTacPhamThemMoi;
DM_LoaiHinhTacPhamThemMoi.constructor = DM_LoaiHinhTacPhamThemMoi;
