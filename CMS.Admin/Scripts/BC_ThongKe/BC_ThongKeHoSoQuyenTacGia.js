var BC_ThongKeHoSoQuyenTacGia = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
}
BC_ThongKeHoSoQuyenTacGia.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
     
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        const that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            Common.BC_ThongKeHoSoQuyenTacGia.SubmitForm();
        });

        $("#btnTimKiem").unbind("click").click(function () {
            Common.BC_ThongKeHoSoQuyenTacGia.SubmitForm();
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
    BeforeSend: function (xhr) {
    },
    Chart: null,
    SubmitForm: function () {
        Common.ShowLoading(true);   
        
        Common.Ajax({
            type: "Post",
            ContentType: 'application/json; charset=utf-8',
            async: false,
            cache: false,
            url: Common.BC_ThongKeHoSoQuyenTacGia.Url.ListBC_ThongKeHoSoQuyenTacGia,
            dataType: 'JSON',
            data: {
                model: { Nam: $('#IdformSearch').find('#Nam').val()} }
        }, function (result) {
            if (!Common.Empty(result)) {
                if (result) {
                    result = result.sort(function (a, b) { return a.STT - b.STT });
                    const labels = ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"];
                    const data = {
                        labels: labels,
                        datasets: [
                            {
                                label: 'Cấp mới',
                                data: result.reduce((c, n) => [...c, n.CapMoi ?? 0], []),
                                backgroundColor: 'rgb(255, 99, 132, 0.5)',
                                borderColor: 'rgb(255, 99, 132, 1)',
                                borderWidth: 1,
                            },
                            {
                                label: 'Cấp lại',
                                data: result.reduce((c, n) => [...c, n.CapLai ?? 0], []),
                                backgroundColor: 'rgb(255, 159, 64, 0.5)',
                                borderColor: 'rgb(255, 159, 64, 1)',
                                borderWidth: 1,
                            },
                            {
                                label: 'Cấp đổi',
                                data: result.reduce((c, n) => [...c, n.CapDoi ?? 0], []),
                                backgroundColor: 'rgb(255, 205, 86, 0.5)',
                                borderColor: 'rgb(255, 205, 86, 1)',
                                borderWidth: 1,
                            },
                            {
                                label: 'Chuyển chủ sở hữu',
                                data: result.reduce((c, n) => [...c, n.ChuyenChuSoHuu ?? 0], []),
                                backgroundColor: 'rgb(75, 192, 192, 0.5)',
                                borderColor: 'rgb(75, 192, 192, 1)',
                                borderWidth: 1,
                            },
                            {
                                label: 'Thu hồi',
                                data: result.reduce((c, n) => [...c, n.ThuHoi ?? 0], []),
                                backgroundColor: 'rgb(54, 162, 2352, 0.5)',
                                borderColor: 'rgb(54, 162, 235, 1)',
                                borderWidth: 1,
                            }
                        ]
                    };
                    if (Common.BC_ThongKeHoSoQuyenTacGia.Chart) {
                        Common.BC_ThongKeHoSoQuyenTacGia.Chart.data = data;
                        Common.BC_ThongKeHoSoQuyenTacGia.Chart.update();
                    }
                    else {
                        const config = {
                            type: 'bar',
                            data: data,
                            options: {
                                responsive: true,
                                plugins: {
                                    legend: {
                                        position: 'top',
                                    },
                                    title: {
                                        display: true,
                                        text: 'Dashboard thống kê hồ sơ quyền tác giả'
                                    }
                                }
                            },
                        };
                        const ctx = document.getElementById('result-searchlist').getContext('2d');
                        Common.BC_ThongKeHoSoQuyenTacGia.Chart = new Chart(ctx, config);
                    }
                }
                else {
                    $.notify({
                        // options
                        message: 'Lấy dữ liệu thống kê thất bại'
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
        Common.BC_ThongKeHoSoQuyenTacGia.RegisterEvent();
    }
};
// create BC_ThongKeHoSoQuyenTacGia Object
var BC_ThongKeHoSoQuyenTacGia = BC_ThongKeHoSoQuyenTacGia;
BC_ThongKeHoSoQuyenTacGia.constructor = BC_ThongKeHoSoQuyenTacGia;
