var DVC_QuyenTacGia_SearchCongDan = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
DVC_QuyenTacGia_SearchCongDan.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;
        Common.Init();

        $("#btnSearchCongDan").unbind("click").click(function () {
            Common.DVC_QuyenTacGia_SearchCongDan.SubmitForm();
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

        $("#selectAllCheckBoxTG").unbind("change").change(function () {
            $("#table-congdan input[name='chkInputCheckedTG[]']:checkbox").not(this).prop('checked', this.checked);
        });

        $('#table-congdan tbody tr input[name="chkInputCheckedCSH[]"]').unbind("change").change(function () {
            var c = $("input[name='chkInputCheckedCSH[]']:checkbox:checked").length;
            var d = $("input[name='chkInputCheckedCSH[]']:checkbox").length;
            $("#selectAllCheckBoxCSH").prop("checked", !(c !== d));
        });

        $('#table-congdan tbody tr input[name="chkInputCheckedTG[]"]').unbind("change").change(function () {
            var c = $("input[name='chkInputCheckedTG[]']:checkbox:checked").length;
            var d = $("input[name='chkInputCheckedTG[]']:checkbox").length;
            $("#selectAllCheckBoxTG").prop("checked", !(c !== d));
        });

        $("#btnAddCongDan").unbind("click").click(function (event) {
            event.preventDefault();
            if ($('input[name="chkInputCheckedCSH[]"]:checked').length > 0) {
                $('input[name="chkInputCheckedCSH[]"]').each(function () {
                    if ($(this).prop("checked")) {
                        var check = Common.DVC_QuyenTacGia_SearchCongDan.ListChuSoHuu.find(x => x.ChuSoHuuID === $(this).closest("tr").data("id"));
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
                        //    Common.DVC_QuyenTacGia_SearchCongDan.ListChuSoHuu.push({
                        //        ChuSoHuuID: $(this).closest("tr").data("id"),
                        //        HoVaTen: $(this).closest("tr").data("name"),
                        //        QuocTich: $(this).closest("tr").data("country"),
                        //        SoCMND: $(this).closest("tr").data("cccd"),
                        //        SoDKKD: $(this).closest("tr").data("dkkd"),
                        //        DiaChi: $(this).closest("tr").data("address")
                        //    });
                        //}

                        if (typeof check == 'undefined') {
                            Common.DVC_QuyenTacGia_SearchCongDan.ListChuSoHuu.push({
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
            if ($('input[name="chkInputCheckedTG[]"]:checked').length > 0) {
                $('input[name="chkInputCheckedTG[]"]').each(function () {
                    if ($(this).prop("checked")) {
                        var check = Common.DVC_QuyenTacGia_SearchCongDan.ListTacGia.find(x => x.TacGiaID === $(this).closest("tr").data("id"));
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
                        //    Common.DVC_QuyenTacGia_SearchCongDan.ListTacGia.push({
                        //        TacGiaID: $(this).closest("tr").data("id"),
                        //        HoVaTen: $(this).closest("tr").data("name"),
                        //        QuocTich: $(this).closest("tr").data("country"),
                        //        SoCMND: $(this).closest("tr").data("cccd"),
                        //        DiaChi: $(this).closest("tr").data("address")
                        //    });
                        //}
                        if (typeof check == 'undefined') {
                            Common.DVC_QuyenTacGia_SearchCongDan.ListTacGia.push({
                                TacGiaID: $(this).closest("tr").data("id"),
                                HoVaTen: $(this).closest("tr").data("name"),
                                QuocTich: $(this).closest("tr").data("country"),
                                SoCMND: $(this).closest("tr").data("cccd"),
                                DiaChi: $(this).closest("tr").data("address")
                            });
                        }
                    }
                });
            }
            Common.DVC_QuyenTacGia_SearchCongDan.LoadTableChuSoHuu();
            Common.DVC_QuyenTacGia_SearchCongDan.LoadTableTacGia();
            Common.DVC_QuyenTacGia_SearchCongDan.HideDialog();
            return false;
        });

        $(".btn-update-congdan").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.DVC_QuyenTacGia_SearchCongDan.Url.UpdateCongDan,
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
                    Common.DVC_QuyenTacGia_SearchCongDan.RegisterEvent();
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
                    url: Common.DVC_QuyenTacGia_SearchCongDan.Url.SaveCongDan,
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
                                Common.DVC_QuyenTacGia_SearchCongDan.SubmitForm();
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
        Common.DVC_QuyenTacGia_SearchCongDan.SubmitForm();
        Common.ShowLoading(true);
        Common.Ajax({
            url: Common.DVC_QuyenTacGia_SearchCongDan.Url.UpdateCongDan,
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
                Common.DVC_QuyenTacGia_SearchCongDan.RegisterEvent();
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
        };
        var formdata = new FormData();
        formdata.append('modelSearch', JSON.stringify(model));
        Common.Ajax({
            type: 'POST',
            url: Common.DVC_QuyenTacGia_SearchCongDan.Url.SearchCongDan,
            data: formdata,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            dataType: 'html',
        }, function (result) {
            $("#result-searchcongdan").empty();
            $("#result-searchcongdan").html(result);
            Common.DVC_QuyenTacGia_SearchCongDan.ShowDialog();
            $('input[name="chkInputCheckedCSH[]"]').each(function () {
                var check = Common.DVC_QuyenTacGia_SearchCongDan.ListChuSoHuu.find(x => x.ChuSoHuuID === $(this).closest("tr").data("id"));
                if (typeof check != 'undefined') {
                    $(this).prop("checked", true);
                }
            });
            $('input[name="chkInputCheckedTG[]"]').each(function () {
                var check = Common.DVC_QuyenTacGia_SearchCongDan.ListTacGia.find(x => x.TacGiaID === $(this).closest("tr").data("id"));
                if (typeof check != 'undefined') {
                    $(this).prop("checked", true);
                }
            });
        });
        return false;
    },
    Paging: function (page) {
        Common.DVC_QuyenTacGia_SearchCongDan.SetPage(page);
        Common.DVC_QuyenTacGia_SearchCongDan.SubmitForm();
        return false;
    },
    SetPage: function (page) {
        $("#dialogSearchCongDan").find("input[name='PageIndex']").val(page);
    },
    SetPageSize: function (page) {
        $("#idFormSearchCongDan").find("input[name='PageSize']").val(page);
    },
    ChangePageSize: function (e) {
        Common.DVC_QuyenTacGia_SearchCongDan.SetPageSize($(e).val());
        Common.DVC_QuyenTacGia_SearchCongDan.SubmitForm();
        return false;
    },
    ShowDialog: function () {
        $("#dialogSearchCongDan").modal("show")
        Common.DVC_QuyenTacGia_SearchCongDan.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogSearchCongDan").modal("hide");
    },
    LoadTableChuSoHuu: function () {
        var html = '';
        if (Common.DVC_QuyenTacGia_SearchCongDan.ListChuSoHuu != null && Common.DVC_QuyenTacGia_SearchCongDan.ListChuSoHuu.length > 0) {
            for (i = 0; i < Common.DVC_QuyenTacGia_SearchCongDan.ListChuSoHuu.length; i++) {
                var chuSoHuu = Common.DVC_QuyenTacGia_SearchCongDan.ListChuSoHuu[i];
                html += '<tr>';
                html += '<td class"cursor" onclick="Common.DVC_QuyenTacGia_SearchCongDan.DetailCongDan(' + chuSoHuu.ChuSoHuuID + ')" data-label="Họ tên">' + chuSoHuu.HoVaTen + '</td>';
                html += '<td data-label="Quốc tịch">' + chuSoHuu.QuocTich + '</td>';
                html += '<td data-label="CCCD/Hộ chiếu">' + chuSoHuu.SoCMND + '</td>';
                html += '<td data-label="Số ĐKKD">' + chuSoHuu.SoDKKD + '</td>';
                html += '<td data-label="Địa chỉ">' + chuSoHuu.DiaChi + '</td>';
                html += '<td data-label="Thao tác" class="text-center">';
                html += '<button title="Xóa chủ sở hữu" data-id="' + chuSoHuu.ChuSoHuuID + '" class="btn btn-danger btn-sm" type="button" onclick="Common.DVC_QuyenTacGia_SearchCongDan.RemovedChuSoHuu(' + i + ')"><i class="far fa-trash-alt"></i></button>';
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
    LoadTableTacGia: function () {
        var html = '';
        if (Common.DVC_QuyenTacGia_SearchCongDan.ListTacGia != null && Common.DVC_QuyenTacGia_SearchCongDan.ListTacGia.length > 0) {
            for (i = 0; i < Common.DVC_QuyenTacGia_SearchCongDan.ListTacGia.length; i++) {
                var tacGia = Common.DVC_QuyenTacGia_SearchCongDan.ListTacGia[i];
                html += '<tr>';
                html += '<td class="cursor" onclick="Common.DVC_QuyenTacGia_SearchCongDan.DetailCongDan(' + tacGia.TacGiaID + ')" data-label="Họ tên">' + tacGia.HoVaTen + '</td>';
                html += '<td data-label="Quốc tịch">' + tacGia.QuocTich + '</td>';
                html += '<td data-label="CCCD/Hộ chiếu">' + tacGia.SoCMND + '</td>';
                html += '<td data-label="Địa chỉ">' + tacGia.DiaChi + '</td>';
                html += '<td data-label="Thao tác" class="text-center">';
                html += '<button title="Xóa tác giả" data-id="' + tacGia.TacGiaID + '" class="btn btn-danger btn-sm" type="button" onclick="Common.DVC_QuyenTacGia_SearchCongDan.RemovedTacGia(' + i + ')"><i class="far fa-trash-alt"></i></button>';
                html += '</td>';
                html += '</tr>';
            }
        }
        else {
            html += '<tr><td colspan="5"><div class="alert alert-info mt-3" role="alert"> Không có dữ liệu</div></td></tr>';
        }
        //$("#tableTacGia tbody").find("tr").empty();
        $("#tableTacGia tbody").empty();
        $('#tableTacGia').append(html);
    },
    RemovedChuSoHuu: function (id) {
        Common.DVC_QuyenTacGia_SearchCongDan.ListChuSoHuu.splice(id, 1)
        Common.DVC_QuyenTacGia_SearchCongDan.LoadTableChuSoHuu();
        return false;
    },
    RemovedTacGia: function (id) {
        Common.DVC_QuyenTacGia_SearchCongDan.ListTacGia.splice(id, 1)
        Common.DVC_QuyenTacGia_SearchCongDan.LoadTableTacGia();
        return false;
    },
};

var DVC_QuyenTacGia_SearchCongDan = DVC_QuyenTacGia_SearchCongDan;
DVC_QuyenTacGia_SearchCongDan.constructor = DVC_QuyenTacGia_SearchCongDan;
