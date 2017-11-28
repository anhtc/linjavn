
var currentValue = 2; // bien luu tru trang thai Radio 1:Taixe,2:Hangxe
var ActionInsertOrUpdate = "Insert";

function showChiDinhTaiXe(datXeId, soDienThoai) {

    checkKhachHangSendedSMS(datXeId); // ham duoc dinh nghia trong file map_khachhang_sendSMS.js

   // log('showChiDinhTaiXe');
    log('datXeId:' + datXeId + ' soDienThoai:'+soDienThoai);

    if (datXeId == '') //Truong hop them moi
    {
       // log('Them moi chi dinh tai xe');
        clearDataInput();
        enableInputKhachHang();
        ActionInsertOrUpdate = "Insert";
        //Hien thi nut Search Tim Kiem doi tac
        $("#BtnChonDoiTac").show();
        document.getElementById('NhomCNDoiTacId').value = null;

    } else {
        //An nut Search Tim kiem thong tin doi tac
        $("#BtnChonDoiTac").hide();
        ActionInsertOrUpdate = "Update";
       // log('showChiDinhTaiXe DatXeId:' + datXeId + ' soDienThoai:' + soDienThoai);
        document.getElementById("htmlDatXeId").value = datXeId;
        document.getElementById("htmlDienThoaiKH").value = soDienThoai;
        document.getElementById("txtGhiChuChiDinhTaiXe").value = "";

        var khachHangInfo = getMarkerKhachHangByDatXeId(datXeId);
       // log('line 32: KhachhangInfo');
        //log(khachHangInfo);
         if (khachHangInfo) {  // marker khach hang
             showInfoKhachHang(khachHangInfo);
             //log('marker from khachhang');
         }
         else // marker dat cho truoc
         {
             khachHangInfo = getMarkerDatChoTruocByDatXeId(datXeId);
             if (khachHangInfo) {
                 showInfoChiDinhDatChoTruoc(khachHangInfo);
             }
         }       
         disableInputKhachHang();
        //hien thi thong tin parner
        showInfoChiDinhThongTinDoiTac(datXeId);
    }

    $("#ChiDinhTaiXeWindow").data("kendoWindow").center().open();
}

function ChiDinhTaiXeOnMapKhachHang() {
       
    // Truong hop Chi dinh cho Ban ghi co san tren Grid
    if (ActionInsertOrUpdate == "Update") { 
        var datXeId = document.getElementById("htmlDatXeId").value;
        var soDienThoai = document.getElementById("htmlDienThoaiKH").value;
       
     //   log('[mapkhachhang][ChiDinhTaiXeOnMapKhachHang] THop Update Thong Tin DatXeId:' + datXeId + ' soDienThoai:' + soDienThoai);

        if (currentValue == 1) { //Chi dinh tai xe - > cap nhat thong tin datxe cho lai xe 
         //   log('=>Chi dinh cho taixe');
            var laiXeId = document.getElementById('TaiXeId').value;
            ChiDinhTaiXeUpdateDatXeByTaiXeOnMap(datXeId, soDienThoai, laiXeId);
        }
        else // chi dinh tai xe -> cap nhat thong tin dat xe cho Hang xe
        {
           
            var hangXeId = document.getElementById('HangXeId').value;
            var soHieuXe = document.getElementById("hangxe_sohieu_taixe").value;
           // log('=>Chi dinh cho Hang xe ID:' + hangXeId);

            ChiDinhTaiXeUpdateDatXeByHangXeOnMap(datXeId, soDienThoai, hangXeId, soHieuXe);
        }
    }
    // Truong hop them moi chi dinh
    else {
      //  log('[mapkhachhang][ChiDinhTaiXeOnMapKhachHang] Insert Them moi ban ghi');
        insertChiDinhProcess();
    }


}

function handleClick(myRadio) {
    currentValue = myRadio.value;
    document.getElementById("BtnChiDinh").disabled = true;
    showHideGroupTaiXeHangXeInfo();
}

