function disableClick(event) {
    event.preventDefault();
}

function HandleJoin(channelId, accessType, userId) {
    var button = $(`#${channelId}`);

    if (accessType == 'FreeAccess') {
        AddMemberToChannel(channelId);
    }

    if (accessType == 'JoinCode') {
        FadeBackground(true);
        new UserInput("Join this Channel", "Please enter the join code for this channel"
                        , "EnterInputPopUp", channelId);
    }

    if (accessType == 'JoinRequest') {
        $.ajax({
            type: "POST",
            url: "Channels/AddChannelParticipationRequest",
            data: {
                channelId: channelId,
                userId: userId
            },
            success: (result) => {
                if (result.success === true) {
                    new ActionNotification("notificationsContainer"
                        , "Your membership request had been succsessfully sent!"
                        , "Channel Request", 4000);
                    $(`.joinButton[id="${channelId}"]`)
                        .removeClass("joinButton")
                        .removeClass("btn-success")
                        .addClass("btn-secondary")
                        .addClass("pendingButton")
                        .text("Pending Request");
                }

                else {
                    new ActionNotification("notificationsContainer",
                        "Something went wrong!",
                        "Channel Request", 4000);
                }
            }
        })
    }


    
}
