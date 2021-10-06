import { UiActions } from './add-ins/uiActions';
import { CollectFieldsAutomatic } from './collect-fields/auto';

// so it can be called from the HTML when content re-initializes dynamically
const winAny = (window as any);
winAny.appEvents6 ??= {};
winAny.appEvents6.init ??= initAppEvents6;

let Pristine = require('../../node_modules/pristinejs')

declare let $2sxc: any;

function initAppEvents6({ domId } : { domId: string }) {
  const eventsWrapper = document.getElementsByClassName(domId)[0];

  if (document.getElementsByTagName('form').length) {
    document.getElementsByTagName('form')[0]?.setAttribute('novalidate', '');
  }

  if (eventsWrapper?.getElementsByClassName('btn-send-events-form').length) {
    eventsWrapper.getElementsByClassName('btn-send-events-form')[0].addEventListener('click', (event: Event) => {
      event.preventDefault();
      send(eventsWrapper, event)
    })
  }
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

  const ws = "Form/ProcessForm";
  let data = collectFieldsAutomatic.collect(wrapper);

  helperFunc.disableInputs(wrapper, true);
  helperFunc.showOneAlert(wrapper, 'msgSending');

  sxc.webApi.post(ws, null, data, true)
    .success(() => {
      const msg = 'msgOk';
      helperFunc.showOneAlert(wrapper, msg);

      const trackingEvent = new CustomEvent('trackEventsForm', { detail: { category: 'events-form', action: 'success', label: label } });
      document.dispatchEvent(trackingEvent);
    })
    .error(() => {
      const msg = 'msgError';
      helperFunc.showOneAlert(wrapper, msg);
      helperFunc.disableInputs(wrapper, false);

      const trackingEvent = new CustomEvent('trackEventsForm', { detail: { category: 'events-form', action: 'error', label: label } });
      document.dispatchEvent(trackingEvent);
    });
}