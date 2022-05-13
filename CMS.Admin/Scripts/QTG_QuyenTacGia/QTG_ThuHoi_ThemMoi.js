var QTG_ThuHoi_ThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
QTG_ThuHoi_ThemMoi.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;

        $(".btnTimKiemGiayChungNhan").unbind("click").click(function () {
            $("#dialogSearchGiayChungNhan").find("input[type=text], textarea").val("");
            $("#dialogSearchGiayChungNhan .modal-title").text("Thông tin giấy chứng nhận");
            Common.QTG_QuyenTacGia_SearchGiayChungNhan.SubmitForm();
            return false;
        });

        $(".btnThemDinhKemMap").unbind("click").click(function () {
            $("#dialogSearchDinhKem").find("input[type=text], textarea").val("");
            $("#dialogSearchDinhKem .modal-title").text("Thông tin đính kèm");
            Common.QTG_QuyenTacGia_SearchDinhKem.SubmitForm();
            return false;
        });

        $(".btnSubmitNewCapNhat").unbind('click').click(function (e) {
            var form = $("#form-update-tt-thuhoi");
            Common.ValidatorForm("#form-update-tt-thuhoi");
            if ($("#form-update-tt-thuhoi").find(".field-validation-error").length == 0) {
                var _model = {
                    QuyenTacGiaID: $("#form-update-tt-tacpham").find("input[name='QuyenTacGiaID']").val() || 0,
                    StaticID: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.StaticID']").val() || 0,
                    HoSoID: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.HoSoID']").val() || 0,
                    STT: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.STT']").val() || null,
                    SoGCN: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.SoGCN']").val() || null,
                    NgayCapGCN: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.NgayCapGCN']").val() || null,
                    SoLanThuHoi: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.SoLanThuHoi']").val() || 0,
                    ThuHoiID: form.find("input[name='ThuHoi.ThuHoiID']").val() || 0,
                    ThongTinThuHoi: form.find("textarea[name='ThuHoi.ThongTinThuHoi']").val() || null,
                    SoQD: form.find("input[name='ThuHoi.SoQD']").val() || null,
                    NgayQD: form.find("input[name='ThuHoi.NgayQD']").val() || null,
                    LyDoThuHoi: form.find("textarea[name='ThuHoi.LyDoThuHoi']").val() || null,
                    ThongTinTacPham: form.find("textarea[name='ThuHoi.ThongTinTacPham']").val() || null,
                    NguoiKyID: form.find("select[name='ThuHoi.NguoiKyID'] option:selected").val() || 0,
                    NgayKy: form.find("input[name='ThuHoi.NgayKy']").val() || null
                };
                _model.ListDinhKem = Common.QTG_QuyenTacGia_SearchDinhKem.ListDinhKem;
                var formdata = new FormData();
                formdata.append('modelThongTin', JSON.stringify(_model));
                Common.ShowLoading(true);
                $.ajax({
                    type: 'POST',
                    url: Common.QTG_ThuHoi_ThemMoi.Url.SaveThuHoi,
                    data: formdata,
                    datatype: "json",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    success: function (result) {
                        Common.ShowLoading(false);
                        if (!Common.Empty(result)) {
                            if (result.status == true) {
                                $.notify({
                                    // options
                                    message: 'Lưu thành công.!!!'
                                }, {
                                    //settings
                                    delay: 1000,
                                    timer: 1000, type: 'success'
                                });
                                $("#form-update-tt-thuhoi").find("input[name='ThuHoi.ThuHoiID']").val(result.data);
                            }
                            else {
                                $.notify({
                                    // options
                                    message: 'Lưu không thành công!!!'
                                }, {
                                    // settings
                                    delay: 1000,
                                    timer: 1000, type: 'danger'
                                });
                            }
                        }
                    },
                    error: function (data) {
                        Common.ShowLoading(false);
                        $.notify({
                            // options
                            message: 'Lưu không thành công.!!!'
                        }, {
                            // settings
                            delay: 1000,
                            timer: 1000, type: 'danger'
                        });
                    }
                });
            }
            return false;
        });

        $(".btn-xoa-tepdinhkem").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.QTG_QuyenTacGia_SearchDinhKem.RemovedDinhKem(id);
            return false;
        });

        $(".btn-view-file").unbind("click").click(function () {
            var url = Common.QTG_ThuHoi_ThemMoi.Url.ViewFile
                + "?strUrl=" + $(this).data('url')
                + "&isMotCua=" + $(this).data('motcua');
            window.open(url, '_blank');
        });

        $(".btnClose").unbind("click").click(function () {
            var id = $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.HoSoID']").val() || 0;
            Common.ShowAlert("Thông báo", "Bạn có muốn đóng màn hình hay không?", {
                Close: { Display: false },
                Items: {
                    Closes: {
                        Name: "Closes",
                        Data: $(this),
                        Class: "btn-default ",
                        OnClick: function (target) {
                            Common.HideAlert();
                        },
                        Value: "Hủy"
                    },
                    Cancel: {
                        Name: "Cancel",
                        Data: $(this),
                        Class: "btn-info ",
                        OnClick: function (target) {
                            var strUrl = window.location.href;
                            var url = new URL(strUrl)
                            var key = url.searchParams.get("key");
                            if (key == 'capso')
                                window.location = Common.QTG_ThuHoi_ThemMoi.Url.IndexCapSo;
                            else if (id > 0)
                                window.location = Common.QTG_ThuHoi_ThemMoi.Url.IndexHoSo;
                            else
                                window.location = Common.QTG_ThuHoi_ThemMoi.Url.Index;
                        },
                        Value: "Đồng ý"
                    }
                }
            }, "Cancel");
        });
    },
};
var QTG_ThuHoi_ThemMoi = QTG_ThuHoi_ThemMoi;
QTG_ThuHoi_ThemMoi.constructor = QTG_ThuHoi_ThemMoi;