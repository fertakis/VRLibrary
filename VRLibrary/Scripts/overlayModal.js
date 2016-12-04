var modal = document.getElementById('myModal');
var modalImg = document.getElementById("modalImage");
var modalTitle = document.getElementById("modalTitle");
var modatAurthor = document.getElementById("modalAuthor");
var modalPublisher = document.getElementById("modalPublisher");
var modalISBN = document.getElementById("modalISBN");
var modalLibrary = document.getElementById("modalLibrary");
var modalShelf = document.getElementById("modalShelf");
var modalDescription = document.getElementById("modalDescription");
var modalRating = document.getElementById("modalRating");
var modalAvailiable = document.getElementById("modalAvailiable");
var modalReserved = document.getElementById("modalReserved");
var modalLoaned = document.getElementById("modalLoaned");
var modalfacebookShare = document.getElementById("modalfacebookShare");
var span = document.getElementsByClassName("close")[0];

function modalFunction(id,num) {
    var img = document.getElementById(id+"+"+num);
    var title = document.getElementById(id + "Title+" + num)
    var author = document.getElementById(id + "Author+" + num)
    var Publisher = document.getElementById(id + "Publisher+" + num)
    var ISBNnum = document.getElementById(id + "ISBN+" + num)
    var Library = document.getElementById(id + "Library+" + num)
    var Shelf = document.getElementById(id + "Shelf+" + num)
    var Description = document.getElementById(id + "Description+" + num)
    var Rating = document.getElementById(id + "Rating+" + num)
    var Availiable = document.getElementById(id + "Availiable+" + num)
    var Reserved = document.getElementById(id + "Reserved+" + num)
    var Loaned = document.getElementById(id + "Loaned+" + num)
    modal.style.display = "block";
    modalImg.src = img.src;
    modalTitle.innerHTML = title.innerHTML;
    modatAurthor.innerHTML = author.innerHTML;
    modalPublisher.innerHTML = Publisher.innerHTML;
    modalISBN.innerHTML = ISBNnum.innerHTML;
    modalLibrary.innerHTML = Library.innerHTML;
    modalShelf.innerHTML = Shelf.innerHTML;
    modalDescription.innerHTML = Description.innerHTML;
    modalRating.innerHTML = Rating.innerHTML;
    modalAvailiable.innerHTML = Availiable.innerHTML;
    modalReserved.innerHTML = Reserved.innerHTML;
    modalLoaned.innerHTML = Loaned.innerHTML;
    var facebookimglink = img.src;
    facebookimglink = facebookimglink.replace(":", "%3A");
    facebookimglink = facebookimglink.replace("/", "%2F");
    modalfacebookShare.href = "https://www.facebook.com/sharer/sharer.php?u=" + facebookimglink + "&amp;src=sdkpreparse";
}

span.onclick = function () {
    modal.style.display = "none";
}
document.onkeydown = function (evt) {
    if (evt.keyCode == 27) { // escape key maps to keycode `27`
        modal.style.display = "none";
    }
}