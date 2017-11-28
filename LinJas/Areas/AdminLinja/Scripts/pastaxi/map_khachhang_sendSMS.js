
$(function() {
    $('#textarea_SMS').keyup(function() {
        var strContent = $('#textarea_SMS').val();
        $('#textarea_feedback').html(strContent.length + ' kí tự');

    });

});

function showWindowSendSMS() {
    $("#windowSendSMS").data("kendoWindow").center().open();

    var soDienThoai;
    var diachidon;
    var diachiden;
    var bodyMessage;
    var hangxe = document.getElementById("hangxe_ten").innerHTML;
    if (hangxe == null) hangxe = "";
    hangxe = unicode2ascii(hangxe);

    if (ActionInsertOrUpdate == "Update") {
        var datXeId = document.getElementById("htmlDatXeId").value;
         soDienThoai = document.getElementById("htmlDienThoaiKH").value;      
        document.getElementById("sendSMS_sodienthoai").value = soDienThoai;

        var khachHangInfo = getMarkerKhachHangByDatXeId(datXeId);
         diachidon = getDiaChiRutGon(khachHangInfo.DiaChiDon);
         diachiden = getDiaChiRutGon(khachHangInfo.DiaChiDen);
               
        if (khachHangInfo) {

             bodyMessage =
            "PASGO:Taxi " + hangxe + " se den don ban trong 5' " +
            "\rLo trinh:" + unicode2ascii(diachidon) + "->" + unicode2ascii(diachiden) +" "+
            "\rUoctinh:" + khachHangInfo.KmUocTinh + "Km, " + khachHangInfo.GiaUocTinh + 'Vnd ' +
            "\rTai Phan mem di Taxi mien phi: bit.ly/pasgo";

             document.getElementById("textarea_SMS").value = bodyMessage;

             document.getElementById("textarea_feedback").innerHTML = document.getElementById("textarea_SMS").value.length + " Kí tự";
        }

    } else { // truong hop them moi        
        soDienThoai = document.getElementById('khachhang_sdt_timkiem').value;
        var khachhangDiachidon = document.getElementById('khachhang_diachidon').value;       
        var khachhangDiachiden = document.getElementById('khachhang_diachiden').value;

        diachidon = getDiaChiRutGon(khachhangDiachidon);
        diachiden = getDiaChiRutGon(khachhangDiachiden);

        document.getElementById("sendSMS_sodienthoai").value = soDienThoai;

        bodyMessage =
        "PASGO:Taxi " + hangxe + " se den don ban trong 5' " +
        "\rLo trinh:" + unicode2ascii(diachidon) + "->" + unicode2ascii(diachiden) +" "+
        "\rTai Phan mem di Taxi mien phi: bit.ly/pasgo";
        
        document.getElementById("textarea_SMS").value = bodyMessage;
        document.getElementById("textarea_feedback").innerHTML = document.getElementById("textarea_SMS").value.length +" Kí tự";
        
    }
}

function unicode2ascii(str) {
    var l;
    // remove accents, swap ñ for n, etc
    var from = 'ăâđêôơưàảãạáằẳẵặắầẩậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍÒỎÕỌÓỒỔỖỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỸỴÝĐ';
    var to = 'aadeoouaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIIIOOOOOOOOOOOOOOUUUUUUUUUUYYYYD';    
    for (var i = 0, l = from.length; i < l; i++) {
        str = str.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
    }
    return str;
}

function getDiaChiRutGon(diachi) {
    var ketqua = '';
    if (diachi) {
        var n = diachi.indexOf(",");
        ketqua = diachi.substring(0, n);

        if (ketqua.length == 0) {
            var k = diachi.indexOf("-");
            ketqua = diachi.substring(0, k);
        }
    }

    if (ketqua.length == 0) ketqua = diachi;

    return ketqua;
}

function sendSMSBrandName() {
     
    $.get("/Map/sendSMSBrandName?soDienThoai=" + document.getElementById("sendSMS_sodienthoai").value + "&noiDung=" + document.getElementById("textarea_SMS").value)
          .done(function (data) {
              $("#windowSendSMS").data("kendoWindow").close();

              pubnub.publish({
                  channel: PASGO_ALERT_SHOWNOTE,
                  message: { "data": "PUBLISH SendSMS", "type": "sendsms", "DatXeId": document.getElementById("htmlDatXeId").value, "Operator": document.getElementById("txtUserName").value }
              });

              if (data) {
                  alert(data.message);
              }             
    });
}

// se duoc goi ben PUBNUB subscribe
function notifyBySendSMS(datXeId, operator) {
    // duyet tren grid dat cho truoc
    var gview = $("#grid-datchotruoc").data("kendoGrid");
    var data = gview.dataSource.data();
    for (var i = 0; i < data.length; i++) {
        if (data[i]["DatXeId"] == datXeId) {

            // luu lai lich su trang thai
            //  saveStatusHistoryCustomer(id, data[i]["Operator"], data[i]["Trangthai_XulyId"]);

            //gan lai gia tri moi
            data[i]["Operator"] = "[" + operator + "] đã gửi SMS";
            data[i]["Trangthai_XulyId"] = "<img src='~/Areas/Admin/Content/images/calling.jpg' style='width:25px;height:25px;'/>";
            gview.refresh();

            return;
        }
    }

    // duyet tren grid khach hang
    gview = $("#grid-khachhang").data("kendoGrid");
    data = gview.dataSource.data();
    for (var i = 0; i < data.length; i++) {
        if (data[i]["DatXeId"] == datXeId) {

            // luu lai lich su trang thai
            //  saveStatusHistoryCustomer(id, data[i]["Operator"], data[i]["Trangthai_XulyId"]);

            data[i]["Operator"] = "[" + operator + "] đã gửi SMS";
            data[i]["Trangthai_XulyId"] = "<img src='~/Areas/Admin/Content/images/calling.jpg' style='width:25px;height:25px;'/>";
            gview.refresh();
            return;
        }
    }

}

function checkKhachHangSendedSMS(datXeId) {
    var isDisableButtonSendSms = false;
    if (KhachHangSendedSMS && KhachHangSendedSMS.length >= 0) {
        for (var i = 0; i < KhachHangSendedSMS.length; i++) {
            if (KhachHangSendedSMS[i]["DatXeId"] == datXeId) {
                // Neu da gui SMS roi thi disable nut SendSMS tren giao dien               
              //  log('in checkKhachHangSendedSMS');
                isDisableButtonSendSms = true;
            }
        }
    }
    document.getElementById("BtnChiDinhSendSMS").disabled = isDisableButtonSendSms;
}

