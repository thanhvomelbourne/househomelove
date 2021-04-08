var SHOPPING_CART_ID = "SHOPPING_CART_ID";
var CURRENT_USER_ID = "USER_ID";
var ORDER_ID = "ORDER_ID";
var DOMAIN = location.protocol + "//" + location.host;
var URL = window.location.href;
var PAYMENT_METHOD = "CreditCard";
var AUS_POST = "AUS_POST";
var HOME_MAIL = "HOME_MAIL";

$(document).ready(function () {
    if (URL.indexOf("/shop/checkout/orderconfirmation") === -1) {
        loadCurrentCart();
    }
    $("#ship_form").click(function () {
        var val = $(this).is(":checked");
        if (val === false) {
            copyBillingToDeliveryForm();
        }
        else {
            clearDeliveryForm();
        }
    });

    $("input:radio[name=shippingMethod]").change(function () {
        var cal = saveOrderAndReCalculate(false);

        if (!cal) {
            $("#ausPostDelivery").prop("checked", true);
        }
    });
});

function addToCart(pid, qty) {
    $("#loading").show();
    var shoppingCartId = getCookie(SHOPPING_CART_ID);

    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/ShoppingCart/AddToCart",
        async: true,
        data: {
            CartId: shoppingCartId !== "" ? shoppingCartId : 0,
            ProductId: pid,
            Quantity: qty
        },
        dataType: "json",
        timeout: 30000,
        success: function (response) {
            if (response.success === true) {
                loadDataToCart(response);
                $("#loading").hide();
                openShoppingModal();
            }
            else {
                $("#loading").hide();
                openMessage("Cannot add this item to cart. There were some errors in the process! </br>" + response.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            openMessage(xhr.status + " / " + thrownError);
            $("#loading").hide();
        }
    });
};

