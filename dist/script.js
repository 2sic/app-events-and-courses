$(function() {
    $(".app-categories").click(function(e) {
        e.stopPropagation();
    });
    $(".app-event-item .btn.btn-primary").click(function(e) {
        e.stopPropagation();
    });
});