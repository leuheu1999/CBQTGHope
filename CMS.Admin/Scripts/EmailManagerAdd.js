var ThemEmailManager = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
ThemEmailManager.prototype = {
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

        $('.btnAddParameter').unbind("click").click(function () {
            var Body = CKEDITOR.instances.editor.getData();
            var Param = $('select[name=Parameter]').val();
            var newBody = Body.substring(0, Body.length - 5) + ' ' + Param + '</p>';
            CKEDITOR.instances.editor.setData(newBody);
        });

        $(".btnLuu").unbind("click").click(function () {

            var ID = $.trim($("input[name='ID']").val());
            var Title = $.trim($("input[name='Title']").val());
            var Body = CKEDITOR.instances.editor.getData();//editor.getData();
            var ThuTuHienThi = $.trim($("input[name='ThuTuHienThi']").val());
            var Used = $.trim($("input[name='Used']").val());
           
            //Title
            $('.thongabaotitle').hide();
            if (Common.Empty(Title)) {
                check = Common.ThemEmailManager.CheckNull("Tiêu đề", "thongabaotitle", "Title");
                if (check == false) return;
            }
            else {
                check = Common.ThemEmailManager.checklength(Title, "thongabaotitle", "Title", 1000, "tiêu đề");
                if (check == false) return;
            }

            //Title
            $('.thongbaonoidung').hide();
            if (Common.Empty(Body)) {
                check = Common.ThemEmailManager.CheckNull("Nội dung", "thongbaonoidung", "Body");
                if (check == false) return;
            }
            var data = {
                ID:ID,
                Title:Title,
                Body:Body,
                ThuTuHienThi:ThuTuHienThi,
                Used:Used,
                IsSendMail: $('input[name="IsSendMail"]').is(":checked") ?true: false
            }
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.ThemEmailManager.Url.ThemMoi,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: {
                        model: data
                    }
                },function (result) {
                    Common.ShowLoading(true);
                    if (!Common.Empty(result)) {
                        if (result.status == true) {
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
      
        //MaiNguyen: fixbug 
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
                            window.location = Common.ThemEmailManager.Url.Index;
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
        Common.ThemEmailManager.RegisterEvent();
    },
    CheckNull: function (str, labelthongbao, Nameforcus) {
        $("." + labelthongbao).html(Common.ThemEmailManager.Message.Nhap + " " + str);
        $("." + labelthongbao).show();
        $("input[name='" + Nameforcus + "']").focus();
        return false;
    },
    checklength(str, labelthongbao, nameforcus, maxlength, nameshow) {
        if (str.length > 0) {
            str = Common.RemoveScript(str);
            if (str === '') {
                $("." + labelthongbao).html(nameshow + " " + Common.ThemEmailManager.Message.MaJavaCript);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
            if (str.length > maxlength) {
                $("." + labelthongbao).html(Common.ThemEmailManager.Message.Length + " " + nameshow + " không quá " + maxlength);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
        }
    },
    checkhtml(str, labelthongbao, nameforcus, nameshow) {

        $("." + labelthongbao).html(nameshow + " " + Common.ThemEmailManager.Message.MaHtml);
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
var ThemEmailManager = ThemEmailManager;
ThemEmailManager.constructor = ThemEmailManager;

