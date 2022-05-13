var DVC_QuyenLienQuan_SearchDinhKem = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
DVC_QuyenLienQuan_SearchDinhKem.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;
        Common.Init();

        $("#btnSearchDinhKem").unbind("click").click(function () {
            Common.DVC_QuyenLienQuan_SearchDinhKem.SubmitForm();
            return false;
        });

        $("#btnAddNewRowDK").unbind("click").click(function (event) {
            event.preventDefault();
            //số thứ tự hiện tại
            let numberCurrent = parseInt($("#tableUpdateDinhKem tbody tr" + ":last-child").find(".row-number").text()) + 1;
            if ($("#tableUpdateDinhKem tbody").find("tr.row-none").length == 1) {
                $("#tableUpdateDinhKem tbody").find("tr.row-none").remove();
                numberCurrent = 1;
            }
            //add thêm dòng mới
            let tr = document.createElement('tr');
            tr.innerHTML = Common.DVC_QuyenLienQuan_SearchDinhKem.AddTrInTable(numberCurrent);
            $("#tableUpdateDinhKem tbody").append(tr);
            return false;
        });

        $(".btn-update-dinhkem").unbind("click").click(function () {
            var id = $(this).data("id");
            let tr = $(this).closest('tr');
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.DVC_QuyenLienQuan_SearchDinhKem.Url.UpdateDinhKem,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'json',
                data: { id: id }
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    if (result.status == true) {
                        if (!Common.Empty(result.data)) {
                            Common.DVC_QuyenLienQuan_SearchDinhKem.BindDataUpdate(tr, result.data);
                            return false;
                        }
                    }
                    else {
                        $.notify({
                            // options
                            message: 'Lấy dữ liệu không thành công!!!'
                        }, {
                            // settings
                            delay: 1000,
                            timer: 1000, type: 'danger'
                        });
                    }
                }
            });
            return false;
        });

        $(".btn-xoa-dinhkem").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.DVC_QuyenLienQuan_SearchDinhKem.Url.DeleteDinhKem,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'json',
                data: { id: id }
            }, function (result) {
                Common.ShowLoading(false);
                if (!Common.Empty(result)) {
                    if (result.status == true) {
                        $.notify({
                            // options
                            message: 'Xóa thành công.!!!'
                        }, {
                            //settings
                            delay: 1000,
                            timer: 1000, type: 'success'
                        });
                        Common.DVC_QuyenLienQuan_SearchDinhKem.SubmitForm();
                        return false;
                    }
                    else {
                        $.notify({
                            // options
                            message: 'Xóa không thành công!!!'
                        }, {
                            // settings
                            delay: 1000,
                            timer: 1000, type: 'danger'
                        });
                    }
                }
            });
            return false;
        });

        $("#selectAllCheckBoxDK").unbind("change").change(function () {
            $('#tableUpdateDinhKem input:checkbox').not(this).prop('checked', this.checked);
        });

        $('#tableUpdateDinhKem tbody tr input[name="chkInputCheckedDK[]"]').unbind("change").change(function () {
            var c = $("input[name='chkInputCheckedDK[]']:checkbox:checked").length;
            var d = $("input[name='chkInputCheckedDK[]']:checkbox").length;
            $("#selectAllCheckBoxDK").prop("checked", !(c !== d));
        });

        $("#btnAddDinhKem").unbind("click").click(function (event) {
            event.preventDefault();
            if ($('input[name="chkInputCheckedDK[]"]:checked').length > 0) {
                $('input[name="chkInputCheckedDK[]"]').each(function () {
                    if ($(this).prop("checked")) {
                        var check = Common.DVC_QuyenLienQuan_SearchDinhKem.ListDinhKem.find(x => x.DinhKemID === $(this).closest("tr").data("id"));
                        if (typeof check != 'undefined') {
                            $.notify({
                                // options
                                message: 'Đính kèm ' + check.Ten + ' đã được chọn!!!'
                            }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                            return false;
                        }
                        else {
                            Common.DVC_QuyenLienQuan_SearchDinhKem.ListDinhKem.push({
                                DinhKemID: $(this).closest("tr").data("id"),
                                Stt: $(this).closest("tr").data("stt"),
                                Ten: $(this).closest("tr").data("name"),
                                CreatedUser: $(this).closest("tr").data("user"),
                                Tag: $(this).closest("tr").data("tag"),
                                GhiChu: $(this).closest("tr").data("note"),
                                TenFile: $(this).closest("tr").data("tenfile"),
                                DuongDan: $(this).closest("tr").data("url"),
                                CreatedDate: $(this).closest("tr").data("date"),
                                IsMotCua: $(this).closest("tr").data("motcua"),
                            });
                        }
                    }
                });
                Common.DVC_QuyenLienQuan_SearchDinhKem.LoadTableDinhKem();
                Common.DVC_QuyenLienQuan_SearchDinhKem.HideDialog();
                return false;
            }
        });
    },
    SubmitForm: function () {
        var form = $("#idFormSearchDinhKem");
        var pageIndex = $("#dialogSearchDinhKem").find("input[name='PageIndex']").val() > 0 ? $("#dialogSearchDinhKem").find("input[name='PageIndex']").val() : 1;
        var pageSize = $("#dialogSearchDinhKem").find("input[name='PageSize']").val() > 0 ? $("#dialogSearchDinhKem").find("input[name='PageSize']").val() : 10;
        var model = {
            pageIndex: pageIndex,
            pageSize: pageSize,
            Ten: form.find("input[name='Ten']").val() || null,
            Tag: form.find("input[name='Tag']").val() || null,
            GhiChu: form.find("input[name='GhiChu']").val() || null
        };
        var formdata = new FormData();
        formdata.append('modelSearch', JSON.stringify(model));
        Common.Ajax({
            type: 'POST',
            url: Common.DVC_QuyenLienQuan_SearchDinhKem.Url.SearchDinhKem,
            data: formdata,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            dataType: 'html',
        }, function (result) {
            $("#result-searchdinhkem").empty();
            $("#result-searchdinhkem").html(result);
            Common.DVC_QuyenLienQuan_SearchDinhKem.ShowDialog();
        });
        return false;
    },
    Paging: function (page) {
        Common.DVC_QuyenLienQuan_SearchDinhKem.SetPage(page);
        Common.DVC_QuyenLienQuan_SearchDinhKem.SubmitForm();
        return false;
    },
    SetPage: function (page) {
        $("#dialogSearchDinhKem").find("input[name='PageIndex']").val(page);
    },
    SetPageSize: function (page) {
        $("#idFormSearchDinhKem").find("input[name='PageSize']").val(page);
    },
    ChangePageSize: function (e) {
        Common.DVC_QuyenLienQuan_SearchDinhKem.SetPageSize($(e).val());
        Common.DVC_QuyenLienQuan_SearchDinhKem.SubmitForm();
        return false;
    },
    ShowDialog: function () {
        $("#dialogSearchDinhKem").modal("show")
        Common.DVC_QuyenLienQuan_SearchDinhKem.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogSearchDinhKem").modal("hide");
    },
    AddTrInTable: function (stt) {
        var createdDate = new Date();
        var dd = String(createdDate.getDate()).padStart(2, '0');
        var mm = String(createdDate.getMonth() + 1).padStart(2, '0');
        var yyyy = createdDate.getFullYear();
        createdDate = dd + '/' + mm + '/' + yyyy;
        var createdUser = $("#idFormSearchDinhKem").find("input[name='CurrentUser']").val() || null;
        let html = '<td class="text-center"></td>';
        html += '<td class="row-number text-center">' + stt + '</td>';
        html += '<td><input type="text" name="' + stt + 'Ten" class="form-control form-control-sm" value=""></td>';
        html += '<td><input type="text" name="' + stt + 'CreatedUser" class="form-control form-control-sm" disabled value="' + createdUser + '"></td>';
        html += '<td><textarea type="text" name="' + stt + 'Tag" class="form-control form-control-sm" rows = 2 value=""></textarea></td>';
        html += '<td><textarea type="text" name="' + stt + 'GhiChu" class="form-control form-control-sm" rows = 2 value=""></textarea></td>';
        html += '<td class="text-center file-field" >'
            + '<button type = "button" class="btn btn-primary btn-block-sm btn-sm mt-md-3 mt-sm-3 uploadicon" onclick = "Common.DVC_QuyenLienQuan_SearchDinhKem.UploadFileClick(this)"><i class="fas fa-upload"></i></button>'
            + '<input hidden type="file" class="form-control-file" name="fileUpload' + stt + '" data-number="' + stt + '" accept=".pdf, .doc, .docx, .xls, xlsx" />'
            + '<div class="pt-3" name="urltepdinhkem' + stt + '" data-tenfile="" data-tenfilegoc="" data-duongdan=""></div>'
            + '</td>';
        html += '<td><input type="text" name="' + stt + 'CreatedDate" class="form-control form-control-sm" disabled value="' + createdDate + '"></td>';
        html += '<td class="text-center cursor" data-label="Thao tác">';
        html += '<button title="lưu" class="btn btn-success btn-sm mr-2" type="button" onclick="' + "Common.DVC_QuyenLienQuan_SearchDinhKem.SaveTrData(this," + stt + ",'Save')" + '"><i class="far fa-save"></i></button>';
        html += '<button title="hủy" class="btn btn-danger btn-sm" type="button" onclick="Common.DVC_QuyenLienQuan_SearchDinhKem.DeleteTrData(this)"><i class="fa fa-times"></i></button>';
        html += '</td>';

        return html;
    },
    EditTrInTable: function (item, stt) {
        let html = '<td class="text-center"><input type="checkbox" class="cursor" name="chkInputCheckedDK[]"></td>';
        html += '<td class="row-number text-center">' + stt + '</td>';
        html += '<td><input type="text" name="' + stt + 'Ten" class="form-control form-control-sm" value="' + item.Ten + '"></td>';
        html += '<td><input type="text" name="' + stt + 'CreatedUser" class="form-control form-control-sm" disabled value="' + item.CreatedUser + '"></td>';
        html += '<td><textarea type="text" name="' + stt + 'Tag" class="form-control form-control-sm" rows = 2 value="' + item.Tag + '">' + item.Tag + '</textarea></td>';
        html += '<td><textarea type="text" name="' + stt + 'GhiChu" class="form-control form-control-sm" rows = 2 value="' + item.GhiChu + '">' + item.GhiChu + '</textarea></td>';
        html += '<td class="text-center file-field" >'
            + '<button type = "button" class="btn btn-primary btn-block-sm btn-sm mt-md-3 mt-sm-3 uploadicon" onclick = "Common.DVC_QuyenLienQuan_SearchDinhKem.UploadFileClick(this)"><i class="fas fa-upload"></i></button>'
            + '<input hidden type="file" class="form-control-file" name="fileUpload' + stt + '" data-number="' + stt + '" accept=".pdf, .doc, .docx, .xls, xlsx" />'
            + '<div class="pt-3" name="urltepdinhkem' + stt + '" data-tenfile="' + item.TenTepTin + '" data-tenfilegoc="' + item.TenGoc + '" data-duongdan="' + item.DuongDan + '">'
            + '<a href="' + item.DuongDan + '" download="' + item.TenGoc + '">' + (item.TenGoc == null ? '' : item.TenGoc) + '</a></div > '
            + '</td>';
        html += '<td><input type="text" name="' + stt + 'CreatedDate" class="form-control form-control-sm" disabled value="' + item.CreatedDate + '"></td>';
        html += '<td class="text-center cursor" data-label="Thao tác">';
        html += '<button title="lưu" class="btn btn-success btn-sm mr-2" type="button" onclick="' + "Common.DVC_QuyenLienQuan_SearchDinhKem.SaveTrData(this," + stt + ",'Save')" + '"><i class="far fa-save"></i></button>';
        html += '<button title="Xóa" class="btn btn-danger btn-sm" type="button" data-id="' + item.DinhKemID + '"><i class="far fa-trash-alt"></i></button>';
        html += '</td>';
        return html;
    },
    UploadFileClick: function (that) {
        that.parentNode.querySelector(".form-control-file").click();
        $('.form-control-file').unbind("change").change(function (ev) {
            ev.preventDefault();
            Common.DVC_QuyenLienQuan_SearchDinhKem.UploadFileChange(this);
            return false;
        });
    },
    UploadFileChange: function (ev) {
        Common.ShowLoading(true);
        let numberCurrent = ev.getAttribute("data-number");
        var fileUploads = ev.files;
        if (fileUploads.length > 0) {
            var regex = new RegExp("(pdf)|(doc)|(docx)|(xls)|(xlsx)$");
            var fileName = fileUploads[0].name;
            let fileSize = fileUploads[0].size;
            var sizeMB = 20;
            if (!regex.test(Common.DVC_QuyenLienQuan_SearchDinhKem.GetTypeFile(fileName))) {
                $.notify({
                    // options
                    message: 'Vui lòng chọn tệp tin đúng định dạng!!'
                }, {
                    // settings
                    delay: 1000,
                    timer: 1000, type: 'danger'
                });
                Common.ShowLoading(false);
                ev.value = null;
                return false;
            }
            else if (fileSize > sizeMB * 1024 * 1024) {
                $.notify({
                    // options
                    message: 'Vui lòng chọn tệp tin không vượt quá ' + sizeMB + ' MB!!'
                }, {
                    // settings
                    delay: 1000,
                    timer: 1000, type: 'danger'
                });
                Common.ShowLoading(false);
                ev.value = null;
                return false;
            }
            else {
                var data = new FormData();
                if (fileUploads.length > 0) {
                    data.append('UploadFile', fileUploads[0], fileUploads[0].name);
                }
                Common.Ajax({
                    type: "POST",
                    url: Common.DVC_QuyenLienQuan_SearchDinhKem.Url.UploadFile,
                    dataType: 'json',
                    data: data,
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                }, function (data) {
                    Common.ShowLoading(false);
                    let urltepdinhkem = document.getElementsByName("urltepdinhkem" + numberCurrent)[0];
                    if (data) {
                        if (data.status) {
                            var str = '<a href="' + data.FileUrl + '" download>' + fileUploads[0].name + '</a>'
                            urltepdinhkem.innerHTML = str;
                            urltepdinhkem.setAttribute("data-tenfilegoc", fileUploads[0].name);
                            urltepdinhkem.setAttribute("data-tenfile", data.FileName);
                            urltepdinhkem.setAttribute("data-duongdan", data.FileUrl);
                            $.notify({
                                // options
                                message: "Upload file thành công!"
                            }, {
                                //settings
                                delay: 1000,
                                timer: 1000, type: 'success'
                            });
                        }
                        else {
                            urltepdinhkem.innerHTML = '';
                            urltepdinhkem.setAttribute("data-tenfilegoc", '');
                            urltepdinhkem.setAttribute("data-tenfile", '');
                            urltepdinhkem.setAttribute("data-duongdan", '');
                            $.notify({
                                // options
                                message: "Upload file lỗi.!!!"
                            }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                        }
                    }
                    else {
                        urltepdinhkem.innerHTML = '';
                        urltepdinhkem.setAttribute("data-tenfilegoc", '');
                        urltepdinhkem.setAttribute("data-tenfile", '');
                        urltepdinhkem.setAttribute("data-duongdan", '');
                        $.notify({
                            // options
                            message: "Upload file lỗi.!!!"
                        }, {
                            // settings
                            delay: 1000,
                            timer: 1000, type: 'danger'
                        });
                    }
                });
            }
        }
        else {
            let urltepdinhkem = document.getElementsByName("urltepdinhkem" + numberCurrent)[0];
            urltepdinhkem.innerHTML = '';
            urltepdinhkem.setAttribute("data-tenfilegoc", '');
            urltepdinhkem.setAttribute("data-tenfile", '');
            urltepdinhkem.setAttribute("data-duongdan", '');
            Common.ShowLoading(false);
        }
    },
    SaveTrData: function (that, stt, action) {
        let tr = $(that).closest("tr");
        let div = $(tr.find("div[name='urltepdinhkem" + stt + "']")[0])
        var _model = {
            DinhKemID: tr.data("id") || 0,
            Ten: $.trim(tr.find("input[name='" + stt + "Ten']").val()) || '',
            Tag: $.trim(tr.find("textarea[name='" + stt + "Tag']").val()) || '',
            GhiChu: $.trim(tr.find("textarea[name='" + stt + "GhiChu']").val()) || '',
            TenTepTin: div.data("tenfile") || '',
            TenGoc: div.data("tenfilegoc") || '',
            DuongDan: div.data("duongdan") || '',
        };
        var formdata = new FormData();
        formdata.append('modelThongTin', JSON.stringify(_model));
        Common.ShowLoading(true);
        $.ajax({
            type: 'POST',
            url: Common.DVC_QuyenLienQuan_SearchDinhKem.Url.SaveDinhKem,
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
                        Common.DVC_QuyenLienQuan_SearchDinhKem.SubmitForm();
                        return false;
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
    },
    DeleteTrData: function (that) {
        let tr = $(that).parents("tr");
        let html = "";
        let tbody = tr.parents("tbody");
        if (tbody.find("tr").length == 1) {
            html += '<tr class="row-none"><td colspan = "9"><div class="alert alert-info mt-3" role="alert">Không có dữ liệu</div></td ></tr>';
        }
        tr.remove();
        $("#tableUpdateDinhKem tbody").append(html);
    },
    BindDataUpdate: function (tr, model) {
        //số thứ tự hiện tại
        let numberCurrent = tr.find(".row-number").text();
        //remove td
        tr.find("td").remove();
        //add td
        let html = new Array();
        let row = Common.DVC_QuyenLienQuan_SearchDinhKem.EditTrInTable(model, numberCurrent);
        html.push(row);
        tr.append(html);
        $(tr).id = 'tr' + numberCurrent;
    },
    GetTypeFile: function (_filename) {
        var _name = '';
        var _f = (_filename != '') ? _filename.lastIndexOf('.') : -1;
        var _t = (_filename != '') ? _filename.length : -1;
        if (_filename != '' && _f > -1) {
            _name = _filename.substring((_f + 1), _t);
        } else {
            _name = '';
        }
        _name = (_name != '') ? _name.toLowerCase() : _name;
        return _name;
    },
    LoadTableDinhKem: function () {
        var html = '';
        if (Common.DVC_QuyenLienQuan_SearchDinhKem.ListDinhKem != null && Common.DVC_QuyenLienQuan_SearchDinhKem.ListDinhKem.length > 0) {
            for (i = 0; i < Common.DVC_QuyenLienQuan_SearchDinhKem.ListDinhKem.length; i++) {
                var dinhKem = Common.DVC_QuyenLienQuan_SearchDinhKem.ListDinhKem[i];
                html += '<tr data-id="' + dinhKem.DinhKemID + '">';
                html += '<td data-label="STT" class="text-center row-number">' + (i + 1) + '</td>';
                html += '<td data-label="Tên tệp tin">' + dinhKem.Ten + '</td>';
                html += '<td data-label="Người đính">' + dinhKem.CreatedUser + '</td>';
                html += '<td data-label="Tag">' + dinhKem.Tag + '</td>';
                html += '<td data-label="Ghi chú">' + dinhKem.GhiChu + '</td>';
                html += '<td data-label="Tập tin"><a href="' + dinhKem.DuongDan + '" target="_blank" download="' + dinhKem.TenFile + '">' + dinhKem.TenFile + '</a></td>';
                html += '<td data-label="Ngày đính kèm">' + dinhKem.CreatedDate + '</td>';
                html += '<td data-label="Thao tác" class="text-center">';
                if (dinhKem.IsMotCua)
                    html += '<button title="Xóa đính kèm" class="btn btn-danger btn-sm disabled" type="button"><i class="far fa-trash-alt"></i></button>';
                else
                    html += '<button title="Xóa đính kèm" data-id="' + dinhKem.DinhKemID + '" class="btn btn-danger btn-sm" type="button" onclick="Common.DVC_QuyenLienQuan_SearchDinhKem.RemovedDinhKem(' + i + ')"><i class="far fa-trash-alt"></i></button>';
                html += '</td>';
                html += '</tr>';
            }
        }
        else {
            html += '<tr><td colspan="8"><div class="alert alert-info mt-3" role="alert"> Không có dữ liệu</div></td></tr>';
        }
        $("#tableTepDinhKem tbody").find("tr").empty();
        $('#tableTepDinhKem').append(html);
    },
    RemovedDinhKem: function (id) {
        Common.DVC_QuyenLienQuan_SearchDinhKem.ListDinhKem.splice(id, 1)
        Common.DVC_QuyenLienQuan_SearchDinhKem.LoadTableDinhKem();
        return false;
    },
};

var DVC_QuyenLienQuan_SearchDinhKem = DVC_QuyenLienQuan_SearchDinhKem;
DVC_QuyenLienQuan_SearchDinhKem.constructor = DVC_QuyenLienQuan_SearchDinhKem;
