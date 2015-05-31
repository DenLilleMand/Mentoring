skip = 0;
    function searchUsersmore(dataUsers) {
        for (var i = 0; i < dataUsers.length; i++) {
            $('.personMore').append('<li><a class="outputSearch" href="/User/index/' + dataUsers[i].Id + '"><img src="~/pictures/b.png" class="searchPicture"/>' + dataUsers[i].FirstName + ' ' + dataUsers[i].LastName + '</a></li>');
        }


    }

function searchMentorProgramsmore(dataPrograms) {
    //  if (dataPrograms.length !== 0) {
    for (var i = 0; i < dataPrograms.length; i++) {
        $('.programsMore').append('<li><a class="outputSearch" href="/Programs/index/' + dataPrograms[i].Id + '"><img src="~/pictures/b.png" class="searchPicture"/>' + dataPrograms[i].Name + '</a></li>');
    }
}
      


    function getMoreSearchData() {
        var storageInput = sessionStorage.getItem("input");
        var test = { "input": storageInput, "take": 5, "skip":skip };
       $.ajax({
           type: 'POST',
           url: '/Utility/GetSearchData',
           data: JSON.stringify(test),
           contentType: 'application/json; charset=utf-8',
           dataType: 'json',
           success: function (data) {
               $('#preloader_1').hide();
               searchMentorProgramsmore(data[0]);
               searchUsersmore(data[1]);
               skip = skip + 5;
               window.scrollTo(0, document.body.scrollHeight);
              

           },
           error: function (data, succes, error) {
               alert('Something went wrong, try and search again');
           }
       });
   }

  


