$(document).ready(function() {
    $('#q80').change(function() {
        if($("#q80 option:selected").val() == 'Other'){
            $('.positionOther').show();
        }else{
            $('.positionOther').hide();
        }
    });
});