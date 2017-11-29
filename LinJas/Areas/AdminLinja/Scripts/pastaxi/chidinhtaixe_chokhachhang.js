


var DatXeId;  //  bien toan cuc
var DoiTuongType;
var ActionInsertOrUpdate = "Insert";

function showEdit(e) {
    e.preventDefault();
    DatXeId = this.dataItem($(e.currentTarget).closest("tr")).DatXeId;

    //Kiem tra xem  da insert du lieu TRONG BANG [XACNHANCHUYENTIEN] VA DAXACNHAN =TRUE -> thi khong cho phep chinh sua
    $.get("/ChiDinhTaiXe/XacNhanChuyenTienFindByDatXeId?datXeId=" + DatXeId)
      .done(function (data) {
          if (data && data.length > 0) {
              alert("Khách hàng đã được xác nhận chuyển tiền rồi. Nên bản ghi này không được phép chỉnh sửa");
              return;
          }
          else {
              allowEditItemInfo(DatXeId);
          }
      });
}

function allowEditItemInfo(datXeId) {
    document.getElementById("BtnChiDinh").disabled = false;
    disableInputKhachHang();

    var win = $("#ChiDinhTaiXeWindow").data("kendoWindow");
    if (!$("#ChiDinhTaiXeWindow").is(":visible")) {
        win.center().open();
    }

    // thay doi tieu de nut bam
    document.getElementById("titleButtonOk").innerHTML = "Cập nhật";

   // log('allow edit item info datxeId='+datXeId);

    var url = "/ChiDinhTaiXe/ChiDinhTaiXeGetByDatXeId";

    $.post(url, { datXeId: datXeId }, function (data) {

           // log(data);

            if (data != null && data.HangXeId !='') {

                DatXeId = data.DatXeId;  // luu vao bien toan cuc
                DoiTuongType = data.DoiTuongType;  // {hangxe,laixe}

                // thong tin khach hang
                document.getElementById('khachhang_sdt_timkiem').value = data.DienThoaiKH;
                document.getElementById('khachhang_diachidon').value = data.DiaChiDon;
                document.getElementById('khachhang_diachiden').value = data.DiaChiDen;
                document.getElementById('khachhang_makhuyenmai').value = data.MaGiamGia;

                document.getElementById("khachhang_tenkhachhang").innerHTML = "";
                document.getElementById("khachhang_sdt").innerHTML = data.DienThoaiKH;
                document.getElementById("khachhang_loaidoituong").innerHTML = "Khách hàng";

                // thong tin tai xe
                if (DoiTuongType == "laixe") {
                    $('#rdTaiXe').prop('checked', true);
                    currentValue = 1;
                    //hien thi tieu de
                    document.getElementById('TaiXeId').value = data.DoiTuongId;
                    document.getElementById('taixe_thongtin_timkiem').value = data.DienThoai;
                    document.getElementById("taixe_tentaixe").innerHTML = data.Ten;
                    document.getElementById("taixe_sdt").innerHTML = data.DienThoai;
                    document.getElementById("taixe_code").innerHTML = data.DinhDanh;
                    document.getElementById("taixe_hangxe").innerHTML = data.Ten;
                    document.getElementById("taixe_bienso").innerHTML = data.BienSo_SoHieu;
                } else { // thong tin hang
                    $('#rdHangXe').prop('checked', true);
                    currentValue = 2;
                    //hien thi tieu de
                    document.getElementById('HangXeId').value = data.DoiTuongId;
                    document.getElementById('taixe_thongtin_timkiem').value = data.DienThoai;
                    document.getElementById("hangxe_ten").innerHTML = data.Ten;
                    document.getElementById("hangxe_khuvuc").innerHTML = data.DinhDanh;
                    document.getElementById("hangxe_sdt").innerHTML = data.DienThoai;
                    document.getElementById("hangxe_sohieu_taixe").value = data.BienSo_SoHieu;
                }

                showHideGroupTaiXeHangXeInfo(); // hien thi thong tin tai xe? hang xe? dua vao thong tin currentValue
            }

        });
}

