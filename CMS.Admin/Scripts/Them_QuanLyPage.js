var ThemQuanLyPage = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
ThemQuanLyPage.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("input[name='Used']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });
        $(".btnLuu").unbind("click").click(function () {
            var form = $("#form-update");
            Common.ValidatorForm("#form-update");
            if (form.find(".field-validation-error").length == 0) {
                var data = {
                    ID: form.find("input[name='ID']").val() || 0,
                    PageName: form.find("input[name='PageName']").val() || null,
                    Title: form.find("input[name='Title']").val() || null,
                    MoTaNgan: $.trim(form.find("textarea[name='MoTaNgan']").val()) || null,
                    Body: Common.Base64EncodeUnicode(Common.EscapeHtml(tinyMCE.get('Body').getContent()) || ""),//base64,
                    Img: null,
                    Img_400x300: null,
                    Img_640x480: null,
                    Img_800x600: null,
                    ThuTuHienThi: form.find("input[name='ThuTuHienThi']").val() || 0,
                    CongKhai: form.find('input[name="CongKhai"]').is(":checked") ? true : false,
                    IsShowHome: form.find('input[name="IsShowHome"]').is(":checked") ? true : false,
                    SeName: form.find("input[name='SeName']").val() || null,
                    MetaKeywords: $.trim(form.find("input[name='MetaKeywords']").val()) || null,
                    MetaDescription: form.find("textarea[name='MetaDescription']").val() || null,
                    MetaTitle: form.find("input[name='MetaTitle']").val() || null,
                    CreatedUserID: Common.NewGuild(),
                };
                Common.ShowLoading(true);
                Common.Ajax({
                    url: Common.ThemQuanLyPage.Url.ThemMoi,
                    type: "POST",
                    ContentType: 'application/json; charset=utf-8',
                    async: false,
                    cache: false,
                    dataType: 'JSON',
                    data: {
                        model: data
                    }
                }, function (result) {
                    Common.ShowLoading(true);
                    if (!Common.Empty(result)) {
                        if (result.id == -5) {
                            $.notify({
                                // options
                                message: 'Tên trang tối ưu cho SEO đã tồn tại!!!'
                            }, {
                                // settings
                                delay: 1000,
                                timer: 1000, type: 'danger'
                            });
                        }
                        else if (result.status == true) {
                            if ($.trim($("ID").val()) > 0) {
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
                            window.location.href = $(".btnLuu").data('url');
                        }
                        else if (result.status == false && result.checkMa == true) {
                            $.notify({
                                // options
                                message: 'Trang này đã tồn tại!!!'
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
            }
            return false;
        });

        //MaiNguyen: fixbug 
        $(".btnClose").unbind("click").click(function () {
            Common.ShowAlert("Thông báo", "Bạn có chắc muốn đóng trình chỉnh sửa?", {
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
                            window.location = Common.ThemQuanLyPage.Url.Index;
                        },
                        Value: "Đồng ý"
                    }
                }
            }, "Cancel");
        });
    },
    BeforeSend: function (xhr) {
    },
    Success: function (data) {
        Common.ShowLoading(false);
        $("body,html").animate({
            scrollTop: 180
        }, 1000);
        Common.ThemQuanLyPage.RegisterEvent();
    },
    CheckNull: function (str, labelthongbao, Nameforcus) {
        $("." + labelthongbao).html(Common.ThemQuanLyPage.Message.Nhap + " " + str);
        $("." + labelthongbao).show();
        $("input[name='" + Nameforcus + "']").focus();
        return false;
    },
    checklength(str, labelthongbao, nameforcus, maxlength, nameshow) {
        if (str.length > 0) {
            str = Common.RemoveScript(str);
            if (str === '') {
                $("." + labelthongbao).html(nameshow + " " + Common.ThemQuanLyPage.Message.MaJavaCript);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
            if (str.length > maxlength) {
                $("." + labelthongbao).html(Common.ThemQuanLyPage.Message.Length + " " + nameshow + " không quá " + maxlength);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
        }
    },
    checkhtml(str, labelthongbao, nameforcus, nameshow) {

        $("." + labelthongbao).html(nameshow + " " + Common.ThemQuanLyPage.Message.MaHtml);
        $("." + labelthongbao).show();
        $("input[name='" + nameforcus + "']").focus();
        return false;
    },
    escapeHtml: function (string) {
        return string
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;");
    },
};
// create NguoiDungHeThong Object
var ThemQuanLyPage = ThemQuanLyPage;
ThemQuanLyPage.constructor = ThemQuanLyPage;

