var NguoiDungHeThong = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
var arrayquyen = [];
NguoiDungHeThong.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], input[type=email], textarea, select").val("");
            Common.NguoiDungHeThong.SubmitForm();
        });
        $("input[name='Khoa']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });
        $("#btnFormSearch").unbind("click").click(function () {
            Common.NguoiDungHeThong.SetPage(1);
            Common.NguoiDungHeThong.SubmitForm();
            return false;
        });
        $("#btnLamMoi").unbind("click").click(function () {
            Common.NguoiDungHeThong.ResetFormSearch();
        });
        $("input[name='Khoa']").unbind("keypress").bind("keypress", function (e) {
            if (e.which === 13) {
                Common.NguoiDungHeThong.SetPage(1);
                Common.NguoiDungHeThong.SubmitForm();
                return false;
            }
        });

        $('input[name=BrowseImage]').change(function (ev) {
            Common.ShowLoading(true);
            var _this = $(this);
            var fileUploads = _this[0].files;
            var FileSize = fileUploads.size / 1024 / 1024;
            if (!!fileUploads) {
                var regex = new RegExp("(png|jpg|jpeg|PNG|JPG)$");
                var fileName = fileUploads[0].name;
                if (!regex.test(Common.Slider.GetTypeFile(fileName))) {
                    $.notify({
                        // options
                        message: 'Vui lòng chọn tệp tin đúng định dạng hình.!!!'
                    }, {
                            // settings
                        delay: 1000,
                        timer: 1000, type: 'danger'
                        });
                    Common.ShowLoading(false);
                    _this.val(null);
                    return false;
                }
                else if (FileSize > 2) {
                    $.notify({
                        // options
                        message: 'Vui lòng chọn tệp tin dưới 2MB.!!!'
                    }, {
                            // settings
                        delay: 1000,
                        timer: 1000, type: 'danger'
                        });
                    Common.ShowLoading(false);
                    _this.val(null);
                    return false;
                }
                else {
                    var data = new FormData();
                    if (fileUploads.length > 0) {
                        data.append('UploadedFiles', fileUploads[0], fileUploads[0].name);
                    }
                    Common.Ajax({
                        type: "POST",
                        url: Common.NguoiDungHeThong.Url.UploadFile,
                        dataType: 'json',
                        data: data,
                        contentType: false, // Not to set any content header
                        processData: false, // Not to process data
                    }, function (data) {
                        Common.ShowLoading(false);
                        if (data) {
                            if (data.status) {
                                //_this.closest(".box_img").find('.img').attr('src', data.url);
                                //_this.closest(".box_img").attr('data-idimg', data.id);
                                $("#form-update-NguoiDung").find("div[data='image-box']").show();
                                $("#form-update-NguoiDung").find("div[data='image-box'] img").attr('src', data.url);
                                $("#form-update-NguoiDung").find("input[name='Avatar']").val(data.url);
                            }
                            else {
                                $.notify({
                                    // options
                                    message: 'Tải hình lỗi.!!!'
                                }, {
                                        // settings
                                    delay: 1000,
                                    timer: 1000, type: 'danger'
                                    });
                            }
                        }
                        else {

                        }
                    });
                }
            }
        });
        //$("input[name='BrowseImage']").unbind("change").change(function () {
        //    if (this.files && this.files[0]) {
        //        var filename = $(this).val();
        //        if (window.FileReader) {
        //            var reader = new window.FileReader();
        //            reader.onload = function (e) {
        //                $("#form-update-NguoiDung").find("div[data='image-box']").show();
        //                $("#form-update-NguoiDung").find("div[data='image-box'] img").attr('src', e.target.result);
        //                $("#form-update-NguoiDung").find("input[name='Avatar']").val(e.target.result);
        //            };
        //            reader.readAsDataURL(this.files[0]);
        //        } else {
        //            //the browser doesn't support the FileReader Object, so do this
        //            return;
        //        }
        //    }
        //});
        $("#btnAddForm").unbind("click").click(function () {
            window.location.href = $(this).data('url');
        });

        $(".update").unbind("click").click(function () {
            var id = $(this).data("id");
            window.location.href = $(this).data('url') + "?id=" + id;
        });

        $(".btnDelRow").unbind("click").click(function (event) {
            event.preventDefault();
            var id = $(this).data("id");
            if (Common.Empty(id)) {
                $.notify({
                    // options
                    message: 'Dữ liệu cần xóa không tồn tại.!!!'
                }, {
                        // settings
                    delay: 1000,
                    timer: 1000, type: 'danger'
                    });
                return false;
            }
            Common.ShowAlert("Thông báo", "Xóa dữ liệu đang chọn?", {
                Close: { Display: true },
                Items: {
                    Cancel: {
                        Name: "Cancel",
                        Data: $(this),
                        Class: "btn-info ",
                        OnClick: function (target) {
                            Common.HideAlert();
                            Common.Ajax({
                                type: "POST",
                                url: Common.NguoiDungHeThong.Url.Delete,
                                async: false,
                                cache: false,
                                data: {
                                    id: id
                                }
                            }, function (data) {
                                if (data) {
                                    Common.HideAlert();
                                    Common.NguoiDungHeThong.SubmitForm();
                                }
                                else {
                                    $.notify({
                                        // options
                                        message: 'Xóa không thành công.!!!'
                                    }, {
                                            // settings
                                        delay: 1000,
                                        timer: 1000, type: 'danger'
                                        });

                                    return false;
                                }
                            });
                        },
                        Value: "Xóa"
                    }
                }
            }, "Cancel");
            return false;
        });

        $(".changepass").unbind("click").click(function () {
            Common.ShowLoading(true);
            var id = $(this).data("id");
            Common.Ajax({
                type: "GET",
                url: Common.NguoiDungHeThong.Url.DoiMatKhau,
                async: false,
                cache: false,
                dataType: 'html',
                data: {
                    id: id
                }
            }, function (data) {
                if (data) {
                    Common.ShowLoading(false);
                    $(".show-content-dialogAdd").html(data);
                    $("#DialogThayDoiMatKhau").modal("show");
                    $('#DialogThayDoiMatKhau').on('shown.bs.modal', function (event) {

                    });
                    Common.NguoiDungHeThong.RegisterEvent();
                }
                else {
                    Common.NguoiDungHeThong.Error();
                    return false;
                }
            });
        });

        $("#form-update-doimatkhau").unbind('submit').submit(function (e) {
            e.preventDefault();  // prevent standard form submission
            Common.ShowLoading(false);
            var MatKhau = $.trim($("#DialogThayDoiMatKhau").find("input[name='MatKhau']").val());
            var NhapLaimatKhau = $.trim($("#DialogThayDoiMatKhau").find("input[name='NhapLaimatKhau']").val());
            var form = $(this);
            //MatKhau
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhau').hide();
            if (Common.Empty(MatKhau)) {
                $("#DialogThayDoiMatKhau").find(".thongbaomatkhau").html("nhập thông tin mật khẩu");
                $("#DialogThayDoiMatKhau").find(".thongbaomatkhau").show();
                $("#DialogThayDoiMatKhau").find("input[name='MatKhau']").focus();
                return false;

            }
            else {
                if (MatKhau.length > 0) {
                    if (MatKhau.length < 4) {
                        $("#DialogThayDoiMatKhau").find(".thongbaomatkhau").html("Mật khẩu phải lớn hơn 4 ký tự!!!");
                        $("#DialogThayDoiMatKhau").find(".thongbaomatkhau").show();
                        $("#DialogThayDoiMatKhau").find("input[name=MatKhau']").focus();
                        return false;
                    }
                }
            }
            //NhapLaimatKhau
            $("#DialogThayDoiMatKhau").find('.thongbaonhaplai').hide();
            if (Common.Empty(NhapLaimatKhau)) {
                $("#DialogThayDoiMatKhau").find(".thongbaonhaplai").html("nhập thông tin mật khẩu");
                $("#DialogThayDoiMatKhau").find(".thongbaonhaplai").show();
                $("#DialogThayDoiMatKhau").find("input[name='NhapLaimatKhau']").focus();
                return false;
            }
            else {
                if (NhapLaimatKhau != MatKhau) {
                    $("#DialogThayDoiMatKhau").find(".thongbaonhaplai").html("Nhập lại mật khẩu không đúng!!!");
                    $("#DialogThayDoiMatKhau").find(".thongbaonhaplai").show();
                    $("#DialogThayDoiMatKhau").find("input[name=NhapLaiMatKhau']").focus();
                    return false;
                }
            }
            Common.Ajax({
                type: form.attr("method"),
                url: form.attr("action"),
                async: false,
                cache: false,
                data: form.serialize()
            }, function (data) {
                if (!Common.Empty(data)) {
                    if (data.status == true) {
                        Common.ShowLoading(true);
                        $.notify({
                            // options
                            message: 'Đổi mật khẩu thành công.!!!'
                        }, {
                                // settings
                            delay: 1000,
                            timer: 1000, type: 'success'
                            });
                        Common.NguoiDungHeThong.HideDialogChangePass();
                    }
                    else {
                        Common.ShowLoading(false);
                        $.notify({
                            // options
                            message: 'Đổi mật khẩu không thành công.!!!'
                        }, {
                                // settings
                            delay: 1000,
                            timer: 1000, type: 'danger'
                            });
                        return false;
                    }
                }
            });
        });

        $(".btnxoaquyen").unbind("click").click(function () {

            var id = $(this).closest("tr").data('id');
            var name = $(this).closest("tr").data('name');
            var x = document.getElementById("NhomQuyenadd");
            if (x != null && x.options != null && x.options.length == 0) {
                var option = document.createElement("option");
                option.text = "Tất cả";
                option.value = "";
                x.add(option);
            }
            var option1 = document.createElement("option");
            option1.text = name;
            option1.value = id;
            x.add(option1);
            $(this).closest("tr").remove();

        });

        $(".phanquyen").unbind("click").click(function () {
            Common.ShowLoading(true);
            var id = $(this).data("id");
            Common.Ajax({
                type: "GET",
                url: Common.NguoiDungHeThong.Url.PhanQuyen,
                async: false,
                cache: false,
                dataType: 'html',
                data: {
                    id: id
                }
            }, function (data) {
                if (data) {
                    Common.ShowLoading(false);
                    $(".show-content-phanquyen").html(data);
                    $("#DialogPhanQuyen").modal("show");
                    $('#DialogPhanQuyen').on('shown.bs.modal', function (event) {

                    });

                    Common.NguoiDungHeThong.RegisterEvent();
                    Common.NguoiDungHeThong.LoadComboPhanQuyen();
                }
                else {
                    Common.NguoiDungHeThong.Error();
                    return false;
                }
            });
        });

        $("#addquyen").unbind("click").click(function () {
            var Id = $.trim($("#DialogPhanQuyen select[name='NhomQuyenadd']").val());

            if (Id != "") {
                var Name = $.trim($("#DialogPhanQuyen select[name='NhomQuyenadd'] option:selected").text());
                var dem = 0;
                $('#table-change-permission-user tbody tr').each(function () {
                    dem++;
                })
                Common.NguoiDungHeThong.GenTableQuyen(Id, Name, dem + 1);
                var x = document.getElementById("NhomQuyenadd");
                x.remove(x.selectedIndex);
            }
            else // chonj all
            {
                var strhtml = '';
                var select = document.getElementById('NhomQuyenadd')
                var options = select.options;
                var dem = 0;
                $('#table-change-permission-user tbody tr').each(function () {
                    dem++;
                })
                for (var i = 0; i < options.length; i++) {

                    if (options[i].value != "") {
                        dem = dem + 1;
                        strhtml += '<tr data-id="' + options[i].value + '" data-name="' + options[i].text + '">'
                        strhtml += '<td class="text-center">' + dem + '</td>'
                        strhtml += '<td>' + options[i].text + '</td>'
                        strhtml += '<td class="text-center cursor">'
                        strhtml += '<button type="button" title="Xóa" class="btn btn-danger btn-sm btnxoaquyen" href=""><i class="far fa-trash-alt"></i></button>'
                        strhtml += '</td>'
                        strhtml += '</tr>'

                    }
                }
                //var length = options.length;
                //for (var i = 0; i < length; i++) {

                //    select.options[i] = null;

                //}
                $("#NhomQuyenadd").empty();
                $("#DialogPhanQuyen").find("#table-change-permission-user").find('tbody').append(strhtml);
                Common.NguoiDungHeThong.RegisterEvent();
            }


        });

        $(".luuphanquyen").unbind("click").click(function () {
            var nguoidungid = $("#NguoiDungPQId").val();
            var dsquyen = "";
            $('#table-change-permission-user tbody tr').each(function () {
                dsquyen = dsquyen + $(this).data('id') + ",";
            })
            dsquyen = dsquyen.substring(0, dsquyen.length - 1);
            if (dsquyen == "") {
                $.notify({
                    // options
                    message: 'Chưa chọn nhóm quyền.!!!'
                }, {
                        // settings
                    delay: 1000,
                    timer: 1000, type: 'danger'
                    });
                return false;
            }
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.NguoiDungHeThong.Url.PhanQuyen,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: {
                    NhomQuyenIDs: dsquyen,
                    NguoiDungID: nguoidungid
                }
            }, function (result) {
                Common.ShowLoading(true);
                if (!Common.Empty(result)) {
                    if (result.status == true) {

                        $.notify({
                            // options
                            message: 'Phân quyền thành công.!!!'
                        }, {
                                // settings
                            delay: 1000,
                            timer: 1000, type: 'success'
                            });
                        $("#DialogPhanQuyen").modal("hide");
                        Common.NguoiDungHeThong.SetPage(1);
                        Common.NguoiDungHeThong.SubmitForm();
                    }
                    else {
                        $.notify({
                            // options
                            message: 'Phân quyền không thành công!!!'
                        }, {
                                // settings
                            delay: 1000,
                            timer: 1000, type: 'danger'
                            });
                    }
                }
            }
            );
            return false;
        });

        $(".btnkhoataikhoan").unbind("click").click(function () {
            Common.ShowLoading(true);
            var id = $(this).data("id");
            var khoa = $(this).data("khoa");
            var isKhoa = false;
            if (khoa == true) { isKhoa = true; }
            Common.Ajax({
                type: "GET",
                url: Common.NguoiDungHeThong.Url.KhoaTaiKhoan,
                async: false,
                cache: false,
                dataType: 'html',
                data: {
                    UserID: id,
                    Khoa: isKhoa
                }
            }, function (data) {
                var result = JSON.parse(data);
                if (result && result.status == true) {
                    var strmessage = "";
                    if (isKhoa == true) strmessage = "Khóa tài khoản thành công!!!";
                    else strmessage = "Mở khóa tài khoản thành công!!!";

                    $.notify({
                        // options
                        message: strmessage
                    }, {
                            // settings
                            delay: 1000,
                            timer: 1000, type: 'success'
                        });
                    Common.NguoiDungHeThong.SetPage(1);
                    Common.NguoiDungHeThong.SubmitForm();
                    return false;
                }
                else {
                    $.notify({
                        // options
                        message: 'Có lỗi xảy ra!!!'
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
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.NguoiDungHeThong.SetPage(page);
        Common.NguoiDungHeThong.SubmitForm();
        return false;
    },
    SetPageSize: function (page) {
        $("#IdformSearch").find("input[name='PageSize']").val(page);
    },

    ChangePageSize: function (e) {
        Common.NguoiDungHeThong.SetPageSize($(e).val());
        Common.NguoiDungHeThong.SubmitForm();
        return false;
    },
    GetCurrentPage: function (target) {
        try {
            page = 1;
            if (parseInt($(target).text()) > 0) {
                page = parseInt($(target).text());
            } else {
                page = parseInt($(".paging-top .pagination ul li.active").filter(function () { return !($(this).hasClass("previous") || $(this).hasClass("next")); }).text());
                if ($(target).closest("li").hasClass("previous")) {
                    page -= 1;
                } else {
                    page += 1;
                }
            }
            if (page > 0) {
                page = 1;
            } else {
                page = 1;
            }
        } catch (ex) {
            page = 1;
        }
        return page;
    },
    BeforeSend: function (xhr) {
    },
    SubmitForm: function () {
        Common.ShowLoading(true);
        $("#IdformSearch").submit();
    },
    Success: function (data) {
        Common.ShowLoading(false);
        $("body,html").animate({
            scrollTop: 180
        }, 1000);
        Common.NguoiDungHeThong.RegisterEvent();
    },
    CheckNull: function (str, labelthongbao, Nameforcus) {
        $("." + labelthongbao).html(Common.NguoiDungHeThong.Message.Nhap + " " + str);
        $("." + labelthongbao).show();
        $("input[name='" + Nameforcus + "']").focus();
        return false;
    },
    checklength(str, labelthongbao, nameforcus, maxlength, nameshow) {
        if (str.length > 0) {
            str = Common.RemoveScript(str);
            if (str === '') {
                $("." + labelthongbao).html(nameshow + " " + Common.NguoiDungHeThong.Message.MaJavaCript);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
            if (str.length > maxlength) {
                $("." + labelthongbao).html(Common.NguoiDungHeThong.Message.Length + " " + nameshow + " không quá " + maxlength);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
        }
    },
    checkhtml(str, labelthongbao, nameforcus, nameshow) {

        $("." + labelthongbao).html(nameshow + " " + Common.NguoiDungHeThong.Message.MaHtml);
        $("." + labelthongbao).show();
        $("input[name='" + nameforcus + "']").focus();
        return false;
    },
    BrowseImage: function () {
        $("#form-update-NguoiDung").find("input[name='BrowseImage']").click();
    },
    getOptionByValue(select, value) {
        var options = select.options;
        for (var i = 0; i < options.length; i++) {
            if (options[i].value == value) {
                select.options[i] = null;
            }
        }
        return null
    },
    LoadComboPhanQuyen: function () {
        setTimeout(function () {
            var dsquyen = [];
            $('#table-change-permission-user tbody tr').each(function () {
                dsquyen.push($(this).data('id'));
            })
            var select = document.getElementById('NhomQuyenadd')
            for (j = 0; j < dsquyen.length; j++) {

                Common.NguoiDungHeThong.getOptionByValue(select, String(dsquyen[j]));


            }
        }, 1000);
    },
    GenTableQuyen: function (id, name, dem) {

        var strhtml = '';

        strhtml += '<tr data-id="' + id + '" data-name="' + name + '">'
        strhtml += '<td class="text-center">' + dem + '</td>'
        strhtml += '<td>' + name + '</td>'
        strhtml += '<td class="text-center cursor">'
        strhtml += '<button type="button" title="Xóa" class="btn btn-danger btn-sm btnxoaquyen" href=""><i class="far fa-trash-alt"></i></button>'
        strhtml += '</td>'
        strhtml += '</tr>'


        $("#DialogPhanQuyen").find("#table-change-permission-user").find('tbody').append(strhtml);
        Common.NguoiDungHeThong.RegisterEvent();

    },
     OnInputCheckPassword_ChangePass: function (variable) {
        var strPassword = $(variable).val();
        var repassWord = $("#DialogThayDoiMatKhau").find('input[name=NhapLaimatKhau]').val();
        $("#DialogThayDoiMatKhau").find('.thongbaomatkhau').hide();
        if (strPassword.length < 6) {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhaukytu').show();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhaukytu').addClass("show");
        }
        else {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhaukytu').hide();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhaukytu').removeClass("show");
        }
        if (!/[0-9]/.test(strPassword)) {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauso').show();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauso').addClass("show");
        }
        else {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauso').hide();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauso').removeClass("show");
        }
        if (!/[a-z]/.test(strPassword)) {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauthuong').show();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauthuong').addClass("show");
        }
        else {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauthuong').hide();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauthuong').removeClass("show");
        }
        if (!/[A-Z]/.test(strPassword)) {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauhoa').show();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauhoa').addClass("show");
        }
        else {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauhoa').hide();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauhoa').removeClass("show");
        }
        if (repassWord && strPassword != repassWord) {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauxacnhan').show();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauxacnhan').addClass("show");
        }
        else {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauxacnhan').hide();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauxacnhan').removeClass("show");
        }
        if (strPassword && repassWord && $('.field-validation-error.show').length == 0)
            $("#DialogThayDoiMatKhau").find('.btnchangepass').attr('disabled', false);

        else
            $("#DialogThayDoiMatKhau").find('.btnchangepass').attr('disabled', true);
    },
    OnInputCheckRePassword_ChangePass: function (variable, element) {
        var passWord = $("#DialogThayDoiMatKhau").find('input[name=MatKhau]').val();
        var repassWord = $(variable).val();
        $("#DialogThayDoiMatKhau").find('.thongbaonhaplai').hide();

        if (passWord != repassWord) {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauxacnhan').show();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauxacnhan').addClass("show");
        }
        else {
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauxacnhan').hide();
            $("#DialogThayDoiMatKhau").find('.thongbaomatkhauxacnhan').removeClass("show");
        }
        if (passWord && repassWord && $('.field-validation-error.show').length == 0)
            $("#DialogThayDoiMatKhau").find('.btnchangepass').attr('disabled', false);
        else
            $("#DialogThayDoiMatKhau").find('.btnchangepass').attr('disabled', true);
    },
    HideDialogChangePass: function () {
        $("#DialogThayDoiMatKhau").modal("hide");
    },
    HideDialogPhanQuyen: function () {
        $("#DialogPhanQuyen").modal("hide");
    },
    ResetFormSearch: function () {
        $("#IdformSearch").find("input[type=text], input[type=email], textarea, select").val("");
        Common.NguoiDungHeThong.SubmitForm();
    },

};
// create NguoiDungHeThong Object
var NguoiDungHeThong = NguoiDungHeThong;
NguoiDungHeThong.constructor = NguoiDungHeThong;

