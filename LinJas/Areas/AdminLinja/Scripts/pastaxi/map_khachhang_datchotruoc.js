
var MarkerDatTruocArray = [];
//var InfoDatTruocArray = [];

function initDatChoTruoc(tinhId) {

    $.get("/Map/LoadAllMarkerDatChoTruoc?TinhId=" + tinhId)
           .done(function (events) {
               for (var i = 0; i < events.length; i++) {
                   var latLng = new google.maps.LatLng(events[i].ViDo, events[i].KinhDo);

                   MarkerDatTruocArray[i] = new google.maps.Marker({
                       position: latLng,
                       icon: "/Content/images/map_KH_hen.png",
                       map: myMap,
                       Id: events[i].Id, //GiaoDichDatXeId  
                       DatXeId:events[i].DatXeId,
                       KhachHangId :events[i].KhachHangId,
                       TenKhachHang: events[i].TenKhachHang,
                       DienThoaiKhachHang: events[i].DienThoaiKhachHang,
                       NgayDatCho: events[i].NgayDatCho,
                       DiaDiemDon: events[i].DiaDiemDon,                       
                       DiaDiemDen: events[i].DiaDiemDen,
                       NgayDenDon: events[i].NgayDenDon,
                       index: i,
                   });

                   attachInfowindowDatChoTruoc(MarkerDatTruocArray[i]);
               }
           });

}

function attachInfowindowDatChoTruoc(marker) {
                      
    google.maps.event.addListener(marker, 'click', function () {

        log('attachInfowindowDatChoTruoc marker:');
        log(marker);

        var content =
       '<h4><u style="color:deeppink;">Khách hàng đặt chỗ trước </u><a href="JavaScript:MoveToTopPage()"><u style="float:right;color:black;font-size:12px;">  Lên Đầu trang </u></a></h4>' +
           '<ul>' +
           '<li>Khách hàng: <b> ' + marker.TenKhachHang + ' </b></li>' +
           '<li> SĐT: <b>' + marker.DienThoaiKhachHang + '</b> </li>' +
           '<li>Ngày đặt chỗ: <b> ' + marker.NgayDatCho + '</b></li>' +
           '<li>Địa điểm đón: <b> ' + marker.DiaDiemDon + '</b></li>' +
           '<li>Thời gian đón: <b> ' + marker.NgayDenDon + '</b></li>';


        //Lay thong tin chi tiet doi tac và loại hình đặt xe.
        $.get("/Map/GetThemThongTinDatXe?datXeId=" + marker.DatXeId)
           .done(function (data) {

               // --- Thêm mới thông tin về đặt xe, loại hình dịch vụ và hãng dịch vụ
               content = content +
                       '<li>Loại hình dịch vụ : <b> ' + data.LoaiHinhDichVu + '</b></li>' +
                       '<li>Hãng dịch vụ : <b> ' + data.HangDichVu + '</b></li>';

               // --- Nếu có khuyến mại thì thêm thông tin khuyến mại
               
               if (data.IsHaveKhuyenMai == 1) {

                   content = content +
                           '<fieldset class="fieldSetCustom">' +
                                '<legend class="legenCustom" style="color: lightseagreen;">Thông tin khuyến mại</legend>' +
                                    '&nbsp - Mã khuyến mại: <b> ' + data.MaKhuyenMai + '</b></br> ' +
                                    '&nbsp- Tiêu đề khuyến mại: <b> ' + data.TieuDeKhuyenMai + '</b></br>' +
                                    '&nbsp- Tiền khuyến mại: <b> ' + data.TienKhuyenMaiFormat + '</b></br>' +
                                    '</br>' +
                        '</fieldset>';
               }
               
               // --- kết thúc thông tin phần đặt xe
               content = content +
                        '</ul>' +
                       '<center>' +
                         '<button type="button" id="BtnCreate" onclick="showChiDinhTaiXe(\'' + marker.DatXeId + '\',\'' + marker.DienThoaiKhachHang + '\');" style="height:30px;" class="buttonChiDinh"><span class="glyphicon glyphicon-phone-alt"></span> Chỉ định tài xế</button>' +
                       '</center>';

               // --- Nếu là đặt xe tới đối tác thì hiển thị cả thông tin của đối tác.
               if (data.IsHavePartner == 1) {

                   content = content +
                            '<h4><u style="color:deeppink;">Thông tin đối tác</u></h4>' +
                            '<ul>' +
                            '<li>Tên đối tác: <b> ' + data.TenDoiTac + ' </b></li>' +
                            '<li> Tên chi nhánh: <b>' + data.TenChiNhanh + '</b> </li>' +
                            '<li>Số điện thoại: <b>' + data.SDT + '</b></li>' +
                            '<li>Địa chỉ: <b> ' + data.DiaChi + '</b></li>' +
                            '</ul>';
               }

               if (InfoKhachHangGlobal != null) {
                   InfoKhachHangGlobal.close();
               }

               //Hien thi thong tin tren marker
               InfoKhachHangGlobal = new google.maps.InfoWindow({ content: content });
               InfoKhachHangGlobal.open(myMap, marker);


           });


    });
}

