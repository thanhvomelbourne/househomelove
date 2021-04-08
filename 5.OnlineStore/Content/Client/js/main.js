(function ($) {
    "use strict"; var $window = $(window); $window.on('scroll', function () { var scroll = $window.scrollTop(); if (scroll < 300) { $(".sticker").removeClass("stick"); } else { $(".sticker").addClass("stick"); } }); $('.main-menu').meanmenu({ meanScreenWidth: '991', meanMenuContainer: '.mobile-menu', meanMenuClose: '<i class="pe-7s-close-circle"></i>', meanMenuOpen: '<i class="pe-7s-menu"></i>', meanRevealPosition: 'right', meanMenuCloseSize: '30px', }); new WOW().init(); $('#home-slider').nivoSlider({ directionNav: true, animSpeed: 1000, effect: 'random', slices: 18, pauseTime: 5000, pauseOnHover: true, controlNav: false, prevText: '<i class="pe-7s-angle-left-circle"></i>', nextText: '<i class="pe-7s-angle-right-circle"></i>' }); $('.testimonial-slider').slick({ slidesToShow: 1, slidesToScroll: 1, prevArrow: '<button type="button" class="arrow-prev"><i class="pe-7s-angle-left-circle"></i></button>', nextArrow: '<button type="button" class="arrow-next"><i class="pe-7s-angle-right-circle"></i></button>', responsive: [{ breakpoint: 767, settings: { arrows: false, } },] }); $('.product-slider-4').slick({ speed: 700, slidesToShow: 4, slidesToScroll: 1, prevArrow: '<button type="button" class="arrow-prev"><i class="pe-7s-angle-left-circle"></i></button>', nextArrow: '<button type="button" class="arrow-next"><i class="pe-7s-angle-right-circle"></i></button>', responsive: [{ breakpoint: 991, settings: { slidesToShow: 3, } }, { breakpoint: 767, settings: { slidesToShow: 2, } }, { breakpoint: 479, settings: { slidesToShow: 1, } }] }); $('.product-slider-2').slick({ speed: 700, slidesToShow: 2, slidesToScroll: 1, prevArrow: '<button type="button" class="arrow-prev"><i class="pe-7s-angle-left-circle"></i></button>', nextArrow: '<button type="button" class="arrow-next"><i class="pe-7s-angle-right-circle"></i></button>', responsive: [{ breakpoint: 991, settings: { slidesToShow: 3, } }, { breakpoint: 767, settings: { slidesToShow: 2, } }, { breakpoint: 479, settings: { slidesToShow: 1, } }] }); $('.pro-thumb-img-slider').slick({ speed: 700, slidesToShow: 4, slidesToScroll: 1, prevArrow: '<button type="button" class="arrow-prev"><i class="fa fa-long-arrow-left"></i></button>', nextArrow: '<button type="button" class="arrow-next"><i class="fa fa-long-arrow-right"></i></button>', responsive: [{ breakpoint: 991, settings: { slidesToShow: 3, } }, { breakpoint: 767, settings: { slidesToShow: 3, } }, { breakpoint: 479, settings: { slidesToShow: 2, } }] })
    $('#price-range').slider({ range: true, min: 0, max: 500, values: [0, 500], slide: function (event, ui) { $('.ui-slider-handle:eq(0)').html('<span id="price-slider-min">' + '$' + ui.values[0] + '</span>'); $('.ui-slider-handle:eq(1)').html('<span id="price-slider-max">' + '$' + ui.values[1] + '</span>'); } }); $('.ui-slider-handle:eq(0)').html('<span id="price-slider-min">' + '$' + $("#price-range").slider("values", 0) + '</span>'); $('.ui-slider-handle:eq(1)').html('<span id="price-slider-max">' + '$' + $("#price-range").slider("values", 1) + '</span>'); $('.product-quantity').append('<span class="dec qtybtn"><i class="fa fa-angle-left"></i></span><span class="inc qtybtn"><i class="fa fa-angle-right"></i></span>');

    $('.qtybtn').on('click', function () {
        var $button = $(this);
        var oldValue = $button.parent().find('input').val();
        var newVal = 0;
        if ($button.hasClass('inc')) {
            newVal = parseFloat(oldValue) + 1;
        }
        else {
            if (oldValue > 1) {
                newVal = parseFloat(oldValue) - 1;
            }
            else {
                newVal = 1;
            }
        }

        $button.parent().find('input').val(newVal);
    }); $('.checkout-form input[type="checkbox"]').on('click', function () { var $collapse = $(this).data('target'); if ($(this).is(':checked')) { $('.collapse[data-collapse="' + $collapse + '"]').slideDown(); } else { $('.collapse[data-collapse="' + $collapse + '"]').slideUp(); } })
    $(".youtube-bg").YTPlayer(); $('.tlt').textillate({ loop: true, in: { effect: 'fadeInRight', }, out: { effect: 'fadeOutLeft', }, }); $.scrollUp({ scrollText: '<i class="fa fa-angle-up"></i>', easingType: 'linear', scrollSpeed: 900, animation: 'fade' });
})(jQuery);