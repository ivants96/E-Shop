HTMLCollection.prototype.indexOf = [].indexOf;

$(document).ready(function () {
    var stars = $(".reviewStar");
    stars.click(setStars);
    stars.mouseenter(starHover);
    stars.mouseleave(returnStarState);
    /*If user doesn't enter stars set stars default value to 3*/
    setStars.call(stars[2]);
});

function starHover() {
    var stars = $(".reviewStar");
    var selectedStars = this.parentElement.children.indexOf(this) + 1;
    /* Set starst to empty*/
    stars.addClass("fa-star-o");
    stars.removeClass("fa-star text-warning");
    /* Lights up all stars till the one user hovers above  */
    for (var i = 0; i < selectedStars; i++) {
        $(stars[i]).removeClass("fa-star-o");
        $(stars[i]).addClass("fa-star text-warning");
    }
}

/*If user's not hovering above stars set them to default value*/
function returnStarState() {
    var stars = $(".reviewStar");
    stars.addClass("fa-star-o");
    stars.removeClass("fa-star text-warning");
    /* Return stars state to default*/
    for (var i = 0; i < $("#rating")[0].value; i++) {
        $(stars[i]).removeClass("fa-star-o");
        $(stars[i]).addClass("fa-star text-warning");
    }
}

/*Save value of clicked starst to input for rating */
function setStars() {
    var selectedStars = this.parentElement.children.indexOf(this);
    $("#rating")[0].value = selectedStars + 1;

    returnStarState();
}
