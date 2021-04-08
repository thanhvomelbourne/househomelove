var DOMAIN = location.protocol + "//" + location.host;
var ITEMS_PER_PAGE = getCookie("ITEMS_PER_PAGE") !== "" ? getCookie("ITEMS_PER_PAGE") : 12;
var SORT_BY = getCookie("SORT_BY") !== "" ? getCookie("SORT_BY") : "";

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
};

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
};

$(document).ready(function () {
    $(".main-menu ul a")
        .click(function (e) {
            var link = $(this);

            var item = link.parent("li");

            if (item.hasClass("active")) {
                item.removeClass("active").children("a").removeClass("active");
            } else {
                item.addClass("active").children("a").addClass("active");
            }
        })
        .each(function () {
            var link = $(this);
            if (link.get(0).href === location.href) {
                link.addClass("active").parents("li").addClass("active");
                //return false;
            }
        });

    $("#selectItemsPerPage").change(function () {
        ITEMS_PER_PAGE = $("#selectItemsPerPage").val();
        setCookie("ITEMS_PER_PAGE", ITEMS_PER_PAGE, 100);
        location.reload();
    });

    $("#sortBy").change(function () {
        SORT_BY = $("#sortBy").val();
        setCookie("SORT_BY", SORT_BY, 100);
        location.reload();
    });

    $('#new-review').autosize({ append: "\n" });

    var reviewBox = $('#post-review-box');
    var newReview = $('#new-review');
    var openReviewBtn = $('#open-review-box');
    var closeReviewBtn = $('#close-review-box');
    var ratingsField = $('#ratings-hidden');

    openReviewBtn.click(function (e) {
        reviewBox.slideDown(400, function () {
            $('#new-review').trigger('autosize.resize');
            newReview.focus();
        });
        openReviewBtn.fadeOut(100);
        closeReviewBtn.show();
    });

    closeReviewBtn.click(function (e) {
        e.preventDefault();
        reviewBox.slideUp(300, function () {
            newReview.focus();
            openReviewBtn.fadeIn(200);
        });
        closeReviewBtn.hide();

    });

    $('.starrr').on('starrr:change', function (e, value) {
        ratingsField.val(value);
    });
});

function subcribe(email) {

    var data = email.val();

    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/Subcription/Subcribe",
        async: false,
        data: {
            EmailSubcribed: data
        },
        dataType: "json",
        timeout: 5000,
        success: function (response) {
            if (response.success == true) {
                openMessage(response.message);
                email.val("");
            }
            else {
                openMessage(response.error);
            }
        },
    });

    event.preventDefault();
};

function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

function openMessage(message) {
    var modal = $('#messageModal');
    var modalContent = $('.modal-content');
    $("#messageContent").html("");
    $("#messageContent").html(message);
    modal.css({ display: 'block' }).animateCss('fadeIn');
    modalContent.animateCss('rollIn');
    isShowModal = true;
};

function closeMessage() {
    var modal = $('#messageModal');
    var modalContent = $('.modal-content');
    modalContent.animateCss('bounceOutLeft');
    modal.animateCss('fadeOut', function () {
        modal.css({ display: 'none' });
    });
    isShowModal = false;
};

function openShoppingModal() {
    var modal = $('#shoppingModal');
    var modalContent = $('.modal-content-shopping');
    modal.css({ display: 'block' }).animateCss('fadeIn');
    modalContent.animateCss('rollIn');
    isShowModal = true;
};

function closeShopping() {
    var modal = $('#shoppingModal');
    var modalContent = $('.modal-content-shopping');
    modalContent.animateCss('bounceOutLeft');
    modal.animateCss('fadeOut', function () {
        modal.css({ display: 'none' });
    });
    isShowModal = false;
};

$.fn.extend({
    animateCss: function (animationName, callback) {
        var animationEnd = (function (el) {
            var animations = {
                animation: 'animationend',
                OAnimation: 'oAnimationEnd',
                MozAnimation: 'mozAnimationEnd',
                WebkitAnimation: 'webkitAnimationEnd',
            };

            for (var t in animations) {
                if (el.style[t] !== undefined) {
                    return animations[t];
                }
            }
        })(document.createElement('div'));

        this.addClass('animated ' + animationName).one(animationEnd, function () {
            $(this).removeClass('animated ' + animationName);

            if (typeof callback === 'function') callback();
        });

        return this;
    },
});

