$(function () {
    //gọi pupup
    $(".rm-btn-buynow").click(function () {
        $(".xingfa-modal-sm").modal("show");
    });
    //gọi lọc giá
    $("#rmprice").click(function () {
        $(".rmlist-price").toggle();
    });
    // Trở về đầu page

    window.onscroll = function () { scrollFunction() };
    function scrollFunction() {
        if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
            document.getElementById("myBtn").style.display = "block";
        } else {
            document.getElementById("myBtn").style.display = "none";
        }
    }
    $("#myBtn").click(function () {
        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0;
    });

    $(document).on("click", function (e) {
        // Đóng thanh search khi click ra ngoài
        if ($(e.target).closest("#rm-search-all").length <= 0
            && $(e.target).closest("#rm-Keyword-suggestion").length <= 0) {
            if ($("#rm-Keyword-suggestion").is(":visible")) {
                $("#rm-Keyword-suggestion").toggle();
            }
        }

        // Đóng Province khi click ra ngoài
        //if ($(e.target).closest(".rmprovince").length <= 0
        //    && $(e.target).closest(".rmlistprovince").length <= 0) {
        //    if ($(".rmlistprovince").is(":visible")) {
        //        toggleProvince();
        //    }
        //}

        // Đóng District khi click ra ngoài
        //if ($(e.target).closest(".rmdistrict").length <= 0
        //    && $(e.target).closest(".rmlistdistrict").length <= 0) {
        //    if ($(".rmlistdistrict").is(":visible")) {
        //        toggleDistrict();
        //    }
        //}

        // Đóng lọc giá khi click ra ngoài
        //if ($(e.target).closest("#rmprice").length <= 0
        //    && $(e.target).closest(".rmlist-price").length <= 0) {
        //    if ($(".rmlist-price").is(":visible")) {
        //        $(".rmlist-price").toggle();
        //    }
        //}
    });
});
