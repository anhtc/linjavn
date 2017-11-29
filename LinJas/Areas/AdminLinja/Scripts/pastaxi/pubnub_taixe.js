
var pubnub;
var TaiXeArray = [];
var InforTaiXeArray = [];
var TotalDriverBusy = 0;
var TotalDriverFree = 0;
var TotalDriverDisconnect = 0;
var TotalDriverOffline = 0;
var DriverOnlineArray = [];

var markerKhachHang;
var InfoKhachHang;

function initPubnub_UpdateDriverInfo() {

    pubnub = PUBNUB.init({
        publish_key: PUBLISH_KEY,
        subscribe_key: SUBSCRIBE_KEY
    });

    var requestPostionDriver =
        {
            "type": "update",
            "provinceId": 0,
            "kindOfTaxiId": 0,
            "timeSend": 0,
            "locationFrom": { "lng": 105.8628203, "lat": 21.003377 }
        }

    log(requestPostionDriver);

    pubnub.publish({
        channel: PASTAXI_DRIVER,
        message: requestPostionDriver,
    });
    log('lang nghe kenh ');
    //lang nghe kenh rieng do server tra ve lai xe cua tat ca cac tinh thanhf
    pubnub.subscribe({
         // channel: PASTAXI_CLIENT,
          channel: PASTAXI_LOCATION_SERVER,
          message: function (object) {
              console.log('CapNhat_TrangThai_TaiXe');
              log(JSON.stringify(object));
              CapNhat_TrangThai_TaiXe(object);
        }
    });

    // Lang nghe kenh Client de lay toa do tai xe
    pubnub.subscribe({
        channel: PASTAXI_CLIENT,
        message: function (object) {
            if (object.id != null) {            
                log('Lang nghe kenh Client de lay toa do tai xe');
                updateTaiXeInfo(object);

            }
        }
    });
    
};


function updateTaiXeInfo(object) {
    console.log(object);
    var index = getIndexDriverOnline(object.id);
    if (index < 0) {  // chua co thi them vao mang 
        DriverOnlineArray.push(object);
    }
    else {
        // co roi cap nhat lai thong tin moi nhat
        DriverOnlineArray[index] = object;
    }

    var lng = object.location.lng;
    var lat = object.location.lat;
    var position = new google.maps.LatLng(lat, lng);

    var trangThai;    // 1:online-free, 2: online-busy, 3:disconnect, 4: offline
    if (object.isFree) {
        trangThai = 1; // online & free
    }
    else {
        trangThai = 2;  // online &  busy
    }

    for (var i = 0; i < TaiXeArray.length; i++) {
        if (TaiXeArray[i].Id == object.id) {
            TaiXeArray[i].setIcon(getUrlMarkerTaiXe(trangThai)); // getUrlMarkerTaiXe dc khai bao trong pastaxi-funcs.js
            TaiXeArray[i].setPosition(position);
            TaiXeArray[i].TrangThai = trangThai;
        }
    }
    ThongKe_ThongTin_TaiXe();
}

function getIndexDriverOnline(taixeId) {
    for (var i = 0; i < DriverOnlineArray.length; i++) {
        if (DriverOnlineArray[i].id === taixeId) {
            return i;
        }
    }
    return -1;
}

function CapNhat_TrangThai_TaiXe(object) {

    if (object && TaiXeArray.length > 0) {
        var lng = object.location.lng;
        var lat = object.location.lat;
        var position = new google.maps.LatLng(lat, lng);

        var trangThai;    // 1:online-free, 2: online-busy, 3:disconnect, 4: offline
        if (object.isFree) {
            trangThai = 1; // online & free
        }
        else {
            trangThai = 2;  // online &  busy
        }

        // thay doi cho danh sach tat ca tai xe trong he thong 
        for (var i = 0; i < TaiXeArray.length; i++) {
            if (TaiXeArray[i].Id == object.id) {
                TaiXeArray[i].setIcon(getUrlMarkerTaiXe(trangThai));
                TaiXeArray[i].setPosition(position);
                TaiXeArray[i].TrangThai = trangThai;
            }
        }
        ThongKe_ThongTin_TaiXe();
    }
}

