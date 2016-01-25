﻿$(document).ready(function () {
    if ($("#banners").lengh != 0) {
        $("#banners").cycle({
            fx: 'scrollHorz',
            timeout: 10000,
            resizeContainer: false,
            slideResize: false,
            pager: '#pager',
            slideExpr: 'div.slide'
        });
    }
});

$('.dropdown-toggle').dropdown();

function changeLang() {
    switch($("html").prop("lang").toLowerCase()) {
        case "en-us":
            location.replace("http://" + window.location.host);
            break;
        case "fi-fi":
            location.replace("http://" + window.location.host + "/en");
            break;
        default:
            return false;
    }

    return false;

}