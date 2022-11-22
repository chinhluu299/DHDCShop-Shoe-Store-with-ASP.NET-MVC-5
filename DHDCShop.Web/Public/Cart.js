//var formatter = new Intl.NumberFormat('en-US', {
//    style: 'currency',
//    currency: 'USD',

//    // These options are needed to round to whole numbers if that's what you want.
//    //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
//    //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
//});
function reloadCart() {

 
    const cartIcon = document.getElementById("icon-cart");
    const cartItem = document.getElementsByClassName("display-card")[0];
    const cartItemMinus = document.getElementsByClassName("cart-minus");
    const cartItemPlus = document.getElementsByClassName("cart-plus");
    const cartItemQuantity = document.getElementsByClassName("cart-quantity-detail");
    const cartItemTotal = document.getElementsByClassName("cart-total");
    const cartItemMoney = document.getElementsByClassName("cart-money");
    const cartItemShoeId = document.getElementsByClassName("shoe-id");
    const cartItemExit = document.getElementsByClassName("cart-item-exit")[0];
    const cartItemRemove = document.getElementsByClassName("cart-trash");

    const cartItemTotalAll = document.getElementsByClassName("cart-total-price-all-item")[0];
    const cartItemTotalAllValueString = document.getElementsByClassName("cart-total-price-all-item-getValue")[0];
    const cartItemSize_list = document.getElementsByClassName("item-cart-size");

    function init() {
        var cartItemTotalAllValue;
        if (cartItemTotalAllValueString != null) {
            cartItemTotalAllValue = Number(cartItemTotalAllValueString.textContent);
        }

        for (let index = 0; index < cartItemMinus.length; index++) {
            var value = cartItemMinus[index];
            value.onclick = function () {
                if (cartItemQuantity[index].value > 0) {
                    cartItemQuantity[index].value -= 1;
                    cartItemTotal[index].textContent = formatter.format(Number(cartItemMoney[index].textContent) * cartItemQuantity[index].value);
                    $.post(`/purchasing/AddItemToCartItem?productId=${cartItemShoeId[index].textContent}&size=${cartItemSize_list[index].textContent}&quantity=${-1}`);
                    cartItemTotalAllValue -= Number(cartItemMoney[index].textContent);
                    cartItemTotalAll.textContent = formatter.format(cartItemTotalAllValue);
                }

            }
        }



        for (let index = 0; index < cartItemPlus.length; index++) {
            var value = cartItemPlus[index];
            value.onclick = function () {
                cartItemQuantity[index].value = Number(cartItemQuantity[index].value) + 1;
                cartItemTotal[index].textContent = formatter.format(Number(cartItemMoney[index].textContent) * cartItemQuantity[index].value);
                $.post(`/purchasing/AddItemToCartItem?productId=${cartItemShoeId[index].textContent}&size=${cartItemSize_list[index].textContent}&quantity=${1}`);
                cartItemTotalAllValue = Number(cartItemMoney[index].textContent) + Number(cartItemTotalAllValue);
                cartItemTotalAll.textContent = formatter.format(cartItemTotalAllValue);

            }
        }


        cartIcon.onclick = () => {
            if (cartItem.classList.contains("d-none"))
                cartItem.classList.remove("d-none");

        }
        cartItemExit.onclick = () => {
            cartItem.classList.add("d-none");
        }

       
    }
    init();
    for (let index = 0; index < cartItemRemove.length; index++) {
        var value = cartItemRemove[index];

        value.onclick = function (e) {
            e.preventDefault();

            var btn = $(this);
            var productId = btn.data('id');
            var size = btn.data('size');

            $.ajax({
                url: `/purchasing/RemoveItemFromCart`,
                data: { productId: productId, size: size },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    var index = response.danhsach;
                    const list_item = document.getElementsByClassName("cart-table-body")[0];
                    list_item.removeChild(list_item.children[index]);

                    var rePrice = response.giatong ? response.giatong : "0.00";
                    console.log(rePrice);
                   
                    document.getElementsByClassName("cart-total-price-all-item")[0].innerHTML = rePrice;
                    document.getElementsByClassName("cart-total-price-all-item-getValue")[0].textContent = rePrice;
                    init();
                }
            })

        }
    }

}
reloadCart();