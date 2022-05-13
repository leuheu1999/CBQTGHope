var QLQ_CapLai_ThemMoi = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
QLQ_CapLai_ThemMoi.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;

        $(".btnTimKiemGiayChungNhan").unbind("click").click(function () {
            $("#dialogSearchGiayChungNhan").find("input[type=text], textarea").val("");
            $("#dialogSearchGiayChungNhan .modal-title").text("Thông tin giấy chứng nhận");
            Common.QLQ_QuyenLienQuan_SearchGiayChungNhan.SubmitForm();
            return false;
        });

        $(".btnThemDinhKemMap").unbind("click").click(function () {
            $("#dialogSearchDinhKem").find("input[type=text], textarea").val("");
            $("#dialogSearchDinhKem .modal-title").text("Thông tin đính kèm");
            Common.QLQ_QuyenLienQuan_SearchDinhKem.SubmitForm();
            return false;
        });

        $(".btnSubmitNewCapNhat").unbind('click').click(function (e) {
            var form = $("#form-update-tt-caplai");
            Common.ValidatorForm("#form-update-tt-tacpham");
            Common.ValidatorForm("#form-update-tt-caplai");
            if ($("#form-update-tt-tacpham").find(".field-validation-error").length == 0 && $("#form-update-tt-caplai").find(".field-validation-error").length == 0) {
                var _model = {
                    QuyenLienQuanCuID: $("#form-update-tt-tacpham").find("input[name='QuyenLienQuanID']").val() || 0,
                    StaticID: $("#form-update-tt-tacpham").find("input[name='QuyenLienQuan.StaticID']").val() || 0,
                    HoSoID: $("#form-update-tt-tacpham").find("input[name='QuyenLienQuan.HoSoID']").val() || 0,
                    STT: $("#form-update-tt-tacpham").find("input[name='CapLai.STT']").val() || null,
                    SoGCN: $("#form-update-tt-tacpham").find("input[name='CapLai.SoGCN']").val() || null,
                    NgayCapGCN: $("#form-update-tt-tacpham").find("input[name='CapLai.NgayCapGCN']").val() || null,
                    SoLanCapLai: $("#form-update-tt-tacpham").find("input[name='QuyenLienQuan.SoLanCapLai']").val() || 0,
                    CapLaiID: form.find("input[name='CapLai.CapLaiID']").val() || 0,
                    ThongTinCapLai: form.find("textarea[name='CapLai.ThongTinCapLai']").val() || null,
                    SoQD: form.find("input[name='CapLai.SoQD']").val() || null,
                    NgayQD: form.find("input[name='CapLai.NgayQD']").val() || null,
                    LyDoCapLai: form.find("textarea[name='CapLai.LyDoCapLai']").val() || null,
                    ThongTinTacPham: form.find("textarea[name='CapLai.ThongTinTacPham']").val() || null,
                    NguoiKyID: form.find("select[name='CapLai.NguoiKyID'] option:selected").val() || 0,
                    NgayKy: form.find("input[name='CapLai.NgayKy']").val() || null
                };
                _model.ListDinhKem = Common.QLQ_QuyenLienQuan_SearchDinhKem.ListDinhKem;
                var formdata = new FormData();
                formdata.append('modelThongTin', JSON.stringify(_model));
                Common.ShowLoading(true);
                $.ajax({
                    type: 'POST',
                    url: Common.QLQ_CapLai_ThemMoi.Url.SaveCapLai,
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
                                $("#form-update-tt-caplai").find("input[name='CapLai.CapLaiID']").val(result.data);
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
            Common.QLQ_QuyenLienQuan_SearchDinhKem.RemovedDinhKem(id);
            return false;
        });

        $(".btn-view-file").unbind("click").click(function () {
            var url = Common.QLQ_CapLai_ThemMoi.Url.ViewFile
                + "?strUrl=" + $(this).data('url')
                + "&isMotCua=" + $(this).data('motcua');
            window.open(url, '_blank');
        });

        $("#btn-exportWord").unbind("click").click(function () {
            var form = $("#form-update-tt-tacpham");
            var id = $("#form-update-tt-caplai").find("input[name='CapLai.CapLaiID']").val() || 0;
            var quyenLienQuanID = $("#form-update-tt-caplai").find("input[name='CapLai.QuyenLienQuanID']").val() || 0;
            var soGCN = form.find("input[name='CapLai.SoGCN']").val() || null;
            if (soGCN != null && soGCN != '') {
                Common.QLQ_CapLai_ThemMoi.ExportWordPdf(quyenLienQuanID);
            }
            else {
                if (id > 0) {
                    Common.ShowLoading(true);
                    Common.Ajax({
                        url: Common.QLQ_CapLai_ThemMoi.Url.CapSo,
                        type: "POST",
                        ContentType: 'application/json; charset=utf-8',
                        async: false,
                        cache: false,
                        dataType: 'json',
                        data: {
                            id: id, loaiNghiepVuID: 7 //Cấp lại quyền liên quan
                        }
                    }, function (result) {
                        Common.ShowLoading(false);
                        if (!Common.Empty(result)) {
                            if (result.status == true) {
                                var vungMienId = form.find("select[name='QuyenLienQuan.VungMienID'] option:selected").val() || 0;
                                //form.find("input[name='CapLai.STT']").val(vungMienId == 1 ? 'B' + result.stt : (vungMienId == 2 ? 'N' + result.stt : (vungMienId == 3 ? 'T' + result.stt : result.stt)));
                                form.find("input[name='CapLai.STT']").val(result.stt);
                                form.find("input[name='CapLai.SoGCN']").val(result.soGCN);
                                $("#form-update-tt-caplai").find("input[name='CapLai.QuyenLienQuanID']").val(result.id);
                                $('.btnSubmitNewCapNhat').trigger('click');
                                Common.QLQ_CapLai_ThemMoi.ExportWordPdf(result.id);
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
            var id = $("#form-update-tt-tacpham").find("input[name='QuyenLienQuan.HoSoID']").val() || 0;
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
                                window.location = Common.QLQ_CapLai_ThemMoi.Url.IndexCapSo;
                            else if (id > 0)
                                window.location = Common.QLQ_CapLai_ThemMoi.Url.IndexHoSo;
                            else
                                window.location = Common.QLQ_CapLai_ThemMoi.Url.Index;
                        },
                        Value: "Đồng ý"
                    }
                }
            }, "Cancel");
        });
    },
    ExportWordPdf: function (id) {
        var _model = '"QuyenLienQuanID":"' + id + '"';
        Common.Ajax({
            type: "POST",
            url: Common.QLQ_CapLai_ThemMoi.Url.ExportWordPdf,
            async: false,
            cache: false,
            data: { Param: _model, Key: 'ExportQLQ_GiayChungNhan_Word' }
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
var QLQ_CapLai_ThemMoi = QLQ_CapLai_ThemMoi;
QLQ_CapLai_ThemMoi.constructor = QLQ_CapLai_ThemMoi;