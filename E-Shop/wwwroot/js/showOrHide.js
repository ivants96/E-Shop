function showOrHide() {
    var checkBox = document.getElementById("DeliveryAddressIsAddress");
    var delAddress = document.getElementById("deliveryaddress");
    if (checkBox.checked == true) {
        delAddress.style.display = "none";
    } else {
        delAddress.style.display = "block";
    }
}
