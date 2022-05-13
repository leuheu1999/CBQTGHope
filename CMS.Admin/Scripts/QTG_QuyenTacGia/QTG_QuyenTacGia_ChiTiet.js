var QTG_QuyenTacGia_ChiTiet = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
QTG_QuyenTacGia_ChiTiet.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;

        $("#btnXoaQuyenTacGia").unbind("click").click(function () {
            var id = $(this).data("id");
            if (Common.Empty(id)) {
                $.notify({
                    message: 'Dữ liệu cần xóa không tồn tại.!!!'
                }, {
                    delay: 1000,
                    timer: 1000, type: 'danger'
                });
                return false;
            }
            Common.ShowAlert("Thông báo", "Xóa dữ liệu đang chọn?", {
                Close: {
                    Display: true
                },
                Items: {
                    Cancel: {
                        Name: "Cancel",
                        Data: $(this),
                        Class: "btn-info ",
                        OnClick: function (target) {
                            Common.HideAlert();
                            Common.Ajax({
                                type: "POST",
                                url: Common.QTG_QuyenTacGia_ChiTiet.Url.DeleteQuyenTacGia,
                                async: false,
                                cache: false,
                                data: {
                                    id: id
                                }
                            }, function (data) {
                                if (data) {
                                    $.notify({
                                        // options
                                        message: 'Xóa dữ liệu thành công!!!'
                                    }, {
                                        // settings
                                        delay: 1000,
                                        timer: 1000, type: 'success'
                                    });
                                    Common.HideAlert();
                                    window.location = Common.QTG_QuyenTacGia_ChiTiet.Url.Index;
                                } else {
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

        $("#btn-exportWord").unbind("click").click(function () {
            var _model = '"QuyenTacGiaID":"' + $(this).data("id") + '"';
            var _key = 'ExportQTG_GiayChungNhan_Mau1_Word';
            for (var i = 0; i < Common.QTG_QuyenTacGia_SearchCongDan.ListTacGia.length; i++) {
                var check = Common.QTG_QuyenTacGia_SearchCongDan.ListChuSoHuu.find(x => x.SoCMND !== Common.QTG_QuyenTacGia_SearchCongDan.ListTacGia[i].SoCMND);
                if (typeof check != 'undefined') {
                    _key = 'ExportQTG_GiayChungNhan_Mau2_Word';
                }
            }

            Common.Ajax({
                type: "POST",
                url: Common.QTG_QuyenTacGia_ChiTiet.Url.ExportWordPdf,
                async: false,
                cache: false,
                data: { Param: _model, Key: _key }
            }, function (result) {
                if (result) {
                    let a = document.createElement('a')
                    a.href = 'data:application/octet-stream;base64,' + result.data;
                    a.download = result.fileName;
                    document.body.appendChild(a)
                    a.click();
                    document.body.removeChild(a);
                }
                else {
                    $.notify({
                        // options
                        message: "Xuất file không thành công!!!",
                    }, {
                        // settings
                        delay: 1000,
                        timer: 1000, type: 'danger'
                    });
                    return false;
                }
            });
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
                            window.location = Common.QTG_QuyenTacGia_ChiTiet.Url.Index;
                        },
                        Value: "Đồng ý"
                    }
                }
            }, "Cancel");
        });
    },
};
var QTG_QuyenTacGia_ChiTiet = QTG_QuyenTacGia_ChiTiet;
QTG_QuyenTacGia_ChiTiet.constructor = QTG_QuyenTacGia_ChiTiet;