//var IS_DEVELOPER = true;
//function log(message) {
//    if (IS_DEVELOPER) {
//        console.log(message);
//    }
//}

//var rowNumber = 0;

//function resetRowNumber() {
//    rowNumber = 0;
//}

//function renderRowNumber(grid) {
//    ++rowNumber;
//    var page = parseInt($("#" + grid).data("kendoGrid").dataSource.page()) - 1;
//    var pagesize = $("#" + grid).data("kendoGrid").dataSource.pageSize();
//    return parseInt(rowNumber + (parseInt(page) * parseInt(pagesize)));
//}

//function getUrlKhachHang(maTrangThai) {
//    var url = '';
//    switch (maTrangThai) {
//        case 1:
//            url = "~/Areas/Admin/Content/images/map_KHT1.png";
//            break;
//        case 2:
//            url = "~/Areas/Admin/Content/images/map_KHT2.png";
//            break;
//        case 3:
//            url = "~/Areas/Admin/Content/images/map_KHT3.png";
//            break;
//        case 4:
//            url = "~/Areas/Admin/Content/images/map_KHT4.png";
//            break;
//        case 5:
//            url = "~/Areas/Admin/Content/images/map_KHT5.png";
//            break;
//        case 6:
//            url = "~/Areas/Admin/Content/images/map_KHT6.png";
//            break;
//        case 7:
//            url = "~/Areas/Admin/Content/images/map_KHT7.png";
//            break;
//        case 8:
//            url = "~/Areas/Admin/Content/images/map_KHT8.png";
//            break;
//        case 9:
//            url = "~/Areas/Admin/Content/images/map_KHT9.png";
//            break;
//        case 10:
//            url = "~/Areas/Admin/Content/images/map_KHT10.png";
//            break;
//        case 11:
//            url = "~/Areas/Admin/Content/images/map_KHT11.png";
//            break;
//        case 12:
//            url = "~/Areas/Admin/Content/images/map_KHT12.png";
//            break;

//        default:
//            url = "~/Areas/Admin/Content/images/ic_calltaxi.png";
//            break;
//    }

//    return url;
//}

//function getUrlMarkerTaiXe(maTrangThai) {
//    var url = '';
//    switch (maTrangThai) {
//        case 1: // online & free
//            url = "~/Areas/Admin/Content/images/ic_map_taixe_online_free.png";
//            break;
//        case 2: // online & busy
//            url = "~/Areas/Admin~/Areas/Admin/Content/images/ic_map_taixe_online_busy.png";
//            break;
//        case 3: // disconnect
//            url = "~/Areas/Admin/Content/images/ic_map_taixe_disconect.png";
//            break;
//        case 4: // offline
//            url = "~/Areas/Admin/Content/images/ic_map_taixe_offline.png";
//            break;
//        default:
//            url = "~/Areas/Admin/Content/images/ic_map_taixe_offline.png";
//            break;
//    }

//    return url;
//}

//function validateNumberFloat(evt) {
//    var theEvent = evt || window.event;
//    var key = theEvent.keyCode || theEvent.which;
//    key = String.fromCharCode(key);
//    var regex = /[0-9]|\./;
//    if (!regex.test(key)) {
//        theEvent.returnValue = false;
//        if (theEvent.preventDefault) theEvent.preventDefault();
//    }
//}
//function validateNumberInt(evt) {
//    var theEvent = evt || window.event;
//    var key = theEvent.keyCode || theEvent.which;
//    key = String.fromCharCode(key);
//    var regex = /[0-9]/;
//    if (!regex.test(key)) {
//        theEvent.returnValue = false;
//        if (theEvent.preventDefault) theEvent.preventDefault();
//    }
//}

function kendoAlert(msg) {
    window.kendoAlert = (function () {

        // this function is executed as soon as
        // it is loaded into the browser

        // create modal window on the fly and 
        // store it in a variable
        var win = $("<div>").kendoWindow({
            modal: true
        }).getKendoWindow();

        // returning a function will set kendoAlert to a function
        return function (msg) {

            // anything inside this function has access to the `win`
            // variable which contains the Kendo UI Window.  Even WAY
            // after the outer function was run and this object was created

            // set the content
            win.content(msg);

            // center it and open it
            win.center().open();
        };

    }());
}

//function ringDatXe() {
//    if (!IS_MUTE) {
//        new Howl({
//            urls: ['Ringing_Phone.mp3']
//        }).play();
//    }
//}

//function ringCheckin() {
//    if (!IS_MUTE) {
//        new Howl({
//            urls: ['RingPhoneCheckin.mp3']
//        }).play();
//    }
//}

$(function() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "showDuration": "0",
        "hideDuration": "0",
        "timeOut": "0",
        "extendedTimeOut": "0",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
});
