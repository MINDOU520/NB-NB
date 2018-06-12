$(function () {

    // 页面加载渲染
    addEventListener("load", function () {
        setTimeout(hideURLbar, 0);
    }, false);
    function hideURLbar() {
        window.scrollTo(0, 1);
    }

    // 设置顶部天气栏和定位扫码区固定
    $(window).scroll(function () {
        // 当滚动距离大于60影藏
        if ($(window).scrollTop() > 60) {
            $(".head-top").hide();
            $(".header").css("top", "0")
                .css("position", "fixed");
            $(".main").css("margin-top", "130px");
        }
        // 滚动条小于等于60显示
        else {
            $(".head-top").show();
            $(".header").css("position", "relative");
            $(".main").css("margin-top", "0");
        }
    });

    // 调用天气并插入到天气栏
    $(".head-top > .container").append("<iframe name=\"weather_inc\" src=\"http:\/\/i.tianqi.com\/index.php?c=code&id=9\" width=\"75%\" height=\"60\" frameborder=\"0\" marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\" allowtransparency=\"true\"><\/iframe>");

    // 调用百度地图API功能
    var map = new BMap.Map("allmap");    // 创建Map实例
    map.centerAndZoom(new BMap.Point(116.022, 28.693), 16);    // 初始化地图,设置中心点坐标和地图级别
    // 添加地图类型控件
    map.addControl(new BMap.MapTypeControl({
        mapTypes: [
            BMAP_NORMAL_MAP,
            BMAP_HYBRID_MAP
        ]
    }));
    map.setCurrentCity("南昌");    // 设置地图显示的城市
    map.enableScrollWheelZoom(true);    // 开启鼠标滚轮缩放
    map.enableKeyboard();    // 开启键盘控制方向
    map.enableContinuousZoom();    // 开启连续缩放效果
    map.enableInertialDragging();    // 开启惯性拖拽效果

    // 点击.posi时要阻止冒泡，否则.map-box和.zzc是不显示的，因为冒泡了，会执行到下面的方法。
    function stopPropagation(e) {
        var ev = e || window.event;
        if (ev.stopPropagation) {
            ev.stopPropagation();
        }
        else if (window.event) {
            window.event.cancelBubble = true;    // 兼容IE
        }
    }

    // 点击定位按钮显示地图和遮罩层
    $(".posi").click(function (e) {
        $(".map-box").fadeIn(1000);
        $(".zzc").fadeIn(1000);
        stopPropagation(e);
        return false;
    });
    // 点击关闭按钮以及地图外其他区域隐藏地图和遮罩层
    $(".map-close").click(function (e) {
        $(".map-box").fadeOut(1000);
        $(".zzc").fadeOut(1000);
        stopPropagation(e);
        return false;
    });
    $(document).bind('click', function () {
        $(".map-box").fadeOut(1000);
        $(".zzc").fadeOut(1000);
    });
    // 点击地图阻止冒泡
    $(".map-box").click(function (e) {
        stopPropagation(e);
    });

    // 返回顶部
    $(window).scroll(function () {
        if ($(window).scrollTop() > 200) {
            $(".backtop").addClass("shang");    // 当页面滚动距离大于200时，添加.shang类，使之显示
        }
        else {
            $(".backtop").removeClass("shang");    // 当页面滚动距离小于200时，删除.shang类，使之隐藏
        }
    });
    $(".backtop").click(function () {
        $("html,body").animate({ scrollTop: 0 }, "slow");    // 点击backtop，回到顶部
    });
});

