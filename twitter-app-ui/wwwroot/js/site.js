// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var process = $('#process');
process.click(function () {
    var xhr;
    if (xhr && xhr.readyState != 4 && process.text() === 'STOP Processing') {
        xhr.abort();
        $('#process').text('Start Processing');
    }
    $('#process').text('STOP Processing');
    xhr = $.ajax({
        url: 'Home/StartProcessing',
        type: 'GET',
        dataType: 'json', // added data type
        success: function (res) {
            console.log(res);
            alert(res);
        }
    });
});