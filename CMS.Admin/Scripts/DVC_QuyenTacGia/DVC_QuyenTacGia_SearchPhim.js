var DVC_QuyenTacGia_SearchPhim = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
DVC_QuyenTacGia_SearchPhim.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;
        Common.Init();

        $("input:checkbox").on('click', function () {
            var $box = $(this);
            if ($box.is(":checked")) {
                var group = "input:checkbox[name='" + $box.attr("name") + "']";
                var name = $(this).data("name");
                $(group).prop("checked", false);
                $box.prop("checked", true);
                $("#TenPhim").val(name);
                $("#form-update-tt-tacpham").find("input[name='PhimStt']").val($(this).closest("tr").data("id"));
            } else {
                $box.prop("checked", false);
                $("#TenPhim").val("");
                $("#form-update-tt-tacpham").find("input[name='PhimStt']").val(-1);
            }
        });

        $("#btnSearchPhim").unbind("click").click(function () {
            Common.DVC_QuyenTacGia_SearchPhim.SubmitForm();
            return false;
        });

        $("#btnAddNewRowP").unbind("click").click(function (event) {
            event.preventDefault();
            //số thứ tự hiện tại
            let numberCurrent = parseInt($("#tableUpdatePhim tbody tr" + ":last-child").find(".row-number").text());
            if ($("#tableUpdatePhim tbody").find("tr.row-none").length == 1) {
                $("#tableUpdatePhim tbody").find("tr.row-none").remove();
                numberCurrent = 0;
            }
            //add thêm dòng mới
            let tr = document.createElement('tr');
            tr.innerHTML = Common.DVC_QuyenTacGia_SearchPhim.AddTrInTable(numberCurrent);
            $("#tableUpdatePhim tbody").append(tr);
            return false;
        });

        $(".btn-update-phim").unbind("click").click(function () {
            var id = $(this).data("id");
            let tr = $(this).closest('tr');
            Common.DVC_QuyenTacGia_SearchPhim.BindDataUpdate(tr, Common.DVC_QuyenTacGia_SearchPhim.ListPhim[id]);
            return false;
            //Common.ShowLoading(true);
            //Common.Ajax({
            //    url: Common.DVC_QuyenTacGia_SearchPhim.Url.UpdatePhim,
            //    type: "POST",
            //    ContentType: 'application/json; charset=utf-8',
            //    async: false,
            //    cache: false,
            //    dataType: 'json',
            //    data: { id: id }
            //}, function (result) {
            //    Common.ShowLoading(false);
            //    if (!Common.Empty(result)) {
            //        if (result.status == true) {
            //            if (!Common.Empty(result.data)) {
            //                Common.DVC_QuyenTacGia_SearchPhim.BindDataUpdate(tr, result.data);
            //                return false;
            //            }
            //        }
            //        else {
            //            $.notify({
            //                // options
            //                message: 'Lấy dữ liệu không thành công!!!'
            //            }, {
            //                // settings
            //                delay: 1000,
            //                timer: 1000, type: 'danger'
            //            });
            //        }
            //    }
            //});
            //return false;
        });

        $(".btn-xoa-phim").unbind("click").click(function () {
            var id = $(this).data("id");
            var phimStt = $("#form-update-tt-tacpham").find("input[name='PhimStt']").val() || -1;
            if (id == phimStt) {
                $("#TenPhim").val("");
                $("#form-update-tt-tacpham").find("input[name='PhimStt']").val(-1);
            }
            Common.DVC_QuyenTacGia_SearchPhim.ListPhim.splice(id, 1);
            Common.DVC_QuyenTacGia_SearchPhim.SubmitForm();
            return false;
            //Common.ShowLoading(true);
            //Common.Ajax({
            //    url: Common.DVC_QuyenTacGia_SearchPhim.Url.DeletePhim,
            //    type: "POST",
            //    ContentType: 'application/json; charset=utf-8',
            //    async: false,
            //    cache: false,
            //    dataType: 'json',
            //    data: { id: id }
            //}, function (result) {
            //    Common.ShowLoading(false);
            //    if (!Common.Empty(result)) {
            //        if (result.status == true) {
            //            $.notify({
            //                // options
            //                message: 'Xóa thành công.!!!'
            //            }, {
            //                //settings
            //                delay: 1000,
            //                timer: 1000, type: 'success'
            //            });
            //            Common.DVC_QuyenTacGia_SearchPhim.SubmitForm();
            //            return false;
            //        }
            //        else {
            //            $.notify({
            //                // options
            //                message: 'Xóa không thành công!!!'
            //            }, {
            //                // settings
            //                delay: 1000,
            //                timer: 1000, type: 'danger'
            //            });
            //        }
            //    }
            //});
            //return false;
        });

        $('.btn-chon-phim').unbind("click").click(function () {
            //var id = $(this).data("id");
            var name = $(this).data("name");
            //$("#PhimID").val(id);
            $("#TenPhim").val(name);
            Common.DVC_QuyenTacGia_SearchPhim.HideDialog();
            return false;
        });
    },
    SubmitForm: function () {
        var form = $("#idFormSearchPhim");
        var quyenTacGiaID = $("#form-update-tt-tacpham").find("input[name='QuyenTacGiaID']").val() || 0;
        var keyPage = form.find("input[name='KeyPage']").val() || null;
        var click = form.find("input[name='Click']").val() || 0;
        var linhVucID = keyPage == 'update' ? 1 : 0;
        var phimStt = $("#form-update-tt-tacpham").find("input[name='PhimStt']").val() || -1;
        if (keyPage == 'update') {
            if (click >= 1) {
                var listDuLieu = Common.DVC_QuyenTacGia_SearchPhim.ListPhim;
                var html = '';
                if (listDuLieu != null && listDuLieu.length > 0) {
                    for (i = 0; i < listDuLieu.length; i++) {
                        html += '<tr data-id="' + i + '" data-name="' + listDuLieu[i].Ten + '" class="trPhim cursor">';
                        html += '<td class="text-center"><input type="checkbox" class="cursor" name="chkInputCheckedPhim[]" ' + (i == phimStt ? 'checked="checked"' : '') + ' data-name="' + listDuLieu[i].Ten + '" ></td>'
                        html += '<td data-label="STT" class="text-center row-number">' + (i + 1) + '</td>';
                        html += '<td data-label="Tên tệp tin">' + listDuLieu[i].Ten + '</td>';
                        html += '<td data-label="Người đính">' + listDuLieu[i].CreatedUser + '</td>';
                        html += '<td data-label="Tag">' + listDuLieu[i].Tag + '</td>';
                        html += '<td data-label="Ghi chú">' + listDuLieu[i].GhiChu + '</td>';
                        html += '<td data-label="Tập tin"><a href="' + listDuLieu[i].DuongDan + '" target="_blank" download="' + listDuLieu[i].TenTepTin + '">' + listDuLieu[i].TenGoc + '</a></td>';
                        html += '<td data-label="Ngày đính kèm">' + listDuLieu[i].CreatedDate + '</td>';
                        html += '<td data-label="Thao tác" class="text-center">';
                        html += '<button title="Chỉnh sửa" data-id="' + i + '" class="btn btn-primary btn-sm mr-1 btn-update-phim" type="button"><i class="far fa-edit"></i></button>';
                        html += '<button title="Xóa hình ảnh" data-id="' + i + '" class="btn btn-danger btn-sm mr-1 btn-xoa-phim"><i class="far fa-trash-alt"></i></button>';
                        //html += '<button title="Chọn" type="button" data-id="' + i + '" data-name="' + listDuLieu[i].Ten + '" class="btn btn-success btn-sm btn-chon-phim"><i class="fas fa-check"></i></button>';
                        html += '</td>';
                        html += '</tr>';
                    }
                }
                else {
                    html += '<tr class="row-none"><td colspan="9"><div class="alert alert-info mt-3" role="alert">Không có dữ liệu</div></td></tr>';
                }
                $("#result-updatephim tbody").empty();
                $('#tableUpdatePhim').append(html);
                Common.DVC_QuyenTacGia_SearchPhim.ShowDialog();
                return false;
            }
            else {
                var pageIndex = $("#dialogSearchPhim").find("input[name='PageIndex']").val() > 0 ? $("#dialogSearchPhim").find("input[name='PageIndex']").val() : 1;
                var pageSize = $("#dialogSearchPhim").find("input[name='PageSize']").val() > 0 ? $("#dialogSearchPhim").find("input[name='PageSize']").val() : 10;
                var model = {
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    QuyenID: quyenTacGiaID,
                    LinhVucID: linhVucID,
                    Checked: phimStt,
                    Ten: form.find("input[name='Ten']").val() || null,
                    Tag: form.find("input[name='Tag']").val() || null,
                    GhiChu: form.find("input[name='GhiChu']").val() || null,
                    KeyPage: form.find("input[name='KeyPage']").val() || null,
                };
                var formdata = new FormData();
                formdata.append('modelSearch', JSON.stringify(model));
                Common.Ajax({
                    type: 'POST',
                    url: Common.DVC_QuyenTacGia_SearchPhim.Url.SearchPhim,
                    data: formdata,
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    dataType: 'html',
                }, function (result) {
                    $("#result-" + model.KeyPage + "phim").empty();
                    $("#result-" + model.KeyPage + "phim").html(result);
                    $("#tableUpdatePhim tbody tr").each(function () {
                        if ($(this).closest("tr").data("id") >= 0) {
                            Common.DVC_QuyenTacGia_SearchPhim.ListPhim.push({
                                Ten: $(this).closest("tr").data("name"),
                                CreatedUser: $(this).closest("tr").data("user"),
                                Tag: $(this).closest("tr").data("tag"),
                                GhiChu: $(this).closest("tr").data("note"),
                                CreatedDate: $(this).closest("tr").data("date"),
                                TenTepTin: $(this).closest("tr").data("tenfile") || '',
                                TenGoc: $(this).closest("tr").data("tengoc") || '',
                                DuongDan: $(this).closest("tr").data("url") || '',
                            });
                        }
                    });
                    form.find("input[name='Click']").val(1);
                    Common.DVC_QuyenTacGia_SearchPhim.ShowDialog();
                    return false;
                });
            }
        }
        else {
            var pageIndex = $("#dialogSearchPhim").find("input[name='PageIndex']").val() > 0 ? $("#dialogSearchPhim").find("input[name='PageIndex']").val() : 1;
            var pageSize = $("#dialogSearchPhim").find("input[name='PageSize']").val() > 0 ? $("#dialogSearchPhim").find("input[name='PageSize']").val() : 10;
            var model = {
                pageIndex: pageIndex,
                pageSize: pageSize,
                QuyenID: quyenTacGiaID,
                LinhVucID: linhVucID,
                Ten: form.find("input[name='Ten']").val() || null,
                Tag: form.find("input[name='Tag']").val() || null,
                GhiChu: form.find("input[name='GhiChu']").val() || null,
                KeyPage: form.find("input[name='KeyPage']").val() || null,
            };
            var formdata = new FormData();
            formdata.append('modelSearch', JSON.stringify(model));
            Common.Ajax({
                type: 'POST',
                url: Common.DVC_QuyenTacGia_SearchPhim.Url.SearchPhim,
                data: formdata,
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                dataType: 'html',
            }, function (result) {
                $("#result-" + model.KeyPage + "phim").empty();
                $("#result-" + model.KeyPage + "phim").html(result);
                Common.DVC_QuyenTacGia_SearchPhim.ShowDialog();
                return false;
            });
        }
        return false;
    },
    Paging: function (page) {
        Common.DVC_QuyenTacGia_SearchPhim.SetPage(page);
        Common.DVC_QuyenTacGia_SearchPhim.SubmitForm();
        return false;
    },
    SetPage: function (page) {
        $("#dialogSearchPhim").find("input[name='PageIndex']").val(page);
    },
    SetPageSize: function (page) {
        $("#idFormSearchPhim").find("input[name='PageSize']").val(page);
    },
    ChangePageSize: function (e) {
        Common.DVC_QuyenTacGia_SearchPhim.SetPageSize($(e).val());
        Common.DVC_QuyenTacGia_SearchPhim.SubmitForm();
        return false;
    },
    ShowDialog: function () {
        $("#dialogSearchPhim").modal("show")
        Common.DVC_QuyenTacGia_SearchPhim.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogSearchPhim").modal("hide");
    },
    AddTrInTable: function (stt) {
        var createdDate = new Date();
        var dd = String(createdDate.getDate()).padStart(2, '0');
        var mm = String(createdDate.getMonth() + 1).padStart(2, '0');
        var yyyy = createdDate.getFullYear();
        createdDate = dd + '/' + mm + '/' + yyyy;
        var createdUser = $("#idFormSearchPhim").find("input[name='CurrentUser']").val() || null;
        let html = '<td class="text-center"></td>';
        html += '<td class="row-number text-center">' + (stt + 1) + '</td>';
        html += '<td><input type="text" name="' + stt + 'Ten" class="form-control form-control-sm" value=""></td>';
        html += '<td><input type="text" name="' + stt + 'CreatedUser" class="form-control form-control-sm" disabled value="' + createdUser + '"></td>';
        html += '<td><textarea type="text" name="' + stt + 'Tag" class="form-control form-control-sm" rows = 2 value=""></textarea></td>';
        html += '<td><textarea type="text" name="' + stt + 'GhiChu" class="form-control form-control-sm" rows = 2 value=""></textarea></td>';
        html += '<td class="text-center file-field" >'
            + '<button type = "button" class="btn btn-primary btn-block-sm btn-sm mt-md-3 mt-sm-3 uploadicon" onclick = "Common.DVC_QuyenTacGia_SearchPhim.UploadFileClick(this)"><i class="fas fa-upload"></i></button>'
            + '<input hidden type="file" class="form-control-file" name="fileUpload' + stt + '" data-number="' + stt + '" accept=".mp4, .mkv, .vob, .jpg, png" />'
            + '<div class="pt-3" name="urltepdinhkem' + stt + '" data-tenfile="" data-tenfilegoc="" data-duongdan=""></div>'
            + '</td>';
        html += '<td><input type="text" name="' + stt + 'CreatedDate" class="form-control form-control-sm" disabled value="' + createdDate + '"></td>';
        html += '<td class="text-center cursor" data-label="Thao tác">';
        html += '<button title="lưu" class="btn btn-success btn-sm mr-1" type="button" onclick="' + "Common.DVC_QuyenTacGia_SearchPhim.SaveTrData(this," + stt + ",'Save')" + '"><i class="far fa-save"></i></button>';
        html += '<button title="hủy" class="btn btn-danger btn-sm" type="button" onclick="Common.DVC_QuyenTacGia_SearchPhim.DeleteTrData(this)"><i class="fa fa-times"></i></button>';
        html += '</td>';

        return html;
    },
    EditTrInTable: function (item, stt) {
        let html = '<td class="text-center"></td>';
        html += '<td class="row-number text-center">' + (stt + 1) + '</td>';
        html += '<td><input type="text" name="' + stt + 'Ten" class="form-control form-control-sm" value="' + item.Ten + '"></td>';
        html += '<td><input type="text" name="' + stt + 'CreatedUser" class="form-control form-control-sm" disabled value="' + item.CreatedUser + '"></td>';
        html += '<td><textarea type="text" name="' + stt + 'Tag" class="form-control form-control-sm" rows = 2 value="' + item.Tag + '">' + item.Tag + '</textarea></td>';
        html += '<td><textarea type="text" name="' + stt + 'GhiChu" class="form-control form-control-sm" rows = 2 value="' + item.GhiChu + '">' + item.GhiChu + '</textarea></td>';
        html += '<td class="text-center file-field" >'
            + '<button type = "button" class="btn btn-primary btn-block-sm btn-sm mt-md-3 mt-sm-3 uploadicon" onclick = "Common.DVC_QuyenTacGia_SearchPhim.UploadFileClick(this)"><i class="fas fa-upload"></i></button>'
            + '<input hidden type="file" class="form-control-file" name="fileUpload' + stt + '" data-number="' + stt + '" accept=".mp4, .mkv, .vob, .jpg, png" />'
            + '<div class="pt-3" name="urltepdinhkem' + stt + '" data-tenfile="' + item.TenTepTin + '" data-tenfilegoc="' + item.TenGoc + '" data-duongdan="' + item.DuongDan + '">'
            + '<a href="' + item.DuongDan + '" download="' + item.TenGoc + '">' + (item.TenGoc == null ? '' : item.TenGoc) + '</a></div > '
            + '</td>';
        html += '<td><input type="text" name="' + stt + 'CreatedDate" class="form-control form-control-sm" disabled value="' + item.CreatedDate + '"></td>';
        html += '<td class="text-center cursor" data-label="Thao tác">';
        html += '<button title="lưu" class="btn btn-success btn-sm mr-1" type="button" onclick="' + "Common.DVC_QuyenTacGia_SearchPhim.SaveTrData(this," + stt + ",'Save')" + '"><i class="far fa-save"></i></button>';
        html += '<button title="Xóa" class="btn btn-danger btn-sm" type="button" data-id="' + stt + '" onclick="Common.DVC_QuyenTacGia_SearchPhim.DeleteData(' + stt + ')"><i class="far fa-trash-alt"></i></button>';
        html += '</td>';
        return html;
    },
    UploadFileClick: function (that) {
        that.parentNode.querySelector(".form-control-file").click();
        $('.form-control-file').unbind("change").change(function (ev) {
            ev.preventDefault();
            Common.DVC_QuyenTacGia_SearchPhim.UploadFileChange(this);
            return false;
        });
    },
    UploadFileChange: function (ev) {
        Common.ShowLoading(true);
        let numberCurrent = ev.getAttribute("data-number");
        var fileUploads = ev.files;
        if (fileUploads.length > 0) {
            var regex = new RegExp("(mp4)|(mkv)|(vob)|(jpg)|(png)$");
            var fileName = fileUploads[0].name;
            let fileSize = fileUploads[0].size;
            var sizeMB = 100;
            if (!regex.test(Common.DVC_QuyenTacGia_SearchPhim.GetTypeFile(fileName))) {
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
                    data.append('UploadVideo', fileUploads[0], fileUploads[0].name);
                }
                Common.Ajax({
                    type: "POST",
                    url: Common.DVC_QuyenTacGia_SearchPhim.Url.UploadVideo,
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
        if (Common.DVC_QuyenTacGia_SearchPhim.ListPhim[stt]) {
            Common.DVC_QuyenTacGia_SearchPhim.ListPhim[stt].Ten = $.trim(tr.find("input[name='" + stt + "Ten']").val()) || '';
            Common.DVC_QuyenTacGia_SearchPhim.ListPhim[stt].CreatedUser = $.trim(tr.find("input[name='" + stt + "CreatedUser']").val()) || '';
            Common.DVC_QuyenTacGia_SearchPhim.ListPhim[stt].Tag = $.trim(tr.find("textarea[name='" + stt + "Tag']").val()) || '';
            Common.DVC_QuyenTacGia_SearchPhim.ListPhim[stt].GhiChu = $.trim(tr.find("textarea[name='" + stt + "GhiChu']").val()) || '';
            Common.DVC_QuyenTacGia_SearchPhim.ListPhim[stt].CreatedDate = $.trim(tr.find("input[name='" + stt + "CreatedDate']").val()) || '';
            Common.DVC_QuyenTacGia_SearchPhim.ListPhim[stt].TenTepTin = div.data("tenfile") || '';
            Common.DVC_QuyenTacGia_SearchPhim.ListPhim[stt].TenGoc = div.data("tenfilegoc") || '';
            Common.DVC_QuyenTacGia_SearchPhim.ListPhim[stt].DuongDan = div.data("duongdan") || '';
        }
        else {
            Common.DVC_QuyenTacGia_SearchPhim.ListPhim.push({
                Ten: $.trim(tr.find("input[name='" + stt + "Ten']").val()) || '',
                CreatedUser: $.trim(tr.find("input[name='" + stt + "CreatedUser']").val()) || '',
                Tag: $.trim(tr.find("textarea[name='" + stt + "Tag']").val()) || '',
                GhiChu: $.trim(tr.find("textarea[name='" + stt + "GhiChu']").val()) || '',
                CreatedDate: $.trim(tr.find("input[name='" + stt + "CreatedDate']").val()) || '',
                TenTepTin: div.data("tenfile") || '',
                TenGoc: div.data("tenfilegoc") || '',
                DuongDan: div.data("duongdan") || '',
            });
        }
        Common.DVC_QuyenTacGia_SearchPhim.SubmitForm();
        return false;
        //let tr = $(that).closest("tr");
        //let div = $(tr.find("div[name='urltepdinhkem" + stt + "']")[0])
        //var _model = {
        //    PhimID: tr.data("id") || 0,
        //    Ten: $.trim(tr.find("input[name='" + stt + "Ten']").val()) || '',
        //    Tag: $.trim(tr.find("textarea[name='" + stt + "Tag']").val()) || '',
        //    GhiChu: $.trim(tr.find("textarea[name='" + stt + "GhiChu']").val()) || '',
        //    TenTepTin: div.data("tenfile") || '',
        //    TenGoc: div.data("tenfilegoc") || '',
        //    DuongDan: div.data("duongdan") || '',
        //};
        //var formdata = new FormData();
        //formdata.append('modelThongTin', JSON.stringify(_model));
        //Common.ShowLoading(true);
        //$.ajax({
        //    type: 'POST',
        //    url: Common.DVC_QuyenTacGia_SearchPhim.Url.SavePhim,
        //    data: formdata,
        //    datatype: "json",
        //    contentType: false, // Not to set any content header
        //    processData: false, // Not to process data
        //    success: function (result) {
        //        Common.ShowLoading(false);
        //        if (!Common.Empty(result)) {
        //            if (result.status == true) {
        //                $.notify({
        //                    // options
        //                    message: 'Lưu thành công.!!!'
        //                }, {
        //                    //settings
        //                    delay: 1000,
        //                    timer: 1000, type: 'success'
        //                });
        //                Common.DVC_QuyenTacGia_SearchPhim.SubmitForm();
        //                return false;
        //            }
        //            else {
        //                $.notify({
        //                    // options
        //                    message: 'Lưu không thành công!!!'
        //                }, {
        //                    // settings
        //                    delay: 1000,
        //                    timer: 1000, type: 'danger'
        //                });
        //            }
        //        }
        //    },
        //    error: function (data) {
        //        Common.ShowLoading(false);
        //        $.notify({
        //            // options
        //            message: 'Lưu không thành công.!!!'
        //        }, {
        //            // settings
        //            delay: 1000,
        //            timer: 1000, type: 'danger'
        //        });
        //    }
        //});
    },
    DeleteTrData: function (that) {
        let tr = $(that).parents("tr");
        let html = "";
        let tbody = tr.parents("tbody");
        if (tbody.find("tr").length == 1) {
            html += '<tr class="row-none"><td colspan = "9"><div class="alert alert-info mt-3" role="alert">Không có dữ liệu</div></td ></tr>';
        }
        tr.remove();
        $("#tableUpdatePhim tbody").append(html);
    },
    DeleteData: function (id) {
        var phimStt = $("#form-update-tt-tacpham").find("input[name='PhimStt']").val() || -1;
        if (id == phimStt) {
            $("#TenPhim").val("");
            $("#form-update-tt-tacpham").find("input[name='PhimStt']").val(-1);
        }
        Common.DVC_QuyenTacGia_SearchPhim.ListPhim.splice(id, 1);
        Common.DVC_QuyenTacGia_SearchPhim.SubmitForm();
        return false;
    },
    BindDataUpdate: function (tr, model) {
        //số thứ tự hiện tại
        let numberCurrent = tr.find(".row-number").text() - 1;
        //remove td
        tr.find("td").remove();
        //add td
        let html = new Array();
        let row = Common.DVC_QuyenTacGia_SearchPhim.EditTrInTable(model, numberCurrent);
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

};

var DVC_QuyenTacGia_SearchPhim = DVC_QuyenTacGia_SearchPhim;
DVC_QuyenTacGia_SearchPhim.constructor = DVC_QuyenTacGia_SearchPhim;