function showHideGroupTaiXeHangXeInfo() {
    // show Lai xe
    if (currentValue == 1) {
        $("#groupThongTinTaiXe").show();
        $("#groupHangXeInfo").hide();
        $("#groupInputSoHieuTaiXe").hide();
        removeHangXeInfoDetail();
        $("#groupSDTHangTaiXe").show();
        $("#groupListHangXe").hide();

    } else {  // hien thi hang xe
        $("#groupThongTinTaiXe").hide();
        $("#groupHangXeInfo").show();
        $("#groupInputSoHieuTaiXe").show();
        removeTaiXeInfoDetail();
        $("#groupSDTHangTaiXe").hide();
        $("#groupListHangXe").show();
    }
}

function removeHangXeInfoDetail() {
    //document.getElementById('taixe_thongtin_timkiem').value = "";
    document.getElementById('HangXeId').value = "";
    document.getElementById("hangxe_ten").innerHTML = "";
    document.getElementById("hangxe_khuvuc").innerHTML = "";
    document.getElementById("hangxe_sdt").innerHTML = "";
    document.getElementById("hangxe_giamocua").innerHTML = "";
    document.getElementById("hangxe_sohieu_taixe").value = "";


    //  document.getElementById("BtnChiDinh").disabled = true;
}

function removeDoiTacInfoDetail() {

        document.getElementById("doitac_tendoitac").innerHTML = "";
        document.getElementById("doitac_tenchinhanh").innerHTML = "";
        document.getElementById("doitac_sodienthoai").innerHTML = "";
        document.getElementById("doitac_diachi").innerHTML = "";
}

function removeTaiXeInfoDetail() {
    document.getElementById('TaiXeId').value = "";
    document.getElementById("taixe_tentaixe").innerHTML = "";
    document.getElementById("taixe_sdt").innerHTML = "";
    document.getElementById("taixe_code").innerHTML = "";
    document.getElementById("taixe_hangxe").innerHTML = "";
    document.getElementById("taixe_bienso").innerHTML = "";
    //  document.getElementById("BtnChiDinh").disabled = true;
}

function choiceSearchByLaiXeOrHangXe() {
    if (currentValue == 1) {
        showInfoTaiXeBySdtOrMa();
    }
    else {
        showInfoHangXeBySdt();
    }

}

function showInfoTaiXeBySdtOrMa() {

    var thongtinTimkiem = document.getElementById("taixe_thongtin_timkiem").value;

    $.get("/ChiDinhTaiXe/GetLaiXeBySdtOrMa?SdtOrMa=" + thongtinTimkiem)
        .done(function (data) {

            if (data != null) {
                document.getElementById('TaiXeId').value = data.Id;
                document.getElementById("taixe_tentaixe").innerHTML = data.TenNguoiDung;
                document.getElementById("taixe_sdt").innerHTML = data.SDT;
                document.getElementById("taixe_code").innerHTML = data.Code;
                document.getElementById("taixe_hangxe").innerHTML = data.HangXe;
                document.getElementById("taixe_bienso").innerHTML = data.BienSoXe;

            }
            else {
                document.getElementById('TaiXeId').value = null;
                document.getElementById("taixe_tentaixe").innerHTML = "Không có trong hệ thống";
                document.getElementById("taixe_sdt").innerHTML = "";
                document.getElementById("taixe_code").innerHTML = "";
                document.getElementById("taixe_hangxe").innerHTML = "";
                document.getElementById("taixe_bienso").innerHTML = "";
            }

            checkCondition();
            showHideGroupTaiXeHangXeInfo();
        });

}