function loadCurrentCart() {
    $("#loading").show();
    var shoppingCartId = getCookie(SHOPPING_CART_ID);

    $.ajax({
        cache: false,
        type: "GET",
        url: DOMAIN + "/ShoppingCart/GetCurrentCart",
        async: true,
        data: {
            cartId: shoppingCartId !== "" ? shoppingCartId : 0
        },
        dataType: "json",
        timeout: 30000,
        success: function (response) {
            if (response.success === true) {
                loadDataToCart(response);
                if (URL.indexOf("/shop/checkout") !== -1) {
                    loadCartToCheckout(response);
                    var currentUserId = getCookie(CURRENT_USER_ID);
                    var currentOrderId = getCookie(ORDER_ID);
                    if (currentUserId != "" && currentOrderId == "") {
                        getCurrentUser();
                    }
                    else {
                        loadCurrentOrder();
                    }
                }
                if (URL.indexOf("/shop/shoppingcart") !== -1) {
                    loadCartToShoppingCart(response);
                }
                $("#loading").hide();
            }
            else {
                $("#loading").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //alert(xhr.status);
            //alert(thrownError);
            $("#loading").hide();
        }
    });
};

function loadDataToCart(response) {
    $("#totalItemsInCart").html("");
    $("#totalItemsInCart").append(response.totalItem + " item(s)");
    $("#totalItemsInCartBadge").html("");
    $("#totalItemsInCartBadge").append(response.totalItem);
    $("#itemListInCart").html("");

    var list = "";

    $.each(response.data.CartItemList, function (k, v) {
        var item = '<div class="single-cart clearfix"><div class="cart-image"><a href="/shop/product?productId=' + v.ProductId + '&productName=' + v.Name + '"><img src="' + v.PrimaryImage + '" alt=""></a></div><div class="cart-info"><h5><a href="/shop/product?productId=' + v.ProductId + '">' + v.Name + '</a></h5><p>Price : $' + v.FinalPrice + '</p><p>Qty : ' + v.Quantity + '</p><a class="cart-delete" onclick="updateCurrentCart(' + v.ProductId + ', 0)" title="Remove this item"><i class="pe-7s-trash"></i></a></div></div >';

        list += item;
    });

    $("#itemListInCart").append(list);
    $("#totalPriceExGstInCart").html("");
    $("#btnCheckout").html("");

    if (list !== "") {
        $("#smallCartInformation").show();
        $("#totalPriceExGstInCart").append("$" + response.data.TotalAmountIncGst);
        $("#btnCheckout").append('<a href="/shop/shoppingcart">Check out</a>');
    }
    else {
        $("#smallCartInformation").hide();
    }
}

function clearDataToCart() {
    $("#totalItemsInCart").html("");
    $("#totalItemsInCartBadge").text(0);
    $("#itemListInCart").html("");
    $("#totalPriceExGstInCart").html("");
}

function loadCartToCheckout(response) {
    $("#productListCheckout").html("");
    var list = "";

    $.each(response.data.CartItemList, function (k, v) {
        var item = '<tr><td class="product-name">' + v.Name + '<strong class="product-qty"> × ' + v.Quantity + '</strong></td ><td class="product-total"><span class="amount">$' + v.FinalPrice + '</span></td></tr >';

        list += item;
    });

    $("#productListCheckout").append(list);

    $("#cartSubTotal").html("");
    $("#cartSubTotal").append("$" + response.data.TotalAmountExGst);

    $("#cartGST").html("");
    $("#cartGST").append("$" + response.data.Gst);

    $("#shippingFee").html("");
    $("#shippingFee").append("$" + response.data.ShippingFee);

    $("#checkoutDeliveryStatus").html("");
    $("#checkoutDeliveryStatus").append(response.data.EstimateDelivery);

    $("#cartDiscount").html("");
    $("#cartDiscount").append("- $" + response.data.Discount);

    if (response.data.Discount > 0) {
        $(".discountSection").show();
    }
    else {
        $(".discountSection").hide();
    }

    $(".promotionAppliedDisplay").html("");
    if (response.data.PromotionCode && response.data.PromotionCode !== "") {
        $(".promotionAppliedDisplay").append("<p class='text-primary'>" + response.data.PromotionCode + " has been applied <a onclick='applyPromotionCode(true)'><i class='fa fa-close'></i></a></p>");
    }

    $("#cartTotal").html("");
    $("#cartTotal").append("$" + response.data.TotalFinalPrice);

    if (response.data.HomeMailAvailable) {
        $("#homeMailDelivery").prop("disabled", false);
    }
    else {
        $("#homeMailDelivery").prop("disabled", true);
    }
}

function loadCartToShoppingCart(response) {
    $("#productListShoppingCart").html(null);
    $("#productListShoppingCartMobile").html(null);

    var list = "";
    var mobileList = "";

    $.each(response.data.CartItemList, function (k, v) {
        var item = '<tr><td class="pro-thumbnail"><a href="/shop/product?productId=' + v.ProductId + '&productName=' + v.Name + '"><img src="' + v.PrimaryImage + '" alt="" /></a></td><td class="pro-title"><a href="/shop/product?productId=' + v.ProductId + '&productName=' + v.Name + '">' + v.Name + '</a></td><td class="pro-price"><span class="amount">$' + v.DiscountPrice + '</span></td><td class="pro-quantity"><div class="product-quantity"><input type="text" id="quan_' + v.ProductId + '" value="' + v.Quantity + '" min="0" onkeypress="return (event.charCode == 8 || event.charCode == 0) ? null : event.charCode >= 48 && event.charCode <= 57"/></div></td><td class="pro-subtotal">$' + v.FinalPrice + '</td><td class="pro-quantity cart-buttons"><input type="hidden" id="quan_hide_' + v.ProductId + '" value="' + v.Quantity + '" /><input type="submit" onclick="updateCurrentCart(' + v.ProductId + ', -99)" value="Update" style="width: 100%"/></td><td class="pro-remove"><a onclick="updateCurrentCart(' + v.ProductId + ', 0)">×</a></td></tr>';
        list += item;

        var mobileItem = '<div class="row pro-container"><div class="col-xs-12"><div class="pro-thumbnail mt-15 mb-15"><a href="/shop/product?productId=' + v.ProductId + '&productName=' + v.Name + '"><img class="img-thumbnail" src="' + v.PrimaryImage + '" alt="" /></a ></div > <h3 class="pro-title text-center"><a href="/shop/product?productId=' + v.ProductId + '&productName=' + v.Name + '">' + v.Name + '</a></h3> <h2 class="pro-price text-center"><span class="amount">$' + v.DiscountPrice + '</span></h2></div > <div class="container-fluid"><div class="row"><div class="col-xs-12"><div class="pro-quantity"><div class="product-quantity"><input type="text" id="quan_mobile_' + v.ProductId + '" value="' + v.Quantity + '" min="0" onkeypress="return (event.charCode == 8 || event.charCode == 0) ? null : event.charCode >= 48 && event.charCode <= 57"/></div></div></div><div class="col-xs-12 mt-10"><h4 class="pro-subtotal text-center pt-15 pb-15">Total $' + v.FinalPrice + '</h4></div><div class="col-xs-6" style="border-right: 1px solid #dfdfdf"><h4 class="pro-remove text-center"><input type="hidden" id="quan_hide_mobile_' + v.ProductId + '" value="' + v.Quantity + '" /><a onclick="updateCurrentCart(' + v.ProductId + ', -99, true)">Update quantity</a></h4></div>' +
            '<div class="col-xs-6" style="border-left: 1px solid #dfdfdf"><h4 class="pro-remove text-center"><a onclick="updateCurrentCart(' + v.ProductId + ', 0)">Delete</a></h4></div>' +
            '</div></div></div>';

        mobileList += mobileItem;
    });

    $("#productListShoppingCart").append(list);
    $("#productListShoppingCartMobile").append(mobileList);

    $("#subTotalShoppingCart").html("");
    $("#subTotalShoppingCart").append("$" + response.data.TotalAmountExGst);

    $("#shippingFeeShoppingCart").html("");
    $("#shippingFeeShoppingCart").append("$" + response.data.ShippingFee);

    $("#subTotalShoppingCartMobile").html("");
    $("#subTotalShoppingCartMobile").append("$" + response.data.TotalAmountExGst);

    $("#shippingFeeShoppingCartMobile").html("");
    $("#shippingFeeShoppingCartMobile").append("$" + response.data.ShippingFee);

    $(".promotionAppliedDisplay").html("");
    if (response.data.PromotionCode && response.data.PromotionCode !== "NO") {
        $(".promotionAppliedDisplay").append("<p class='text-primary'>" + response.data.PromotionCode + " has been applied <a onclick='applyPromotionCode(true)'><i class='fa fa-close'></i></a></p>");
    }

    $("#totalShoppingCart").html("");
    $("#totalShoppingCart").append("$" + response.data.TotalFinalPrice);

    $("#totalShoppingCartMobile").html("");
    $("#totalShoppingCartMobile").append("$" + response.data.TotalFinalPrice);

    $('.product-quantity').append('<span class="dec qtybtn"><i class="fa fa-angle-left"></i></span><span class="inc qtybtn"><i class="fa fa-angle-right"></i></span>');

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
    });
}

