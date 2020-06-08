declare let $2sxc: any;
declare let fancybox: any;

require('../styles/_styles.scss');

$(document).ready(function() {
  const validator = ($("form") as any).validate();

  jQuery.extend(($ as any).validator.messages, {
      required: $('.app-course-form').data('string-required'),
      email: $('.app-course-form').data('string-email')
  });

  $(".cf-submit").click(function(e) { 
          
    if(validator.form()) {
        const inputs = $(".cf-wrapper :input");
        const obj: any = {};
        $.each(inputs, function(i: any, e: any) {
          const propName = e.name;
          obj[propName] = $(e).val();
        });
        
        const sxc = $2sxc(this);
        sxc.webApi.post("Form/ProcessForm", {}, obj).then(function() {
          $('.cf-wrapper').hide();
          $('.cf-info-sent').show();$
          $('.cf-submit').prop('disabled', true);
        }, function() {
          $('.cf-info-error').show();
          $('.cf-submit').prop('disabled', false);
        });

    }

    // Add and remove has-error classes
    $('.cf-wrapper .form-group').removeClass('has-error');
    $('.cf-info-error').hide(); 
    for (let i=0;i<validator.errorList.length;i++){
        $(validator.errorList[i].element).parents('.form-group').addClass('has-error');
    }
  });
});

$(function() {
  /* Fancybox */
  if (($('.fancybox') as any).fancybox) {
    ($('.fancybox') as any).fancybox();
  }
});
