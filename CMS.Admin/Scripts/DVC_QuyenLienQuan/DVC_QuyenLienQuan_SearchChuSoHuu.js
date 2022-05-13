var DVC_QuyenLienQuan_SearchChuSoHuu = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
DVC_QuyenLienQuan_SearchChuSoHuu.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;
        Common.Init();

        $("#btnSearchChuSoHuu").unbind("click").click(function () {
            Common.DVC_QuyenLienQuan_SearchChuSoHuu.SubmitForm();
            return false;
        });

        $("#btnInsertChuSoHuu").unbind("click").click(function () {
            $("#dialogSearchChuSoHuu").find("input[type=text], textarea").val("");
            $("#dialogSearchChuSoHuu").find("input[type=hidden]").val("");
            $("#dialogSearchChuSoHuu").find(".datepicker-share").datepicker("destroy");
            $("#dialogSearchChuSoHuu").find(".datepicker-share").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn'
            });
            $("#dialogSearchChuSoHuu").find(".datepicker-share").datepicker("refresh");
            $("#dialogSearchChuSoHuu").find("input[name='ChuSoHuuID']").val(0);
            $("#dialogSearchChuSoHuu").find("select").val(null).trigger('change');
            $("#dialogSearchChuSoHuu .modal-title").text("Thêm mới chủ sở hữu");
            $(".sh-search-chusohuu").addClass("hidden");
            $(".sh-info-chusohuu").removeClass("hidden");
        });

        $("#btnCancelChuSoHuu").unbind("click").click(function () {
            $("#dialogSearchChuSoHuu").find("input[type=text], textarea").val("");
            $("#dialogSearchChuSoHuu").find("input[type=hidden]").val("");
            $("#dialogSearchChuSoHuu").find(".datepicker-share").datepicker("destroy");
            $("#dialogSearchChuSoHuu").find(".datepicker-share").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn'
            });
            $("#dialogSearchChuSoHuu").find(".datepicker-share").datepicker("refresh");
            $("#dialogSearchChuSoHuu").find("select").val(null).trigger('change');
            $("#dialogSearchChuSoHuu .modal-title").text("Thông tin chủ sở hữu");
            $(".sh-search-chusohuu").removeClass("hidden");
            $(".sh-info-chusohuu").addClass("hidden");
        });

        $("#selectAllCheckBoxCSH").unbind("change").change(function () {
            $('#table-chusohuu input:checkbox').not(this).prop('checked', this.checked);
        });

        $('#table-chusohuu tbody tr input[name="chkInputCheckedCSH[]"]').unbind("change").change(function () {
            var c = $("input[name='chkInputCheckedCSH[]']:checkbox:checked").length;
            var d = $("input[name='chkInputCheckedCSH[]']:checkbox").length;
            $("#selectAllCheckBoxCSH").prop("checked", !(c !== d));
        });

        $("#btnAddChuSoHuu").unbind("click").click(function (event) {
            event.preventDefault();
            if ($('input[name="chkInputCheckedCSH[]"]:checked').length > 0) {
                $('input[name="chkInputCheckedCSH[]"]').each(function () {
                    if ($(this).prop("checked")) {
                        var check = Common.DVC_QuyenLienQuan_SearchChuSoHuu.ListChuSoHuu.find(x => x.ChuSoHuuID === $(this).closest("tr").data("id"));
                        if (typeof check != 'undefined') {
                            $.notify({
                                // options
                                message: 'Chủ sở hữu ' + check.HoVaTen + ' đã được chọn!!!'
                            }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                            return false;
                        }
                        else {
                            Common.DVC_QuyenLienQuan_SearchChuSoHuu.ListChuSoHuu.push({
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
                Common.DVC_QuyenLienQuan_SearchChuSoHuu.LoadTableChuSoHuu();
                Common.DVC_QuyenLienQuan_SearchChuSoHuu.HideDialog();
                return false;
            }
        });

        $(".btn-update-chusohuu").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.DVC_QuyenLienQuan_SearchChuSoHuu.Url.UpdateChuSoHuu,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'html',
                data: { id: id }
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    $("#form-update-chusohuu").html(result);
                    $(".sh-search-chusohuu").addClass("hidden");
                    $(".sh-info-chusohuu").removeClass("hidden");
                    $("#dialogSearchChuSoHuu .modal-title").text("Chỉnh sửa thông tin chủ sở hữu");
                    Common.DVC_QuyenLienQuan_SearchChuSoHuu.RegisterEvent();
                }
            }
            );
            return false;
        });

        $("#btnSaveChuSoHuu").unbind("click").click(function () {
            var form = $("#form-update-chusohuu");
            Common.ValidatorForm("#form-update-chusohuu");
            if ($("#form-update-chusohuu").find(".field-validation-error").length == 0) {
                var _model = {
                    ChuSoHuuID: form.find("input[name='ChuSoHuuID']").val() || 0,
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
                    DiaChi: form.find("input[name='DiaChi']").val() || null,
                };
                var formdata = new FormData();
                formdata.append('modelThongTin', JSON.stringify(_model));
                Common.ShowLoading(true);
                $.ajax({
                    type: 'POST',
                    url: Common.DVC_QuyenLienQuan_SearchChuSoHuu.Url.SaveChuSoHuu,
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
                                $("#dialogSearchChuSoHuu").find("input[type=text], textarea").val("");
                                $("#dialogSearchChuSoHuu").find("select").val(null).trigger('change');
                                $("#dialogSearchChuSoHuu .modal-title").text("Thông tin chủ sở hữu");
                                $(".sh-search-chusohuu").removeClass("hidden");
                                $(".sh-info-chusohuu").addClass("hidden");
                                Common.DVC_QuyenLienQuan_SearchChuSoHuu.SubmitForm();
                            }
                            else if (result.status == false && result.checkCCCD == true) {
                                $.notify({
                                    // options
                                    message: 'Số CCCD/Hộ chiếu cơ sở đã tồn tại!!!'
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
    SubmitForm: function () {
        var form = $("#idFormSearchChuSoHuu");
        var pageIndex = $("#dialogSearchChuSoHuu").find("input[name='PageIndex']").val() > 0 ? $("#dialogSearchChuSoHuu").find("input[name='PageIndex']").val() : 1;
        var pageSize = $("#dialogSearchChuSoHuu").find("input[name='PageSize']").val() > 0 ? $("#dialogSearchChuSoHuu").find("input[name='PageSize']").val() : 10;
        var model = {
            pageIndex: pageIndex,
            pageSize: pageSize,
            HoVaTen: form.find("input[name='HoVaTen']").val() || null,
            SoCMND: form.find("input[name='SoCMND']").val() || null,
            NgayCap: form.find("input[name='NgayCap']").val() || null,
            SoDKKD: form.find("input[name='SoDKKD']").val() || null,
            NgayCapDKKD: form.find("input[name='NgayCapDKKD']").val() || null,
            DiaChi: form.find("input[name='DiaChi']").val() || null,
        };
        var formdata = new FormData();
        formdata.append('modelSearch', JSON.stringify(model));
        Common.Ajax({
            type: 'POST',
            url: Common.DVC_QuyenLienQuan_SearchChuSoHuu.Url.SearchChuSoHuu,
            data: formdata,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            dataType: 'html',
        }, function (result) {
            $("#result-searchchusohuu").empty();
            $("#result-searchchusohuu").html(result);
            Common.DVC_QuyenLienQuan_SearchChuSoHuu.ShowDialog();
        });
        return false;
    },
    Paging: function (page) {
        Common.DVC_QuyenLienQuan_SearchChuSoHuu.SetPage(page);
        Common.DVC_QuyenLienQuan_SearchChuSoHuu.SubmitForm();
        return false;
    },
    SetPage: function (page) {
        $("#dialogSearchChuSoHuu").find("input[name='PageIndex']").val(page);
    },
    SetPageSize: function (page) {
        $("#idFormSearchChuSoHuu").find("input[name='PageSize']").val(page);
    },
    ChangePageSize: function (e) {
        Common.DVC_QuyenLienQuan_SearchChuSoHuu.SetPageSize($(e).val());
        Common.DVC_QuyenLienQuan_SearchChuSoHuu.SubmitForm();
        return false;
    },
    ShowDialog: function () {
        $("#dialogSearchChuSoHuu").modal("show")
        Common.DVC_QuyenLienQuan_SearchChuSoHuu.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogSearchChuSoHuu").modal("hide");
    },
    LoadTableChuSoHuu: function () {
        var html = '';
        if (Common.DVC_QuyenLienQuan_SearchChuSoHuu.ListChuSoHuu != null && Common.DVC_QuyenLienQuan_SearchChuSoHuu.ListChuSoHuu.length > 0) {
            for (i = 0; i < Common.DVC_QuyenLienQuan_SearchChuSoHuu.ListChuSoHuu.length; i++) {
                var chuSoHuu = Common.DVC_QuyenLienQuan_SearchChuSoHuu.ListChuSoHuu[i];
                html += '<tr>';
                html += '<td data-label="Họ tên">' + chuSoHuu.HoVaTen + '</td>';
                html += '<td data-label="Quốc tịch">' + chuSoHuu.QuocTich + '</td>';
                html += '<td data-label="CCCD/Hộ chiếu">' + chuSoHuu.SoCMND + '</td>';
                html += '<td data-label="Số ĐKKD">' + chuSoHuu.SoDKKD + '</td>';
                html += '<td data-label="Địa chỉ">' + chuSoHuu.DiaChi + '</td>';
                html += '<td data-label="Thao tác" class="text-center">';
                html += '<button title="Xóa chủ sở hữu" data-id="' + chuSoHuu.ChuSoHuuID + '" class="btn btn-danger btn-sm" type="button" onclick="Common.DVC_QuyenLienQuan_SearchChuSoHuu.RemovedChuSoHuu(' + i + ')"><i class="far fa-trash-alt"></i></button>';
                html += '</td>';
                html += '</tr>';
            }
        }
        else {
            html += '<tr><td colspan="6"><div class="alert alert-info mt-3" role="alert"> Không có dữ liệu</div></td></tr>';
        }
        $("#tableChuSoHuu tbody").find("tr").empty();
        $('#tableChuSoHuu').append(html);
    },
    RemovedChuSoHuu: function (id) {
        Common.DVC_QuyenLienQuan_SearchChuSoHuu.ListChuSoHuu.splice(id, 1)
        Common.DVC_QuyenLienQuan_SearchChuSoHuu.LoadTableChuSoHuu();
        return false;
    },
};

var DVC_QuyenLienQuan_SearchChuSoHuu = DVC_QuyenLienQuan_SearchChuSoHuu;
DVC_QuyenLienQuan_SearchChuSoHuu.constructor = DVC_QuyenLienQuan_SearchChuSoHuu;
