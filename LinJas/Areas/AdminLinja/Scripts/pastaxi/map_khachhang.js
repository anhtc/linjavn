var TotalDriverOnline = 0;
var TotalDriverBusy = 0;
var TotalDriverFree = 0;
var DriverOnlineArray = [];
var NewCallMarkerArray = [];
var NewCallInfoArray = [];
var KhachHangArray = [];
var markerTaiXe;
var InfoKhachHangGlobal;
var KhachHangSendedSMS = [];

var pubnub;
var isOnVolume = true;

function initPubnub_TrangThai_KhachHang() {
    //Cap nhat trang thai thong tin khach hang
    //log('initPubnub_TrangThai_KhachHang');

    pubnub = PUBNUB.init({
        publish_key: PUBLISH_KEY,
        subscribe_key: SUBSCRIBE_KEY
    });

    pubnub.subscribe({
        channel: PASTAXI_ORDER_STATUS,
        message: function (object) {
            log('PASTAXI_ORDER_STATUS');
            log(JSON.stringify(object));
            if (object && object.DatXeId) {
                updateCustomerStatus(object);
            }

        }
    });

    //LANG GHI SU KIEN TONG DAI VIEN   
    pubnub.subscribe({
        channel: PASGO_ALERT_SHOWNOTE,
        message: function (object) {

            log("PASGO_ALERT_SHOWNOTE");
            log(object);

            if (object == null) return;

            if (object.type == "sendsms") {
                //duoc dinh nghia ben map_khachhang_sendSMS
                notifyBySendSMS(object.DatXeId, object.Operator);
                //Luu lai thong tin vao Mang de xu ly sau  nay
                var smsInfo = { "DatXeId": object.DatXeId, "Operator": object.Operator };
                KhachHangSendedSMS.push(smsInfo);
                document.getElementById("BtnChiDinhSendSMS").disabled = true;
            }
            else if (object.type == "closenote") {
                // duoc dinh nghia ben index.cshtml
                changeItemGridByCloseNote(object.GiaoDichDatXeId, object.DatXeId);
            }
            else if (object.type == "chidinhtaixe") {
                // duoc dinh nghia ben index.cshtml                
                removeItemGridByDatXeId(object.DatXeId);
            }
            else if (object.type == "customerCare") {
                customerCare(object);
            }

            else if (object.type == "shownote") {
                // trieu goi ham ben Index.cshtml
                changeItemGridByShowNote(object.GiaoDichDatXeId, object.Operator);
            }

        }
    });

    // Lang nghe kenh trang thai cua hang dang xu ly khach hang
    pubnub.subscribe({
        channel: PASTAXI_ONEPAS_HANGXE_PUBNUB,
        message: function (object) {
            
            
            //log("PUBNUB OF HANG XE khaidvd:" + JSON.stringify(object));
            log(object);
            if (object == null) return;
            //log("PUBNUB OF HANG XE 111111:" + PASTAXI_ONEPAS_HANGXE_PUBNUB);

            //__Update Trang Thai Hang xe Len grid
            //log('trangThai' + object.trangThai);
            //log('datXeId ' + object.datXeId);
            if (object.trangThai <= 6) {

                //__Update trang thai cua hang xe

                if ((object.trangThai == 1) || (object.trangThai == 2)) {

                    object.hangXeName = "...";
                    object.tongDaiHangName = "...";
                }

                updateStatusHangXeOnGrid(object.datXeId, object.hangXeName, object.tongDaiHangName, object.trangThai, object.ghiChu, "");
            } else {
                // Xoa Hang xe khi da hoan thanh giao dich
                //log('RECEIVED PUBNUB OF HANG XE:' + object.datXeId);

                $.get("/Map/GetGiaoDichDatXeId?datXeId=" + object.datXeId)
               .done(function (data) {
                     
                   if (data) {
                       //log('Get Giao dich dat xe ID' + data);

                       object.TrangThaiXuLyId = 3;
                       object.GiaoDichDatXeId = data.Id;
                       customerCare(object);
                   }

               });
 
            }

            /*
            //__UPdate Hang Xe Cham Soc Vao SQL.
            $.get("/Map/UpdateHangXeChamSoc?datXeId=" + object.datXeId+"&hangXeId=" + object.hangXeId+"&nhanVienId=" + object.tongDaiHangId+"&Trangthai_XulyId=" + object.trangThai+"&Ghichu=" + object.ghiChu )
            .done(function (data) {
               //___LOG OR FAIL
            });
           */
        }
    });


    //___Nhan thong bao khi co 1 check in moi
    pubnub.subscribe({
        channel: PASTAXI_PARNER_ONEPAS_CHECKIN_PUBNUB,
        message: function (object) {

            //log("PASTAXI_PARNER_ONEPAS_CHECKIN_PUBNUB:" + object.dateXeId);
            $(document).ready(function () {
                toastr.clear();
                toastr["info"]("Có giao dịch mới!", "Thông báo");

                ringCheckin();
            });
        }

        });

};

