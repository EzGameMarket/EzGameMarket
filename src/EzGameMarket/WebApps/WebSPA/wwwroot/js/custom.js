/*

-Slider-home
-Nav mobile
-Back-to-top
-Counter
-Popup-video
-Search
-FilterSlider
-Slick-slider

*/


$(document).ready(function() {

    "use strict";


    /*-----------------------------
          Slider-home
      -------------------------------*/
      // Init slick slider + animation
      $('.slider_slick').slick({
        autoplay: true,
        speed: 800,
        lazyLoad: 'progressive',
        arrows: true,
        dots: true,
      }).slickAnimation();

      // Toggle mobile navigation
      function toggleMenuMobile() {
          var navbar = $(".navigation-mobile");
          var openBtn = $(".navbar-header .open-btn");
          var closeBtn = $(".navigation-mobile .close-navbar");

          openBtn.on("click", function() {
              if (!navbar.hasClass("nav-open")) {
                  navbar.addClass("nav-open");
              }
              return false;
          })

          closeBtn.on("click", function() {
              if (navbar.hasClass("nav-open")) {
                  navbar.removeClass("nav-open");
              }
              return false;
          })
      }

      toggleMenuMobile();


        // Function for small menu
      function smallNavchild() {
          var mainNav = $(".navigation-mobile");
          var smallNav = $(".navigation-mobile > .small-nav");
          var subMenu = smallNav.find(".sub-menu");
          var menuItemWidthSubMenu = smallNav.find("> li:has(ul) > a ");

         subMenu.hide();
          menuItemWidthSubMenu.on("click", function(e) {
              var $this = $(this);
              $this.siblings().slideToggle();
               e.preventDefault();
              e.stopImmediatePropagation();
          })
      }
      smallNavchild();


        // Function for accordionShop
      function accordionShop() {
          var ShopNav = $(".widget-woof-content > section");
          var ShopMenu = ShopNav.find(".widget-woof-content .widget-content");
          var menuItemShop = ShopNav.find(" .widget_collapse ");

          ShopMenu.hide();
          menuItemShop.on("click", function(e) {
              var $this = $(this);
              $this.siblings().slideToggle();
               e.preventDefault();
              e.stopImmediatePropagation();
          })
      }
      accordionShop();

    
      /*-----------------------------
          Back-to-top
      -------------------------------*/
      $(window).on('scroll', function (e) {
          e.preventDefault();
        if ($(this).scrollTop() > 100) $('#back-to-top').fadeIn();
         else $('#back-to-top').fadeOut();
        });
        $('#back-to-top').on('click', function(e) {
          e.preventDefault();
          $('body,html').animate({scrollTop: 0}, 'slow');
      });

      /*-----------------------------
          Counter
      -------------------------------*/
      $('.counter').counterUp({
          delay: 10,
          time: 1000
      });

      /*-----------------------------
              popup-video
      -------------------------------*/
      $('.popup-video > a').each(function() {
        $(this).magnificPopup({
            disableOn: 700,
            type: 'iframe',
            mainClass: 'mfp-fade',
            removalDelay: 160,
            preloader: false,
            fixedContentPos: false
        });
      }); 

      /*-----------------------------
          Search
      -------------------------------*/
      

      function search() {
        var searchOpen = $('.ps-search-btn'),
            searchClose = $('.ps-search__close'),
            searchbox = $('.ps-search');
        searchOpen.on('click', function(e) {
            e.preventDefault();
            searchbox.addClass('open');
        });
        searchClose.on('click', function(e) {
            e.preventDefault();
            searchbox.removeClass('open');
        });
      }

      /*-----------------------------
          FilterSlider
      -------------------------------*/
      function filterSlider() {
        var el = $('.ps-slider');
        var min = el.siblings().find('.ps-slider__min');
        var max = el.siblings().find('.ps-slider__max');
        var defaultMinValue = el.data('default-min');
        var defaultMaxValue = el.data('default-max');
        var maxValue = el.data('max');
        var step = el.data('step');
        if (el.length > 0) {
            el.slider({
                min: 0,
                max: maxValue,
                step: step,
                range: true,
                values: [defaultMinValue, defaultMaxValue],
                slide: function(event, ui) {
                    var values = ui.values;
                    min.text('$' + values[0]);
                    max.text('$' + values[1]);
                }
            });
            var values = el.slider("option", "values");
            console.log(values[1]);
            min.text('$' + values[0]);
            max.text('$' + values[1]);
        }
        else {
            // return false;
        }
    }
    $(function() {
       search();
       filterSlider();
    });  

  /*-------------------------------------------------------
          slick slider
      -------------------------------*/
    $(".owl-carousel").each( function(){

      var slidesToShow    =     $(this).data('items'); 


        if ($(this).data('large')) {
            var desktop = $(this).data('large');
        } else {
            var desktop = slidesToShow;
        }
        if ($(this).data('medium')) {
            var medium = $(this).data('medium');
        } else {
            var medium = slidesToShow;
        }
        if ($(this).data('smallmedium')) {
            var smallmedium = $(this).data('smallmedium');
        } else {
            var smallmedium = slidesToShow;
        }
        if ($(this).data('extrasmall')) {
            var extrasmall = $(this).data('extrasmall');
        } else {
            var extrasmall = 2;
        }
        if ($(this).data('verysmall')) {
            var verysmall = $(this).data('verysmall');
        } else {
            var verysmall = 2;
        }


      var _config = [];

      _config.dots             = $(this).data( 'pagination' );
      _config.arrows           = $(this).data( 'nav' );
      _config.infinite         = true;
      _config.speed            = 500;
      _config.autoplay         = $(this).data('autoplay');
      _config.cssEase          = 'linear';
      _config.slidesToShow     = slidesToShow;
      _config.slidesToScroll   = 1;
      _config.mobileFirst      = true;
      _config.vertical         = false;


      _config.responsive       = [
         {
            breakpoint: 1500,
            settings: { 
              slidesToShow: slidesToShow,
              slidesToScroll: slidesToShow,
            }
          },
          {
            breakpoint: 1280,
            settings: { 
              slidesToShow: desktop,
              slidesToScroll: desktop,
            }
          },
          {
            breakpoint: 980,
            settings: {
              slidesToShow: medium,
              slidesToScroll: medium,
            }
          },
          {
            breakpoint: 767,
            settings: {
              slidesToShow:smallmedium,
              slidesToScroll: smallmedium,
              infinite: false,
            }
          },                
          {
            breakpoint: 479,
            settings: {
              slidesToShow:extrasmall,
              slidesToScroll: extrasmall,
              infinite: false,
              unslick: true,
            },
          },                
          {
            breakpoint: 0,
            settings: {
              slidesToShow:verysmall,
              slidesToScroll: verysmall,
              infinite: false,
            },
          }
      ];  

      $(this).slick( _config );
    });

  /*-------------------------------------------------------
          slick slider single product
      -------------------------------*/
    $('.slider-for').slick({
     slidesToShow: 1,
     slidesToScroll: 1,
     arrows: false,
     fade: true,
     asNavFor: '.slider-nav'
   });
   $('.slider-nav').slick({
     slidesToShow: 5,
     slidesToScroll: 1,
     asNavFor: '.slider-for',
     dots: false,
     focusOnSelect: true
   });


});


