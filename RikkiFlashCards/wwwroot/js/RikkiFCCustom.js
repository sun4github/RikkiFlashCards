var isTagOpened = false;

function addCodeTag(elementId) {
    var curElementVal = document.getElementById(elementId).value;
    var appendForwardNL = (curElementVal.length > 0) ? ("\r\n") : ("");
    document.getElementById(elementId).value += appendForwardNL + "<code>\r\n\r\n</code>\r\n";
}

function addCustomLinkTag(elementId) {
    var curElementVal = document.getElementById(elementId).value;
    var appendForwardNL = (curElementVal.length > 0) ? ("\r\n") : ("");
    document.getElementById(elementId).value += " [link][/link]";
}

function addCustomBoldTag(elementId) {
    var curElementVal = document.getElementById(elementId).value;
    var appendForwardNL = (curElementVal.length > 0) ? ("\r\n") : ("");
    document.getElementById(elementId).value += " [bold][/bold]";
}

function stripBRtoRN() {
    var codeBlocks = document.getElementsByName("codeBlock");
    codeBlocks.forEach(x => {
        x.innerHTML = x.innerHTML.replace(/<br>/g, "\r\n");
        x.innerHTML = escapeHtml(x.innerHTML);
    });

}

function escapeHtml(text) {
    alert('text input ' + text)
    var map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#039;'
    };

    //var replacedText = text.replace(/[&<>"']/g, function (m) { alert('found: ' + m); alert('replaced with: ' + map[m]); return map[m]; });
    var replacedText = text.replace(/[&<>"']/g, function (m) { return map[m]; });
    alert('text output ' + replacedText);
    return replacedText;
}


function TickingTimer(startDateTime) {
    // Get today's date and time
    var now = new Date().getTime();

    // Find the distance between now and the start date from past
    var distance = now - startDateTime;

    // Time calculations for hours, minutes and seconds
    var hours = Math.floor(distance / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    // Output the result in an element with id="tickingTimer"
    document.getElementById("tickingTimer").innerHTML = hours + "h " + minutes + "m " + seconds + "s ";
}

function CountTimeElapsedInSession(startDateTime) {  
    TickingTimer(startDateTime);
    // Update the count down every 5 seconds
    var x = setInterval(TickingTimer, 5000, startDateTime);
}


function readURL(input) {
    if (input.files) {
        var prevImgCount = $('.upImg').length;        
        for (var i = 0; i < input.files.length; i++) {
            var fileName = input.files[i].name;
            alert(fileName);
            (function (fileName, inputFile) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var imgId = fileName;
                    $('#imageArea').append('<img id="' + imgId + '" class="upImg" width="100" height="100"  />');
                    $('#' + imgId).attr('src', e.target.result);
                }
                reader.readAsDataURL(inputFile); // convert to base64 string
            }).call(this, i + prevImgCount, input.files[i]);
        }
    }


    //if (input.files && input.files[0]) {
    //    var reader = new FileReader();

    //    reader.onload = function (e) {
    //        $('#blah').attr('src', e.target.result);
    //    }

    //    reader.readAsDataURL(input.files[0]); // convert to base64 string
    //}
}

