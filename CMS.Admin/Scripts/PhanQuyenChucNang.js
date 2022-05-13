var PhanQuyenChucNang = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
var arrayquyen = [];
PhanQuyenChucNang.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("input[name='checkchon']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });
        $(".btnnhomquyen").unbind("click").click(function () {
            window.open($(this).data("url"), '_blank');
        });
        $(".btnquyen").unbind("click").click(function () {
            window.open($(this).data("url"), '_blank');
        });
        $(".btnnguoidung").unbind("click").click(function () {
            window.open($(this).data("url"), '_blank');
        });
        $(".btnLuu").unbind("click").click(function () {
            var arraydata = [];
            $('#tbphanquyenchucnang tbody tr').each(function () {
                $(this).find('td').each(function () {
                    var checkbox = $(this).find("input:checkbox")
                    if (checkbox.prop("checked") == true) {
                        var idnhomquyen = checkbox.data("idnhomquyen");
                        var quyenchucnangid = checkbox.data("quyenchucnangid");
                        arraydata.push({
                            ID: 0,
                            NhomQuyenID: idnhomquyen,
                            QuyenChucNangID: quyenchucnangid
                        });
                    }
                })
            });
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.PhanQuyenChucNang.Url.CapNhat,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: {model: arraydata}
                },function (result) {
                    Common.ShowLoading(true);
                    if (!Common.Empty(result)) {
                        if (result.status == true) {
                                $.notify({
                                    // options
                                    message: 'Lưu thành công.!!!'
                                }, {
                                        // settings
                                    delay: 1000,
                                    timer: 1000, type: 'success'
                                    });
                        }
                        else
                            $.notify({
                                // options
                                message: 'Lưu không thành công!!!'
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
            );
            return false;
        });
        $("#tbphanquyenchucnang [data-type='Category']").unbind("click").click(function () {
            var id = $(this).data().id;
            if ($(this).data("status") == "minus") {
                $(this).find(".icon").html("<i class='fa fa-plus-square' aria-hidden='true'></i>");
                $(this).closest("tbody").find("tr[data-category='" + id + "']").hide();
                $(this).data("status", "plus");
            } else {
                $(this).find(".icon").html("<i class='fa fa-minus-square' aria-hidden='true'></i>");
                $(this).data("status", "minus");
                $(this).closest("tbody").find("tr[data-category='" + id + "']").show();
            }
        });
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
        Common.PhanQuyenChucNang.RegisterEvent();
    }
};
// create PhanQuyenChucNang Object
var PhanQuyenChucNang = PhanQuyenChucNang;
PhanQuyenChucNang.constructor = PhanQuyenChucNang;

