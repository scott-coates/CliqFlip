
function InitInbox() {
    var options = {
        header: "div.header",
        autoHeight: false,
        collapsable: true,
        active: false,
        change: function (event, ui) {
            ui.newHeader.find("p").remove();
            var href = ui.newHeader.find("a").attr("href");
            var conversationMessages = ui.newContent.find("div.conversation-messages");

            $.ajax({
                url: href,
                cache: false,
                dataType: "html",
                success: function (data) {
                    conversationMessages.html(data);
                    ShowLastConversationMessage(conversationMessages);
                }
            });
        }
    };
    $("#conversations").accordion(options);
}

function ShowLastConversationMessage(container) {
    var lastMessage = container.find("div.message").last().focus();
    container.animate({ scrollTop: lastMessage.offset().top }, 'fast');
}

function OnSuccessfulReply(form, msgContainer) {
    form[0].reset();
    ShowLastConversationMessage(msgContainer);
}