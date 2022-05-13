var QLQ_QuyenLienQuan_SearchSoThuTu = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
QLQ_QuyenLienQuan_SearchSoThuTu.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        var that = this;
        Common.Init();

        $("#btnSearchSoThuTu").unbind("click").click(function () {
            Common.QLQ_QuyenLienQuan_SearchSoThuTu.SubmitForm();
            return false;
        });

        $('.trSoThuTu').unbind("click").click(function () {
            var id = $(this).data("id");
            window.location.href = "/QLQ_QuyenLienQuan/" + $("#form-update-tt-tacpham").find("input[name='KeyPage']").val()
                + "?id=" + id
                + "&hoSoId=" + $("#form-update-tt-tacpham").find("input[name='HoSoID']").val()
                + "&isMap=" + null;
            Common.QLQ_QuyenLienQuan_SearchSoThuTu.HideDialog();
            return false;
        });
    },
    SubmitForm: function () {
        var form = $("#idFormSearchSoThuTu");
        var pageIndex = $("#dialogSearchSoThuTu").find("input[name='PageIndex']").val() > 0 ? $("#dialogSearchSoThuTu").find("input[name='PageIndex']").val() : 1;
        var pageSize = $("#dialogSearchSoThuTu").find("input[name='PageSize']").val() > 0 ? $("#dialogSearchSoThuTu").find("input[name='PageSize']").val() : 10;
        var model = {
            pageIndex: pageIndex,
            pageSize: pageSize,
            TuKhoa: form.find("input[name='TuKhoa']").val() || null
        };
        var formdata = new FormData();
        formdata.append('modelSearch', JSON.stringify(model));
        Common.Ajax({
            type: 'POST',
            url: Common.QLQ_QuyenLienQuan_SearchSoThuTu.Url.SearchSoThuTu,
            data: formdata,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            dataType: 'html',
        }, function (result) {
            $("#result-searchsothutu").empty();
            $("#result-searchsothutu").html(result);
            Common.QLQ_QuyenLienQuan_SearchSoThuTu.ShowDialog();
        });
        return false;
    },
    Paging: function (page) {
        Common.QLQ_QuyenLienQuan_SearchSoThuTu.SetPage(page);
        Common.QLQ_QuyenLienQuan_SearchSoThuTu.SubmitForm();
        return false;
    },
    SetPage: function (page) {
        $("#dialogSearchSoThuTu").find("input[name='PageIndex']").val(page);
    },
    SetPageSize: function (page) {
        $("#idFormSearchSoThuTu").find("input[name='PageSize']").val(page);
    },
    ChangePageSize: function (e) {
        Common.QLQ_QuyenLienQuan_SearchSoThuTu.SetPageSize($(e).val());
        Common.QLQ_QuyenLienQuan_SearchSoThuTu.SubmitForm();
        return false;
    },
    ShowDialog: function () {
        $("#dialogSearchSoThuTu").modal("show")
        Common.QLQ_QuyenLienQuan_SearchSoThuTu.RegisterEvent();
    },
    HideDialog: function () {
        $("#dialogSearchSoThuTu").modal("hide");
    },
};

var QLQ_QuyenLienQuan_SearchSoThuTu = QLQ_QuyenLienQuan_SearchSoThuTu;
QLQ_QuyenLienQuan_SearchSoThuTu.constructor = QLQ_QuyenLienQuan_SearchSoThuTu;