function saveContactMessage() {
    $("#loading").show();

    var $myForm = document.getElementById("contact-form");
    if (!$myForm.elements["contactName"].checkValidity() || !$myForm.elements["contactEmail"].checkValidity()) {
        $("#loading").hide();
        return false;
    }

    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/Contact/SubmitMessage",
        data: {
            ContactName: $("#contactName").val(),
            ContactPhone: $("#contactPhone").val(),
            ContactEmail: $("#contactEmail").val(),
            Subject: $("#subject").val(),
            Message: $("#message").val(),
        },
        dataType: "json",
        async: true,
        timeout: 35000,
        success: function (response) {
            if (response.success == true) {
                $("#loading").hide();
                openMessage("Your message has been sent to us! We will touch to you soon!");
                $("#contactName").val(null);
                $("#contactPhone").val(null);
                $("#contactEmail").val(null);
                $("#subject").val(null);
                $("#message").val(null);
            }
            else {
                openMessage("Your message coudn't be sent to us, please try again!");
                $("#loading").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            openMessage(xhr.status + " / " + thrownError);
            $("#loading").hide();
        }
    });

    $("#loading").hide();
    event.preventDefault();
};

function searchPrice() {
    var maxPrice = $("#price-slider-max").text().replace('$', '');
    var minPrice = $("#price-slider-min").text().replace('$', '');
    var currentSearch = getUrlParameter("CurrentSearch");
    var pageNo = getUrlParameter("PageNo");
};

function outOfStock(pId, pName) {
    openMessage("This item is out of stock, please <a class='text-primary' href='/shop/product?productId=" + pId + "&productName=" + pName + "'>click here</a> go to product detail page to pre-order!");
};

function removeWishlist(wishlistId) {
    $("#loading").show();

    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/Product/DeleteWishlist",
        data: {
            wishlistId: wishlistId
        },
        async: true,
        dataType: "json",
        timeout: 30000,
        success: function (response) {
            if (response.success === true) {
                loadWishlist();
                openMessage("Removed product out of your wishlist sucessfully!");
                $("#loading").hide();
            }
            else {
                openMessage("Cannot remove this product out of wishlist. There were some errors in the process!");
                $("#loading").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            openMessage(xhr.status + " / " + thrownError);
            $("#loading").hide();
        }
    });
};

