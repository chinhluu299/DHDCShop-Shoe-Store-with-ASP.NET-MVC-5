$(function () {
    var chatHub = $.connection.chatHub;
   
    var currentUser = $("#usernameCurrent").val();
    
    if (currentUser != "") {
        chatHub.client.pushNewMessage = function (messageView) {
            var avatar = "../../" + messageView.Avatar.substring(2);
            var html = `
            <div class="d-flex justify-content-between">
                <p class="small mb-1 text-muted">${messageView.Timestamp}</p>
                <p class="small mb-1">${messageView.Username}</p>
            </div>
            <div class="d-flex flex-row justify-content-end mb-4 pt-1">
                <div>
                    <p class="small p-2 me-3 mb-3 text-white rounded-3 bg-default">
                        ${messageView.Content}
                    </p>
                </div>
                <img src="${avatar}"
                     alt="avatar 1" style="width: 45px; height: 100%;">
            </div>`;
            $(".chat-body").append(html);
            $(".chat-body").animate({ scrollTop: $(".chat-body")[0].scrollHeight }, 1000);
        };

        chatHub.client.getNewMessage = function (messageView) {
            console.log(messageView);
            var html = `
            <div class="d-flex justify-content-between">
                <p class="small mb-1">${"ADMIN"}</p>
                <p class="small mb-1 text-muted">${messageView.Timestamp}</p>
            </div>
            <div class="d-flex flex-row justify-content-start">
                <img src="../../Public/Assets/admin.png"
                        alt="avatar 1" style="width: 45px; height: 100%;">
                <div>
                    <p class="small p-2 ms-3 mb-3 rounded-3" style="background-color: #f5f6f7;">
                        ${messageView.Content}
                    </p>
                </div>
            </div>`;
            $(".chat-body").append(html);
            $(".chat-body").animate({ scrollTop: $(".chat-body")[0].scrollHeight }, 1000);
        };

        $.connection.hub.start().done(function () {
            chatHub.server.getMessageHistory(currentUser).done(function (result) {

                $(".chat-body").html("");
                if (result) {
                    for (let i = 0; i < result.length; i++) {
                        var messageView = result[i];
                        if (messageView.Type == 1) {
                            var avatar = "../../" + messageView.Avatar.substring(2);
                            var html = `
                            <div class="d-flex justify-content-between">
                                <p class="small mb-1 text-muted">${messageView.Timestamp}</p>
                                <p class="small mb-1">${messageView.Username}</p>
                            </div>
                            <div class="d-flex flex-row justify-content-end mb-4 pt-1">
                                <div>
                                    <p class="small p-2 me-3 mb-3 text-white rounded-3 bg-default">
                                        ${messageView.Content}
                                    </p>
                                </div>
                                <img src="${avatar}"
                                     alt="avatar 1" style="width: 45px; height: 100%;">
                            </div>`;
                            $(".chat-body").append(html);
                        } else {
                            var html = `
                            <div class="d-flex justify-content-between">
                                <p class="small mb-1">${"ADMIN"}</p>
                                <p class="small mb-1 text-muted">${messageView.Timestamp}</p>
                            </div>
                            <div class="d-flex flex-row justify-content-start">
                                <img src="../../Public/Assets/admin.png"
                                      alt="avatar 1" style="width: 45px; height: 100%;">
                                <div>
                                     <p class="small p-2 ms-3 mb-3 rounded-3" style="background-color: #f5f6f7;">
                                         ${messageView.Content}
                                     </p>
                                </div>
                            </div>`;
                            $(".chat-body").append(html);
                        }
                    }
                    $(".chat-body").animate({ scrollTop: $(".chat-body")[0].scrollHeight }, 0);
                    if ($("chat-box-expand").hasClass("d-none") == false) {
                        $("#chat-box-expand").addClass("d-none");
                    }
                    $("#chat-box-expand").removeClass("opacity-0");

                }
                

            })

            $("#button-addon2").click(function (event) {
                chatHub.server.sendToAdmin($("#text-message").val(), currentUser);
                $("#text-message").val("");
            })
        });
    }
})

