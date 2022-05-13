var BC_BaoCaoTongHopHoatDong = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
BC_BaoCaoTongHopHoatDong.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();

    },
    RegisterEvent: function () {
        this.IsPaging = false;
        const that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea").val("");
            Common.BC_BaoCaoTongHopHoatDong.SubmitForm();
        });

        $("#btnTimKiem").unbind("click").click(function () {
            Common.BC_BaoCaoTongHopHoatDong.SetPage(1);
            Common.BC_BaoCaoTongHopHoatDong.SubmitForm();
            return false;
        });

        $("#KyBaoCao").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#KyBaoCao option:selected").val();
            $("#IdformSearch").find("#TuNgay, #DenNgay").val("");
            $("#IdformSearch").find(".div-nam, .div-quy, .div-thang, .div-tungay, .div-denngay").each((i, ele) => {
                if (!$(ele).hasClass('hidden')) {
                    $(ele).addClass('hidden');
                }
            });
            switch (val) {
                case "1": //Năm
                    $('.div-nam').removeClass('hidden');
                    break;
                case "2": //Quy
                    $('.div-nam').removeClass('hidden');
                    $('.div-quy').removeClass('hidden');
                    break;
                case "3": //Thang
                    $('.div-nam').removeClass('hidden');
                    $('.div-thang').removeClass('hidden');
                    break;
                case "4": //Ngay
                    $('.div-tungay').removeClass('hidden');
                    $('.div-denngay').removeClass('hidden');
                    break;
            }
            Common.BC_BaoCaoTongHopHoatDong.RefeshSelect2("#Nam, #Quy, #Thang");
        });
        $("#btn-word").unbind("click").click(function () {
            var form = $("#IdformSearch");
            var _model = '"KyBaoCao":"' + form.find("select[name='KyBaoCao']").val() + '",';
            _model += '"Nam":"' + form.find("select[name='Nam']").val() + '",';
            _model += '"Quy":"' + form.find("select[name='Quy']").val() + '",';
            _model += '"Thang":"' + (form.find("select[name='Thang']").val()) + '",';
            _model += '"TuNgay":"' + (form.find("input[name='TuNgay']").val()) + '",';
            _model += '"DenNgay":"' + (form.find("input[name='DenNgay']").val()) + '"';
            Common.Ajax({
                type: "POST",
                url: Common.BC_BaoCaoTongHopHoatDong.Url.ExportWordPdf,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportBC_BaoCaoTongHopHoatDongList_Word' }
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
        $("#btn-excel").unbind("click").click(function () {
            var form = $("#IdformSearch");
            var _model = '"KyBaoCao":"' + form.find("select[name='KyBaoCao']").val() + '",';
            _model += '"Nam":"' + form.find("select[name='Nam']").val() + '",';
            _model += '"Quy":"' + form.find("select[name='Quy']").val() + '",';
            _model += '"Thang":"' + (form.find("select[name='Thang']").val()) + '",';
            _model += '"TuNgay":"' + (form.find("input[name='TuNgay']").val()) + '",';
            _model += '"DenNgay":"' + (form.find("input[name='DenNgay']").val()) + '"';
            Common.Ajax({
                type: "POST",
                url: Common.BC_BaoCaoTongHopHoatDong.Url.ExportExcel,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportBC_BaoCaoTongHopHoatDongList_Excel' }
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
        $("#btn-pdf").unbind("click").click(function () {
            var form = $("#IdformSearch");
            var _model = '"KyBaoCao":"' + form.find("select[name='KyBaoCao']").val() + '",';
            _model += '"Nam":"' + form.find("select[name='Nam']").val() + '",';
            _model += '"Quy":"' + form.find("select[name='Quy']").val() + '",';
            _model += '"Thang":"' + (form.find("select[name='Thang']").val()) + '",';
            _model += '"TuNgay":"' + (form.find("input[name='TuNgay']").val()) + '",';
            _model += '"DenNgay":"' + (form.find("input[name='DenNgay']").val()) + '"';
            Common.Ajax({
                type: "POST",
                url: Common.BC_BaoCaoTongHopHoatDong.Url.ExportWordPdf,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportBC_BaoCaoTongHopHoatDongList_Pdf' }
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
        $(".select2-share").select2();
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
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.BC_BaoCaoTongHopHoatDong.SetPage(page);
        Common.BC_BaoCaoTongHopHoatDong.SubmitForm();
        return false;
    },
    SetPageSize: function (page) {
        $("#IdformSearch").find("input[name='PageSize']").val(page);
    },

    ChangePageSize: function (e) {
        Common.BC_BaoCaoTongHopHoatDong.SetPageSize($(e).val());
        Common.BC_BaoCaoTongHopHoatDong.SubmitForm();
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
        Common.BC_BaoCaoTongHopHoatDong.RegisterEvent();
    },
};
// create BC_BaoCaoTongHopHoatDong Object
var BC_BaoCaoTongHopHoatDong = BC_BaoCaoTongHopHoatDong;
BC_BaoCaoTongHopHoatDong.constructor = BC_BaoCaoTongHopHoatDong;