function showInfoHangXeBySdt() {

    var thongtinTimkiem = document.getElementById("taixe_thongtin_timkiem").value;

    $.get("/ChiDinhTaiXe/GetHangXeBySdt?dienThoaiHangXe=" + thongtinTimkiem)
        .done(function (data) {

            if (data != null) {
                document.getElementById('HangXeId').value = data.HangXeId;
                document.getElementById("hangxe_ten").innerHTML = data.HangXe;
                document.getElementById("hangxe_khuvuc").innerHTML = data.KhuVuc;
                document.getElementById("hangxe_sdt").innerHTML = data.SDT;
            } else {
                document.getElementById('HangXeId').value = "";
                document.getElementById("hangxe_ten").innerHTML = "";
                document.getElementById("hangxe_khuvuc").innerHTML = "";
                document.getElementById("hangxe_sdt").innerHTML = "";
                document.getElementById("hangxe_giamocua").innerHTML = "";
            }

            checkCondition();
            showHideGroupTaiXeHangXeInfo();
        });

}

function checkCondition() {
    var taixeId = document.getElementById('TaiXeId').value;
    var hangxeId = document.getElementById('HangXeId').value;
    log('checkCondition taixeId:' + taixeId + ' hangxeId' + hangxeId);

    if (taixeId || hangxeId) {
        document.getElementById("BtnChiDinh").disabled = false;
    } else {
        document.getElementById("BtnChiDinh").disabled = true;
    }
}

function ChiDinhTaiXeUpdateDatXeByTaiXeOnMap(datXeId, sdt, laiXeId) {

    $("#windowConfirm").data("kendoWindow").center().open();
    $("#confirmYes").unbind();

    // button Yes Click
    $('#confirmYes').click(function () {
        var ghiChu = document.getElementById("txtGhiChuChiDinhTaiXe").value;
        var url = "/ChiDinhTaiXe/ChiDinhTaiXeUpdateDatXeByTaiXe";

        $.post(url, { datXeId: datXeId, sdt: sdt, laiXeId: laiXeId, ghiChu: ghiChu }, function (data) {
            alert(data.message);
            // dong cua so chi dinh tai xe
            $("#ChiDinhTaiXeWindow").data("kendoWindow").close();

           // log("PUBLISH CHIDINHTAIXE ON MAP KHACHHANG ->TAIXE");
            pubnub.publish({
                channel: PASGO_ALERT_SHOWNOTE,
                message: { "type":"chidinhtaixe","DatXeId": datXeId },

            });

            clearTaiXeInfo_ChiDinhTaiXe();

        });

        // end to do
        $("#windowConfirm").data("kendoWindow").close();
    });

    // button NO
    $('#confirmNo').click(function () {
        $("#windowConfirm").data("kendoWindow").close();
    });


}

function ChiDinhTaiXeUpdateDatXeByHangXeOnMap(datXeId, sdt, hangXeId, soHieuXe) {
    $("#windowConfirm").data("kendoWindow").center().open();
    $("#confirmYes").unbind();

    // button Yes Click
    $('#confirmYes').click(function () {
        var ghiChu = document.getElementById("txtGhiChuChiDinhTaiXe").value;
        var url = "/ChiDinhTaiXe/ChiDinhTaiXeUpdateDatXeByHangXe";

        //DatXeId bien toan cuc duoc lay tren Url
        $.post(url, { datXeId: datXeId, sdt: sdt, hangXeId: hangXeId, soHieuXe: soHieuXe, ghiChu: ghiChu }, function (data) {
            alert(data.message);
            // dong cua so chi dinh tai xe
            $("#ChiDinhTaiXeWindow").data("kendoWindow").close();
           
            pubnub.publish({
                channel: PASGO_ALERT_SHOWNOTE,
                message: { "type": "chidinhtaixe", "DatXeId": datXeId },

            });

          //  removeHangXeInfoDetail();
        });

        // end to do
        $("#windowConfirm").data("kendoWindow").close();
    });

    // button NO
    $('#confirmNo').click(function () {
        $("#windowConfirm").data("kendoWindow").close();
    });
}

function clearDataInput() {
    //log('clearDataInput');
    clearInfoKhachHang();
    clearTaiXeInfo_ChiDinhTaiXe();
    removeHangXeInfoDetail();
    removeDoiTacInfoDetail();
}