function updateCurrentCart(pid, qty, isMobile) {
    $("#loading").show();
    var shoppingCartId = getCookie(SHOPPING_CART_ID);
    var isDelete = false;
    var isUpdate = false;

    if (qty === -99) {
        var currentQty = 0;
        var newQty = 0;

        if (isMobile === true) {
            currentQty = $("#quan_hide_mobile_" + pid).val();
            newQty = $("#quan_mobile_" + pid).val();
        }
        else {
            currentQty = $("#quan_hide_" + pid).val();
            newQty = $("#quan_" + pid).val();
        }

        if (currentQty === newQty) {
            $("#loading").hide();
            return false;
        }
        else {
            qty = newQty - currentQty;
            isUpdate = true;
        }
    }

    if (qty === 0) {
        isDelete = true;
    }

    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/ShoppingCart/UpdateCurrentCart",
        data: {
            CartId: shoppingCartId,
            ProductId: pid,
            Quantity: qty
        },
        async: true,
        dataType: "json",
        timeout: 30000,
        success: function (response) {
            if (response.success === true) {
                loadDataToCart(response);
                if (URL.indexOf("/shop/shoppingcart") !== -1) {
                    loadCartToShoppingCart(response);
                }
                if (URL.indexOf("/shop/checkout") !== -1) {
                    loadCartToCheckout(response);
                }
                $("#loading").hide();

                if (isDelete) {
                    openMessage("The item has been removed out of your shopping cart!");
                }
                else if (isUpdate) {
                    openMessage("Your shopping cart has been updated successfully!");
                }
                else {
                    openShoppingModal();
                }
            }
            else {
                openMessage("Cannot add item to cart. There were some errors in the process! </br>" + response.message);
                $("#loading").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            openMessage(xhr.status + " / " + thrownError);
            $("#loading").hide();
        }
    });
}