function showInfowindowDatChoTruocByItemGrid(marker) {
    log('showInfowindowDatChoTruocByItemGrid');
    log(marker);


    var content =
   '<h4><u style="color:deeppink;">Khách hàng đặt chỗ trước </u><a href="JavaScript:MoveToTopPage()"><u style="float:right;color:black;font-size:12px;">  Lên Đầu trang </u></a></h4>' +
       '<ul>' +
       '<li>Khách hàng: <b> ' + marker.TenKhachHang + ' </b></li>' +
       '<li> SĐT: <b>' + marker.DienThoaiKhachHang + '</b> </li>' +
       '<li>Ngày đặt chỗ: <b> ' + marker.NgayDatCho + '</b></li>' +
       '<li>Địa điểm đón: <b> ' + marker.DiaDiemDon + '</b></li>' +
       '<li>Thời gian đón: <b> ' + marker.NgayDenDon + '</b></li>';
           

    //Lay thong tin chi tiet doi tac và loại hình đặt xe.
        $.get("/Map/GetThemThongTinDatXe?datXeId=" + marker.DatXeId)
           .done(function (data) {

               // --- Thêm mới thông tin về đặt xe, loại hình dịch vụ và hãng dịch vụ
               content = content +
                       '<li>Loại hình dịch vụ : <b> ' + data.LoaiHinhDichVu + '</b></li>' +
                       '<li>Hãng dịch vụ : <b> ' + data.HangDichVu + '</b></li>';

               // --- Nếu có khuyến mại thì thêm thông tin khuyến mại

               
               if (data.IsHaveKhuyenMai == 1) {

                   content = content +
                           '<fieldset class="fieldSetCustom">' +
                                '<legend class="legenCustom" style="color: lightseagreen;">Thông tin khuyến mại</legend>' +
                                    '&nbsp - Mã khuyến mại: <b> ' + data.MaKhuyenMai + '</b></br> ' +
                                    '&nbsp- Tiêu đề khuyến mại: <b> ' + data.TieuDeKhuyenMai + '</b></br>' +
                                    '&nbsp- Tiền khuyến mại: <b> ' + data.TienKhuyenMaiFormat + '</b></br>' +
                                    '</br>' +
                        '</fieldset>';
               }
               
               // --- kết thúc thông tin phần đặt xe
               content = content +
                        '</ul>' +
                       '<center>' +
                         '<button type="button" id="BtnCreate" onclick="showChiDinhTaiXe(\'' + marker.DatXeId + '\',\'' + marker.DienThoaiKhachHang + '\');" style="height:30px;" class="buttonChiDinh"><span class="glyphicon glyphicon-phone-alt"></span> Chỉ định tài xế</button>' +
                       '</center>';

               // --- Nếu là đặt xe tới đối tác thì hiển thị cả thông tin của đối tác.
               if (data.IsHavePartner == 1) {

                   content = content +
                            '<h4><u style="color:deeppink;">Thông tin đối tác</u></h4>' +
                            '<ul>' +
                            '<li>Tên đối tác: <b> ' + data.TenDoiTac + ' </b></li>' +
                            '<li> Tên chi nhánh: <b>' + data.TenChiNhanh + '</b> </li>' +
                            '<li>Số điện thoại: <b>' + data.SDT + '</b></li>' +
                            '<li>Địa chỉ: <b> ' + data.DiaChi + '</b></li>' +
                            '</ul>';
               }

               if (InfoKhachHangGlobal != null) {
                   InfoKhachHangGlobal.close();
               }

               //Hien thi thong tin tren marker
               InfoKhachHangGlobal = new google.maps.InfoWindow({ content: content });
               InfoKhachHangGlobal.open(myMap, marker);


           });


}

function showMarkerDatChoTruoc() {    
        for (var i = 0; i < MarkerDatTruocArray.length; i++) {
            MarkerDatTruocArray[i].setMap(myMap);
        }    
}

function hideAllMarerDatChoTruoc() {    
        for (var i = 0; i < MarkerDatTruocArray.length; i++) {
            MarkerDatTruocArray[i].setMap(null);
        }    
}


function showPinMarkerDatChoTruoc(giaoDichDatXeId) {

    var marker = findMarkerDatChoTruoc(giaoDichDatXeId);
        if (marker != null) {
            myMap.setCenter(marker.getPosition());
            myMap.setZoom(17);
            marker.setZIndex(1004);
            showInfowindowDatChoTruocByItemGrid(marker);

            // --- Scroll tới marker đang hiển thị thông tin.
            var element = document.getElementById("map");
            element.scrollIntoView();
        }
}

function findMarkerDatChoTruoc(giaoDichDatXeId) {
    if (MarkerDatTruocArray != null) {
        for (var i = 0; i < MarkerDatTruocArray.length; i++) {
            if (MarkerDatTruocArray[i].Id == giaoDichDatXeId)
                return MarkerDatTruocArray[i];
        }
    }
}

function getMarkerDatChoTruocByDatXeId(datXeId) {
    for (var i = 0; i < MarkerDatTruocArray.length; i++) {

        if (MarkerDatTruocArray[i].DatXeId == datXeId) {
            return MarkerDatTruocArray[i];
        }
    }

    return null;
}


function hideMarkerDatChoTruocById(giaoDichDatXeId) {
    for (var i = 0; i < MarkerDatTruocArray.length; i++) {
        if (MarkerDatTruocArray[i].Id == giaoDichDatXeId) {
            log('*** [DATCHOTRUOC] Tong dai vien cham soc xong - > an marker GDDXID =' + giaoDichDatXeId);
            MarkerDatTruocArray[i].setMap(null);
        }
    }
}

function hideMarkerDatChoTruocByDatXeId(datXeId) {
    for (var i = 0; i < MarkerDatTruocArray.length; i++) {
        if (MarkerDatTruocArray[i].DatXeId == datXeId) {
            log('*** [DATCHOTRUOC] Tong dai vien cham soc xong - > an marker DatXeId =' + datXeId);
            MarkerDatTruocArray[i].setMap(null);
        }
    }
}