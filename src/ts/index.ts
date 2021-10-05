import { UiActions } from './add-ins/uiActions';
import { CollectFieldsAutomatic } from './collect-fields/auto';

// so it can be called from the HTML when content re-initializes dynamically
const winAny = (window as any);
winAny.appEvents6 ??= {};
winAny.appEvents6.init ??= initAppEvents6;

let Pristine = require('../../node_modules/pristinejs')

declare let $2sxc: any;

function initAppEvents6({ domId } : { domId: string }) {
  const mobuisWrapper = document.getElementsByClassName(domId)[0];
  console.log(mobuisWrapper);

  if (document.getElementsByTagName('form').length) {
    document.getElementsByTagName('form')[0]?.setAttribute('novalidate', '');
  }

  mobuisWrapper.getElementsByClassName('btn-send-events-form')[0].addEventListener('click', (event: Event) => {
    event.preventDefault();
    send(mobuisWrapper, event)
  })
}

function send(wrapper: Element, event: Event) {
  const helperFunc = new UiActions();
  const collectFieldsAutomatic = new CollectFieldsAutomatic();
  const pristine = new Pristine(wrapper);
  const btn = event.currentTarget as HTMLElement;
  const sxc = $2sxc(btn);
  const label = btn.innerText;

  helperFunc.showOneAlert(wrapper, '');

  const trackingEvent = new CustomEvent('trackEventsForm', { detail: { category: 'events-form', action: 'submit', label: label } });
  document.dispatchEvent(trackingEvent);
  
  const valid = pristine.validate();
  if (!valid)
    return helperFunc.showOneAlert(wrapper, 'msgIncomplete');

  const ws = (wrapper as HTMLElement).dataset.webservice; // should be "Form/ProcessForm" or a custom override
  // get data
  // alternative example with manual build, but we prefer automatic
  // const collectFieldsManual = new CollectFieldsManual();
  // let data;
  // data = collectFieldsManual.collect(wrapper);

  let data = collectFieldsAutomatic.collect(wrapper);
  console.log(data);

  helperFunc.disableInputs(wrapper, true);
  helperFunc.showOneAlert(wrapper, 'msgSending');

//   sxc.webApi.post(ws, null, data, true)
//     .success(() => {
//       const msg = 'msgOk';
//       helperFunc.showOneAlert(wrapper, msg);

//       const trackingEvent = new CustomEvent('trackEventsForm', { detail: { category: 'events-form', action: 'success', label: label } });
//       document.dispatchEvent(trackingEvent);
//     })
//     .error(() => {
//       const msg = 'msgError';
//       helperFunc.showOneAlert(wrapper, msg);
//       helperFunc.disableInputs(wrapper, false);

//       const trackingEvent = new CustomEvent('trackEventsForm', { detail: { category: 'events-form', action: 'error', label: label } });
//       document.dispatchEvent(trackingEvent);
//     });
}

// declare let $2sxc: any;
// import * as $ from 'jquery';
// const validate = require("jquery-validation");

// require('../styles/_styles.scss');

// $(document).ready(function() {
//   const validator = ($("form") as any).validate();

//   jQuery.extend(($ as any).validator.messages, {
//       required: $('.app-events-form').data('string-required'),
//       email: $('.app-events-form').data('string-email')
//   });

//   $(".form-submit").click(function(e) { 
//     if(validator.form()) {
//       const inputs = $(".event-form :input");
//       const obj: any = {};
//       $.each(inputs, function(i: any, e: any) {
//         const propName = e.name;

//         if ($(e).attr('type') && $(e).attr('type').toLowerCase() == 'radio') { // For radio fields get checked values
//           if ($(e).is(':checked')) {
//             obj[propName] = $(e).val();
//           }
//         } else {
//           obj[propName] = $(e).val();
//         }
//       });

//       const sxc = $2sxc(this);
//       sxc.webApi.post("Form/ProcessForm", {}, obj).then(function() {
//         $('.event-form').hide();
//         $('.form-info-success').show();
//         $('.form-submit').prop('disabled', true);
//       }, function() {
//         $('.form-info-error').show();
//         $('.form-submit').prop('disabled', false);
//       });
//     }

//     // Add and remove has-error classes
//     $('.event-form .form-group').removeClass('has-error');
//     $('.form-info-error').hide(); 
//     for (let i=0;i<validator.errorList.length;i++){
//         $(validator.errorList[i].element).parents('.form-group').addClass('has-error');
//     }
//   });
// });