function processCheckout() {
    $("#loading").show();
    var shoppingCartId = getCookie(SHOPPING_CART_ID);
    var currentUserId = getCookie(CURRENT_USER_ID);
    var orderId = getCookie(ORDER_ID);

    var val = $("#deliveryToDeifferentAddress").is(":checked");
    if (val === false) {
        copyBillingToDeliveryForm();
    }

    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/Order/Checkout",
        data: {
            //Order - checkout
            Id: orderId !== "" ? orderId : 0,
            CartId: shoppingCartId !== "" ? shoppingCartId : 0,
            UserId: currentUserId !== "" ? currentUserId : 0,
            FirstName: $("#billingFirstName").val(),
            LastName: $("#billingLastName").val(),
            CompanyName: $("#billingCompanyName").val(),
            Address: $("#billingAddress").val() + " " + $("#billingAddress2").val(),
            Suburb: $("#billingSuburb").val(),
            Postcode: $("#billingPostcode").val(),
            State: $("#billingState").val(),
            Country: $("#billingCountry").val(),
            Email: $("#emailAddress").val(),
            Phone: $("#phone").val(),
            DeliveryFirstName: $("#deliveryFirstName").val(),
            DeliveryLastName: $("#deliveryLastName").val(),
            DeliveryCompanyName: $("#deliveryCompanyName").val(),
            DeliveryAddress: $("#deliveryAddress").val() + " " + $("#deliveryAddress2").val(),
            DeliverySuburb: $("#deliverySuburb").val(),
            DeliveryPostcode: $("#deliveryPostcode").val(),
            DeliveryState: $("#deliveryState").val(),
            DeliveryCountry: $("#deliveryCountry").val(),
            Note: $("#order_note").val(),
            //Payment
            Method: PAYMENT_METHOD,
            CardFirstName: $("#cardFirstName").val(),
            CardLastName: $("#cardLastName").val(),
            CardNumber: $("#cardNumber").val(),
            MonthExpire: $("#monthExpire").val(),
            YearExpire: $("#yearExpire").val(),
            CCV: $("#ccv").val(),
            CreditCardType: "visa",
            EVoucherSerialNo: $("#evoucherSerialNo").val(),
            ActivationCode: $("#evoucherActivationCode").val()
        },
        dataType: "json",
        timeout: 35000,
        success: function (response) {
            if (response.success === true) {
                if (response.urlReturn !== "" && response.urlReturn !== "EVoucher") {
                    $("#loading").hide();
                    window.location.href = response.urlReturn;
                }
                else if (response.urlReturn === "EVoucher") {
                    loadCurrentCart();
                    $("#loading").hide();
                    $("#evoucherBalance").val(null);
                    $("#evoucherSerialNo").val(null);
                    $("#evoucherActivationCode").val(null);
                    openMessage("Your e-voucher has been successfully added to your card!");
                }
                else {
                    $("#loading").hide();
                    window.location.href = "/shop/checkout/orderconfirmation?id=" + orderId;
                }
            }
            else {
                openMessage("Cannot make a payment. There were some errors in the process!");
                $("#loading").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            openMessage(xhr.status + " / " + thrownError);
            $("#loading").hide();
        }
    });
}

