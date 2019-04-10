HTMLCollection.prototype.indexOf = [].indexOf;

$(function () {
    var stars = $(".reviewStar");
    stars.click(setStars);
    stars.mouseenter(starHover);
    stars.mouseleave(returnStarState);
    /*If user doesn't set stars value, set default value to 3*/
    setStars.call(stars[0]);
});

function starHover() {
    var stars = $(".reviewStar");
    var selectedStars = this.parentElement.children.indexOf(this) + 1;   
    /*Set starst to empty*/
    stars.addClass("fa-star-o");
    stars.removeClass("fa-star text-warning");
    /* Lights up all stars till the one that user hovers above  */
    for (var i = 0; i < selectedStars; i++) {
        $(stars[i]).removeClass("fa-star-o");
        $(stars[i]).addClass("fa-star text-warning");
    }
}

/*If user's not hovering above stars set them to default or clicked value*/
function returnStarState() {
    var stars = $(".reviewStar");
    stars.addClass("fa-star-o");
    stars.removeClass("fa-star text-warning");  
    for (var i = 0; i < $("#rating")[0].value; i++) {
        $(stars[i]).removeClass("fa-star-o");
        $(stars[i]).addClass("fa-star text-warning");
    }
}

/*Save value of clicked stars to input for rating */
function setStars() {
    var selectedStars = this.parentElement.children.indexOf(this);
    $("#rating")[0].value = selectedStars + 1;

    returnStarState();
}
