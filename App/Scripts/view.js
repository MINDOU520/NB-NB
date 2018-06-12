$(function () {
    // 设置商城图片宽高比，契合网站图片响应式
    var goodsimg = $(".goods-view-img img");
    goodsimgw = goodsimg.width();
    goodsimgh = goodsimgw * 0.8;
    goodsimg.css({ height: goodsimgh });
});

$(function () {
    $(".type1").click(function () {
        $(".type1").addClass("type-active").siblings("div").removeClass("type-active");
        $(".allgoods-li").show().siblings("li").hide();
    });
    $(".type2").click(function () {
        $(".type2").addClass("type-active").siblings("div").removeClass("type-active");
        $(".basketball-li").show().siblings("li").hide();
    });
    $(".type3").click(function () {
        $(".type3").addClass("type-active").siblings("div").removeClass("type-active");
        $(".clothes-li").show().siblings("li").hide();
    });
    $(".type4").click(function () {
        $(".type4").addClass("type-active").siblings("div").removeClass("type-active");
        $(".shose-li").show().siblings("li").hide();
    });
    $(".type5").click(function () {
        $(".type5").addClass("type-active").siblings("div").removeClass("type-active");
        $(".protective-li").show().siblings("li").hide();
    });
    $(".type6").click(function () {
        $(".type6").addClass("type-active").siblings("div").removeClass("type-active");
        $(".other-li").show().siblings("li").hide();
    });
    $(".goods-detail").click(function () {
        $(".goods-detail").addClass("goods-active").siblings("div").removeClass("goods-active");
        $(".goods-deta-li").show().siblings("li").hide();
    });
    $(".goods-buy").click(function () {
        $(".goods-buy").addClass("goods-active").siblings("div").removeClass("goods-active");
        $(".goods-buy-li").show().siblings("li").hide();
    });
    $(".goods-comm").click(function () {
        $(".goods-comm").addClass("goods-active").siblings("div").removeClass("goods-active");
        $(".goods-comm-li").show().siblings("li").hide();
    });    
});

function magnify(imgID, zoom) {
    var img, glass, w, h, bw;
    img = document.getElementById(imgID);

    // 创建放大镜
    glass = document.createElement("DIV");
    glass.setAttribute("class", "img-magnifier-glass");

    // 插入放大镜 
    img.parentElement.insertBefore(glass, img);

    // 为放大镜设置背景属性
    glass.style.backgroundImage = "url('" + img.src + "')";
    glass.style.backgroundRepeat = "no-repeat";
    glass.style.backgroundSize = (img.width * zoom) + "px " + (img.height * zoom) + "px";
    bw = 3;
    w = glass.offsetWidth / 2;
    h = glass.offsetHeight / 2;

    // 将放大镜移动到图像上时，执行函数
    glass.addEventListener("mousemove", moveMagnifier);
    img.addEventListener("mousemove", moveMagnifier);

    // 适用于触摸屏
    glass.addEventListener("touchmove", moveMagnifier);
    img.addEventListener("touchmove", moveMagnifier);
    function moveMagnifier(e) {
        var pos, x, y;
        // 防止在移动图像时可能发生的任何其他操作
        e.preventDefault();
        // 获取光标的x和y位置
        pos = getCursorPos(e);
        x = pos.x;
        y = pos.y;
        // 防止放大镜被放置在图像的外部
        if (x > img.width - (w / zoom)) { x = img.width - (w / zoom); }
        if (x < w / zoom) { x = w / zoom; }
        if (y > img.height - (h / zoom)) { y = img.height - (h / zoom); }
        if (y < h / zoom) { y = h / zoom; }
        // 设置放大镜的位置
        glass.style.left = (x - w) + "px";
        glass.style.top = (y - h) + "px";
        // 展示放大镜“看到的东西”
        glass.style.backgroundPosition = "-" + ((x * zoom) - w + bw) + "px -" + ((y * zoom) - h + bw) + "px";
    }

    function getCursorPos(e) {
        var a, x = 0, y = 0;
        e = e || window.event;
        // 得到图像的x和y位置
        a = img.getBoundingClientRect();
        // 相对于图像，计算光标的x和y坐标
        x = e.pageX - a.left;
        y = e.pageY - a.top;
        // 考虑任何页面滚动
        x = x - window.pageXOffset;
        y = y - window.pageYOffset;
        return { x: x, y: y };
    }
}

