var routeURL = location.protocol + "//" + location.host;

$(document).ready(function () {
    $("#startDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });

    InitializeCalendar();
});

function InitializeCalendar() {
    try {

        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                }
            });
            calendar.render();
        }
       

    }
    catch (e) {
        alert(e);
    }
}

function onShowModal(obj, isEventDetails) {
    $("#appointmentInput").modal("show");
}

function onCloseModal() {
    $("#appointmentInput").modal("hide");
}

function onSubmitForm() {
    var requestData = {
        Id: parseInt($("#id").val()),
        FullName: $("#fullName").val(),
        Age: $("#age").val(),
        AdditionalNotes: $("#additionalNotes").val(),
        StartDate: $("#startDate").val(),
    }

    $.ajax({
        url: routeURL + '/api/Appointment/SaveAppointmentData',
        type: 'POST',
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (response) {
            if (response.status === 1 || response.status === 2) {
                calendar.refetchEvents();
                $.notify(response.message, "success");
                onCloseModal();
            }
            else {
                $.notify(response.message, "error")
            }
        },
        error: function (xhr) {
            $.notify("Error", "error")
        }
    });
}