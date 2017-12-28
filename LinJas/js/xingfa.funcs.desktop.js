// log
function log(message) {
    console.log(message);
}

// debounce
function debounce(fn, delay) {
    var timer = null;
    return function () {
        var context = this, args = arguments;
        clearTimeout(timer);
        timer = setTimeout(function () {
            fn.apply(context, args);
        }, delay);
    };
}

// throttle
function throttle(fn, threshhold, scope) {
    threshhold || (threshhold = 250);
    var last,
        deferTimer;
    return function () {
        var context = scope || this;

        var now = +new Date,
            args = arguments;
        if (last && now < last + threshhold) {
            // hold on to it
            clearTimeout(deferTimer);
            deferTimer = setTimeout(function () {
                last = now;
                fn.apply(context, args);
            }, threshhold);
        } else {
            last = now;
            fn.apply(context, args);
        }
    };
}

// Remove Utf8
function removeUtf8(n) {
    return n = n.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a"), n = n.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e"), n = n.replace(/ì|í|ị|ỉ|ĩ/g, "i"), n = n.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o"), n = n.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u"), n = n.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y"), n = n.replace(/đ/g, "d"), n = n.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A"), n = n.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E"), n = n.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I"), n = n.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O"), n = n.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U"), n = n.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y"), n.replace(/Đ/g, "D");
}

// ScrollFunction
function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 100) {
        document.getElementById("myBtn").style.display = "block";
    } else {
        document.getElementById("myBtn").style.display = "none";
    }
}

// httpGet
function httpGet(url, params, loading) {
    log("url: " + url);
    log("params:" + JSON.stringify(params));

    if (navigator.onLine) {
        loading = loading != null ? loading : true;

        return new Promise(
            function (resolve, reject) {
                $.ajax({
                    url: url,
                    type: "get",
                    dateType: "json",
                    global: loading,
                    cache: false,
                    data: params,
                    beforeSend: function () {
                        log(loading);

                        if (loading === true) {
                            $(".tree-dot").show();
                        }
                    },
                    complete: function () {
                        $(".tree-dot").hide();
                    },
                    success: function (result) {
                        if (result
                            && result.MaLoi < 0) {
                            log(result.ThongDiep);
                        } else {
                            resolve(result);
                        }
                    },
                    error: function (error) {
                        $(".tree-dot").hide();

                        log(error);
                    }
                });
            });
    } else {
        log("Lỗi không thể kết nối đến máy chủ. Vui lòng kiểm tra kết nối mạng và Nhấn vào nút Refresh trên trình duyệt!");
    }

    return null;
}

// httpPost
function httpPost(url, params, loading) {
    log("url: " + url);
    log("params:" + JSON.stringify(params));

    if (navigator.onLine) {
        loading = loading != null ? loading : true;

        return new Promise(
            function (resolve, reject) {
                $.ajax({
                    url: url,
                    type: "post",
                    dateType: "json",
                    global: loading,
                    cache: false,
                    data: params,
                    beforeSend: function () {
                        log(loading);

                        if (loading === true) {
                            $(".tree-dot").show();    
                        }
                    },
                    complete: function () {
                        $(".tree-dot").hide();
                    },
                    success: function (result) {
                        if (result
                            && result.MaLoi < 0) {
                            log(result.ThongDiep);
                        } else {
                            resolve(result);
                        }
                    },
                    error: function (error) {
                        $(".tree-dot").hide();

                        log(error);
                    }
                });
            });
    } else {
        log("Lỗi không thể kết nối đến máy chủ. Vui lòng kiểm tra kết nối mạng!");
    }

    return null;
}

// Toggle Search
function toggleSearch() {
    $("#rm-Keyword-suggestion").toggle();
}