function ThongKe_ThongTin_TaiXe() {
    TotalDriverFree = 0;
    TotalDriverBusy = 0;
    TotalDriverDisconnect = 0;
    TotalDriverOffline = 0;
    var resultArray = TaiXeArray; // Neu chua bam nut Search bao gio mac dinh tim -> all Tai xe       

    for (var i = 0; i < resultArray.length; i++) {
        if (resultArray[i].TrangThai === 1)  // online &  free
        {
            TotalDriverFree++;
        }
        else if (resultArray[i].TrangThai === 2)// online & busy
        {
            TotalDriverBusy++;
        } else if (resultArray[i].TrangThai === 3)// disconnect
        {
            TotalDriverDisconnect++;
        }
        else if (resultArray[i].TrangThai === 4)// disconnect
        {
            TotalDriverOffline++;
        }
    }

    document.getElementById("TotalDriver").innerHTML = resultArray.length;
    document.getElementById("TotalOnlineFree").innerHTML = TotalDriverFree;
    document.getElementById("TotalOnlineBusy").innerHTML = TotalDriverBusy;
    document.getElementById("TotalDriverDisconnect").innerHTML = TotalDriverDisconnect;
    document.getElementById("TotalDriverOffline").innerHTML = TotalDriverOffline;
}

function initTaiXe(isFirst,hangId, tinhId, statusId, textSearch) {
    var mapId = myMap;
    $.get("/MapTaiXe/LoadMarkerTaiXe", { hangXeId: hangId, khuVucId: tinhId, trangThaiId: statusId, search: textSearch })
      .done(function (events) {
            removeAllMarker();
          for (var i = 0; i < events.length; i++) {
              var latLng = new google.maps.LatLng(events[i].ViDo, events[i].KinhDo);
              TaiXeArray[i] = new google.maps.Marker({
                  position: latLng,
                  icon: getUrlMarkerTaiXe(events[i].TrangThai),
                  map: mapId,
                  Id: events[i].Id,
                  ToaDo: latLng,
                  TaiXe: events[i].TaiXe,
                  TenDayDu: events[i].TenDayDu,
                  DienThoaiTaiXe: events[i].DienThoaiTaiXe,
                  BienSoXe: events[i].BienSoXe,
                  DienThoaiHangXe: events[i].DienThoaiHangXe,
                  LoaiHang: events[i].LoaiHang,
                  LoaiHangId: events[i].LoaiHangId,
                  KhuVucId: events[i].KhuVucId,
                  TrangThai: events[i].TrangThai, 
                  index: i,
              });

              attachInfo(TaiXeArray[i]);
          }
          getAllStatusDriver(hangId, tinhId, statusId, textSearch);
          // Cap nhat thong tin tai xe tu pubnub  ->pubnub_taixe.js
          if (isFirst)
              initPubnub_UpdateDriverInfo();
        });
}
// lấy tổng tài xế khi load data
function getAllStatusDriver(hangId, tinhId, statusId, textSearch) {
    $.get("/MapTaiXe/GetAllStatusDriver", { hangXeId: hangId, khuVucId: tinhId, trangThaiId: statusId, search: textSearch })
    .done(function (data) {
        TotalDriverOffline = data.TotalDriverOffline;
        TotalDriverFree = data.TotalOnlineFree;
        TotalDriverBusy = data.TotalOnlineBusy;
        TotalDriverDisconnect = data.TotalDriverDisconnect;
        document.getElementById("TotalDriver").innerHTML = data.TotalDriver;
        document.getElementById("TotalOnlineFree").innerHTML = TotalDriverFree;
        document.getElementById("TotalOnlineBusy").innerHTML = TotalDriverBusy;
        document.getElementById("TotalDriverDisconnect").innerHTML = TotalDriverDisconnect;
        document.getElementById("TotalDriverOffline").innerHTML = TotalDriverOffline;
    });
}
function removeAllMarker() {
    for (var i = 0; i < TaiXeArray.length; i++) {
        TaiXeArray[i].setMap(null);
    }
    TaiXeArray = [];
}
function showAllTaiXe() {
    for (var i = 0; i < TaiXeArray.length; i++) {
        TaiXeArray[i].setMap(myMap);
    }
}

function showTaiXeById(taixeId) {
    for (var i = 0; i < TaiXeArray.length; i++) {
        if (TaiXeArray[i].index == taixeId) {
            //hien thi marker tren ban do
            TaiXeArray[i].setMap(myMap);
            // hien thi thong tin tren ban do
            InforTaiXeArray[0].open(myMap, TaiXeArray[i]);

        }
        else {
            TaiXeArray[i].setMap(null);
        }
    }
}

function hideAllTaiXe() {
    for (var i = 0; i < TaiXeArray.length; i++) {
        TaiXeArray[i].setMap(null);
    }
}

