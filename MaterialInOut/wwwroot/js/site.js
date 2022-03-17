// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const processEAN = async (ean) => {
    const fetchResponse = await fetch('/api/MaterialItems/' + ean);
    const responseJSON = await fetchResponse.json();

    if (responseJSON) {
        const ul = document.querySelector('#scanned-material');
        const li = document.createElement('li');
        li.innerHTML = '<b>' + responseJSON.label + '</b> ' + responseJSON.mnemonic + ' (' + ean + ')';
        ul.appendChild(li);
    }
};

/**
 * Deals with the EAN being submitted
 * @param {Event} ev the submitted event
 */
const onEANSubmitted = (ev) => {
    const eanInput = ev.target.querySelector('input[name="ean"]');
    processEAN(eanInput.value);
    ev.preventDefault();
    eanInput.value = '';
    return false;
};

const eanReadoutForms = Array.from(document.querySelectorAll('#eanreadout'));

for (let eanReadoutForm of eanReadoutForms) {
    eanReadoutForm.addEventListener('submit', onEANSubmitted);
    const eanInput = document.querySelector('input[name="ean"]');
    if (eanInput) {
        eanInput.focus();
    }
}