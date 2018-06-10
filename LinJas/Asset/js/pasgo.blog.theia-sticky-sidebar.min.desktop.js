(function (d) {
    d.fn.theiaStickySidebar = function (c) {
        function h(f, c) { if (!0 === f.initialized) return !0; if (d("body").width() < f.minWidth) return !1; p(f, c); return !0 } function p(f, c) {
            f.initialized = !0; d("head").append(d('<style>.theiaStickySidebar:after {content: ""; display: table; clear: both;}</style>')); c.each(function () {
                function c() { b.fixedScrollTop = 0; b.sidebar.css({ "min-height": "1px" }); b.stickySidebar.css({ position: "static", width: "" }) } function q(a) {
                    var b = a.height(); a.children().each(function () {
                        b = Math.max(b,
                        d(this).height())
                    }); return b
                } var b = {}; b.sidebar = d(this); b.options = f || {}; b.container = d(b.options.containerSelector); 0 == b.container.size() && (b.container = b.sidebar.parent()); b.sidebar.css({ position: "relative", overflow: "visible", "-webkit-box-sizing": "border-box", "-moz-box-sizing": "border-box", "box-sizing": "border-box" }); b.stickySidebar = b.sidebar.find(".theiaStickySidebar"); 0 == b.stickySidebar.length && (b.sidebar.find("script").remove(), b.stickySidebar = d("<div>").addClass("theiaStickySidebar").append(b.sidebar.children()),
                b.sidebar.append(b.stickySidebar)); b.marginTop = parseInt(b.sidebar.css("margin-top")); b.marginBottom = parseInt(b.sidebar.css("margin-bottom")); b.paddingTop = parseInt(b.sidebar.css("padding-top")); b.paddingBottom = parseInt(b.sidebar.css("padding-bottom")); var l = b.stickySidebar.offset().top, h = b.stickySidebar.outerHeight(); b.stickySidebar.css("padding-top", 1); b.stickySidebar.css("padding-bottom", 1); l -= b.stickySidebar.offset().top; h = b.stickySidebar.outerHeight() - h - l; 0 == l ? (b.stickySidebar.css("padding-top",
                0), b.stickySidebarPaddingTop = 0) : b.stickySidebarPaddingTop = 1; 0 == h ? (b.stickySidebar.css("padding-bottom", 0), b.stickySidebarPaddingBottom = 0) : b.stickySidebarPaddingBottom = 1; b.previousScrollTop = null; b.fixedScrollTop = 0; c(); b.onScroll = function (a) {
                    if (a.stickySidebar.is(":visible")) if (d("body").width() < a.options.minWidth) c(); else if (a.sidebar.outerWidth(!0) + 50 > a.container.width()) c(); else {
                        var b = d(document).scrollTop(), k = "static"; if (b >= a.container.offset().top + (a.paddingTop + a.marginTop - a.options.additionalMarginTop)) {
                            var g =
                            a.paddingTop + a.marginTop + f.additionalMarginTop, h = a.paddingBottom + a.marginBottom + f.additionalMarginBottom, m = a.container.offset().top, e = a.container.offset().top + q(a.container), k = 0 + f.additionalMarginTop, g = a.stickySidebar.outerHeight() + g + h < d(window).height() ? k + a.stickySidebar.outerHeight() : d(window).height() - a.marginBottom - a.paddingBottom - f.additionalMarginBottom, m = m - b + a.paddingTop + a.marginTop, h = e - b - a.paddingBottom - a.marginBottom, e = a.stickySidebar.offset().top - b, l = a.previousScrollTop - b; "fixed" == a.stickySidebar.css("position") &&
                            "modern" == a.options.sidebarBehavior && (e += l); "legacy" == a.options.sidebarBehavior && (e = g - a.stickySidebar.outerHeight(), e = Math.max(e, g - a.stickySidebar.outerHeight())); e = 0 < l ? Math.min(e, k) : Math.max(e, g - a.stickySidebar.outerHeight()); e = Math.max(e, m); e = Math.min(e, h - a.stickySidebar.outerHeight()); k = (m = a.container.height() == a.stickySidebar.outerHeight()) || e != k ? m || e != g - a.stickySidebar.outerHeight() ? b + e - a.sidebar.offset().top - a.paddingTop <= f.additionalMarginTop ? "static" : "absolute" : "fixed" : "fixed"
                        } "fixed" == k ?
                        a.stickySidebar.css({ position: "fixed", width: a.sidebar.width(), top: e, left: a.sidebar.offset().left + parseInt(a.sidebar.css("padding-left")) }) : "absolute" == k ? (g = {}, "absolute" != a.stickySidebar.css("position") && (g.position = "absolute", g.top = b + e - a.sidebar.offset().top - a.stickySidebarPaddingTop - a.stickySidebarPaddingBottom), g.width = a.sidebar.width(), g.left = "", a.stickySidebar.css(g)) : "static" == k && c(); "static" != k && 1 == a.options.updateSidebarHeight && a.sidebar.css({
                            "min-height": a.stickySidebar.outerHeight() + a.stickySidebar.offset().top -
                            a.sidebar.offset().top + a.paddingBottom
                        }); a.previousScrollTop = b
                    }
                }; b.onScroll(b); d(document).scroll(function (a) { return function () { a.onScroll(a) } }(b)); d(window).resize(function (a) { return function () { a.stickySidebar.css({ position: "static" }); a.onScroll(a) } }(b))
            })
        } c = d.extend({ containerSelector: "", additionalMarginTop: 0, additionalMarginBottom: 0, updateSidebarHeight: !0, minWidth: 0, sidebarBehavior: "modern" }, c); c.additionalMarginTop = parseInt(c.additionalMarginTop) || 0; c.additionalMarginBottom = parseInt(c.additionalMarginBottom) ||
        0; (function (c, n) { h(c, n) || (d(document).scroll(function (c, f) { return function (b) { h(c, f) && d(this).unbind(b) } }(c, n)), d(window).resize(function (c, f) { return function (b) { h(c, f) && d(this).unbind(b) } }(c, n))) })(c, this)
    }
})(jQuery);
$(document).ready(function () {
    $('.wap-left,.wap-right')
        .theiaStickySidebar({
            additionalMarginTop: 30
        });
});