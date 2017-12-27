$(function () {
    //gọi pupup
    $(".rm-btn-buynow").click(function () {
        $(".xingfa-modal-sm").modal("show");
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
    
});