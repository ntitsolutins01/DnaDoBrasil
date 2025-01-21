(function ($) {
    'use strict';

    //Header Search
    if ($('.search-box-outer').length) {
        $('.search-box-outer').on('click', function () {
            $('body').addClass('search-active');
        });
        $('.close-search').on('click', function () {
            $('body').removeClass('search-active');
        });
    }

    // Mobile Menu
    $('.mobile-menu nav').meanmenu({
        meanScreenWidth: "991",
        meanMenuContainer: ".mobile-menu",
        meanMenuOpen: "<span></span> <span></span> <span></span>",
        onePage: true,
    });

    // Manipulador de clique para links do menu móvel
    $(document).on('click', '.mean-nav ul li a', function (e) {
        // Verifica se o link clicado não é um submenu toggle
        if (!$(this).hasClass('mean-expand')) {
            // Previne múltiplos cliques
            if (e.originalEvent !== undefined) {
                // Fecha o menu diretamente sem toggle
                $('.mean-nav ul:first').slideUp();
                $('.meanmenu-reveal').removeClass('meanclose');
                menuOn = false; // Reseta o estado do menu

                // Se for um link de âncora (hash), previne o comportamento padrão
                if (this.hash !== "") {
                    e.preventDefault();

                    // Scroll suave até a âncora
                    $('html, body').animate({
                        scrollTop: $(this.hash).offset().top - 50
                    }, 800);
                }
            }
        }
    });

    // Previne que o menu reabra ao terminar a animação
    $(document).on('click', '.meanmenu-reveal', function (e) {
        if (!$(this).hasClass('meanclose')) {
            e.stopPropagation();
        }
    });

    // sticky
    var wind = $(window);
    var sticky = $('#sticky-header');
    wind.on('scroll', function () {
        var scroll = wind.scrollTop();
        if (scroll < 50) {
            sticky.removeClass('sticky');
        } else {
            sticky.addClass('sticky');
        }
    });

    // Loder  //
    $(function () {
        $('body').addClass('loaded');
    });

    // Case Study Active

    $('.reviews').owlCarousel({
        loop: true,
        autoplay: true,
        smartSpeed: 1500,
        autoplayTimeout: 2000,
        dots: true,
        nav: false,
        margin: 30,
        navText: [""],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            },
            1200: {
                items: 5
            },
            1920: {
                items: 5
            }
        }
    })

    // testimonial Active

    $('.testimonial').owlCarousel({
        loop: true,
        autoplay: true,
        smartSpeed: 1500,
        autoplayTimeout: 2000,
        dots: false,
        nav: true,
        center: true,
        margin: 30,
        center: true,
        navText: [""],
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 2
            },
            1200: {
                items: 4
            },
            1920: {
                items: 4
            }
        }
    })

    /*  Cart Plus Minus Button
    /*----------------------------------------*/

    $('.ctnbutton').on('click', function () {
        var $button = $(this);
        var oldValue = $button.parent().find('input').val();
        if ($button.hasClass('inc')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below zero
            if (oldValue > 1) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 1;
            }
        }
        $button.parent().find('input').val(newVal);
    });


    /*---------------------
    WOW active js 
    --------------------- */
    new WOW().init();

    // counterUp
    $('.counter').counterUp({
        delay: 5,
        time: 1500
    });

    /*--------------------------
     scrollUp
    ---------------------------- */
    $.scrollUp({
        scrollText: '<i class="fa fa-angle-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    })


    jQuery(document).ready(function ($) {
        "use strict";

        // =======< accordion js >========
        $(".accordion > li:eq(0) a").addClass("active").next().slideDown();
        $('.accordion a').on('click', function (j) {
            var dropDown = $(this).closest("li").find("p");

            $(this).closest(".accordion").find("p").not(dropDown).slideUp();

            if ($(this).hasClass("active")) {
                $(this).removeClass("active");
            } else {
                $(this).closest(".accordion").find("a.active").removeClass("active");
                $(this).addClass("active");
            }

            dropDown.stop(false, true).slideToggle();

            j.preventDefault();
        });


        //======< Custom Tab >======
        $('.tab ul.tabs').addClass('active').find('> li:eq(0)').addClass('current');

        $(".tab ul.tabs li a").on("click", function (g) {
            var tab = $(this).closest('.tab'),
                index = $(this).closest('li').index();

            tab.find('ul.tabs > li').removeClass('current');
            $(this).closest('li').addClass('current');

            tab.find('.tab_content').find('div.tabs_item').not('div.tabs_item:eq(' + index + ')').slideUp();
            tab.find('.tab_content').find('div.tabs_item:eq(' + index + ')').slideDown();

            g.preventDefault();
        });

    });


    // scroll up

    if ($('.prgoress_indicator path').length) {
        var progressPath = document.querySelector('.prgoress_indicator path');
        var pathLength = progressPath.getTotalLength();
        progressPath.style.transition = progressPath.style.WebkitTransition = 'none';
        progressPath.style.strokeDasharray = pathLength + ' ' + pathLength;
        progressPath.style.strokeDashoffset = pathLength;
        progressPath.getBoundingClientRect();
        progressPath.style.transition = progressPath.style.WebkitTransition = 'stroke-dashoffset 10ms linear';
        var updateProgress = function () {
            var scroll = $(window).scrollTop();
            var height = $(document).height() - $(window).height();
            var progress = pathLength - (scroll * pathLength / height);
            progressPath.style.strokeDashoffset = progress;
        }
        updateProgress();
        $(window).on('scroll', updateProgress);
        var offset = 250;
        var duration = 550;
        jQuery(window).on('scroll', function () {
            if (jQuery(this).scrollTop() > offset) {
                jQuery('.prgoress_indicator').addClass('active-progress');
            } else {
                jQuery('.prgoress_indicator').removeClass('active-progress');
            }
        });
        jQuery('.prgoress_indicator').on('click', function (event) {
            event.preventDefault();
            jQuery('html, body').animate({ scrollTop: 0 }, duration);
            return false;
        });
    }

    // Sidebar

    "use strict";
    jQuery(document).ready(function (o) {
        0 < o(".offset-side-bar").length &&
            o(".offset-side-bar").on("click", function (e) {
                e.preventDefault(), e.stopPropagation(), o(".cart-group").addClass("isActive");
            }),
            0 < o(".close-side-widget").length &&
            o(".close-side-widget").on("click", function (e) {
                e.preventDefault(), o(".cart-group").removeClass("isActive");
            }),
            0 < o(".navSidebar-button").length &&
            o(".navSidebar-button").on("click", function (e) {
                e.preventDefault(), e.stopPropagation(), o(".info-group").addClass("isActive");
            }),
            0 < o(".close-side-widget").length &&
            o(".close-side-widget").on("click", function (e) {
                e.preventDefault(), o(".info-group").removeClass("isActive");
            }),
            o("body").on("click", function (e) {
                o(".info-group").removeClass("isActive"), o(".cart-group").removeClass("isActive");
            }),
            o(".xs-sidebar-widget").on("click", function (e) {
                e.stopPropagation();
            }),
            0 < o(".xs-modal-popup").length &&
            o(".xs-modal-popup").magnificPopup({
                type: "inline",
                fixedContentPos: !2,
                fixedBgPos: !0,
                overflowY: "auto",
                closeBtnInside: !2,
                callbacks: {
                    beforeOpen: function () {
                        this.st.mainClass = "my-mfp-slide-bottom xs-promo-popup";
                    },
                },
            });
    });


})(jQuery);