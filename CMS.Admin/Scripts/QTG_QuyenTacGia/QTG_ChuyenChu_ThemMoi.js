var QTG_ChuyenChu_ThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
QTG_ChuyenChu_ThemMoi.prototype = {
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
            //$('#idFormSearchCongDan').find("input[name='Key']").val('ChuyenChu');
            Common.QTG_QuyenTacGia_SearchCongDan.SubmitForm();
            return false;
        });

        $(".btnThemDinhKemMap").unbind("click").click(function () {
            $("#dialogSearchDinhKem").find("input[type=text], textarea").val("");
            $("#dialogSearchDinhKem .modal-title").text("Thông tin đính kèm");
            Common.QTG_QuyenTacGia_SearchDinhKem.SubmitForm();
            return false;
        });

        $(".btn-xoa-chusohuu").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.QTG_QuyenTacGia_SearchChuSoHuu.RemovedChuSoHuu(id);
            return false;
        });

        $(".btn-xoa-tepdinhkem").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.QTG_QuyenTacGia_SearchDinhKem.RemovedDinhKem(id);
            return false;
        });

        $(".btn-view-file").unbind("click").click(function () {
            var url = Common.QTG_ChuyenChu_ThemMoi.Url.ViewFile
                + "?strUrl=" + $(this).data('url')
                + "&isMotCua=" + $(this).data('motcua');
            window.open(url, '_blank');
        });

        $(".btnSubmitNewCapNhat").unbind('click').click(function (e) {
            var form = $("#form-update-tt-chuyenchu");
            Common.ValidatorForm("#form-update-tt-tacpham");
            Common.ValidatorForm("#form-update-tt-chuyenchu");
            if ($("#form-update-tt-tacpham").find(".field-validation-error").length == 0 && $("#form-update-tt-chuyenchu").find(".field-validation-error").length == 0) {
                var _model = {
                    QuyenTacGiaCuID: $("#form-update-tt-tacpham").find("input[name='QuyenTacGiaID']").val() || 0,
                    StaticID: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.StaticID']").val() || 0,
                    HoSoID: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.HoSoID']").val() || 0,
                    STT: $("#form-update-tt-tacpham").find("input[name='ChuyenChu.STT']").val() || null,
                    SoGCN: $("#form-update-tt-tacpham").find("input[name='ChuyenChu.SoGCN']").val() || null,
                    NgayCapGCN: $("#form-update-tt-tacpham").find("input[name='ChuyenChu.NgayCapGCN']").val() || null,
                    SoLanDoiChu: $("#form-update-tt-tacpham").find("input[name='QuyenTacGia.SoLanDoiChu']").val() || 0,
                    ChuyenChuSoHuuID: form.find("input[name='ChuyenChu.ChuyenChuSoHuuID']").val() || 0,
                    ThongTinChuyenChuSoHuu: form.find("textarea[name='ChuyenChu.ThongTinChuyenChuSoHuu']").val() || null,
                    LyDoChuyenChuSoHuu: form.find("textarea[name='ChuyenChu.LyDoChuyenChuSoHuu']").val() || null,
                    NguoiKyID: form.find("select[name='ChuyenChu.NguoiKyID'] option:selected").val() || 0,
                    NgayKy: form.find("input[name='ChuyenChu.NgayKy']").val() || null,
                    IsDauKy: form.find('input[name="QuyenTacGia.IsDauKy"]').is(":checked") ? true : false,
                };
                _model.ListChuSoHuu = Common.QTG_QuyenTacGia_SearchCongDan.ListChuSoHuu;
                _model.ListDinhKem = Common.QTG_QuyenTacGia_SearchDinhKem.ListDinhKem;
                var formdata = new FormData();
                formdata.append('modelThongTin', JSON.stringify(_model));
                Common.ShowLoading(true);
                $.ajax({
                    type: 'POST',
                    url: Common.QTG_ChuyenChu_ThemMoi.Url.SaveChuyenChu,
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
                                $("#form-update-tt-chuyenchu").find("input[name='ChuyenChu.ChuyenChuSoHuuID']").val(result.data);
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

        $(".btnSubmitDuyet").unbind('click').click(function (e) {
            var id = $("#form-update-tt-chuyenchu").find("input[name='ChuyenChu.ChuyenChuSoHuuID']").val() || 0;
            if (id > 0) {
                Common.ShowLoading(true);
                Common.Ajax({
                    url: Common.QTG_ChuyenChu_ThemMoi.Url.CapSo,
                    type: "POST",
                    ContentType: 'application/json; charset=utf-8',
                    async: false,
                    cache: false,
                    dataType: 'json',
                    data: {
                        id: id,
                        loaiNghiepVuID: 4, //Đổi chủ quyền tác giả
                        isDuyet: true
                    }
                }, function (result) {
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
                        }
                        else {
                            $.notify({
                                // options
                                message: 'Duyệt không thành công!!!'
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
            return false;
        });

        $(".btnSubmitKhongDuyet").unbind('click').click(function (e) {
            var id = $("#form-update-tt-chuyenchu").find("input[name='ChuyenChu.ChuyenChuSoHuuID']").val() || 0;
            if (id > 0) {
                Common.ShowLoading(true);
                Common.Ajax({
                    url: Common.QTG_ChuyenChu_ThemMoi.Url.CapSo,
                    type: "POST",
                    ContentType: 'application/json; charset=utf-8',
                    async: false,
                    cache: false,
                    dataType: 'json',
                    data: {
                        id: id,
                        loaiNghiepVuID: 4, //Đổi chủ quyền tác giả
                        isDuyet: false
                    }
                }, function (result) {
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
                        }
                        else {
                            $.notify({
                                // options
                                message: 'Duyệt không thành công!!!'
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
            return false;
        });

        $("#btn-exportWord").unbind("click").click(function () {
            var form = $("#form-update-tt-tacpham");
            var id = $("#form-update-tt-chuyenchu").find("input[name='ChuyenChu.ChuyenChuSoHuuID']").val() || 0;
            var quyenTacGiaID = $("#form-update-tt-chuyenchu").find("input[name='ChuyenChu.QuyenTacGiaID']").val() || 0;
            var soGCN = form.find("input[name='ChuyenChu.SoGCN']").val() || null;
            if (soGCN != null && soGCN != '') {
                Common.QTG_ChuyenChu_ThemMoi.ExportWordPdf(quyenTacGiaID);
            }
            else {
                if (id > 0) {
                    Common.ShowLoading(true);
                    Common.Ajax({
                        url: Common.QTG_ChuyenChu_ThemMoi.Url.CapSo,
                        type: "POST",
                        ContentType: 'application/json; charset=utf-8',
                        async: false,
                        cache: false,
                        dataType: 'json',
                        data: {
                            id: id, loaiNghiepVuID: 4 //Đổi chủ quyền tác giả
                        }
                    }, function (result) {
                        Common.ShowLoading(false);
                        if (!Common.Empty(result)) {
                            if (result.status == true) {
                                var vungMienId = form.find("select[name='QuyenTacGia.VungMienID'] option:selected").val() || 0;
                                //form.find("input[name='ChuyenChu.STT']").val(vungMienId == 1 ? 'B' + result.stt : (vungMienId == 2 ? 'N' + result.stt : (vungMienId == 3 ? 'T' + result.stt : result.stt)));
                                form.find("input[name='ChuyenChu.STT']").val(result.stt);
                                form.find("input[name='ChuyenChu.SoGCN']").val(result.soGCN);
                                $("#form-update-tt-chuyenchu").find("input[name='ChuyenChu.QuyenTacGiaID']").val(result.id);
                                $('.btnSubmitNewCapNhat').trigger('click');
                                Common.QTG_ChuyenChu_ThemMoi.ExportWordPdf(result.id);
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
                                window.location = Common.QTG_ChuyenChu_ThemMoi.Url.IndexCapSo;
                            else if (id > 0)
                                window.location = Common.QTG_ChuyenChu_ThemMoi.Url.IndexHoSo;
                            else
                                window.location = Common.QTG_ChuyenChu_ThemMoi.Url.Index;
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
            url: Common.QTG_ChuyenChu_ThemMoi.Url.ExportWordPdf,
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
var QTG_ChuyenChu_ThemMoi = QTG_ChuyenChu_ThemMoi;
QTG_ChuyenChu_ThemMoi.constructor = QTG_ChuyenChu_ThemMoi;