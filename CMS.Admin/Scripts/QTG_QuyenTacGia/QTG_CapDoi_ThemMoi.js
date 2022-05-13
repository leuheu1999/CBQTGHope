var QTG_CapDoi_ThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
QTG_CapDoi_ThemMoi.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        $("input[name='IsToChuc']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });

        $("input[name='CoNguoiDaiDien']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
                $(".sh-info-ndd").removeClass("hidden");
                $(".sh-info-ndd").find('input[name="IsToChuc"]').prop('checked', true);
                $(".sh-info-ndd").find("input[type=text]").attr("data-rule-required", "True");
            } else {
                $(this).attr("value", "False");
                $(".sh-info-ndd").addClass("hidden");
                $(".sh-info-ndd").find("input[type=text], textarea").val("");
                $(".sh-info-ndd").find('input[name="IsToChuc"]').prop('checked', false);
                $(".sh-info-ndd").find("input[type=text]").removeAttr("data-rule-required");
            }
        });

        $(".btnTimKiemGiayChungNhan").unbind("click").click(function () {
            $("#dialogSearchGiayChungNhan").find("input[type=text], textarea").val("");
            $("#dialogSearchGiayChungNhan .modal-title").text("Thông tin giấy chứng nhận");
            Common.QTG_QuyenTacGia_SearchGiayChungNhan.SubmitForm();
            return false;
        });

        $(".btnTimKiemHinhAnh").unbind("click").click(function () {
            $("#dialogSearchHinhAnh").find("input[type=text], textarea").val("");
            $("#dialogSearchHinhAnh .modal-title").text("Thông tin hình ảnh");
            $("#dialogSearchHinhAnh").find(".sh-list-hinhanh").removeClass("hidden");
            $("#dialogSearchHinhAnh").find(".sh-update-hinhanh").addClass("hidden");
            $("#idFormSearchHinhAnh").find("input[name='KeyPage']").val("search");
            Common.QTG_QuyenTacGia_SearchHinhAnh.SubmitForm();
            return false;
        });

        $(".btnThemMoiHinhAnh").unbind("click").click(function () {
            $("#dialogSearchHinhAnh").find("input[type=text], textarea").val("");
            $("#dialogSearchHinhAnh .modal-title").text("Thêm mới hình ảnh");
            $("#dialogSearchHinhAnh").find(".sh-list-hinhanh").addClass("hidden");
            $("#dialogSearchHinhAnh").find(".sh-update-hinhanh").removeClass("hidden");
            $("#idFormSearchHinhAnh").find("input[name='KeyPage']").val("update");
            Common.QTG_QuyenTacGia_SearchHinhAnh.SubmitForm();
            return false;
        });

        $(".btnTimKiemPhim").unbind("click").click(function () {
            $("#dialogSearchPhim").find("input[type=text], textarea").val("");
            $("#dialogSearchPhim .modal-title").text("Thông tin phim");
            $("#dialogSearchPhim").find(".sh-list-phim").removeClass("hidden");
            $("#dialogSearchPhim").find(".sh-update-phim").addClass("hidden");
            $("#idFormSearchPhim").find("input[name='KeyPage']").val("search");
            Common.QTG_QuyenTacGia_SearchPhim.SubmitForm();
            return false;
        });

        $(".btnThemMoiPhim").unbind("click").click(function () {
            $("#dialogSearchPhim").find("input[type=text], textarea").val("");
            $("#dialogSearchPhim .modal-title").text("Thêm mới phim");
            $("#dialogSearchPhim").find(".sh-list-phim").addClass("hidden");
            $("#dialogSearchPhim").find(".sh-update-phim").removeClass("hidden");
            $("#idFormSearchPhim").find("input[name='KeyPage']").val("update");
            Common.QTG_QuyenTacGia_SearchPhim.SubmitForm();
            return false;
        });

        $(".btnThemTacGiaMap").unbind("click").click(function () {
            $("#dialogSearchTacGia").find("input[type=text], textarea").val("");
            $("#dialogSearchTacGia").find("select").val(null).trigger('change');
            $("#dialogSearchTacGia .modal-title").text("Thông tin tác giả");
            $(".sh-search-tacgia").removeClass("hidden");
            $(".sh-info-tacgia").addClass("hidden");
            Common.QTG_QuyenTacGia_SearchTacGia.SubmitForm();
            return false;
        });

        $(".btnThemTacGiaChuSoHuuMap").unbind("click").click(function () {
            $("#dialogSearchCongDan").find("input[type=text], textarea").val("");
            $("#dialogSearchCongDan").find("select").val(null).trigger('change');
            $("#dialogSearchCongDan .modal-title").text("Thông tin công dân");
            $(".sh-search-congdan").removeClass("hidden");
            $(".sh-info-congdan").addClass("hidden");
            //$('#idFormSearchCongDan').find("input[name='Key']").val('CapDoi');
            Common.QTG_QuyenTacGia_SearchCongDan.SubmitForm();
            return false;
        });

        $(".btnThemDinhKemMap").unbind("click").click(function () {
            $("#dialogSearchDinhKem").find("input[type=text], textarea").val("");
            $("#dialogSearchDinhKem .modal-title").text("Thông tin đính kèm");
            Common.QTG_QuyenTacGia_SearchDinhKem.SubmitForm();
            return false;
        });

        $(".btnSubmitNewCapNhat").unbind('click').click(function (e) {
            var form = $("#form-update-tt-capdoi");
            var coNguoiDaiDien = form.find('input[name="CoNguoiDaiDien"]').is(":checked") ? true : false;
            if (coNguoiDaiDien == false)
                $(".sh-info-ndd").find("input[type=text]").removeAttr("data-rule-required");
            Common.ValidatorForm("#form-update-tt-tacpham");
            Common.ValidatorForm("#form-update-tt-capdoi");
            if ($("#form-update-tt-tacpham").find(".field-validation-error").length == 0 && $("#form-update-tt-capdoi").find(".field-validation-error").length == 0) {
                var _model = {
                    QuyenTacGiaCuID: $("#form-update-tt-tacpham").find("input[name='QuyenTacGiaID']").val() || 0,
                    StaticID: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.StaticID']").val() || 0,
                    HoSoID: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.HoSoID']").val() || 0,
                    STT: $("#form-update-tt-tacpham").find("input[name='CapDoi.STT']").val() || null,
                    SoGCN: $("#form-update-tt-tacpham").find("input[name='CapDoi.SoGCN']").val() || null,
                    NgayCapGCN: $("#form-update-tt-tacpham").find("input[name='CapDoi.NgayCapGCN']").val() || null,
                    SoLanCapDoi: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.SoLanCapDoi']").val() || 0,
                    VungMienID: $("#form-update-tt-tacpham").find("select[name='QuyenTacGia.VungMienID'] option:selected").val() || 0,
                    CoNguoiDaiDien: $("#form-update-tt-tacpham").find('input[name="CoNguoiDaiDien"]').is(":checked") ? true : false,
                    NDDHoVaTen: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.NDDHoVaTen']").val() || null,
                    NDDSoCMND: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.NDDSoCMND']").val() || null,
                    NDDDiaChi: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.NDDDiaChi']").val() || null,
                    NDDSoDienThoai: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.NDDSoDienThoai']").val() || null,
                    IsToChuc: $("#form-update-tt-tacpham").find('input[name="IsToChuc"]').is(":checked") ? true : false,
                    LoaiDangKyID: $("#form-update-tt-tacpham").find("select[name='QuyenTacGia.LoaiDangKyID'] option:selected").val() || 0,
                    TenTacPham: $("#form-update-tt-tacpham").find("textarea[name='QuyenTacGia.TenTacPham']").val() || null,
                    TenPhim: $("#form-update-tt-tacpham").find("input[name = 'TenPhim']").val() || null,
                    PhimID: $("#form-update-tt-tacpham").find("input[name='PhimID']").val() || 0,
                    PhimStt: $("#form-update-tt-tacpham").find("input[name='PhimStt']").val() || -1,
                    HinhDaiDienUrl: $("#HinhDaiDienUrl").attr("src") || null,
                    HinhDaiDienID: $("#form-update-tt-tacpham").find("input[name='HinhDaiDienID']").val() || 0,
                    HinhDaiDienStt: $("#form-update-tt-tacpham").find("input[name='HinhDaiDienStt']").val() || 0,
                    TrangThaiCongBoID: $("#form-update-tt-tacpham").find("select[name='QuyenTacGia.TrangThaiCongBoID'] option:selected").val() || 0,
                    NgayCongBo: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.NgayCongBo']").val() || null,
                    LoaiHinhID: $("#form-update-tt-tacpham").find("select[name='QuyenTacGia.LoaiHinhID'] option:selected").val() || 0,
                    NoiCongBo: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.NoiCongBo']").val() || null,
                    CapDoiID: form.find("input[name='CapDoi.CapDoiID']").val() || 0,
                    ThongTinCapDoi: form.find("textarea[name='CapDoi.ThongTinCapDoi']").val() || null,
                    SoQD: form.find("input[name='CapDoi.SoQD']").val() || null,
                    NgayQD: form.find("input[name='CapDoi.NgayQD']").val() || null,
                    LyDoCapDoi: form.find("textarea[name='CapDoi.LyDoCapDoi']").val() || null,
                    ThongTinTacPham: form.find("textarea[name='CapDoi.ThongTinTacPham']").val() || null,
                    NguoiKyID: form.find("select[name='CapDoi.NguoiKyID'] option:selected").val() || 0,
                    NgayKy: form.find("input[name='CapDoi.NgayKy']").val() || null
                };
                _model.ListHinhAnh = Common.QTG_QuyenTacGia_SearchHinhAnh.ListHinhAnh;
                _model.ListPhim = Common.QTG_QuyenTacGia_SearchPhim.ListPhim;
                _model.ListTacGia = Common.QTG_QuyenTacGia_SearchTacGia.ListTacGia;
                _model.ListDinhKem = Common.QTG_QuyenTacGia_SearchDinhKem.ListDinhKem;
                var formdata = new FormData();
                formdata.append('modelThongTin', JSON.stringify(_model));
                Common.ShowLoading(true);
                $.ajax({
                    type: 'POST',
                    url: Common.QTG_CapDoi_ThemMoi.Url.SaveCapDoi,
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
                                $("#form-update-tt-capdoi").find("input[name='CapDoi.CapDoiID']").val(result.data);
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

        $(".btn-xoa-tacgia").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.QTG_QuyenTacGia_SearchTacGia.RemovedTacGia(id);
            return false;
        });

        $(".btn-xoa-tepdinhkem").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.QTG_QuyenTacGia_SearchDinhKem.RemovedDinhKem(id);
            return false;
        });

        $(".btn-view-file").unbind("click").click(function () {
            var url = Common.QTG_CapDoi_ThemMoi.Url.ViewFile
                + "?strUrl=" + $(this).data('url')
                + "&isMotCua=" + $(this).data('motcua');
            window.open(url, '_blank');
        });

        $("#btn-exportWord").unbind("click").click(function () {
            var form = $("#form-update-tt-tacpham");
            var id = $("#form-update-tt-capdoi").find("input[name='CapDoi.CapDoiID']").val() || 0;
            var quyenTacGiaID = $("#form-update-tt-capdoi").find("input[name='CapDoi.QuyenTacGiaID']").val() || 0;
            var soGCN = form.find("input[name='CapDoi.SoGCN']").val() || null;
            if (soGCN != null && soGCN != '') {
                Common.QTG_CapDoi_ThemMoi.ExportWordPdf(quyenTacGiaID);
            }
            else {
                if (id > 0) {
                    Common.ShowLoading(true);
                    Common.Ajax({
                        url: Common.QTG_CapDoi_ThemMoi.Url.CapSo,
                        type: "POST",
                        ContentType: 'application/json; charset=utf-8',
                        async: false,
                        cache: false,
                        dataType: 'json',
                        data: {
                            id: id, loaiNghiepVuID: 5 //Cấp đổi quyền tác giả
                        }
                    }, function (result) {
                        Common.ShowLoading(false);
                        if (!Common.Empty(result)) {
                            if (result.status == true) {
                                var vungMienId = form.find("select[name='QuyenTacGia.VungMienID'] option:selected").val() || 0;
                                //form.find("input[name='CapDoi.STT']").val(vungMienId == 1 ? 'B' + result.stt : (vungMienId == 2 ? 'N' + result.stt : (vungMienId == 3 ? 'T' + result.stt : result.stt)));
                                form.find("input[name='CapDoi.STT']").val(result.stt);
                                form.find("input[name='CapDoi.SoGCN']").val(result.soGCN);
                                $("#form-update-tt-capdoi").find("input[name='CapDoi.QuyenTacGiaID']").val(result.id);
                                $('.btnSubmitNewCapNhat').trigger('click');
                                Common.QTG_CapDoi_ThemMoi.ExportWordPdf(result.id);
                                return false;
                            }
                            else {
                                $.notify({
                                    // options
                                    message: 'In file không thành công!!!'
                                }, {
                                    // settings
                                    delay: 1000,
                                    timer: 1000, type: 'danger'
                                });
                            }
                        }
                    });
                }
                else {
                    Common.ShowLoading(false);
                    $.notify({
                        // options
                        message: 'Thông tin chưa được lưu lại.!!!'
                    }, {
                        // settings
                        delay: 1000,
                        timer: 1000, type: 'danger'
                    });
                }
            }
            return false;
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
                                window.location = Common.QTG_CapDoi_ThemMoi.Url.IndexCapSo;
                            else if (id > 0)
                                window.location = Common.QTG_CapDoi_ThemMoi.Url.IndexHoSo;
                            else
                                window.location = Common.QTG_CapDoi_ThemMoi.Url.Index;
                        },
                        Value: "Đồng ý"
                    }
                }
            }, "Cancel");
        });
    },
    ExportWordPdf: function (id) {
        var _model = '"QuyenTacGiaID":"' + id + '"';
        var _key = 'ExportQTG_GiayChungNhan_Mau1_Word';
        for (var i = 0; i < Common.QTG_QuyenTacGia_SearchCongDan.ListTacGia.length; i++) {
            var check = Common.QTG_QuyenTacGia_SearchCongDan.ListChuSoHuu.find(x => x.SoCMND !== Common.QTG_QuyenTacGia_SearchCongDan.ListTacGia[i].SoCMND);
            if (typeof check != 'undefined') {
                _key = 'ExportQTG_GiayChungNhan_Mau2_Word';
            }
        }

        Common.Ajax({
            type: "POST",
            url: Common.QTG_CapDoi_ThemMoi.Url.ExportWordPdf,
            async: false,
            cache: false,
            data: { Param: _model, Key: _key }
        }, function (result) {
            if (result) {
                let a = document.createElement('a')
                a.href = 'data:application/octet-stream;base64,' + result.data;
                a.download = result.fileName;
                document.body.appendChild(a)
                a.click();
                document.body.removeChild(a);
            }
            else {
                $.notify({
                    // options
                    message: "In file không thành công!!!",
                }, {
                    // settings
                    delay: 1000,
                    timer: 1000, type: 'danger'
                });
                return false;
            }
        });
    }

};
var QTG_CapDoi_ThemMoi = QTG_CapDoi_ThemMoi;
QTG_CapDoi_ThemMoi.constructor = QTG_CapDoi_ThemMoi;