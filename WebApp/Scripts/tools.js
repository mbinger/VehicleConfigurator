var Tools = {

    //show/hide warning text
    showWarning: function(text) {
        $("#main-warning").html($("#main-warning").html() + "<br>" + text);
        $("#main-warning").show();
    },
    hideWarning: function() {
        $("#main-warning").hide();
        $("#main-warning").html("");
    },

    //show/hide error text
    showError: function(text) {
        $("#main-error").html($("#main-error").html() + "<br>" + text);
        $("#main-error").show();
    },

    hideError: function() {
        $("#main-error").hide();
        $("#main-error").html("");
    },

    changeLang: function (cultureName) {
        document.cookie = window.vcfg.options.cookieLocalizationModuleCookieName+"="+cultureName;
        window.location.reload();
        return false;
    }
};