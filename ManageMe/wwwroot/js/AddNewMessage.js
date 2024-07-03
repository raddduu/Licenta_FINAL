function checkAnnouncement(button) {
    if (button.attr("isAnnouncement") != "true") {
        button.attr("isAnnouncement", "true");
        button.removeClass("btn-outline-warning");
        button.addClass("btn-warning");
    } else {
        button.attr("isAnnouncement", "false");
        button.addClass("btn-outline-warning");
        button.removeClass("btn-warning");
    }
}

function AddNewMessage(messageText, channelId, parentMessageId, isAnnouncement) {
    $.ajax({
        type: "POST",
        url: `/Messages/AddNewMessage`,
        data: {
            text: messageText,
            channelId: channelId,
            isAnnouncement: isAnnouncement,
            parentMessageId: parentMessageId == undefined ? null : parentMessageId,
            messageTypeId: 1,
        },
        success: (result) => {
            location.reload();
        },
        error: (result) => {
            new ActionNotification("notificationsContainer", result.responseJSON.errorMessage || "Something went wrong! Please try again later!", "Error", 4000);
        }
    });
}

function sendRootMessage(channelId) {
    let messageText = $("#typeRootMessageBar").val();
    let isAnnouncement = $("#announcementCheckRoot").attr("isAnnouncement") == "true";
    AddNewMessage(messageText, channelId, undefined, isAnnouncement);
}