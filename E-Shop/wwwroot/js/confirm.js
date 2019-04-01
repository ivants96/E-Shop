$(document).ready(function () {
    $("a[data-confirm]").click(function (e) {
        if (!confirm($(this).data('confirm')))
            e.preventDefault();
    });
});