function copyBillingToDeliveryForm() {
    $("#deliveryFirstName").val($("#billingFirstName").val());
    $("#deliveryLastName").val($("#billingLastName").val());
    $("#deliveryCompanyName").val($("#billingCompanyName").val());
    $("#deliveryAddress").val($("#billingAddress").val());
    $("#deliverySuburb").val($("#billingSuburb").val());
    $("#deliveryPostcode").val($("#billingPostcode").val());
    $("#deliveryState").val($("#billingState").val());
    $("#deliveryCountry").val($("#billingCountry").val());
}

function clearDeliveryForm() {
    $("#deliveryFirstName").val("");
    $("#deliveryLastName").val("");
    $("#deliveryCompanyName").val("");
    $("#deliveryAddress").val("");
    $("#deliveryAddress2").val("");
    $("#deliverySuburb").val("");
    $("#deliveryPostcode").val("");
    $("#deliveryState").val("");
    $("#deliveryCountry").val("");
}

function choosePaymentMethod(value) {
    if (value === 1) {
        PAYMENT_METHOD = "CreditCard";
    }
    else if (value === 2) {
        PAYMENT_METHOD = "PayPal";
    }
    else if (value === 3) {
        PAYMENT_METHOD = "EVoucher";
    }
    else if (value === 4) {
        PAYMENT_METHOD = "AfterPay";
    }
    else {
        PAYMENT_METHOD = "ZipPay";
    }
}

