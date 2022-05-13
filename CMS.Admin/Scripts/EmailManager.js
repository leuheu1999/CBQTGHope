var EmailManager = function (options) {
    for (var property in options) {
        this[property] = options[property];
    }
    return this.Initialize(options);
};
var arrayquyen = [];
EmailManager.prototype = {
    Initialize: function (options) {
        this.RegisterEvent();
    },
    RegisterEvent: function () {
        this.IsPaging = false;
        var that = this;
        $("a[data-action='reload']").unbind("click").click(function () {
            $("#IdformSearch").find("input[type=text], textarea").val("");
            Common.EmailManager.SubmitForm();
        });
        $("#btnFormSearch").unbind("click").click(function () {
            Common.EmailManager.SetPage(1);
            Common.EmailManager.SubmitForm();
            return false;
        });
        $("#btnAddForm").unbind("click").click(function () {
            window.location.href = $(this).data('url');
        });

        $(".update").unbind("click").click(function () {
            var id = $(this).data("id");
            window.location.href = $(this).data('url') + "?id=" + id;
        });
    },
    SetPage: function (page) {
        $("#IdformSearch").find("input[name='PageIndex']").val(page);
    },
    Paging: function (page) {
        Common.EmailManager.SetPage(page);
        Common.EmailManager.SubmitForm();
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
        Common.EmailManager.RegisterEvent();
    },
};
// create EmailManager Object
var EmailManager = EmailManager;
EmailManager.constructor = EmailManager;

