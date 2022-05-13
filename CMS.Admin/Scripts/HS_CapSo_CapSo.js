var HS_CapSo_CapSo = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
HS_CapSo_CapSo.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
     
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        const that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#form-update").find("input[type=text], input[type=hidden] , textarea").val("");
            $("#form-update").find("select").val("");
            Common.HS_CapSo_CapSo.RefeshSelect2('select');
            $("#form-update input[type=text]").prop("disabled", false);
            Common.HS_CapSo_CapSo.SubmitForm();
        });
        $(".btnHuy").unbind("click").click(function () {
            $("#form-update").find("input[type=text], input[type=hidden] , textarea").val("");
            $("#form-update").find("select").val("");
            Common.HS_CapSo_CapSo.RefeshSelect2('select');
            $("#form-update input[type=text]").prop("disabled", false);
            // Common.HS_CapSo_CapSo.SubmitForm();
        });
        $(".btnClose").unbind("click").click(function () {
            window.location.href = Common.HS_CapSo_CapSo.Url.Index;
        });
        $("#form-update #LoaiSoID").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#form-update #LoaiSoID option:selected").val();
           
            if (val) {
                Common.ShowLoading(true);
                Common.HS_CapSo_CapSo.AjaxGetPrefix(val);
            }
            else {
                $("#form-update").find("#So").val("");
                $("#form-update").find("#Prefix").val("");
                $("#form-update").find("#TuTang").prop("checked", false);
                $("#form-update input[name=Prefix]").prop("disabled", false);
                $("#form-update input[name=So]").prop("disabled", false);
            }
        });
        $("#form-update input[name=TuTang]").unbind("change").on("change", (e) => {
            const checked = $(e.target).is(':checked');
            $("#form-update input[name=TuTang]").val(checked);
            $("#form-update input[name=Prefix]").prop("disabled", checked);
            $("#form-update input[name=So]").prop("disabled", checked)
            return false;
        });
        $('#form-update input[name=So').unbind("input").on('input', function (e) {
            this.value = this.value.replace(/[^0-9]/g, '');
        });
        $(".select2-share").select2();
        $(".btnLuu").unbind("click").click(function () {
            Common.ValidatorForm("#form-update");
            if ($("#form-update").find(".field-validation-error").length == 0) {
                var _model = Common.HS_CapSo_CapSo.SerializeObject($("#form-update"));
                _model.HoSoID = $('#HoSoID').val();
                _model.LoaiNghiepVuID = $('#LoaiNghiepVuID').val();
                _model.So = $('#form-update input[name=So]').val();
                _model.Prefix = $('#form-update input[name=Prefix]').val();
                Common.ShowLoading(true);
                Common.Ajax({
                    type: "Post",
                    ContentType: 'application/json; charset=utf-8',
                    async: false,
                    cache: false,
                    url: Common.HS_CapSo_CapSo.Url.ThemMoiCapSo,
                    dataType: 'JSON',
                    data: _model
                }, function (result) {
                        if (!Common.Empty(result)) {
                            if (result.status == true) {
                                if ($.trim($("#CapSoID").val()) > 0) {
                                    $.notify({
                                        // options
                                        message: 'Cập nhật thành công.!!!'
                                    }, {
                                        // settings
                                        delay: 1000,
                                        timer: 3000, type: 'success'
                                    });
                                }
                                else {
                                    $.notify({
                                        // options
                                        message: 'Thêm mới thành công.!!!'
                                    }, {
                                        // settings
                                        delay: 1000,
                                        timer: 3000, type: 'success'
                                    });
                                }
                                $("#form-update").find("input[type=text], input[type=hidden] , textarea").val("");
                                $("#form-update").find("select").val("");
                                Common.HS_CapSo_CapSo.RefeshSelect2('select');
                                $("#form-update input[type=text]").prop("disabled", false);
                                Common.HS_CapSo_CapSo.SubmitForm();
                            }
                            else {
                                $.notify({
                                    // options
                                    message: result.message ?? 'Cấp sổ thất bại!!!'
                                }, {
                                    // settings
                                    delay: 1000,
                                    timer: 3000, type: 'danger'
                                });
                            }
                        }
                });
            }
        });
        $(".btn-update").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                type: "GET",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                url: Common.HS_CapSo_CapSo.Url.ThemMoiCapSo + "?CapSoID=" + id + "&LoaiNghiepVuID=" + $('#LoaiNghiepVuID').val(),
                dataType: 'HTML'
            }, function (result) {
                if (!Common.Empty(result)) {
                    $('#form-update').html(result);
                    Common.HS_CapSo_CapSo.RegisterEvent();
                    }
                else {
                    $.notify({
                        // options
                        message: result.message ?? 'Cấp sổ thất bại!!!'
                    }, {
                        // settings
                        delay: 1000,
                        timer: 3000, type: 'danger'
                    });
                }
            });
        });
        $(".btn-delete").unbind("click").click(function () {
            var id = $(this).data("id");
            Common.ShowLoading(true);
            Common.Ajax({
                url: Common.HS_CapSo_CapSo.Url.XoaCapSo,
                type: "POST",
                ContentType: 'application/json; charset=utf-8',
                async: false,
                cache: false,
                dataType: 'JSON',
                data: { ID: id }
            }, function (result) {
                Common.ShowLoading(false);
                    if (!Common.Empty(result)) {
                        if (result.status == true) {
                            $.notify({
                                // options
                                message: 'Xóa thành công.!!!'
                            }, {
                                // settings
                                delay: 1000,
                                timer: 3000, type: 'success'
                            });
                            Common.HS_CapSo_CapSo.SubmitForm();
                        }
                        else {
                            $.notify({
                                // options
                                message: 'Xóa thất bại!!!'
                            }, {
                                // settings
                                delay: 1000,
                                timer: 3000, type: 'danger'
                            });
                        }
                    }
            }
            );
        });
    },
    RefeshSelect2: function (selects) {
        $(selects).each(function (e, index) {
            const self = $(this);
            const select2Instance = self.select2().data("select2");
            const resetOptions = select2Instance.options.options;
            self.select2("destroy")
                .select2(resetOptions);
        });
    },
    AjaxGetPrefix: function (id) {
        Common.Ajax({
            url: Common.HS_CapSo_CapSo.Url.GetPrefix,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { ID: id }
        }, function (result) {
            if (!Common.Empty(result)) {
                $("#form-update").find("#So").val("");
                $("#form-update").find("#Prefix").val(result.Prefix);
                $("#form-update").find("#TuTang").prop("checked", result.TuTang);
                $("#form-update input[name=TuTang]").val(result.TuTang);
                $("#form-update input[name=Prefix]").prop("disabled", result.TuTang);
                $("#form-update input[name=So]").prop("disabled", result.TuTang)
            }
            else {
                $.notify({
                    // options
                    message: 'Lấy thông tin loại số thất bại!!!'
                }, {
                    // settings
                    delay: 1000,
                    timer: 3000, type: 'danger'
                });
            }
        }
        );
    },
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.HS_CapSo_CapSo.SetPage(page);
        Common.HS_CapSo_CapSo.SubmitForm();
        return false;
    },
    SetPageSize: function (page) {
        $("#IdformSearch").find("input[name='PageSize']").val(page);
    },
   
    ChangePageSize: function (e) {
        Common.HS_CapSo_CapSo.SetPageSize($(e).val());
        Common.HS_CapSo_CapSo.SubmitForm();
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
        var _model = {};
        _model.HoSoID = $('#HoSoID').val();
        _model.LoaiNghiepVuID = $('#LoaiNghiepVuID').val();
        Common.ShowLoading(true);
        Common.Ajax({
            type: "Post",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            url: Common.HS_CapSo_CapSo.Url.ListCapSo,
            dataType: 'HTML',
            data: _model
        }, function (result) {
            if (!Common.Empty(result)) {
                $('#result-searchlist').html(result);
                Common.HS_CapSo_CapSo.RegisterEvent();
            }
        });
    },
    Success: function (data) {
        Common.ShowLoading(false);
        $("body,html").animate({
            scrollTop: 180
        }, 1000);
        Common.HS_CapSo_CapSo.RegisterEvent();
    },
    SerializeObject: function (obj) {

        var self = obj,
            json = {},
            push_counters = {},
            patterns = {
                "validate": /^[a-zA-Z][a-zA-Z0-9_]*(?:\[(?:\d*|[a-zA-Z0-9_]+)\])*$/,
                "key": /[a-zA-Z0-9_]+|(?=\[\])/g,
                "push": /^$/,
                "fixed": /^\d+$/,
                "named": /^[a-zA-Z0-9_]+$/
            };


        obj.build = function (base, key, value) {
            base[key] = value;
            return base;
        };

        obj.push_counter = function (key) {
            if (push_counters[key] === undefined) {
                push_counters[key] = 0;
            }
            return push_counters[key]++;
        };

        $.each($(obj).serializeArray(), function () {

            // Skip invalid keys
            if (!patterns.validate.test(this.name)) {
                return;
            }

            var k,
                keys = this.name.match(patterns.key),
                merge = this.value,
                reverse_key = this.name;

            while ((k = keys.pop()) !== undefined) {

                // Adjust reverse_key
                reverse_key = reverse_key.replace(new RegExp("\\[" + k + "\\]$"), '');

                // Push
                if (k.match(patterns.push)) {
                    merge = self.build([], self.push_counter(reverse_key), merge);
                }

                // Fixed
                else if (k.match(patterns.fixed)) {
                    merge = self.build([], k, merge);
                }

                // Named
                else if (k.match(patterns.named)) {
                    merge = self.build({}, k, merge);
                }
            }

            json = $.extend(true, json, merge);
        });

        return json;
    }
};
// create HoSo Object
var HS_CapSo_CapSo = HS_CapSo_CapSo;
HS_CapSo_CapSo.constructor = HS_CapSo_CapSo;
