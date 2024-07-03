async function FetchDataIntoSelect(selectId, httpGetAction) {
    var selectRegion = document.getElementById(selectId);
    selectRegion.innerHTML = "";

    return fetch(`/${httpGetAction}`, {
        method: "get"
    })
        .then(response => response.json())
        .then(resp => {
            //debugger;
            for (var option of resp) {
                var newOption = document.createElement("option")
                newOption.text = option["text"];
                newOption.value = option["value"];
                selectRegion.appendChild(newOption);
            }
        });
}

async function Y(groupId, dayOfWeek, hour, minute) {
    //debugger;
    async function UpdateActivitySelect() {
        let subjectId = $('#SelectSubject').val();

        if (subjectId == null) {
            subjectId = $('#SelectSubject option:first-child').val();
        }

        await FetchDataIntoSelect('SelectActivity', `Schedules/GetAllActivities?subjectId=${subjectId}&groupId=${groupId}`);
        $('#SelectActivity').parent().removeClass('d-none');

        await UpdateTeacherSelect();
        await UpdateClassroomSelect();
    }

    async function UpdateSelectGroup() {
        let subjectId = $('#SelectSubject').val();
        let activityId = $('#SelectActivity').val();
        let teacherId = $('#SelectTeacher').val();

        if (activityId == 4) {
            await FetchDataIntoSelect('SelectGroup', `Groups/GetGroupsWhereUserIsLectorAtThisSubject?userId=${teacherId}&subjectId=${subjectId}`);
            $('#SelectGroup').parent().removeClass('d-none');
        }
        else {
            $('#SelectGroup').parent().addClass('d-none');
        }
    }

    async function UpdateTeacherSelect() {
        let subjectId = $('#SelectSubject').val();

        if (subjectId == null) {
            subjectId = $('#SelectSubject option:first-child').val();
        }

        let activityId = $('#SelectActivity').val();

        if (activityId == null) {
            activityId = $('#SelectActivity option:first').val();
        }

        await FetchDataIntoSelect('SelectTeacher', `Users/GetAllTeachersThatTeachThisActivityAtThisGroup?subjectId=${subjectId}&activityId=${activityId}&groupId=${groupId}`);

        $('#SelectTeacher').parent().removeClass('d-none');
    }

    async function UpdateClassroomSelect() {
        var distributionId = $('#SelectDistribution').val();

        if (distributionId == null) {
            distributionId = $('#SelectDistribution option:first').val();
        }

        var day = dayOfWeek;

        var duration = $('#DurationInput').val();

        if (duration == null || duration == 0) {
            duration = 2;
        }

        var frequencyId = $('#SelectFrequency').val();

        if (frequencyId == null) {
            frequencyId = $('#SelectFrequency option:first').val();
        }

        var teacherId = $('#SelectTeacher').val();

        if (teacherId == null) {
            teacherId = $('#SelectTeacher option:first').val();
        }

        var activityId = $('#SelectActivity').val();

        if (activityId == null) {
            activityId = $('#SelectActivity option:first').val();
        }

        await FetchDataIntoSelect('SelectClassroom', `Halls/GetAllHalls?distributionId=${distributionId}&day=${day}&hour=${hour}&activityId=${activityId}&duration=${duration}&frequencyId=${frequencyId}&groupId=${groupId}`);
        $('#SelectClassroom').parent().removeClass('d-none');
    }

    $('#SelectSubject').change(async function () {
        await UpdateActivitySelect();
        //$('#SelectTeacher').parent().addClass('d-none');
        //$('#SelectClassroom').parent().addClass('d-none');
    });

    $('#SelectActivity').change(async function () {
        await UpdateClassroomSelect();
        await UpdateTeacherSelect();
        await UpdateSelectGroup();
    });

    $('#SelectDistribution').change(async function () {
        await UpdateClassroomSelect();
    });

    $('#SelectFrequency').change(async function () {
        await UpdateClassroomSelect();
    });

    $('#DurationInput').change(async function () {
        await UpdateClassroomSelect();
    });

    $('#submitButton').click(function () {
        var subjectId = $('#SelectSubject').val();
        var activityId = $('#SelectActivity').val();
        var teacherId = $('#SelectTeacher').val();
        var hallId = $('#SelectClassroom').val();
        var distributionId = $('#SelectDistribution').val();
        var duration = $('#DurationInput').val();
        var frequencyId = $('#SelectFrequency').val();
        var groupIds = $('#SelectGroup').val();

        if (groupIds == null || groupIds.length == 0) {
            groupIds = [groupId]
        }

        //alert(groupIds);

        for (var i = 0; i < groupIds.length; i++) {
            var currentGroupId = groupIds[i];

            $.ajax({
                type: "POST",
                url: "/Schedules/Create",
                data: {
                    groupId: currentGroupId,
                    dayOfWeek: dayOfWeek,
                    hour: hour,
                    minute: minute,
                    subjectId: subjectId,
                    activityId: activityId,
                    teacherId: teacherId,
                    hallId: hallId,
                    distributionId: distributionId,
                    duration: duration,
                    activityFrequencyId: frequencyId
                },
                success: function (data) {
                    location.reload();
                },
                error: function (data) {
                    new ActionNotification("notificationsContainer", "Something went wrong! Please try again later!", "Error", 4000);
                }
            });
        }
    });

    $('#cancelButton').click(function () {
        $('#AddScheduleModal').css('display', 'none');
        FadeBackground(false);
    });

    //debugger;

    await FetchDataIntoSelect("SelectSubject", `Subjects/GetAllSubjects?groupId=${groupId}`);
    await FetchDataIntoSelect("SelectDistribution", 'Schedules/GetAllDistributions');
    await FetchDataIntoSelect("SelectFrequency", 'Schedules/GetAllActivityFrequencies');

    //debugger;

    await UpdateActivitySelect();

    //FetchDataIntoSelect("SelectActivity", `Schedules/GetAllActivities?subjectId=${subjectId}&groupId=${groupId}`);

    UpdateTeacherSelect();

    UpdateClassroomSelect();

    //debugger;
}