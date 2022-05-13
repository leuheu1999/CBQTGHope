var ThemNguoiDungHeThong = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
var arrayquyen = [];
ThemNguoiDungHeThong.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("input[name='Khoa']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });
        //$(".datepicker-icon").unbind("click").click(function () {
        //    $(".datepicker-share-ttcn").focus();
        //});

        $('input[name=BrowseImage]').change(function (ev) {
            Common.ShowLoading(true);
            var _this = $(this);
            var fileUploads = _this[0].files;
            var FileSize = fileUploads[0].size / 1024 / 1024;
            if (!!fileUploads) {
                var regex = new RegExp("(png|jpg|jpeg|PNG|JPG)$");
                var fileName = fileUploads[0].name;
                if (!regex.test(Common.ThemNguoiDungHeThong.GetTypeFile(fileName))) {
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
                        url: Common.ThemNguoiDungHeThong.Url.UploadFile,
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
                                $("#form-update-NguoiDung").find("div[data='image-box'] img").attr('src', data.url.replace("admin\\", ""));
                                $("#form-update-NguoiDung").find("input[name='Avatar']").val(data.url);
                                $("#form-update-NguoiDung").find("input[name='AvatarShow']").val(fileUploads[0].name);
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
                        //else {

                        //}
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
        $("#btnLuu").unbind("click").click(function () {
            var TenTaiKhoan = $.trim($("input[name='TenTaiKhoan']").val());
            var MatKhau = $.trim($("input[name='MatKhau']").val());
            var NhapLaimatKhau = $.trim($("input[name='NhapLaimatKhau']").val());
            var HoVaTen = $.trim($("input[name='HoVaTen']").val());
            var TenHienThi = $.trim($("input[name='TenHienThi']").val());
            var Email = $.trim($("input[name='Email']").val());
            var NgaySinh = $.trim($("input[name='NgaySinh']").val());
            var CMND = $.trim($("input[name='CMND']").val());
            var NgayCapCMND = $.trim($("input[name='NgayCapCMND']").val());
            var NoiCapCMND = $.trim($("input[name='NoiCapCMND']").val());
            var DienThoai = $.trim($("input[name='DienThoai']").val());
            var DiaChi = $.trim($("textarea[name='DiaChi']").val());
            var GioiTinh = $.trim($("select[name='GioiTinh']").val());
            var taiKhoanGuiID = $.trim($("input[name='TaiKhoanGuid']").val());
            if (Common.Empty(GioiTinh)) {
                $(".thongbaoGioiTinh").html(that.Message.GioiTinh);
                $(".thongbaoGioiTinh").show();
                $(".thongbaoGioiTinh").addClass("show");
                $("select[name='GioiTinh']").focus();
                return false;
            }
            //TenTaiKhoan
            $('.thongbaotentaikhoan').hide();
            if (Common.Empty(TenTaiKhoan)) {
                check = Common.ThemNguoiDungHeThong.CheckNull("Tên tài khoản", "thongbaotentaikhoan", "TenTaiKhoan");
                if (check == false) return;
            }
            else {
                check = Common.ThemNguoiDungHeThong.checklength(TenTaiKhoan, "thongbaotentaikhoan", "TenTaiKhoan", 100, "tên tài khoản")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(TenTaiKhoan) || (/<[/s/S][a-z][\s\S]*>/i.test(TenTaiKhoan))) {
                check = Common.ThemNguoiDungHeThong.checkhtml(TenTaiKhoan, "thongbaotentaikhoan", "TenTaiKhoan", "tên tài khoản")
                if (check == false) return;
            }
            if (Common.ThemNguoiDungHeThong.checktenTK(TenTaiKhoan, "TenTaiKhoan") == false) {
                return;
            }
            //MatKhau
            if (taiKhoanGuiID == null || taiKhoanGuiID == "" || taiKhoanGuiID == Common.GuidEmpty) {
                $('.thongbaomatkhau').hide();
                if (Common.Empty(MatKhau)) {
                    check = Common.ThemNguoiDungHeThong.CheckNull("Mật khẩu", "thongbaomatkhau", "MatKhau");
                    if (check == false) return;
                }
                else {
                    if (MatKhau.length > 0) {
                        if (MatKhau.length < 4) {
                            $(".thongbaomatkhau").html("Mật khẩu phải lớn hơn 4 ký tự!!!");
                            $(".thongbaomatkhau").show();
                            $("input[name=MatKhau']").focus();
                            return false;
                        }
                    }
                }
                //NhapLaimatKhau
                $('.thongbaonhaplai').hide();
                if (Common.Empty(NhapLaimatKhau)) {
                    check = Common.ThemNguoiDungHeThong.CheckNull("Nhập lại mật khẩu", "thongbaonhaplai", "NhapLaiMatKhau");
                    if (check == false) return;
                }
                else {
                    if (NhapLaimatKhau != MatKhau) {
                        $(".thongbaonhaplai").html("Nhập lại mật khẩu không đúng!!!");
                        $(".thongbaonhaplai").show();
                        $("input[name=NhapLaiMatKhau']").focus();
                        return false;
                    }
                }
            }
            //HoVaTen
            $('.thongbaohovaten').hide();
            if (Common.Empty(HoVaTen)) {
                check = Common.ThemNguoiDungHeThong.CheckNull("Họ và tên lót", "thongbaohovaten", "HoVaTen");
                if (check == false) return;
            }
            else {
                check = Common.ThemNguoiDungHeThong.checklength(HoVaTen, "thongbaohovaten", "HoVaTen", 100, "họ và tên lót")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(HoVaTen) || (/<[/s/S][a-z][\s\S]*>/i.test(HoVaTen))) {
                check = Common.ThemNguoiDungHeThong.checkhtml(HoVaTen, "thongbaohovaten", "HoVaTen", "họ và tên lót")
                if (check == false) return;
            }
            //$('.ThongBaoTenHoVaTen').hide();
            //if (Common.ThemNguoiDungHeThong.checkten(HoVaTen, "HoVaTen") == false) {
            //    return;
            //}
            //TenHienThi
            $('.thongbaotenhienthi').hide();
            if (Common.Empty(TenHienThi)) {
                check = Common.ThemNguoiDungHeThong.CheckNull("Tên hiển thị", "thongbaotenhienthi", "TenHienThi");
                if (check == false) return;
            }
            else {
                check = Common.ThemNguoiDungHeThong.checklength(TenHienThi, "thongbaotenhienthi", "TenHienThi", 300, "tên hiển thị")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(TenHienThi) || (/<[/s/S][a-z][\s\S]*>/i.test(TenHienThi))) {
                check = Common.ThemNguoiDungHeThong.checkhtml(TenHienThi, "thongbaotenhienthi", "TenHienThi", "tên hiển thị")
                if (check == false) return;
            }
            //$('.ThongBaoTenTenHienThi').hide();
            //if (Common.ThemNguoiDungHeThong.checkten(TenHienThi, "TenHienThi") == false) {
            //    return;
            //}
            //Email
            $('.thongbaoemail').hide();
            if (Common.Empty(Email)) {
                check = Common.ThemNguoiDungHeThong.CheckNull("Email", "thongbaoemail", "Email");
                if (check == false) return;
            }
            else {
                check = Common.ThemNguoiDungHeThong.checklength(Email, "thongbaoemail", "Email", 200, "email")
                if (check == false) return;
            }

            if (Common.ThemNguoiDungHeThong.checkemail(Email, "Email") == false) {
                return;
            }
            //NgaySinh
            //$('.thongbaongaysinh').hide();
            //if (Common.Empty(NgaySinh)) {
            //    check = Common.ThemNguoiDungHeThong.CheckNull("ngày sinh", "thongbaongaysinh", "NgaySinh");
            //    if (check == false) return;
            //}
            //CMND
            $('.thongbaocmnd').hide();
            //if (Common.Empty(CMND)) {
            //    check = Common.ThemNguoiDungHeThong.CheckNull("CMND", "thongbaocmnd", "CMND");
            //    if (check == false) return;
            //}
            //else {
            //    check = Common.ThemNguoiDungHeThong.checklength(CMND, "thongbaocmnd", "CMND", 20, "CMND");
            //    if (check == false) return;
            //}
            if (Common.ThemNguoiDungHeThong.checkCMND(CMND, "CMND") == false) {
                return;
            }
            if (Common.ThemNguoiDungHeThong.checktextDB(CMND, "CMND", "thongbaocmnd", "CMND") == false) {
                return;
            }
            //noi cap CMND
            $('.thongbaonoicapcmnd').hide();
            if (!Common.Empty(NoiCapCMND)) {
                check = Common.ThemNguoiDungHeThong.checklength(NoiCapCMND, "thongbaonoicapcmnd", "NoiCapCMND", 1000, "NoiCapCMND");
                if (check == false) return;
            }
            if (Common.ThemNguoiDungHeThong.checktextDB(NoiCapCMND, "NoiCapCMND", "thongbaonoicapcmnd", "NoiCapCMND") == false) {
                return;
            }
            //DienThoai
            $('.thongbaodienthoai').hide();
            if (Common.Empty(DienThoai)) {
                check = Common.ThemNguoiDungHeThong.CheckNull("điện thoại", "thongbaodienthoai", "DienThoai");
                if (check == false) return;
            }
            else {
                check = Common.ThemNguoiDungHeThong.checklength(DienThoai, "thongbaodienthoai", "DienThoai", 11, "điện thoại")
                if (check == false) return;
            }
            if (Common.ThemNguoiDungHeThong.checkSDT(DienThoai, "DienThoai") == false) {
                return;
            }
            if (Common.ThemNguoiDungHeThong.checktextDB(DienThoai, "DienThoai", "thongbaodienthoai", "Số điện thoại") == false) {
                return;
            }
            //DiaChi
            $('.thongbaodiachi').hide();
            check = Common.ThemNguoiDungHeThong.checklength(DiaChi, "thongbaodiachi", "DiaChi", 1000, "địa chỉ")
            if (check == false) return;
            if (/<[a-z][\s\S]*>/i.test(DiaChi) || (/<[/s/S][a-z][\s\S]*>/i.test(DiaChi))) {
                check = Common.ThemNguoiDungHeThong.checkhtml(DiaChi, "thongbaodiachi", "DiaChi", "địa chỉ")
                if (check == false) return;
            }
            //if (Common.ThemNguoiDungHeThong.checktextDB(DiaChi, "DiaChi", "thongbaodiachi", "Địa chỉ") == false) {
            //    return;
            //}
            var data = $("#form-update-NguoiDung").serialize();
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.ThemNguoiDungHeThong.Url.ThemMoi,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: data,
            }, function (result) {
                Common.ShowLoading(true);
                if (!Common.Empty(result)) {
                    if (result.status == true) {
                        if ($.trim($(".clssupdate").val()) !="00000000-0000-0000-0000-000000000000") {
                            $.notify({
                                // options
                                message: 'Cập nhật thành công.!!!'
                            }, {
                                    // settings
                                delay: 1000,
                                timer: 1000, type: 'success'
                                });
                        }
                        else {
                            $.notify({
                                // options
                                message: 'Lưu thành công.!!!'
                            }, {
                                    // settings
                                delay: 1000,
                                timer: 1000, type: 'success'
                                });
                        }
                        $(".clssupdate").val(result.taikhoanguid);
                        $(".changepass").data("id", result.taikhoanguid);
                        $(".phanquyen").data("id", result.taikhoanguid);
                        $(".changepass").attr('disabled', false);
                        $(".phanquyen").attr('disabled', false);
                    }
                    else if (result.status == false && result.checkMa == true) {
                        $.notify({
                            // options
                            message: 'Tên tài khoản đã tồn tại!!!'
                        }, {
                                // settings
                            delay: 1000,
                            timer: 1000, type: 'danger'
                            });
                    }
                    else if (result.status == false && result.checkSDT == true) {
                        $.notify({
                            // options
                            message: 'Số điện thoại đã được đăng ký!!!'
                        }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                    }
                    else if (result.status == false && result.checkEmail == true) {
                        $.notify({
                            // options
                            message: 'Email đã được đăng ký!!!'
                        }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                    }
                    else if (result.status == false && result.checkMK == true) {
                        $.notify({
                            // options
                            message: 'Nhập lại mật khẩu không trùng!!!'
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
            }
            );
            return false;
        });

        $(".changepass").unbind("click").click(function () {
            Common.ShowLoading(true);
            var id = $(this).data("id");

            Common.Ajax({
                type: "GET",
                url: Common.ThemNguoiDungHeThong.Url.DoiMatKhau,
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
                    Common.ThemNguoiDungHeThong.RegisterEvent();
                }
                else {
                    Common.ThemNguoiDungHeThong.Error();
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
            NhapLaimatKhau
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
                        Common.ThemNguoiDungHeThong.HideDialogChangePass();
                    }
                    else if (result.status == false && result.checkMK == true) {
                        Common.ShowLoading(false);
                        $.notify({
                            // options
                            message: 'Nhập lại mật khẩu không trùng!!!'
                        }, {
                                // settings
                            delay: 1000,
                            timer: 1000, type: 'danger'
                            });
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
                url: Common.ThemNguoiDungHeThong.Url.PhanQuyen,
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

                    Common.ThemNguoiDungHeThong.RegisterEvent();
                    Common.ThemNguoiDungHeThong.LoadComboPhanQuyen();
                }
                else {
                    Common.ThemNguoiDungHeThong.Error();
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
                Common.ThemNguoiDungHeThong.GenTableQuyen(Id, Name, dem + 1);
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
                $("#NhomQuyenadd").empty();
                $("#DialogPhanQuyen").find("#table-change-permission-user").find('tbody').append(strhtml);
                Common.ThemNguoiDungHeThong.RegisterEvent();
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
                url: Common.ThemNguoiDungHeThong.Url.PhanQuyen,
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
                        Common.ThemNguoiDungHeThong.HideDialogPhanQuyen();
                        $("a[data-action='reload']").click();
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
        $(".btnClose").unbind("click").click(function () {
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
                            window.location = Common.ThemNguoiDungHeThong.Url.Index;
                        },
                        Value: "Đồng ý"
                    }
                }
            }, "Cancel");
        });

    },
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.ThemNguoiDungHeThong.SetPage(page);
        Common.ThemNguoiDungHeThong.SubmitForm();
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
        Common.ThemNguoiDungHeThong.RegisterEvent();
    },
    CheckNull: function (str, labelthongbao, Nameforcus) {
        $("." + labelthongbao).html(Common.ThemNguoiDungHeThong.Message.Nhap + " " + str);
        $("." + labelthongbao).show();
        $("input[name='" + Nameforcus + "']").focus();
        return false;
    },
    checklength(str, labelthongbao, nameforcus, maxlength, nameshow) {
        if (str.length > 0) {
            str = Common.RemoveScript(str);
            if (str === '') {
                $("." + labelthongbao).html(nameshow + " " + Common.ThemNguoiDungHeThong.Message.MaJavaCript);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
            if (str.length > maxlength) {
                $("." + labelthongbao).html(Common.ThemNguoiDungHeThong.Message.Length + " " + nameshow + " không quá " + maxlength);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
        }
    },
    checkhtml(str, labelthongbao, nameforcus, nameshow) {

        $("." + labelthongbao).html(nameshow + " " + Common.ThemNguoiDungHeThong.Message.MaHtml);
        $("." + labelthongbao).show();
        $("input[name='" + nameforcus + "']").focus();
        return false;
    },
    checkten(str, control) {
        if (/[0-9]$/i.test(str) == true || /\`|\~|\!|\@|\#|\$|\%|\^|\&|\*|\(|\)|\+|\=|\[|\{|\]|\}|\||\\|\'|\<|\,|\.|\>|\?|\/|\""|\;|\:/i.test(str) == true) {
            $(".ThongBaoTen" + control).html(Common.ThemNguoiDungHeThong.Message.CheckTen);
            $(".ThongBaoTen" + control).show();
            $("input[name='" + control + "']").focus();
            return false;
        }
        return true;
    },
    checktextDB(str, control, message,strMsg) {
        if (/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/.test(str)) {
            $("." + message).html(strMsg +  Common.ThemNguoiDungHeThong.Message.CheckTenDB);
            $("." + message).show();
            $("input[name='" + control + "']").focus();
            return false;
        }
        return true;
    },
    checktenTK(str, control) {
        if (/\`|\~|\!|\@|\#|\$|\%|\^|\&|\*|\(|\)|\+|\=|\[|\{|\]|\}|\||\\|\'|\<|\,|\.|\>|\?|\/|\""|\;|\:/i.test(str) == true) {
            $(".thongbaotentaikhoan").html("Tên tài khoản không được nhập ký tự đặc biệt");
            $(".thongbaotentaikhoan").show();
            $("input[name='" + control + "']").focus();
            return false;
        }
        else if (/^[A-Za-z][A-Za-z0-9]*$/.test(str) == false) {
            $(".thongbaotentaikhoan").html("Tên tài khoản viết liền không dấu");
            $(".thongbaotentaikhoan").show();
            $("input[name='" + control + "']").focus();
            return false;
        }
        return true;
    },
    checkemail(str, control) {
        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})*$/i.test(str) == false) {
            $(".thongbaoemail").html(Common.ThemNguoiDungHeThong.Message.CheckEmail);
            $(".thongbaoemail").show();
            $("input[name='" + control + "']").focus();
            return false;
        }
        return true;
    },
    checkSDT(str, control) {
        if (/^[+]*[0-9]*$/i.test(str) == false) {
            $(".thongbaodienthoai").html("Số điện thoại không đúng định dạng");
            $(".thongbaodienthoai").show();
            $("input[name='" + control + "']").focus();
            return false;
        }
        if (str.length < 10 || str.length > 11) {
            $(".thongbaodienthoai").html("Số điện thoại không đúng định dạng");
            $(".thongbaodienthoai").show();
            $("input[name='" + control + "']").focus();
            return false;
        }
        return true;
    },
    checkCMND(str, control) {
        if (!Common.Empty(str)) {
            if (/[0-9]/i.test(str) == false) {
                $(".thongbaocmnd").html("CMND không đúng định dạng");
                $(".thongbaocmnd").show();
                $("input[name='" + control + "']").focus();
                return false;
            }
            else if (str.length != 9 && str.length != 12) {
                $(".thongbaocmnd").html("CMND không đúng định dạng");
                $(".thongbaocmnd").show();
                $("input[name='" + control + "']").focus();
                return false;
            }
        }
        
        return true;
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

                Common.ThemNguoiDungHeThong.getOptionByValue(select, String(dsquyen[j]));


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
        Common.ThemNguoiDungHeThong.RegisterEvent();

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
    OnInputCheckPassword: function (variable) {
        var strPassword = $(variable).val();
        var repassWord = $('input[name=NhapLaimatKhau]').val();
        $('.thongbaomatkhau').hide();
        if (strPassword.length < 6) {
            $('.thongbaomatkhaukytu').show();
            $('.thongbaomatkhaukytu').addClass("show");
        }
        else {
            $('.thongbaomatkhaukytu').hide();
            $('.thongbaomatkhaukytu').removeClass("show");
        }
        if (!/[0-9]/.test(strPassword)) {
            $('.thongbaomatkhauso').show();
            $('.thongbaomatkhauso').addClass("show");
        }
        else {
            $('.thongbaomatkhauso').hide();
            $('.thongbaomatkhauso').removeClass("show");
        }
        if (!/[a-z]/.test(strPassword)) {
            $('.thongbaomatkhauthuong').show();
            $('.thongbaomatkhauthuong').addClass("show");
        }
        else {
            $('.thongbaomatkhauthuong').hide();
            $('.thongbaomatkhauthuong').removeClass("show");
        }
        if (!/[A-Z]/.test(strPassword)) {
            $('.thongbaomatkhauhoa').show();
            $('.thongbaomatkhauhoa').addClass("show");
        }
        else {
            $('.thongbaomatkhauhoa').hide();
            $('.thongbaomatkhauhoa').removeClass("show");
        }
        if (repassWord && strPassword != repassWord) {
            $('.thongbaomatkhauxacnhan').show();
            $('.thongbaomatkhauxacnhan').addClass("show");
        }
        else {
            $('.thongbaomatkhauxacnhan').hide();
            $('.thongbaomatkhauxacnhan').removeClass("show");
        }
        if (strPassword && repassWord && $('.field-validation-error.show').length == 0)
            $('#btnLuu').attr('disabled', false);

        else
            $('#btnLuu').attr('disabled', true);
    },
    OnInputCheckRePassword: function (variable, element) {
        var passWord = $('input[name=MatKhau]').val();
        var repassWord = $(variable).val();
        $('.thongbaonhaplai').hide();

        if (passWord != repassWord) {
            $('.thongbaomatkhauxacnhan').show();
            $('.thongbaomatkhauxacnhan').addClass("show");
        }
        else {
            $('.thongbaomatkhauxacnhan').hide();
            $('.thongbaomatkhauxacnhan').removeClass("show");
        }
        if (passWord && repassWord && $('.field-validation-error.show').length == 0)
            $('#btnLuu').attr('disabled', false);
        else
            $('#btnLuu').attr('disabled', true);
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

};
// create NguoiDungHeThong Object
var ThemNguoiDungHeThong = ThemNguoiDungHeThong;
ThemNguoiDungHeThong.constructor = ThemNguoiDungHeThong;

