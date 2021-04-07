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
        resetValues();
    } else {
        $('#process').text('STOP Processing');
        xhr = $.ajax({
            url: 'Home/StartProcessing',
            type: 'GET',
            dataType: 'json', // added data type
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
                    $('#second').text(res.averageNumberOfTweetsPerSecond);
                    $('#minute').text(res.averageNumberOfTweetsPerMinute);                
                    $('#url-percent').text(res.percentOfTweetsThatContainUrl+ '%');
                    $('#photo-percent').text(res.percentOfTweetsThatContainPhotoUrl + '%');
                    $('#emoji-percent').text(res.percentOfTweetsThatContainEmojis + '%');
                    ToHtmlList($('#emoji-list'), res.top10EmojisInTweets);
                    ToHtmlList($('#domains-list'), res.top10UrlTweets);
                    ToHtmlList($('#hashtag-list'), res.top10HashTags);             
                },
                error: function (err) {
                    alert("We are not able to process your request at this time.");
                    resetValues();
                }
            });
        }, 3000);// 3 seconds
    }
});

function ToHtmlList(element, collection)
{
    //clear the element
    element.empty();
    console.log('Collection', collection)
    $.each(collection, function (key, value) {
        element.append("<li>" + key + "<span> : " + value + "</span></li>")
    });
}

function resetValues() {
    xhr.abort();
    process.text('Start Processing');
    clearInterval(interval);
    xhr = null;
    interval = null;
}


