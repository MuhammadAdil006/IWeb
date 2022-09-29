

$(document).ready(function () {
   
    var btn = document.getElementById("PostAcomment");
 
    
    document.addEventListener('click', function (e) {
        var like = e.target;
        //deleting a comment
        if (like.classList.contains('DeleteComment')) {
            /*console.log("ind delete commenting compared success");*/
            DeleteComment(like);
        }
        if (like.classList.contains("CommentEditbtn")) {
            //checking if input field is empty or not
            if (!UpdatedCommentEmpty(like)) {
                //just update using ajax and change node at runtime

                UpdateComment(like);
            } else {
                //use alert to display 
                var divForAlert = like.parentElement.parentElement.parentElement.parentElement.parentElement.children[0];
                divForAlert.innerHTML = "You can not update comment with empty comment.Try updating.";
                divForAlert.classList.remove("d-none");
                
               
                setTimeout(function () {
                    divForAlert.classList.add('d-none');
                    divForAlert.innerHTML = "You can not delete this comment.";
         
                }, 2000);
            }
            
        }
        //editing a comment
        if (like.classList.contains('EditComment')) {
            //editing a user comment
            EditComment(like);
        }

        if (like.classList.contains('postbtn')) {
            var divSection = like.parentElement.parentElement;
            PostAComment(like.id, divSection,like);
        }
        else if (like.classList.contains('getcomments')) {
            if (like.classList.contains('getcomments_child')) {
                like = like.parentNode;
            }
            if (!like.classList.contains('collapsed')) {
                console.log(like);
          
                GetAllcomments(like.firstElementChild.value);
            }
    
            
        }
    });

    //this function will help to edit the user commented post

    function EditComment(obj) {
        
        var wholeCommentNode = obj.parentElement.parentElement.parentElement.parentElement.parentElement;
        var PosterId = wholeCommentNode.children[2].value;
        var MessageId = wholeCommentNode.children[3].value;
        var CommentId = wholeCommentNode.children[4].value;
        //now we have to check the validity of edit either the true owner is editing the comment or not
        //getting the logged in user id
        var CurrentUserId = document.getElementById("CurrentUserId").value;
        var divForAlert = wholeCommentNode.parentElement.parentElement.parentElement.children[0];
        console.log("in editing");
        if (PosterId == CurrentUserId) {
            
            //change whole comment node to editable form node.

            ChangeCommentNode(wholeCommentNode);
      

        } else {
            //display an alert u can not delete this comment
            console.log("in else");
            divForAlert.classList.remove("d-none");
            divForAlert.innerHTML="Sorry You can not edit this comment."
            setTimeout(function () {
                divForAlert.classList.add('d-none');
                divForAlert.innerHTML = "You can not delete this comment."
            }, 2000);
        }
    }
    //this function will delete the comment
    function DeleteComment(obj) {
        console.log(obj);
        var wholeCommentNode = obj.parentElement.parentElement.parentElement.parentElement.parentElement;
       
        var PosterId = wholeCommentNode.children[2].value;
        var MessageId = wholeCommentNode.children[3].value;
        var CommentId = wholeCommentNode.children[4].value;
        //now we have to check the validity of deletion either the true owner is deleting the comment or not
        //getting the logged in user id
        var CurrentUserId = document.getElementById("CurrentUserId").value;
        var divForAlert = wholeCommentNode.parentElement.parentElement.parentElement.children[0];
        console.log("in deleiton");

        if (PosterId == CurrentUserId) {
            //further go on with this deletion
            var f = new FormData();
            f.append("MessageId",  MessageId);
            f.append("CurrentUserId", CurrentUserId);
            f.append("CommentId", CommentId);
            $.ajax({
                type: "Post",
                url: "/Comment/DeleteComment",
                contentType: false,
                processData: false,
                data: f,
                success: function (data) {
                    console.log(data);

                    /*alert(data);*/
                    //now delete the node at runtime
                    var parentWholenode = wholeCommentNode.parentElement;
                    parentWholenode.removeChild(wholeCommentNode);
                    
                    divForAlert.innerHTML = "Your Comment has been deleted from post.";
                    divForAlert.classList.remove("d-none");
                    divForAlert.classList.remove("alert-danger");
                    divForAlert.classList.add('alert-success');
                    setTimeout(function () {
                        divForAlert.classList.add('d-none');
                        divForAlert.innerHTML = "You can not delete this comment.";
                        divForAlert.classList.add("alert-danger");
                        divForAlert.classList.remove('alert-success');
                    }, 2000);

                    //also decrement the comment no
                    console.log(parentWholenode);
                    var NoOfcommentNOde = parentWholenode.parentElement.parentElement.parentElement.children[0].children[0].children[0];
                    console.log(NoOfcommentNOde);
                    IncDecComment(NoOfcommentNOde, 2);
                },
                error: function (e) {
                    alert(e);
                    alert("getting comment deletion error Reload the page and try again");

                }
            });

        } else {
            //display an alert u can not delete this comment
            console.log("in else");
            divForAlert.classList.remove("d-none");
            setTimeout(function () {
                divForAlert.classList.add('d-none');
            }, 2000);
        }
    }
    function GetAllcomments(id) {
        var f = new FormData();
        f.append("postid", id);
        console.log(id);
        $.ajax({
            type: "Post",
            url: "/Comment/GetAllComments",
            contentType: false,
            processData: false,
            data: f,
            success: function (data) {
                displayComments(data,id);
                
                /*alert(data);*/

            },
            error: function (e) {
                alert(e);
                alert("getting comments error");

            }
        });

    }
    function PostAComment(id, divSection, postbtn) {
        console.log(divSection);
        var main = divSection.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.children[0].children[1].children[0].children[2].children[0];
        console.log(main);
        var srcImage = main.children[0].src;
        var name = main.children[1].innerHTML;
        var commentsection = divSection.parentElement.parentElement.children[0].children[0].children[0];
        var inputField = postbtn.parentElement.children[1];
        //posting a comment 
        console.log("in post a comment");
        var f = new FormData();
        var cid= "CommentatorId".concat(id)
        var pid = "CommentatorPostId".concat(id)
        var mid = "comment_Msg".concat(id)
        var id = document.getElementById(cid).value;
        var pid = document.getElementById(pid).value;
        var message = document.getElementById(mid).value;
        f.append('SenderId', id );
        f.append("PostId", pid);
        f.append("message", message);
        console.log(message);
        var children = divSection.children[0].cloneNode(true);
        var children2 = divSection.children[0].cloneNode(true);
    
        if (message.length != 0) {
            $.ajax({
                type: "POST",
                url: "/Comment/AddComment",
                contentType: false,
                processData: false,
                data: f,
                success: function (data) {

                    console.log(children);
                    console.log(data);
                    data = JSON.parse(data);
                    console.log(data);
                    //error here in json format
                    var msg = data["msg"].Msg;
                    var SenderId = data["msg"].SenderId;
                    var MessageId = data["msg"].Id;
                    children.classList.remove('d-none');
                
                    var img = children.children[0];
                    children.children[1].children[1].innerHTML = name;
                    children.children[1].children[2].innerHTML = msg;
                    img.src = srcImage;
                    children.children[2].value = data["msg"].SenderId;
                    children.children[3].value = data["msg"].Id;
                    children.children[4].value = data["com"].Id;
                   
                    divSection.children[1].prepend(children);

                    //incrementing comment counter
                    IncDecComment(commentsection, 1);
                    //clearing the input field
                    console.log(inputField);
                    inputField.value = "";
                },
                error: function (e) {
                    alert(e);
                    alert("comment error");

                }
            });
        } else {
            //your comment should not be empty
        }
    };
    //this function will increment and decrement the comment counter on post
    function IncDecComment(obj, a) {
        var value = obj.innerHTML;
        var replaced = value.replace(/\D/g, '');
        if (a == 1) {
            replaced = parseInt(replaced) + 1;
        } else {
            replaced = parseInt(replaced) - 1;
        }
        obj.innerHTML = replaced + " " + "Comments";
    }
    //thhis fuunciton will display all the comments in posts.
    function displayComments(data, id) {
        var ele = "collapseExample_".concat(id);
        
        var element = document.getElementById(ele);
        console.log(element);
        element = element.getElementsByTagName('div')[1];
        var copied = element.children[0].cloneNode(true);
        var lastcopied = element.lastElementChild.cloneNode(true);
        element.innerHTML = "";
        element.appendChild(lastcopied);
        var hr = document.createElement("hr");
        element.prepend(hr);
        element.prepend(copied);
        var div = document.createElement("div");
        
       /* div.style.width = "100px";*/
        div.style.maxHeight = "350px";
        div.style.overflowY = "auto";
        if (data.length != 0) {
            data = JSON.parse(data);
            //remove all child exceopt 1st
          
            data.forEach(function (obj) {
                var copied = element.children[0].cloneNode(true);
                var node = changeContents(copied, obj);
                //node.children[2].nodevalue = "4";
                //console.log(node.children[2]);
                div.prepend(node);
            });
            element.prepend(div);
            element.prepend(copied);
            
        } else {
            //no comments
        }
    }
    



    //utility funcitons
    function changeContents(copied, data) {
        copied.classList.remove('d-none');
        console.log(data);
        var img = copied.children[0];
        copied.children[1].children[1].innerHTML = data["firstname"].concat(" ", data["lastname"]);
        copied.children[1].children[2].innerHTML = data["msg"].Msg;
        img.src = "/".concat(data["imageUrl"]);
        copied.children[2].setAttribute('value', data["msg"].SenderId);
        copied.children[3].setAttribute('value', data["msg"].Id);
        copied.children[4].setAttribute('value', data["com"].Id);
        
        return copied;
        
    }

    //this funcition will change the whole comment node to editable comment node

    function ChangeCommentNode(wholeCommentNode) {
        //delete edit icon has been removed as edit mode is turned on for that comment

        var DeleteEditicon = wholeCommentNode.children[1].children[0];
        //1st retriving its parent so to add input and button
        var parent = wholeCommentNode.children[1];
        //now deleting
        console.log(DeleteEditicon);
        console.log(wholeCommentNode.children[1]);
        wholeCommentNode.children[1].removeChild(DeleteEditicon);
        //previous message
        var previousMessage = wholeCommentNode.children[1].children[1].innerHTML; //after removing
        wholeCommentNode.children[1].removeChild(wholeCommentNode.children[1].children[1]);
        //creating input element
        var input = document.createElement("input");
        input.setAttribute('type', 'text');
        input.setAttribute('class', 'form-control border-1 rounded-pill bg-gray');
        input.setAttribute('value', previousMessage);

        //creating input button for edit

        var button = document.createElement("button");
        button.setAttribute('type', 'button');
        button.setAttribute('class', 'btn btn-primary rounded-pill CommentEditbtn');
        button.innerHTML = "Edit";
        console.log(parent);
        //adding both to parent
        parent.append(input);
        parent.append(button);
        input.focus();
    }

    //this function will update comment in db
    function UpdateComment(obj) {
        var message = obj.parentElement.children[1].value;
        var messageId = obj.parentElement.parentElement.children[3].value;
        var f = new FormData();
        f.append("message", message);
        f.append("messageId", messageId);
        $.ajax({
            type: "Post",
            url: "/Comment/ModifyComment",
            contentType: false,
            processData: false,
            data: f,
            success: function (data) {
                console.log(data);

           
                /*now update the node at runtime*/
                var cloneNode = obj.parentElement.parentElement.parentElement.parentElement.children[0].cloneNode(true);

                //changing contents

                cloneNode.classList.remove('d-none');
                var img = cloneNode.children[0];
               
                cloneNode.children[1].children[1].innerHTML = obj.parentElement.children[0].innerHTML;
                cloneNode.children[1].children[2].innerHTML = message;
                img.src = obj.parentElement.parentElement.children[0].src;
                cloneNode.children[2].setAttribute('value', obj.parentElement.parentElement.children[2].value);
                cloneNode.children[3].setAttribute('value', messageId);
                cloneNode.children[4].setAttribute('value', obj.parentElement.parentElement.children[4].value);

                //node contents created

                var previousnode = obj.parentElement.parentElement;
                var commentSection = previousnode.parentElement;

                commentSection.replaceChild(cloneNode, previousnode);

                
            },
            error: function (e) {
                alert(e);
                alert("getting comment updation error Reload the page and try again");

            }
        }
        );

    }
    //this funciton will check whether entered comment is empty or not
    function UpdatedCommentEmpty(obj) {
        console.log("updated")
        console.log(obj.parentElement.children[1]);
        var inputF = obj.parentElement.children[1].value;
        if (inputF.length != 0) {
            return false;
        }
        return true;
    }
});