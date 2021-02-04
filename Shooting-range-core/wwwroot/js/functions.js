var $ = jQuery.noConflict();


$(document).ready(function () {
  
    if ($("#StartTime").length > 0) {
        $('#StartTime').timepicker({

        });
    }
    if ($("#EndTime").length > 0) {

        $('#EndTime').timepicker({
        });
    }

})

/* file upload plugin css end */

function trimStr(str, length) {
    str = jQuery.trim(str).substring(0, length) + "...";
    return str;
}



//})
$(document).ready(function () {
    if ($(".datepicker").length > 0) {

        $(".datepicker").datepicker({ format: 'yyyy-mm-dd' });
    }
})

$(document).ready(function () {
    $(".btn-pref .btn").click(function () {
        $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
        // $(".tab").addClass("active"); // instead of this do the below 
        $(this).removeClass("btn-default").addClass("btn-primary");
    });
});



    $(document).ready(function () {
        $(".printLink").click(function () {
            printSection('');
        });
    });

    //// Example starter JavaScript for disabling form submissions if there are invalid fields
    //(function () {
    //    'use strict';
    //    window.addEventListener('load', function () {
    //        // Fetch all the forms we want to apply custom Bootstrap validation styles to
    //        var forms = document.getElementsByClassName('needs-validation');
    //        // Loop over them and prevent submission
    //        var validation = Array.prototype.filter.call(forms, function (form) {
    //            form.addEventListener('submit', function (event) {
    //                if (form.checkValidity() === false) {
    //                    event.preventDefault();
    //                    event.stopPropagation();
    //                }
    //                form.classList.add('was-validated');
    //            }, false);
    //        });
    //    }, false);
    //})();
