window.interopFunctions = {
    getOffsetTop: function () {
        var offset_top = document.getElementsByTagName("thead")[0].getBoundingClientRect().height + 1;
        return offset_top;
    },
    getOffsetLeft: function () {
        var offset_left = document.getElementsByClassName("hour")[0].getBoundingClientRect().width + 1;
        return offset_left;
    }
};
