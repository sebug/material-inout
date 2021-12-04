// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const processEAN = async (ean) => {
    console.log(ean);
};

/**
 * Deals with the EAN being submitted
 * @param {Event} ev the submitted event
 */
const onEANSubmitted = (ev) => {
    const eanInput = ev.target.querySelector('input[name="ean"]');
    processEAN(eanInput.value);
    return false;
};

const eanReadoutForms = Array.from(document.querySelectorAll('#eanreadout'));

for (let eanReadoutForm of eanReadoutForms) {
    eanReadoutForm.addEventListener('submit', onEANSubmitted);
}