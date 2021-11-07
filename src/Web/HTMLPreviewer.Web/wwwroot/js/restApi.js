function executeRun(jsonBody, token, iframe) {
    $.ajax({
        url: "/HTMLExamples/Run",
        type: "POST",
        data: JSON.stringify(jsonBody),
        contentType: "application/json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            iframe.setAttribute('srcdoc', data.htmlContent);
        },
        error: function (xhr, status, error) {
            for (let i = 0; i < xhr.responseJSON.errors.HTMLContent.length; i++) {
                Toastify({
                    text: xhr.responseJSON.errors.HTMLContent[i],
                    style: {
                        background: "red",
                    },
                    duration: 3000,
                    newWindow: true,
                    gravity: "top",
                    position: "right",
                }).showToast();
            }
        }
    });
}

function executeCheckOriginal(jsonBody, token) {
    $.ajax({
        url: "/HTMLExamples/CheckWithOriginal",
        type: "POST",
        data: JSON.stringify(jsonBody),
        contentType: "application/json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (isOriginal) {
            if (isOriginal) {
                Toastify({
                    text: "The same as original one.",
                    duration: 3000,
                    newWindow: true,
                    gravity: "top",
                    position: "right",
                }).showToast();
            }
            else {
                Toastify({
                    text: "Different from the original one.",
                    duration: 3000,
                    newWindow: true,
                    gravity: "top",
                    position: "right",
                }).showToast();
            }
        },
        error: function (xhr, status, error) {
            Toastify({
                text: error,
                style: {
                    background: "red",
                },
                duration: 3000,
                newWindow: true,
                gravity: "top",
                position: "right",
            }).showToast();
        }
    });
}

function executeEdit(jsonBody, token) {
    $.ajax({
        url: "/HTMLExamples/Edit",
        type: "POST",
        data: JSON.stringify(jsonBody),
        contentType: "application/json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (isEdited) {
            if (isEdited) {
                Toastify({
                    text: "Successfully Edited.",
                    style: {
                        background: "green",
                    },
                    duration: 3000,
                    newWindow: true,
                    gravity: "top",
                    position: "right",
                }).showToast();
            }
        },
        error: function (xhr, status, error) {
            for (let i = 0; i < xhr.responseJSON.errors.HTMLContent.length; i++) {
                Toastify({
                    text: xhr.responseJSON.errors.HTMLContent[i],
                    style: {
                        background: "red",
                    },
                    duration: 3000,
                    newWindow: true,
                    gravity: "top",
                    position: "right",
                }).showToast();
            }
        }
    });
}