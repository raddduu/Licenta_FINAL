function AddTeacher(subjectId, activityId, teacherId) {
    $.ajax({
        url: "/Subject/AddTeacher",
        type: "POST",
        data: {
            subjectId: subjectId,
            teacherId: teacherId,
            activityId: activityId
        },
        success: function (data) {
            $("#addTeacherModal").html(data);
            $("#addTeacherModal").modal("show");
        }
    });
}