
function createMultiSelect(data, modalId, subjectId, activityId, groupId) {
    let modal = document.getElementById(modalId);
    modal.innerHTML = "";

    var title = document.createElement("h1");
    title.innerHTML = "Choose teachers to add to this group";
    modal.appendChild(title);

    var submitButton = document.createElement("button");
    submitButton.classList.add("btn");
    submitButton.classList.add("btn-success");
    submitButton.innerHTML = "Add Teachers to group";
    submitButton.onclick = () => getMultiSelectResult(modal, subjectId, activityId, groupId);
    modal.appendChild(submitButton);

    let cancelButton = document.createElement("button");
    cancelButton.innerHTML = "Cancel"
    $(cancelButton).addClass(["btn", "btn-light"]);
    cancelButton.addEventListener("click", function () {
        modal.style.display = "none";
        FadeBackground(false);
    });

    modal.appendChild(cancelButton);

    let numberOfOptions = data.length;
    for (let i = 0; i < numberOfOptions; i++) {
        let currentOption = data[i];

        let currentOptionDiv = document.createElement("div");
        currentOptionDiv.userId = currentOption.id;
        currentOptionDiv.classList.add("card-body");
        currentOptionDiv.classList.add("option");
        currentOptionDiv.classList.add("unselected");

        let currentOptionName = document.createElement("p");
        currentOptionName.innerHTML = currentOption.name;
        currentOptionDiv.appendChild(currentOptionName);

        let currentOptionEmail = document.createElement("p");
        currentOptionEmail.innerHTML = currentOption.email;
        currentOptionDiv.appendChild(currentOptionEmail);

        currentOptionDiv.IsSelected = 0;
        currentOptionDiv.onclick = function () {
            if (currentOptionDiv.IsSelected) {
                currentOptionDiv.IsSelected = 0;
                currentOptionDiv.classList.remove("selected");
                currentOptionDiv.classList.add("unselected");
            }
            else {
                currentOptionDiv.IsSelected = 1;
                currentOptionDiv.classList.remove("unselected");
                currentOptionDiv.classList.add("selected");
            }
        }
        modal.appendChild(currentOptionDiv)
    }
}

function AddTeachersToGroup(groupId, subjectId, activityId, teacherIds) {
    var numberOfOptions = teacherIds.length;
    var numberOfFailedItems = 0;

    var ajaxRequests = [];

    for (let i = 0; i < numberOfOptions; i++) {
        let teacherId = teacherIds[i];

        var request = $.ajax({
            type: "POST",
            url: "/Groups/AddTeacherToGroup",
            data: {
                groupId: groupId,
                teacherId: teacherId,
                subjectId: subjectId,
                activityId: activityId
            },
            success: (result) => {
                if (result.success !== true) {
                    numberOfFailedItems++;
                }
            },
            error: () => {
                numberOfFailedItems++;
            }
        });
        ajaxRequests.push(request);
    }

    $.when.apply($, ajaxRequests).done(function () {
        $("#AddTeacherToGroupModal").css("display", "none");
        FadeBackground(false);
        localStorage.setItem("reloadPage", true);
        localStorage.setItem("failedTeacherAddsCount", numberOfFailedItems)
        localStorage.setItem("succsessTeacherAddsCount", numberOfOptions - numberOfFailedItems)
        location.reload();
    });
}

function getMultiSelectResult(modal, subjectId, activityId, groupId) {
    let selectedOptions = modal.getElementsByClassName("selected");
    let numberOfOptions = selectedOptions.length;
    let teacherIds = [];

    for (let i = 0; i < numberOfOptions; i++) {
        let currentOption = selectedOptions[i];
        teacherIds.push(currentOption.userId);
    }
    AddTeachersToGroup(groupId, subjectId, activityId, teacherIds);
}



function MultipleCheckTeachersForGroup(groupId, subjectId, activityId) {
    $("#AddTeacherToGroupModal").css("display", "flex");
    $.ajax({
        type: "GET",
        url: "/Users/GetTeachersForSubjectActivity",
        data: {
            subjectId: subjectId,
            activityId: activityId,
            groupId: groupId
        },
        success: (result) => {
            createMultiSelect(result, "AddTeacherToGroupModal", subjectId, activityId, groupId);
        },
        error: () => {
            new ActionNotification("notificationsContainer", "Something went wrong!", "Error", 4000);
        }
    });
}


function LaunchTeacherSelectionForGroup(groupId, subjectId, activityId) {
    FadeBackground(true);
    new MultipleCheckTeachersForGroup(groupId, subjectId, activityId);
}