function closeAllInforTaiXe() {
    if (InforTaiXeArray != null) {
        for (var i = 0; i < InforTaiXeArray.length; i++) {
            if (InforTaiXeArray[i] != null) {
                InforTaiXeArray[i].close();
            }
        }
        //inforWinDowsArray = [];
    }

    removeMarkerKhachHang();
}

function attachInfo(marker) {
    google.maps.event.addListener(marker, 'click', function () {
        var content =
               '<h4><u style="color:deeppink;">  Thông tin tài xế </u></h4>' +
               '<ul>' +
                   '<li>Tài xế: <b> ' + marker.TaiXe + ' </b></li>' +
                    '<li>Tên đầy đủ: <b> ' + marker.TenDayDu + ' </b></li>' +
                   '<li> SĐT: <b>' + marker.DienThoaiTaiXe + '</b> </li>' +
                   '<li>Biển số xe: <b>' + marker.BienSoXe + '</b></li>' +
                   '<li>Hãng xe: <b> ' + marker.LoaiHang + '</b></li>' +
                   '<li>SĐT hãng xe : <b> ' + marker.DienThoaiHangXe + '</b></li>' +
               '</ul>'

        InforTaiXeArray[marker.index] = new google.maps.InfoWindow({
            content: content
        });

        // dong cac window info khac
        closeAllInforTaiXe();
        //hien thi thong tin khach hang
        InforTaiXeArray[marker.index].open(myMap, marker);
        // bỏ phần set center khi click vào marker đi
        //myMap.setCenter(marker.getPosition());
        // myMap.setZoom(15);

        var checkDriverBusy = check_Driver_Online_Busy(marker.Id);
        //  alert(checkDriverBusy);

        if (checkDriverBusy) {
            showMarkerKhachHang(marker.Id);
        }

    });
}


function showMarkerKhachHang(TaiXeId) {

    $.get("/MapTaiXe/GetInfoKhachHangByTaiXe?TaiXeId=" + TaiXeId)
   // $.get('@Url.Action("GetInfoKhachHangByTaiXe", "MapTaiXe")', { TaiXeId: TaiXeId })
           .done(function (data) {
               if (data != null) {
                   var latLng = new google.maps.LatLng(data.ViDo, data.KinhDo);

                   markerKhachHang = new google.maps.Marker({
                       position: latLng,
                       icon: getUrlKhachHang(99),
                       map: myMap,
                       TenKhachHang: data.TenKhachHang,
                       SoDienThoai: data.SoDienThoai,
                       ThoiGianDonXe: data.ThoiGianDonXe,
                       DiaChiDon: data.DiaChiDon,
                       DiaChiDen: data.DiaChiDen

                   });
                   markerKhachHang.setZIndex(1000);

                   attachInfoMarkerKhachHang(markerKhachHang);
               }
           });
}

function attachInfoMarkerKhachHang(marker) {
    google.maps.event.addListener(marker, 'click', function () {

        var content =
           '<h4><u style="color:deeppink;">  Thông tin khách hàng </u></h4>' +
           '<ul>' +
               '<li>Tên khách hàng: <b> ' + marker.TenKhachHang + ' </b></li>' +
                '<li>Số điện thoại: <b> ' + marker.SoDienThoai + ' </b></li>' +
               '<li>Thời gian đón: <b>' + marker.ThoiGianDonXe + '</b> </li>' +
               '<li>Địa chỉ đón: <b>' + marker.DiaChiDon + '</b></li>' +
               '<li>Địa chỉ đến: <b> ' + marker.DiaChiDen + '</b></li>' +
           '</ul>'

        InfoKhachHang = new google.maps.InfoWindow({ content: content });
        InfoKhachHang.open(myMap, marker);

    });

}

function removeMarkerKhachHang() {
    if (markerKhachHang != null) {
        markerKhachHang.setMap(null);
        markerKhachHang = null;
    }

    if (InfoKhachHang != null) {
        InfoKhachHang.close();
    }
}



function check_Driver_Online_Busy(DriverId) {

    var resultArray = TaiXeArray; // Neu chua bam nut Search bao gio mac dinh tim -> all Tai xe
    for (var i = 0; i < resultArray.length; i++) {
        if (resultArray[i].Id == DriverId) {
            log('trangthaiId:' + resultArray[i].TrangThai);
        }

        if (resultArray[i].TrangThai == 0 && resultArray[i].Id == DriverId)  // online &  busy
        {
            //alert('markerId:' + RESULT_ARRAY[i].Id + ' | TaiXeId:' + DriverId);
            log('markerId:' + resultArray[i].Id + ' | TaiXeId:' + DriverId);
            return true;
        }
    }
    return false;
}