//Show cart flow
  // تابع برای باز و بسته کردن سبد خرید
  function toggleCart(event) {
    event.preventDefault(); // جلوگیری از عملکرد پیش‌فرض لینک‌ها
    const cart = document.getElementById('cart-buy');
    cart.classList.toggle('active'); // اگر سبد باز است، آن را می‌بندد و برعکس
}

// تابع برای بستن سبد خرید وقتی کاربر روی بیرون از سبد کلیک می‌کند
document.addEventListener('click', function(event) {
    const cart = document.getElementById('cart-buy');
    if (!cart.contains(event.target) && !event.target.closest('.cart-toggle-btn') && !event.target.closest('.close-btn')) {
        if (cart.classList.contains('active')) {
            toggleCart(event); // بستن سبد خرید
        }
    }
});

// Calling Only countShopCart When The Page Is Finished Loading 
$(function () {
    countShopCart();
});


function countShopCart() {
    $.get("/Api/ShopCart", function (res) {
        $("#countShopCart").html(res);
    });
}


// When CLick Add To Cart
function addToCart(id) {
    count = $("#countProduct").val();
    $("#countProduct").val(1);

    $.get("/Api/ShopCart/" + id +"?count=" + count , function (res) {
        $("#countShopCart").html(res);

        updateShowCart();
    });

}


function updateShowCart() {
    $("#cart").load("/ShopCartInfo/ShowCart"); 
}



// Calling Order Action 
function removeItemOrder(id, count) {
    // $.get("/ShopCartInfo/CommandOrder"+id+"?count="+count)
    $.ajax({
        url: "/ShopCartInfo/RemoveItemOrder/" + id,
        type: "Get",
        dataType: "script",
        data: { count: count }
    }).done(function (res) {
        $("#showOrder").html(res);
        countShopCart();
        updateShowCart();
    });
}
