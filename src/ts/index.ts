import { getRecaptchaToken, requiresRecaptcha } from './add-ins/recaptcha';
import { UiActions } from './add-ins/uiActions';
import { CollectFieldsAutomatic } from './collect-fields/auto';

// so it can be called from the HTML when content re-initializes dynamically
const winAny = (window as any);
winAny.appEvents6 ??= {};
winAny.appEvents6.init ??= initAppEvents6;

let Pristine = require('../../node_modules/pristinejs')

const debug = true;

declare let $2sxc: any;

function initAppEvents6({ domAttribute } : { domAttribute: string }) {
  if (debug) console.log("Events6 loading, debug is enabled");

  const eventsWrapper = document.querySelectorAll(`[${domAttribute}]`)[0];

  if (document.getElementsByTagName('form').length) document.getElementsByTagName('form')[0]?.setAttribute('novalidate', '');

  eventsWrapper.querySelectorAll('[app-events6-send]')[0].addEventListener('click', (event: Event) => {
    event.preventDefault();
    var valid = validate(eventsWrapper, event);
    if (valid) {
      new CollectFieldsAutomatic().collect(eventsWrapper, sendWhenReady);
    }
  })

  // if (eventsWrapper?.getElementsByClassName('btn-send-events-form').length) {
  //   eventsWrapper.getElementsByClassName('app-mobius5-send]')[0].addEventListener('click', (event: Event) => {
  //     event.preventDefault();
  //     send(eventsWrapper, event)
  //   })
  // }
}

function validate(wrapper: Element, event: Event): boolean {
  const helperFunc = new UiActions();
  const pristine = new Pristine(wrapper);
  const btn = event.currentTarget as HTMLElement;
  const label = btn.innerText;

  helperFunc.showOneAlert(wrapper, '');

  const trackingEvent = new CustomEvent('trackEventsForm', { detail: { category: 'events-form', action: 'submit', label: label } });
  document.dispatchEvent(trackingEvent);
  
  const valid = pristine.validate();
  if (!valid)
    helperFunc.showOneAlert(wrapper, 'msgIncomplete');

  return valid;
}

async function sendWhenReady(data: any, wrapper: HTMLElement) {
  const helperFunc = new UiActions();
  
  if (requiresRecaptcha(wrapper)) {
    let token = await getRecaptchaToken(wrapper)
    if (!token) return helperFunc.showOneAlert(wrapper, 'msgRecap');    

    // set token for backend
    data.Recaptcha = token;
  }

  helperFunc.disableInputs(wrapper, true);
  helperFunc.showOneAlert(wrapper, 'msgSending');

  sendForm(data, wrapper)
}

function sendForm(data: any, wrapper: HTMLElement) {
  const helperFunc = new UiActions();
  const ws = (wrapper as HTMLElement).dataset.webservice; // should be "Form/ProcessForm" or a custom override
  const btn = (wrapper.querySelectorAll('[app-events6-send]')[0] as HTMLButtonElement);
  const label = btn.innerText;
  const sxc = $2sxc(btn);
  
  if(debug) console.log(data);
  
  var saveCall = sxc.webApi.post(ws, null, data, true);
  
  function showSuccess() {
    helperFunc.showOneAlert(wrapper, 'msgOk');
    helperFunc.showConfigWarnings(wrapper);
    
    // Send browser event in case an Analytics-listener wants to get notified
    const trackingEvent = new CustomEvent('trackEventsForm', { detail: { category: 'events-form', action: 'success', label: label } });
    document.dispatchEvent(trackingEvent);
  }
  
  function showError() {
    helperFunc.showOneAlert(wrapper, 'msgError');
    helperFunc.showConfigWarnings(wrapper);
    helperFunc.disableInputs(wrapper, false);

    // Send browser event in case an Analytics-listener wants to get notified
    const trackingEvent = new CustomEvent('trackEventsForm', { detail: { category: 'events-form', action: 'error', label: label } });
    document.dispatchEvent(trackingEvent);
  }
  
  saveCall.success((successData: unknown) => {
    if(debug) console.log('success', successData);
    btn.setAttribute("disabled", "")
    showSuccess();
  });
  
  saveCall.error((errorData: unknown) => {
    if(debug) console.log('error', errorData);
    showError();
  });
}

// function send(wrapper: Element, event: Event) {
//   const helperFunc = new UiActions();
//   const collectFieldsAutomatic = new CollectFieldsAutomatic();
//   const pristine = new Pristine(wrapper);
//   const btn = event.currentTarget as HTMLElement;
//   const sxc = $2sxc(btn);
//   const label = btn.innerText;

//   helperFunc.showOneAlert(wrapper, '');

//   const trackingEvent = new CustomEvent('trackEventsForm', { detail: { category: 'events-form', action: 'submit', label: label } });
//   document.dispatchEvent(trackingEvent);
  
//   const valid = pristine.validate();
//   if (!valid)
//     return helperFunc.showOneAlert(wrapper, 'msgIncomplete');

//   const ws = "Form/ProcessForm";
//   let data = collectFieldsAutomatic.collect(wrapper);

//   helperFunc.disableInputs(wrapper, true);
//   helperFunc.showOneAlert(wrapper, 'msgSending');

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
// }