function showWindowAddNew() {
    enableInputKhachHang();

    var win = $("#ChiDinhTaiXeWindow").data("kendoWindow");
    if (!$("#ChiDinhTaiXeWindow").is(":visible")) {
        win.center().open();
    }
    // thay doi tieu de nut bam
    document.getElementById("titleButtonOk").innerHTML = "Thêm mới";    
    ActionInsertOrUpdate = "Insert";
}

function removeKhachhangInfoDetail() {
    document.getElementById('KhachHangId').value = null;
    document.getElementById("khachhang_tenkhachhang").innerHTML = "";
    document.getElementById("khachhang_sdt").innerHTML = "";
    document.getElementById("khachhang_loaidoituong").innerHTML = "";
    document.getElementById("lblMaKhuyenMai").innerHTML = "";
    // document.getElementById("BtnChiDinh").disabled = true;
}

function disableRadio() {
    document.getElementById("rdTaiXe").disabled = true;
    document.getElementById("rdHangXe").disabled = true;
}

function clearKhachHangInfo_ChiDinhTaiXe() {
    log('clearKhachHangInfo_ChiDinhTaiXe');

    document.getElementById('KhachHangId').value = null;
    document.getElementById('khachhang_sdt_timkiem').value = "";
    document.getElementById('khachhang_diachidon').value = "";
    document.getElementById('khachhang_diachiden').value = "";
    document.getElementById('khachhang_makhuyenmai').value = "";

    document.getElementById("khachhang_tenkhachhang").innerHTML = "";
    document.getElementById("khachhang_sdt").innerHTML = "";
    document.getElementById("khachhang_loaidoituong").innerHTML = "";
    document.getElementById("khachhang_diachidon").innerHTML = "";
    document.getElementById("khachhang_diachiden").innerHTML = "";
    document.getElementById("khachhang_makhuyenmai").innerHTML = "";
    document.getElementById("BtnChiDinh").disabled = true;

    document.getElementById("lblMaKhuyenMai").innerHTML = "";

}

function removeTaiXeInfoDetail() {
    log('removeTaiXeInfoDetail');
    document.getElementById('TaiXeId').value = null;
    document.getElementById("taixe_tentaixe").innerHTML = "";
    document.getElementById("taixe_sdt").innerHTML = "";
    document.getElementById("taixe_code").innerHTML = "";
    document.getElementById("taixe_hangxe").innerHTML = "";
    document.getElementById("taixe_bienso").innerHTML = "";
    //  document.getElementById("BtnChiDinh").disabled = true;
}

function clearTaiXeInfo_ChiDinhTaiXe() {
    log('clearTaiXeInfo_ChiDinhTaiXe');
    document.getElementById('TaiXeId').value = null;
    document.getElementById('taixe_thongtin_timkiem').value = "";
    document.getElementById("taixe_tentaixe").innerHTML = "";
    document.getElementById("taixe_sdt").innerHTML = "";
    document.getElementById("taixe_code").innerHTML = "";
    document.getElementById("taixe_hangxe").innerHTML = "";
    document.getElementById("taixe_bienso").innerHTML = "";
    //  document.getElementById("BtnChiDinh").disabled = true;

}

function removeHangXeInfoDetail() {
    //document.getElementById('taixe_thongtin_timkiem').value = "";
    log('removeHangXeInfoDetail');
    document.getElementById('HangXeId').value = "";
    document.getElementById("hangxe_ten").innerHTML = "";
    document.getElementById("hangxe_khuvuc").innerHTML = "";
    document.getElementById("hangxe_sdt").innerHTML = "";
    document.getElementById("hangxe_sohieu_taixe").value = "";

    //  document.getElementById("BtnChiDinh").disabled = true;
}

