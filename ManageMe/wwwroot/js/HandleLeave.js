function HandleLeave(channelId, userId) {
    var button = $(`#${channelId}`);

    $.ajax({
        type: "GET",
        url: `Channels/RemoveMember?channelId=${channelId}`,
        success: (result) => {
            button.removeClass("leaveButton");
            button.addClass("joinButton");
            button.text("Join Channel");

            new ActionNotification("notificationsContainer", "You have succsessfully left this group!", "Leave", 4000);
        },
        error: () => {
            new ActionNotification("notificationsContainer", "Something went wrong!", "Error", 4000);
        }
    }
    );


    

    
}