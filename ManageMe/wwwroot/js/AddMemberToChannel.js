function ReplaceJoinButtonWithLeave(buttonId)
{
    var button = document.getElementById(buttonId);
    button.classList.remove("joinButton");
    button.classList.add("leaveButton");
    button.innerHTML = "Leave Channel";
}

function AddMemberToChannel(channelId) {
    console.log("DA");
    $.ajax({
        type: "GET",
        url: `Channels/AddNewMember?channelId=${channelId}`,
        success: (result) => {
            ReplaceJoinButtonWithLeave(channelId);
            new ActionNotification("notificationsContainer", "You have succsessfully joined this group!", "Leave", 4000);
        },
        error: () => {
            new ActionNotification("notificationsContainer", "Something went wrong!", "Error", 4000);
        }
    });
}