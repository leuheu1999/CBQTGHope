var BC_BaoCaoTacPham = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
BC_BaoCaoTacPham.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
     
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        const that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea").val("");
            Common.BC_BaoCaoTacPham.SubmitForm();
        });

        $("#btnTimKiem").unbind("click").click(function () {
            Common.BC_BaoCaoTacPham.SetPage(1);
            Common.BC_BaoCaoTacPham.SubmitForm();
            return false;
        });

        $("#QuyenID").unbind("change").change(function () {
            var val = $("#QuyenID option:selected").val();
            Common.ShowLoading(true);
            Common.BC_BaoCaoTacPham.AjaxCBOLoaiHinh(val);
            return false;
        });

        $("#KyBaoCao").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#KyBaoCao option:selected").val();
            //$("#IdformSearch").find("#Nam, #Quy, #Thang").val("1");
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
            Common.BC_BaoCaoTacPham.RefeshSelect2("#Nam, #Quy, #Thang");
        });
        $("#btn-word").unbind("click").click(function () {
            var form = $("#IdformSearch");
            var _model = '"KyBaoCao":"' + form.find("select[name='KyBaoCao']").val() + '",';
            _model += '"Nam":"' + form.find("select[name='Nam']").val() + '",';
            _model += '"Quy":"' + form.find("select[name='Quy']").val() + '",';
            _model += '"Thang":"' + (form.find("select[name='Thang']").val()) + '",';
            _model += '"TuNgay":"' + (form.find("input[name='TuNgay']").val()) + '",';
            _model += '"DenNgay":"' + (form.find("input[name='DenNgay']").val()) + '",';
            _model += '"LoaiHinhID":"' + (form.find("select[name='LoaiHinhID']").val() || "0") + '",';
            _model += '"VungMienID":"' + (form.find("select[name='VungMienID']").val() || "0") + '",';
            _model += '"QuyenID":"' + (form.find("select[name='QuyenID']").val() || "0") + '",';
            _model += '"LoaiThuTucID":"' + (form.find("select[name='LoaiThuTucID']").val() || "0") + '",';
            _model += '"TacGia":"' + form.find("input[name='TacGia']").val() + '",';
            _model += '"ChuSoHuu":"' + form.find("input[name='ChuSoHuu']").val() + '",';
            _model += '"NguoiDaiDien":"' + form.find("input[name='NguoiDaiDien']").val() + '",';
            _model += '"IsToChuc":"' + (form.find("input[name='IsToChuc']").is(':checked') || "0") + '"';
            Common.Ajax({
                type: "POST",
                url: Common.BC_BaoCaoTacPham.Url.ExportWordPdf,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportBC_BaoCaoTacPhamList_Word' }
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
            _model += '"DenNgay":"' + (form.find("input[name='DenNgay']").val()) + '",';
            _model += '"LoaiHinhID":"' + (form.find("select[name='LoaiHinhID']").val() || "0") + '",';
            _model += '"VungMienID":"' + (form.find("select[name='VungMienID']").val() || "0") + '",';
            _model += '"QuyenID":"' + (form.find("select[name='QuyenID']").val() || "0") + '",';
            _model += '"LoaiThuTucID":"' + (form.find("select[name='LoaiThuTucID']").val() || "0") + '",';
            _model += '"TacGia":"' + form.find("input[name='TacGia']").val() + '",';
            _model += '"ChuSoHuu":"' + form.find("input[name='ChuSoHuu']").val() + '",';
            _model += '"NguoiDaiDien":"' + form.find("input[name='NguoiDaiDien']").val() + '",';
            _model += '"IsToChuc":"' + (form.find("input[name='IsToChuc']").is(':checked') || "0") + '"';
            Common.Ajax({
                type: "POST",
                url: Common.BC_BaoCaoTacPham.Url.ExportExcel,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportBC_BaoCaoTacPhamList_Excel' }
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
            _model += '"DenNgay":"' + (form.find("input[name='DenNgay']").val()) + '",';
            _model += '"LoaiHinhID":"' + (form.find("select[name='LoaiHinhID']").val() || "0") + '",';
            _model += '"VungMienID":"' + (form.find("select[name='VungMienID']").val() || "0") + '",';
            _model += '"QuyenID":"' + (form.find("select[name='QuyenID']").val() || "0") + '",';
            _model += '"LoaiThuTucID":"' + (form.find("select[name='LoaiThuTucID']").val() || "0") + '",';
            _model += '"TacGia":"' + form.find("input[name='TacGia']").val() + '",';
            _model += '"ChuSoHuu":"' + form.find("input[name='ChuSoHuu']").val() + '",';
            _model += '"NguoiDaiDien":"' + form.find("input[name='NguoiDaiDien']").val() + '",';
            _model += '"IsToChuc":"' + (form.find("input[name='IsToChuc']").is(':checked') || "0") + '"';
            Common.Ajax({
                type: "POST",
                url: Common.BC_BaoCaoTacPham.Url.ExportWordPdf,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportBC_BaoCaoTacPhamList_Pdf' }
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
        Common.BC_BaoCaoTacPham.SetPage(page);
        Common.BC_BaoCaoTacPham.SubmitForm();
        return false;
    },
    SetPageSize: function (page) {
        $("#IdformSearch").find("input[name='PageSize']").val(page);
    },
   
    ChangePageSize: function (e) {
        Common.BC_BaoCaoTacPham.SetPageSize($(e).val());
        Common.BC_BaoCaoTacPham.SubmitForm();
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
        Common.BC_BaoCaoTacPham.RegisterEvent();
    },
    AjaxCBOLoaiHinh: function (id) {
        Common.Ajax({
            url: Common.BC_BaoCaoTacPham.Url.CBOLoaiHinhById,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { id: id }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                var html = Common.BC_BaoCaoTacPham.TemplateHtmlCBO("Loại hình", "LoaiHinhID", "LoaiHinhID", result.data);
                $('#IdformSearch select[id=LoaiHinhID]').empty().append(html);
            }
        }
        );
    },
    TemplateHtmlCBO(nameHienThi, idControl, nameControl, dataObj) {
        var html = "<option value='0'>--- " + nameHienThi + " ---</option>";
        for (var i = 0; i < dataObj.length; i++) {
            html += "<option value='" + dataObj[i].Value + "'>" + dataObj[i].Text + "</option>";
        }
        html += "</select>";
        return html;
    },
};
// create BC_BaoCaoTacPham Object
var BC_BaoCaoTacPham = BC_BaoCaoTacPham;
BC_BaoCaoTacPham.constructor = BC_BaoCaoTacPham;