function clearTaiXeInfo_ChiDinhTaiXe() {
  //  log('clearTaiXeInfo_ChiDinhTaiXe');
    document.getElementById('TaiXeId').value = null;
    document.getElementById('taixe_thongtin_timkiem').value = "";
    document.getElementById("taixe_tentaixe").innerHTML = "";
    document.getElementById("taixe_sdt").innerHTML = "";
    document.getElementById("taixe_code").innerHTML = "";
    document.getElementById("taixe_hangxe").innerHTML = "";
    document.getElementById("taixe_bienso").innerHTML = "";
    // document.getElementById("BtnChiDinh").disabled = true;

}

//function removeHangXeInfoDetail() {
//    //document.getElementById('taixe_thongtin_timkiem').value = "";
//    document.getElementById('HangXeId').value = null;
//    document.getElementById("hangxe_ten").innerHTML = "";
//    document.getElementById("hangxe_khuvuc").innerHTML = "";
//    document.getElementById("hangxe_sdt").innerHTML = "";
//    document.getElementById("hangxe_sohieu_taixe").value = "";
//    document.getElementById("hangxe_giamocua").innerHTML = "";
//    // document.getElementById("BtnChiDinh").disabled = true;
//}

function disableInputKhachHang() {
  //  log('disableInputKhachHang');
    document.getElementById('khachhang_sdt_timkiem').disabled = true;
    document.getElementById('khachhang_diachidon').disabled = true;
    document.getElementById('khachhang_diachiden').disabled = true;
    //document.getElementById('khachhang_makhuyenmai').disabled = true;
    document.getElementById('btnKhachHangSearch').disabled = true;
   // document.getElementById('btnCheckMaKhuyenMai').disabled = true;


}

function enableInputKhachHang() {
    //log('enableInputKhachHang');
    document.getElementById('khachhang_sdt_timkiem').disabled = false;
    document.getElementById('khachhang_diachidon').disabled = false;
    document.getElementById('khachhang_diachiden').disabled = false;
    document.getElementById('khachhang_makhuyenmai').disabled = false;
    document.getElementById('btnKhachHangSearch').disabled = false;
    document.getElementById('btnCheckMaKhuyenMai').disabled = false;

    //$('#khachhang_sdt_timkiem').css('background-color', 'white');
    //$('#khachhang_diachidon').css('background-color', 'white');
    //$('#khachhang_diachiden').css('background-color', 'white');
    //$('#khachhang_makhuyenmai').css('background-color', 'white');
}

function showInfoKhachHang(data) {
  //  log('showInfoKhachHang');
  //  log(data);

    if (data != null) {
        document.getElementById('KhachHangId').value = data.KhachHangId;
        document.getElementById("khachhang_tenkhachhang").innerHTML = data.TenKhachHang;
        document.getElementById("khachhang_sdt").innerHTML = data.SoDienThoai;
        document.getElementById("khachhang_loaidoituong").innerHTML = "Khách hàng";        
        document.getElementById("khachhang_nhomxe").innerHTML = data.NhomXe;

        document.getElementById('khachhang_sdt_timkiem').value = data.SoDienThoai;
        document.getElementById('khachhang_diachidon').value = data.DiaChiDon;
        document.getElementById('khachhang_diachiden').value = data.DiaChiDen;
        document.getElementById('khachhang_makhuyenmai').value = data.MaGiamGia;
        document.getElementById('khachhang_tieude_khuyenmai').innerHTML = data.TieuDeKhuyenMai;
        document.getElementById('khachhang_tienkhuyenmai').innerHTML = data.TienKhuyenMai;        
        document.getElementById('khachhang_lbl_makhuyenmai').innerHTML = data.MaGiamGia;


    }
}

