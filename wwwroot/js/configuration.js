var connection =
    new signalR.HubConnectionBuilder()
        .withUrl('/chatHub').build();
(function () {
    connection.start()
        .catch(error => {
            console.error(error.message);
        });
})()