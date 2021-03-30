// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var process = $('#process');
var xhr;
var interval;
process.click(function () {
    console.log(process.text());
    console.log('xhr', xhr);
    if (xhr && xhr.readyState != 4 || process.text() === 'STOP Processing') {
        xhr.abort();
        process.text('Start Processing');
        clearInterval(interval);
        resetValues();
    } else {
        $('#process').text('STOP Processing');
        xhr = $.ajax({
            url: 'Home/StartProcessing',
            type: 'GET',
            dataType: 'json', // added data type
            success: function (res) {
                console.log('Processing Message:', res);
            }
        });

        console.log('Setting Timer');
        interval = setInterval(function () {
            console.log('times up');
            $.ajax({
                url: 'Home/GetReportData',
                type: 'GET',
                dataType: 'json',
                success: function (res) {
                    console.log('Report Data:', res);
                    $('#number-of-tweets').text(res.totalNumberOfTweets);
                    $('#hour').text(res.averageNumberOfTweetsPerHour);
                    $('#minute').text(res.averageNumberOfTweetsPerMinute);
                    $('#second').text(res.averageNumberOfTweetsPerSecond);
                    $('#url-percent').text(res.percentOfTweetsThatContainUrl+ '%');
                    $('#photo-percent').text(res.percentOfTweetsThatContainPhotoUrl + '%');
                    $('#emoji-percent').text(res.percentOfTweetsThatContainEmojis + '%');
                }
            });
        }, 3000);// 5 seconds
    }
});

function resetValues() {
    xhr = null;
    interval = null;
}


