

$.ajax({
    type: 'GET',
    url: '/Utility/GetPersonNoti',
    data: JSON.stringify(""),
    contentType: 'application/json; charset=utf-8',
    dataType: 'json',
    success: function (data) {
        for (var i = 0; i < data.length; i++) {
            $('.notiAdd').append('<div style="border:2px solid black" id="' + i
                + '"><a style="display:inline" href="User/index/' + data[i].CreatorId + '">'
                + data[i].NotificationCreator.FirstName
                + '</a><p style="display:inline" >' + " "
                + data[i].Text + '</p><a  href="Programs/index/'
                + data[i].ProgramId + '">' + " " + data[i].Program.Name + '</a></div>');
        }
    }, error: function (data, succes, error) {
        alert('Something went wrong, with your notfication, try and reload');
    }
});