(function ($) {

   /* ..............................................
    Tooltip
    ................................................. */

   $(document).ready(function () {
      $('[data-toggle="tooltip"]').tooltip();
   });

   /* ..............................................
      Special Menu
      ................................................. */

   $(document).ready(function () {
      $('.category-list > ul > li:first-child').addClass('active');
      $('.category-list > ul > li').on('mouseenter', function () {
         $(this).addClass('active').siblings().removeClass('active');
      });
   });


   var Container = $('.container');
   Container.imagesLoaded(function () {
      var portfolio = $('.special-menu');
      portfolio.on('click', 'button', function () {
         $(this).addClass('active').siblings().removeClass('active');
         var filterValue = $(this).attr('data-filter');
         $grid.isotope({
            filter: filterValue
         });
      });
      var $grid = $('.special-list').isotope({
         itemSelector: '.special-grid'
      });
   });


   /* ..............................................
      Offer Box
      ................................................. */

   $('.offer-box').inewsticker({
      speed: 3000,
      effect: 'fade',
      dir: 'ltr',
      color: ' var(--main-color-light)',
      font_family: 'Montserrat, sans-serif',
      delay_after: 1000
   });

   /* ..............................................
      ................................................. */

   /* ..............................................
                 Start Discount
      ................................................. */

   const swiper = new Swiper('.swiper', {
      spaceBetween: 10,
      slidesPerView: "auto",
      freeMode: true,

      // Navigation arrows
      navigation: {
         nextEl: '.swiper-button-next',
         prevEl: '.swiper-button-prev',
      },
   });


   /* ..............................................
            END Discount
      ................................................. */


   /* ..............................................
           Start Swiper Blog Box
      ................................................. */
   var swiper_Blog = new Swiper("#swiper-box", {
      spaceBetween: 10,
      navigation: {
         nextEl: ".swiper-button-next",
         prevEl: ".swiper-button-prev",
      },
      pagination: {
         el: ".swiper-pagination",
         clickable: true,
      },
      loop: true,
      speed: 1000,
      breakpoints: {
         200: {
            slidesPerView: 1,
            spaceBetween: 20,
         },
         576: {
            slidesPerView: 2,
            spaceBetween: 20,
         },
         768: {
            slidesPerView: 3,
            spaceBetween: 40,
         }
      },
   });
   /* ..............................................
          END Swiper Blog Box
      ................................................. */

   /* ..............................................
         Start Swiper Product Feature Box
................................................. */
   var swiper_Blog = new Swiper("#swiper-Product-Feature", {
      spaceBetween: 10,
      /*     navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
         },*/
      pagination: {
         el: ".swiper-pagination",
         clickable: true,
      },
      loop: true,
      speed: 1000,
      breakpoints: {
         200: {
            slidesPerView: 1,
            spaceBetween: 10,
         },
         575: {
            slidesPerView: 2,
            spaceBetween: 10,
         },

         800: {
            slidesPerView: 3,
            spaceBetween: 10,
         },
         900: {
            slidesPerView: 4,
            spaceBetween: 10,
         },
         998.99: {
            slidesPerView: 5,
            spaceBetween: 10,
         }
      },
   });

   /* ..............................................
         End Swiper Product Feature Box
................................................. */





/* ..............................................
start page cart quantity
................................................. */
// انتخاب تمام محصولات
const products = document.querySelectorAll('.product-flow-item');
const productsPageCart = document.querySelectorAll('.shopping-cart>.main-info-product');

ChangeContentProduct(products);
ChangeContentProduct(productsPageCart);





function ChangeContentProduct(products) {

products.forEach(function (product) {
    const quantityInput = product.querySelector('.quantity');
    const decreaseButton = product.querySelector('.decrease');
    const increaseButton = product.querySelector('.increase');
    const errorMessage = product.querySelector('.error-message');
    const maxQuantity = parseInt(quantityInput.max);

    // رویداد کلیک برای دکمه افزایش مقدار
    increaseButton.addEventListener('click', function () {
        let currentValue = parseInt(quantityInput.value);
        if (currentValue < maxQuantity) {
            quantityInput.value = currentValue + 1;
            errorMessage.style.display = 'none'; // مخفی کردن پیام خطا
        } else {
            errorMessage.style.display = 'block'; // نمایش پیام خطا
        }
        updateDecreaseButton(quantityInput, decreaseButton); // به‌روزرسانی وضعیت دکمه کاهش
    });

    // رویداد کلیک برای دکمه کاهش مقدار
    decreaseButton.addEventListener('click', function (event) {
        event.stopPropagation(); // جلوگیری از انتقال رویداد به والدین
        let currentValue = parseInt(quantityInput.value);
        if (currentValue > 1) {
            quantityInput.value = currentValue - 1;
            errorMessage.style.display = 'none'; // مخفی کردن پیام خطا
         } else if (currentValue === 1) {
            // اضافه کردن کلاس slide-out برای انیمیشن
            product.classList.add('slide-out');
    
            // بعد از پایان انیمیشن، محصول را از DOM حذف کن
            setTimeout(() => {
                if (product.parentNode) { // بررسی وجود والدین
                    product.remove(); // حذف عنصر از DOM
                }
            }, 500); // 500ms برابر با مدت زمان transition در CSS
        }
    
        updateDecreaseButton(quantityInput, decreaseButton); // به‌روزرسانی وضعیت دکمه کاهش
    });

    // به‌روزرسانی وضعیت دکمه کاهش هنگام بارگذاری صفحه
    updateDecreaseButton(quantityInput, decreaseButton);
});

}
// حلقه بر روی هر محصول

// تابع برای به‌روزرسانی وضعیت دکمه کاهش
function updateDecreaseButton(quantityInput, decreaseButton) {
    let currentValue = parseInt(quantityInput.value);

    if (currentValue === 1) {
        // اگر مقدار 1 باشد، دکمه کاهش را به حالت حذف تبدیل کن
        decreaseButton.classList.add('trash'); // اضافه کردن کلاس trash
        decreaseButton.innerHTML = '<i class="bi bi-trash"></i>'; // تغییر آیکون به سطل آشغال
    } else {
        // اگر مقدار بیشتر از 1 باشد، دکمه کاهش را به حالت کم کردن تبدیل کن
        decreaseButton.classList.remove('trash'); // حذف کلاس trash
        decreaseButton.innerHTML = '<i class="bi bi-dash"></i>'; // تغییر آیکون به علامت کم کردن
    }
}
/* ..............................................
END page cart quantity
................................................. */


}(jQuery));


