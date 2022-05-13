var LogSystem = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
var lstid = [];
LogSystem.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea, select").val("");
            Common.LogSystem.SubmitForm();
        });
        $("#btnResetForm").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea, select").val("");
            Common.LogSystem.SubmitForm();
        });
        $("#CreatedFrom").datepicker({
            isRTL: false,
            format: 'dd/mm/yyyy',
            autoclose: true,
            orientation: 'bottom auto',
            useCurrent: false,
            language: 'vn'
        }).on('changeDate', function (e) {
            if (e.date) {
                var date = new Date(e.date);
                $('#CreatedTo').datepicker('setStartDate', date);
                var temp = $("#CreatedTo").datepicker('getDate');
                if (!temp || temp < date) {
                    $('#CreatedTo').datepicker("setDate", null);
                }
            }
            else {
                $('#CreatedTo').datepicker('setStartDate', null);
            }
        });

        $("#CreatedTo").datepicker({
            isRTL: false,
            format: 'dd/mm/yyyy',
            autoclose: true,
            useCurrent: false,
            orientation: 'bottom auto',
            language: 'vn'
        });
        $("#selectAll").click(function () {
            $('input:checkbox').not(this).prop('checked', this.checked);
            $('#recent-orders input[name="chkCustbl[]"]').each(function () {
                if ($(this).is(":checked")) {
                    if (!Common.LogSystem.f_CheckExistInArray(lstid, $(this).attr('data-id'))) {
                        lstid.push($(this).attr('data-id'));
                    }
                }
                else {
                    lstid = Common.LogSystem.f_RemoveItemArray(lstid, $(this).attr('data-id'));
                }
            });
        });
        $("#btnFormSearch").unbind("click").click(function () {
            Common.LogSystem.SetPage(1);
            Common.LogSystem.SubmitForm();
            return false;
        });
        $("input[name='Message']").unbind("keypress").bind("keypress", function (e) {
            if (e.which === 13) {
                Common.LogSystem.SetPage(1);
                Common.LogSystem.SubmitForm();
                return false;
            }
        });
        $(".detail").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.LogSystem.Url.ChiTietLog,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'html',
                data: { id: id }
                },function (result) {
                    Common.ShowLoading(false);
                    if (!Common.Empty(result)) {
                        $("#form-detail").html(result);
                        Common.LogSystem.ShowDialog();
                    }
                }
            );
            return false;
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
                                url: Common.LogSystem.Url.Delete,
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
                                    Common.LogSystem.SubmitForm();
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
        $(".btnDelDetail").unbind("click").click(function () {
            event.preventDefault();
            var id = $("#thongtinlog").find("input[name='Id']").val();         
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
            Common.Ajax({
                type: "POST",
                url: Common.LogSystem.Url.Delete,
                async: false,
                cache: false,
                data: {
                    id: id
                }
            }, function (data) {
                if (data) {
                    $("#thongtinlog").modal("hide");
                    Common.LogSystem.SubmitForm();
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
            return false;
        });
        $(".btnDeleteAll").unbind("click").click(function () {
            if ($('input[name="chkCustbl[]"]:checked').length > 0) {
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
                        url: Common.LogSystem.Url.DeleteLst,
                        async: false,
                        cache: false,
                        data: {
                            ids: lstid
                        }
                    }, function (data) {
                        if (data) { 
                            Common.LogSystem.SetPage(1);
                            Common.LogSystem.SubmitForm();
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
    },
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.LogSystem.SetPage(page);
        Common.LogSystem.SubmitForm();
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
        Common.LogSystem.RegisterEvent();
    },
    ShowDialog: function () {
        $("#thongtinlog").modal("show")
        Common.LogSystem.RegisterEvent();
    },
    f_CheckedItemNhatKy: function (e) {
        if ($(e).is(":checked")) {
            if (!Common.LogSystem.f_CheckExistInArray(lstid, $(e).attr('data-id'))) {
                lstid.push($(e).attr('data-id'));
            }
        }
        else {
            lstid = Common.LogSystem.f_RemoveItemArray(lstid, $(e).attr('data-id'));
        }
    },
    f_RemoveItemArray: function (arr, item) {
        for (var i in arr) {
            if (arr[i] == item) {
                arr.splice(i, 1);
                break;
            }
        }
        return arr;
    },
    f_CheckExistInArray: function (arr, item) {
        var _result = false;
        for (var i in arr) {
            if (arr[i].toString() == item.toString()) {
                _result = true;
                break;
            }
        }
        return _result;
    },
    f_CheckedItemInArray: function () {
        if (lstid.length > 0) {
            // console.log(item)
            for (var i in lstid) {
                //console.log(array[i]);
                $('input[data-id=' + lstid[i] + ']').prop('checked', true);
            }

        }
    }
};
// create LogSystem Object
var LogSystem = LogSystem;
LogSystem.constructor = LogSystem;
