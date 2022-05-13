var HS_CapSo = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
HS_CapSo.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
     
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        const that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea").val("");
            Common.HS_CapSo.SubmitForm();
        });

        $("#btnTimKiem").unbind("click").click(function () {
            Common.HS_CapSo.SetPage(1);
            Common.HS_CapSo.SubmitForm();
            return false;
        });
        $("#btnDuyet").unbind("click").click(function () {
            Common.HS_CapSo.DuyetHoSo();
            return false;
        });
        $("#btnKhongDuyet").unbind("click").click(function () {
            Common.HS_CapSo.KhongDuyetHoSo();
            return false;
        });
        $("#TinhThanhID").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#TinhThanhID option:selected").val();
            Common.ShowLoading(true);
            Common.HS_CapSo.AjaxCBOQuanHuyen(val);
        });
        $("#QuanHuyenID").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#QuanHuyenID option:selected").val();
            Common.ShowLoading(true);
            Common.HS_CapSo.AjaxCBOPhuongXa(val);
        });
        $("#LinhVucID").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#LinhVucID option:selected").val();
            Common.ShowLoading(true);
            Common.HS_CapSo.AjaxCBOThuTuc(val);
        });
        $("#ThuTucID").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#LinhVucID option:selected").val();
            const val2 = $("#ThuTucID option:selected").val();
            //Common.ShowLoading(true);
            //Common.HS_CapSo.AjaxCBOTenBuocKeTiep(val,val2);
        });
        $("#NhanAll").unbind("change").on("change",(e) => {
            const checked = $(e.target).is(':checked');
            $("input[name=Nhan]:not(:disabled)").prop("checked", checked)
            return false;
        });
        $("input[name=Nhan]:not(:disabled)").unbind("change").on("change", (e) => {
            $("#NhanAll").prop("checked", !($("input[name=Nhan]:not(:disabled):not(:checked)").length > 0));
            return false;
        });
        $(".sobiennhan").unbind("click").click(function (e) {
            const item = $(e.target);
            const tr = $(item.closest('tr'));
            window.location.href = Common.HS_CapSo.Url.HoSoChuyenNganh
                + "?HoSoID=" + tr.data('hosoid')
                + "&ThuTucHanhChinhID=" + tr.data('thutucid')
                + "&Key=capso";
            return false;
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
    RefeshNguoiNhan: function (selects) {

        if (!Common.Empty(Common.HoSo) && !Common.Empty(Common.HS_CapSo.NguoiNhan)) {
            const nodes = Common.HS_CapSo.NguoiNhan.map(x => {
                const o = {
                    id: x.STT,
                    title: x.Ten,
                    pid: x.ParentID
                };
                return o;
            });
            const helper = nodes.reduce((h, o) => (h[o.id] = Object.assign({}, o), h), Object.create(null));
            const tree = nodes.reduce((t, node) => {
                const current = helper[node.id];
                if (Common.Empty(current.pid) || current.pid === 0) {
                    t.push(current);
                } else {
                    helper[node.pid].subs || (helper[node.pid].subs = [])
                    helper[node.pid].subs.push(current);
                }
                return t;
            }, []);
            Common.HS_CapSo.NguoiNhanTreeCombo.setSource(tree);
            Common.HS_CapSo.NguoiNhanTreeCombo.clearSelection();
        }

    },
    AjaxCBOQuanHuyen: function (id) {
        Common.Ajax({
            url: Common.HS_CapSo.Url.ListDMQuanHuyen,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { ID: id }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                const html = Common.HS_CapSo.TemplateHtmlCBO("Chọn quận huyện", result);
                $("#QuanHuyenID").html(html);
                $("#PhuongXaID").html(Common.HS_CapSo.TemplateHtmlCBO("Chọn phường xã", []));
                Common.HS_CapSo.RefeshSelect2("#QuanHuyenID, #PhuongXaID");
            }
        }
        );
    },
  
    AjaxCBOPhuongXa: function (id) {
        Common.Ajax({
            url: Common.HS_CapSo.Url.ListDMPhuongXa,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { ID: id }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                const html = Common.HS_CapSo.TemplateHtmlCBO("Chọn phường xã", result);
                $("#PhuongXaID").html(html);
                Common.HS_CapSo.RefeshSelect2("#PhuongXaID");
            }
        }
        );
    },
    AjaxCBOThuTuc: function (id) {
        Common.Ajax({
            url: Common.HS_CapSo.Url.ListDMThuTuc,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { ID: id }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                const html = Common.HS_CapSo.TemplateHtmlCBO("Chọn thủ tục", result);
                $("#ThuTucID").html(html);
                $("#TenBuocKeTiep").html(Common.HS_CapSo.TemplateHtmlCBO("Chọn tên bước kế tiếp", []));
                Common.HS_CapSo.RefeshSelect2("#ThuTucID, #TenBuocKeTiep");
            }
        }
        );
    },
    TemplateHtmlCBO(nameHienThi, dataObj) {
        let html = "";
        html += "<option value=''>--" + nameHienThi + "--</option>";
        for (let i = 0; i < dataObj.length; i++) {
            html += "<option value='" + dataObj[i].Value + "'>" + dataObj[i].Text + "</option>";
        }
        return html;
    },
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.HS_CapSo.SetPage(page);
        Common.HS_CapSo.SubmitForm();
        return false;
    },
    SetPageSize: function (page) {
        $("#IdformSearch").find("input[name='PageSize']").val(page);
    },
   
    ChangePageSize: function (e) {
        Common.HS_CapSo.SetPageSize($(e).val());
        Common.HS_CapSo.SubmitForm();
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
    DuyetHoSo: function () {
        const arr = $("input[name=Nhan]:not(:disabled):checked");
        if (!arr.length) {
            $.notify({
                // options
                message: 'Vui lòng chọn hồ sồ cần duyệt.!!!'
            }, {
                // settings
                delay: 1000,
                timer: 1000, type: 'danger'
            });
            return;
        }
        let ds = [];
        for (let i = 0; i < arr.length; i++) {
            const tr = $($(arr[i]).closest('tr'));
            ds.push({
                "CapQuyenID": tr.data('id'), //HoSoID
                "SoBienNhan": tr.data('sobiennhan')
            });
        }
        Common.ShowLoading(true);
        Common.Ajax({
            type: "Post",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            url: Common.HS_CapSo.Url.DuyetHoSo,
            dataType: 'JSON',
            data: { model:ds}
        }, function (result) {
            if (!Common.Empty(result)) {
                if (result.status == true) {
                    $.notify({
                        // options
                        message: 'Duyệt hồ sơ thành công.!!!'
                    }, {
                        // settings
                        delay: 1000,
                        timer: 1000, type: 'success'
                    });
                    Common.HS_CapSo.SetPage(1);
                    Common.HS_CapSo.SubmitForm();
                }
                else {
                    $.notify({
                        // options
                        message: result.message ?? "Duyệt hồ sơ thất bại"
                    }, {
                        // settings
                        delay: 1000,
                        timer: 1000, type: 'danger'
                    });
                }
             
            }
        });
        return false;
    },
    KhongDuyetHoSo: function () {
        const arr = $("input[name=Nhan]:not(:disabled):checked");
        if (!arr.length) {
            $.notify({
                // options
                message: 'Vui lòng chọn hồ sồ cần duyệt.!!!'
            }, {
                // settings
                delay: 1000,
                timer: 1000, type: 'danger'
            });
            return;
        }
        let ds = [];
        for (let i = 0; i < arr.length; i++) {
            const tr = $($(arr[i]).closest('tr'));
            ds.push({
                "CapQuyenID": tr.data('id'), //HoSoID
                "SoBienNhan": tr.data('sobiennhan')
            });
        }
        Common.ShowLoading(true);
        Common.Ajax({
            type: "Post",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            url: Common.HS_CapSo.Url.KhongDuyetHoSo,
            dataType: 'JSON',
            data: { model: ds }
        }, function (result) {
            if (!Common.Empty(result)) {
                if (result.status == true) {
                    $.notify({
                        // options
                        message: 'Không duyệt hồ sơ thành công.!!!'
                    }, {
                        // settings
                        delay: 1000,
                        timer: 1000, type: 'success'
                    });
                    Common.HS_CapSo.SetPage(1);
                    Common.HS_CapSo.SubmitForm();
                }
                else {
                    $.notify({
                        // options
                        message: result.message ?? "Không duyệt hồ sơ thất bại"
                    }, {
                        // settings
                        delay: 1000,
                        timer: 1000, type: 'danger'
                    });
                }

            }
        });
        return false;
    },
    Success: function (data) {
        Common.ShowLoading(false);
        $("body,html").animate({
            scrollTop: 180
        }, 1000);
        Common.HS_CapSo.RegisterEvent();
    }
};
// create HoSo Object
var HS_CapSo = HS_CapSo;
HS_CapSo.constructor = HS_CapSo;