function customerCare(object) {
    //log('--- subsribe  customerCare---');
   // log(JSON.stringify(object));
    //An marker khach hang khi Nhan vien tong dai da su ly xong
    var trangThaiXyLyId = object.TrangThaiXuLyId;
    var trangThaiChamSoc = '';

    if (trangThaiXyLyId == 1) trangThaiChamSoc = 'Đang xử lý';
    if (trangThaiXyLyId == 2) trangThaiChamSoc = 'Không liên lạc được';
    if (trangThaiXyLyId == 4) trangThaiChamSoc = 'Khác';
    if (trangThaiXyLyId == 40) trangThaiChamSoc = 'Đã xử lý xong';

    if (trangThaiXyLyId && trangThaiXyLyId == 40) {  //=40 : Tong Dai Vien Da xu ly xong
       // log('trangThaiXyLyId =40');
        //an marker khach hang
        hideMarkerKhachHangById(object.GiaoDichDatXeId);
        //an marker dat cho truoc
        hideMarkerDatChoTruocById(object.GiaoDichDatXeId);

        removeItemGridByGiaoDichDatXeId(object.GiaoDichDatXeId); // duoc khai bao ben index.cshtml
    } else {
        //Cap nhat  du lieu moi nhat tren luoi                             
        log('trangThaiXyLyId:' + trangThaiXyLyId + ' trangThaiChamSoc:' + trangThaiChamSoc);
        //ham duoc dinh nghia ben index.cshtml
        changeItemGridByUpdateNote(object.GiaoDichDatXeId, object.Operator, trangThaiChamSoc, object.GhiChu);
    }

    //Kiem tra ban ghi dang lam viec hien tai  -> neu trung & da duoc cham soc thi dua ra canh bao 
    //currentValueOfGiaoDichDatXeId : bien toan cuc duoc gan gia tri ben map/index -> khi tong dai vien click showNote
    if (currentValueOfGiaoDichDatXeId == object.GiaoDichDatXeId) {
        document.getElementById("lblNoteThongBao").innerHTML = "Khách hàng này vừa được tổng đài viên khác chăm sóc & cập nhật trạng thái";
    }
}

