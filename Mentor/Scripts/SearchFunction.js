function searchUsers(dataUsers) {
    if (dataUsers.length !== 0) {
        for (var i = 0; i <dataUsers.length; i++) {
          
            $('.Persons').append('<li><a class="outputSearch" href="/User/index/' + dataUsers[i].Id + '"><img src="~/pictures/b.png" class="searchPicture"/>' + dataUsers[i].FirstName + ' ' + dataUsers[i].LastName + '</a></li>');

            $('#noResultPerson').hide();

        }
    } else {

        $('#noResultPerson').show();

    }

}
function searchMentorPrograms(dataPrograms) {
    if (dataPrograms.length !== 0) {
        //tæl kun de 5 første
        //dataPrograms.length
        for (var i = 0; i < dataPrograms.length; i++) {
           
            $('.Interest').append('<li><a class="outputSearch" href="/Programs/index/' + dataPrograms[i].Id + '"><img src="~/pictures/b.png" class="searchPicture"/>' + dataPrograms[i].Name + '</a></li>');
            $('#noResultMentorPrograms').hide();
        }
        
    } else {

        $('#noResultMentorPrograms').show();

    }

}

function searchForThings() {
    //show the searches
  
        var inputSearchText = $("#inputSearch").val();
    
    

    if (inputSearchText === "") {
        $('.searchField').hide();
    }
    else {
        var test = { 'input': inputSearchText, "take":3,"skip":0 };
        
            $.ajax({
                type: 'POST',
                url: '/Utility/GetSearchData',
                data: JSON.stringify(test),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function(data) {
                   
                    $('.searchHeaders').children().remove();
                    $('.searchField').show();
                    searchMentorPrograms(data[0]);
                    searchUsers(data[1]);

                },
                error: function(data, succes, error) {
                    alert('Something went wrong, try and search again');
                }
            });

       


    }
    $('#searchResult').text("Show more results for: " + inputSearchText);
}