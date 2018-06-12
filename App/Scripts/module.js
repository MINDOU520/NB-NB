$(function () {

    // 图片悬浮鼠标经过放大
    var $picwidth = $(".pic").width();
    var $pichight = $(".pic").height();
    var $picwidth2 = $picwidth + 18;
    var $pichight2 = $pichight + 12;

    $(".pic").hover(function () {
        $(this).stop().animate({ height: $pichight2, width: $picwidth2, left: "-9px", top: "-6px" }, 400);
    }, function () {
        $(this).stop().animate({ height: $pichight, width: $picwidth, left: "0px", top: "0px" }, 400);
    });

    // 教练框鼠标经过显示
    var $coachli = $("#coachList li");
    $coachli.mouseover(function () {
        $coachli.removeClass("active");
        $(this).addClass("active");
    });

    // 点击.coach-list时要阻止冒泡，否则.coach-info和.zzc是不显示的，因为冒泡了，会执行到下面的方法。
    function stopPropagation(e) {
        var ev = e || window.event;
        if (ev.stopPropagation) {
            ev.stopPropagation();
        }
        else if (window.event) {
            window.event.cancelBubble = true;    // 兼容IE
        }
    }

    // 点击按钮查看教练信息以及显示遮罩层
    $(".coach-list li a").click(function (e) {
        $(".coachinfo").fadeIn(1000);
        $(".zzc").fadeIn(1000);
        stopPropagation(e);
        return false;
    });
    // 点击关闭按钮和遮罩层影藏教练信息
    $(".coach-info-close").click(function (e) {
        $(".coachinfo").fadeOut(1000);
        $(".zzc").fadeOut(1000);
        stopPropagation(e);
        return false;
    });
    $(document).bind('click', function () {
        $(".coachinfo").fadeOut(1000);
        $(".zzc").fadeOut(1000);
    });
    // 点击信息阻止冒泡，不执行
    $(".coachinfo").click(function (e) {
        stopPropagation(e);
    });
    // 申请成为篮球教练弹出框
    $(".coach-info p span").click(function () {
        alert("抱歉！此功能暂未开放！我们将在后续完善，敬请期待！");
    });

    // 首页底部滚动栏
    var timer = '';    // 设置一个定时器
    var $box1 = $("#box1").children().clone(true);    // 克隆box1的子元素
    $("#box2").append($box1);    // 将复制的元素插入到#box2中
    var $left = parseInt($(".box").css("left"));    // 获取.box的left值
    var scroll = function () {
        $left -= 2;    // 设置滚动速度为2
        $(".box").css("left", $left + "px");    // left赋值
        if ($left < -1840) {    // 当box值小于-1840px时，重置.box left值为0；
            $(".box").css("left", "0");
            $left = 0;
        }
        timer = setTimeout(scroll, 30);
    };
    setTimeout(scroll, 100);
    $(".scroll-bar").hover(function () {
        clearTimeout(timer);
    }, function () {
        setTimeout(scroll, 100);
    });
});