function log() {
    let result = document.getElementById('results');
    result.innerHTML = "";

    Array.prototype.forEach.call(arguments, function (msg){
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        } else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null,2);
        }
        result.innerHTML = msg;
    });
}


let config = {
    authority: "http://localhost:5000",
    client_id: "mgxhtml5client",
    redirect_uri: "http://localhost:5550/callback.html",
    response_type: "code",
    scope:"openid profile mgxserver api12",
    post_logout_redirect_uri : "http://localhost:5550/index.html",
};

let mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }else {
        log("User not logged in");
    }
});


function login() {
    mgr.signinRedirect();
}

function api() {
    mgr.getUser().then(function (user) {
        let url = "http://localhost:5001/identity";
        let xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        };
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}


document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);


document.getElementById("sendButton").disabled = true;
mgr.getUser().then(function (user) {

    function getAccessToken() {
        return user.access_token;
    }

    var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/chat", {
        accessTokenFactory: getAccessToken
    }).build();

    connection.on("ReceiveMessage", function (user, message) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = user + " says " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("sendButton").addEventListener("click", function (event) {
        var message = document.getElementById("messageInput").value;
        connection.invoke("SendMessage", message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
});




//Disable send button until connection is established




