import { showAlert, showConfigWarnings } from './lib-2sxc-alerts';
import { disableInputs, enableInputs, getFormValues, sendForm, validateForm } from './lib-2sxc-forms';
import { PristineOptions } from './lib-2sxc-pristine-options';
import { getRecaptchaToken, requiresRecaptcha } from './lib-2sxc-recaptcha';
import { addTrackingEvent } from './lib-2sxc-tracking';

// so it can be called from the HTML when content re-initializes dynamically
const winAny = (window as any)
winAny.appEvents6 ??= {}
winAny.appEvents6.init ??= initAppEvents6

const debug = false

function initAppEvents6({ domAttribute, options } : { domAttribute: string, options: PristineOptions }) {
  if (document.getElementsByTagName('form').length) document.getElementsByTagName('form')[0]?.setAttribute('novalidate', '')
  if (debug) console.log("Events6 loading, debug is enabled");

  var btnToggleExpired = document.querySelector('.js-app-events6-toggle-expired-dates');
  if(btnToggleExpired != null) {
    btnToggleExpired.addEventListener('click', () => {
      document.querySelectorAll('.event-hidden').forEach((elem: HTMLElement, index) => {
        console.log(elem.style.display)
        if (elem.style.display == '' || elem.style.display == "none") {
          elem.style.display = "block";
        } else {
          elem.style.display = "none";
        }
      })
    })
  }
  
  const eventsWrapper = document.querySelectorAll(`[${domAttribute}]`)[0];
  if(!eventsWrapper) return

  const submitButton = (eventsWrapper.querySelectorAll('[app-events6-send]')[0] as HTMLButtonElement)
  submitButton.addEventListener('click', async (event: Event) => {
    event.preventDefault();

    const eventBtn = event.currentTarget as HTMLElement;
    addTrackingEvent('trackEventsForm', 'events-form', eventBtn.innerText)
    
    var valid = validateForm(eventsWrapper, options)
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
    
    //#region request handling

    let endpoint = (eventsWrapper as HTMLElement).dataset.webservice // (should be "Form/ProcessForm" or a custom override)


    sendForm(formValues, submitButton, endpoint)
      .then((result: any) => {
        // error
        if(!result.ok) {
          if(debug) console.log('error', result.status);
    
          showAlert(eventsWrapper, 'msgError')
          showConfigWarnings(eventsWrapper, 'app-events6-config-warning')
          enableInputs(eventsWrapper)
    
          addTrackingEvent('trackEventsForm', 'events-form', submitButton.innerText)
          return
        }
        
        // success
        if(debug) console.log('success', result.json())
        submitButton.setAttribute("disabled", "")
  
        showAlert(eventsWrapper, 'msgOk')
        showConfigWarnings(eventsWrapper, 'app-events6-config-warning')
        disableInputs(eventsWrapper, false)
  
        addTrackingEvent('trackEventsForm', 'events-form', submitButton.innerText)
      })

    //#endregion
  })  
}
