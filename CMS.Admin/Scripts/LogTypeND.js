var stridSelected = [];
var stridUnSelected = [];
var LogTypeND = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
var arrayquyen = [];
LogTypeND.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        var demcheck = 0;
        $('#tbLogTypeND tbody tr').each(function () {
            var checkbox = $(this).find("input:checkbox")
            if (checkbox.prop("checked") == true) {
                demcheck++;
            }
        });
        if (demcheck == $('#tbLogTypeND tbody tr').length) {
            $("#selectAll").prop('checked', 'checked');
        }
        $(".btnnhatkynguoidung").unbind("click").click(function () {
            window.open($(this).data("url"), '_blank');
        });
        $("input[name='Used']").unbind("click").click(function () {
            if (this.checked) {
                $(this).attr("value", "True");
            } else {
                $(this).attr("value", "False");
            }
        });
        $(".btnLuu").unbind("click").click(function () {
            var strid = '', stridUn = '';
            if (stridSelected.length > 0) {
                $.each(stridSelected, function (index, value) {
                    strid = strid + value + ',';
                });
            }
            if (stridUnSelected.length > 0) {
                $.each(stridUnSelected, function (index, value) {
                    stridUn = stridUn + value + ',';
                });
            }

            $('#tbLogTypeND tbody tr').each(function () {
                var checkbox = $(this).find("input:checkbox")
                if (checkbox.prop("checked") == true) {
                    var id = checkbox.data("id");
                    strid = strid + id + ',';
                }
            })
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.LogTypeND.Url.Luu,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: { ids: strid, idsUn: stridUn }
            }, function (result) {
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
                        stridSelected = [];
                        stridUnSelected = [];
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
        $("#selectAll").click(function () {
            $('input:checkbox').not(this).prop('checked', this.checked);
        });

        $(".btnDelRow").unbind("click").click(function () {
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
                                url: Common.LogTypeND.Url.Delete,
                                async: false,
                                cache: false,
                                data: {
                                    id: id
                                }
                            }, function (data) {
                                if (data) {
                                    $.notify({
                                        // options
                                        message: 'Xóa thành công.!!!'
                                    }, {
                                            // settings
                                            delay: 1000,
                                            timer: 1000, type: 'success'
                                        });

                                    Common.HideAlert();
                                    Common.LogTypeND.SubmitForm();
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
        $(".btnDeleteAll").unbind("click").click(function () {

            if ($('input[name="chkCustbl[]"]:checked').length > 0) {
                var lstid = [];
                $('input[name="chkCustbl[]"]').each(function () {
                    if ($(this).prop("checked")) {
                        var itemid = $(this).data("id");
                        lstid.push(itemid);
                    }
                });
                if (lstid.length > 0) {
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
                                        url: Common.LogTypeND.Url.DeleteLst,
                                        async: false,
                                        cache: false,
                                        data: {
                                            ids: lstid
                                        }
                                    }, function (data) {
                                        if (data) {
                                            Common.LogTypeND.SetPage(1);
                                            Common.LogTypeND.SubmitForm();
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
                }
            }
            else {
                $.notify({
                    // options
                    message: 'Vui lòng chọn dữ liệu để xóa!!!'
                }, {
                        // settings
                        delay: 1000,
                        timer: 1000, type: 'danger'
                    });
            }
            return false;
        });
        $("a[data-action='reload']").unbind("click").click(function () {
            Common.LogTypeND.SubmitForm();
        });
    },
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.LogTypeND.SetPage(page);
        Common.LogTypeND.SubmitForm();
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
        Common.LogTypeND.RegisterEvent();
    },
    CheckNull: function (str, labelthongbao, Nameforcus) {
        $("." + labelthongbao).html(Common.LogTypeND.Message.Nhap + " " + str);
        $("." + labelthongbao).show();
        $("input[name='" + Nameforcus + "']").focus();
        return false;
    },
    checklength(str, labelthongbao, nameforcus, maxlength, nameshow) {
        if (str.length > 0) {
            str = Common.RemoveScript(str);
            if (str === '') {
                $("." + labelthongbao).html(nameshow + " " + Common.LogTypeND.Message.MaJavaCript);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
            if (str.length > maxlength) {
                $("." + labelthongbao).html(Common.LogTypeND.Message.Length + " " + nameshow + " không quá " + maxlength);
                $("." + labelthongbao).show();
                $("input[name='" + nameforcus + "']").focus();
                return false;
            }
        }
    },
    checkhtml(str, labelthongbao, nameforcus, nameshow) {
        $("." + labelthongbao).html(nameshow + " " + Common.LogTypeND.Message.MaHtml);
        $("." + labelthongbao).show();
        $("input[name='" + nameforcus + "']").focus();
        return false;
    },
    f_ChonNhomNhatKy: function (e) {
        if ($(e).is(":checked")) {
            stridSelected.push($(e).attr('data-id'));
            stridUnSelected = Common.LogTypeND.f_RemoveItemArray(stridUnSelected, $(e).attr('data-id'));
        }
        else {
            stridSelected = Common.LogTypeND.f_RemoveItemArray(stridSelected, $(e).attr('data-id'));
            stridUnSelected.push($(e).attr('data-id'));
        }
    },
    f_RemoveItemArray: function (arr, item) {
        for (var i in arr) {
            if (arr[i].toString() == item.toString()) {
                arr.splice(i, 1);
                break;
            }
        }
        return arr;
    },
};
// create LogTypeND Object
var LogTypeND = LogTypeND;
LogTypeND.constructor = LogTypeND;