function loadOrderConfirmation() {
    $("#loading").show();
    var orderId = getCookie(ORDER_ID);

    if (orderId === null || orderId === "") {
        orderId = getUrlParameter('id');
    }

    $.ajax({
        cache: false,
        type: "GET",
        url: DOMAIN + "/Order/GetOrderConfirmation",
        data: {
            orderId: orderId !== "" ? orderId : 0
        },
        dataType: "json",
        timeout: 35000,
        success: function (response) {
            if (response.success === true) {

                $("#helloOrderConfirmation").html("");
                $("#helloOrderConfirmation").append("&nbsp; Hello " + response.data.Checkout.FirstName + " " + response.data.Checkout.LastName + ",");

                $("#thankyouPayment").html("");
                $("#thankyouPayment").append("Your order " + response.data.Checkout.Id + " is currently being processed. We'll send you an email when your order is on its way.");

                $("#productListOrderConfirmation").html("");
                var list = "";

                $.each(response.data.ShoppingCart.CartItemList, function (k, v) {
                    var item = '<tr><td class="pro-thumbnail"><a href="/shop/product?productId=' + v.ProductId + '"><img src="' + v.PrimaryImage + '" alt="" /></a></td><td class="pro-title"><a href="/shop/product?productId=' + v.ProductId + '">' + v.Name + '</a></td><td class="pro-price"><span class="amount">$' + v.DiscountPrice + '</span></td><td class="pro-quantity">' + v.Quantity + '</td><td class="pro-subtotal">$' + v.FinalPrice + '</td>';

                    list += item;
                });

                $("#productListOrderConfirmation").append(list);

                $("#subTotalOrderConfirmation").html("");
                $("#subTotalOrderConfirmation").append("$" + response.data.ShoppingCart.TotalAmountExGst);

                $("#gstOrderConfirmation").html("");
                $("#gstOrderConfirmation").append("$" + response.data.ShoppingCart.Gst);

                $("#shippingFeeOrderConfirmation").html("");
                $("#shippingFeeOrderConfirmation").append("$" + response.data.ShoppingCart.ShippingFee);

                $("#cartDiscountOrderConfirmation").html("");
                $("#cartDiscountOrderConfirmation").append("- $" + response.data.ShoppingCart.Discount);

                $("#totalOrderConfirmation").html("");
                $("#totalOrderConfirmation").append("$" + response.data.ShoppingCart.TotalFinalPrice);

                $("#billingOrderConfirmation").html("");
                $("#billingOrderConfirmation").append("<span>" + response.data.Checkout.FirstName + " " + response.data.Checkout.LastName + "</span><br /><span>" + response.data.Checkout.CompanyName + "</span > <br /><span>" + response.data.Checkout.Address + "</span > <br /><span>" + response.data.Checkout.Suburb + ", " + response.data.Checkout.State + " " + response.data.Checkout.Postcode + "</span> <br /><span>" + response.data.Checkout.Country + "</span><br /><span>" + response.data.Checkout.Email + "</span> <br /><span>" + response.data.Checkout.Phone + "</span>");

                $("#deliveryOrderConfirmation").html("");

                var deliveryAdd = "<span>" + response.data.Checkout.DeliveryFirstName + " " + response.data.Checkout.DeliveryLastName + "</span><br /><span>" + response.data.Checkout.CompanyName + "</span > <br /><span>" + response.data.Checkout.DeliveryAddress + "</span > <br /><span>" + response.data.Checkout.DeliverySuburb + ", " + response.data.Checkout.DeliveryState + " " + response.data.Checkout.DeliveryPostcode + "</span> <br /><span>" + response.data.Checkout.DeliveryCountry + "</span>";

                if (response.data.Checkout.ShippingMethod == "CLICK_AND_COLLECT") {
                    var clc = '<img src="/images/app/clickandcollect.svg" width="160" />';
                    $("#deliveryOrderConfirmation").append(clc);
                }
                else if (response.data.Checkout.ShippingMethod == "HOME_MAIL") {
                    $("#deliveryOrderConfirmation").append(deliveryAdd + '<img src="/images/app/homemail.svg" width="160" />');
                }
                else {
                    $("#deliveryOrderConfirmation").append(deliveryAdd + '<img src="/images/app/auspost_logo.png" width="100" />');
                }

                $("#totalItemsInCartBadge").val(0);

                clearDataToCart();
                $("#loading").hide();
            }
            else {
                openMessage(response.message);
                $("#loading").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            openMessage(xhr.status + " / " + thrownError);
            $("#loading").hide();
        }
    });
}

function loadCurrentOrder() {
    $("#loading").show();
    var orderId = getCookie(ORDER_ID);

    $.ajax({
        cache: false,
        type: "GET",
        url: DOMAIN + "/Order/GetOrderByOrderId",
        data: {
            orderId: orderId !== "" ? orderId : 0
        },
        dataType: "json",
        timeout: 35000,
        async: true,
        success: function (response) {
            if (response.success === true) {
                $("#billingCountry").val(response.data.Checkout.Country);
                $("#billingFirstName").val(response.data.Checkout.FirstName);
                $("#billingLastName").val(response.data.Checkout.LastName);
                $("#billingCompanyName").val(response.data.Checkout.CompanyName);
                $("#billingAddress").val(response.data.Checkout.Address);
                $("#billingSuburb").val(response.data.Checkout.Suburb);
                $("#billingState").val(response.data.Checkout.State);
                $("#billingPostcode").val(response.data.Checkout.Postcode);
                $("#emailAddress").val(response.data.Checkout.Email);
                $("#phone").val(response.data.Checkout.Phone);
                $('input:radio[name=shippingMethod]').val([response.data.Checkout.ShippingMethod]);

                if (response.data.Checkout.DeliveryToDeifferentAddress) {
                    $("#deliveryToDeifferentAddress").prop("checked", response.data.Checkout.DeliveryToDeifferentAddress);
                    $("#deliveryCountry").val(response.data.Checkout.DeliveryCountry);
                    $("#deliveryFirstName").val(response.data.Checkout.DeliveryFirstName);
                    $("#deliveryLastName").val(response.data.Checkout.DeliveryLastName);
                    $("#deliveryCompanyName").val(response.data.Checkout.DeliveryCompanyName);
                    $("#deliveryAddress").val(response.data.Checkout.DeliveryAddress);
                    $("#deliverySuburb").val(response.data.Checkout.DeliverySuburb);
                    $("#deliveryState").val(response.data.Checkout.DeliveryState);
                    $("#deliveryPostcode").val(response.data.Checkout.DeliveryPostcode);

                    $("#shipping_form").show();
                }

                $("#loading").hide();
            }
            else {
                $("#loading").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
            $("#loading").hide();
        }
    });
}

function getCurrentUser() {
    $("#loading").show();

    $.ajax({
        cache: false,
        type: "GET",
        url: DOMAIN + "/UserProfile/GetCurrentUser",
        dataType: "json",
        timeout: 35000,
        async: true,
        success: function (response) {
            if (response.success === true) {
                $("#billingCountry").val(response.data.Country);
                $("#billingFirstName").val(response.data.FirstName);
                $("#billingLastName").val(response.data.LastName);
                $("#billingCompanyName").val(response.data.CompanyName);
                $("#billingAddress").val(response.data.Address);
                $("#billingSuburb").val(response.data.Suburb);
                $("#billingState").val(response.data.State);
                $("#billingPostcode").val(response.data.Postcode);
                $("#emailAddress").val(response.data.Email);
                $("#phone").val(response.data.Phone);
                $("#loading").hide();
            }
            else {
                $("#loading").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
            $("#loading").hide();
        }
    });
}

function saveOrderAndReCalculate(openPaymentForm) {
    $("#loading").show();

    var shoppingCartId = getCookie(SHOPPING_CART_ID);
    var currentUserId = getCookie(CURRENT_USER_ID);
    var orderId = getCookie(ORDER_ID);

    var billingFirstName = $("#billingFirstName").val();
    var billingLastName = $("#billingLastName").val();
    var billingAddress = $("#billingAddress").val();
    var billingSuburb = $("#billingSuburb").val();
    var billingPostcode = $("#billingPostcode").val();
    var billingState = $("#billingState").val();
    var billingCountry = $("#billingCountry").val();

    if (billingFirstName === null || billingFirstName === '' || billingLastName === null || billingLastName === '' || billingAddress === null || billingAddress === '' || billingSuburb === null || billingSuburb === '', billingPostcode === null || billingPostcode === '' || billingState === null || billingState === '' || billingCountry === null || billingCountry === '') {
        openMessage("Please fill up all your personal information before choose shipping method or place order!");
        $("#loading").hide();
        return false;
    }

    var val = $("#deliveryToDeifferentAddress").is(":checked");
    if (val === false) {
        copyBillingToDeliveryForm();
    }

    var shippingMethod = $('input[name=shippingMethod]:checked').val();

    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/Order/PlaceOrder",
        data: {
            //Order - checkout
            Id: orderId !== "" ? orderId : 0,
            CartId: shoppingCartId !== "" ? shoppingCartId : 0,
            UserId: currentUserId !== "" ? currentUserId : 0,
            FirstName: $("#billingFirstName").val(),
            LastName: $("#billingLastName").val(),
            CompanyName: $("#billingCompanyName").val(),
            Address: $("#billingAddress").val(),
            Suburb: $("#billingSuburb").val(),
            Postcode: $("#billingPostcode").val(),
            State: $("#billingState").val(),
            Country: $("#billingCountry").val(),
            Email: $("#emailAddress").val(),
            Phone: $("#phone").val(),
            DeliveryFirstName: $("#deliveryFirstName").val(),
            DeliveryLastName: $("#deliveryLastName").val(),
            DeliveryCompanyName: $("#deliveryCompanyName").val(),
            DeliveryAddress: $("#deliveryAddress").val(),
            DeliverySuburb: $("#deliverySuburb").val(),
            DeliveryPostcode: $("#deliveryPostcode").val(),
            DeliveryState: $("#deliveryState").val(),
            DeliveryCountry: $("#deliveryCountry").val(),
            Note: $("#order_note").val(),
            ShippingMethod: shippingMethod
        },
        dataType: "json",
        async: true,
        timeout: 35000,
        success: function (response) {
            if (response.success === true) {
                loadCurrentCart();
                if (openPaymentForm) {
                    $(".payment-method").show("slow");
                }
                $("#loading").hide();
            }
            else {
                if (response.message === "Sorry, our HomeMail service is not available in your area. Please accept Australia Post instead of HomeMail!") {
                    $("#ausPostDelivery").prop("checked", true);
                }
                openMessage(response.message);
                $("#loading").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            openMessage(xhr.status + " / " + thrownError);
            $("#loading").hide();
        }
    });
}

function applyPromotionCode(remove, isMobile) {
    $("#loading").show();

    var shoppingCartId = getCookie(SHOPPING_CART_ID);

    var promotionCode = "REMOVE";

    if (!remove) {
        promotionCode = $(".promotionCode").val();

        if (isMobile) {
            promotionCode = $(".promotionCode_mobile").val();
        }
    }

    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/ShoppingCart/ApplyPromotionCode",
        data: {
            cartId: shoppingCartId,
            promotionCode: promotionCode
        },
        async: true,
        dataType: "json",
        timeout: 30000,
        success: function (response) {
            if (response.success === true) {
                loadDataToCart(response);

                if (URL.indexOf("/shop/shoppingcart") !== -1) {
                    loadCartToShoppingCart(response);
                }
                if (URL.indexOf("/shop/checkout") !== -1) {
                    loadCartToCheckout(response);
                }

                $("#loading").hide();

                if (!remove) {
                    openMessage("Your coupon has been added to your shopping cart!");
                }
                else {
                    openMessage("Your coupon has been removed successfully!");
                }
            }
            else {
                debugger;
                if (response.message != null && response.message != "") {
                    openMessage(response.message);
                }
                else {
                    openMessage("Cannot add this coupon to your cart. There were some errors in the process!");
                }

                $("#loading").hide();
            }

            $(".promotionCode").val(null);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            openMessage(xhr.status + " / " + thrownError);
            $("#loading").hide();
        }
    });
}

