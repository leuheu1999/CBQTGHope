var QTG_QuyenTacGia_ThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
QTG_QuyenTacGia_ThemMoi.prototype = {
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

        $(".btnTimKiemSTT").unbind("click").click(function () {
            $("#dialogSearchSoThuTu").find("input[type=text], textarea").val("");
            $("#dialogSearchSoThuTu .modal-title").text("Thông tin số thứ tự");
            Common.QTG_QuyenTacGia_SearchSoThuTu.SubmitForm();
            return false;
        });

        //$("#VungMienID").unbind("change").change(function () {
        //    var form = $("#form-update-tt-tacpham");
        //    var vungMienId = form.find("select[name='VungMienID'] option:selected").val() || 0;
        //    var stt = form.find("input[name='STT']").val() || null;
        //    if (stt != null && stt != '') {
        //        var stuff = ['B', 'T', 'N'];
        //        for (var i = 0; i < stuff.length; i++) {
        //            stt = stt.replace(stuff[i], '');
        //        }
        //        form.find("input[name='STT']").val(vungMienId == 1 ? 'B' + stt : (vungMienId == 2 ? 'N' + stt : (vungMienId == 3 ? 'T' + stt : stt)))
        //    }
        //    return false;
        //});

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

        $(".btnThemDinhKemMap").unbind("click").click(function () {
            $("#dialogSearchDinhKem").find("input[type=text], textarea").val("");
            $("#dialogSearchDinhKem .modal-title").text("Thông tin đính kèm");
            Common.QTG_QuyenTacGia_SearchDinhKem.SubmitForm();
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

        $(".btnThemChuSoHuuMap").unbind("click").click(function () {
            $("#dialogSearchChuSoHuu").find("input[type=text], textarea").val("");
            $("#dialogSearchChuSoHuu").find("select").val(null).trigger('change');
            $("#dialogSearchChuSoHuu .modal-title").text("Thông tin chủ sở hữu");
            $(".sh-search-chusohuu").removeClass("hidden");
            $(".sh-info-chusohuu").addClass("hidden");
            Common.QTG_QuyenTacGia_SearchChuSoHuu.SubmitForm();
            return false;
        });

        $(".btnThemTacGiaChuSoHuuMap").unbind("click").click(function () {
            $("#dialogSearchCongDan").find("input[type=text], textarea").val("");
            $("#dialogSearchCongDan").find("select").val(null).trigger('change');
            $("#dialogSearchCongDan .modal-title").text("Thông tin công dân");
            $(".sh-search-congdan").removeClass("hidden");
            $(".sh-info-congdan").addClass("hidden");
            Common.QTG_QuyenTacGia_SearchCongDan.SubmitForm();
            return false;
        });

        $(".btnSubmitNewCapNhat").unbind('click').click(function (e) {
            var form = $("#form-update-tt-tacpham");
            var coNguoiDaiDien = form.find('input[name="CoNguoiDaiDien"]').is(":checked") ? true : false;
            if (coNguoiDaiDien == false)
                $(".sh-info-ndd").find("input[type=text]").removeAttr("data-rule-required");
            Common.ValidatorForm("#form-update-tt-tacpham");
            if ($("#form-update-tt-tacpham").find(".field-validation-error").length == 0) {
                var _model = {
                    QuyenTacGiaID: form.find("input[name='QuyenTacGiaID']").val() || 0,
                    HoSoID: form.find("input[name='HoSoID']").val() || 0,
                    STT: form.find("input[name='STT']").val() || null,
                    SoGCN: form.find("input[name='SoGCN']").val() || null,
                    VungMienID: form.find("select[name='VungMienID'] option:selected").val() || 0,
                    CoNguoiDaiDien: form.find('input[name="CoNguoiDaiDien"]').is(":checked") ? true : false,
                    NgayCap: form.find("input[name='NgayCap']").val() || null,
                    NDDHoVaTen: form.find("input[name='NDDHoVaTen']").val() || null,
                    NDDSoCMND: form.find("input[name='NDDSoCMND']").val() || null,
                    NDDDiaChi: form.find("input[name='NDDDiaChi']").val() || null,
                    NDDSoDienThoai: form.find("input[name='NDDSoDienThoai']").val() || null,
                    IsToChuc: form.find('input[name="IsToChuc"]').is(":checked") ? true : false,
                    LoaiDangKyID: form.find("select[name='LoaiDangKyID'] option:selected").val() || 0,
                    NgayHT: form.find("input[name='NgayHT']").val() || null,
                    TenTacPham: form.find("textarea[name='TenTacPham']").val() || null,
                    TenPhim: form.find("input[name = 'TenPhim']").val() || null,
                    PhimID: form.find("input[name='PhimID']").val() || 0,
                    PhimStt: form.find("input[name='PhimStt']").val() || -1,
                    HinhDaiDienUrl: $("#HinhDaiDienUrl").attr("src") || null,
                    HinhDaiDienID: form.find("input[name='HinhDaiDienID']").val() || 0,
                    HinhDaiDienStt: form.find("input[name='HinhDaiDienStt']").val() || 0,
                    TrangThaiCongBoID: form.find("select[name='TrangThaiCongBoID'] option:selected").val() || 0,
                    NgayCongBo: form.find("input[name='NgayCongBo']").val() || null,
                    LoaiHinhID: form.find("select[name='LoaiHinhID'] option:selected").val() || 0,
                    NoiCongBo: form.find("input[name='NoiCongBo']").val() || null,
                    NguoiKyID: $('#NguoiKyID').val() || 0,
                    NgayKy: $('#NgayKy').val() || null,
                    DVCID: $('#DVCID').val() || 0,
                };
                _model.ListHinhAnh = Common.QTG_QuyenTacGia_SearchHinhAnh.ListHinhAnh;
                _model.ListPhim = Common.QTG_QuyenTacGia_SearchPhim.ListPhim;
                _model.ListTacGia = Common.QTG_QuyenTacGia_SearchCongDan.ListTacGia;
                _model.ListChuSoHuu = Common.QTG_QuyenTacGia_SearchCongDan.ListChuSoHuu;
                _model.ListDinhKem = Common.QTG_QuyenTacGia_SearchDinhKem.ListDinhKem;
                //if (_model.STT != null && _model.STT != '') {
                //    var stuff = ['B', 'T', 'N'];
                //    for (var i = 0; i < stuff.length; i++) {
                //        _model.STT = _model.STT.replace(stuff[i], '');
                //    }
                //}
                var formdata = new FormData();
                formdata.append('modelThongTin', JSON.stringify(_model));
                Common.ShowLoading(true);
                $.ajax({
                    type: 'POST',
                    url: Common.QTG_QuyenTacGia_ThemMoi.Url.SaveQuyenTacGia,
                    data: formdata,
                    datatype: "json",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    success: function (result) {
                        Common.ShowLoading(false);
                        if (!Common.Empty(result)) {
                            if (result.checkSoTT == false) {
                                $.notify({
                                    // options
                                    message: 'Số TT đã tồn tại!!!'
                                }, {
                                    // settings
                                    delay: 1000,
                                    timer: 1000, type: 'danger'
                                });
                            }
                            else if (result.checkSoGCN == false) {
                                $.notify({
                                    // options
                                    message: 'Số GCN đã tồn tại!!!'
                                }, {
                                    // settings
                                    delay: 1000,
                                    timer: 1000, type: 'danger'
                                });
                            }
                            else if (result.status == true) {
                                $.notify({
                                    // options
                                    message: 'Lưu thành công.!!!'
                                }, {
                                    //settings
                                    delay: 1000,
                                    timer: 1000, type: 'success'
                                });
                                $("#form-update-tt-tacpham").find("input[name='QuyenTacGiaID']").val(result.data);
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

        $("#btn-exportWord").unbind("click").click(function () {
            var form = $("#form-update-tt-tacpham");
            var id = form.find("input[name='QuyenTacGiaID']").val() || 0;
            var soGCN = form.find("input[name='SoGCN']").val() || null;
            if (soGCN != null && soGCN != '') {
                Common.QTG_QuyenTacGia_ThemMoi.ExportWordPdf(id);
            }
            else {
                if (id > 0) {
                    Common.ShowLoading(true);
                    Common.Ajax({
                        url: Common.QTG_QuyenTacGia_ThemMoi.Url.CapSo,
                        type: "POST",
                        ContentType: 'application/json; charset=utf-8',
                        async: false,
                        cache: false,
                        dataType: 'json',
                        data: {
                            id: id, loaiNghiepVuID: 1 //Cấp mới quyền tác giả
                        }
                    }, function (result) {
                        Common.ShowLoading(false);
                        if (!Common.Empty(result)) {
                            if (result.status == true) {
                                var stt = form.find("input[name='STT']").val();
                                //var vungMienId = form.find("select[name='VungMienID'] option:selected").val() || 0;
                                //form.find("input[name='STT']").val(vungMienId == 1 ? 'B' + result.stt : (vungMienId == 2 ? 'N' + result.stt : (vungMienId == 3 ? 'T' + result.stt : result.stt)));
                                form.find("input[name='STT']").val(result.stt + ' ' + stt);
                                form.find("input[name='SoGCN']").val(result.soGCN);
                                Common.QTG_QuyenTacGia_ThemMoi.ExportWordPdf(id);
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
            var id = $("#form-update-tt-tacpham").find("input[name='HoSoID']").val() || 0;
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
                                window.location = Common.QTG_QuyenTacGia_ThemMoi.Url.IndexCapSo;
                            else if (id > 0)
                                window.location = Common.QTG_QuyenTacGia_ThemMoi.Url.IndexHoSo;
                            else
                                window.location = Common.QTG_QuyenTacGia_ThemMoi.Url.Index;                         
                        },
                        Value: "Đồng ý"
                    }
                }
            }, "Cancel");
        });

        $(".btn-xoa-tacgia").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.QTG_QuyenTacGia_SearchCongDan.RemovedTacGia(id);
            return false;
        });

        $(".btn-xoa-chusohuu").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.QTG_QuyenTacGia_SearchCongDan.RemovedChuSoHuu(id);
            return false;
        });

        $(".btn-xoa-tepdinhkem").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.QTG_QuyenTacGia_SearchDinhKem.RemovedDinhKem(id);
            return false;
        });

        $(".btn-view-file").unbind("click").click(function () {
            var url = Common.QTG_QuyenTacGia_ThemMoi.Url.ViewFile
                + "?strUrl=" + $(this).data('url')
                + "&isMotCua=" + $(this).data('motcua');               
            window.open(url, '_blank');
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
            url: Common.QTG_QuyenTacGia_ThemMoi.Url.ExportWordPdf,
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
                    message: "Xuất file không thành công!!!",
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
var QTG_QuyenTacGia_ThemMoi = QTG_QuyenTacGia_ThemMoi;
QTG_QuyenTacGia_ThemMoi.constructor = QTG_QuyenTacGia_ThemMoi;