var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
   return new bootstrap.Tooltip(tooltipTriggerEl)
});

jQuery('.slider-item').owlCarousel({
   loop: true,
   margin: 10,
   rtl: true,
   nav: false,
   dots: true,
   responsiveClass: true,
   responsive: {
      0: {
         items: 1,
      },
      600: {
         items: 2,
      },
      1000: {
         items: 2,

      }
   }
});


/* ..............................................
        Start Swiper Feature Product box
   ................................................. */
jQuery('.slider-item-category').owlCarousel({
   loop: true,
   margin: 10,
   rtl: true,
   nav: false,
   dots: true,
   responsiveClass: true,
   responsive: {
      0: {
         items: 2,
      },
      600: {
         items: 3,
      },
      1000: {
         items: 4,

      }
   }
});
/* ..............................................
        END Swiper Feature Product box
   ................................................. */

jQuery('.product-gallery').owlCarousel({
   loop: true,
   margin: 10,
   rtl: true,
   nav: false,
   dots: true,
   responsiveClass: true,
   responsive: {
      0: {
         items: 2,
      },
      600: {
         items: 3,
      },
      1000: {
         items: 4,

      }
   }
});


jQuery('.brands-slider').owlCarousel({
   loop: true,
   margin: 10,
   rtl: true,
   nav: false,
   dots: false,
   responsiveClass: true,
   responsive: {
      0: {
         items: 2,
      },
      600: {
         items: 3,
      },
      1000: {
         items: 5,

      }
   }
});


jQuery('footer span.scrooltop ').click(function () {
   jQuery('html , body').animate({
      scrollTop: 0
   }, 100)
});

jQuery('.responsive-menu-container ul li').has('ul').append("<span class='resp-menu-ul-show'><i  class='bi bi-caret-down-fill'></i></span>");
jQuery('.responsive-menu-container ul li span.resp-menu-ul-show').click(function () {
   jQuery(this).prev('ul').slideToggle();
   jQuery(this).find('i').toggleClass('bi bi-caret-down-fill');
   jQuery(this).find('i').toggleClass('bi bi-caret-up-fill');
});

jQuery('#responsive-Menu-Btn').click(function () {
   jQuery('.responsive-menu-container').toggleClass('responsive-menu-show');
   jQuery('body,html').toggleClass('overflow-hidden');
});

jQuery('.close_responsive_Menu').click(function () {
   jQuery('.responsive-menu-container').removeClass('responsive-menu-show');
   jQuery('body,html').removeClass('overflow-hidden');
});

function get_window_width() {
   var win_width = jQuery(window).width();
   if (win_width > 768) {
      jQuery('.responsive-menu-container').removeClass('responsive-menu-show');
      jQuery('body,html').removeClass('overflow-hidden');
   }
}

get_window_width();

jQuery(window).resize(function () {
   get_window_width();

});



// عوض کردن رنگ شماره صفحه کلیک شده

document.addEventListener('DOMContentLoaded', function () {
   const pageNumbers = document.querySelectorAll('.pagination .page-number');

   pageNumbers.forEach(page => {
      page.addEventListener('click', function (e) {
         e.preventDefault();
         // حذف کلاس 'active' از همه دکمه‌ها
         pageNumbers.forEach(p => p.classList.remove('active'));
         // اضافه کردن کلاس 'active' به دکمه کلیک شده
         this.classList.add('active');
      });
   });
});