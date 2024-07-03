

function HandleDelete(itemId, deleteActionLink) {
    FadeBackground(true);

    function callback() {
        $.ajax({
            type: "POST",
            url: deleteActionLink,
            data: { id: itemId },
            success: (result) => {
                FadeBackground(false);
                $(".AlertPopUp").remove();
                if (result.success === true) {
                    let deletedItem = $(`.card[channel-id="${itemId}"]`);
                    deletedItem.remove();
                    new ActionNotification("notificationsContainer", "You have successfully deleted this channel!", "Delete", 4000);
                }
                else {
                    new ActionNotification("notificationsContainer", "Something went wrong!", "Delete", 4000);
                }
            }
        })
    }

    var message = "You are about to delete this item and this action is unreversable.\nAre you sure you want to do this ?";
    var status = "WARNING";
    new PopUp(status, message, callback, "Delete", "AlertPopUp");
}