function clearDataInput() {
    log('clearDataInput');
    clearKhachHangInfo_ChiDinhTaiXe();
    clearTaiXeInfo_ChiDinhTaiXe();
    removeHangXeInfoDetail();
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

function showInfoTaiXeBySdtOrMa() {

    var thongtin_timkiem = document.getElementById("taixe_thongtin_timkiem").value;

    $.get("/ChiDinhTaiXe/GetLaiXeBySdtOrMa?SdtOrMa=" + thongtin_timkiem)
        .done(function (data) {

            if (data != null) {
                document.getElementById('TaiXeId').value = data.Id;
                document.getElementById("taixe_tentaixe").innerHTML = data.TenNguoiDung
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

    var thongtin_timkiem = document.getElementById("taixe_thongtin_timkiem").value;

    $.get("/ChiDinhTaiXe/GetHangXeBySdt?dienThoaiHangXe=" + thongtin_timkiem)
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
            }

            checkCondition();
            showHideGroupTaiXeHangXeInfo();
        });

}

function checkCondition() {
    var taixeId = document.getElementById('TaiXeId').value;
    var sodienthoai = document.getElementById("khachhang_sdt_timkiem").value;
    var hangxeId = document.getElementById('HangXeId').value;

    if ((taixeId || hangxeId) && sodienthoai) {
        document.getElementById("BtnChiDinh").disabled = false;
    } else {
        document.getElementById("BtnChiDinh").disabled = true;
    }
}

function insertChiDinhProcess() {

    var taixeId = document.getElementById('TaiXeId').value;
    var hangXeId = document.getElementById('HangXeId').value;
    var soHieuXe = document.getElementById("hangxe_sohieu_taixe").value;

    if (taixeId && hangXeId) {
        alert('Thông tin tài xế hoặc hãng xe không hợp lệ');
        return;
    }

    $("#windowConfirm").data("kendoWindow").center().open();
    $("#confirmYes").unbind();

    // button Yes Click
    $('#confirmYes').click(function () {
        //to do
        // Thong tin khach hang
        var khachHangId = document.getElementById('KhachHangId').value;
        var khachhangSdt = document.getElementById('khachhang_sdt_timkiem').value;
        var khachhangDiachidon = document.getElementById('khachhang_diachidon').value;
        var khachhangDiachiden = document.getElementById('khachhang_diachiden').value;
        var khachhangMakhuyenmai = document.getElementById('khachhang_makhuyenmai').value;

        var url = "/ChiDinhTaiXe/ChiDinhTaiXeChoKhachHang";

        $.post(url, { KhachHangId: khachHangId, SDT: khachhangSdt, DiaChiDon: khachhangDiachidon, DiaChiDen: khachhangDiachiden, MaKhuyenMai: khachhangMakhuyenmai, LaiXeId: taixeId, HangXeId: hangXeId, SoHieuXe: soHieuXe, RadioValue: currentValue }, function (data) {
            alert(data.message);

            // dong cua so chi dinh tai xe
            $("#ChiDinhTaiXeWindow").data("kendoWindow").close();
            // lay du lieu moi
            $("#grid").data("kendoGrid").dataSource.read();
            $("#grid").data("kendoGrid").refresh();

        });

        // end to do
        $("#windowConfirm").data("kendoWindow").close();
    });

    // button NO
    $('#confirmNo').click(function () {
        $("#windowConfirm").data("kendoWindow").close();
    });


}

function updateChiDinhProcess() {
    // Cap nhat  lai thong tin tai xe cho khach hang
    if (currentValue == 1) {
        var laiXeId = document.getElementById('TaiXeId').value;
        log('updateChiDinhProcess laixeid=' + laiXeId);
        $.get("/ChiDinhTaiXe/UpdateChiDinhTaiXeByTaiXe?datXeId=" + DatXeId + "&laiXeId=" + laiXeId) // datXeId la bien toan cuc duoc gan gia tri  sau khi an nut Edit roi
           .done(function (data) {
               alert(data.message);
               // lay du lieu moi
               $("#grid").data("kendoGrid").dataSource.read();
               $("#grid").data("kendoGrid").refresh();
               $("#ChiDinhTaiXeWindow").data("kendoWindow").close();

           });
    }
        // cap nhat lai thong tin hang xe cho khach hang 
    else {
        //public ActionResult UpdateChiDinhTaiXeByHangXe(string datXeId, string hangXeId, string soHieuXe)
        var hangXeId = document.getElementById('HangXeId').value;
        var soHieuXe = document.getElementById("hangxe_sohieu_taixe").value;
        //log('updateChiDinhProcess hangXeId=' + hangXeId + ' soHieuXe=' + soHieuXe);
        var url = "/ChiDinhTaiXe/UpdateChiDinhTaiXeByHangXe?datXeId=" + DatXeId + "&hangXeId=" + hangXeId + "&soHieuXe=" + soHieuXe;
        log('[updateChiDinhProcess->HangXe]: '+url);
        $.get(url) // datXeId la bien toan cuc duoc gan gia tri  sau khi an nut Edit roi
           .done(function (data) {
               alert(data.message);
               // lay du lieu moi
               $("#grid").data("kendoGrid").dataSource.read();
               $("#grid").data("kendoGrid").refresh();
               $("#ChiDinhTaiXeWindow").data("kendoWindow").close();

           });
    }

}

function ChiDinhTaiXeUpdateDatXeByTaiXe() {

    var laiXeId = document.getElementById('TaiXeId').value;
    var khachhangSdt = document.getElementById('khachhang_sdt_timkiem').value;

    $("#windowConfirm").data("kendoWindow").center().open();
    $("#confirmYes").unbind();

    // button Yes Click
    $('#confirmYes').click(function () {

        var url = "/ChiDinhTaiXe/ChiDinhTaiXeUpdateDatXeByTaiXe";
        log('ChiDinhTaiXeUpdateDatXeByTaiXe ');

        //DatXeId bien toan cuc duoc lay tren Url
        $.post(url, { datXeId: DatXeId, sdt: khachhangSdt, laiXeId: laiXeId }, function (data) {
            alert(data.message);

            // dong cua so chi dinh tai xe
            $("#ChiDinhTaiXeWindow").data("kendoWindow").close();
            // lay du lieu moi
            $("#grid").data("kendoGrid").dataSource.read();
            $("#grid").data("kendoGrid").refresh();

            enableInputKhachHang();
            DatXeId = null;

        });

        // end to do
        $("#windowConfirm").data("kendoWindow").close();
    });

    // button NO
    $('#confirmNo').click(function () {
        $("#windowConfirm").data("kendoWindow").close();
    });


}

function ChiDinhTaiXeUpdateDatXeByHangXe() {

    var hangXeId = document.getElementById('HangXeId').value;
    var soHieuXe = document.getElementById("hangxe_sohieu_taixe").value;
    var khachhangSdt = document.getElementById('khachhang_sdt_timkiem').value;

    $("#windowConfirm").data("kendoWindow").center().open();
    $("#confirmYes").unbind();

    // button Yes Click
    $('#confirmYes').click(function () {

        var url = "/ChiDinhTaiXe/ChiDinhTaiXeUpdateDatXeByHangXe";

        //DatXeId bien toan cuc duoc lay tren Url
        $.post(url, { datXeId: DatXeId, sdt: khachhangSdt, hangXeId: hangXeId, soHieuXe: soHieuXe }, function (data) {
            alert(data.message);

            // dong cua so chi dinh tai xe
            $("#ChiDinhTaiXeWindow").data("kendoWindow").close();
            // lay du lieu moi
            $("#grid").data("kendoGrid").dataSource.read();
            $("#grid").data("kendoGrid").refresh();

            enableInputKhachHang();
            DatXeId = null;

        });

        // end to do
        $("#windowConfirm").data("kendoWindow").close();
    });

    // button NO
    $('#confirmNo').click(function () {
        $("#windowConfirm").data("kendoWindow").close();
    });


}

// BUTTON : nut xu ly chi dinh -> cap nhat he  thong
function ChiDinhTaiXe() {
    var title = document.getElementById("titleButtonOk").innerHTML;
    // Truong hop user nhan nut ChinhSua tren GridView
    if (title == "Cập nhật") {
        updateChiDinhProcess();
    }
    else {
        // Trường hợp thông tin lấy từ trang khác  
        if (ActionInsertOrUpdate == "Update") {
            if (currentValue == 1) {    //Chi dinh tai xe - > cap nhat thong tin datxe cho lai xe 
                log('Chi dinh tai xe - > cap nhat thong tin datxe cho lai xe ');
                ChiDinhTaiXeUpdateDatXeByTaiXe();
            }
            else  // chi dinh tai xe -> cap nhat thong tin dat xe cho Hang xe
            {
                log('chi dinh tai xe -> cap nhat thong tin dat xe cho Hang xe');
                ChiDinhTaiXeUpdateDatXeByHangXe();
            }

            PublishAlertChiDinhTaiXe(DatXeId); // duoc khai bao trong pubnub_chidinhtaixe.js

        }
        else  //truong hợp thêm mới bản ghi tren Tool bar
        {
            insertChiDinhProcess();
        }
    }

}

function loadData() {

    log('chi dinh tai xe cho khach hang loaddata');

    return {
        TuNgay: $('#TuNgay').val(),
        DenNgay: $('#DenNgay').val(),
    };
}

// INIT DOCUMENT
$(document).ready(function () {
    $("#btnTimKiem").click(function (e) {
        $("#grid").data("kendoGrid").dataSource.read();
        $("#grid").data("kendoGrid").refresh();
        e.preventDefault();
    });

    choiceActionProcess();
});

var currentValue = 1;

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

    } else {  // hien thi hang xe
        $("#groupThongTinTaiXe").hide();
        $("#groupHangXeInfo").show();
        $("#groupInputSoHieuTaiXe").show();
        removeTaiXeInfoDetail();
    }
}

