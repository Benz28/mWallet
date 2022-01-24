var myInput = document.getElementById("InputPassword");
//var length = document.getElementById("length");
//var both = document.getElementById("both");

// Show the message when user click the password field
//myInput.onfocus = function () {
//    document.getElementById("message").style.display = "block";
//}

//// When the user clicks outside of the password field, hide the message box
//myInput.onblur = function () {
//    document.getElementById("message").style.display = "none";
//}


//myInput.onkeyup = function () {

//    var letterS = /[A-z]/g;
//    var numberS = /[0-9]/g;

//    if (myInput.value.length >= 8) {
//        length.classList.remove("invalid");
//        length.classList.add("valid");
//    }
//    else {
//        length.classList.remove("valid");
//        length.classList.add("invalid");
//    }

//    if (myInput.value.match(letterS) && myInput.value.match(numberS)) {
//        both.classList.remove("invalid");
//        both.classList.add("valid");
//    }
//    else {
//        both.classList.remove("valid");
//        both.classList.add("invalid");
//    }
//}

//function numOnly(evt) {
//    var charCode = (evt.which) ? evt.which : event.keyCode
//    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
//        return false;
//    }
//    return true;
//}

function validate() {
    var rePass = document.getElementById("InputCfmPassword").value;
    var pass = document.getElementById("InputPassword").value;

    if (pass != rePass) {
        alert("Passwords do not match.");
        return false;
    }
    else {
        return true;
    }
}











