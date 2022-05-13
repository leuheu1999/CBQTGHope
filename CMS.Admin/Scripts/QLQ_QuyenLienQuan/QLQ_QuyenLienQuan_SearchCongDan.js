var QLQ_QuyenLienQuan_SearchCongDan = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
QLQ_QuyenLienQuan_SearchCongDan.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;
        Common.Init();

        $("#btnSearchCongDan").unbind("click").click(function () {
            Common.QLQ_QuyenLienQuan_SearchCongDan.SubmitForm();
            return false;
        });

        $("#btnInsertCongDan").unbind("click").click(function () {
            $("#dialogSearchCongDan").find("input[type=text], textarea").val("");
            $("#dialogSearchCongDan").find("input[type=hidden]").val("");
            $("#dialogSearchCongDan").find(".datepicker-share").datepicker("destroy");
            $("#dialogSearchCongDan").find(".datepicker-share").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn'
            });
            $("#dialogSearchCongDan").find(".datepicker-share").datepicker("refresh");
            $("#dialogSearchCongDan").find("input[name='CongDanID']").val(0);
            $("#dialogSearchCongDan").find("select").val(null).trigger('change');
            $("#dialogSearchCongDan .modal-title").text("Thêm mới thông tin công dân");
            $(".sh-search-congdan").addClass("hidden");
            $(".sh-info-congdan").removeClass("hidden");
        });

        $("#btnCancelCongDan").unbind("click").click(function () {
            $("#dialogSearchCongDan").find("input[type=text], textarea").val("");
            $("#dialogSearchCongDan").find("input[type=hidden]").val("");
            $("#dialogSearchCongDan").find(".datepicker-share").datepicker("destroy");
            $("#dialogSearchCongDan").find(".datepicker-share").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn'
            });
            $("#dialogSearchCongDan").find(".datepicker-share").datepicker("refresh");
            $("#dialogSearchCongDan").find("select").val(null).trigger('change');
            $("#dialogSearchCongDan .modal-title").text("Thông tin thông tin công dân");
            $(".sh-search-congdan").removeClass("hidden");
            $(".sh-info-congdan").addClass("hidden");
        });

        $("#selectAllCheckBoxCSH").unbind("change").change(function () {
            $("#table-congdan input[name='chkInputCheckedCSH[]']:checkbox").not(this).prop('checked', this.checked);
        });

        $("#selectAllCheckBoxNBD").unbind("change").change(function () {
            $("#table-congdan input[name='chkInputCheckedNBD[]']:checkbox").not(this).prop('checked', this.checked);
        });

        $('#table-congdan tbody tr input[name="chkInputCheckedCSH[]"]').unbind("change").change(function () {
            var c = $("input[name='chkInputCheckedCSH[]']:checkbox:checked").length;
            var d = $("input[name='chkInputCheckedCSH[]']:checkbox").length;
            $("#selectAllCheckBoxCSH").prop("checked", !(c !== d));
        });

        $('#table-congdan tbody tr input[name="chkInputCheckedNBD[]"]').unbind("change").change(function () {
            var c = $("input[name='chkInputCheckedNBD[]']:checkbox:checked").length;
            var d = $("input[name='chkInputCheckedNBD[]']:checkbox").length;
            $("#selectAllCheckBoxNBD").prop("checked", !(c !== d));
        });

        $("#btnAddCongDan").unbind("click").click(function (event) {
            event.preventDefault();
            if ($('input[name="chkInputCheckedCSH[]"]:checked').length > 0) {
                $('input[name="chkInputCheckedCSH[]"]').each(function () {
                    if ($(this).prop("checked")) {
                        var check = Common.QLQ_QuyenLienQuan_SearchCongDan.ListChuSoHuu.find(x => x.ChuSoHuuID === $(this).closest("tr").data("id"));
                        //if (typeof check != 'undefined') {
                        //    $.notify({
                        //        // options
                        //        message: 'Chủ sở hữu ' + check.HoVaTen + ' đã được chọn!!!'
                        //    }, {
                        //            // settings
                        //            delay: 1000,
                        //            timer: 1000, type: 'danger'
                        //        });
                        //    //return false;
                        //}
                        //else {
                        //    Common.QLQ_QuyenLienQuan_SearchCongDan.ListChuSoHuu.push({
                        //        ChuSoHuuID: $(this).closest("tr").data("id"),
                        //        HoVaTen: $(this).closest("tr").data("name"),
                        //        QuocTich: $(this).closest("tr").data("country"),
                        //        SoCMND: $(this).closest("tr").data("cccd"),
                        //        SoDKKD: $(this).closest("tr").data("dkkd"),
                        //        DiaChi: $(this).closest("tr").data("address")
                        //    });
                        //}
                        if (typeof check == 'undefined') {
                            Common.QLQ_QuyenLienQuan_SearchCongDan.ListChuSoHuu.push({
                                ChuSoHuuID: $(this).closest("tr").data("id"),
                                HoVaTen: $(this).closest("tr").data("name"),
                                QuocTich: $(this).closest("tr").data("country"),
                                SoCMND: $(this).closest("tr").data("cccd"),
                                SoDKKD: $(this).closest("tr").data("dkkd"),
                                DiaChi: $(this).closest("tr").data("address")
                            });
                        }
                    }
                });
            }
            if ($('input[name="chkInputCheckedNBD[]"]:checked').length > 0) {
                $('input[name="chkInputCheckedNBD[]"]').each(function () {
                    if ($(this).prop("checked")) {
                        var check = Common.QLQ_QuyenLienQuan_SearchCongDan.ListNguoiBieuDien.find(x => x.NguoiBieuDienID === $(this).closest("tr").data("id"));
                        //if (typeof check != 'undefined') {
                        //    $.notify({
                        //        // options
                        //        message: 'Tác giả ' + check.HoVaTen + ' đã được chọn!!!'
                        //    }, {
                        //            // settings
                        //            delay: 1000,
                        //            timer: 1000, type: 'danger'
                        //        });
                        //    //return false;
                        //}
                        //else {
                        //    Common.QLQ_QuyenLienQuan_SearchCongDan.ListNguoiBieuDien.push({
                        //        NguoiBieuDienID: $(this).closest("tr").data("id"),
                        //        HoVaTen: $(this).closest("tr").data("name"),
                        //        QuocTich: $(this).closest("tr").data("country"),
                        //        SoCMND: $(this).closest("tr").data("cccd"),
                        //        DiaChi: $(this).closest("tr").data("address")
                        //    });
                        //}
                        if (typeof check == 'undefined') {
                            Common.QLQ_QuyenLienQuan_SearchCongDan.ListNguoiBieuDien.push({
                                NguoiBieuDienID: $(this).closest("tr").data("id"),
                                HoVaTen: $(this).closest("tr").data("name"),
                                QuocTich: $(this).closest("tr").data("country"),
                                SoCMND: $(this).closest("tr").data("cccd"),
                                DiaChi: $(this).closest("tr").data("address")
                            });
                        }
                    }
                });
            }
            var key = $("#idFormSearchCongDan").find("input[name='Key']").val();
            if (key == 'ChuyenChu')
                Common.QLQ_QuyenLienQuan_SearchCongDan.LoadTableChuSoHuu();
            else if (key == 'CapDoi')
                Common.QLQ_QuyenLienQuan_SearchCongDan.LoadTableNguoiBieuDien();
            else {
                Common.QLQ_QuyenLienQuan_SearchCongDan.LoadTableChuSoHuu();
                Common.QLQ_QuyenLienQuan_SearchCongDan.LoadTableNguoiBieuDien();
            }
            Common.QLQ_QuyenLienQuan_SearchCongDan.HideDialog();
            return false;
        });

        $(".btn-update-congdan").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.QLQ_QuyenLienQuan_SearchCongDan.Url.UpdateCongDan,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'html',
                data: { id: id }
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    $("#form-update-congdan").html(result);
                    $(".sh-search-congdan").addClass("hidden");
                    $(".sh-info-congdan").removeClass("hidden");
                    $("#dialogSearchCongDan .modal-title").text("Chỉnh sửa thông tin thông tin công dân");
                    Common.QLQ_QuyenLienQuan_SearchCongDan.RegisterEvent();
                }
            }
            );
            return false;
        });

        $("#btnSaveCongDan").unbind("click").click(function () {
            var form = $("#form-update-congdan");
            Common.ValidatorForm("#form-update-congdan");
            if ($("#form-update-congdan").find(".field-validation-error").length == 0) {
                var _model = {
                    CongDanID: form.find("input[name='CongDanID']").val() || 0,
                    HoVaTen: form.find("input[name='HoVaTen']").val() || null,
                    QuocTichID: form.find("select[name='QuocTichID']").val() || 0,
                    QuocTich: form.find("select[name='QuocTichID'] option:selected").text() || null,
                    SoCMND: form.find("input[name='SoCMND']").val() || null,
                    NgayCapCMND: form.find("input[name='NgayCapCMND']").val() || null,
                    NoiCapID: form.find("select[name='NoiCapID']").val() || 0,
                    NoiCap: form.find("select[name='NoiCapID'] option:selected").text() || null,
                    SoDKKD: form.find("input[name='SoDKKD']").val() || null,
                    NgayCapDKKD: form.find("input[name='NgayCapDKKD']").val() || null,
                    NoiCapDKKDID: form.find("select[name='NoiCapDKKDID']").val() || 0,
                    NoiCapDKKD: form.find("select[name='NoiCapDKKDID'] option:selected").text() || null,
                    ButDanh: form.find("input[name='ButDanh']").val() || null,
                    DiaChi: form.find("input[name='DiaChi']").val() || null,
                };
                var formdata = new FormData();
                formdata.append('modelThongTin', JSON.stringify(_model));
                Common.ShowLoading(true);
                $.ajax({
                    type: 'POST',
                    url: Common.QLQ_QuyenLienQuan_SearchCongDan.Url.SaveCongDan,
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
                                $("#dialogSearchCongDan").find("input[type=text], textarea").val("");
                                $("#dialogSearchCongDan").find("select").val(null).trigger('change');
                                $("#dialogSearchCongDan .modal-title").text("Thông tin thông tin công dân");
                                $(".sh-search-congdan").removeClass("hidden");
                                $(".sh-info-congdan").addClass("hidden");
                                Common.QLQ_QuyenLienQuan_SearchCongDan.SubmitForm();
                            }
                            else if (result.status == false && result.checkCCCD == true) {
                                $.notify({
                                    // options
                                    message: 'Số CCCD/Hộ chiếu đã tồn tại!!!'
                                }, {
                                        // settings
                                        delay: 1000,
                                        timer: 1000, type: 'danger'
                                    });
                            }
                            else if (result.status == false && result.checkDKKD == true) {
                                $.notify({
                                    // options
                                    message: 'Số ĐKKD đã tồn tại!!!'
                                }, {
                                        // settings
                                        delay: 1000,
                                        timer: 1000, type: 'danger'
                                    });
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
        });
    },
    DetailCongDan: function (id) {
        Common.QLQ_QuyenLienQuan_SearchCongDan.SubmitForm();
        Common.ShowLoading(true);
        Common.Ajax({
            url: Common.QLQ_QuyenLienQuan_SearchCongDan.Url.UpdateCongDan,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'html',
            data: { id: id }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                $("#form-update-congdan").html(result);
                $(".sh-search-congdan").addClass("hidden");
                $(".sh-info-congdan").removeClass("hidden");
                $("#dialogSearchCongDan .modal-title").text("Chỉnh sửa thông tin thông tin công dân");
                Common.QLQ_QuyenLienQuan_SearchCongDan.RegisterEvent();
            }
        });
        return false;
    },
    SubmitForm: function () {
        var form = $("#idFormSearchCongDan");
        var pageIndex = $("#dialogSearchCongDan").find("input[name='PageIndex']").val() > 0 ? $("#dialogSearchCongDan").find("input[name='PageIndex']").val() : 1;
        var pageSize = $("#dialogSearchCongDan").find("input[name='PageSize']").val() > 0 ? $("#dialogSearchCongDan").find("input[name='PageSize']").val() : 10;
        var model = {
            pageIndex: pageIndex,
            pageSize: pageSize,
            HoVaTen: form.find("input[name='HoVaTen']").val() || null,
            SoCMND: form.find("input[name='SoCMND']").val() || null,
            NgayCap: form.find("input[name='NgayCap']").val() || null,
            SoDKKD: form.find("input[name='SoDKKD']").val() || null,
            NgayCapDKKD: form.find("input[name='NgayCapDKKD']").val() || null,
            ButDanh: form.find("input[name='ButDanh']").val() || null,
            DiaChi: form.find("input[name='DiaChi']").val() || null,
            Key: form.find("input[name='Key']").val() || null,
        };
        var formdata = new FormData();
        formdata.append('modelSearch', JSON.stringify(model));
        Common.Ajax({
            type: 'POST',
            url: Common.QLQ_QuyenLienQuan_SearchCongDan.Url.SearchCongDan,
            data: formdata,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            dataType: 'html',
        }, function (result) {
            $("#result-searchcongdan").empty();
            $("#result-searchcongdan").html(result);
            Common.QLQ_QuyenLienQuan_SearchCongDan.ShowDialog();
            $('input[name="chkInputCheckedCSH[]"]').each(function () {
                var check = Common.QLQ_QuyenLienQuan_SearchCongDan.ListChuSoHuu.find(x => x.ChuSoHuuID === $(this).closest("tr").data("id"));
                if (typeof check != 'undefined') {
                    $(this).prop("checked", true);
                }
            });
            $('input[name="chkInputCheckedNBD[]"]').each(function () {
                var check = Common.QLQ_QuyenLienQuan_SearchCongDan.ListNguoiBieuDien.find(x => x.NguoiBieuDienID === $(this).closest("tr").data("id"));
                if (typeof check != 'undefined') {
                    $(this).prop("checked", true);
                }
            });     
        });
        return false;
    },
    Paging: function (page) {
        Common.QLQ_QuyenLienQuan_SearchCongDan.SetPage(page);
        Common.QLQ_QuyenLienQuan_SearchCongDan.SubmitForm();
        return false;
    },
    SetPage: function (page) {
        $("#dialogSearchCongDan").find("input[name='PageIndex']").val(page);
    },
    SetPageSize: function (page) {
        $("#idFormSearchCongDan").find("input[name='PageSize']").val(page);
    },
    ChangePageSize: function (e) {
        Common.QLQ_QuyenLienQuan_SearchCongDan.SetPageSize($(e).val());
        Common.QLQ_QuyenLienQuan_SearchCongDan.SubmitForm();
        return false;
    },
    ShowDialog: function () {
        $("#dialogSearchCongDan").modal("show")
        Common.QLQ_QuyenLienQuan_SearchCongDan.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogSearchCongDan").modal("hide");
    },
    LoadTableChuSoHuu: function () {
        var html = '';
        if (Common.QLQ_QuyenLienQuan_SearchCongDan.ListChuSoHuu != null && Common.QLQ_QuyenLienQuan_SearchCongDan.ListChuSoHuu.length > 0) {
            for (i = 0; i < Common.QLQ_QuyenLienQuan_SearchCongDan.ListChuSoHuu.length; i++) {
                var chuSoHuu = Common.QLQ_QuyenLienQuan_SearchCongDan.ListChuSoHuu[i];
                html += '<tr>';
                html += '<td class"cursor" onclick="Common.QLQ_QuyenLienQuan_SearchCongDan.DetailCongDan(' + chuSoHuu.ChuSoHuuID + ')" data-label="Họ tên">' + chuSoHuu.HoVaTen + '</td>';
                html += '<td data-label="Quốc tịch">' + chuSoHuu.QuocTich + '</td>';
                html += '<td data-label="CCCD/Hộ chiếu">' + chuSoHuu.SoCMND + '</td>';
                html += '<td data-label="Số ĐKKD">' + chuSoHuu.SoDKKD + '</td>';
                html += '<td data-label="Địa chỉ">' + chuSoHuu.DiaChi + '</td>';
                html += '<td data-label="Thao tác" class="text-center">';
                html += '<button title="Xóa chủ sở hữu" data-id="' + chuSoHuu.ChuSoHuuID + '" class="btn btn-danger btn-sm" type="button" onclick="Common.QLQ_QuyenLienQuan_SearchCongDan.RemovedChuSoHuu(' + i + ')"><i class="far fa-trash-alt"></i></button>';
                html += '</td>';
                html += '</tr>';
            }
        }
        else {
            html += '<tr><td colspan="6"><div class="alert alert-info mt-3" role="alert"> Không có dữ liệu</div></td></tr>';
        }
        //$("#tableChuSoHuu tbody").find("tr").empty();
        $("#tableChuSoHuu tbody").empty();
        $('#tableChuSoHuu').append(html);
    },
    LoadTableNguoiBieuDien: function () {
        var html = '';
        if (Common.QLQ_QuyenLienQuan_SearchCongDan.ListNguoiBieuDien != null && Common.QLQ_QuyenLienQuan_SearchCongDan.ListNguoiBieuDien.length > 0) {
            for (i = 0; i < Common.QLQ_QuyenLienQuan_SearchCongDan.ListNguoiBieuDien.length; i++) {
                var nguoiBieuDien = Common.QLQ_QuyenLienQuan_SearchCongDan.ListNguoiBieuDien[i];
                html += '<tr>';
                html += '<td class="cursor" onclick="Common.QLQ_QuyenLienQuan_SearchCongDan.DetailCongDan(' + nguoiBieuDien.NguoiBieuDienID + ')" data-label="Họ tên">' + nguoiBieuDien.HoVaTen + '</td>';
                html += '<td data-label="Quốc tịch">' + nguoiBieuDien.QuocTich + '</td>';
                html += '<td data-label="CCCD/Hộ chiếu">' + nguoiBieuDien.SoCMND + '</td>';
                html += '<td data-label="Địa chỉ">' + nguoiBieuDien.DiaChi + '</td>';
                html += '<td data-label="Thao tác" class="text-center">';
                html += '<button title="Xóa tác giả" data-id="' + nguoiBieuDien.NguoiBieuDienID + '" class="btn btn-danger btn-sm" type="button" onclick="Common.QLQ_QuyenLienQuan_SearchCongDan.RemovedNguoiBieuDien(' + i + ')"><i class="far fa-trash-alt"></i></button>';
                html += '</td>';
                html += '</tr>';
            }
        }
        else {
            html += '<tr><td colspan="5"><div class="alert alert-info mt-3" role="alert"> Không có dữ liệu</div></td></tr>';
        }
        //$("#tableNguoiBieuDien tbody").find("tr").empty();
        $("#tableNguoiBieuDien tbody").empty();
        $('#tableNguoiBieuDien').append(html);
    },
    RemovedChuSoHuu: function (id) {
        Common.QLQ_QuyenLienQuan_SearchCongDan.ListChuSoHuu.splice(id, 1)
        Common.QLQ_QuyenLienQuan_SearchCongDan.LoadTableChuSoHuu();
        return false;
    },
    RemovedNguoiBieuDien: function (id) {
        Common.QLQ_QuyenLienQuan_SearchCongDan.ListNguoiBieuDien.splice(id, 1)
        Common.QLQ_QuyenLienQuan_SearchCongDan.LoadTableNguoiBieuDien();
        return false;
    },
};

var QLQ_QuyenLienQuan_SearchCongDan = QLQ_QuyenLienQuan_SearchCongDan;
QLQ_QuyenLienQuan_SearchCongDan.constructor = QLQ_QuyenLienQuan_SearchCongDan;