function choiceSearchByLaiXeOrHangXe() {
    if (currentValue == 1) {
        showInfoTaiXeBySdtOrMa();
    }
    else {
        showInfoHangXeBySdt();
    }

}

function getURLParameters(paramName) {
    var sURL = window.document.URL.toString();
    if (sURL.indexOf("?") > 0) {
        var arrParams = sURL.split("?");
        var arrURLParams = arrParams[1].split("&");
        var arrParamNames = new Array(arrURLParams.length);
        var arrParamValues = new Array(arrURLParams.length);
        var i = 0;
        for (i = 0; i < arrURLParams.length; i++) {
            var sParam = arrURLParams[i].split("=");
            arrParamNames[i] = sParam[0];
            if (sParam[1] != "")
                arrParamValues[i] = unescape(sParam[1]);
            else
                arrParamValues[i] = "No Value";
        }

        for (i = 0; i < arrURLParams.length; i++) {
            if (arrParamNames[i] == paramName) {
                //alert("Param:"+arrParamValues[i]);
                return arrParamValues[i];
            }
        }
        return "No Parameters Found";
    }

}

function choiceActionProcess() {

    DatXeId = getURLParameters("datxeid");
    var sdt = getURLParameters("sdt");
    if (DatXeId != null) {

        showWindowAddNew();

        var diachidon = getURLParameters("don");
        var diachiden = getURLParameters("den");
        var khachhangid = getURLParameters("khachhangid");
        var tenkhachhang = getURLParameters("tenkhachhang");

        // hien thi thong tin textbox
        document.getElementById('KhachHangId').value = khachhangid;
        document.getElementById('khachhang_sdt_timkiem').value = sdt;
        document.getElementById('khachhang_diachidon').value = diachidon;
        document.getElementById('khachhang_diachiden').value = diachiden;
        document.getElementById('khachhang_makhuyenmai').value = "";

        // hien thi thong tin chi tiet
        document.getElementById("khachhang_tenkhachhang").innerHTML = tenkhachhang;
        document.getElementById("khachhang_sdt").innerHTML = sdt;
        document.getElementById("khachhang_loaidoituong").innerHTML = "Khách hàng";

        $("#groupThongTinKhachHang").show();

        ActionInsertOrUpdate = "Update"; // Co trang thai de phan biet update/Insert database

        // chi hien thi khong cho phep nhap thong tin khach hang
        disableInputKhachHang();
    }

}