function clearInfoKhachHang() {
   // log('clearInfoKhachHang');

    document.getElementById('KhachHangId').value = null;
    document.getElementById("khachhang_tenkhachhang").innerHTML = "";
    document.getElementById("khachhang_sdt").innerHTML = "";
    document.getElementById("khachhang_loaidoituong").innerHTML = "";
    document.getElementById("khachhang_nhomxe").innerHTML = "";

    document.getElementById('khachhang_sdt_timkiem').value = "";
    document.getElementById('khachhang_diachidon').value = "";
    document.getElementById('khachhang_diachiden').value = "";
    document.getElementById('khachhang_makhuyenmai').value = "";
    document.getElementById("txtGhiChuChiDinhTaiXe").value = "";

    document.getElementById('khachhang_makhuyenmai').value = "";
    document.getElementById('khachhang_tieude_khuyenmai').innerHTML = "";
    document.getElementById('khachhang_tienkhuyenmai').innerHTML = "";
    document.getElementById('khachhang_lbl_makhuyenmai').innerHTML = "";

}

function showInfoKhachHangBySDT() {

    var sodienthoai = document.getElementById("khachhang_sdt_timkiem").value;

    $.get("/ChiDinhTaiXe/GetInfoKhachHangBySDT?sdt=" + sodienthoai)
        .done(function (data) {

            if (data != null) {
                document.getElementById('KhachHangId').value = data.Id;
                document.getElementById("khachhang_tenkhachhang").innerHTML = data.TenNguoiDung;
                document.getElementById("khachhang_sdt").innerHTML = sodienthoai;
                document.getElementById("khachhang_loaidoituong").innerHTML = data.TenLoaiDoiTuong;

            }
            else {
                document.getElementById('KhachHangId').value = null;
                document.getElementById("khachhang_tenkhachhang").innerHTML = "Không có trong hệ thống";
                document.getElementById("khachhang_sdt").innerHTML = "";
                document.getElementById("khachhang_loaidoituong").innerHTML = "";
            }

            checkCondition();

        });
}

function removeKhachhangInfoDetail() {
    document.getElementById('KhachHangId').value = "";
    document.getElementById("khachhang_tenkhachhang").innerHTML = "";
    document.getElementById("khachhang_sdt").innerHTML = "";
    document.getElementById("khachhang_loaidoituong").innerHTML = "";
    
    document.getElementById('khachhang_tieude_khuyenmai').innerHTML = "";
    document.getElementById('khachhang_tienkhuyenmai').innerHTML = "";
    document.getElementById('khachhang_lbl_makhuyenmai').innerHTML = "";

    // document.getElementById("BtnChiDinh").disabled = true;
}

function insertChiDinhProcess() {

    var taixeId = document.getElementById('TaiXeId').value;
    var hangXeId = document.getElementById('HangXeId').value;
    var soHieuXe = document.getElementById("hangxe_sohieu_taixe").value;
    

    if (taixeId && hangXeId) {
        alert('Thông tin tài xế hoặc hãng xe không hợp lệ');
        return;
    }

    var khachHangId = document.getElementById('KhachHangId').value;
    var khachhangSdt = document.getElementById('khachhang_sdt_timkiem').value;
    var khachhangDiachidon = document.getElementById('khachhang_diachidon').value;
    var khachhangDiachiden = document.getElementById('khachhang_diachiden').value;
    var khachhangMakhuyenmai = document.getElementById('khachhang_makhuyenmai').value;
    var nhomCNDoiTacId = document.getElementById('NhomCNDoiTacId').value;

    var url = "/Map/ChiDinhTaiXeChoKhachHang";

    if (nhomCNDoiTacId) {
        url = "/Map/ChiDinhTaiXeChoKhachHangWithPartner";
    }

    if (khachhangMakhuyenmai != '') {
        $.get("/ChiDinhTaiXe/GetThongTinKhuyenMai?maKhuyenMai=" + khachhangMakhuyenmai)
            .done(function (data) {
                if (data.TieuDeKhuyenMai) {
                    khachhangMakhuyenmai = data.MaKhuyenMai;
                    $("#windowConfirm").data("kendoWindow").center().open();
                } else {
                    document.getElementById('khachhang_tieude_khuyenmai').innerHTML = "";
                    document.getElementById('khachhang_tienkhuyenmai').innerHTML = "";
                    document.getElementById('khachhang_lbl_makhuyenmai').innerHTML = 'Mã không hợp lệ';
                }
            });

    } else {
        $("#windowConfirm").data("kendoWindow").center().open();
    }

    $("#confirmYes").unbind();

    // button Yes Click
    $('#confirmYes').click(function () {
        
        $.post(url, { KhachHangId: khachHangId, SDT: khachhangSdt, DiaChiDon: khachhangDiachidon, DiaChiDen: khachhangDiachiden, MaKhuyenMai: khachhangMakhuyenmai, LaiXeId: taixeId, HangXeId: hangXeId, SoHieuXe: soHieuXe, RadioValue: currentValue,NhomCNDoiTacId:nhomCNDoiTacId}, function (data) {
            alert(data.message);

            // dong cua so chi dinh tai xe
            $("#ChiDinhTaiXeWindow").data("kendoWindow").close();
        });

        // dong cua so xac nhan
        $("#windowConfirm").data("kendoWindow").close();
    });

    // button NO
    $('#confirmNo').click(function () {
        $("#windowConfirm").data("kendoWindow").close();
    });
}

