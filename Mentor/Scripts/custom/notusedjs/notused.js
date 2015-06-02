﻿
/**
 * Author: Jon
 */
    var programs = [];
var program = {};
//constructor for programs
function Program(imageSrc, title, desc, visb, creaId, progId) {
    this.imageSource = imageSrc;
    this.description = desc;
    this.visibility = visb;
    this.creatorId = creaId;
    this.programId = progId;
    this.title = title;
}

program = new Program('/Images/program1.jpg', "brain enhancment through play", "this is a good program", true, 2, 6);
programs.push(program);
program = new Program('/Images/program2.jpg', "oldies basket", "this is an excellent program", true, 2, 7);
programs.push(program);
program = new Program('/Images/program3.jpg', "home tatooing", "this is a really good program", true, 2, 6);
programs.push(program);
program = new Program('/Images/program2.jpg', "basket for real", "this is an excellent program", true, 2, 7);
programs.push(program);
program = new Program('/Images/program3.jpg', "chess", "this is a good program", true, 2, 6);
programs.push(program);
program = new Program('/Images/program1.jpg', "chess", "this sdfnoaep wraeijfo<åsdfkjaf casdofi jcasdfcasådfopsdmf licfoc åasdfkmxsad fciasdoåfsdvkzmxbmndog sdfåbzmxcb kdsgvåsdpvl<m is an excellent program", true, 2, 7);
programs.push(program);
program = new Program('/Images/program1.jpg', "skate", "this is a really good program", true, 2, 6);
programs.push(program);
program = new Program('/Images/program2.jpg', "basket", "this is an excellent program", true, 2, 7);
programs.push(program);

for (var i = 0; i < programs.length; i++) {
    //a container for the shown program
    var containerLi = document.createElement("li");
    containerLi.className = "col-md-3 programDiv col-centered";

    //content divs
    var title = document.createElement("h3");
    title.innerHTML = programs[i].title;
    title.className = "title-programs";

    var imageDiv = document.createElement("div");
    imageDiv.className = "thumbnail imageDiv";
    //link div
    var link = document.createElement("a");
    link.href = "Javascript:void(0)";

    var image = document.createElement("img");
    image.setAttribute("height", "360");
    image.setAttribute("width", "240");
    image.setAttribute("src", programs[i].imageSource);
    link.appendChild(image);
    link.appendChild(title);
    imageDiv.appendChild(link);

    containerLi.appendChild(imageDiv);

    //give the containerId the id of the program
    containerLi.id = programs[i].programId;

    //add the containerDiv to the document
    document.getElementById("myPrograms").appendChild(containerLi);

    var clonedLi = containerLi.cloneNode(true);
    //just for display
    document.getElementById("suggestedPrograms").appendChild(clonedLi);
}