function disableInputKhachHang() {
    //log('disableInputKhachHang');
    document.getElementById('khachhang_sdt_timkiem').disabled = true;
    document.getElementById('khachhang_diachidon').disabled = true;
    document.getElementById('khachhang_diachiden').disabled = true;
    document.getElementById('khachhang_makhuyenmai').disabled = true;

    $('#khachhang_sdt_timkiem').css('background-color', 'aliceblue');
    $('#khachhang_diachidon').css('background-color', 'aliceblue');
    $('#khachhang_diachiden').css('background-color', 'aliceblue');
    $('#khachhang_makhuyenmai').css('background-color', 'aliceblue');

}

function enableInputKhachHang() {
    log('enableInputKhachHang');
    document.getElementById('khachhang_sdt_timkiem').disabled = false;
    document.getElementById('khachhang_diachidon').disabled = false;
    document.getElementById('khachhang_diachiden').disabled = false;
    document.getElementById('khachhang_makhuyenmai').disabled = false;

    $('#khachhang_sdt_timkiem').css('background-color', 'white');
    $('#khachhang_diachidon').css('background-color', 'white');
    $('#khachhang_diachiden').css('background-color', 'white');
    $('#khachhang_makhuyenmai').css('background-color', 'white');
}

function CheckXacNhanChuyenTienExist(datXeId) {

    var ketqua = false;
    // neu ton tai ban ghi thi tra ve true
    $.get("/ChiDinhTaiXe/XacNhanChuyenTienFindByDatXeId?datXeId=" + datXeId)
        .done(function (data) {
            log('CheckXacNhanChuyenTienExist');
            log(data);
            log('data lenght =' + data.length);

            if (data && data.length > 0) {
                log('return true');
                ketqua = true;
            }

        });
    return ketqua;
}

