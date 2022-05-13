﻿var DVC_QuyenLienQuan_ThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
DVC_QuyenLienQuan_ThemMoi.prototype = {
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

        $("#VungMienID").unbind("change").change(function () {
            var form = $("#form-update-tt-tacpham");
            var id = form.find("input[name='QuyenLienQuanID']").val() || 0;
            if (id > 0) {
                var vungMienId = form.find("select[name='VungMienID'] option:selected").val() || 0;
                var stt = form.find("input[name='STT']").val() || null;
                if (stt != null && stt != '') {
                    var stuff = ['B', 'T', 'N'];
                    for (var i = 0; i < stuff.length; i++) {
                        stt = stt.replace(stuff[i], '');
                    }
                    form.find("input[name='STT']").val(vungMienId == 1 ? 'B' + stt : (vungMienId == 2 ? 'T' + stt : (vungMienId == 3 ? 'N' + stt : stt)))
                }
            } else {
                Common.ShowLoading(true);
                Common.Ajax({
                    url: Common.DVC_QuyenLienQuan_ThemMoi.Url.CapSo,
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
                            var vungMienId = form.find("select[name='VungMienID'] option:selected").val() || 0;
                            form.find("input[name='STT']").val(vungMienId == 1 ? 'B' + result.stt : (vungMienId == 2 ? 'T' + result.stt : (vungMienId == 3 ? 'N' + result.stt : result.stt)));
                            return false;
                        }
                        else {
                            $.notify({
                                // options
                                message: 'Lấy stt không thành công!!!'
                            }, {
                                    // settings
                                    delay: 1000,
                                    timer: 1000, type: 'danger'
                                });
                        }
                    }
                });
            }
            return false;
        });

        $(".btnTimKiemHinhAnh").unbind("click").click(function () {
            $("#dialogSearchHinhAnh").find("input[type=text], textarea").val("");
            $("#dialogSearchHinhAnh .modal-title").text("Thông tin hình ảnh");
            $("#dialogSearchHinhAnh").find(".sh-list-hinhanh").removeClass("hidden");
            $("#dialogSearchHinhAnh").find(".sh-update-hinhanh").addClass("hidden");
            $("#idFormSearchHinhAnh").find("input[name='KeyPage']").val("search");
            Common.DVC_QuyenLienQuan_SearchHinhAnh.SubmitForm();
            return false;
        });

        $(".btnThemMoiHinhAnh").unbind("click").click(function () {
            $("#dialogSearchHinhAnh").find("input[type=text], textarea").val("");
            $("#dialogSearchHinhAnh .modal-title").text("Thêm mới hình ảnh");
            $("#dialogSearchHinhAnh").find(".sh-list-hinhanh").addClass("hidden");
            $("#dialogSearchHinhAnh").find(".sh-update-hinhanh").removeClass("hidden");
            $("#idFormSearchHinhAnh").find("input[name='KeyPage']").val("update");
            Common.DVC_QuyenLienQuan_SearchHinhAnh.SubmitForm();
            return false;
        });

        $(".btnTimKiemPhim").unbind("click").click(function () {
            $("#dialogSearchPhim").find("input[type=text], textarea").val("");
            $("#dialogSearchPhim .modal-title").text("Thông tin phim");
            $("#dialogSearchPhim").find(".sh-list-phim").removeClass("hidden");
            $("#dialogSearchPhim").find(".sh-update-phim").addClass("hidden");
            $("#idFormSearchPhim").find("input[name='KeyPage']").val("search");
            Common.DVC_QuyenLienQuan_SearchPhim.SubmitForm();
            return false;
        });

        $(".btnThemMoiPhim").unbind("click").click(function () {
            $("#dialogSearchPhim").find("input[type=text], textarea").val("");
            $("#dialogSearchPhim .modal-title").text("Thêm mới phim");
            $("#dialogSearchPhim").find(".sh-list-phim").addClass("hidden");
            $("#dialogSearchPhim").find(".sh-update-phim").removeClass("hidden");
            $("#idFormSearchPhim").find("input[name='KeyPage']").val("update");
            Common.DVC_QuyenLienQuan_SearchPhim.SubmitForm();
            return false;
        });

        $(".btnThemDinhKemMap").unbind("click").click(function () {
            $("#dialogSearchDinhKem").find("input[type=text], textarea").val("");
            $("#dialogSearchDinhKem .modal-title").text("Thông tin đính kèm");
            Common.DVC_QuyenLienQuan_SearchDinhKem.SubmitForm();
            return false;
        });

        $(".btnThemNguoiBieuDienMap").unbind("click").click(function () {
            $("#dialogSearchNguoiBieuDien").find("input[type=text], textarea").val("");
            $("#dialogSearchNguoiBieuDien").find("select").val(null).trigger('change');
            $("#dialogSearchNguoiBieuDien .modal-title").text("Thông tin tác giả");
            $(".sh-search-nguoibieudien").removeClass("hidden");
            $(".sh-info-nguoibieudien").addClass("hidden");
            Common.DVC_QuyenLienQuan_SearchNguoiBieuDien.SubmitForm();
            return false;
        });

        $(".btnThemChuSoHuuMap").unbind("click").click(function () {
            $("#dialogSearchChuSoHuu").find("input[type=text], textarea").val("");
            $("#dialogSearchChuSoHuu").find("select").val(null).trigger('change');
            $("#dialogSearchChuSoHuu .modal-title").text("Thông tin chủ sở hữu");
            $(".sh-search-chusohuu").removeClass("hidden");
            $(".sh-info-chusohuu").addClass("hidden");
            Common.DVC_QuyenLienQuan_SearchChuSoHuu.SubmitForm();
            return false;
        });

        $(".btnThemNguoiBieuDienChuSoHuuMap").unbind("click").click(function () {
            $("#dialogSearchCongDan").find("input[type=text], textarea").val("");
            $("#dialogSearchCongDan").find("select").val(null).trigger('change');
            $("#dialogSearchCongDan .modal-title").text("Thông tin công dân");
            $(".sh-search-congdan").removeClass("hidden");
            $(".sh-info-congdan").addClass("hidden");
            Common.DVC_QuyenLienQuan_SearchCongDan.SubmitForm();
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
                    QuyenLienQuanID: form.find("input[name='QuyenLienQuanID']").val() || 0,
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
                    NgayKy: $('#NgayKy').val() || null
                };
                _model.ListHinhAnh = Common.DVC_QuyenLienQuan_SearchHinhAnh.ListHinhAnh;
                _model.ListPhim = Common.DVC_QuyenLienQuan_SearchPhim.ListPhim;
                _model.ListNguoiBieuDien = Common.DVC_QuyenLienQuan_SearchCongDan.ListNguoiBieuDien;
                _model.ListChuSoHuu = Common.DVC_QuyenLienQuan_SearchCongDan.ListChuSoHuu;
                _model.ListDinhKem = Common.DVC_QuyenLienQuan_SearchDinhKem.ListDinhKem;
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
                    url: Common.DVC_QuyenLienQuan_ThemMoi.Url.SaveQuyenLienQuan,
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
                                $("#form-update-tt-tacpham").find("input[name='QuyenLienQuanID']").val(result.data);
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
                                window.location = Common.DVC_QuyenLienQuan_ThemMoi.Url.IndexCapSo;
                            else if (id > 0)
                                window.location = Common.DVC_QuyenLienQuan_ThemMoi.Url.IndexHoSo;
                            else
                                window.location = Common.DVC_QuyenLienQuan_ThemMoi.Url.Index;
                        },
                        Value: "Đồng ý"
                    }
                }
            }, "Cancel");
        });

        $(".btn-xoa-nguoibieudien").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.DVC_QuyenLienQuan_SearchNguoiBieuDien.RemovedNguoiBieuDien(id);
            return false;
        });

        $(".btn-xoa-chusohuu").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.DVC_QuyenLienQuan_SearchChuSoHuu.RemovedChuSoHuu(id);
            return false;
        });

        $(".btn-xoa-tepdinhkem").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.DVC_QuyenLienQuan_SearchDinhKem.RemovedDinhKem(id);
            return false;
        });

        $(".btn-view-file").unbind("click").click(function () {
            var url = Common.DVC_QuyenLienQuan_ThemMoi.Url.ViewFile
                + "?strUrl=" + $(this).data('url')
                + "&isMotCua=" + $(this).data('motcua');
            window.open(url, '_blank');
        });

        $(".btnExportFile").unbind("click").click(function () {
            var form = $("#form-update-tt-tacpham");
            var _model = '"QuyenID":"' + form.find("input[name='QuyenLienQuanID']").val() + '",';
            _model += '"HoTenNguoiXuat":"' + form.find("input[name='UserFullName']").val() + '"';
            Common.Ajax({
                type: "POST",
                url: Common.DVC_QuyenLienQuan_ThemMoi.Url.ExportWordPdf,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportDVC_QLQPhieuKiemSoat' }
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
        });       
    },
};
var DVC_QuyenLienQuan_ThemMoi = DVC_QuyenLienQuan_ThemMoi;
DVC_QuyenLienQuan_ThemMoi.constructor = DVC_QuyenLienQuan_ThemMoi;