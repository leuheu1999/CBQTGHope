var BC_BaoCaoTongHopSoLieu = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
BC_BaoCaoTongHopSoLieu.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();

    },
    RegisterEvent: function () {
        this.IsPaging = false;
        const that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea").val("");
            Common.BC_BaoCaoTongHopSoLieu.SubmitForm();
        });

        $("#btn-word").unbind("click").click(function () {
            var form = $("#IdformSearch");
            var _model = '"Nam":"' + form.find("select[name='Nam']").val() + '"';
            Common.Ajax({
                type: "POST",
                url: Common.BC_BaoCaoTongHopSoLieu.Url.ExportWordPdf,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportBC_BaoCaoTongHopSoLieuList_Word' }
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
            var _model = '"Nam":"' + form.find("select[name='Nam']").val() + '"';
            Common.Ajax({
                type: "POST",
                url: Common.BC_BaoCaoTongHopSoLieu.Url.ExportWordPdf,
                async: false,
                cache: false,
                data: { Param: _model, Key: 'ExportBC_BaoCaoTongHopSoLieuList_Pdf' }
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
    },
};
