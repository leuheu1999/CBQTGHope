var QTG_QuyenTacGia_SearchTacGia = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
QTG_QuyenTacGia_SearchTacGia.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;
        Common.Init();

        $("#btnSearchTacGia").unbind("click").click(function () {
            Common.QTG_QuyenTacGia_SearchTacGia.SubmitForm();
            return false;
        });

        $("#btnInsertTacGia").unbind("click").click(function () {
            $("#dialogSearchTacGia").find("input[type=text], textarea").val("");
            $("#dialogSearchTacGia").find("input[type=hidden]").val("");
            $("#dialogSearchTacGia").find(".datepicker-share").datepicker("destroy");
            $("#dialogSearchTacGia").find(".datepicker-share").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn'
            });
            $("#dialogSearchTacGia").find(".datepicker-share").datepicker("refresh");
            $("#dialogSearchTacGia").find("select").val(null).trigger('change');
            $("#dialogSearchTacGia .modal-title").text("Thêm mới tác giả");
            $(".sh-search-tacgia").addClass("hidden");
            $(".sh-info-tacgia").removeClass("hidden");
        });

        $("#btnCancelTacGia").unbind("click").click(function () {
            $("#dialogSearchTacGia").find("input[type=text], textarea").val("");
            $("#dialogSearchTacGia").find("input[type=hidden]").val("");
            $("#dialogSearchTacGia").find(".datepicker-share").datepicker("destroy");
            $("#dialogSearchTacGia").find(".datepicker-share").datepicker({
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                useCurrent: false,
                language: 'vn'
            });
            $("#dialogSearchTacGia").find(".datepicker-share").datepicker("refresh");
            $("#dialogSearchTacGia").find("select").val(null).trigger('change');
            $("#dialogSearchTacGia .modal-title").text("Thông tin tác giả");
            $(".sh-search-tacgia").removeClass("hidden");
            $(".sh-info-tacgia").addClass("hidden");
        });

        $("#selectAllCheckBoxTG").unbind("change").change(function () {
            $('#table-tacgia input:checkbox').not(this).prop('checked', this.checked);
        });

        $('#table-tacgia tbody tr input[name="chkInputCheckedTG[]"]').unbind("change").change(function () {
            var c = $("input[name='chkInputCheckedTG[]']:checkbox:checked").length;
            var d = $("input[name='chkInputCheckedTG[]']:checkbox").length;
            $("#selectAllCheckBoxTG").prop("checked", !(c !== d));
        });

        $("#btnAddTacGia").unbind("click").click(function (event) {
            event.preventDefault();
            if ($('input[name="chkInputCheckedTG[]"]:checked').length > 0) {
                $('input[name="chkInputCheckedTG[]"]').each(function () {
                    if ($(this).prop("checked")) {
                        var check = Common.QTG_QuyenTacGia_SearchTacGia.ListTacGia.find(x => x.TacGiaID === $(this).closest("tr").data("id"));
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
                            Common.QTG_QuyenTacGia_SearchTacGia.ListTacGia.push({
                                TacGiaID: $(this).closest("tr").data("id"),
                                HoVaTen: $(this).closest("tr").data("name"),
                                QuocTich: $(this).closest("tr").data("country"),
                                SoCMND: $(this).closest("tr").data("cccd"),
                                DiaChi: $(this).closest("tr").data("address")
                            });
                        }
                    }
                });
                Common.QTG_QuyenTacGia_SearchTacGia.LoadTableTacGia();
                Common.QTG_QuyenTacGia_SearchTacGia.HideDialog();
                return false;
            }
        });

        $(".btn-update-tacgia").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.QTG_QuyenTacGia_SearchTacGia.Url.UpdateTacGia,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'html',
                data: { id: id }
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    $("#form-update-tacgia").html(result);
                    $(".sh-search-tacgia").addClass("hidden");
                    $(".sh-info-tacgia").removeClass("hidden");
                    $("#dialogSearchTacGia .modal-title").text("Chỉnh sửa thông tin tác giả");
                    Common.QTG_QuyenTacGia_SearchTacGia.RegisterEvent();
                }
            }
            );
            return false;
        });

        $("#btnSaveTacGia").unbind("click").click(function () {
            var form = $("#form-update-tacgia");
            Common.ValidatorForm("#form-update-tacgia");
            if ($("#form-update-tacgia").find(".field-validation-error").length == 0) {
                var _model = {
                    TacGiaID: form.find("input[name='TacGiaID']").val() || 0,
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
                    url: Common.QTG_QuyenTacGia_SearchTacGia.Url.SaveTacGia,
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
                                $("#dialogSearchTacGia").find("input[type=text], textarea").val("");
                                $("#dialogSearchTacGia").find("select").val(null).trigger('change');
                                $("#dialogSearchTacGia .modal-title").text("Thông tin tác giả");
                                $(".sh-search-tacgia").removeClass("hidden");
                                $(".sh-info-tacgia").addClass("hidden");
                                Common.QTG_QuyenTacGia_SearchTacGia.SubmitForm();
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
        var form = $("#idFormSearchTacGia");
        var pageIndex = $("#dialogSearchTacGia").find("input[name='PageIndex']").val() > 0 ? $("#dialogSearchTacGia").find("input[name='PageIndex']").val() : 1;
        var pageSize = $("#dialogSearchTacGia").find("input[name='PageSize']").val() > 0 ? $("#dialogSearchTacGia").find("input[name='PageSize']").val() : 10;
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
            url: Common.QTG_QuyenTacGia_SearchTacGia.Url.SearchTacGia,
            data: formdata,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            dataType: 'html',
        }, function (result) {
            $("#result-searchtacgia").empty();
            $("#result-searchtacgia").html(result);
            Common.QTG_QuyenTacGia_SearchTacGia.ShowDialog();
        });
        return false;
    },
    Paging: function (page) {
        Common.QTG_QuyenTacGia_SearchTacGia.SetPage(page);
        Common.QTG_QuyenTacGia_SearchTacGia.SubmitForm();
        return false;
    },
    SetPage: function (page) {
        $("#dialogSearchTacGia").find("input[name='PageIndex']").val(page);
    },
    SetPageSize: function (page) {
        $("#idFormSearchTacGia").find("input[name='PageSize']").val(page);
    },
    ChangePageSize: function (e) {
        Common.QTG_QuyenTacGia_SearchTacGia.SetPageSize($(e).val());
        Common.QTG_QuyenTacGia_SearchTacGia.SubmitForm();
        return false;
    },
    ShowDialog: function () {
        $("#dialogSearchTacGia").modal("show")
        Common.QTG_QuyenTacGia_SearchTacGia.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogSearchTacGia").modal("hide");
    },
    LoadTableTacGia: function () {
        var html = '';
        if (Common.QTG_QuyenTacGia_SearchTacGia.ListTacGia != null && Common.QTG_QuyenTacGia_SearchTacGia.ListTacGia.length > 0) {
            for (i = 0; i < Common.QTG_QuyenTacGia_SearchTacGia.ListTacGia.length; i++) {
                var tacGia = Common.QTG_QuyenTacGia_SearchTacGia.ListTacGia[i];
                html += '<tr>';
                html += '<td data-label="Họ tên">' + tacGia.HoVaTen + '</td>';
                html += '<td data-label="Quốc tịch">' + tacGia.QuocTich + '</td>';
                html += '<td data-label="CCCD/Hộ chiếu">' + tacGia.SoCMND + '</td>';
                html += '<td data-label="Địa chỉ">' + tacGia.DiaChi + '</td>';
                html += '<td data-label="Thao tác" class="text-center">';
                html += '<button title="Xóa tác giả" data-id="' + tacGia.TacGiaID + '" class="btn btn-danger btn-sm" type="button" onclick="Common.QTG_QuyenTacGia_SearchTacGia.RemovedTacGia(' + i + ')"><i class="far fa-trash-alt"></i></button>';
                html += '</td>';
                html += '</tr>';
            }
        }
        else {
            html += '<tr><td colspan="5"><div class="alert alert-info mt-3" role="alert"> Không có dữ liệu</div></td></tr>';
        }
        $("#tableTacGia tbody").find("tr").empty();
        $('#tableTacGia').append(html);
    },
    RemovedTacGia: function (id) {
        Common.QTG_QuyenTacGia_SearchTacGia.ListTacGia.splice(id, 1)
        Common.QTG_QuyenTacGia_SearchTacGia.LoadTableTacGia();
        return false;
    },
};

var QTG_QuyenTacGia_SearchTacGia = QTG_QuyenTacGia_SearchTacGia;
QTG_QuyenTacGia_SearchTacGia.constructor = QTG_QuyenTacGia_SearchTacGia;
