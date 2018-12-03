window.interopFunctions = {
    getOffsetTop: function (row) {
        var offset_top = $('tbody tr:nth-child(' + row + ')').get(0).getBoundingClientRect().top;
        return offset_top;
    },
    getOffsetLeft: function (row, col) {
        var offset_left = $('tbody tr:nth-child(' + row + ') td:not(.hour)').get(col).getBoundingClientRect().left;
        return offset_left;
    },
    getColumnWidth: function (row, col) {
        var col_width = $('tbody tr:nth-child(' + row + ') td:not(.hour)').get(col).getBoundingClientRect().width;
        return col_width;
    }
};
