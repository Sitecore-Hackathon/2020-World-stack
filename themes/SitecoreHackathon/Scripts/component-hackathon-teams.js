/*global notOnPageEditor, Swiper, VanillaTilt*/
/*eslint no-undef: "error"*/
jQuery(document).ready(function($) {
    var allowTouchMove = true;
    if(notOnPageEditor()) {
        allowTouchMove = true;
    } else {
        allowTouchMove = false;
    }
    var component = $(".hackathon-teams");
    var slider = new Swiper (component.find(".swiper-container"), {
        direction: 'horizontal',
        loop: false,
        allowTouchMove: allowTouchMove,
        slidesPerView: 'auto',
        watchOverflow: true,
        navigation: {
            nextEl: '.custom-arrow-right',
            prevEl: '.custom-arrow-left',
        },
        pagination: {
            el: '.swiper-pagination',
            dynamicBullets: true,
            dynamicMainBullets: 3
        },
    });
});
