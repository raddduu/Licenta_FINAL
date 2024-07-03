function createMultiSelect(values, containerId, subjectId, activityId) {
    var modal = document.getElementById(containerId);
    //get the modal with the id of containerId and set it to modal variable with jquerry
    //let modal = $(`#${containerId}`);
    modal.innerHTML = "";

    var title = document.createElement("h1");
    title.innerHTML = "Choose teachers to grant permissions to";
    modal.appendChild(title);

    let selectedTeachersCount = 0;

    var submitButton = document.createElement("button");
    submitButton.classList.add("btn");
    submitButton.classList.add("btn-success");
    submitButton.innerHTML = "Add Teacher Permissions";
    submitButton.onclick = () => getMultiSelectResult(modal, subjectId, activityId);
    modal.appendChild(submitButton);

    let cancelButton = document.createElement("button");
    cancelButton.innerHTML = "Cancel"
    $(cancelButton).addClass(["btn", "btn-light"]);
    cancelButton.addEventListener("click", function () {
        modal.style.display = "none";
        FadeBackground(false);
    });

    modal.appendChild(cancelButton);

    var numberOfOptions = values.length;
    for (let i = 0; i < numberOfOptions; i++) {
        let currentOption = values[i];

        let currentOptionDiv = document.createElement("div");
        currentOptionDiv.userId = currentOption.id;
        currentOptionDiv.classList.add("card-body");
        currentOptionDiv.classList.add("option");
        currentOptionDiv.classList.add("unselected");

        currentOptionName = document.createElement("p");
        currentOptionName.innerHTML = currentOption.name;
        currentOptionDiv.appendChild(currentOptionName);

        currentOptionEmail = document.createElement("p");
        currentOptionEmail.innerHTML = currentOption.email;
        currentOptionDiv.appendChild(currentOptionEmail);

        currentOptionDiv.IsSelected = 0;
        currentOptionDiv.onclick = function () {
            if (currentOptionDiv.IsSelected) {
                currentOptionDiv.IsSelected = 0;
                currentOptionDiv.classList.remove("selected");
                currentOptionDiv.classList.add("unselected");
                selectedTeachersCount--;
            }
            else {
                currentOptionDiv.IsSelected = 1;
                currentOptionDiv.classList.remove("unselected");
                currentOptionDiv.classList.add("selected");
                selectedTeachersCount++;
            }

            if (selectedTeachersCount == 0) {
                submitButton.innerHTML = "Add Teacher Permissions";
            }
            else {
                if (selectedTeachersCount == 1) {
                    submitButton.innerHTML = "Add 1 Teacher Permission";
                }
                else {
                    submitButton.innerHTML = `Add ${selectedTeachersCount} Teacher Permissions`;
                }
            }
        }
        modal.appendChild(currentOptionDiv)
    }
}

function AddTeacherPermissions(subjectId, activityId, teacherIds) {
    var numberOfOptions = teacherIds.length;
    var numberOfFailedItems = 0;

    var ajaxRequests = [];

    for (let i = 0; i < numberOfOptions; i++) {
        let teacherId = teacherIds[i];

        var request = $.ajax({
            type: "POST",
            url: "/TeacherPermissions/AddTeacherPermission",
            data: {
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
        $("#AddTeacherPermissionModal").css("display", "none");
        FadeBackground(false);
        localStorage.setItem("reloadPage", true);
        localStorage.setItem("failedAddsCount", numberOfFailedItems)

        location.reload();
    });
}


function getMultiSelectResult(selectionSection, subjectId, activityId) {
    let chosenUsersIds = []
    let optionFields = selectionSection.getElementsByClassName("option");
    let numberOfOptions = optionFields.length
    for (let i = 0; i < numberOfOptions; i++) {
        let currentOption = optionFields[i].IsSelected
        if (currentOption == 1) {
            chosenUsersIds.push(optionFields[i].userId)
        }
    }
    AddTeacherPermissions(subjectId, activityId, chosenUsersIds);
}

function MultipleCheckTeachers(subjectId, activityId) {
    $("#AddTeacherPermissionModal").css("display", "flex");
    $.ajax({
        type: "GET",
        url: `/Users/GetAllTeachersForActivity?activityId=${activityId}&subjectId=${subjectId}`,
        success: (result) => {
            createMultiSelect(result, "AddTeacherPermissionModal", subjectId, activityId);
        },
        error: () => {
            new ActionNotification("notificationsContainer", "Something went wrong!", "Error", 4000);
        }
    });
}


function LaunchTeacherSelection(subjectId, activityId) {
    FadeBackground(true);
    new MultipleCheckTeachers(subjectId, activityId);
}