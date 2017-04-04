$(document).ready(function() {
    jQuery.extend($.validator.messages, {
        required: courseRegResources.ValidationRequired,
        email: courseRegResources.ValidationEmail
    });

    var validator = $("form").validate();

    $(".cf-submit").click(function(e) {
        if(validator.form()) {
            var inputs = $(".cf-wrapper :input");
            var obj = {};
            $.each(inputs, function(i,e) {
                obj[e.name] = $(e).val();
            });
            
            var sxc = $2sxc(this);
            sxc.webApi.post("Form/ProcessForm", {}, obj).then(function() {
                $('.cf-wrapper').hide();
                $('.cf-info-sent').show();
            }, function() {
                alert(courseRegResources.CourseRegistrationError);
            });

            $('.cf-submit').prop('disabled', true);
        }

        // Add and remove has-error classes
        $('.cf-wrapper .form-group').removeClass('has-error');
        for (var i=0;i<validator.errorList.length;i++){
            $(validator.errorList[i].element).parents('.form-group').addClass('has-error');
        }
    });
});