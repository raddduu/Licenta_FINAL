function CalendarEdit(scope) {
    $('.schedule').each(function () {
        var currentSchedule = $(this);
        var currentScheduleRows = currentSchedule.find('tr');

        for (var i = 1; i < 6; i++) {
            var currentScheduleRow = currentScheduleRows.eq(i);
            var currentScheduleRowCells = currentScheduleRow.find('td');

            for (let j = 1; j < 13; j++) {
                let currentScheduleRowCell = currentScheduleRowCells.eq(j);
                let numberOfItems = 0;

                if (currentScheduleRowCell.attr('durations').split(' ')[0] != '') {
                    numberOfItems = currentScheduleRowCell.attr('durations').split(' ').length;
                }
                else {
                    continue;
                }

                let currentScheduleRowCellDurations = currentScheduleRowCell.attr('durations').split(' ');
                let currentScheduleRowCellBackgroundColors = currentScheduleRowCell.attr('backgroundColors').split(' ');
                let currentScheduleRowCellTextColors = currentScheduleRowCell.attr('textColors').split(' ');
                let currentScheduleRowCellActivityNames = currentScheduleRowCell.attr('activityNames').split(' ');
                let currentScheduleRowCellGroupNumbers = currentScheduleRowCell.attr('groupNumbers').split(' ');
                let currentScheduleRowCellSubjectNames = currentScheduleRowCell.attr('subjectNames').split(' ');
                let currentScheduleRowCellTeacherNames = currentScheduleRowCell.attr('teacherNames').split(' ');
                let currentScheduleRowCellDistributionIds = currentScheduleRowCell.attr('distributionIds').split(' ');
                let currentScheduleRowCellFrequencyIds = currentScheduleRowCell.attr('frequencyIds').split(' ');
                let currentScheduleRowCellHallNames = currentScheduleRowCell.attr('hallnames').split(' ');

                let lastScheduleRowCellHeight = 0;

                for (let k = 0; k < numberOfItems; k++) {
                    let currentScheduleRowCellDuration = currentScheduleRowCellDurations[k];
                    let currentScheduleRowCellBackgroundColor = currentScheduleRowCellBackgroundColors[k];
                    let currentScheduleRowCellTextColor = currentScheduleRowCellTextColors[k];
                    let currentScheduleRowCellActivityName = currentScheduleRowCellActivityNames[k];
                    let currentScheduleRowCellGroupNumber = currentScheduleRowCellGroupNumbers[k];
                    let currentScheduleRowCellSubjectName = currentScheduleRowCellSubjectNames[k];
                    let currentScheduleRowCellTeacherName = currentScheduleRowCellTeacherNames[k].replaceAll('-', ' ');
                    let currentScheduleRowCellDistributionId = currentScheduleRowCellDistributionIds[k];
                    let currentScheduleRowCellFrequencyId = currentScheduleRowCellFrequencyIds[k];
                    let currentScheduleRowCellHallName = currentScheduleRowCellHallNames[k].replaceAll('-', ' ');
                    let currentScheduleRowCellGroupId = currentScheduleRowCell.attr('groupIds').split(' ')[k];
                    let currentScheduleRowCellActivityId = currentScheduleRowCell.attr('activityIds').split(' ')[k];
                    let currentScheduleRowCellTeacherId = currentScheduleRowCell.attr('teacherIds').split(' ')[k];
                    let currentScheduleRowCellHallId = currentScheduleRowCell.attr('hallIds').split(' ')[k];
                    let currentScheduleRowCellSubjectId = currentScheduleRowCell.attr('subjectIds').split(' ')[k];

                    if (currentScheduleRowCellDuration != undefined && currentScheduleRowCellDuration != 0) {
                        let currentScheduleRowCellDurationInt = parseInt(currentScheduleRowCellDuration);

                        let heightRatio = 1;

                        if (currentScheduleRowCellDistributionId != '1') {
                            heightRatio *= 2;
                        }

                        if (currentScheduleRowCellFrequencyId != '1') {
                            heightRatio *= 2;
                        }

                        let currentScheduleRowCellDiv = $('<div></div>');
                        currentScheduleRowCellDiv.css('height', currentScheduleRowCell.height() / heightRatio + 2 / numberOfItems);
                        currentScheduleRowCellDiv.css('width', currentScheduleRowCell.width() * currentScheduleRowCellDurationInt + 2 * currentScheduleRowCellDurationInt + 1);
                        currentScheduleRowCellDiv.css('background-color', currentScheduleRowCellBackgroundColor);
                        currentScheduleRowCellDiv.css('color', currentScheduleRowCellTextColor);
                        currentScheduleRowCellDiv.css('position', 'absolute');
                        currentScheduleRowCellDiv.css('top', currentScheduleRowCell.position().top + lastScheduleRowCellHeight);
                        currentScheduleRowCellDiv.css('left', currentScheduleRowCell.position().left - 0.5);
                        currentScheduleRowCellDiv.css('z-index', '0');
                        currentScheduleRowCellDiv.css('border', `1px solid var(--primary)`);
                        if (k != 0) {
                            currentScheduleRowCellDiv.css('border-bottom', 'none');
                        }
                        if (k != numberOfItems - 1) {
                            currentScheduleRowCellDiv.css('border-top', 'none');
                        }

                        if (numberOfItems == 1) {
                            currentScheduleRowCellDiv.css('border-top', 'none');
                        }

                        currentScheduleRowCellDiv.addClass('filled');
                        currentScheduleRowCellDiv.attr('groupId', currentScheduleRowCellGroupId);
                        currentScheduleRowCellDiv.attr('activityId', currentScheduleRowCellActivityId);
                        currentScheduleRowCellDiv.attr('teacherId', currentScheduleRowCellTeacherId);
                        currentScheduleRowCellDiv.attr('hallId', currentScheduleRowCellHallId);
                        currentScheduleRowCellDiv.attr('subjectId', currentScheduleRowCellSubjectId);
                        currentScheduleRowCellDiv.attr('distributionId', currentScheduleRowCellDistributionId);
                        currentScheduleRowCellDiv.attr('frequencyId', currentScheduleRowCellFrequencyId);

                        lastScheduleRowCellHeight += currentScheduleRowCellDiv.height();

                        let frequnecyName = "";

                        switch (currentScheduleRowCellFrequencyId) {
                            case "2":
                                frequnecyName = " - EW";
                                break;
                            case "3":
                                frequnecyName = " - OW";
                                break;
                            default:
                                break;
                        }

                        let currentScheduleRowCellDivTeacherName = $('<div></div>');
                        currentScheduleRowCellDivTeacherName.css('position', 'absolute');
                        currentScheduleRowCellDivTeacherName.css('top', '2px');
                        currentScheduleRowCellDivTeacherName.css('left', '2px');
                        currentScheduleRowCellDivTeacherName.css('font-size', `${12 + 4 / heightRatio}px`);
                        currentScheduleRowCellDivTeacherName.text(currentScheduleRowCellTeacherName);

                        let currentScheduleRowCellDivHallName = $('<div></div>');
                        currentScheduleRowCellDivHallName.css('position', 'absolute');
                        currentScheduleRowCellDivHallName.css('top', '2px');
                        currentScheduleRowCellDivHallName.css('right', '2px');
                        currentScheduleRowCellDivHallName.css('font-size', `${12 + 4 / heightRatio}px`);
                        currentScheduleRowCellDivHallName.text(currentScheduleRowCellHallName);

                        let currentScheduleRowCellDivSubjectName = $('<div></div>');
                        currentScheduleRowCellDivSubjectName.css('position', 'absolute');
                        currentScheduleRowCellDivSubjectName.css('top', '50%');
                        currentScheduleRowCellDivSubjectName.css('left', '50%');
                        currentScheduleRowCellDivSubjectName.css('transform', 'translate(-50%, -50%)');
                        currentScheduleRowCellDivSubjectName.css('font-size', `${12 + 6 / heightRatio}px`);
                        currentScheduleRowCellDivSubjectName.text(currentScheduleRowCellSubjectName + ' (' + currentScheduleRowCellActivityName + ')' + frequnecyName);


                        let currentScheduleRowCellDivGroupName = $('<div></div>');
                        currentScheduleRowCellDivGroupName.css('position', 'absolute');
                        currentScheduleRowCellDivGroupName.css('bottom', '0px');
                        currentScheduleRowCellDivGroupName.css('left', '0px');
                        currentScheduleRowCellDivGroupName.css('font-size', '20px');
                        currentScheduleRowCellDivGroupName.text(currentScheduleRowCellGroupNumber);

                        if (scope == "teacher") {
                            currentScheduleRowCellDivGroupName.css('display', 'none');
                        }

                        let currentScheduleRowCellDivDistributionId = $('<div></div>');
                        currentScheduleRowCellDivDistributionId.css('position', 'absolute');
                        currentScheduleRowCellDivDistributionId.css('bottom', '0px');
                        currentScheduleRowCellDivDistributionId.css('right', '0px');
                        currentScheduleRowCellDivDistributionId.css('font-size', '10px');

                        if (currentScheduleRowCellDistributionId == 2 || currentScheduleRowCellDistributionId == 3) {
                            let distribution = "HG";

                            switch (currentScheduleRowCellDistributionId) {
                                case "2":
                                    distribution += "1";
                                    break;
                                case "3":
                                    distribution += "2";
                                    break;
                                default:
                                    break;
                            }

                            currentScheduleRowCellDivDistributionId.text(distribution);
                        }

                        currentScheduleRowCell.append(currentScheduleRowCellDiv);
                        currentScheduleRowCellDiv.append(currentScheduleRowCellDivTeacherName);
                        currentScheduleRowCellDiv.append(currentScheduleRowCellDivHallName);
                        currentScheduleRowCellDiv.append(currentScheduleRowCellDivSubjectName);
                        currentScheduleRowCellDiv.append(currentScheduleRowCellDivGroupName);
                        currentScheduleRowCellDiv.append(currentScheduleRowCellDivDistributionId);
                    }
                }

                currentScheduleRowCell.addClass('compiled');
            }
        }
    });


}