function showMarkerInfoNewCall(marker) {
    //log("NEW CALL");
    //log(marker);
    //log("END LOG");

    google.maps.event.addListener(marker, 'click', function () {
        var content =
        '<h4><u style="color:deeppink;">Thông tin khách hàng </u> <a href="JavaScript:MoveToTopPage()"><u style="float:right;color:black;font-size:12px;">  Lên Đầu trang </u></a></h4>' +
        '<ul>' +
        '<li>Khách hàng: <b> ' + marker.fullName + ' </b></li>' +
        '<li> SĐT: <b>' + marker.numberPhone + '</b> </li>' +
        '<li>Địa chỉ đón: <b> ' + marker.locationFrom + '</b></li>' +
        '<li>Địa chỉ đến: <b> ' + marker.locationTo + '</b></li>' +
        '<li>Mô tả: <b> ' + marker.near + '</b></li>' +
        '<li>Thời điểm: <b> ' + marker.timeCome + '</b></li>' +
        '<li>Nhóm xe: <b> ' + getTenNhomXeById(marker.kindOfTaxiId) + '</b></li>' +
        '<li>Khoảng cách: <b> ' + marker.distance + '</b></li>' +
        '<li>Giá ước tính: <b> ' + marker.price + '</b></li>';

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
                        '</ul>';

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

function showWindowInfo(marker) {
    //log('getTenNhomXeById(marker.kindOfTaxiId)=' + getTenNhomXeById(marker.kindOfTaxiId));
}

function getTenNhomXeById(id) {

    var nhomXe = '';
    if (id == 0 || id == '0') nhomXe = 'Tất cả';
    if (id == 1 || id == '1') nhomXe = '4 chỗ cụt đuôi';
    if (id == 2 || id == '2') nhomXe = '4 chỗ dài đuôi';
    if (id == 3 || id == '3') nhomXe = '7 chỗ';

    return nhomXe;

}

function closeAllNewCallInfo() {
    if (NewCallInfoArray != null) {
        for (var i = 0; i < NewCallInfoArray.length; i++) {
            NewCallInfoArray[i].close();
        }
        //inforWinDowsArray = [];
    }
}

function checkExistNewCall(bookId) {
    for (var i = 0; i < NewCallMarkerArray.length; i++) {
        if (NewCallMarkerArray[i].bookingId == bookId) {
            return true;
        }
    }

    return false;
}


//Kiem tra thoi gian sau 2 phut thi An marker Cuoc goi
var myVar = setInterval(function () { checkTimeForCall(); }, 10 * 1000);

//An hien marker cuoc goi dien sau 2' ->close
function checkTimeForCall() {
    for (var i = 0; i < NewCallMarkerArray.length; i++) {

        var expTime = compareTimeForMarker(NewCallMarkerArray[i].currentdate, new Date());
        if (expTime >= 30)  // 2s thi xoa & remove marker
        {
             // log('[map_khachhang][checkTimeForCall] NewCall Time >2 minus remove marker on map sdt: ' + NewCallMarkerArray[i].numberPhone);

            NewCallMarkerArray[i].setMap(null);
            //  NewCallMarkerArray.splice(i, 1); // xoa phan tu khoi mang           
        }
    }

}

function compareTimeForMarker(date1, date2) {
    var h = 60 * 60;
    var m = 60;
    var s1 = h * date1.getHours() + m * date1.getMinutes() + date1.getSeconds();
    var s2 = h * date2.getHours() + m * date2.getMinutes() + date2.getSeconds();
    var countSecond = Math.abs(s2 - s1);
    return countSecond;
}

function deleteMarker(index) {
    NewCallMarkerArray[index].setMap(null);
    NewCallMarkerArray.splice(index, 1);
}

function getTrangThaiDatXeById(trangThaiId) {

    var trangthai = '';
    if (trangThaiId == 1) trangthai = 'Đang gọi xe';
    if (trangThaiId == 2) trangthai = 'Khách hàng hủy';
    if (trangThaiId == 3) trangthai = 'Không có tài xế';
    if (trangThaiId == 4) trangthai = 'Đang thử lại';
    if (trangThaiId == 5) trangthai = 'Đang tìm hãng';
    if (trangThaiId == 6) trangthai = 'Tìm thấy tài xế';
    if (trangThaiId == 7) trangthai = 'Tài xế đã đến đón';
    if (trangThaiId == 8) trangthai = 'Khách đã gặp tài xế';
    if (trangThaiId == 9) trangthai = 'Tài xế để khách lên xe';
    if (trangThaiId == 10) trangthai = 'Hoàn thành';
    if (trangThaiId == 11) trangthai = 'Khách hàng hủy';
    if (trangThaiId == 12) trangthai = 'Tài xế hủy';

    return trangthai;
}

function updateCustomerStatus(object) {

    //B1: Kiem tra DatXeId co trung voi Marker New Call dang nhap nhay khong ? neu trung thi xoa marker nay di
    hideMarkerNewCallById(object.DatXeId);
    
    //log("TRANG THAI DAT XE:" + object.TrangThaiDatXeId);
    //log(object);
    //Duyet cac trang thai cham soc khach hang de hien thi tren grid
    for (var i = 0; i < TRANGTHAI_DATXE_ARRAY.length; i++) {
        //Neu cac trang thai ={1,3,4,11,12} thi thuc hien ben duoi
        if (TRANGTHAI_DATXE_ARRAY[i] == object.TrangThaiDatXeId) {

            //B2: Xoa het marker tren ban do            
            for (var k = 0; k < KhachHangArray.length; k++) {
                if (KhachHangArray[k].DatXeId == object.DatXeId) {
                    KhachHangArray[k].setMap(null);
                    KhachHangArray.splice(k, 1);
                }
            }

            //B3: Xoa het du lieu tren Grid
            removeItemGridKhachHangByChangeStatus(object.DatXeId); // ham duoc dinh nghia ben index.cshtml 

            // lay trang thi hien thi tu Id trang thai
            var trangThaiHienThi = getTrangThaiDatXeById(object.TrangThaiDatXeId);

            //log("TrangThaiId:" + object.TrangThaiDatXeId);
            //B4: Tao moi thong tin dat xe  marker tren ban do & tren GridView
           // log('Tao moi thong tin dat xe  marker tren ban do & tren GridView datxeid=' + object.DatXeId);

            //log('GetGiaoDichDatXeId');

            $.get("/Map/GetGiaoDichDatXeId?datXeId=" + object.DatXeId)
                .done(function(data) {

                log(data);

                if (data) {
                    if (!data.IsDatTruoc) {

                        var index = KhachHangArray.length;
                        var latLng = new google.maps.LatLng(data.ViDo, data.KinhDo);

                        KhachHangArray[index] = new google.maps.Marker({
                            position: latLng,
                            icon: getUrlKhachHang(object.TrangThaiDatXeId),
                            map: myMap,
                            Id: data.DatXeId, // get Id from database
                            DatXeId: data.DatXeId,
                            DiaChiDon: data.DiaChiDon,
                            DiaChiDen: data.DiaChiDen,
                            TrangThai: trangThaiHienThi,
                            TrangThaiId: object.TrangThaiDatXeId,
                            TenKhachHang: data.TenKhachHang,
                            SoDienThoai: data.SoDienThoaiKH,
                            TenTaiXe: data.TenLaiXe,
                            DienThoaiTaiXe: data.DienThoaiTaiXe,
                            ThoiGian: data.ThoiGian,
                            index: index,
                            NhomXe: getTenNhomXeById(data.NhomXeId), // Ham lay ten nhom xe tu Id duoc dinh nghia ben index.cshtml
                            KmUocTinh: data.KmUocTinhFormat,
                            GiaUocTinh: data.GiaUocTinhFormat,
                            MoTa: data.MoTa,
                            MaGiamGia: data.MaGiamGia,
                            TieuDeKhuyenMai: data.TieuDeKhuyenMai,
                            TienKhuyenMai: data.TienKhuyenMai,
                        });

                        KhachHangArray[index].setZIndex(1007);
                        attachInfowindowKhachHang(KhachHangArray[index]);

                        if (object.TrangThaiDatXeId == 1) {
                            KhachHangArray[index].setAnimation(google.maps.Animation.BOUNCE);
                            ringDatXe();
                        }

                        //Them moi thong tin tren GridView Khach hang tren ban do theo doi  khach hang
                        //log('[updateCustomerStatus] create new Item on GridView');
                        addNewRecordItemOnGrid(KhachHangArray[index]); // duoc dinh nghia ben index.cshtml

                        log(KhachHangArray[index]);
                    } else {
                        ringDatXe();
                    }
                }
            });
        }
    }

}

function hideMarkerNewCallById(datXeId) {
    //  log('Chuyển trạng thái -> Ẩn marker cuộc gọi datxeId=' + datXeId);
    for (var i = 0; i < NewCallMarkerArray.length; i++) {
        if (NewCallMarkerArray[i].DatXeId == datXeId) {
            NewCallMarkerArray[i].setMap(null);
            NewCallMarkerArray.splice(i, 1); // xoa phan tu khoi mang           
        }
    }
}

function checkExistCustomer(datXeId) {
    for (var i = 0; i < KhachHangArray.length; i++) {
        if (KhachHangArray[i].DatXeId == datXeId) {
            return true;
        }
    }

    return false;
}

function getMarkerForDatXeId(DatXeId) {    
    for (var i = 0; i < NewCallMarkerArray.length; i++) {

        if (NewCallMarkerArray[i].DatXeId == DatXeId) {
            // log('return true -> getMarkerForDatXeId' + DatXeId);
            return NewCallMarkerArray[i];
        }
    }

    return null;
}

function initKhachHag(tinhId) {

    // sua lai thu tuc de filter theo khu vuc 
    $.get("/Map/LoadMarkerKhachHang?tinhId=" + tinhId)
           .done(function (events) {
               for (var i = 0; i < events.length; i++) {
                   var latLng = new google.maps.LatLng(events[i].ViDo, events[i].KinhDo);
                   KhachHangArray[i] = new google.maps.Marker({
                       position: latLng,
                       icon: getUrlKhachHang(events[i].TrangThaiId),
                       map: myMap,
                       Id: events[i].Id, //GiaoDichDatXeId
                       DatXeId: events[i].DatXeId,
                       DiaChiDon: events[i].DiaChiDon,
                       DiaChiDen: events[i].DiaChiDen,
                       ToaDo: latLng,
                       TrangThai: getTrangThaiDatXeById(events[i].TrangThaiId),
                       TrangThaiId: events[i].TrangThaiId,
                       KhachHangId: events[i].KhachHangId,
                       TenKhachHang: events[i].TenKhachHang,
                       SoDienThoai: events[i].SoDienThoai,
                       TenTaiXe: events[i].TenTaiXe,
                       TaiXeId: events[i].TaiXeId,
                       DienThoaiTaiXe: events[i].DienThoaiTaiXe,
                       ThoiGian: events[i].ThoiGian,
                       index: i,
                       NhomXe: events[i].NhomXe,
                       KmUocTinh: events[i].KmUocTinhFormat,
                       GiaUocTinh: events[i].GiaUocTinhFormat,
                       MoTa: events[i].MoTa,
                       MaGiamGia: events[i].MaGiamGia,
                       TienKhuyenMai: events[i].TienKhuyenMai,
                       TieuDeKhuyenMai: events[i].TieuDeKhuyenMai
                   });

                   attachInfowindowKhachHang(KhachHangArray[i]);
               }
           });

}

function openWindowChiDinhTaiXe(datxeid, sdt, tenkhachhang, khachhangid, diaChiDon, diaChiDen) {
    var url = '/ChiDinhTaiXe/Index?datxeid=' + datxeid + '&sdt=' + sdt + '&tenkhachhang=' + escape(tenkhachhang) + '&khachhangid=' + khachhangid + '&don=' + escape(diaChiDon) + '&den=' + escape(diaChiDen);
    window.open(url);
}

//gan su kien click hien thi thong tin chi tiet khach hang & doi tac tren marker
function attachInfowindowKhachHang(marker) {
    // marker.setZIndex(1002);
    google.maps.event.addListener(marker, 'click', function () {
        showInfoDetal(marker);
    });
}

function showInfoDetal(marker) {
    //log("MARKER INFO:");
    //log(marker);

    var content =
   '<h4><u style="color:deeppink;">  Thông tin khách hàng </u> <a href="JavaScript:MoveToTopPage()"><u style="float:right;color:black;font-size:12px;">  Lên Đầu trang </u></a></h4>' +
   '<ul>' +
   '<li>Khách hàng: <b> ' + marker.TenKhachHang + ' </b></li>' +
   '<li> SĐT: <b>' + marker.SoDienThoai + '</b> </li>' +
   '<li>Trạng thái: <b>' + marker.TrangThai + '</b></li>' +
   '<li>Địa chỉ đón: <b> ' + marker.DiaChiDon + '</b></li>' +
   '<li>Địa chỉ đến: <b> ' + marker.DiaChiDen + '</b></li>' +
    '<li>Mô tả: <b> ' + marker.MoTa + '</b></li>' +
   '<li>Nhóm xe: <b> ' + marker.NhomXe + '</b></li>' +
   '<li>Km ước tính: <b> ' + marker.KmUocTinh + '</b></li>' +
   '<li>Giá ước tính: <b> ' + marker.GiaUocTinh + '</b></li>' +
   '<li>Thời điểm: <b> ' + marker.ThoiGian + '</b></li>' +
   '<li>Tài xế đón: <b> ' + marker.TenTaiXe + '</b></li>' +
   '<li>SĐT tài xế : <b> ' + marker.DienThoaiTaiXe + '</b></li>';

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
                        '<button type="button" id="BtnCreate" onclick="showChiDinhTaiXe(\'' + marker.DatXeId + '\',\'' + marker.SoDienThoai + '\');" style="height:30px;" class="buttonChiDinh"><span class="glyphicon glyphicon-phone-alt"></span> Chỉ định tài xế</button>' +
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


function stringNotIncludeSpecificChar(contentInput) {
    return contentInput.replace(/"/g, '').replace(/'/g, '').trim();
}

function showMarkerTaiXe(taiXeId) {

    if (taiXeId) {
        removeMarkerTaiXe(); // Close other Marker
        //hideAllMarerKhachHang();

        $.get('@Url.Action("showMarkerByTaiXeId", "Map")', { TaiXeId: taiXeId })
               .done(function (data) {
                   if (data != null) {
                       var latLng = new google.maps.LatLng(data.ViDo, data.KinhDo);

                       markerTaiXe = new google.maps.Marker({
                           position: latLng,
                           icon: getUrlMarkerTaiXe(data.TrangThai),
                           map: myMap,
                           Id: data.Id,
                           DatXeId: data.DatXeId,
                           ToaDo: latLng,
                           TaiXe: data.TaiXe,
                           TenDayDu: data.TenDayDu,
                           DienThoaiTaiXe: data.DienThoaiTaiXe,
                           BienSoXe: data.BienSoXe,
                           LoaiHang: data.LoaiHang,
                           DienThoaiHangXe: data.DienThoaiHangXe

                       });

                       attachInfoMarkerTaiXe(markerTaiXe);
                   }

               });
    }

}

function attachInfoMarkerTaiXe(marker) {
    google.maps.event.addListener(marker, 'click', function () {
        var content =
         '<h4><u style="color:deeppink;">  Thông tin tài xế </u></h4>' +
             '<ul>' +
             '<li>Tài xế: <b> ' + marker.TaiXe + ' </b></li>' +
             '<li>Tên đầy đủ: <b> ' + marker.TenDayDu + ' </b></li>' +
             '<li> SĐT: <b>' + marker.DienThoaiTaiXe + '</b> </li>' +
             '<li>Biển số xe: <b>' + marker.BienSoXe + '</b></li>' +
             '<li>Hãng xe: <b> ' + marker.LoaiHang + '</b></li>' +
             '</ul>';
        var info = new google.maps.InfoWindow({
            content: content
        });
        info.open(myMap, marker);

    });
}

function showMarkerKhachHangOnMap() {
    for (var i = 0; i < KhachHangArray.length; i++) {
        KhachHangArray[i].setMap(myMap);
    }
}

function hideAllMarerKhachHangOnMap() {
    for (var i = 0; i < KhachHangArray.length; i++) {
        KhachHangArray[i].setMap(null);
    }
}

function removeMarkerTaiXe() {
    if (markerTaiXe != null) {
        markerTaiXe.setMap(null);
        markerTaiXe = null;
    }
}

function findIndexByKhachHangId(id) {

    log(KhachHangArray);

    if (KhachHangArray != null) {
        for (var i = 0; i < KhachHangArray.length; i++) {
            if (KhachHangArray[i].Id == id)
                return KhachHangArray[i];
        }
    }
}

function setIndexDefaultAllMarker() {
    if (KhachHangArray != null) {
        for (var i = 0; i < KhachHangArray.length; i++) {
            KhachHangArray[i].setZIndex(0);
        }
    }
}

function hideMarkerKhachHangById(giaoDichDatXeId) {
    for (var i = 0; i < KhachHangArray.length; i++) {
        if (KhachHangArray[i].Id == giaoDichDatXeId) {
            KhachHangArray[i].setMap(null);
        }
    }
}

function hideMarkerKhachHangByDatXeId(datXeId) {
    for (var i = 0; i < KhachHangArray.length; i++) {
        if (KhachHangArray[i].DatXeId == datXeId) {
            KhachHangArray[i].setMap(null);
        }
    }
}

function getMarkerKhachHangByDatXeId(datXeId) {
    for (var i = 0; i < KhachHangArray.length; i++) {

        if (KhachHangArray[i].DatXeId == datXeId) {
            return KhachHangArray[i];
        }
    }

    return null;
}

function volume_on_off()
{
    if (IS_MUTE)
    {
        // bật volume
        IS_MUTE = false;
        $('#volume').attr("src", "../Content/images/volume_on.png");

        log("Mở tiếng");
    }else
    {
        // tắt volume
        IS_MUTE = true;
        $('#volume').attr("src", "../Content/images/volume_off.png");

        log("Tắt tiếng");
    }
}

