document.addEventListener('DOMContentLoaded', function () {
    var userName = prompt("Please, Enter your Name..");

    var messageInp = document.getElementById("messageInp");
    var groupNameInp = document.getElementById("groupNameInp");
    var messageToGroupInp = document.getElementById("messageToGroupInp");


    messageInp.focus();

    var proxyConnection = new signalR.HubConnectionBuilder().withUrl("/chat").build();


    proxyConnection.start().then(function () {
        document.getElementById("sendMessageBtn").addEventListener("click", function (e) {
            e.preventDefault();
            proxyConnection.invoke("Send", userName, messageInp.value)
        })
    }).catch(function (error) {
        console.log(error);
    });

    proxyConnection.on("ReceiveMessage", function (userName, message) {

        var listElm = document.createElement("li");

        listElm.innerHTML = `<strong>${userName} : </strong>  ${message}`;

        document.getElementById("conversation").appendChild(listElm);
    });



})