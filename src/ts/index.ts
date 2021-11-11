import { showAlert, showConfigWarnings } from './lib-2sxc-alerts';
import { disableInputs, getFormValues, sendForm, validateForm } from './lib-2sxc-forms';
import { getRecaptchaToken, requiresRecaptcha } from './lib-2sxc-recaptcha';
import { addTrackingEvent } from './lib-2sxc-tracking';

// so it can be called from the HTML when content re-initializes dynamically
const winAny = (window as any)
winAny.appEvents6 ??= {}
winAny.appEvents6.init ??= initAppEvents6

const debug = true

function initAppEvents6({ domAttribute } : { domAttribute: string }) {
  if (document.getElementsByTagName('form').length) document.getElementsByTagName('form')[0]?.setAttribute('novalidate', '')
  if (debug) console.log("Events6 loading, debug is enabled");

  const eventsWrapper = document.querySelectorAll(`[${domAttribute}]`)[0];
  eventsWrapper.querySelectorAll('[app-events6-send]')[0].addEventListener('click', async (event: Event) => {
    event.preventDefault();

    const eventBtn = event.currentTarget as HTMLElement;
    addTrackingEvent('trackEventsForm', 'events-form', eventBtn.innerText)
    
    var valid = validateForm(eventsWrapper)
    if (!valid) {
      showAlert(eventsWrapper, 'msgIncomplete')
      return
    }
    
    const formValues = await getFormValues(eventsWrapper)

    if (requiresRecaptcha(eventsWrapper)) {
      let token = await getRecaptchaToken(eventsWrapper)
      if (!token) return showAlert(eventsWrapper, 'msgRecap')
  
      // set token for backend
      formValues.Recaptcha = token
    }


    // imply that message is sending by ui modifications 

    disableInputs(eventsWrapper, true)
    showAlert(eventsWrapper, 'msgSending')

    const result = sendForm(formValues, eventsWrapper)

    //#region request handling
    
    result.success((successData: unknown) => {
      if(debug) console.log('success', successData)
      let btn = (eventsWrapper.querySelectorAll('[app-events6-send]')[0] as HTMLButtonElement)
      btn.setAttribute("disabled", "")

      showAlert(eventsWrapper, 'msgOk')
      showConfigWarnings(eventsWrapper, 'app-events6-config-warning')
      disableInputs(eventsWrapper, false)

      addTrackingEvent('trackEventsForm', 'events-form', btn.innerText)
    });
  
    result.error((errorData: unknown) => {
      if(debug) console.log('error', errorData);
      let btn = (eventsWrapper.querySelectorAll('[app-events6-send]')[0] as HTMLButtonElement)

      showAlert(eventsWrapper, 'msgError')
      showConfigWarnings(eventsWrapper, 'app-events6-config-warning')

      addTrackingEvent('trackEventsForm', 'events-form', btn.innerText)
    });

    //#endregion
  })
}