window.onload = function () {
    // 执行放大功能
    magnify("myimage", 3);    // 指定图像的id和放大镜的强度
}

$(function () {
    // 页面加载渲染使放大镜隐藏
    addEventListener("load", function () {
        setTimeout(hideglass, 0);
    }, false);
    function hideglass() {
        $(".img-magnifier-glass").hide();
    }

    // 鼠标进入显示放大镜，移出则隐藏
    $(".commidity-img").mouseenter(function () {
        $(".img-magnifier-glass").show();
    });
    $(".commidity-img").mouseleave(function () {
        $(".img-magnifier-glass").hide();
    });
})

// 兼容IE
$(function () {
    var lastTime = 0;
    var vendors = ['webkit', 'moz'];
    for (var x = 0; x < vendors.length && !window.requestAnimationFrame; ++x) {
        window.requestAnimationFrame = window[vendors[x] + 'RequestAnimationFrame'];
        window.cancelAnimationFrame =
            window[vendors[x] + 'CancelAnimationFrame'] || window[vendors[x] + 'CancelRequestAnimationFrame'];
    }

    if (!window.requestAnimationFrame) {
        window.requestAnimationFrame = function (callback, element) {
            var currTime = new Date().getTime();
            var timeToCall = Math.max(0, 16 - (currTime - lastTime));
            var id = window.setTimeout(function () {
                callback(currTime + timeToCall);
            },
                timeToCall);
            lastTime = currTime + timeToCall;
            return id;
        };
    }
    if (!window.cancelAnimationFrame) {
        window.cancelAnimationFrame = function (id) {
            clearTimeout(id);
        };
    }
}());

// 点击加入购物车按钮商品飞入购物车
$(function () {
    $(".add-to-cart").click(function (event) { 
        var _this = $(this);    // 获取点击元素
        
        // 获取购物车位置
        var cartLeft = $(".cart").offset().left;    // 获取.cart距离屏幕顶端的距离
        var cartTop = $(".cart").offset().top - $(document).scrollTop();    // 获取.cart的y坐标(因为fly插件的start开始位置是根据屏幕可视区域x，y来计算的，而不是根据整个文档的x，y来计算的)

        // 获取商品图片位置
        var imgLeft = $(".commidity-img").offset().left;
        var imgTop = $(".commidity-img").offset().top - $(document).scrollTop();

        // 新创建一个飞入购物车的商品图
        var addcar = $(".add-to-cart");
        var img = addcar.parent(".goods-info-other").siblings().children("div.commidity-img").children("div.img-magnifier-container").children("#myimage").attr('src');

        var flyer = $('<img class="u-flyer" src="' + img + '">');
        event.preventDefault();    // 阻止a链接的默认行为 
        flyer.fly({
            start: {    // 开始位置
                left: imgLeft,
                top: imgTop
            },
            end: {    // 结束位置
                left: cartLeft + 35,
                top: cartTop + 35,
                width: 0,    // 结束时宽度 
                height: 0    // 结束时高度 
            },
            onEnd: function () {    // 结束回调                
                this.destory();    // 移除dom 
                // alert('加入成功！');
                window.location.href = _this.attr("href");    // 定向到该元素链接地址      
            }
        });              
    });
})

$(function () {
    $(".rep").click(function () {
        $(this).siblings(".rep-postbox").show();
        $(this).parent().siblings(".news-comm").children(".rep-postbox").hide();
    })
})

$(function () {
    $(".tjdd").click(function () {
        $(".ddinfo").fadeIn(1000);
    });
    // 点击关闭按钮和遮罩层影藏教练信息
    $(".dd-info-close").click(function () {
        $(".ddinfo").fadeOut(1000);
    });
})
