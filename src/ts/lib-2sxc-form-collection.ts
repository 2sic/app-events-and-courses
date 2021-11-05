/*
  This is a shared code used in various 2sxc apps. Make sure that they are in sync, so if you improve it, improve all 2sxc apps which use this. 
  ATM they are:
  - EventsAndCourses6
  The master with the newest / best version must always be the Gallery7, the others should then get a fresh copy.
  Because this is shared, all parameters like DOM-IDs etc. must be provided in the Init-call that it can work across apps
*/ 

// automatically build the send-object with all properties, 
// based on all form-fields which have a item-property=""
export function getFormValues(formWrapper: Element) {
  let data: any = {
    Files: []
  };

  const fields = formWrapper.querySelectorAll('input,textarea,select');
  fields.forEach((field: HTMLInputElement) => {
    const fieldKey = getFieldKey(field)
    if (!fieldKey) return
    data[fieldKey] = getFieldValue(field)
  });
  let filesLoaded = formWrapper.querySelector('input[type="file"]') ? false : true;
  
  let checkInterval = setInterval(() => {
    if(filesLoaded) {
      clearInterval(checkInterval);
      return data;
    }  
  })
}

function getFieldKey(formField: HTMLInputElement): string {
  // get the property name from special-attribut, name OR id
  return formField.getAttribute('name') || formField.getAttribute('id');
}

function getFieldValue(formField: HTMLInputElement): unknown {
  // extract data from file fields
  if (formField.getAttribute('type') && formField.getAttribute('type').toLowerCase() == 'file') {
    const file = formField.files[0];
    if (!file) {
      filesLoaded = true;
      return;
    }
      
    const reader = new FileReader();

    reader.addEventListener('load', function () {
      data.Files.push({
        Encoded: reader.result,
        Name: file.name,
        Field: propName
      });
    }, true);

    reader.onloadend = () => {  
      filesLoaded = true;
    }

    reader.readAsDataURL(file);
    
  } else if (formField.getAttribute('type') && formField.getAttribute('type').toLowerCase() == 'radio') { // For radio fields get checked values
    if (formField.checked) {
      data[propName] = formField.value;
    }
  } else if (formField.getAttribute('type') && formField.getAttribute('type').toLowerCase() == 'checkbox') { // For radio fields get checked values
    const checkValue = formField.checked ? "True" : "False";
    data[propName] = checkValue;
  } else { // For all standard fields, set value directly
    data[propName] = formField.value
  }      
}