function clearVerifyCheckMaKhuyenMai() {
    document.getElementById("khachhang_lbl_makhuyenmai").innerHTML = "";
}

function checkMaKhuyenMai() {    
    var maGiamGia = document.getElementById('khachhang_makhuyenmai').value;
    $.get("/ChiDinhTaiXe/GetThongTinKhuyenMai?maKhuyenMai=" + maGiamGia)
       .done(function (data) {
           if (data.TieuDeKhuyenMai) {
               document.getElementById('khachhang_tieude_khuyenmai').innerHTML = data.TieuDeKhuyenMai;
               document.getElementById('khachhang_tienkhuyenmai').innerHTML = data.TienKhuyenMai;
               document.getElementById('khachhang_lbl_makhuyenmai').innerHTML = data.MaKhuyenMai;

           } else {                              
               document.getElementById('khachhang_tieude_khuyenmai').innerHTML = "";
               document.getElementById('khachhang_tienkhuyenmai').innerHTML = "";
               document.getElementById('khachhang_lbl_makhuyenmai').innerHTML = 'Mã không hợp lệ';
           }
       });
}

function showInfoChiDinhDatChoTruoc(data) {
    if (data != null) {
        document.getElementById('KhachHangId').value = data.KhachHangId;
        document.getElementById("khachhang_tenkhachhang").innerHTML = data.TenKhachHang;
        document.getElementById("khachhang_sdt").innerHTML = data.DienThoaiKhachHang;
        document.getElementById("khachhang_loaidoituong").innerHTML = "Khách hàng";

        document.getElementById('khachhang_sdt_timkiem').value = data.DienThoaiKhachHang;
        document.getElementById('khachhang_diachidon').value = data.DiaDiemDon;
        document.getElementById('khachhang_diachiden').value = data.DiaDiemDen;
        document.getElementById('khachhang_makhuyenmai').value = "";

    }
}

function showInfoChiDinhThongTinDoiTac(datXeId) {
    clearChiDinhPartnerInfo();

    $.get("/Map/GetThongTinDoiTac?datXeId=" + datXeId)
         .done(function (data) {
             if (data != null && data.TenDoiTac != '') {
                 //hien thi thong tin partner tren window chi dinh
                 $('#groupChiDinhPartner').show();

                 document.getElementById('doitac_tendoitac').innerHTML = data.TenDoiTac;
                 document.getElementById('doitac_tenchinhanh').innerHTML = data.TenChiNhanh;
                 document.getElementById('doitac_sodienthoai').innerHTML = data.SDT;
                 document.getElementById('doitac_diachi').innerHTML = data.DiaChi;
             } else { // an thong tin partner chi dinh
                 $('#groupChiDinhPartner').hide();
             }
         });
}

function clearChiDinhPartnerInfo() {
    document.getElementById('doitac_tendoitac').innerHTML = "";
    document.getElementById('doitac_tenchinhanh').innerHTML = "";
    document.getElementById('doitac_sodienthoai').innerHTML = "";
    document.getElementById('doitac_diachi').innerHTML = "";
}