function ChiDinhTaiXeDeleteByDatXeId(datXeId) {
    // neu ton tai ban ghi thi tra ve true
    $.get("/ChiDinhTaiXe/ChiDinhTaiXeDeleteByDatXeId?datXeId=" + datXeId)
        .done(function (data) {
            log(data.message);
            return data.message;
        });

}

function deleteItemClick(e) {
    e.preventDefault();
    DatXeId = this.dataItem($(e.currentTarget).closest("tr")).DatXeId;

    //Kiem tra xem  da insert du lieu TRONG BANG [XACNHANCHUYENTIEN] VA DAXACNHAN =TRUE -> thi khong cho phep chinh sua
    $.get("/ChiDinhTaiXe/XacNhanChuyenTienFindByDatXeId?datXeId=" + DatXeId)
      .done(function (data) {
          if (data && data.length > 0) {
              alert("Khách hàng đã được xác nhận chuyển tiền rồi. Nên bản ghi này không được phép xóa");
              return;
          }
          else {
              allowDeleteItem();
          }
      });
}

function allowDeleteItem() {
    $("#windowConfirm").data("kendoWindow").center().open();
    $("#confirmYes").unbind();

    // button Yes Click
    $('#confirmYes').click(function () {
        // Xu ly thao tac xoa
        ChiDinhTaiXeDeleteByDatXeId(DatXeId);
        alert('Đã xóa dữ liệu thành công');
        //lam moi du lieu
        $("#grid").data("kendoGrid").dataSource.read();
        $("#grid").data("kendoGrid").refresh();

        // end to do
        $("#windowConfirm").data("kendoWindow").close();
    });

    // button NO
    $('#confirmNo').click(function () {
        $("#windowConfirm").data("kendoWindow").close();
    });
}

function checkMaKhuyenMai() {

    var sdt = document.getElementById('khachhang_sdt_timkiem').value;
    var maGiamGia = document.getElementById('khachhang_makhuyenmai').value;
    $.get("/ChiDinhTaiXe/verifyMaGiamGia?sdt=" + sdt + "&maGiamGia=" + maGiamGia)
       .done(function (data) {
           // 0: Ma hop le , 1: Khong hop le
           if (data && data.Result == 0) {
               // alert('Mã khuyến mại & Đối tượng được hưởng khuyến mại hợp lệ');
               //lblMaKhuyenMai
               document.getElementById("lblMaKhuyenMai").innerHTML = 'Mã hợp lệ';

           } else {
               //alert('Mã khuyến mại không hợp lệ hoặc đối tượng này không được hưởng khuyến mại');
               document.getElementById("lblMaKhuyenMai").innerHTML = 'Mã không hợp lệ hoặc khách hàng trên không thuộc diện được hưởng khuyến mại';
           }

       });

}

function clearVerifyCheckMaKhuyenMai() {
    document.getElementById("lblMaKhuyenMai").innerHTML = "";
}

