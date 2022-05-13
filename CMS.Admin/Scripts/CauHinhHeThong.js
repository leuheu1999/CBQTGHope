var CauHinhHeThong = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
CauHinhHeThong.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;

        $("#btnLuu").unbind("click").click(function () {
            var ThoiGianTruyCapTu = $.trim($("input[name='cauhinhhethong.ThoiGianTruyCapTu']").val());
            var ThoiGianTruyCapDen = $.trim($("input[name='cauhinhhethong.ThoiGianTruyCapDen']").val());
            var SoHangHienThi = $.trim($("input[name='cauhinhhethong.SoHangHienThi']").val());
            var TieuDeTrang = $.trim($("input[name='cauhinhhethong.TieuDeTrang']").val());
            var TuKhoa = $.trim($("input[name='cauhinhhethong.TuKhoa']").val());
            var MoTaTuKhoa = $.trim($("input[name='cauhinhhethong.MoTaTuKhoa']").val());
            var Email = $.trim($("input[name='cauhinhmail.Email']").val());
            var TenHienThi = $.trim($("input[name='cauhinhmail.TenHienThi']").val());
            var Host = $.trim($("input[name='cauhinhmail.Host']").val());
            var Port = $.trim($("input[name='cauhinhmail.Port']").val());
            var NguoiDung = $.trim($("input[name='cauhinhmail.NguoiDung']").val());
            var MatKhau = $.trim($("input[name='cauhinhmail.MatKhau']").val());
            var TenDonVi = $.trim($("input[name='donvi.TenDonVi']").val());
            var TenVietTat = $.trim($("input[name='donvi.TenVietTat']").val());
            var DiaChi = $.trim($("input[name='donvi.DiaChi']").val());
            var DienThoai = $.trim($("input[name='donvi.DienThoai']").val());
            var Fax = $.trim($("input[name='donvi.Fax']").val());
            var Emaildv = $.trim($("input[name='donvi.Email']").val());
            var Website = $.trim($("input[name='donvi.Website']").val());
            var Copyright = $.trim($("input[name='donvi.Copyright']").val());
            var check = true;
            //kiem tra SoHangHienThi
            $('.errorsohanghienthi').hide();
            if (Common.Empty(SoHangHienThi)) {
                check = Common.CauHinhHeThong.CheckNull(SoHangHienThi, "errorsohanghienthi", "cauhinhhethong.SoHangHienThi");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(SoHangHienThi, "errorsohanghienthi", "cauhinhhethong.SoHangHienThi", 10, "Số hàng hiển thị")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(SoHangHienThi) || (/<[/s/S][a-z][\s\S]*>/i.test(SoHangHienThi))) {
                check = Common.CauHinhHeThong.checkhtml(SoHangHienThi, "errorsohanghienthi", "cauhinhhethong.SoHangHienThi", "Số hàng hiển thị")
                if (check == false) return;
            }
            //kiem tra TieuDeTrang
            $('.errortieude').hide();
            if (Common.Empty(TieuDeTrang)) {
                check = Common.CauHinhHeThong.CheckNull(TieuDeTrang, "errortieude", "cauhinhhethong.TieuDeTrang");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(TieuDeTrang, "errortieude", "cauhinhhethong.TieuDeTrang", 1000, "Tiêu đề trang")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(TieuDeTrang) || (/<[/s/S][a-z][\s\S]*>/i.test(TieuDeTrang))) {
                check =  Common.CauHinhHeThong.checkhtml(TieuDeTrang, "errortieude", "cauhinhhethong.TieuDeTrang", "Tiêu đề trang")
                if (check == false) return;
            }
            //kiem tra TuKhoa
            $('.errortukhoa').hide();
            if (Common.Empty(TuKhoa)) {
                check = Common.CauHinhHeThong.CheckNull(TuKhoa, "errortukhoa", "cauhinhhethong.TuKhoa");

                if (check == false) return;}
            else {
                check = Common.CauHinhHeThong.checklength(TuKhoa, "errortukhoa", "cauhinhhethong.TuKhoa", 1000, "Từ khóa")
            }
            if (/<[a-z][\s\S]*>/i.test(TuKhoa) || (/<[/s/S][a-z][\s\S]*>/i.test(TuKhoa))) {
                check = Common.CauHinhHeThong.checkhtml(TuKhoa, "errortukhoa", "cauhinhhethong.TuKhoa", "Từ khóa")
                if (check == false) return;
            }
            //kiem tra MoTaTuKhoa
            $('.errormotatukhoa').hide();
            if (Common.Empty(MoTaTuKhoa)) {
                check = Common.CauHinhHeThong.CheckNull(MoTaTuKhoa, "errormotatukhoa", "cauhinhhethong.MoTaTuKhoa");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(MoTaTuKhoa, "errormotatukhoa", "cauhinhhethong.MoTaTuKhoa", 1000, "Từ khóa")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(MoTaTuKhoa) || (/<[/s/S][a-z][\s\S]*>/i.test(MoTaTuKhoa))) {
                check = Common.CauHinhHeThong.checkhtml(MoTaTuKhoa, "errormotatukhoa", "cauhinhhethong.MoTaTuKhoa", "Từ khóa")
                if (check == false) return;
            }
            //kiem tra Email
            $('.erroremail').hide();
            if (Common.Empty(Email)) {
                check = Common.CauHinhHeThong.CheckNull(Email, "erroremail", "cauhinhmail.Email");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(Email, "erroremail", "cauhinhmail.Email", 100, "Email")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(Email) || (/<[/s/S][a-z][\s\S]*>/i.test(Email))) {
                check =  Common.CauHinhHeThong.checkhtml(Email, "erroremail", "cauhinhmail.Email", "Email")
                if (check == false) return;
            }
            //kiem tra TenHienThi
            $('.errortenhienthi').hide();
            if (Common.Empty(TenHienThi)) {
                check = Common.CauHinhHeThong.CheckNull(TenHienThi, "errortenhienthi", "cauhinhmail.TenHienThi");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(TenHienThi, "errortenhienthi", "cauhinhmail.TenHienThi", 1000, "Tên hiển thị")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(TenHienThi) || (/<[/s/S][a-z][\s\S]*>/i.test(TenHienThi))) {
                check =  Common.CauHinhHeThong.checkhtml(TenHienThi, "errortenhienthi", "cauhinhmail.TenHienThi", "Tên hiển thị")
                if (check == false) return;
            }
            //kiem tra Host
            $('.errorhost').hide();
            if (Common.Empty(Host)) {
                check = Common.CauHinhHeThong.CheckNull(Host, "errorhost", "cauhinhmail.Host");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(Host, "errorhost", "cauhinhmail.Host", 100, "Host")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(Host) || (/<[/s/S][a-z][\s\S]*>/i.test(Host))) {
                check = Common.CauHinhHeThong.checkhtml(Host, "errorhost", "cauhinhmail.Host", "Host")
                if (check == false) return;
            }
            //kiem tra Port
            $('.errorport').hide();
            if (Common.Empty(Port)) {
                check = Common.CauHinhHeThong.CheckNull(Port, "errorport", "cauhinhmail.Port");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(Port, "errorport", "cauhinhmail.Port", 100, "Cổng")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(Port) || (/<[/s/S][a-z][\s\S]*>/i.test(Port))) {
                check = Common.CauHinhHeThong.checkhtml(Port, "errorport", "cauhinhmail.Port", "Cổng")
                if (check == false) return;
            }
            //kiem tra NguoiDung
            $('.errornguoidung').hide();
            if (Common.Empty(NguoiDung)) {
                check = Common.CauHinhHeThong.CheckNull(NguoiDung, "errornguoidung", "cauhinhmail.NguoiDung");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(NguoiDung, "errornguoidung", "cauhinhmail.NguoiDung", 100, "Cổng")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(NguoiDung) || (/<[/s/S][a-z][\s\S]*>/i.test(NguoiDung))) {
                check = Common.CauHinhHeThong.checkhtml(NguoiDung, "errornguoidung", "cauhinhmail.NguoiDung", "Cổng")
                if (check == false) return;
            }
            //kiem tra MatKhau
            $('.errormatkhau').hide();
            if (Common.Empty(MatKhau)) {
                check = Common.CauHinhHeThong.CheckNull(MatKhau, "errormatkhau", "cauhinhmail.MatKhau");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(MatKhau, "errormatkhau", "cauhinhmail.MatKhau", 100, "Mật khẩu")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(MatKhau) || (/<[/s/S][a-z][\s\S]*>/i.test(MatKhau))) {
                check =  Common.CauHinhHeThong.checkhtml(MatKhau, "errormatkhau", "cauhinhmail.MatKhau", "Mật khẩu")
                if (check == false) return;
            }
            //kiem tra TenDonVi
            $('.errortendonvi').hide();
            if (Common.Empty(TenDonVi)) {
                check = Common.CauHinhHeThong.CheckNull(TenDonVi, "errortendonvi", "donvi.TenDonVi");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(TenDonVi, "errortendonvi", "donvi.TenDonVi", 1000, "Tên đơn vị")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(TenDonVi) || (/<[/s/S][a-z][\s\S]*>/i.test(TenDonVi))) {
                check = Common.CauHinhHeThong.checkhtml(TenDonVi, "errortendonvi", "donvi.TenDonVi", "Tên đơn vị")
                if (check == false) return;
            }
            //kiem tra TenVietTat
            $('.errortenviettat').hide();
            if (Common.Empty(TenVietTat)) {
                check = Common.CauHinhHeThong.CheckNull(TenVietTat, "errortenviettat", "donvi.TenVietTat");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(TenVietTat, "errortenviettat", "donvi.TenVietTat", 100, "Tên viết tắt")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(TenVietTat) || (/<[/s/S][a-z][\s\S]*>/i.test(TenVietTat))) {
                check =  Common.CauHinhHeThong.checkhtml(TenVietTat, "errortenviettat", "donvi.TenVietTat", "Tên viết tắt")
                if (check == false) return;
            }
            //kiem tra DiaChi
            $('.errordiachi').hide();
            if (Common.Empty(DiaChi)) {
                check = Common.CauHinhHeThong.CheckNull(DiaChi, "errordiachi", "donvi.DiaChi");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(DiaChi, "errordiachi", "donvi.DiaChi", 1000, "Địa chỉ")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(DiaChi) || (/<[/s/S][a-z][\s\S]*>/i.test(DiaChi))) {
                check = Common.CauHinhHeThong.checkhtml(DiaChi, "errordiachi", "donvi.DiaChi", "Địa chỉ")
                if (check == false) return;
            }
            //kiem tra DiaChi
            $('.errordienthoai').hide();
            if (Common.Empty(DienThoai)) {
                check =  Common.CauHinhHeThong.CheckNull(DienThoai, "errordienthoai", "donvi.DienThoai");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(DienThoai, "errordienthoai", "donvi.DienThoai", 20, "Điện thoại")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(DienThoai) || (/<[/s/S][a-z][\s\S]*>/i.test(DienThoai))) {
                check = Common.CauHinhHeThong.checkhtml(DienThoai, "errordienthoai", "donvi.DienThoai", "Điện thoại")
                if (check == false) return;
            }
            //kiem tra DiaChi
            $('.errorfax').hide();
            if (Common.Empty(Fax)) {
                check = Common.CauHinhHeThong.CheckNull(Fax, "errorfax", "donvi.Fax");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(Fax, "errorfax", "donvi.Fax", 20, "Fax")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(Fax) || (/<[/s/S][a-z][\s\S]*>/i.test(Fax))) {
                check = Common.CauHinhHeThong.checkhtml(Fax, "errorfax", "donvi.Fax", "Fax")
                if (check == false) return;
            }
            //kiem tra DiaChi
            $('.erroremaildonvi').hide();
            if (Common.Empty(Emaildv)) {
                check = Common.CauHinhHeThong.CheckNull(Emaildv, "erroremaildonvi", "donvi.Email");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(Emaildv, "erroremaildonvi", "donvi.Email", 50, "Email đơn vị")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(Emaildv) || (/<[/s/S][a-z][\s\S]*>/i.test(Emaildv))) {
                check =  Common.CauHinhHeThong.checkhtml(Emaildv, "erroremaildonvi", "donvi.Email", "Email đơn vị")
                if (check == false) return;
            }
            //kiem tra Website
            $('.errorwebsite').hide();
            if (Common.Empty(Website)) {
                check = Common.CauHinhHeThong.CheckNull(Website, "errorwebsite", "donvi.Website");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(Website, "errorwebsite", "donvi.Website", 100, "Website")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(Website) || (/<[/s/S][a-z][\s\S]*>/i.test(Website))) {
                check = Common.CauHinhHeThong.checkhtml(Website, "errorwebsite", "donvi.Website", "Website")
                if (check == false) return;
            }
            //kiem tra Website
            $('.errorcoppy').hide();
            if (Common.Empty(Copyright)) {
                check =  Common.CauHinhHeThong.CheckNull(Copyright, "errorcoppy", "donvi.Copyright");
                if (check == false) return;
            }
            else {
                check = Common.CauHinhHeThong.checklength(Copyright, "errorcoppy", "donvi.Copyright", 100, "Copyright")
                if (check == false) return;
            }
            if (/<[a-z][\s\S]*>/i.test(Copyright) || (/<[/s/S][a-z][\s\S]*>/i.test(Copyright))) {
                check = Common.CauHinhHeThong.checkhtml(Copyright, "errorcoppy", "donvi.Copyright", "Copyright")
                if (check == false) return;
            }
            var data = $("#form-update").serialize();
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.CauHinhHeThong.Url.Update,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: data
                },function (result) {
                    Common.ShowLoading(true);
                    if (!Common.Empty(result)) {
                        if (result.status == true) {
                            if ($.trim($("#cauhinhhethong.ID").val()) > 0) {
                                $.notify({
                                    // options
                                    message: 'Cập nhật thành công.!!!'
                                }, {
                                    // settings
                                    delay: 1000,
                                    timer: 1000,
                                        type: 'success'
                                    });
                            }
                            else {
                                $.notify({
                                    // options
                                    message: 'Lưu thành công.!!!'
                                }, {
                                    // settings
                                    delay: 1000,
                                    timer: 1000,
                                        type: 'success'
                                    });
                            }
                        }
                        else {
                            $.notify({
                                // options
                                message: 'lưu không thành công.!!!'
                            }, {
                                // settings
                                delay: 1000,
                                timer: 1000,
                                    type: 'danger'
                                });
                        }
                    }
                }
            );
            return false;
        });

        $(".doimatkhau").unbind("click").click(function () {
            var pass = $.trim($("input[name='cauhinhmail.MatKhau']").val());
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.CauHinhHeThong.Url.DoiMatKhau,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: { pass: pass }
                },function (result) {
                    Common.ShowLoading(true);
                    if (!Common.Empty(result)) {
                        if (result.status == true) {
                        $.notify({
                            // options
                            message: 'đổi mật khẩu thành công.!!!'
                        }, {
                            // settings
                            delay: 1000,
                            timer: 1000,
                            type: 'success'
                            });
                        }
                    }
                    else {
                        $.notify({
                            // options
                            message: 'đổi mật khẩu không thành công.!!!'
                        }, {
                            // settings
                            delay: 1000,
                            timer: 1000,
                                type: 'danger'
                            });
                    }
                }
            );
            return false;
        });

        $("a[data-action='reload']").unbind("click").click(function () {
            Common.CauHinhHeThong.SubmitForm();
        });
    },
  
    BeforeSend: function (xhr) {
    },
    SubmitForm: function () {
        Common.ShowLoading(true);
        window.location.reload(true);
    },
    Success: function (data) {
        Common.ShowLoading(false);
        $("body,html").animate({
            scrollTop: 180
        }, 1000);
        Common.CauHinhHeThong.RegisterEvent();
    },
    CheckNull: function(str, labelthongbao,Nameforcus) {
            $("." + labelthongbao).html(Common.CauHinhHeThong.Message.batbuocnhap);
            $("." + labelthongbao).show();
            $("input[name='" + Nameforcus + "']").focus();
            return false;
    },
    checklength(str, labelthongbao, nameforcus,maxlength,nameshow) {
        if (str.length > 0) {
            str = Common.RemoveScript(str);
            if (str === '') {
                $("." + labelthongbao).html(nameshow + " " + Common.CauHinhHeThong.Message.MaJavaCript);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
            if (str.length > maxlength) {
                $("." + labelthongbao).html(Common.CauHinhHeThong.Message.Length + maxlength);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
        }
    },
    checkhtml(str, labelthongbao, nameforcus, nameshow) {
       
            $("." + labelthongbao).html(nameshow + " " + Common.CauHinhHeThong.Message.MaHtml);
            $("." + labelthongbao).show();
        $("input[name='" + nameforcus + "']").focus();
        return false;
    }
};
// create CauHinhHeThong Object
var CauHinhHeThong = CauHinhHeThong;
CauHinhHeThong.constructor = CauHinhHeThong;