function loadWishlist() {
    $("#loading").show();
    $("#productListWishlist").html(null);

    $.ajax({
        cache: false,
        type: "GET",
        url: DOMAIN + "/Product/GetWishlish",
        async: true,
        dataType: "json",
        timeout: 30000,
        success: function (response) {
            if (response.success === true) {
                var list = "";
                $.each(response.data, function (k, v) {

                    var wishlist = "";

                    if (v.Product.InStock === "InStock") {
                        wishlist = '<td class="text-center text-primary"><i class="	fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i></td>';
                    }
                    else if (v.Product.InStock === "LowStock") {
                        wishlist = '<td class="text-center text-warning"><i class="fa fa-star"></i><i class="fa fa-star-half-empty"></i><i class="fa fa-star-o"></i></td>';
                    }
                    else {
                        wishlist = '<td class="text-center text-danger"><i class="fa fa-star-o"></i><i class="fa fa-star-o"></i><i class="fa fa-star-o"></i></td>';
                    }

                    var item = '<tr class="productWl" data-index="' + v.ProductId + '"><td class="pro-thumbnail"><a href="/shop/product?productId=' + v.ProductId + '&productName=' + v.Product.Name + '"><img src="' + v.Product.PrimaryImage + '" alt="" /></a></td><td class="pro-title"><a href="">' + v.Product.CategoryName + '</a></td><td class="pro-title"><a href="/shop/product?productId=' + v.ProductId + '">' + v.Product.Name + '</a></td><td class="pro-title">' + v.Product.Namekey + '</td><td class="pro-price"><span class="amount">$' + v.Product.OriginalPrice + '</span></td><td class="pro-price">$' + v.Product.DiscountPrice + '</td>' + wishlist + '<td class="pro-remove"><a onclick="removeWishlist(' + v.Id + ')">×</a></td></tr > ';
                    list += item;
                });
                $("#productListWishlist").append(list);
                $("#loading").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
            $("#loading").hide();
        }
    });
};

function addWishlist(productId) {
    $("#loading").show();
    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/Product/AddProductToWishlist",
        data: {
            productId: productId
        },
        async: true,
        dataType: "json",
        timeout: 30000,
        success: function (response) {
            if (response.success === true) {
                openMessage("This item has been added successfully to your wishlist!");
                $(".wishlistLoading").html(null);
                $(".wishlistLoading").append('<a class="text-primary"><i class="fa fa-heart" style="margin-top: -2px;"></i> In your Wishlist</a>');
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
};

function likeProduct(productId) {
    $("#loading").show();
    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/Product/LikeProduct",
        data: {
            productId: productId
        },
        async: true,
        dataType: "json",
        timeout: 30000,
        success: function (response) {
            if (response.success === true) {
                $(".likeProductLoading").html(null);
                if (response.added === true) {
                    $(".likeProductLoading").append('<a onclick="likeProduct(' + productId + ')" class="text-primary"><i class= "fa fa-thumbs-o-up" style = "margin-top: -2px;" ></i>' + response.value.NumberOfLiked + ' Liked</a>');
                }
                else {
                    $(".likeProductLoading").append('<a onclick="likeProduct(' + productId + ')"><i class= "fa fa-thumbs-o-up" style = "margin-top: -2px;" ></i>' + response.value.NumberOfLiked + ' Like</a>');
                }
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
};

function reviewProduct(productId) {
    $("#loading").show();

    var rating = $("#ratings-hidden").val();
    var comment = $("#new-review").val();

    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/Product/ReviewProduct",
        data: {
            productId: productId,
            comment: comment,
            rating: rating
        },
        async: true,
        dataType: "json",
        timeout: 30000,
        success: function (response) {
            if (response.success === true) {
                bindingReview(response.data);
                $("#new-review").text(null);
                $("#new-review").val(null);
                $(".glyphicon").removeClass("glyphicon-star");
                $(".glyphicon").addClass("glyphicon-star-empty");
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

function bindingReview(data) {
    var currentUserId = getCookie(CURRENT_USER_ID);
    var isAuthenticated = false;

    if (currentUserId > 0 && currentUserId != "") {
        isAuthenticated = true;
    }

    $(".reviewSection").html(null);
    var list = '';
    $.each(data.ReviewedProduct, function (k, v) {
        var rating = '';

        if (v.Rating > 0) {
            for (var i = 1; i <= 5; i++) {
                if (i <= v.Rating) {
                    if (isAuthenticated && currentUserId == v.UserId) {
                        rating += '<i class="fa fa-star checked"></i>';
                    }
                    else {
                        rating += '<i class="fa fa-star"></i>';
                    }
                }
                else {
                    if (isAuthenticated && currentUserId == v.UserId) {
                        rating += '<i class="fa fa-star-o checked"></i>';
                    }
                    else {
                        rating += '<i class="fa fa-star-o"></i>';
                    }
                }
            }
        }

        var item = '<div class="row"><div class="col-sm-12" ><div class="review-block"><div class="row"><div class="col-sm-3 text-center"><div class="chip"><img src="' + v.UserAvatar + '" alt="Person" width="96" height="96">' + v.UserFullname + '</div><br /><br /></div><div class="col-sm-9"><div class="review-block-rate">' + rating + '</div><div class="review-block-description">' + v.Comment + '<p class="text-justify">' + v.CreatedAt + '</p></div></div ></div></div></div></div>';

        list += item;
    });
    $(".reviewSection").append(list);
}

function preOrder() {
    var modal = $('#preOrderModal');
    var modalContent = $('.modal-content-preOrder');
    modal.css({ display: 'block' }).animateCss('fadeIn');
    modalContent.animateCss('rollIn');
    isShowModal = true;
}

function closePreOrder() {
    var modal = $('#preOrderModal');
    var modalContent = $('.modal-content-preOrder');
    modalContent.animateCss('bounceOutLeft');
    modal.animateCss('fadeOut', function () {
        modal.css({ display: 'none' });
    });
    isShowModal = false;
}

function placePreOrder() {
    $("#loading").show();
    var firstName = $("#firstNamePreOrder").val();
    var lastName = $("#lastNamePreOrder").val();
    var email = $("#emailPreOrder").val();
    var phone = $("#phonePreOrder").val();
    var productId = getUrlParameter("productId");

    if (firstName === "" || firstName === undefined || lastName === "" || lastName === undefined || email === "" || email === undefined || phone === "" || phone === undefined) {
        openMessage("Please fill all required information!");
        $("#loading").hide();
        return;
    }

    $.ajax({
        cache: false,
        type: "POST",
        url: DOMAIN + "/PreOrder/CreatePreOrder",
        data: {
            FirstName: firstName,
            LastName: lastName,
            Email: email,
            Phone: phone,
            ProductId: productId
        },
        async: true,
        dataType: "json",
        timeout: 30000,
        success: function (response) {
            if (response.success === true) {
                openMessage(response.message);
                $("#loading").hide();
                closePreOrder();
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

function stringIsEmptyOrSpaces(str) {
    return str === null || str.match(/^ *$/) !== null || str === null || str === undefined;
}