function checkEVoucherBalance() {
    $("#loading").show();
    $("#evoucherBalance").val(null);
    var serialNo = $("#evoucherSerialNo").val();
    var activationCode = $("#evoucherActivationCode").val();

    if (serialNo && serialNo !== "" && activationCode && activationCode !== "") {
        $.ajax({
            cache: false,
            type: "GET",
            url: DOMAIN + "/EVoucher/CheckBalance",
            dataType: "json",
            data: {
                serialNo: serialNo,
                activationCode: activationCode
            },
            timeout: 35000,
            async: true,
            success: function (response) {
                if (response.success === true) {
                    $("#evoucherBalance").val("$" + response.data);
                    $("#loading").hide();
                }
                else {
                    openMessage(response.message);
                    $("#loading").hide();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
                $("#loading").hide();
            }
        });
    }
    else {
        openMessage("Please enter your evoucher information before checking balance!");
        $("#loading").hide();
    }
}

function addToCartWithoutAnyMessage(pids, qty) {
    var shoppingCartId = getCookie(SHOPPING_CART_ID);

    var addToCart = $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/ShoppingCart/AddWishlistToCart",
        async: true,
        data: {
            CartId: shoppingCartId !== "" ? shoppingCartId : 0,
            ProductIds: pids,
            Quantity: qty
        },
        dataType: "json",
        timeout: 60000,
        success: function (response) {
            if (response.success === true) {
                loadCurrentCart();
                $("#loading").hide();
                openShoppingModal();
            }
            else {
                loadCurrentCart();
                $("#loading").hide();
                openMessage("Cannot add this item to cart. There were some errors in the process! </br>" + response.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            openMessage(xhr.status + " / " + thrownError);
        }
    });
    return addToCart.promise();
}

function addWishlistToCart() {
    $("#loading").show();

    var pIds = [];
    var promises = [];

    $('tr[class="productWl"]').each(function (index, item) {
        pIds.push(item.dataset.index);
    });

    if (pIds.length > 0) {
        addToCartWithoutAnyMessage(pIds, 1);
    }
    else {
        $("#loading").hide();
        openMessage("You don't have any item in wishlist!");
    }
}