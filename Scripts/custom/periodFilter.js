let period;

let years;

let periodSelector = document.getElementById("periodSelector");


let frequencySelector = document.getElementById('frequencySelector');

frequencySelector.addEventListener('change', () => {

    let selections = periodSelector.innerHTML === '<option>Select time ...</option>' ? periodSelector.innerHTML : '<option value="">Select time ...</option>';

    if(frequencySelector.value.length > 0) {
        result = filterPeriod(frequencySelector.value, 2022);
        result.forEach((period) => {
            selections = selections + '<option value="' + period.value + '">' + period.name + "</option>";
        });
    }

    periodSelector.innerHTML = selections;
    
})


periodSelector.addEventListener('change', () => {
    if (periodSelector.value.length > 0) {
        result = calculateDates(frequencySelector.value, periodSelector.value)

        //format dates
        let firstDateYear = new Date(result.firstDate).getFullYear();
        let firstDateMonth = String(new Date(result.firstDate).getMonth()+1).length > 1 ? new Date(result.firstDate).getMonth()+1 : "0" + (new Date(result.firstDate).getMonth()+1)
        let firstDateDay = String(new Date(result.firstDate).getDate()).length > 1 ? new Date(result.firstDate).getDate() : "0" + new Date(result.firstDate).getDate()

        let firstDate = firstDateYear + '-' + firstDateMonth + '-' + firstDateDay;
        document.getElementsByName("DateFrom")[0].value = firstDate;

        let lastDateYear = new Date(result.lastDate).getFullYear();
        let lastDateMonth = String(new Date(result.lastDate).getMonth() + 1).length > 1 ? new Date(result.lastDate).getMonth() + 1 : "0" + (new Date(result.lastDate).getMonth()+1)
        let lastDateDay = String(new Date(result.lastDate).getDate()).length > 1 ? new Date(result.lastDate).getDate() : "0" + new Date(result.lastDate).getDate()
        

        let lastDate = lastDateYear + '-' + lastDateMonth + '-' + lastDateDay;
        document.getElementsByName("DateTo")[0].value = lastDate;
    }
})



function filterPeriod(_periodType, year) {
    switch (_periodType) {
        case 'Yearly':
            let currrentyear = new Date().getFullYear();

            this.years = [
                {
                    name: currrentyear.toString(),
                    value: currrentyear,
                },
            ];

            for (let i = 1; i <= 20; i++) {
                this.years.push({
                    name: (currrentyear - i).toString(),
                    value: currrentyear - i,
                });
            }
            return this.years;

        case 'Monthly':
            return [
                {
                    name: 'January',
                    value: 0,
                },
                {
                    name: 'February',
                    value: 1,
                },
                {
                    name: 'March',
                    value: 2,
                },
                {
                    name: 'April',
                    value: 3,
                },
                {
                    name: 'May',
                    value: 4,
                },
                {
                    name: 'June',
                    value: 5,
                },
                {
                    name: 'July',
                    value: 6,
                },
                {
                    name: 'August',
                    value: 7,
                },
                {
                    name: 'September',
                    value: 8,
                },
                {
                    name: 'October',
                    value: 9,
                },
                {
                    name: 'November',
                    value: 10,
                },
                {
                    name: 'December',
                    value: 11,
                },
            ];
        case 'Weekly':
            return this.weeksInYear(year)
        default:
            return undefined;
    }
}

function calculateDates(_periodType, value, year) {
    year = year === undefined ? new Date().getFullYear() : year;
    let firstDateObject;
    let lastDateObject;
    let firstDate;
    let lastDate;

    switch (_periodType) {
        case 'Yearly':
            value = value === undefined ? new Date().getFullYear() : value;

            //Create date objects
            firstDateObject = new Date(value, 0, 1);
            lastDateObject = new Date(value, 11, 31);

            //Format dates accordingly
            firstDate =
                firstDateObject.getFullYear() +
                '-' +
                (firstDateObject.getMonth() + 1) +
                '-' +
                firstDateObject.getDate();
            lastDate =
                lastDateObject.getFullYear() +
                '-' +
                (lastDateObject.getMonth() + 1) +
                '-' +
                lastDateObject.getDate();

            return {
                firstDate: firstDate,
                lastDate: lastDate,
            };

        case 'Monthly':
            //Create date objects
            firstDateObject = new Date(year, value, 1);
            console.log('First Date Object: ', firstDateObject);

            lastDateObject = new Date(
                firstDateObject.getFullYear(),
                firstDateObject.getMonth() + 1,
                0
            );

            //Format dates accordingly
            firstDate =
                firstDateObject.getFullYear() +
                '-' +
                (firstDateObject.getMonth() + 1) +
                '-' +
                firstDateObject.getDate();
            lastDate =
                lastDateObject.getFullYear() +
                '-' +
                (lastDateObject.getMonth() + 1) +
                '-' +
                lastDateObject.getDate();

            return {
                firstDate: firstDate,
                lastDate: lastDate,
            };

        case 'Weekly':
            let weekObject = this.getDateRangeOfWeek(value, year);
            return {
                firstDate: weekObject.firstDate,
                lastDate: weekObject.lastDate,
            }
        default:
            return undefined;
    }
}


function weeksInYear(year) {
    let month;
    let week;
    let day;

    if (year === new Date().getFullYear()) {
        month = new Date().getMonth();
        day = new Date().getDate();
    } else {
        month = 11;
        day = 31;
    }

    // Find week that last date in a given year. If is first week, reduce date until
    // get previous week.
    if (year !== new Date().getFullYear()) {
        do {
            let d = new Date(year, month, day--);
            week = this.getWeekNumber(d);
        } while (week == 1);
    } else {
        week = this.getWeekNumber(new Date());
    }

    let weekObjects = [];

    for (let i = 1; i <= week; i++) {
        let weekValues = this.getDateRangeOfWeek(i, year);
        let weekObject = {
            name: `Week ${weekValues.weekNumber} (${weekValues.firstDate} - ${weekValues.lastDate})`,
            value: weekValues.weekNumber
        }
        weekObjects.push(weekObject);
    }

    return weekObjects;
}

function getWeekNumber(date) {
    // initialize a new date object
    let d = new Date(
        Date.UTC(date.getFullYear(), date.getMonth(), date.getDate())
    );

    let startDate = new Date(date.getFullYear(), 0, 1);
    var days = Math.floor(
        (date.getTime() - startDate.getTime()) / (24 * 60 * 60 * 1000)
    );

    let weekNumber = Math.ceil(days / 7);

    return weekNumber;
}

function getDateRangeOfWeek(weekNo, year) {
    var d1 = new Date(year, 11, 31);
    const numOfdaysPastSinceLastMonday = eval(String((d1.getDay() + 1) - 1));

    d1.setDate(d1.getDate() - numOfdaysPastSinceLastMonday);

    let weekNoToday = this.getWeekNumber(d1);
    let weeksInTheFuture = (weekNo - weekNoToday);

    d1.setDate(d1.getDate() + (7 * weeksInTheFuture));
    let rangeIsFrom = String(
        d1.getFullYear() + '-' + (d1.getMonth() + 1) + '-' + d1.getDate()
    );

    d1.setDate(d1.getDate() + 6);
    let rangeIsTo = String(
        d1.getFullYear() + '-' + (d1.getMonth() + 1) + '-' + d1.getDate()
    );

    return {
        firstDate: rangeIsFrom,
        lastDate: rangeIsTo,
        weekNumber: weekNo,
    };
}


