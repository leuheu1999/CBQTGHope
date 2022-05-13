var HoSo = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
HoSo.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();

    },
    NguoiNhan: [],
    NguoiNhanTreeCombo: $('.NguoiNhanIDs').comboTree({
        source: [],
        cascadeSelect: true,
        isMultiple: true,
    }),
    RegisterEvent: function () {
        this.IsPaging = false;
        const that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea").val("");
            Common.HoSo.SubmitForm();
        });

        $("#btnTimKiem").unbind("click").click(function () {
            Common.HoSo.SetPage(1);
            Common.HoSo.SubmitForm();
            return false;
        });
        $("#btnNhan").unbind("click").click(function () {
            Common.HoSo.NhanHoSo();
            return false;
        });
        $("#btnChuyen").unbind("click").click(function () {
            Common.HoSo.ChuyenHoSo();
            return false;
        });
        $("#TinhThanhID").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#TinhThanhID option:selected").val();
            Common.ShowLoading(true);
            Common.HoSo.AjaxCBOQuanHuyen(val);
        });
        $("#QuanHuyenID").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#QuanHuyenID option:selected").val();
            Common.ShowLoading(true);
            Common.HoSo.AjaxCBOPhuongXa(val);
        });
        $("#LinhVucID").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#LinhVucID option:selected").val();
            Common.ShowLoading(true);
            Common.HoSo.AjaxCBOThuTuc(val);
        });
        $("#ThuTucID").unbind("select2:select").on("select2:select", (e) => {
            const val = $("#LinhVucID option:selected").val();
            const val2 = $("#ThuTucID option:selected").val();
            Common.ShowLoading(true);
            Common.HoSo.AjaxCBOTenBuocKeTiep(val, val2);
        });
        $("#NhanAll").unbind("change").on("change", (e) => {
            const checked = $(e.target).is(':checked');
            $("input[name=Nhan]:not(:disabled)").prop("checked", checked)
            return false;
        });
        $("input[name=Nhan]:not(:disabled)").unbind("change").on("change", (e) => {
            $("#NhanAll").prop("checked", !($("input[name=Nhan]:not(:disabled):not(:checked)").length > 0));
            return false;
        });
        $("#ChuyenAll").unbind("change").on("change", (e) => {
            const checked = $(e.target).is(':checked');
            $("input[name=Chuyen]:not(:disabled)").prop("checked", checked)
            return false;
        });
        $("input[name=Chuyen]:not(:disabled)").unbind("change").on("change", (e) => {
            $("#ChuyenAll").prop("checked", !($("input[name=Chuyen]:not(:disabled):not(:checked)").length > 0));
            return false;
        });
        $(".sobiennhan").unbind("click").click(function (e) {
            const item = $(e.target);
            const tr = $(item.closest('tr'));
            var hoSoId = tr.data('id');            
            if (tr.data('thutucid') == 5 || tr.data('thutucid') == 7) {
                Common.Ajax({
                    url: Common.HoSo.Url.GetLoaiNghiepVu,
                    type: "POST",
                    ContentType: 'application/json; charset=utf-8',
                    async: false,
                    cache: false,
                    dataType: 'JSON',
                    data: { id: hoSoId }
                }, function (result) {
                    Common.ShowLoading(false);
                    if (result != null) {  
                        var id = 'dropdown-menu-' + hoSoId;
                        document.getElementById(id).innerHTML = Common.HoSo.TemplateHtmlThuTuc(Common.HoSo.Url.HoSoChuyenNganh, hoSoId, tr.data('thutucid'), result);
                    }
                    return false;
                });
            }
            else {
                window.location.href = Common.HoSo.Url.HoSoChuyenNganh
                    + "?HoSoID=" + hoSoId
                    + "&ThuTucHanhChinhID=" + tr.data('thutucid');
                return false;
            }
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

        if (!Common.Empty(Common.HoSo) && !Common.Empty(Common.HoSo.NguoiNhan)) {
            const nodes = Common.HoSo.NguoiNhan.map(x => {
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
            Common.HoSo.NguoiNhanTreeCombo.setSource(tree);
            Common.HoSo.NguoiNhanTreeCombo.clearSelection();
        }

    },
    AjaxCBOQuanHuyen: function (id) {
        Common.Ajax({
            url: Common.HoSo.Url.ListDMQuanHuyen,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { ID: id }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                const html = Common.HoSo.TemplateHtmlCBO("Chọn quận huyện", result);
                $("#QuanHuyenID").html(html);
                $("#PhuongXaID").html(Common.HoSo.TemplateHtmlCBO("Chọn phường xã", []));
                Common.HoSo.RefeshSelect2("#QuanHuyenID, #PhuongXaID");
            }
        }
        );
    },
    AjaxCBOPhuongXa: function (id) {
        Common.Ajax({
            url: Common.HoSo.Url.ListDMPhuongXa,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { ID: id }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                const html = Common.HoSo.TemplateHtmlCBO("Chọn phường xã", result);
                $("#PhuongXaID").html(html);
                Common.HoSo.RefeshSelect2("#PhuongXaID");
            }
        }
        );
    },
    AjaxCBOThuTuc: function (id) {
        Common.Ajax({
            url: Common.HoSo.Url.ListDMThuTuc,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { ID: id }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                const html = Common.HoSo.TemplateHtmlCBO("Chọn thủ tục", result);
                $("#ThuTucID").html(html);
                $("#TenBuocKeTiep").html(Common.HoSo.TemplateHtmlCBO("Chọn tên bước kế tiếp", []));
                Common.HoSo.RefeshSelect2("#ThuTucID, #TenBuocKeTiep");
            }
        }
        );
    },
    AjaxCBOTenBuocKeTiep: function (linhvucid, thutucid) {
        Common.Ajax({
            url: Common.HoSo.Url.ListDMTenBuocKeTiep,
            type: "POST",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            dataType: 'JSON',
            data: { LinhVucID: linhvucid, ThuTucHanhChinhID: thutucid, ChucNangHienTai: "THAMDINHHOSO", LoaiXuLy: "1" }
        }, function (result) {
            Common.ShowLoading(false);
            if (!Common.Empty(result)) {
                const html = Common.HoSo.TemplateHtmlCBO("Chọn tên bước kế tiếp", result);
                $("#TenBuocKeTiep").html(html);
                Common.HoSo.RefeshSelect2("#TenBuocKeTiep");
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
        Common.HoSo.SetPage(page);
        Common.HoSo.SubmitForm();
        return false;
    },
    SetPageSize: function (page) {
        $("#IdformSearch").find("input[name='PageSize']").val(page);
    },

    ChangePageSize: function (e) {
        Common.HoSo.SetPageSize($(e).val());
        Common.HoSo.SubmitForm();
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
        let DuongDiNguoiNhan = "";
        const arrSelected = Common.HoSo.NguoiNhanTreeCombo.getSelectedIds();
        if (arrSelected && arrSelected.length) {
            arrSelected.forEach((item, i) => {
                const obj = Common.HoSo.NguoiNhan.find(x => x.STT == item);
                if (obj && obj.Ma) {
                    DuongDiNguoiNhan += (DuongDiNguoiNhan ? ',' : '') + obj.Ma;
                }
            });
        }
        $("#IdformSearch").find("input[name='DuongDiNguoiNhan']").val(DuongDiNguoiNhan);
        $("#IdformSearch").submit();
    },
    NhanHoSo: function () {
        const arr = $("input[name=Nhan]:not(:disabled):checked");
        if (!arr.length) {
            $.notify({
                // options
                message: 'Vui lòng chọn hồ sồ cần nhận.!!!'
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
                "HoSoID": tr.data('id'), //HoSoID
                "LinhVucID": tr.data('linhvucid'), //LinhVucID
                "ThuTucHanhChinhID": tr.data('thutucid'), //ThuTucHanhChinhID
                "ChucNangHienTai": tr.data('chucnanghientai'), //ChucNangHienTai
                "QuaTrinhXuLyID": tr.data('quatrinhxulyid'), // QuaTrinhXuLyHienTaiID
                "SoBienNhan": tr.data('sobiennhan')
            });
        }
        Common.ShowLoading(true);
        Common.Ajax({
            type: "Post",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            url: Common.HoSo.Url.NhanHoSo,
            dataType: 'JSON',
            data: { model: ds }
        }, function (result) {
            if (!Common.Empty(result)) {
                if (result.status == true) {
                    $.notify({
                        // options
                        message: 'Nhận hồ sơ thành công.!!!'
                    }, {
                            // settings
                            delay: 1000,
                            timer: 1000, type: 'success'
                        });
                    Common.HoSo.SetPage(1);
                    Common.HoSo.SubmitForm();
                }
                else {
                    $.notify({
                        // options
                        message: result.message
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
    ChuyenHoSo: function () {
        const checks = $("input[name=Chuyen]:not(:disabled):checked");
        if (!checks.length) {
            $.notify({
                // options
                message: 'Vui lòng chọn hồ sồ cần chuyển.!!!'
            }, {
                    // settings
                    delay: 1000,
                    timer: 1000, type: 'danger'
                });
            return;
        }
        let DuongDiNguoiNhan = "";
        const arrSelected = Common.HoSo.NguoiNhanTreeCombo.getSelectedIds();
        if (arrSelected && arrSelected.length) {
            arrSelected.forEach((item, i) => {
                const obj = Common.HoSo.NguoiNhan.find(x => x.STT == item);
                if (obj && obj.Ma) {
                    DuongDiNguoiNhan += (DuongDiNguoiNhan ? ',' : '') + obj.Ma;
                }
            });
        }

        const ds = [];
        checks.each((i, item) => {
            const tr = $($(item).closest('tr'));
            ds.push({
                "NguoiXuLyID": tr.data('nguoixulyid'), //giá trị bên ds
                "PhongBanXuLyID": tr.data('phongbanxulyid'), //giá trị bên ds
                "DonViXuLyID": tr.data('donvixulyid'), //giá trị bên ds
                "HoSoID": tr.data('id'), //giá trị bên ds
                "HoSoOnlineID": tr.data('hosoonlineid'), //giá trị bên ds
                "ThuTucHanhChinhID": tr.data('thutucid'), //giá trị bên ds
                "LinhVucID": tr.data('linhvucid'), //giá trị bên ds
                "QuaTrinhXuLyHienTaiID": tr.data('quatrinhxulyid'), //giá trị bên ds
                "DuongDiHoSo": tr.data('duongdihoso'), //giá trị bên ds
                "DuongDiNguoiNhan": DuongDiNguoiNhan, //Lấy từ combo chọn người nhận
                "SoBienNhan": tr.data('sobiennhan'), //giá trị bên ds
                "LoaiXuLy": tr.data('loaixuly'), //giá trị bên ds
                "DonViNhanID": tr.data('donvinhanid'), //giá trị bên ds
                "XuLyLai": tr.data('xulylai') //giá trị bên ds
            });
        });
        Common.ShowLoading(true);
        Common.Ajax({
            type: "Post",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            url: Common.HoSo.Url.ChuyenHoSo,
            dataType: 'JSON',
            data: { model: ds }
        }, function (result) {
            if (!Common.Empty(result)) {
                if (result.status == true) {
                    $.notify({
                        // options
                        message: 'Chuyển hồ sơ thành công.!!!'
                    }, {
                            // settings
                            delay: 1000,
                            timer: 3000, type: 'success'
                        });
                    Common.HoSo.SetPage(1);
                    Common.HoSo.SubmitForm();
                }
                else {
                    $.notify({
                        // options
                        message: result.message
                    }, {
                            // settings
                            delay: 1000,
                            timer: 3000, type: 'danger'
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
        Common.HoSo.RegisterEvent();
    },
    TemplateHtmlThuTuc(url, hoSoId, thuTucId, loaiNghiepVuId) {
        let html = '';
        if (thuTucId == 5) {
            if (loaiNghiepVuId == 5) {
                html += '<li><a href="' + url + '?HoSoID=' + hoSoId + '&ThuTucHanhChinhID=5" class="line">Cấp đổi</a></li>';
                html += '<li class="disabled"><a href="javascript:void(0);" class="line">Chuyển chủ</a></li>';
            } else if (loaiNghiepVuId == 4) {
                html += '<li class="disabled"><a href="javascript:void(0);" class="line">Cấp đổi</a></li>';
                html += '<li><a href="' + url + '?HoSoID=' + hoSoId + '&ThuTucHanhChinhID=97" class="line">Chuyển chủ</a></li>';
            }
            else {
                html += '<li><a href="' + url + '?HoSoID=' + hoSoId + '&ThuTucHanhChinhID=5" class="line">Cấp đổi</a></li>';
                html += '<li><a href="' + url + '?HoSoID=' + hoSoId + '&ThuTucHanhChinhID=97" class="line">Chuyển chủ</a></li>';
            }
        }
        else {
            if (loaiNghiepVuId == 10) {
                html += '<li><a href="' + url + '?HoSoID=' + hoSoId + '&ThuTucHanhChinhID=7" class="line">Cấp đổi</a></li>';
                html += '<li class="disabled"><a href="javascript:void(0);" class="line">Chuyển chủ</a></li>';
            } else if (loaiNghiepVuId == 9) {
                html += '<li class="disabled"><a href="javascript:void(0);" class="line">Cấp đổi</a></li>';
                html += '<li><a href="' + url + '?HoSoID=' + hoSoId + '&ThuTucHanhChinhID=98" class="line">Chuyển chủ</a></li>';
            }
            else {
                html += '<li><a href="' + url + '?HoSoID=' + hoSoId + '&ThuTucHanhChinhID=7" class="line">Cấp đổi</a></li>';
                html += '<li><a href="' + url + '?HoSoID=' + hoSoId + '&ThuTucHanhChinhID=98" class="line">Chuyển chủ</a></li>';
            }
        }
        return html;
    },
};
// create HoSo Object
var HoSo = HoSo;
HoSo.constructor = HoSo;
