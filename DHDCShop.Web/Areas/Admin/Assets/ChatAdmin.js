$(function () {
    var chatHub = $.connection.chatHub;
    var selectedIndex = -1;
    
    //var currentUser = $("#usernameCurrent").val();

    chatHub.client.pushNewMessage = function (messageView) {
       
      
        var html = `
            <div class="d-flex justify-content-between">
                <p class="small mb-1 text-muted">${messageView.Timestamp}</p>
                <p class="small mb-1">${"ADMIN"}</p>
            </div>
            <div class="d-flex flex-row justify-content-end mb-4 pt-1">
                <div>
                    <p class="small p-2 me-3 mb-3 text-white rounded-3 bg-default">
                        ${messageView.Content}
                    </p>
                </div>
                <img src="../../Public/Assets/admin.png" 
                     alt="avatar 1" style="width: 45px; height: 100%;">
            </div>`;
        $(".chat-body").append(html);
        $(".chat-body").animate({ scrollTop: $(".chat-body")[0].scrollHeight }, 1000);
    };

    chatHub.client.getNewMessage = function (messageView) {
        var avatar = "../../" + messageView.Avatar.substring(2);
        var html = `
            <div class="d-flex justify-content-between">
                <p class="small mb-1">${messageView.Username}</p>
                <p class="small mb-1 text-muted">${messageView.Timestamp}</p>
            </div>
            <div class="d-flex flex-row justify-content-start">
                <img src="${avatar}"
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
        chatHub.server.getListChatHistory().done(function (result) {   
            ResetListEvent(result);           
        })

        $("#send-message").click(function (event) {
            chatHub.server.replayToCustomer($("#text-message").val(), $("#chat-list").children()[selectedIndex].dataset.value);
            $("#text-message").val("");
        })
    });

    function ResetListEvent(result) {
         
        //show list chat
        $("#chat-list").html("");
        if (result) {
         
            for (let i = 0; i < result.length; i++) {
                var chat = result[i];
                var avatar = "../../" + chat.Avatar.substring(2);

                var html =
                    `<li class="p-2 border-bottom" data-value="${chat.UserName}">
                        <a href="#!" class="d-flex justify-content-between">
                            <div class="d-flex flex-row">
                                <div>
                                    <img src="${avatar}"
                                            alt="avatar" class="d-flex align-self-center me-3" width="60">
                                    <span class="badge bg-success badge-dot"></span>
                                </div>
                                <div class="pt-1">
                                    <p class="fw-bold mb-0 ">${chat.UserName}</p>
                                </div>
                            </div>                                          
                        </a>
                    </li>`;
                $("#chat-list").append(html);
            }
            //show chat
            if (selectedIndex === -1) {
                selectedIndex = 0;
            }
            let selectedItem = $("#chat-list").children()[selectedIndex];
            selectedItem.classList.add("active");
            chatHub.server.getMessageHistory(selectedItem.dataset.value).done(function (res) {
                $(".chat-body").html("");
                if (res) {
                    for (let i = 0; i < res.length; i++) {
                        var messageView = res[i];
                        if (messageView.Type == 2) {
                            var html = `
                            <div class="d-flex justify-content-between">
                                <p class="small mb-1 text-muted">${messageView.Timestamp}</p>
                                <p class="small mb-1">${"ADMIN"}</p>
                            </div>
                            <div class="d-flex flex-row justify-content-end mb-4 pt-1">
                                <div>
                                    <p class="small p-2 me-3 mb-3 text-white rounded-3 bg-default">
                                        ${messageView.Content}
                                    </p>
                                </div>
                                <img src="../../Public/Assets/admin.png"
                                        alt="avatar 1" style="width: 45px; height: 100%;">
                            </div>`;
                            $(".chat-body").append(html);
                        } else {
                            var avatar = "../../" + messageView.Avatar.substring(2);
                            var html = `
                            <div class="d-flex justify-content-between">
                                <p class="small mb-1">${messageView.Username}</p>
                                <p class="small mb-1 text-muted">${messageView.Timestamp}</p>
                            </div>
                            <div class="d-flex flex-row justify-content-start">
                                <img src="${avatar}"
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
                }
            })

        }
        

        //event chat click
        var chatList = $("#chat-list").children();
        for (let i = 0; i < chatList.length; i++) {
            let chatItem = chatList[i];
            console.log(chatItem);

            chatItem.onclick = function (e) {
                selectedIndex = i;
                UnactiveAllItem();
                chatItem.classList.add("active");
                chatHub.server.getMessageHistory(chatItem.dataset.value).done(function (res) {
                    $(".chat-body").html("");
                    if (res) {
                        for (let i = 0; i < res.length; i++) {
                            var messageView = res[i];
                            if (messageView.Type == 2) {
                                var html = `
                                    <div class="d-flex justify-content-between">
                                        <p class="small mb-1 text-muted">${messageView.Timestamp}</p>
                                        <p class="small mb-1">${"ADMIN"}</p>
                                    </div>
                                    <div class="d-flex flex-row justify-content-end mb-4 pt-1">
                                        <div>
                                            <p class="small p-2 me-3 mb-3 text-white rounded-3 bg-default">
                                                ${messageView.Content}
                                            </p>
                                        </div>
                                        <img src="../../Public/Assets/admin.png"
                                             alt="avatar 1" style="width: 45px; height: 100%;">
                                    </div>`;
                                $(".chat-body").append(html);
                            } else {
                                var avatar = "../../" + messageView.Avatar.substring(2);

                                var html = `
                                    <div class="d-flex justify-content-between">
                                        <p class="small mb-1">${messageView.Username}</p>
                                        <p class="small mb-1 text-muted">${messageView.Timestamp}</p>
                                    </div>
                                    <div class="d-flex flex-row justify-content-start">
                                        <img src="${avatar}"
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
                    }
                })
            }
        }
    }
})
function UnactiveAllItem() {
    let chatList = $("#chat-list").children();
    for (let i = 0; i < chatList.length; i++) {
        let chatItem = chatList[i];
        chatItem.classList.remove("active");
    }
}