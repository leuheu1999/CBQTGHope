var QLQ_QuyenLienQuan_SearchNguoiBieuDien = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
QLQ_QuyenLienQuan_SearchNguoiBieuDien.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;
        Common.Init();

        $("#btnSearchNguoiBieuDien").unbind("click").click(function () {
            Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.SubmitForm();
            return false;
        });

        $("#btnInsertNguoiBieuDien").unbind("click").click(function () {
            $("#dialogSearchNguoiBieuDien").find("input[type=text], textarea").val("");
            $("#dialogSearchNguoiBieuDien").find("input[type=hidden]").val("");
            $("#dialogSearchNguoiBieuDien").find(".datepicker-share").datepicker("destroy");
            $("#dialogSearchNguoiBieuDien").find(".datepicker-share").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn'
            });
            $("#dialogSearchNguoiBieuDien").find(".datepicker-share").datepicker("refresh");
            $("#dialogSearchNguoiBieuDien").find("select").val(null).trigger('change');
            $("#dialogSearchNguoiBieuDien .modal-title").text("Thêm mới người biểu diễn");
            $(".sh-search-nguoibieudien").addClass("hidden");
            $(".sh-info-nguoibieudien").removeClass("hidden");
        });

        $("#btnCancelNguoiBieuDien").unbind("click").click(function () {
            $("#dialogSearchNguoiBieuDien").find("input[type=text], textarea").val("");
            $("#dialogSearchNguoiBieuDien").find("input[type=hidden]").val("");
            $("#dialogSearchNguoiBieuDien").find(".datepicker-share").datepicker("destroy");
            $("#dialogSearchNguoiBieuDien").find(".datepicker-share").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn'
            });
            $("#dialogSearchNguoiBieuDien").find(".datepicker-share").datepicker("refresh");
            $("#dialogSearchNguoiBieuDien").find("select").val(null).trigger('change');
            $("#dialogSearchNguoiBieuDien .modal-title").text("Thông tin người biểu diễn");
            $(".sh-search-nguoibieudien").removeClass("hidden");
            $(".sh-info-nguoibieudien").addClass("hidden");
        });

        $("#selectAllCheckBoxNBD").unbind("change").change(function () {
            $('#table-nguoibieudien input:checkbox').not(this).prop('checked', this.checked);
        });

        $('#table-nguoibieudien tbody tr input[name="chkInputCheckedNBD[]"]').unbind("change").change(function () {
            var c = $("input[name='chkInputCheckedNBD[]']:checkbox:checked").length;
            var d = $("input[name='chkInputCheckedNBD[]']:checkbox").length;
            $("#selectAllCheckBoxNBD").prop("checked", !(c !== d));
        });

        $("#btnAddNguoiBieuDien").unbind("click").click(function (event) {
            event.preventDefault();
            if ($('input[name="chkInputCheckedNBD[]"]:checked').length > 0) {
                $('input[name="chkInputCheckedNBD[]"]').each(function () {
                    if ($(this).prop("checked")) {
                        var check = Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.ListNguoiBieuDien.find(x => x.NguoiBieuDienID === $(this).closest("tr").data("id"));
                        if (typeof check != 'undefined') {
                            $.notify({
                                // options
                                message: 'Tác giả ' + check.HoVaTen + ' đã được chọn!!!'
                            }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                            return false;
                        }
                        else {
                            Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.ListNguoiBieuDien.push({
                                NguoiBieuDienID: $(this).closest("tr").data("id"),
                                HoVaTen: $(this).closest("tr").data("name"),
                                QuocTich: $(this).closest("tr").data("country"),
                                SoCMND: $(this).closest("tr").data("cccd"),
                                DiaChi: $(this).closest("tr").data("address")
                            });
                        }
                    }
                });
                Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.LoadTableNguoiBieuDien();
                Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.HideDialog();
                return false;
            }
        });

        $(".btn-update-nguoibieudien").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.Url.UpdateNguoiBieuDien,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'html',
                data: { id: id }
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    $("#form-update-nguoibieudien").html(result);
                    $(".sh-search-nguoibieudien").addClass("hidden");
                    $(".sh-info-nguoibieudien").removeClass("hidden");
                    $("#dialogSearchNguoiBieuDien .modal-title").text("Chỉnh sửa thông tin người biểu diễn");
                    Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.RegisterEvent();
                }
            }
            );
            return false;
        });

        $("#btnSaveNguoiBieuDien").unbind("click").click(function () {
            var form = $("#form-update-nguoibieudien");
            Common.ValidatorForm("#form-update-nguoibieudien");
            if ($("#form-update-nguoibieudien").find(".field-validation-error").length == 0) {
                var _model = {
                    NguoiBieuDienID: form.find("input[name='NguoiBieuDienID']").val() || 0,
                    HoVaTen: form.find("input[name='HoVaTen']").val() || null,
                    QuocTichID: form.find("select[name='QuocTichID']").val() || 0,
                    QuocTich: form.find("select[name='QuocTichID'] option:selected").text() || null,
                    SoCMND: form.find("input[name='SoCMND']").val() || null,
                    NgayCapCMND: form.find("input[name='NgayCapCMND']").val() || null,
                    NoiCapID: form.find("select[name='NoiCapID']").val() || 0,
                    NoiCap: form.find("select[name='NoiCapID'] option:selected").text() || null,
                    DiaChi: form.find("input[name='DiaChi']").val() || null,
                    ButDanh: form.find("input[name='ButDanh']").val() || null,
                };
                var formdata = new FormData();
                formdata.append('modelThongTin', JSON.stringify(_model));
                Common.ShowLoading(true);
                $.ajax({
                    type: 'POST',
                    url: Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.Url.SaveNguoiBieuDien,
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
                                $("#dialogSearchNguoiBieuDien").find("input[type=text], textarea").val("");
                                $("#dialogSearchNguoiBieuDien").find("select").val(null).trigger('change');
                                $("#dialogSearchNguoiBieuDien .modal-title").text("Thông tin người biểu diễn");
                                $(".sh-search-nguoibieudien").removeClass("hidden");
                                $(".sh-info-nguoibieudien").addClass("hidden");
                                Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.SubmitForm();
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
        var form = $("#idFormSearchNguoiBieuDien");
        var pageIndex = $("#dialogSearchNguoiBieuDien").find("input[name='PageIndex']").val() > 0 ? $("#dialogSearchNguoiBieuDien").find("input[name='PageIndex']").val() : 1;
        var pageSize = $("#dialogSearchNguoiBieuDien").find("input[name='PageSize']").val() > 0 ? $("#dialogSearchNguoiBieuDien").find("input[name='PageSize']").val() : 10;
        var model = {
            pageIndex: pageIndex,
            pageSize: pageSize,
            HoVaTen: form.find("input[name='HoVaTen']").val() || null,
            SoCMND: form.find("input[name='SoCMND']").val() || null,
            NgayCap: form.find("input[name='NgayCap']").val() || null,
            DiaChi: form.find("input[name='DiaChi']").val() || null,
            ButDanh: form.find("input[name='ButDanh']").val() || null,
        };
        var formdata = new FormData();
        formdata.append('modelSearch', JSON.stringify(model));
        Common.Ajax({
            type: 'POST',
            url: Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.Url.SearchNguoiBieuDien,
            data: formdata,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            dataType: 'html',
        }, function (result) {
            $("#result-searchnguoibieudien").empty();
            $("#result-searchnguoibieudien").html(result);
            Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.ShowDialog();
        });
        return false;
    },
    Paging: function (page) {
        Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.SetPage(page);
        Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.SubmitForm();
        return false;
    },
    SetPage: function (page) {
        $("#dialogSearchNguoiBieuDien").find("input[name='PageIndex']").val(page);
    },
    SetPageSize: function (page) {
        $("#idFormSearchNguoiBieuDien").find("input[name='PageSize']").val(page);
    },
    ChangePageSize: function (e) {
        Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.SetPageSize($(e).val());
        Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.SubmitForm();
        return false;
    },
    ShowDialog: function () {
        $("#dialogSearchNguoiBieuDien").modal("show")
        Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogSearchNguoiBieuDien").modal("hide");
    },
    LoadTableNguoiBieuDien: function () {
        var html = '';
        if (Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.ListNguoiBieuDien != null && Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.ListNguoiBieuDien.length > 0) {
            for (i = 0; i < Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.ListNguoiBieuDien.length; i++) {
                var nguoiBieuDien = Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.ListNguoiBieuDien[i];
                html += '<tr>';
                html += '<td data-label="Họ tên">' + nguoiBieuDien.HoVaTen + '</td>';
                html += '<td data-label="Quốc tịch">' + nguoiBieuDien.QuocTich + '</td>';
                html += '<td data-label="CCCD/Hộ chiếu">' + nguoiBieuDien.SoCMND + '</td>';
                html += '<td data-label="Địa chỉ">' + nguoiBieuDien.DiaChi + '</td>';
                html += '<td data-label="Thao tác" class="text-center">';
                html += '<button title="Xóa người biểu diễn" data-id="' + nguoiBieuDien.NguoiBieuDienID + '" class="btn btn-danger btn-sm" type="button" onclick="Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.RemovedNguoiBieuDien(' + i + ')"><i class="far fa-trash-alt"></i></button>';
                html += '</td>';
                html += '</tr>';
            }
        }
        else {
            html += '<tr><td colspan="5"><div class="alert alert-info mt-3" role="alert"> Không có dữ liệu</div></td></tr>';
        }
        $("#tableNguoiBieuDien tbody").find("tr").empty();
        $('#tableNguoiBieuDien').append(html);
    },
    RemovedNguoiBieuDien: function (id) {
        Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.ListNguoiBieuDien.splice(id, 1)
        Common.QLQ_QuyenLienQuan_SearchNguoiBieuDien.LoadTableNguoiBieuDien();
        return false;
    },
};

var QLQ_QuyenLienQuan_SearchNguoiBieuDien = QLQ_QuyenLienQuan_SearchNguoiBieuDien;
QLQ_QuyenLienQuan_SearchNguoiBieuDien.constructor = QLQ_QuyenLienQuan_SearchNguoiBieuDien;
