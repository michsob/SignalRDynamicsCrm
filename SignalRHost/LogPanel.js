$(function () {
    // Reference the auto-generated proxy for the hub.
    var logPanel = $.connection.logHub;
    // Create a function that the hub can call back to display messages.
    logPanel.client.addLog = function (name, date) {
        $('#logtable').append('<tr><td>' + name
            + '</td><td>' + date + '</td></tr>');
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        //calback
    });
});