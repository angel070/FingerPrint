(function(b){"function"===typeof define&&define.amd?define(["jquery","datatables.net-bs","datatables.net-staterestore"],function(a){return b(a)}):"object"===typeof exports?module.exports=function(a,c){a||(a=window);c&&c.fn.dataTable||(c=require("datatables.net-bs")(a,c).$);c.fn.dataTable.StateRestore||require("datatables.net-staterestore")(a,c);return b(c)}:b(jQuery)})(function(b){var a=b.fn.dataTable;b.extend(!0,a.StateRestoreCollection.classes,{checkBox:"dtsr-check-box form-check-input",creationButton:"dtsr-creation-button btn btn-default",
creationForm:"dtsr-creation-form modal-body",creationText:"dtsr-creation-text modal-header",creationTitle:"dtsr-creation-title modal-title",nameInput:"dtsr-name-input form-control"});b.extend(!0,a.StateRestore.classes,{confirmationButton:"dtsr-confirmation-button btn btn-default",confirmationTitle:"dtsr-confirmation title modal-header",input:"dtsr-input form-control"});return a.stateRestore});